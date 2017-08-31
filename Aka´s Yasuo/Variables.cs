
using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

namespace AkaYasuo
{
    class Variables
    {
        public static bool wallCasted;

        public static YasWall wall = new YasWall();

        public static bool IsDashing = false;

        public static float startDash = 0;

        public static float time = 0;

        public static Vector3 castFrom;

        private static BuffType[] buffs;

        public static string[] interrupt;

        public static AIHeroClient _Player { get { return ObjectManager.Player; } }

        public static int[] abilitySequence;

        public static int QOff = 0, WOff = 0, EOff = 0, ROff = 0;

        public static List<Skillshot> DetectedSkillShots = new List<Skillshot>();

        public static List<Skillshot> EvadeDetectedSkillshots = new List<Skillshot>();

        public static float HealthPercent { get { return _Player.Health / _Player.MaxHealth * 100; } }

        internal class YasWall
        {
            public MissileClient pointL;
            public MissileClient pointR;
            public float endtime = 0;
            public YasWall()
            {

            }

            public YasWall(MissileClient L, MissileClient R)
            {
                pointL = L;
                pointR = R;
                endtime = Game.Time + 4;
            }

            public void setR(MissileClient R)
            {
                pointR = R;
                endtime = Game.Time + 4;
            }

            public void setL(MissileClient L)
            {
                pointL = L;
                endtime = Game.Time + 4;
            }

            public bool isValid(int time = 0)
            {
                return pointL != null && pointR != null && endtime - (time / 1000) > Game.Time;
            }
        }

        public static bool Q3READY(AIHeroClient unit)
        {
            return _Player.HasBuff("YasuoQ3W");
        }

        public static bool CanCastE(Obj_AI_Base target)
        {
            return !target.HasBuff("YasuoDashWrapper");
        }

        public static bool IsKnockedUp(AIHeroClient target)
        {
            return target.HasBuffOfType(BuffType.Knockup) || target.HasBuffOfType(BuffType.Knockback);
        }

        public static bool CanCastDelayR(AIHeroClient target)
        {
            var buff = target.Buffs.FirstOrDefault(i => i.Type == BuffType.Knockback || i.Type == BuffType.Knockup);
            return buff != null && buff.EndTime - Game.Time <= (buff.EndTime - buff.StartTime) / 3;
        }

        public static bool AlliesNearTarget(Obj_AI_Base target, float range)
        {
            return EntityManager.Heroes.Allies.Where(tar => tar.Distance(target) < range).Any(tar => tar != null);
        }

        public static Vector2 V2E(Vector3 from, Vector3 direction, float distance)
        {
            return (from + distance * Vector3.Normalize(direction - from)).To2D();
        }

        public static bool isDashing
        {
            get
            {
                return IsDashing || _Player.IsDashing();
            }
        }

        public static bool UnderTower(Vector3 v)
        {
            return EntityManager.Turrets.Enemies.Where(a => a.Health > 0 && !a.IsDead).Any(a => a.Distance(v) < 950);
        }

        public static bool skillShotIsDangerous(string Name)
        {
            if (MenuManager.SubMenu["SMIN"]["IsDangerous" + Name] != null)
            {
                return MenuManager.SubMenu["SMIN"]["IsDangerous" + Name].Cast<CheckBox>().CurrentValue;
            }
            return true;
        }

        public static bool EvadeSpellEnabled(string Name)
        {
            if (MenuManager.SubMenu["SMIN"]["Enabled" + Name] != null)
            {
                return MenuManager.SubMenu["SMIN"]["Enabled" + Name].Cast<CheckBox>().CurrentValue;
            }
            return true;
        }

        public static bool enemyIsJumpable(Obj_AI_Base enemy, List<AIHeroClient> ignore = null)
        {
            if (enemy.IsValid && enemy.IsEnemy && !enemy.IsInvulnerable && !enemy.MagicImmune && !enemy.IsDead &&
                !(enemy is FollowerObject))
            {
                if (ignore != null)
                    foreach (AIHeroClient ign in ignore)
                    {
                        if (ign.NetworkId == enemy.NetworkId)
                            return false;
                    }
                foreach (BuffInstance buff in enemy.Buffs)
                {
                    if (buff.Name == "YasuoDashWrapper")
                        return false;
                }
                return true;
            }
            return false;
        }

