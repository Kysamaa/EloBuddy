using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AddonTemplate;
using Aka_s_Vayne_reworked.Functions;
using SharpDX;
using ClipperLib;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using Color = System.Drawing.Color;
using Path = System.Collections.Generic.List<ClipperLib.IntPoint>;
using Paths = System.Collections.Generic.List<System.Collections.Generic.List<ClipperLib.IntPoint>>;
using GamePath = System.Collections.Generic.List<SharpDX.Vector2>;

namespace Aka_s_Vayne_reworked.Logic
{
    internal class QProvider
    {

        /// <summary>
        /// Gets the Lucian E Position position using a patented logic!
        /// </summary>
        /// <returns></returns>
        public Vector3 GetQPosition()
        {
            #region The Required Variables

            var positions = DashHelper.GetRotatedQPositions();
            var enemyPositions = DashHelper.GetEnemyPoints();
            var safePositions = positions.Where(pos => !enemyPositions.Contains(pos.To2D())).ToList();
            var BestPosition = ObjectManager.Player.ServerPosition.Extend(Game.CursorPos, 300f);
            var AverageDistanceWeight = .60f;
            var ClosestDistanceWeight = .40f;

            var bestWeightedAvg = 0f;

            var highHealthEnemiesNear =
                EntityManager.Heroes.Enemies.Where(m => !m.IsMelee && m.IsValidTarget(1300f) && m.HealthPercent > 7).ToList()
                    ;

            var alliesNear = EntityManager.Heroes.Allies.Count(ally => !ally.IsMe && ally.IsValidTarget(1500f, false));

            var enemiesNear =
                EntityManager.Heroes.Enemies.Where(
                    m => m.IsValidTarget(Variables._Player.GetAutoAttackRange(m) + 300f + 65f)).ToList();

            #endregion

            #region 1 Enemy around only Defensive

            if (ObjectManager.Player.CountEnemiesInRange(1500f) <= 1 && MenuManager.ComboMenu["Qmode2"].Cast<Slider>().CurrentValue == 2)
            {
                //Logic for 1 enemy near
                var backwardsPosition =
                    (ObjectManager.Player.ServerPosition.To2D() + 300f*ObjectManager.Player.Direction.To2D()).To3D();

                if (!other.UnderEnemyTower((Vector2) backwardsPosition))
                {
                    return backwardsPosition;
                }

            }

            #endregion

            #region 1 Enemy around only Aggressive

            if (ObjectManager.Player.CountEnemiesInRange(1500f) <= 1 && MenuManager.ComboMenu["Qmode2"].Cast<Slider>().CurrentValue == 1)
            {
                //Logic for 1 enemy near
                var position = (Vector3)ObjectManager.Player.ServerPosition.Extend(Game.CursorPos, 300f);
                return position.IsSafeEx() ? position : Vector3.Zero;
            }

            #endregion

            #region Alone, 2 Enemies, 1 Killable

            if (enemiesNear.Count() <= 2)
            {
                if (
                    enemiesNear.Any(
                        t =>
                            t.Health + 15 <
                            ObjectManager.Player.GetAutoAttackDamage(t) +
                            Variables._Player.GetSpellDamage(t, SpellSlot.Q)
                            && t.Distance(ObjectManager.Player) < Variables._Player.GetAutoAttackRange(t) + 80f))
                {
                    var QPosition =
                        ObjectManager.Player.ServerPosition.Extend(
                            highHealthEnemiesNear.OrderBy(t => t.Health).First().ServerPosition, 300f);

                    if (!other.UnderEnemyTower(QPosition))
                    {
                        return (Vector3) QPosition;
                    }
                }
            }

            #endregion

            #region Alone, 2 Enemies, None Killable

            if (alliesNear == 0 && highHealthEnemiesNear.Count() <= 2)
            {
                //Logic for 2 enemies Near and alone

                //If there is a killable enemy among those. 
                var backwardsPosition =
                    (ObjectManager.Player.ServerPosition.To2D() + 300f*ObjectManager.Player.Direction.To2D()).To3D();

                if (!other.UnderEnemyTower((Vector2) backwardsPosition))
                {
                    return backwardsPosition;
                }
            }

            #endregion

            #region Already in an enemy's attack range.

            var closeNonMeleeEnemy =
                DashHelper.GetClosestEnemy((Vector3) ObjectManager.Player.ServerPosition.Extend(Game.CursorPos, 300f));

            if (closeNonMeleeEnemy != null
                && ObjectManager.Player.Distance(closeNonMeleeEnemy) <= closeNonMeleeEnemy.AttackRange - 85
                && !closeNonMeleeEnemy.IsMelee)
            {
                return ObjectManager.Player.ServerPosition.Extend(Game.CursorPos, 300f).IsSafeEx2()
                    ? (Vector3) ObjectManager.Player.ServerPosition.Extend(Game.CursorPos, 300f)
                    : Vector3.Zero;
            }

            #endregion

            #region Logic for multiple enemies / allies around.

            foreach (var position in safePositions)
            {
                var enemy = DashHelper.GetClosestEnemy(position);
                if (!enemy.IsValidTarget())
                {
                    continue;
                }

                var avgDist = DashHelper.GetAvgDistance(position);

                if (avgDist > -1)
                {
                    var closestDist = ObjectManager.Player.ServerPosition.Distance(enemy.ServerPosition);
                    var weightedAvg = closestDist*ClosestDistanceWeight + avgDist*AverageDistanceWeight;
                    if (weightedAvg > bestWeightedAvg && position.IsSafeEx())
                    {
                        bestWeightedAvg = weightedAvg;
                        BestPosition = (Vector2) position;
                    }
                }
            }

            #endregion

            var endPosition = (BestPosition.IsSafe2()) ? BestPosition : Vector2.Zero;

            #region Couldn't find a suitable position, tumble to nearest ally logic

            if (endPosition == Vector2.Zero)
            {
                //Try to find another suitable position. This usually means we are already near too much enemies turrets so just gtfo and tumble
                //to the closest ally ordered by most health.
                var alliesClose =
                    EntityManager.Heroes.Allies.Where(ally => !ally.IsMe && ally.IsValidTarget(1500, false)).ToList();
                if (alliesClose.Any() && enemiesNear.Any())
                {
                    var closestMostHealth =
                        alliesClose.OrderBy(m => m.Distance(ObjectManager.Player))
                            .ThenByDescending(m => m.Health)
                            .FirstOrDefault();

                    if (closestMostHealth != null
                        &&
                        closestMostHealth.Distance(
                            enemiesNear.OrderBy(m => m.Distance(ObjectManager.Player)).FirstOrDefault())
                        >
                        ObjectManager.Player.Distance(
                            enemiesNear.OrderBy(m => m.Distance(ObjectManager.Player)).FirstOrDefault()))
                    {
                        var tempPosition = ObjectManager.Player.ServerPosition.Extend(closestMostHealth.ServerPosition,
                            300f);
                        if (tempPosition.IsSafeEx2())
                        {
                            endPosition = tempPosition;
                        }
                    }

                }

            }

            #endregion

            #region Couldn't even tumble to ally, just go to mouse

            if (endPosition == Vector2.Zero)
            {
                var mousePosition = ObjectManager.Player.ServerPosition.Extend(Game.CursorPos, 300f);
                if (mousePosition.IsSafe2())
                {
                    endPosition = mousePosition;
                }
            }

            #endregion

            return (Vector3) endPosition;
        }
    }

