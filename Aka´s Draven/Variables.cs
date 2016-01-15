
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

            /// <summary>
            ///     Initializes a new instance of the <see cref="QRecticle" /> class.
            /// </summary>
            /// <param name="rectice">The rectice.</param>
            /// <param name="expireTime">The expire time.</param>
            public QRecticle(GameObject rectice, int expireTime)
            {
                Object = rectice;
                ExpireTime = expireTime;
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
