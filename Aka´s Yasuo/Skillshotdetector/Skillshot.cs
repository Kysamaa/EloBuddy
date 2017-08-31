using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace AkaYasuo
{
    public enum SkillShotType
    {
        SkillshotCircle,
        SkillshotLine,
        SkillshotMissileLine,
        SkillshotCone,
        SkillshotMissileCone,
        SkillshotRing
    }

    public enum DetectionType
    {
        RecvPacket,
        ProcessSpell
    }

    public struct SafePathResult
    {
        public FoundIntersection Intersection;
        public bool IsSafe;

        public SafePathResult(bool isSafe, FoundIntersection intersection)
        {
            Intersection = intersection;
            IsSafe = isSafe;
        }
    }

    public struct FoundIntersection
    {
        private const int GridSize = 10;
        public Vector2 ComingFromVector2;
        public float Distance;
        public bool IsValid;
        public Vector2 PointVector2;
        public int Time;

        public FoundIntersection(float distance, int time, Vector2 pointVector2, Vector2 comingFromVector2)
        {
            Distance = distance;
            ComingFromVector2 = comingFromVector2;
            IsValid = pointVector2.IsValid();
            PointVector2 = pointVector2 + GridSize * (ComingFromVector2 - pointVector2).Normalized();
            Time = time;
        }
    }

    public class Skillshot
    {
        private const int ExtraEvadeDistance = 15;
        private Vector2 _collisionEnd;
        private int _lastCollisionCalc;
        public Geometry.Circle Circle;
        public DetectionType DetectionType;
        public Vector2 Direction;
        public Geometry.Polygon DrawingPolygon;
        public Vector2 End;
        public bool ForceDisabled;
        public Vector2 MissilePosition;
        public Geometry.Polygon Polygon;
        public Geometry.Rectangle Rectangle;
        public Geometry.Ring Ring;
        public Geometry.Sector Sector;
        public SpellData SpellData;
        public Vector2 Start;
        public int StartTick;
        public Obj_AI_Base Target;

        public Skillshot(DetectionType detectionType,
            SpellData spellData,
            int startT,
            Vector2 start,
            Vector2 end,
            Obj_AI_Base unit,
            Obj_AI_Base target = null
            )
        {
            DetectionType = detectionType;
            SpellData = spellData;
            StartTick = startT;
            Start = start;
            End = end;
            MissilePosition = start;
            Direction = (end - start).Normalized();
            Target = target;
            Unit = unit;

            //Create the spatial object for each type of skillshot.
            switch (spellData.Type)
            {
                case SkillShotType.SkillshotCircle:
                    Circle = new Geometry.Circle(CollisionEnd, spellData.Radius);
                    break;
                case SkillShotType.SkillshotLine:
                    Rectangle = new Geometry.Rectangle(Start, CollisionEnd, spellData.Radius);
                    break;
                case SkillShotType.SkillshotMissileLine:
                    Rectangle = new Geometry.Rectangle(Start, CollisionEnd, spellData.Radius);
                    break;
                case SkillShotType.SkillshotCone:
                    Sector = new Geometry.Sector(
                        start, CollisionEnd - start, spellData.Radius * (float) Math.PI / 180, spellData.Range);
                    break;
                case SkillShotType.SkillshotRing:
                    Ring = new Geometry.Ring(CollisionEnd, spellData.Radius, spellData.RingRadius);
                    break;
            }

            UpdatePolygon(); //Create the polygon.
        }

        public Vector2 Perpendicular
        {
            get { return Direction.Perpendicular(); }
        }

        public Vector2 CollisionEnd
        {
            get
            {
                if (_collisionEnd.IsValid())
                {
                    return _collisionEnd;
                }

                if (IsGlobal)
                {
                    return GlobalGetMissilePosition(0) +
                           Direction * SpellData.MissileSpeed *
                           (0.5f + SpellData.Radius * 2 / ObjectManager.Player.MoveSpeed);
                }

                return End;
            }
        }

        public bool IsGlobal
        {
            get { return SpellData.RawRange == 20000; }
        }

        public Geometry.Polygon EvadePolygon { get; set; }
        public Obj_AI_Base Unit { get; set; }

        /// <summary>
        ///     Returns if the skillshot has expired.
        /// </summary>
        public bool IsActive()
        {
            if (SpellData.MissileAccel != 0)
            {
                return Environment.TickCount <= StartTick + 5000;
            }
            if (Target != null)
            {
                return Environment.TickCount <=
                   StartTick + SpellData.Delay + SpellData.ExtraDuration +
                   1000 * (Start.Distance(Target) / SpellData.MissileSpeed);
            }

            return Environment.TickCount <=
                   StartTick + SpellData.Delay + SpellData.ExtraDuration +
                   1000 * (Start.Distance(End) / SpellData.MissileSpeed);
        }

        public void Game_OnGameUpdate()
        {
            //Even if it doesnt consume a lot of resources with 20 updatest second works k
            if (SpellData.CollisionObjects.Count() > 0 && SpellData.CollisionObjects != null &&
                Environment.TickCount - _lastCollisionCalc > 50)
            {
                _lastCollisionCalc = Environment.TickCount;
                _collisionEnd = Collision.GetCollisionPoint(this);
            }

            //Update the missile position each time the game updates.
            if (SpellData.Type == SkillShotType.SkillshotMissileLine)
            {
                Rectangle = new Geometry.Rectangle(GetMissilePosition(0), CollisionEnd, SpellData.Radius);
                UpdatePolygon();
            }

            //Spells that update to the unit position.
            if (SpellData.MissileFollowsUnit)
            {
                if (Unit.IsVisible)
                {
                    End = Unit.ServerPosition.To2D();
                    Direction = (End - Start).Normalized();
                    UpdatePolygon();
                }
            }
        }

        public void UpdatePolygon()
        {
            switch (SpellData.Type)
            {
                case SkillShotType.SkillshotCircle:
                    Polygon = Circle.ToPolygon();
                    EvadePolygon = Circle.ToPolygon(ExtraEvadeDistance);
                    DrawingPolygon = Circle.ToPolygon(
                        0,
                        !SpellData.AddHitbox
                            ? SpellData.Radius
                            : (SpellData.Radius - ObjectManager.Player.BoundingRadius));
                    break;
                case SkillShotType.SkillshotLine:
                    Polygon = Rectangle.ToPolygon();
                    DrawingPolygon = Rectangle.ToPolygon(
                        0,
                        !SpellData.AddHitbox
                            ? SpellData.Radius
                            : (SpellData.Radius - ObjectManager.Player.BoundingRadius));
                    EvadePolygon = Rectangle.ToPolygon(ExtraEvadeDistance);
                    break;
                case SkillShotType.SkillshotMissileLine:
                    Polygon = Rectangle.ToPolygon();
                    DrawingPolygon = Rectangle.ToPolygon(
                        0,
                        !SpellData.AddHitbox
                            ? SpellData.Radius
                            : (SpellData.Radius - ObjectManager.Player.BoundingRadius));
                    EvadePolygon = Rectangle.ToPolygon(ExtraEvadeDistance);
                    break;
                case SkillShotType.SkillshotCone:
                    Polygon = Sector.ToPolygon();
                    DrawingPolygon = Polygon;
                    EvadePolygon = Sector.ToPolygon(ExtraEvadeDistance);
                    break;
                case SkillShotType.SkillshotRing:
                    Polygon = Ring.ToPolygon();
                    DrawingPolygon = Polygon;
                    EvadePolygon = Ring.ToPolygon(ExtraEvadeDistance);
                    break;
            }
        }

        /// <summary>
        ///     Returns the missile position after time time.
        /// </summary>
        public Vector2 GlobalGetMissilePosition(int time)
        {
            var t = Math.Max(0, Environment.TickCount + time - StartTick - SpellData.Delay);
            var fraction = t * SpellData.MissileSpeed / 0x3e8; // 0x3e8 = 1000
            t = (int) Math.Max(0, Math.Min(End.Distance(Start), fraction));
            return Start + Direction * t;
        }

        /// <summary>
        ///     Returns the missile position after time time.
        /// </summary>
        public Vector2 GetMissilePosition(int time)
        {
            var t = Math.Max(0, Environment.TickCount + time - StartTick - SpellData.Delay);
            int x;

            //Missile with acceleration = 0.
            if (SpellData.MissileAccel == 0)
            {
                x = t * SpellData.MissileSpeed / 1000;
            }
            else
            {
                //Missile with constant acceleration.
                var t1 = (SpellData.MissileAccel > 0
                    ? SpellData.MissileMaxSpeed
                    : SpellData.MissileMinSpeed - SpellData.MissileSpeed) * 1000f / SpellData.MissileAccel;

                if (t <= t1)
                {
                    x =
                        (int)
                            (t * SpellData.MissileSpeed / 1000d + 0.5d * SpellData.MissileAccel * Math.Pow(t / 1000d, 2));
                }
                else
                {
                    x =
                        (int)
                            (t1 * SpellData.MissileSpeed / 1000d +
                             0.5d * SpellData.MissileAccel * Math.Pow(t1 / 1000d, 2) +
                             (t - t1) / 1000d *
                             (SpellData.MissileAccel < 0 ? SpellData.MissileMaxSpeed : SpellData.MissileMinSpeed));
                }
            }

            t = (int) Math.Max(0, Math.Min(CollisionEnd.Distance(Start), x));
            return Start + Direction * t;
        }

        /// <summary>
        ///     Returns if the skillshot will hit you when trying to blink to the point.
        /// </summary>
        public bool IsSafeToBlink(Vector2 point, int timeOffset, int delay = 0)
        {
            timeOffset /= 2;

            if (IsSafe(ObjectManager.Player.ServerPosition.To2D()))
            {
                return true;
            }

            //Skillshots with missile
            if (SpellData.Type == SkillShotType.SkillshotMissileLine)
            {
                var missilePositionAfterBlink = GetMissilePosition(delay + timeOffset);
                var myPositionProjection = ObjectManager.Player.ServerPosition.To2D().ProjectOn(Start, End);

                if (missilePositionAfterBlink.Distance(End) < myPositionProjection.SegmentPoint.Distance(End))
                {
                    return false;
                }

                return true;
            }

            //skillshots without missile
            var timeToExplode = SpellData.ExtraDuration + SpellData.Delay +
                                (int) (1000 * Start.Distance(End) / SpellData.MissileSpeed) -
                                (Environment.TickCount - StartTick);

            return timeToExplode > timeOffset + delay;
        }

        /// <summary>
        ///     Returns if the skillshot will hit the unit if the unit follows the path.
        /// </summary>
        public SafePathResult IsSafePath(List<Vector2> path, int timeOffset, int speed = -1, int delay = 0)
        {
            var distance = 0f;
            timeOffset += Game.Ping / 2;

            speed = (speed == -1) ? (int) ObjectManager.Player.MoveSpeed : speed;

            var allIntersections = new List<FoundIntersection>();
            for (var i = 0; i <= path.Count - 2; i++)
            {
                var from = path[i];
                var to = path[i + 1];
                var segmentIntersections = new List<FoundIntersection>();

                for (var j = 0; j <= Polygon.Points.Count - 1; j++)
                {
                    var sideStart = Polygon.Points[j];
                    var sideEnd = Polygon.Points[j == (Polygon.Points.Count - 1) ? 0 : j + 1];

                    var intersection = from.Intersection(to, sideStart, sideEnd);

                    if (intersection.Intersects)
                    {
                        segmentIntersections.Add(
                            new FoundIntersection(
                                distance + intersection.Point.Distance(from),
                                (int) ((distance + intersection.Point.Distance(from)) * 1000 / speed),
                                intersection.Point, from));
                    }
                }

                var sortedList = segmentIntersections.OrderBy(o => o.Distance).ToList();
                allIntersections.AddRange(sortedList);

                distance += from.Distance(to);
            }

            //Skillshot with missile.
            if (SpellData.Type == SkillShotType.SkillshotMissileLine ||
                SpellData.Type == SkillShotType.SkillshotMissileCone)
            {
                //Outside the skillshot
                if (IsSafe(ObjectManager.Player.ServerPosition.To2D()))
                {
                    //No intersections -> Safe
                    if (allIntersections.Count == 0)
                    {
                        return new SafePathResult(true, new FoundIntersection());
                    }

                    for (var i = 0; i <= allIntersections.Count - 1; i = i + 2)
                    {
                        var enterIntersection = allIntersections[i];
                        var enterIntersectionProjection =
                            enterIntersection.PointVector2.ProjectOn(Start, End).SegmentPoint;

                        //Intersection with no exit point.
                        if (i == allIntersections.Count - 1)
                        {
                            var missilePositionOnIntersection = GetMissilePosition(enterIntersection.Time - timeOffset);
                            return
                                new SafePathResult(
                                    (End.Distance(missilePositionOnIntersection) + 50 <=
                                     End.Distance(enterIntersectionProjection)) &&
                                    ObjectManager.Player.MoveSpeed < SpellData.MissileSpeed, allIntersections[0]);
                        }


                        var exitIntersection = allIntersections[i + 1];
                        var exitIntersectionProjection =
                            exitIntersection.PointVector2.ProjectOn(Start, End).SegmentPoint;

                        var missilePosOnEnter = GetMissilePosition(enterIntersection.Time - timeOffset);
                        var missilePosOnExit = GetMissilePosition(exitIntersection.Time + timeOffset);

                        //Missile didnt pass.
                        if (missilePosOnEnter.Distance(End) + 50 > enterIntersectionProjection.Distance(End))
                        {
                            if (missilePosOnExit.Distance(End) <= exitIntersectionProjection.Distance(End))
                            {
                                return new SafePathResult(false, allIntersections[0]);
                            }
                        }
                    }

                    return new SafePathResult(true, allIntersections[0]);
                }
                //Inside the skillshot.
                if (allIntersections.Count == 0)
                {
                    return new SafePathResult(false, new FoundIntersection());
                }

                if (allIntersections.Count > 0)
                {
                    //Check only for the exit point
                    var exitIntersection = allIntersections[0];
                    var exitIntersectionProjection = exitIntersection.PointVector2.ProjectOn(Start, End).SegmentPoint;

                    var missilePosOnExit = GetMissilePosition(exitIntersection.Time + timeOffset);
                    if (missilePosOnExit.Distance(End) <= exitIntersectionProjection.Distance(End))
                    {
                        return new SafePathResult(false, allIntersections[0]);
                    }
                }
            }


            if (IsSafe(ObjectManager.Player.ServerPosition.To2D()))
            {
                if (allIntersections.Count == 0)
                {
                    return new SafePathResult(true, new FoundIntersection());
                }

                if (SpellData.DontCross)
                {
                    return new SafePathResult(false, allIntersections[0]);
                }
            }
            else
            {
                if (allIntersections.Count == 0)
                {
                    return new SafePathResult(false, new FoundIntersection());
                }
            }

            var timeToExplode = (SpellData.DontAddExtraDuration ? 0 : SpellData.ExtraDuration) + SpellData.Delay +
                                (int) (1000 * Start.Distance(End) / SpellData.MissileSpeed) -
                                (Environment.TickCount - StartTick);


            var myPositionWhenExplodes = path.PositionAfter(timeToExplode, speed, delay);

            if (!IsSafe(myPositionWhenExplodes))
            {
                return new SafePathResult(false, allIntersections[0]);
            }

            var myPositionWhenExplodesWithOffset = path.PositionAfter(timeToExplode, speed, timeOffset);

            return new SafePathResult(IsSafe(myPositionWhenExplodesWithOffset), allIntersections[0]);
        }

        public bool IsSafe(Vector2 point)
        {
            return Polygon.IsOutside(point);
        }

        public bool IsDanger(Vector2 point)
        {
            return !IsSafe(point);
        }

        //Returns if the skillshot is about to hit the unit in the next time seconds.
        public bool IsAboutToHit(int time, Obj_AI_Base unit)
        {
            if (SpellData.Type == SkillShotType.SkillshotMissileLine)
            {
                var missilePos = GetMissilePosition(0);
                var missilePosAfterT = GetMissilePosition(time);
                var projection = unit.ServerPosition.To2D().ProjectOn(missilePos, missilePosAfterT);

                return projection.IsOnSegment &&
                       projection.SegmentPoint.Distance(unit.ServerPosition) < SpellData.Radius;
            }

            if (IsSafe(unit.ServerPosition.To2D()))
            {
                return false;
            }

            var timeToExplode = SpellData.ExtraDuration + SpellData.Delay +
                                (int) ((1000 * Start.Distance(End)) / SpellData.MissileSpeed) -
                                (Environment.TickCount - StartTick);

            if (Target != null)
            {
                timeToExplode = SpellData.ExtraDuration + SpellData.Delay +
                                (int) ((1000 * Start.Distance(Target)) / SpellData.MissileSpeed) -
                                (Environment.TickCount - StartTick);
            }
            return timeToExplode <= time;
        }
        public bool IsAboutToHit2(int time, Obj_AI_Base unit)
        {
            if (this.SpellData.Type != SkillShotType.SkillshotMissileLine)
            {
                return !this.IsSafe(unit.ServerPosition.To2D())
                       && this.SpellData.ExtraDuration + this.SpellData.Delay
                       + (int)(1000 * this.Start.Distance(this.End) / this.SpellData.MissileSpeed)
                       - (Environment.TickCount - this.StartTick) <= time;
            }
            var project = unit.ServerPosition.To2D()
                .ProjectOn(this.GetMissilePosition(0), this.GetMissilePosition(time));
            return project.IsOnSegment && unit.Distance(project.SegmentPoint) < this.SpellData.Radius;
        }
    }
}