    public static class GameObjects
    {
        #region Static Fields

        /// <summary>
        ///     The ally heroes list.
        /// </summary>
        private static readonly List<AIHeroClient> AllyHeroesList = new List<AIHeroClient>();

        /// <summary>
        ///     The ally inhibitors list.
        /// </summary>
        private static readonly List<Obj_BarracksDampener> AllyInhibitorsList = new List<Obj_BarracksDampener>();

        /// <summary>
        ///     The ally list.
        /// </summary>
        private static readonly List<Obj_AI_Base> AllyList = new List<Obj_AI_Base>();

        /// <summary>
        ///     The ally minions list.
        /// </summary>
        private static readonly List<Obj_AI_Minion> AllyMinionsList = new List<Obj_AI_Minion>();

        /// <summary>
        ///     The ally shops list.
        /// </summary>
        private static readonly List<Obj_Shop> AllyShopsList = new List<Obj_Shop>();

        /// <summary>
        ///     The ally spawn points list.
        /// </summary>
        private static readonly List<Obj_SpawnPoint> AllySpawnPointsList = new List<Obj_SpawnPoint>();

        /// <summary>
        ///     The ally turrets list.
        /// </summary>
        private static readonly List<Obj_AI_Turret> AllyTurretsList = new List<Obj_AI_Turret>();

        /// <summary>
        ///     The ally wards list.
        /// </summary>
        private static readonly List<Obj_AI_Minion> AllyWardsList = new List<Obj_AI_Minion>();

        /// <summary>
        ///     The attackable unit list.
        /// </summary>
        private static readonly List<AttackableUnit> AttackableUnitsList = new List<AttackableUnit>();

