using System;
using System.Collections.Generic;
using System.Linq;
using Aka_s_Vayne_reworked;
using SharpDX;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace AddonTemplate.Logic
{
    public static class QLogic
    {
        public static Vector3 TumbleOrderPos = Vector3.Zero;

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
                (Aka_s_Vayne_reworked.Functions.other.UnderEnemyTower((Vector2) pos) && Aka_s_Vayne_reworked.Functions.other.UnderEnemyTower((Vector2) Variables._Player.Position)) || collFlags.HasFlag(CollisionFlags.Wall) || collFlags.HasFlag(CollisionFlags.Building);
        }

        public static Vector3 GetJungleSafeTumblePos(Obj_AI_Base target)
        {
            var cursorPos = Game.CursorPos;
            if (IsSafeTumblePos(cursorPos)) return cursorPos;

            if (!target.IsValidTarget()) return Vector3.Zero;

            var targetPosition = target.ServerPosition;

            var myTumbleRangeCircle =
                new QGeometry.Circle(ObjectManager.Player.ServerPosition.To2D(), 300).ToPolygon().ToClipperPath();

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
                new QGeometry.Circle(ObjectManager.Player.ServerPosition.To2D(), 300).ToPolygon().ToClipperPath();

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
    }
}