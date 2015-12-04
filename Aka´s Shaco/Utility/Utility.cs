using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;
using Color = System.Drawing.Color;


namespace AddonTemplate.Utility
{
    /// <summary>
    ///     Game functions related utilities.
    /// </summary>
    public static class Utility2
    {
        /// <summary>
        ///     Returns if the source is facing the target.
        /// </summary>
        public static bool IsFacing2(this Obj_AI_Base source, Obj_AI_Base target)
        {
            if (source == null || target == null)
            {
                return false;
            }

            const float angle = 90;
            return source.Direction.To2D2().Perpendicular2().AngleBetween2((target.Position - source.Position).To2D2()) < angle;
        }

        /// <summary>
        ///     Returns if both source and target are Facing Themselves.
        /// </summary>
        public static bool IsBothFacing(Obj_AI_Base source, Obj_AI_Base target)
        {
            return source.IsFacing2(target) && target.IsFacing2(source);
        }

        /// <summary>
        ///     Returns if the target is valid (not dead, targetable, visible...).
        /// </summary>
        public static bool IsValidTarget(this AttackableUnit unit,
            float range = float.MaxValue,
            bool checkTeam = true,
            Vector3 from = new Vector3())
        {
            if (unit == null || !unit.IsValid || unit.IsDead || !unit.IsVisible || !unit.IsTargetable ||
                unit.IsInvulnerable)
            {
                return false;
            }

            if (checkTeam && unit.Team == ObjectManager.Player.Team)
            {
                return false;
            }

            var @base = unit as Obj_AI_Base;
            var unitPosition = @base != null ? @base.ServerPosition : unit.Position;

            return !(range < float.MaxValue) ||
                   !(Vector2.DistanceSquared(
                       (@from.To2D2().IsValid() ? @from : ObjectManager.Player.ServerPosition).To2D2(),
                       unitPosition.To2D2()) > range * range);
        }

        public static SpellDataInst GetSpell(this AIHeroClient hero, SpellSlot slot)
        {
            return hero.Spellbook.GetSpell(slot);
        }

        /// <summary>
        ///     Returns if the spell is ready to use.
        /// </summary>
        public static bool IsReady(this SpellDataInst spell, int t = 0)
        {
            return spell != null && spell.Slot != SpellSlot.Unknown && t == 0
                ? spell.State == SpellState.Ready
                : (spell.State == SpellState.Ready ||
                   (spell.State == SpellState.Cooldown && (spell.CooldownExpires - Game.Time) <= t / 1000f));
        }

        public static bool IsReady(this Spell2 spell, int t = 0)
        {
            return IsReady(spell.Instance, t);
        }

        public static bool IsReady(this SpellSlot slot, int t = 0)
        {
            var s = ObjectManager.Player.Spellbook.GetSpell(slot);
            return s != null && IsReady(s, t);
        }

        public static bool IsValid<T>(this GameObject obj) where T : GameObject
        {
            return obj as T != null && obj.IsValid;
        }

        public static bool IsValidSlot(this InventorySlot slot)
        {
            return slot != null && slot.SpellSlot != SpellSlot.Unknown;
        }

        /// <summary>
        /// Returns the unit's ability power
        /// </summary>
        /// 
        public static float AbilityPower(this Obj_AI_Base @base)
        {
            return @base.FlatMagicDamageMod + (@base.PercentMagicDamageMod * @base.FlatMagicDamageMod);
        }

        /// <summary>
        ///     Returns the unit's health percentage (From 0 to 100).
        /// </summary>
        [Obsolete("Use HealthPercent attribute.", false)]
        public static float HealthPercentage(this Obj_AI_Base unit)
        {
            return unit.Health / unit.MaxHealth * 100;
        }

        /// <summary>
        ///     Returns the unit's mana percentage (From 0 to 100).
        /// </summary>
        [Obsolete("Use ManaPercent attribute.", false)]
        public static float ManaPercentage(this Obj_AI_Base unit)
        {
            return unit.Mana / unit.MaxMana * 100;
        }

