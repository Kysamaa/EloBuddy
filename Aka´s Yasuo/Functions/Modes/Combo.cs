using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace AkaYasuo.Functions.Modes
{
    class Combo
    {
        public static
    Vector2 GetDashingEnd(Obj_AI_Base target)
        {
            if (!target.IsValidTarget())
            {
                return Vector2.Zero;
            }

            var baseX = ObjectManager.Player.Position.X;
            var baseY = ObjectManager.Player.Position.Y;
            var targetX = target.Position.X;
            var targetY = target.Position.Y;

            var vector = new Vector2(targetX - baseX, targetY - baseY);
            var sqrt = Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);

            var x = (float)(baseX + (Program.E.Range * (vector.X / sqrt)));
            var y = (float)(baseY + (Program.E.Range * (vector.Y / sqrt)));

            return new Vector2(x, y);
        }

        public static void putWallBehind(AIHeroClient target)
        {
            if (!Program.W.IsReady() || !Program.E.IsReady() || target.IsMelee)
                return;
            Vector2 dashPos = getNextPos(target);

            var po = Program.W.GetPrediction(target);

            float dist = Variables._Player.Position.Distance(po.UnitPosition);
            if (!target.IsMoving || Variables._Player.Distance(dashPos) <= dist + 40)
                if (dist < 330 && dist > 100 && Program.W.IsReady())
                {
                    Program.W.Cast(po.UnitPosition);
                }
        }

        public static Vector2 getNextPos(AIHeroClient target)
        {
            Vector2 dashPos = target.Position.To2D();
            if (target.IsMoving && target.Path.Count() != 0)
            {
                Vector2 tpos = target.Position.To2D();
                Vector2 path = target.Path[0].To2D() - tpos;
                path.Normalize();
                dashPos = tpos + (path * 100);
            }
            return dashPos;
        }

        public static void eBehindWall(AIHeroClient target)
        {
            if (!Program.E.IsReady() || !Variables.enemyIsJumpable(target) || target.IsMelee)
                return;
            float dist = Variables._Player.Distance(target);
            var pPos = Variables._Player.Position.To2D();
            Vector2 dashPos = target.Position.To2D();
            if (!target.IsMoving || Variables._Player.Distance(dashPos) <= dist)
            {
                foreach (Obj_AI_Base enemy in ObjectManager.Get<Obj_AI_Base>().Where(enemy => Variables.enemyIsJumpable(enemy)))
                {
                    Vector2 posAfterE = pPos + (Vector2.Normalize(enemy.Position.To2D() - pPos) * Program.E.Range);
                    if ((target.Distance(posAfterE) < dist
                         || target.Distance(posAfterE) < Variables._Player.GetAutoAttackRange(target) + 100)
                        && goesThroughWall(target.Position, posAfterE.To3D()))
                    {
                        if (Events._game.useENormal(target))
                            return;
                    }
                }
            }
        }

        public static bool goesThroughWall(Vector3 vec1, Vector3 vec2)
        {
            if (Variables.wall.endtime < Game.Time || Variables.wall.pointL == null || Variables.wall.pointL == null)
                return false;
            Vector2 inter = Variables.LineIntersectionPoint(vec1.To2D(), vec2.To2D(), Variables.wall.pointL.Position.To2D(),
                Variables.wall.pointR.Position.To2D());
            float wallW = (300 + 50 * Program.W.Level);
            if (Variables.wall.pointL.Position.To2D().Distance(inter) > wallW ||
                Variables.wall.pointR.Position.To2D().Distance(inter) > wallW)
                return false;
            var dist = vec1.Distance(vec2);
            if (vec1.To2D().Distance(inter) + vec2.To2D().Distance(inter) - 30 > dist)
                return false;

            return true;
        }
    }
}
