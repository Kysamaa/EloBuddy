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

        public static Vector3 GetAggressiveTumblePos(this Obj_AI_Base target)
        {
            var cursorPos = Game.CursorPos;

            if (!cursorPos.IsDangerousPosition()) return cursorPos;
            //if the target is not a melee and he's alone he's not really a danger to us, proceed to 1v1 him :^ )
            if (!target.IsMelee && Variables._Player.CountEnemiesInRange(800) == 1) return cursorPos;

            var aRC = new QGeometry.Circle(Variables._Player.ServerPosition.To2D(), 300).ToPolygon().ToClipperPath();
            var targetPosition = target.ServerPosition;


            foreach (var p in aRC)
            {
                var v3 = new Vector2(p.X, p.Y).To3D();
                var dist = v3.Distance(targetPosition);
                if (dist > 325 && dist < 450)
                {
                    return v3;
                }
            }
            return Vector3.Zero;
        }

        public static Vector3 GetTumblePos(this Obj_AI_Base target)
        {
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            return GetAggressiveTumblePos(target);

            var cursorPos = Game.CursorPos;

            if (!cursorPos.IsDangerousPosition()) return cursorPos;
            //if the target is not a melee and he's alone he's not really a danger to us, proceed to 1v1 him :^ )
            if (!target.IsMelee && Variables._Player.CountEnemiesInRange(800) == 1) return cursorPos;

            var aRC = new QGeometry.Circle(Variables._Player.ServerPosition.To2D(), 300).ToPolygon().ToClipperPath();
            var targetPosition = target.ServerPosition;
            var pList = new List<Vector3>();
            var additionalDistance = (0.106 + Game.Ping / 2000f) * target.MoveSpeed;


            foreach (var p in aRC)
            {
                var v3 = Extensions.To3D(new Vector2(p.X, p.Y));

                if (target.IsFacing(Variables._Player))
                {
                    if (!v3.IsDangerousPosition() && v3.Distance(targetPosition) < 550) pList.Add(v3);
                }
                else
                {
                    if (!v3.IsDangerousPosition() && v3.Distance(targetPosition) < 550 - additionalDistance) pList.Add(v3);
                }
            }
            if (Aka_s_Vayne_reworked.Functions.other.UnderEnemyTower((Vector2) Variables._Player.Position) || Variables._Player.CountEnemiesInRange(800) == 1)
            {
                return pList.Count > 1 ? pList.OrderBy(el => el.Distance(cursorPos)).FirstOrDefault() : Vector3.Zero;
            }
            if (!cursorPos.IsDangerousPosition())
            {
                return pList.Count > 1 ? pList.OrderBy(el => el.Distance(cursorPos)).FirstOrDefault() : Vector3.Zero;
            }
            return pList.Count > 1 ? pList.OrderByDescending(el => el.Distance(cursorPos)).FirstOrDefault() : Vector3.Zero;
        }

        public static void JungleClear()
        {
            Obj_AI_Base jungleMobs = EntityManager.MinionsAndMonsters.GetJungleMonsters(Variables._Player.Position, Program.Q.Range, true).FirstOrDefault();
            {
                if (MenuManager.JungleClearMenu["JCQ"].Cast<CheckBox>().CurrentValue && Program.Q.IsReady() && jungleMobs != null && jungleMobs.IsValidTarget(Program.Q.Range))             
                {
                    QLogic.Cast(Variables._Player.GetTumblePos());
                }
            }
        }
    }
}