        /// <summary>
        ///     The enemy heroes list.
        /// </summary>
        private static readonly List<AIHeroClient> EnemyHeroesList = new List<AIHeroClient>();

        /// <summary>
        ///     The enemy inhibitors list.
        /// </summary>
        private static readonly List<Obj_BarracksDampener> EnemyInhibitorsList = new List<Obj_BarracksDampener>();

        /// <summary>
        ///     The enemy list.
        /// </summary>
        private static readonly List<Obj_AI_Base> EnemyList = new List<Obj_AI_Base>();

        /// <summary>
        ///     The enemy minions list.
        /// </summary>
        private static readonly List<Obj_AI_Minion> EnemyMinionsList = new List<Obj_AI_Minion>();

        /// <summary>
        ///     The enemy shops list.
        /// </summary>
        private static readonly List<Obj_Shop> EnemyShopsList = new List<Obj_Shop>();

        /// <summary>
        ///     The enemy spawn points list.
        /// </summary>
        private static readonly List<Obj_SpawnPoint> EnemySpawnPointsList = new List<Obj_SpawnPoint>();

        /// <summary>
        ///     The enemy turrets list.
        /// </summary>
        private static readonly List<Obj_AI_Turret> EnemyTurretsList = new List<Obj_AI_Turret>();

        /// <summary>
        ///     The enemy wards list.
        /// </summary>
        private static readonly List<Obj_AI_Minion> EnemyWardsList = new List<Obj_AI_Minion>();

        /// <summary>
        ///     The game objects list.
        /// </summary>
        private static readonly List<GameObject> GameObjectsList = new List<GameObject>();

        /// <summary>
        ///     The heroes list.
        /// </summary>
        private static readonly List<AIHeroClient> HeroesList = new List<AIHeroClient>();

        /// <summary>
        ///     The inhibitors list.
        /// </summary>
        private static readonly List<Obj_BarracksDampener> InhibitorsList = new List<Obj_BarracksDampener>();

        /// <summary>
        ///     The jungle large list.
        /// </summary>
        private static readonly List<Obj_AI_Minion> JungleLargeList = new List<Obj_AI_Minion>();

        /// <summary>
        ///     The jungle legendary list.
        /// </summary>
        private static readonly List<Obj_AI_Minion> JungleLegendaryList = new List<Obj_AI_Minion>();

        /// <summary>
        ///     The jungle list.
        /// </summary>
        private static readonly List<Obj_AI_Minion> JungleList = new List<Obj_AI_Minion>();

        /// <summary>
        ///     The jungle small list.
        /// </summary>
        private static readonly List<Obj_AI_Minion> JungleSmallList = new List<Obj_AI_Minion>();

        /// <summary>
        ///     The minions list.
        /// </summary>
        private static readonly List<Obj_AI_Minion> MinionsList = new List<Obj_AI_Minion>();

        /// <summary>
        ///     The nexus list.
        /// </summary>
        private static readonly List<Obj_HQ> NexusList = new List<Obj_HQ>();

        /// <summary>
        ///     The shops list.
        /// </summary>
        private static readonly List<Obj_Shop> ShopsList = new List<Obj_Shop>();

        /// <summary>
        ///     The spawn points list.
        /// </summary>
        private static readonly List<Obj_SpawnPoint> SpawnPointsList = new List<Obj_SpawnPoint>();

        /// <summary>
        ///     The turrets list.
        /// </summary>
        private static readonly List<Obj_AI_Turret> TurretsList = new List<Obj_AI_Turret>();

        /// <summary>
        ///     The wards list.
        /// </summary>
        private static readonly List<Obj_AI_Minion> WardsList = new List<Obj_AI_Minion>();

        /// <summary>
        ///     Indicates whether the <see cref="GameObjects" /> stack was initialized and saved required instances.
        /// </summary>
        private static bool initialized;

        #endregion

        /// <summary>
        ///     Gets the game objects.
        /// </summary>
        public static IEnumerable<GameObject> AllGameObjects = GameObjectsList;

        /// <summary>
        ///     Gets the ally.
        /// </summary>
        public static IEnumerable<Obj_AI_Base> Ally = AllyList;

        /// <summary>
        ///     Gets the ally heroes.
        /// </summary>
        public static IEnumerable<AIHeroClient> AllyHeroes = AllyHeroesList;

        /// <summary>
        ///     Gets the ally inhibitors.
        /// </summary>
        public static IEnumerable<Obj_BarracksDampener> AllyInhibitors = AllyInhibitorsList;

        /// <summary>
        ///     Gets the ally minions.
        /// </summary>
        public static IEnumerable<Obj_AI_Minion> AllyMinions = AllyMinionsList;

