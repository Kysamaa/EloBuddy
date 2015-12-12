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
using ClipperLib;

namespace AkaYasuo
{
    public enum CollisionObjectTypes
    {
        Minion,
        Champion,
        YasuoWall,
    }

    internal class FastPredResult
    {
        public Vector2 CurrentPos;
        public bool IsMoving;
        public Vector2 PredictedPos;
    }

    internal class DetectedCollision
    {
        public float Diff;
        public float Distance;
        public Vector2 Position;
        public CollisionObjectTypes Type;
        public Obj_AI_Base Unit;
    }

    internal static class Collision
    {
        private static int WallCastT;
        private static Vector2 YasuoWallCastedPos;

        public static void Init()
        {
            Obj_AI_Base.OnProcessSpellCast += Obj_AI_Hero_OnProcessSpellCast;
        }


        private static void Obj_AI_Hero_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsValid && sender.Team == ObjectManager.Player.Team && args.SData.Name == "YasuoWMovingWall")
            {
                WallCastT = Environment.TickCount;
                YasuoWallCastedPos = sender.ServerPosition.To2D();
            }
        }

        public static FastPredResult FastPrediction(Vector2 from, Obj_AI_Base unit, int delay, int speed)
        {
            var tDelay = delay / 1000f + (from.Distance(unit) / speed);
            var d = tDelay * unit.MoveSpeed;
            var path = unit.GetWaypoints();

            if (path.PathLength() > d)
            {
                return new FastPredResult
                {
                    IsMoving = true,
                    CurrentPos = unit.ServerPosition.To2D(),
                    PredictedPos = path.CutPath((int)d)[0],
                };
            }
            if (path.Count == 0)
            {
                return new FastPredResult
                {
                    IsMoving = false,
                    CurrentPos = unit.ServerPosition.To2D(),
                    PredictedPos = unit.ServerPosition.To2D(),
                };
            }
            return new FastPredResult
            {
                IsMoving = false,
                CurrentPos = path[path.Count - 1],
                PredictedPos = path[path.Count - 1],
            };
        }

        public static Vector2 GetCollisionPoint(Skillshot skillshot)
        {
            var collisions = new List<DetectedCollision>();
            var from = skillshot.GetMissilePosition(0);
            skillshot.ForceDisabled = false;
            foreach (var cObject in skillshot.SpellData.CollisionObjects)
            {
                switch (cObject)
                {
                    case CollisionObjectTypes.Minion:

                        foreach (var minion in EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, ObjectManager.Player.ServerPosition ))
                            
                        {
                            var pred = FastPrediction(
                                from, minion,
                                Math.Max(0, skillshot.SpellData.Delay - (Environment.TickCount - skillshot.StartTick)),
                                skillshot.SpellData.MissileSpeed);
                            var pos = pred.PredictedPos;
                            var w = skillshot.SpellData.RawRadius +
                                    (!pred.IsMoving ? (minion.BoundingRadius - 15) : 0) -
                                    pos.Distance(from, skillshot.End, true);
                            if (w > 0)
                            {
                                collisions.Add(
                                    new DetectedCollision
                                    {
                                        Position =
                                            pos.ProjectOn(skillshot.End, skillshot.Start).LinePoint +
                                            skillshot.Direction * 30,
                                        Unit = minion,
                                        Type = CollisionObjectTypes.Minion,
                                        Distance = pos.Distance(from),
                                        Diff = w,
                                    });
                            }
                        }

                        break;

                    case CollisionObjectTypes.Champion:
                        foreach (var hero in
                            ObjectManager.Get<AIHeroClient>()
                                .Where(
                                    h =>
                                        (h.IsValidTarget(1200) && h.Team == ObjectManager.Player.Team && !h.IsMe ||
                                         h.Team != ObjectManager.Player.Team)))
                        {
                            var pred = FastPrediction(
                                from, hero,
                                Math.Max(0, skillshot.SpellData.Delay - (Environment.TickCount - skillshot.StartTick)),
                                skillshot.SpellData.MissileSpeed);
                            var pos = pred.PredictedPos;

                            var w = skillshot.SpellData.RawRadius + 30 - pos.Distance(from, skillshot.End, true);
                            if (w > 0)
                            {
                                collisions.Add(
                                    new DetectedCollision
                                    {
                                        Position =
                                            pos.ProjectOn(skillshot.End, skillshot.Start).LinePoint +
                                            skillshot.Direction * 30,
                                        Unit = hero,
                                        Type = CollisionObjectTypes.Minion,
                                        Distance = pos.Distance(from),
                                        Diff = w,
                                    });
                            }
                        }
                        break;

                    case CollisionObjectTypes.YasuoWall:
                        if (
                            !ObjectManager.Get<AIHeroClient>()
                                .Any(
                                    hero =>
                                        hero.IsValidTarget(float.MaxValue) &&
                                        hero.Team == ObjectManager.Player.Team && hero.ChampionName == "Yasuo"))
                        {
                            break;
                        }
                        GameObject wall = null;
                        foreach (var gameObject in ObjectManager.Get<GameObject>())
                        {
                            if (gameObject.IsValid &&
                                Object.Equals(
                                    gameObject.Name, "_w_windwall.\\.troy"
                                    ))
                            {
                                wall = gameObject;
                            }
                        }
                        if (wall == null)
                        {
                            break;
                        }
                        var level = wall.Name.Substring(wall.Name.Length - 6, 1);
                        var wallWidth = (300 + 50 * Convert.ToInt32(level));


                        var wallDirection = (wall.Position.To2D() - YasuoWallCastedPos).Normalized().Perpendicular();
                        var wallStart = wall.Position.To2D() + wallWidth / 2 * wallDirection;
                        var wallEnd = wallStart - wallWidth * wallDirection;
                        var wallPolygon = new Geometry.Rectangle(wallStart, wallEnd, 75).ToPolygon();
                        var intersection = new Vector2();
                        var intersections = new List<Vector2>();

                        for (var i = 0; i < wallPolygon.Points.Count; i++)
                        {
                            var inter =
                                wallPolygon.Points[i].Intersection(
                                    wallPolygon.Points[i != wallPolygon.Points.Count - 1 ? i + 1 : 0], from,
                                    skillshot.End);
                            if (inter.Intersects)
                            {
                                intersections.Add(inter.Point);
                            }
                        }

                        if (intersections.Count > 0)
                        {
                            intersection = intersections.OrderBy(item => item.Distance(from)).ToList()[0];
                            var collisionT = Environment.TickCount +
                                             Math.Max(
                                                 0,
                                                 skillshot.SpellData.Delay -
                                                 (Environment.TickCount - skillshot.StartTick)) + 100 +
                                             (1000 * intersection.Distance(from)) / skillshot.SpellData.MissileSpeed;
                            if (collisionT - WallCastT < 4000)
                            {
                                if (skillshot.SpellData.Type != SkillShotType.SkillshotMissileLine)
                                {
                                    skillshot.ForceDisabled = true;
                                }
                                return intersection;
                            }
                        }

                        break;
                }
            }

            Vector2 result;
            if (collisions.Count > 0)
            {
                result = collisions.OrderBy(c => c.Distance).ToList()[0].Position;
            }
            else
            {
                result = new Vector2();
            }

            return result;
        }
    }
}