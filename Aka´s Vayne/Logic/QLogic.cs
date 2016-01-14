using System;
using System.Collections.Generic;
using System.Linq;
using Aka_s_Vayne_reworked;
using Aka_s_Vayne_reworked.Functions;
using Aka_s_Vayne_reworked.Logic;
using SharpDX;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace AddonTemplate.Logic
{
    public static class QLogic
    {
        public static Vector3 TumbleOrderPos = Vector3.Zero;

        private static QProvider Provider = new QProvider();

        public static void Cast(Vector3 position)
        {
            TumbleOrderPos = position;
            if (position != Vector3.Zero)
            {
                Player.CastSpell(SpellSlot.Q, TumbleOrderPos);
            }
        }

        public static bool IsDangerousPosition(this Vector3 pos)
        {
            var collFlags = NavMesh.GetCollisionFlags(pos);
            return
                EntityManager.Heroes.Enemies.Any(
                    e => e.IsValidTarget(600) && e.IsVisible &&
                        e.Distance(pos) < 375) ||
                Traps.EnemyTraps.Any(t => pos.Distance(t.Position) < 125) ||
                (Aka_s_Vayne_reworked.Functions.other.UnderEnemyTower((Vector2)pos) && Aka_s_Vayne_reworked.Functions.other.UnderEnemyTower((Vector2)Variables._Player.Position)) || collFlags.HasFlag(CollisionFlags.Wall) || collFlags.HasFlag(CollisionFlags.Building);
        }

        public static Vector3 GetJungleSafeTumblePos(Obj_AI_Base target)
        {
            var cursorPos = Game.CursorPos;
            if (IsSafeTumblePos(cursorPos)) return cursorPos;

            if (!target.IsValidTarget()) return Vector3.Zero;

            var targetPosition = target.ServerPosition;

            var myTumbleRangeCircle =
                new QGeometry.Circle(ObjectManager.Player.ServerPosition.To2D(), 300).topolygon().ToClipperPath();

            var goodCandidates = from p in myTumbleRangeCircle
                                 select new Vector2(p.X, p.Y).To3D() into v3
                                 let dist = v3.Distance(targetPosition)
                                 where dist > MenuManager.ComboMenu["QDistance"].Cast<Slider>().CurrentValue && dist < 500
                                 select v3;

            return goodCandidates.OrderByDescending(candidate => candidate.Distance(cursorPos)).FirstOrDefault();
        }

        public static Vector3 GetSafeTumblePos(AIHeroClient target)
        {
            if (!target.IsValidTarget()) return Vector3.Zero;

            var targetPosition = target.ServerPosition;

            var myTumbleRangeCircle =
                new QGeometry.Circle(ObjectManager.Player.ServerPosition.To2D(), 300).topolygon().ToClipperPath();

            var goodCandidates = from p in myTumbleRangeCircle
                                 select new Vector2(p.X, p.Y).To3D() into v3
                                 let dist = v3.Distance(targetPosition)
                                 where dist > MenuManager.ComboMenu["QDistance"].Cast<Slider>().CurrentValue && dist < 500
                                 select v3;

            return goodCandidates.OrderBy(candidate => candidate.Distance(Game.CursorPos)).FirstOrDefault();
        }

        public static bool IsSafeTumblePos(Vector3 position)
        {
            return
                !ObjectManager.Get<AIHeroClient>()
                    .Any(e => e.IsEnemy && e.Distance(position) < MenuManager.ComboMenu["QDistance"].Cast<Slider>().CurrentValue);
        }

        public static void JungleClear()
        {
            Obj_AI_Base jungleMobs = EntityManager.MinionsAndMonsters.GetJungleMonsters(Variables._Player.Position, Program.Q.Range, true).FirstOrDefault();
            {
                if (MenuManager.JungleClearMenu["JCQ"].Cast<CheckBox>().CurrentValue && Program.Q.IsReady() && jungleMobs != null && jungleMobs.IsValidTarget(Program.Q.Range))
                {
                    QLogic.Cast(GetJungleSafeTumblePos(jungleMobs));
                }
            }
        }

        public static Vector3 NewQPrediction()
        {
            if (!MenuManager.ComboMenu["QE"].Cast<CheckBox>().CurrentValue ||
    !Program.E.IsReady())
            {
                return Vector3.Zero;
            }

            const int currentStep = 30;
            var direction = ObjectManager.Player.Direction.To2D().Perpendicular();
            for (var i = 0f; i < 360f; i += currentStep)
            {
                var angleRad = Geometry.DegreeToRadian(i);
                var rotatedPosition = ObjectManager.Player.Position.To2D() + (300f * direction.Rotated(angleRad));
                if (NewELogic.GetCondemnTarget(rotatedPosition.To3D()).IsValidTarget() && rotatedPosition.To3D().IsSafe())
                {
                    return rotatedPosition.To3D();
                }
            }

            return Vector3.Zero;
        }

        public static List<Vector2> GetEnemyPoints(bool dynamic = true)
        {
            var staticRange = 360f;
            var polygonsList = Variables.EnemiesClose.Select(enemy => new VHRGeometry.Circle(enemy.ServerPosition.To2D(), (dynamic ? (enemy.IsMelee ? enemy.AttackRange * 1.5f : enemy.AttackRange) : staticRange) + enemy.BoundingRadius + 20).ToPolygon()).ToList();
            var pathList = VHRGeometry.ClipPolygons(polygonsList);
            var pointList = pathList.SelectMany(path => path, (path, point) => new Vector2(point.X, point.Y)).Where(currentPoint => !NavMesh.GetCollisionFlags(currentPoint).HasFlag(CollisionFlags.Wall) || !NavMesh.GetCollisionFlags(currentPoint).HasFlag(CollisionFlags.Building)).ToList();
            return pointList;
        }

        public static bool IsSafe(this Vector3 position, bool noQIntoEnemiesCheck = false)
        {
            if (Aka_s_Vayne_reworked.Functions.other.UnderEnemyTower((Vector2)position) && !Aka_s_Vayne_reworked.Functions.other.UnderEnemyTower((Vector2)Variables._Player.Position))
            {
                return false;
            }

            var allies = position.CountAlliesInRange(ObjectManager.Player.AttackRange);
            var enemies = position.CountEnemiesInRange(ObjectManager.Player.AttackRange);
            var lhEnemies = position.GetLhEnemiesNear(ObjectManager.Player.AttackRange, 15).Count();

            if (enemies <= 1) ////It's a 1v1, safe to assume I can Q
            {
                return true;
            }

            if (position.UnderAllyTurret_Ex())
            {
                var nearestAllyTurret = ObjectManager.Get<Obj_AI_Turret>().Where(a => a.IsAlly).OrderBy(d => d.Distance(position, true)).FirstOrDefault();

                if (nearestAllyTurret != null)
                {
                    ////We're adding more allies, since the turret adds to the firepower of the team.
                    allies += 2;
                }
            }

            ////Adding 1 for my Player
            var normalCheck = (allies + 1 > enemies - lhEnemies);
            var QEnemiesCheck = true;

            if (MenuManager.ComboMenu["UseQE"].Cast<CheckBox>().CurrentValue && noQIntoEnemiesCheck)
            {
                if (!MenuManager.ComboMenu["UseQE"].Cast<CheckBox>().CurrentValue)
                {
                    var Vector2Position = position.To2D();
                    var enemyPoints = MenuManager.ComboMenu["UseSafeQ"].Cast<CheckBox>().CurrentValue
                        ? GetEnemyPoints()
                        : GetEnemyPoints(false);
                    if (enemyPoints.Contains(Vector2Position) &&
                        !MenuManager.ComboMenu["UseQspam"].Cast<CheckBox>().CurrentValue)
                    {
                        QEnemiesCheck = false;
                    }

                    var closeEnemies =
                    EntityManager.Heroes.Enemies.FindAll(en => en.IsValidTarget(1500f) && !(en.Distance(ObjectManager.Player.ServerPosition) < en.AttackRange + 65f))
                    .OrderBy(en => en.Distance(position));

                    if (
                        !closeEnemies.All(
                            enemy =>
                                position.CountEnemiesInRange(
                                    MenuManager.ComboMenu["UseSafeQ"].Cast<CheckBox>().CurrentValue
                                        ? enemy.AttackRange
                                        : 405f) <= 1))
                    {
                        QEnemiesCheck = false;
                    }
                }
                else
                {
                    var closeEnemies =
                    EntityManager.Heroes.Enemies.FindAll(en => en.IsValidTarget(1500f)).OrderBy(en => en.Distance(position));
                    if (closeEnemies.Any())
                    {
                        QEnemiesCheck =
                            !closeEnemies.All(
                                enemy =>
                                    position.CountEnemiesInRange(
                                        MenuManager.ComboMenu["UseSafeQ"].Cast<CheckBox>().CurrentValue
                                            ? enemy.AttackRange
                                            : 405f) <= 1);
                    }
                }

            }

            return normalCheck && QEnemiesCheck;
        }

        public static void PreCastTumble(Obj_AI_Base target)
        {
            if (!target.IsValidTarget(ObjectManager.Player.AttackRange + 65f + 65f + 300f))
            {
                return;
           }

            var smartQPosition = NewQPrediction();
            var smartQCheck = smartQPosition != Vector3.Zero;
            var QPosition = smartQCheck ? smartQPosition : Game.CursorPos;

            OnCastTumble(target, QPosition);
        }

        private static void OnCastTumble(Obj_AI_Base target, Vector3 position)
        {
            var mode = MenuManager.ComboMenu["Qmode"].Cast<Slider>().CurrentValue;
            var afterTumblePosition = ObjectManager.Player.ServerPosition.Extend(position, 300f);
            var distanceToTarget = afterTumblePosition.Distance(target.ServerPosition, true);
            if ((distanceToTarget < Math.Pow(ObjectManager.Player.AttackRange + 65, 2) && distanceToTarget > 110 * 110)
                || MenuManager.ComboMenu["UseQspam"].Cast<CheckBox>().CurrentValue)
            {
                switch (mode)
                {
                    case 2:
                        var smartQPosition = NewQPrediction();
                        var smartQCheck = smartQPosition != Vector3.Zero;
                        var QPosition = smartQCheck ? smartQPosition : Game.CursorPos;
                        var QPosition2 = Provider.GetQPosition() != Vector3.Zero ? Provider.GetQPosition() : QPosition;

                        if (!Aka_s_Vayne_reworked.Functions.other.UnderEnemyTower((Vector2)QPosition2) || (other.UnderEnemyTower((Vector2)QPosition2) && other.UnderEnemyTower((Vector2)Variables._Player.Position)))
                        {
                            CastQ(QPosition2);
                        }
                        break;
                    case 1:
                        //To mouse
                        DefaultQCast(position, target);
                        break;
                    case 3:
                        //Away from melee enemies
                        if (Variables.MeleeEnemiesTowardsMe.Any() &&
                            !Variables.MeleeEnemiesTowardsMe.All(m => m.HealthPercent <= 15))
                        {
                            var Closest =
                                Variables.MeleeEnemiesTowardsMe.OrderBy(m => m.Distance(ObjectManager.Player)).First();
                            var whereToQ = (Vector3)Closest.ServerPosition.Extend(
                                ObjectManager.Player.ServerPosition, Closest.Distance(ObjectManager.Player) + 300f);

                            if (whereToQ.IsSafe())
                            {
                                CastQ(whereToQ);
                            }
                        }
                        else
                        {
                            DefaultQCast(position, target);
                        }
                        break;
                }
            }
        }

        public static Vector3 GetAfterTumblePosition(Vector3 endPosition)
        {
            return (Vector3)ObjectManager.Player.ServerPosition.Extend(endPosition, 300f);
        }

        public static void DefaultQCast(Vector3 position, Obj_AI_Base Target)
        {
            var afterTumblePosition = GetAfterTumblePosition(Game.CursorPos);
            var CursorPos = Game.CursorPos;
            var EnemyPoints = GetEnemyPoints();
            if (afterTumblePosition.IsSafe(true) || (!EnemyPoints.Contains(Game.CursorPos.To2D())) || (Variables.EnemiesClose.Count() == 1))
            {
                if (afterTumblePosition.Distance(Target.ServerPosition) <= Target.GetAutoAttackRange())
                {
                    CastQ(position);
                }
            }
        }

        private static void CastQ(Vector3 Position)
        {
            var endPosition = Position;

            if (MenuManager.ComboMenu["Mirin"].Cast<CheckBox>().CurrentValue)
            {
                var qBurstModePosition = GetQBurstModePosition();
                if (qBurstModePosition != null)
                {
                    endPosition = (Vector3)qBurstModePosition;
                }
            }

            Player.CastSpell(SpellSlot.Q, endPosition);
        }

        private static Vector3? GetQBurstModePosition()
        {
            var positions =
                GetWallQPositions(70).ToList().OrderBy(pos => pos.Distance(ObjectManager.Player.ServerPosition, true));

            foreach (var position in positions)
            {
                var collFlags = NavMesh.GetCollisionFlags(position);
                if (collFlags.HasFlag(CollisionFlags.Wall) || collFlags.HasFlag(CollisionFlags.Building) && position.IsSafe(true))
                {
                    return position;
                }
            }

            return null;
        }


        private static Vector3[] GetWallQPositions(float Range)
        {
            Vector3[] vList =
            {
                (ObjectManager.Player.ServerPosition.To2D() + Range * ObjectManager.Player.Direction.To2D()).To3D(),
                (ObjectManager.Player.ServerPosition.To2D() - Range * ObjectManager.Player.Direction.To2D()).To3D()

            };

            return vList;
        }
    }
}