        public static bool willColide(Skillshot ss, Vector2 from, float speed, Vector2 direction, float radius)
        {
            Vector2 ssVel = ss.Direction.Normalized() * ss.SpellData.MissileSpeed;
            Vector2 dashVel = direction * speed;
            Vector2 a = ssVel - dashVel;
            Vector2 realFrom = from.Extend(direction, ss.SpellData.Delay + speed);
            if (!ss.IsAboutToHit((int)((dashVel.Length() / 475) * 1000) + Game.Ping + 100, ObjectManager.Player))
                return false;
            if (ss.IsAboutToHit(1000, ObjectManager.Player) &&
                interCir(ss.MissilePosition, ss.MissilePosition.Extend(ss.MissilePosition + a, ss.SpellData.Range + 50),
                    from,
                    radius))
                return true;
            return false;
        }

        public static bool wontHitOnDash(Skillshot ss, Obj_AI_Base jumpOn, Skillshot skillShot, Vector2 dashDir)
        {
            float currentDashSpeed = 700 + _Player.MoveSpeed; 

            Vector2 intersectionPoint = LineIntersectionPoint(_Player.Position.To2D(),
                V2E(_Player.Position, jumpOn.Position, 475), ss.Start, ss.End);

            float arrivingTime = Vector2.Distance(_Player.Position.To2D(), intersectionPoint) / currentDashSpeed;

            Vector2 skillshotPosition = ss.GetMissilePosition((int)(arrivingTime * 1000));
            if (Vector2.DistanceSquared(skillshotPosition, intersectionPoint) <
                (ss.SpellData.Radius + _Player.BoundingRadius) &&
                !willColide(skillShot, _Player.Position.To2D(), 700f + _Player.MoveSpeed, dashDir,
                    _Player.BoundingRadius + skillShot.SpellData.Radius))
                return false;
            return true;
        }

        public static bool interCir(Vector2 E, Vector2 L, Vector2 C, float r)
        {
            Vector2 d = L - E;
            Vector2 f = E - C;

            float a = Vector2.Dot(d, d);
            float b = 2 * Vector2.Dot(f, d);
            float c = Vector2.Dot(f, f) - r * r;

            float discriminant = b * b - 4 * a * c;
            if (discriminant < 0)
            {

            }
            else
            {

                discriminant = (float)Math.Sqrt(discriminant);

                float t1 = (-b - discriminant) / (2 * a);
                float t2 = (-b + discriminant) / (2 * a);

                if (t1 >= 0 && t1 <= 1)
                {
                    return true;
                }

                if (t2 >= 0 && t2 <= 1)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public static int GetNewQSpeed()
        {
            return (int)(1 / (1 / 0.5 * Player.Instance.AttackSpeedMod));
        }

        public static Vector2 LineIntersectionPoint(Vector2 ps1, Vector2 pe1, Vector2 ps2,
        Vector2 pe2)
        {
            // Get A,B,C of first line - points : ps1 to pe1
            float A1 = pe1.Y - ps1.Y;
            float B1 = ps1.X - pe1.X;
            float C1 = A1 * ps1.X + B1 * ps1.Y;

            // Get A,B,C of second line - points : ps2 to pe2
            float A2 = pe2.Y - ps2.Y;
            float B2 = ps2.X - pe2.X;
            float C2 = A2 * ps2.X + B2 * ps2.Y;

            // Get delta and check if the lines are parallel
            float delta = A1 * B2 - A2 * B1;
            if (delta == 0)
                return new Vector2(-1, -1);

            // now return the Vector2 intersection point
            return new Vector2(
                (B2 * C1 - B1 * C2) / delta,
                (A1 * C2 - A2 * C1) / delta
                );
        }

        public static Vector2 PosAfterE(Obj_AI_Base target)
        {
            if (!target.IsValidTarget())
            {
                return Vector2.Zero;
            }

            var baseX = _Player.Position.X;
            var baseY = _Player.Position.Y;
            var targetX = target.Position.X;
            var targetY = target.Position.Y;

            var vector = new Vector2(targetX - baseX, targetY - baseY);
            var sqrt = Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);

            var x = (float)(baseX + (Program.E.Range * (vector.X / sqrt)));
            var y = (float)(baseY + (Program.E.Range * (vector.Y / sqrt)));

            return new Vector2(x, y);
        }
    }
}
