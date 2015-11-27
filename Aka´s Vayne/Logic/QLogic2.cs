using System;
using System.Collections.Generic;
using System.Linq;
using AddonTemplate.Utility;
using SharpDX;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace AddonTemplate.Logic
{
    public static class QLogic2
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
            return
                EntityManager.Heroes.Enemies.Any(
                    e => e.IsValidTarget(600) && e.IsVisible &&
                        e.Distance(pos) < 375) ||
                Traps.EnemyTraps.Any(t => pos.Distance6(t.Position) < 125) ||
                (pos.UnderTurret(true) && !ObjectManager.Player.UnderTurret(true)) || pos.IsWall();
        }

        public static Vector3 GetAggressiveTumblePos(this Obj_AI_Base target)
        {
            var cursorPos = Game.CursorPos;

            if (!cursorPos.IsDangerousPosition()) return cursorPos;
            //if the target is not a melee and he's alone he's not really a danger to us, proceed to 1v1 him :^ )
            if (!target.IsMelee && Heroes.Player.CountEnemiesInRange(800) == 1) return cursorPos;

            var aRC = new QGeometry.Circle(ObjectManager.Player.ServerPosition.To2D(), 300).ToPolygon().ToClipperPath();
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
            if (!target.IsMelee && ObjectManager.Player.CountEnemiesInRange2(800) == 1) return cursorPos;

            var aRC = new QGeometry.Circle(ObjectManager.Player.ServerPosition.To2D2(), 300).ToPolygon().ToClipperPath();
            var targetPosition = target.ServerPosition;
            var pList = new List<Vector3>();
            var additionalDistance = (0.106 + Game.Ping / 2000f) * target.MoveSpeed;


            foreach (var p in aRC)
            {
                var v3 = Extensions.To3D(new Vector2(p.X, p.Y));

                if (target.IsFacing2(ObjectManager.Player))
                {
                    if (!v3.IsDangerousPosition() && v3.Distance6(targetPosition) < 550) pList.Add(v3);
                }
                else
                {
                    if (!v3.IsDangerousPosition() && v3.Distance6(targetPosition) < 550 - additionalDistance) pList.Add(v3);
                }
            }
            if (ObjectManager.Player.UnderTurret() || ObjectManager.Player.CountEnemiesInRange2(800) == 1)
            {
                return pList.Count > 1 ? pList.OrderBy(el => el.Distance6(cursorPos)).FirstOrDefault() : Vector3.Zero;
            }
            if (!cursorPos.IsDangerousPosition())
            {
                return pList.Count > 1 ? pList.OrderBy(el => el.Distance6(cursorPos)).FirstOrDefault() : Vector3.Zero;
            }
            return pList.Count > 1 ? pList.OrderByDescending(el => el.Distance6(cursorPos)).FirstOrDefault() : Vector3.Zero;
        }
    }
}