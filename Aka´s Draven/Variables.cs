
using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

namespace AkaDraven
{
    internal class Variables
    {
        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }

        public static int[] abilitySequence;

        public static int QOff = 0, WOff = 0, EOff = 0, ROff = 0;

        public static int QCount
        {
            get
            {
                return (_Player.HasBuff("dravenspinning") ? 1 : 0)
                       + (_Player.HasBuff("dravenspinningleft") ? 1 : 0) + QReticles.Count;
            }
        }

        public static List<QRecticle> QReticles { get; set; }

        public int LastAxeMoveTime { get; set; }

        internal class QRecticle
        {
            #region Constructors and Destructors

            public static float LimitTime = 1.20f;

            public float StartTime;

            public bool MoveSent = false;

            private float TimeToMakeAutoAttack
            {
                get { return _Player.AttackCastDelay; }
            }

            /// <summary>
            ///     Initializes a new instance of the <see cref="QRecticle" /> class.
            /// </summary>
            /// <param name="rectice">The rectice.</param>
            /// <param name="expireTime">The expire time.</param>
            public QRecticle(GameObject rectice, int expireTime)
            {
                Object = rectice;
                ExpireTime = expireTime;
                StartTime = Game.Time;
            }

            #endregion

            #region Public Properties

            /// <summary>
            ///     Gets or sets the expire time.
            /// </summary>
            /// <value>
            ///     The expire time.
            /// </value>
            public int ExpireTime { get; set; }

            /// <summary>
            ///     Gets or sets the object.
            /// </summary>
            /// <value>
            ///     The object.
            /// </value>
            public GameObject Object { get; set; }


            public bool CanAttack
            {
                get { return TimeToCatchReticle - TimeToMakeAutoAttack > 0; }
            }

            public bool CanOrbwalkWithUserDelay
            {
                get { return TimeToCatchReticle - 1 * LimitTime - OffsetTimeNeededToCatchReticle > 0; }
            }

            public bool InTime
            {
                get { return Game.Time - this.StartTime <= LimitTime + 0.2f && Object.IsValid; }
            }

            public float TimeToCatchReticle
            {
                get
                {
                    var TimeLefts = 0f;
                    foreach (QRecticle a1 in QReticles.Where(m => m.InTime && m.TimeLeft < TimeLeft))
                    {
                        TimeLefts += a1.TimeLeft;
                    }
                    return TimeLeft - (TimeLefts + TimeNeededToCatchReticle);
                }
            }

            public float OffsetTimeNeededToCatchReticle
            {
                get
                {
                    return 0f;
                    //return Offset / Util.myHero.MoveSpeed;
                }
            }

            public float TimeLeft
            {
                get
                {
                    if (Object.IsValid)
                        return LimitTime - (Game.Time - this.StartTime);
                    //2 * Extensions.Distance(this.Reticle.Position, this.Missile.Position) / this.Speed; //
                    return float.MaxValue;
                }
            }

            public float TimeNeededToCatchReticle
            {
                get
                {
                    return Extensions.Distance(StartPosition, EndPosition) / _Player.MoveSpeed;
                }
            }

            public static QRecticle AxeBefore(QRecticle a)
            {
                return QReticles.Where(m => m.InTime && m.TimeLeft < a.TimeLeft).OrderBy(m => m.TimeLeft).LastOrDefault();
            }

            public static bool IsFirst(QRecticle a)
            {
                return FirstAxe == a;
            }

            public static QRecticle FirstAxe
            {
                get
                {
                    return QReticles.Where(m => m.InTime).OrderBy(m => m.TimeLeft).FirstOrDefault();
                }
            }

            public Vector3 StartPosition
            {
                get
                {
                    foreach (QRecticle a2 in QReticles.Where(x => x.Object.IsValid))
                    if (!IsFirst(a2) && AxeBefore(a2) != null)
                    {
                        return AxeBefore(a2).EndPosition;
                    }
                    return _Player.Position;
                }
            }
            public Vector3 EndPosition
            {
                get
                {
                    if (Object.IsValid)
                    {
                        return Object.Position;
                    }
                    return Vector3.Zero;
                }
            }

            /// <summary>
            ///     Gets the position.
            /// </summary>
            /// <value>
            ///     The position.
            /// </value>
            public Vector3 Position
            {
                get { return Object.Position; }
            }

            #endregion
        }
    }
}