        /// <summary>
        ///     Gets or sets the ally nexus.
        /// </summary>
        public static Obj_HQ AllyNexus { get; set; }

        /// <summary>
        ///     Gets the ally shops.
        /// </summary>
        public static IEnumerable<Obj_Shop> AllyShops = AllyShopsList;

        /// <summary>
        ///     Gets the ally spawn points.
        /// </summary>
        public static IEnumerable<Obj_SpawnPoint> AllySpawnPoints = AllySpawnPointsList;

        /// <summary>
        ///     Gets the ally turrets.
        /// </summary>
        public static IEnumerable<Obj_AI_Turret> AllyTurrets = AllyTurretsList;

        /// <summary>
        ///     Gets the ally wards.
        /// </summary>
        public static IEnumerable<Obj_AI_Minion> AllyWards = AllyWardsList;

        /// <summary>
        ///     Gets the attackable units.
        /// </summary>
        public static IEnumerable<AttackableUnit> AttackableUnits = AttackableUnitsList;

        /// <summary>
        ///     Gets the enemy.
        /// </summary>
        public static IEnumerable<Obj_AI_Base> Enemy = EnemyList;

        /// <summary>
        ///     Gets the enemy heroes.
        /// </summary>
        public static IEnumerable<AIHeroClient> EnemyHeroes = EnemyHeroesList;

        /// <summary>
        ///     Gets the enemy inhibitors.
        /// </summary>
        public static IEnumerable<Obj_BarracksDampener> EnemyInhibitors = EnemyInhibitorsList;

        /// <summary>
        ///     Gets the enemy minions.
        /// </summary>
        public static IEnumerable<Obj_AI_Minion> EnemyMinions = EnemyMinionsList;

        /// <summary>
        ///     Gets or sets the enemy nexus.
        /// </summary>
        public static Obj_HQ EnemyNexus { get; set; }

        /// <summary>
        ///     Gets the enemy shops.
        /// </summary>
        public static IEnumerable<Obj_Shop> EnemyShops = EnemyShopsList;

        /// <summary>
        ///     Gets the enemy spawn points.
        /// </summary>
        public static IEnumerable<Obj_SpawnPoint> EnemySpawnPoints = EnemySpawnPointsList;

        /// <summary>
        ///     Gets the enemy turrets.
        /// </summary>
        public static IEnumerable<Obj_AI_Turret> EnemyTurrets = EnemyTurretsList;

        /// <summary>
        ///     Gets the enemy wards.
        /// </summary>
        public static IEnumerable<Obj_AI_Minion> EnemyWards = EnemyWardsList;

        /// <summary>
        ///     Gets the heroes.
        /// </summary>
        public static IEnumerable<AIHeroClient> Heroes = HeroesList;

        /// <summary>
        ///     Gets the inhibitors.
        /// </summary>
        public static IEnumerable<Obj_BarracksDampener> Inhibitors = InhibitorsList;

        /// <summary>
        ///     Gets the jungle.
        /// </summary>
        public static IEnumerable<Obj_AI_Minion> Jungle = JungleList;

        /// <summary>
        ///     Gets the jungle large.
        /// </summary>
        public static IEnumerable<Obj_AI_Minion> JungleLarge = JungleLargeList;

        /// <summary>
        ///     Gets the jungle legendary.
        /// </summary>
        public static IEnumerable<Obj_AI_Minion> JungleLegendary = JungleLegendaryList;

        /// <summary>
        ///     Gets the jungle small.
        /// </summary>
        public static IEnumerable<Obj_AI_Minion> JungleSmall = JungleSmallList;

        /// <summary>
        ///     Gets the minions.
        /// </summary>
        public static IEnumerable<Obj_AI_Minion> Minions = MinionsList;

        /// <summary>
        ///     Gets the nexuses.
        /// </summary>
        public static IEnumerable<Obj_HQ> Nexuses = NexusList;

        /// <summary>
        ///     Gets or sets the player.
        /// </summary>
        public static AIHeroClient Player { get; set; }

        /// <summary>
        ///     Gets the shops.
        /// </summary>
        public static IEnumerable<Obj_Shop> Shops = ShopsList;

        /// <summary>
        ///     Gets the spawn points.
        /// </summary>
        public static IEnumerable<Obj_SpawnPoint> SpawnPoints = SpawnPointsList;

        /// <summary>
        ///     Gets the turrets.
        /// </summary>
        public static IEnumerable<Obj_AI_Turret> Turrets = TurretsList;

        /// <summary>
        ///     Gets the wards.
        /// </summary>
        public static IEnumerable<Obj_AI_Minion> Wards = WardsList;
    }