        public static float TotalMagicalDamage(this AIHeroClient target)
        {
            return target.BaseAbilityDamage + target.FlatMagicDamageMod;
        }

        public static float TotalAttackDamage(this AIHeroClient target)
        {
            return target.BaseAttackDamage + target.FlatPhysicalDamageMod;
        }

        /// <summary>
        ///     Checks if the unit is a Hero or Champion
        /// </summary>
        public static bool IsChampion(this Obj_AI_Base unit)
        {
            var hero = unit as AIHeroClient;
            return hero != null && hero.IsValid;
        }

        public static bool IsChampion(this Obj_AI_Base unit, string championName)
        {
            var hero = unit as AIHeroClient;
            return hero != null && hero.IsValid && hero.ChampionName.Equals(championName);
        }

        public static bool IsRecalling(this AIHeroClient unit)
        {
            return unit.Buffs.Any(buff => buff.Name.ToLower().Contains("recall") && buff.Type == BuffType.Aura);
        }

        public static bool IsOnScreen(this Vector3 position)
        {
            var pos = Drawing.WorldToScreen(position);
            return pos.X > 0 && pos.X <= Drawing.Width && pos.Y > 0 && pos.Y <= Drawing.Height;
        }

        public static bool IsOnScreen(this Vector2 position)
        {
            return position.To3D().IsOnScreen();
        }

        public static Vector3 Randomize(this Vector3 position, int min, int max)
        {
            var ran = new Random(Utils.TickCount);
            return position + new Vector2(ran.Next(min, max), ran.Next(min, max)).To3D();
        }

        public static Vector2 Randomize(this Vector2 position, int min, int max)
        {
            return position.To3D().Randomize(min, max).To2D2();
        }

        public static bool IsAutoAttack(this SpellData spellData)
        {
            return Orbwalking.IsAutoAttack(spellData.Name);
        }

        public static bool IsAutoAttack(this SpellDataInst spellData)
        {
            return Orbwalking.IsAutoAttack(spellData.Name);
        }

        public static bool IsWall(this Vector3 position)
        {
            return NavMesh.GetCollisionFlags(position).HasFlag(CollisionFlags.Wall) || NavMesh.GetCollisionFlags(position).HasFlag(CollisionFlags.Building);
        }

        public static bool IsWall(this Vector2 position)
        {
            return position.To3D().IsWall();
        }

        public static bool IsCasted(this Spell2.CastStates state)
        {
            return state == Spell2.CastStates.SuccessfullyCasted;
        }

        public static int GetRecallTime(AIHeroClient obj)
        {
            return GetRecallTime(obj.Spellbook.GetSpell(SpellSlot.Recall).Name);
        }

        public static int GetRecallTime(string recallName)
        {
            var duration = 0;

            switch (recallName.ToLower())
            {
                case "recall":
                    duration = 8000;
                    break;
                case "recallimproved":
                    duration = 7000;
                    break;
                case "odinrecall":
                    duration = 4500;
                    break;
                case "odinrecallimproved":
                    duration = 4000;
                    break;
                case "superrecall":
                    duration = 4000;
                    break;
                case "superrecallimproved":
                    duration = 4000;
                    break;
            }
            return duration;
        }

        public static void LevelUpSpell(this Spellbook book, SpellSlot slot, bool evolve = false)
        {
            book.LevelSpell(slot);
        }

        public static List<Vector2> CutPath(this List<Vector2> path, float distance)
        {
            var result = new List<Vector2>();
            var Distance = distance;
            if (distance < 0)
            {
                path[0] = path[0] + distance * (path[1] - path[0]).Normalized2();
                return path;
            }

            for (var i = 0; i < path.Count - 1; i++)
            {
                var dist = path[i].Distance7(path[i + 1]);
                if (dist > Distance)
                {
                    result.Add(path[i] + Distance * (path[i + 1] - path[i]).Normalized2());
                    for (var j = i + 1; j < path.Count; j++)
                    {
                        result.Add(path[j]);
                    }

                    break;
                }
                Distance -= dist;
            }
            return result.Count > 0 ? result : new List<Vector2> { path.Last() };
        }

