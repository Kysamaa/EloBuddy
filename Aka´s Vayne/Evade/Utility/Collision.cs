﻿// Copyright 2014 - 2015 Esk0r
// Collision.cs is part of Evade.
// 
// Evade is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Evade is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Evade. If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace Aka_s_Vayne_reworked.Evade.Utility
{
    public enum CollisionObjectTypes
    {
        Minion,
        Champion,
        YasuoWall
    }

    public class FastPredictionResult
    {
        public Vector2 CurrentPosVector2;
        public bool IsMoving;
        public Vector2 PredictedPosVector2;
    }

    public class DetectedCollision
    {
        public float Difference;
        public float Distance;
        public Vector2 PositionVector2;
        public CollisionObjectTypes Type;
        public AIHeroClient UnitAiBase;
    }

    public static class Collision
    {
        private static int _wallCastTick;
        private static Vector2 _yasuoWallVector2;

        public static void Init()
        {
            Obj_AI_Base.OnProcessSpellCast += Obj_AI_Base_OnProcessSpellCast;
        }

        private static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!sender.IsValid || sender.Team != GameObjectTeam.Neutral || !args.SData.Name.Equals("YasuoWMovingWall"))
            {
                return;
            }

            _wallCastTick = Environment.TickCount;
            _yasuoWallVector2 = sender.ServerPosition.To2D();
        }

        public static Vector3[] CutPath(Vector3[] path, float distance)
        {
            var result = new List<Vector3>();
            var Distance = distance;

            if (path.Length > 0)
            {
                result.Add(path.First());
            }

            for (var i = 0; i < path.Length - 1; i++)
            {
                var dist = path[i].Distance(path[i + 1]);
                if (dist > Distance)
                {
                    result.Add(path[i] + Distance * (path[i + 1] - path[i]).Normalized());
                    break;
                }
                else
                {
                    result.Add(path[i + 1]);
                }
                Distance -= dist;
            }
            return result.Count > 0 ? result.ToArray() : new List<Vector3> { path.Last() }.ToArray();
        }

        public static FastPredictionResult FastPrediction(Vector2 fromVector2,
            Obj_AI_Base unitAiBase,
            int delay,
            int speed)
        {
            var tickDelay = delay / 1000f + (fromVector2.Distance(unitAiBase) / speed);
            var moveSpeedF = tickDelay * unitAiBase.MoveSpeed;
            var path = unitAiBase.Path;

            if (path.Length > moveSpeedF)
            {
                return new FastPredictionResult
                {
                    IsMoving = true,
                    CurrentPosVector2 = unitAiBase.ServerPosition.To2D(),
                    PredictedPosVector2 = CutPath(path, moveSpeedF)[0].To2D()
                };
            }

            if (path.Count() == 0)
            {
                return new FastPredictionResult
                {
                    IsMoving = false,
                    CurrentPosVector2 = unitAiBase.ServerPosition.To2D(),
                    PredictedPosVector2 = unitAiBase.ServerPosition.To2D()
                };
            }

            return new FastPredictionResult
            {
                IsMoving = false,
                CurrentPosVector2 = path[path.Count() - 1].To2D(),
                PredictedPosVector2 = path[path.Count() - 1].To2D(),
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

                        collisions.AddRange(
                            from minion in
                                EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy,
                                    @from.To3D(), 1200,
                                    skillshot.Caster.Team == ObjectManager.Player.Team)
                            let pred =
                                FastPrediction(
                                    @from, minion,
                                    Math.Max(
                                        0, skillshot.SpellData.Delay - (Environment.TickCount - skillshot.StartTick)),
                                    skillshot.SpellData.MissileSpeed)
                            let pos = pred.PredictedPosVector2
                            let w =
                                skillshot.SpellData.RawRadius + (!pred.IsMoving ? (minion.BoundingRadius - 15) : 0) -
                                pos.Distance(@from, skillshot.End, true)
                            where w > 0
                            select
                                new DetectedCollision
                                {
                                    PositionVector2 =
                                        pos.ProjectOn(skillshot.End, skillshot.Start).LinePoint +
                                        skillshot.Direction * 30,
                                    Type = CollisionObjectTypes.Minion,
                                    Distance = pos.Distance(@from),
                                    Difference = w
                                });

                        break;

                    case CollisionObjectTypes.Champion:
                        collisions.AddRange(
                            from hero in
                                ObjectManager.Get<AIHeroClient>()
                                    .Where(
                                        h =>
                                            (h.IsValidTarget(1200, false) && h.Team == ObjectManager.Player.Team &&
                                             !h.IsMe || h.Team != ObjectManager.Player.Team))
                            let pred =
                                FastPrediction(
                                    @from, hero,
                                    Math.Max(
                                        0, skillshot.SpellData.Delay - (Environment.TickCount - skillshot.StartTick)),
                                    skillshot.SpellData.MissileSpeed)
                            let pos = pred.PredictedPosVector2
                            let w = skillshot.SpellData.RawRadius + 30 - pos.Distance(@from, skillshot.End, true)
                            where w > 0
                            select
                                new DetectedCollision
                                {
                                    PositionVector2 =
                                        pos.ProjectOn(skillshot.End, skillshot.Start).LinePoint +
                                        skillshot.Direction * 30,
                                    UnitAiBase = hero,
                                    Type = CollisionObjectTypes.Minion,
                                    Distance = pos.Distance(@from),
                                    Difference = w
                                });
                        break;

                    case CollisionObjectTypes.YasuoWall:
                        if (
                            !ObjectManager.Get<AIHeroClient>()
                                .Any(
                                    hero =>
                                        hero.IsValidTarget(float.MaxValue, false) &&
                                        hero.Team == ObjectManager.Player.Team && hero.ChampionName == "Yasuo"))
                        {
                            break;
                        }
                        GameObject wall = null;
                        foreach (
                            var gameObject in
                                ObjectManager.Get<GameObject>()
                                    .Where(
                                        gameObject =>
                                            gameObject.IsValid &&
                                            Regex.IsMatch(
                                                gameObject.Name, "_w_windwall.\\.troy", RegexOptions.IgnoreCase)))
                        {
                            wall = gameObject;
                        }
                        if (wall == null)
                        {
                            break;
                        }
                        var level = wall.Name.Substring(wall.Name.Length - 6, 1);
                        var wallWidth = (300 + 50 * Convert.ToInt32(level));


                        var wallDirection = (wall.Position.To2D() - _yasuoWallVector2).Normalized().Perpendicular();
                        var fraction = wallWidth / 0x2; // 0x2 = 2
                        var wallStart = wall.Position.To2D() + fraction * wallDirection;
                        var wallEnd = wallStart - wallWidth * wallDirection;
                        var wallPolygon = new Geometry.Rectangle(wallStart, wallEnd, 75).ToPolygon();
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
                            var intersection = intersections.OrderBy(item => item.Distance(from)).ToList()[0];
                            var collisionT = Environment.TickCount +
                                             Math.Max(
                                                 0,
                                                 skillshot.SpellData.Delay -
                                                 (Environment.TickCount - skillshot.StartTick)) + 100 +
                                             (1000 * intersection.Distance(from)) / skillshot.SpellData.MissileSpeed;
                            if (collisionT - _wallCastTick < 4000)
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

            return collisions.Count > 0
                ? collisions.OrderBy(c => c.Distance).ToList()[0].PositionVector2
                : new Vector2();
        }
    }
}