    internal class DashHelper
    {
        /// <summary>
        /// Gets the rotated q positions.
        /// </summary>
        /// <returns></returns>
        public static List<Vector3> GetRotatedQPositions()
        {
            const int currentStep = 30;
            // var direction = ObjectManager.Player.Direction.To2D().Perpendicular();
            var direction = (Game.CursorPos - ObjectManager.Player.ServerPosition).Normalized().To2D();

            var list = new List<Vector3>();
            for (var i = -105; i <= 105; i += currentStep)
            {
                var angleRad = Geometry.DegreeToRadian(i);
                var rotatedPosition = ObjectManager.Player.Position.To2D() + (300f*direction.Rotated(angleRad));
                list.Add(rotatedPosition.To3D());
            }
            return list;
        }

        /// <summary>
        /// Gets the closest enemy.
        /// </summary>
        /// <param name="from">From.</param>
        /// <returns></returns>
        public static AIHeroClient GetClosestEnemy(Vector3 from)
        {
            if (Orbwalker.LastTarget is AIHeroClient)
            {
                var owAI = Orbwalker.LastTarget as AIHeroClient;
                if (owAI.IsValidTarget(Variables._Player.GetAutoAttackRange(null) + 120f, true, from))
                {
                    return owAI;
                }
            }

            return null;
        }

        /// <summary>
        /// Determines whether the specified position is Safe using AA ranges logic.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns></returns>
        public static bool IsSafeEx(Vector3 position)
        {
            var closeEnemies =
                EntityManager.Heroes.Enemies.FindAll(
                    en =>
                        en.IsValidTarget(1500f) &&
                        !(en.Distance(ObjectManager.Player.ServerPosition) < en.AttackRange + 65f))
                    .OrderBy(en => en.Distance(position));

            return closeEnemies.All(
                enemy =>
                    position.CountEnemiesInRange(enemy.AttackRange) <= 1);
        }

        /// <summary>
        /// Gets the average distance of a specified position to the enemies.
        /// </summary>
        /// <param name="from">From.</param>
        /// <returns></returns>
        public static float GetAvgDistance(Vector3 from)
        {
            var numberOfEnemies = from.CountEnemiesInRange(1200f);
            if (numberOfEnemies != 0)
            {
                var enemies = EntityManager.Heroes.Enemies.Where(en => en.IsValidTarget(1200f, true, from)
                                                                       &&
                                                                       en.Health >
                                                                       ObjectManager.Player.GetAutoAttackDamage(en)*3 +
                                                                       Variables._Player.GetSpellDamage(en, SpellSlot.W) +
                                                                       Variables._Player.GetSpellDamage(en, SpellSlot.Q)).ToList()
                    ;
                var enemiesEx = EntityManager.Heroes.Enemies.Where(en => en.IsValidTarget(1200f, true, from)).ToList();
                var LHEnemies = enemiesEx.Count() - enemies.Count();

                var totalDistance = (LHEnemies > 1 && enemiesEx.Count() > 2)
                    ? enemiesEx.Sum(en => en.Distance(ObjectManager.Player.ServerPosition))
                    : enemies.Sum(en => en.Distance(ObjectManager.Player.ServerPosition));

                return totalDistance/numberOfEnemies;
            }
            return -1;
        }

        /// <summary>
        /// Gets the enemy points.
        /// </summary>
        /// <param name="dynamic">if set to <c>true</c> [dynamic].</param>
        /// <returns></returns>
        public static List<Vector2> GetEnemyPoints(bool dynamic = true)
        {
            var staticRange = 360f;
            var polygonsList =
                DashVariables.EnemiesClose.Select(
                    enemy =>
                        new SOLOGeometry.Circle(enemy.ServerPosition.To2D(),
                            (dynamic ? (enemy.IsMelee ? enemy.AttackRange*1.5f : enemy.AttackRange) : staticRange) +
                            enemy.BoundingRadius + 20).ToPolygon()).ToList();
            var pathList = SOLOGeometry.ClipPolygons(polygonsList);
            var pointList =
                pathList.SelectMany(path => path, (path, point) => new Vector2(point.X, point.Y))
                    .Where(
                        currentPoint =>
                            !NavMesh.GetCollisionFlags(currentPoint).HasFlag(CollisionFlags.Wall) ||
                            !NavMesh.GetCollisionFlags(currentPoint).HasFlag(CollisionFlags.Building))
                    .ToList();
            return pointList;
        }
    }