        /// <summary>
        ///     Returns the path of the unit appending the ServerPosition at the start, works even if the unit just entered fow.
        /// </summary>
        public static List<Vector2> GetWaypoints(this Obj_AI_Base unit)
        {
            var result = new List<Vector2>();

            if (unit.IsVisible)
            {
                result.Add(unit.ServerPosition.To2D2());
                var path = unit.Path;
                if (path.Length > 0)
                {
                    var first = path[0].To2D2();
                    if (first.Distance7(result[0], true) > 40)
                    {
                        result.Add(first);
                    }

                    for (int i = 1; i < path.Length; i++)
                    {
                        result.Add(path[i].To2D2());
                    }
                }
            }
            else if (WaypointTracker.StoredPaths.ContainsKey(unit.NetworkId))
            {
                var path = WaypointTracker.StoredPaths[unit.NetworkId];
                var timePassed = (Utils.TickCount - WaypointTracker.StoredTick[unit.NetworkId]) / 1000f;
                if (path.PathLength() >= unit.MoveSpeed * timePassed)
                {
                    result = CutPath(path, (int)(unit.MoveSpeed * timePassed));
                }
            }

            return result;
        }

        public static List<Vector2Time> GetWaypointsWithTime(this Obj_AI_Base unit)
        {
            var wp = unit.GetWaypoints();

            if (wp.Count < 1)
            {
                return null;
            }

            var result = new List<Vector2Time>();
            var speed = unit.MoveSpeed;
            var lastPoint = wp[0];
            var time = 0f;

            foreach (var point in wp)
            {
                time += point.Distance7(lastPoint) / speed;
                result.Add(new Vector2Time(point, time));
                lastPoint = point;
            }

            return result;
        }

        /// <summary>
        ///     Returns true if the buff is active and didn't expire.
        /// </summary>
        public static bool IsValidBuff(this BuffInstance buff)
        {
            return buff.IsActive && buff.EndTime - Game.Time > 0;
        }

        /// <summary>
        ///     Returns if the unit has the specified buff in the indicated amount of time
        /// </summary>
        public static bool HasBuffIn(this Obj_AI_Base unit, string displayName, float tickCount, bool includePing = true)
        {
            return
                unit.Buffs.Any(
                    buff =>
                        buff.IsValid && buff.DisplayName == displayName &&
                        buff.EndTime - Game.Time > tickCount - (includePing ? (Game.Ping / 2000f) : 0));
        }

        public static void AddStringList(this Menu m, string uniqueId, string displayName, string[] values, int defaultValue)
        {
            var mode = m.Add(uniqueId, new Slider(displayName, defaultValue, 0, values.Length - 1));
            mode.DisplayName = displayName + ": " + values[mode.CurrentValue];
            mode.OnValueChange += delegate (ValueBase<int> sender, ValueBase<int>.ValueChangeArgs args)
            {
                sender.DisplayName = displayName + ": " + values[args.NewValue];
            };
        }

        /// <summary>
        ///     Returns if the unit has the buff and it is active
        /// </summary>
        [Obsolete("Use Obj_AI_Base.HasBuff")]
        public static bool HasBuff(this Obj_AI_Base unit,
            string buffName,
            bool dontUseDisplayName = false,
            bool kappa = true)
        {
            return
                unit.Buffs.Any(
                    buff =>
                        ((dontUseDisplayName &&
                          String.Equals(buff.Name, buffName, StringComparison.CurrentCultureIgnoreCase)) ||
                         (!dontUseDisplayName &&
                          String.Equals(buff.DisplayName, buffName, StringComparison.CurrentCultureIgnoreCase))) &&
                        buff.IsValidBuff());
        }

        /// <summary>
        ///     Returns the spell slot with the name.
        /// </summary>
        public static SpellSlot GetSpellSlot(this AIHeroClient unit, string name)
        {
            foreach (var spell in
                unit.Spellbook.Spells.Where(
                    spell => String.Equals(spell.Name, name, StringComparison.CurrentCultureIgnoreCase)))
            {
                return spell.Slot;
            }

            return SpellSlot.Unknown;
        }

        /// <summary>
        ///     Returns true if the unit is under tower range.
        /// </summary>
        public static bool UnderTurret(this Obj_AI_Base unit)
        {
            return UnderTurret(unit.Position, true);
        }

        /// <summary>
        ///     Returns true if the unit is under turret range.
        /// </summary>
        public static bool UnderTurret(this Obj_AI_Base unit, bool enemyTurretsOnly)
        {
            return UnderTurret(unit.Position, enemyTurretsOnly);
        }

        public static bool UnderTurret(this Vector3 position, bool enemyTurretsOnly)
        {
            return
                ObjectManager.Get<Obj_AI_Turret>().Any(turret => turret.IsValidTarget(950, enemyTurretsOnly, position));
        }

        public static NavMeshCell ToNavMeshCell(this Vector3 position)
        {
            var nav = NavMesh.WorldToGrid(position.X, position.Y);
            return NavMesh.GetCell((short)nav.X, (short)nav.Y);
        }

        [Obsolete("Use CountEnemiesInRange", false)]
        public static int CountEnemysInRange2(this Obj_AI_Base unit, float range)
        {
            return unit.ServerPosition.CountEnemiesInRange2(range);
        }

        [Obsolete("Use CountEnemiesInRange", false)]
        public static int CountEnemysInRange2(this Vector3 point, float range)
        {
            return point.CountEnemiesInRange2(range);
        }

        /// <summary>
        ///     Counts the enemies in range of Player.
        /// </summary>
        public static int CountEnemiesInRange2(float range)
        {
            return ObjectManager.Player.CountEnemiesInRange2(range);
        }

        /// <summary>
        ///     Counts the enemies in range of Unit.
        /// </summary>
        public static int CountEnemiesInRange2(this Obj_AI_Base unit, float range)
        {
            return unit.ServerPosition.CountEnemiesInRange2(range);
        }

        /// <summary>
        ///     Counts the enemies in range of point.
        /// </summary>
        public static int CountEnemiesInRange2(this Vector3 point, float range)
        {
            return EntityManager.Heroes.Enemies.Count(h => h.IsValidTarget(range, true, point));
        }

        // Use same interface as CountEnemiesInRange
        /// <summary>
        ///     Count the allies in range of the Player.
        /// </summary>
        public static int CountAlliesInRange(float range)
        {
            return ObjectManager.Player.CountAlliesInRange(range);
        }

        /// <summary>
        ///     Counts the allies in range of the Unit.
        /// </summary>
        public static int CountAlliesInRange(this Obj_AI_Base unit, float range)
        {
            return unit.ServerPosition.CountAlliesInRange(range, unit);
        }

        /// <summary>
        ///     Counts the allies in the range of the Point. 
        /// </summary>
        public static int CountAlliesInRange(this Vector3 point, float range, Obj_AI_Base originalunit = null)
        {
            if (originalunit != null)
            {
                return EntityManager.Heroes.Allies
                    .Count(x => x.NetworkId != originalunit.NetworkId && x.IsValidTarget(range, false, point));
            }
            return EntityManager.Heroes.Allies
             .Count(x => x.IsValidTarget(range, false, point));
        }

        public static List<AIHeroClient> GetAlliesInRange(this Obj_AI_Base unit, float range)
        {
            return GetAlliesInRange(unit.ServerPosition, range, unit);
        }

        public static List<AIHeroClient> GetAlliesInRange(this Vector3 point, float range, Obj_AI_Base originalunit = null)
        {
            if (originalunit != null)
            {
                return
                    EntityManager.Heroes.Allies
                        .FindAll(x => x.NetworkId != originalunit.NetworkId && point.Distance(x.ServerPosition, true) <= range * range);
            }
            return
                   EntityManager.Heroes.Allies
                       .FindAll(x => point.Distance(x.ServerPosition, true) <= range * range);
        }

        public static List<AIHeroClient> GetEnemiesInRange(this Obj_AI_Base unit, float range)
        {
            return GetEnemiesInRange(unit.ServerPosition, range);
        }