    internal class DashVariables
    {
        /// <summary>
        /// Gets the enemies close.
        /// </summary>
        /// <value>
        /// The enemies close.
        /// </value>
        public static IEnumerable<Obj_AI_Base> EnemiesClose
        {
            get
            {
                return
                    EntityManager.Heroes.Enemies.Where(
                        m =>
                            m.Distance(ObjectManager.Player, true) <= Math.Pow(1000, 2) && m.IsValidTarget(1500) &&
                            m.CountEnemiesInRange(m.IsMelee ? m.AttackRange*1.5f : m.AttackRange + 20*1.5f) > 0);
            }
        }
    }

    internal static class DashExtensions
    {
        /// <summary>
        /// Determines whether the position is safe.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns></returns>
        public static bool IsSafe(this Vector3 position)
        {
            return position.IsSafeEx()
                   && position.IsNotIntoEnemies()
                   && EntityManager.Heroes.Enemies.All(m => m.Distance(position) > 350f)
                   &&
                   (!other.UnderEnemyTower((Vector2) position) ||
                    (other.UnderEnemyTower((Vector2) Variables._Player.ServerPosition) &&
                     other.UnderEnemyTower((Vector2) position) && ObjectManager.Player.HealthPercent > 10));
            //Either it is not under turret or both the player and the position are under turret already and the health percent is greater than 10.
        }

        public static bool IsSafe2(this Vector2 position)
        {
            return position.IsSafeEx2()
                   && position.IsNotIntoEnemies2()
                   && EntityManager.Heroes.Enemies.All(m => m.Distance(position) > 350f)
                   &&
                   (!other.UnderEnemyTower((Vector2) position) ||
                    (other.UnderEnemyTower((Vector2) Variables._Player.ServerPosition) &&
                     other.UnderEnemyTower((Vector2) position) && ObjectManager.Player.HealthPercent > 10));
            //Either it is not under turret or both the player and the position are under turret already and the health percent is greater than 10.
        }

        public static bool IsSafeEx2(this Vector2 Position)
        {
            if (other.UnderEnemyTower((Vector2) Position) &&
                !other.UnderEnemyTower((Vector2) Variables._Player.ServerPosition))
            {
                return false;
            }
            var range = 1000f;
            var lowHealthAllies =
                EntityManager.Heroes.Allies.Where(a => a.IsValidTarget(range, false) && a.HealthPercent < 10 && !a.IsMe);
            var lowHealthEnemies =
                EntityManager.Heroes.Allies.Where(a => a.IsValidTarget(range) && a.HealthPercent < 10);
            var enemies = ObjectManager.Player.CountEnemiesInRange(range);
            var allies = ObjectManager.Player.CountAlliesInRange(range);
            var enemyTurrets = Turrets.EnemyTurrets.Where(m => m.IsValidTarget(975f));
            var allyTurrets = Turrets.AllyTurrets.Where(m => m.IsValidTarget(975f, false));

            return (allies - lowHealthAllies.Count() + allyTurrets.Count()*2 + 1 >=
                    enemies - lowHealthEnemies.Count() +
                    (!other.UnderEnemyTower((Vector2) Variables._Player.ServerPosition) ? enemyTurrets.Count()*2 : 0));
        }

        /// <summary>
        /// Determines whether the position is Safe using the allies/enemies logic
        /// </summary>
        /// <param name="Position">The position.</param>
        /// <returns></returns>
        public static bool IsSafeEx(this Vector3 Position)
        {
            if (other.UnderEnemyTower((Vector2) Position) &&
                !other.UnderEnemyTower((Vector2) Variables._Player.ServerPosition))
            {
                return false;
            }
            var range = 1000f;
            var lowHealthAllies =
                EntityManager.Heroes.Allies.Where(a => a.IsValidTarget(range, false) && a.HealthPercent < 10 && !a.IsMe);
            var lowHealthEnemies =
                EntityManager.Heroes.Allies.Where(a => a.IsValidTarget(range) && a.HealthPercent < 10);
            var enemies = ObjectManager.Player.CountEnemiesInRange(range);
            var allies = ObjectManager.Player.CountAlliesInRange(range);
            var enemyTurrets = Turrets.EnemyTurrets.Where(m => m.IsValidTarget(975f));
            var allyTurrets = Turrets.AllyTurrets.Where(m => m.IsValidTarget(975f, false));

            return (allies - lowHealthAllies.Count() + allyTurrets.Count()*2 + 1 >=
                    enemies - lowHealthEnemies.Count() +
                    (!other.UnderEnemyTower((Vector2) Variables._Player.ServerPosition) ? enemyTurrets.Count()*2 : 0));
        }

        /// <summary>
        /// Determines whether the position is not into enemies.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns></returns>
        public static bool IsNotIntoEnemies(this Vector3 position)
        {

            var enemyPoints = DashHelper.GetEnemyPoints();
            if (enemyPoints.ToList().Contains(position.To2D()) &&
                !enemyPoints.Contains(ObjectManager.Player.ServerPosition.To2D()))
            {
                return false;
            }

            var closeEnemies =
                EntityManager.Heroes.Enemies.FindAll(
                    en =>
                        en.IsValidTarget(1500f) &&
                        !(en.Distance(ObjectManager.Player.ServerPosition) < en.AttackRange + 65f));
            if (!closeEnemies.All(enemy => position.CountEnemiesInRange(enemy.AttackRange) <= 1))
            {
                return false;
            }

            return true;
        }

        public static bool IsNotIntoEnemies2(this Vector2 position)
        {

            var enemyPoints = DashHelper.GetEnemyPoints();
            if (enemyPoints.ToList().Contains(position) &&
                !enemyPoints.Contains(ObjectManager.Player.ServerPosition.To2D()))
            {
                return false;
            }

            var closeEnemies =
                EntityManager.Heroes.Enemies.FindAll(
                    en =>
                        en.IsValidTarget(1500f) &&
                        !(en.Distance(ObjectManager.Player.ServerPosition) < en.AttackRange + 65f));
            if (!closeEnemies.All(enemy => position.CountEnemiesInRange(enemy.AttackRange) <= 1))
            {
                return false;
            }

            return true;
        }
    }

    internal static class SOLOGeometry
    {
        private const int CircleLineSegmentN = 22;

        public static Vector3 SwitchYZ(this Vector3 v)
        {
            return new Vector3(v.X, v.Z, v.Y);
        }

        public static bool IsOverWall(Vector3 start, Vector3 end)
        {
            double distance = Vector3.Distance(start, end);
            for (uint i = 0; i < distance; i += 10)
            {
                var tempPosition = start.Extend(end, i).To3D();
                var collFlags = NavMesh.GetCollisionFlags(tempPosition);
                if (collFlags.HasFlag(CollisionFlags.Wall) || collFlags.HasFlag(CollisionFlags.Building))
                {
                    return true;
                }
            }
            return false;
        }

        //Clipper
        public static List<Polygon> ToPolygons(this Paths v)
        {
            var result = new List<Polygon>();

            foreach (var path in v)
            {
                result.Add(path.ToPolygon());
            }

            return result;
        }

        /// <summary>
        /// Returns the position on the path after t milliseconds at speed speed.
        /// </summary>
        public static Vector2 PositionAfter(this GamePath self, int t, int speed, int delay = 0)
        {
            var distance = Math.Max(0, t - delay)*speed/1000;
            for (var i = 0; i <= self.Count - 2; i++)
            {
                var from = self[i];
                var to = self[i + 1];
                var d = (int) to.Distance(from);
                if (d > distance)
                {
                    return from + distance*(to - from).Normalized();
                }
                distance -= d;
            }
            return self[self.Count - 1];
        }

        public static Polygon ToPolygon(this Path v)
        {
            var polygon = new Polygon();
            foreach (var point in v)
            {
                polygon.Add(new Vector2(point.X, point.Y));
            }
            return polygon;
        }


        public static Paths ClipPolygons(List<Polygon> polygons)
        {
            var subj = new Paths(polygons.Count);
            var clip = new Paths(polygons.Count);

            foreach (var polygon in polygons)
            {
                subj.Add(polygon.ToClipperPath());
                clip.Add(polygon.ToClipperPath());
            }

            var solution = new Paths();
            var c = new Clipper();
            c.AddPaths(subj, PolyType.ptSubject, true);
            c.AddPaths(clip, PolyType.ptClip, true);
            c.Execute(ClipType.ctUnion, solution, PolyFillType.pftPositive, PolyFillType.pftEvenOdd);

            return solution;
        }


        public class Circle
        {
            public Vector2 Center;
            public float Radius;

            public Circle(Vector2 center, float radius)
            {
                Center = center;
                Radius = radius;
            }

            public Polygon ToPolygon(int offset = 0, float overrideWidth = -1)
            {
                var result = new Polygon();
                var outRadius = (overrideWidth > 0
                    ? overrideWidth
                    : (offset + Radius)/(float) Math.Cos(2*Math.PI/CircleLineSegmentN));

                for (var i = 1; i <= CircleLineSegmentN; i++)
                {
                    var angle = i*2*Math.PI/CircleLineSegmentN;
                    var point = new Vector2(
                        Center.X + outRadius*(float) Math.Cos(angle), Center.Y + outRadius*(float) Math.Sin(angle));
                    result.Add(point);
                }

                return result;
            }
        }