        public static List<AIHeroClient> GetEnemiesInRange(this Vector3 point, float range)
        {
            return
                EntityManager.Heroes.Enemies
                    .FindAll(x => point.Distance(x.ServerPosition, true) <= range * range);
        }

        public static List<T> GetObjects<T>(this Vector3 position, float range) where T : GameObject, new()
        {
            return ObjectManager.Get<T>().Where(x => position.Distance6(x.Position, true) < range * range).ToList();
        }

        public static List<T> GetObjects<T>(string objectName, float range, Vector3 rangeCheckFrom = new Vector3())
            where T : GameObject, new()
        {
            if (rangeCheckFrom.Equals(Vector3.Zero))
            {
                rangeCheckFrom = ObjectManager.Player.ServerPosition;
            }

            return ObjectManager.Get<T>().Where(x => rangeCheckFrom.Distance6(x.Position, true) < range * range).ToList();
        }

        internal static int GetVenomStacks(this Obj_AI_Base target)
        {
            for (var i = 1; i < 7; i++)
            {
                if (ObjectManager.Get<Obj_GeneralParticleEmitter>()
                        .Any(e => e.Position.Distance6(target.ServerPosition) <= 70 &&
                                  e.Name == "twitch_poison_counter_0" + i + ".troy"))
                {
                    return i;
                }
            }

            return 0;
        }

        public static bool IsMovementImpaired(this AIHeroClient hero)
        {
            return hero.HasBuffOfType(BuffType.Snare) || hero.HasBuffOfType(BuffType.Stun) ||
                   hero.HasBuffOfType(BuffType.Taunt);
        }

        /// <summary>
        ///     Returns true if hero is in shop range.
        /// </summary>
        /// <returns></returns>
        public static bool InShop(this AIHeroClient hero)
        {
            return hero.IsVisible &&
                   ObjectManager.Get<Obj_Shop>()
                       .Any(s => s.Team == hero.Team && hero.Distance4(s.Position, true) < 1562500); // 1250²
        }

        public static bool InFountain(this AIHeroClient hero)
        {
            float fountainRange = 562500; //750 * 750
            var map = Map.GetMap();
            if (map != null && map.Type == Map.MapType.SummonersRift)
            {
                fountainRange = 1102500; //1050 * 1050
            }
            return hero.IsVisible &&
                   ObjectManager.Get<Obj_SpawnPoint>()
                       .Any(sp => sp.Team == hero.Team && hero.Distance4(sp.Position, true) < fountainRange);
        }

        public static short GetPacketId(this GamePacketEventArgs gamePacketEventArgs)
        {
            var packetData = gamePacketEventArgs.PacketData;
            if (packetData.Length < 2)
            {
                return 0;
            }
            return (short)(packetData[0] + packetData[1] * 256);
        }

        public static void SendAsPacket(this byte[] packetData,
            PacketChannel channel = PacketChannel.C2S,
            PacketProtocolFlags protocolFlags = PacketProtocolFlags.Reliable)
        {
            Game.SendPacket(packetData, channel, protocolFlags);
        }

        public static void ProcessAsPacket(this byte[] packetData, PacketChannel channel = PacketChannel.S2C)
        {
            Game.ProcessPacket(packetData, channel);
        }

        public static class DelayAction
        {
            public delegate void Callback();

            public static List<Action> ActionList = new List<Action>();

            static DelayAction()
            {
                Game.OnUpdate += GameOnOnGameUpdate;
            }

            private static void GameOnOnGameUpdate(EventArgs args)
            {
                for (var i = ActionList.Count - 1; i >= 0; i--)
                {
                    if (ActionList[i].Time <= Utils.GameTimeTickCount)
                    {
                        try
                        {
                            if (ActionList[i].CallbackObject != null)
                            {
                                ActionList[i].CallbackObject();
                                //Will somehow result in calling ALL non-internal marked classes of the called assembly and causes NullReferenceExceptions.
                            }
                        }
                        catch (Exception)
                        {
                            // ignored
                        }

                        ActionList.RemoveAt(i);
                    }
                }
            }