        public class Polygon
        {
            public List<Vector2> Points = new List<Vector2>();

            public void Add(Vector2 point)
            {
                Points.Add(point);
            }

            public Path ToClipperPath()
            {
                var result = new Path(Points.Count);

                foreach (var point in Points)
                {
                    result.Add(new IntPoint(point.X, point.Y));
                }

                return result;
            }

            public bool IsOutside(Vector2 point)
            {
                var p = new IntPoint(point.X, point.Y);
                return Clipper.PointInPolygon(p, ToClipperPath()) != 1;
            }

            public void Draw(Color color, int width = 1)
            {
                for (var i = 0; i <= Points.Count - 1; i++)
                {
                    var nextIndex = (Points.Count - 1 == i) ? 0 : (i + 1);
                    DrawLineInWorld(Points[i].To3D(), Points[nextIndex].To3D(), width, color);
                }
            }

            public static void DrawLineInWorld(Vector3 start, Vector3 end, int width, Color color)
            {
                var from = Drawing.WorldToScreen(start);
                var to = Drawing.WorldToScreen(end);
                Drawing.DrawLine(from[0], from[1], to[0], to[1], width, color);
            }
        }

        public class Rectangle
        {
            public Vector2 Direction;
            public Vector2 Perpendicular;
            public Vector2 REnd;
            public Vector2 RStart;
            public float Width;

            public Rectangle(Vector2 start, Vector2 end, float width)
            {
                RStart = start;
                REnd = end;
                Width = width;
                Direction = (end - start).Normalized();
                Perpendicular = Direction.Perpendicular();
            }

            public Polygon ToPolygon(int offset = 0, float overrideWidth = -1)
            {
                var result = new Polygon();

                result.Add(
                    RStart + (overrideWidth > 0 ? overrideWidth : Width + offset)*Perpendicular - offset*Direction);
                result.Add(
                    RStart - (overrideWidth > 0 ? overrideWidth : Width + offset)*Perpendicular - offset*Direction);
                result.Add(
                    REnd - (overrideWidth > 0 ? overrideWidth : Width + offset)*Perpendicular + offset*Direction);
                result.Add(
                    REnd + (overrideWidth > 0 ? overrideWidth : Width + offset)*Perpendicular + offset*Direction);

                return result;
            }
        }


        public class Ring
        {
            public Vector2 Center;
            public float Radius;
            public float RingRadius; //actually radius width.

            public Ring(Vector2 center, float radius, float ringRadius)
            {
                Center = center;
                Radius = radius;
                RingRadius = ringRadius;
            }

            public Polygon ToPolygon(int offset = 0)
            {
                var result = new Polygon();

                var outRadius = (offset + Radius + RingRadius)/(float) Math.Cos(2*Math.PI/CircleLineSegmentN);
                var innerRadius = Radius - RingRadius - offset;

                for (var i = 0; i <= CircleLineSegmentN; i++)
                {
                    var angle = i*2*Math.PI/CircleLineSegmentN;
                    var point = new Vector2(
                        Center.X - outRadius*(float) Math.Cos(angle), Center.Y - outRadius*(float) Math.Sin(angle));
                    result.Add(point);
                }

                for (var i = 0; i <= CircleLineSegmentN; i++)
                {
                    var angle = i*2*Math.PI/CircleLineSegmentN;
                    var point = new Vector2(
                        Center.X + innerRadius*(float) Math.Cos(angle),
                        Center.Y - innerRadius*(float) Math.Sin(angle));
                    result.Add(point);
                }


                return result;
            }
        }

        public class Sector
        {
            public float Angle;
            public Vector2 Center;
            public Vector2 Direction;
            public float Radius;

            public Sector(Vector2 center, Vector2 direction, float angle, float radius)
            {
                Center = center;
                Direction = direction;
                Angle = angle;
                Radius = radius;
            }

            public Polygon ToPolygon(int offset = 0)
            {
                var result = new Polygon();
                var outRadius = (Radius + offset)/(float) Math.Cos(2*Math.PI/CircleLineSegmentN);

                result.Add(Center);
                var Side1 = Direction.Rotated(-Angle*0.5f);

                for (var i = 0; i <= CircleLineSegmentN; i++)
                {
                    var cDirection = Side1.Rotated(i*Angle/CircleLineSegmentN).Normalized();
                    result.Add(new Vector2(Center.X + outRadius*cDirection.X, Center.Y + outRadius*cDirection.Y));
                }

                return result;
            }
        }
    }
}