            public static void Add(int time, Callback func)
            {
                var action = new Action(time, func);
                ActionList.Add(action);
            }

            public struct Action
            {
                public Callback CallbackObject;
                public int Time;

                public Action(int time, Callback callback)
                {
                    Time = time + Utils.GameTimeTickCount;
                    CallbackObject = callback;
                }
            }
        }
        public class Map
        {
            public enum MapType
            {
                Unknown,
                SummonersRift,
                CrystalScar,
                TwistedTreeline,
                HowlingAbyss
            }

            private static readonly IDictionary<int, Map> MapById = new Dictionary<int, Map>
            {
                {
                    8,
                    new Map
                    {
                        Name = "The Crystal Scar",
                        ShortName = "crystalScar",
                        Type = MapType.CrystalScar,
                        Grid = new Vector2(13894f / 2, 13218f / 2),
                        StartingLevel = 3
                    }
                },
                {
                    10,
                    new Map
                    {
                        Name = "The Twisted Treeline",
                        ShortName = "twistedTreeline",
                        Type = MapType.TwistedTreeline,
                        Grid = new Vector2(15436f / 2, 14474f / 2),
                        StartingLevel = 1
                    }
                },
                {
                    11,
                    new Map
                    {
                        Name = "Summoner's Rift",
                        ShortName = "summonerRift",
                        Type = MapType.SummonersRift,
                        Grid = new Vector2(13982f / 2, 14446f / 2),
                        StartingLevel = 1
                    }
                },
                {
                    12,
                    new Map
                    {
                        Name = "Howling Abyss",
                        ShortName = "howlingAbyss",
                        Type = MapType.HowlingAbyss,
                        Grid = new Vector2(13120f / 2, 12618f / 2),
                        StartingLevel = 3
                    }
                }
            };

            public MapType Type { get; private set; }
            public Vector2 Grid { get; private set; }
            public string Name { get; private set; }
            public string ShortName { get; private set; }
            public int StartingLevel { get; private set; }

            /// <summary>
            ///     Returns the current map.
            /// </summary>
            public static Map GetMap()
            {
                if (MapById.ContainsKey((int)Game.MapId))
                {
                    return MapById[(int)Game.MapId];
                }

                return new Map
                {
                    Name = "Unknown",
                    ShortName = "unknown",
                    Type = MapType.Unknown,
                    Grid = new Vector2(0, 0),
                    StartingLevel = 1
                };
            }
        }

        /// <summary>
        ///     Internal class used to get the waypoints even when the enemy enters the fow of war.
        /// </summary>
        internal static class WaypointTracker
        {
            public static readonly Dictionary<int, List<Vector2>> StoredPaths = new Dictionary<int, List<Vector2>>();
            public static readonly Dictionary<int, int> StoredTick = new Dictionary<int, int>();
        }
    }

    public static class Version
    {
        public static int MajorVersion;
        public static int MinorVersion;
        public static int Build;
        public static int Revision;
        private static readonly int[] VersionArray;

        static Version()
        {
            var d = Game.Version.Split('.');
            MajorVersion = Convert.ToInt32(d[0]);
            MinorVersion = Convert.ToInt32(d[1]);
            Build = Convert.ToInt32(d[2]);
            Revision = Convert.ToInt32(d[3]);

            VersionArray = new[] { MajorVersion, MinorVersion, Build, Revision };
        }

        public static bool IsOlder(string version)
        {
            var d = version.Split('.');
            return MinorVersion < Convert.ToInt32(d[1]);
        }

        public static bool IsNewer(string version)
        {
            var d = version.Split('.');
            return MinorVersion > Convert.ToInt32(d[1]);
        }

        public static bool IsEqual(string version)
        {
            var d = version.Split('.');
            for (var i = 0; i <= d.Length; i++)
            {
                if (d[i] == null || Convert.ToInt32(d[i]) != VersionArray[i])
                {
                    return false;
                }
            }
            return true;
        }
    }

    public class Vector2Time
    {
        public Vector2 Position;
        public float Time;

        public Vector2Time(Vector2 pos, float time)
        {
            Position = pos;
            Time = time;
        }
    }
}