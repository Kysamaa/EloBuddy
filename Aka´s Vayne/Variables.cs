
using System.Collections.Generic;
using EloBuddy;
using SharpDX;
using EloBuddy.SDK;
using System;
using System.Media;

namespace Aka_s_Vayne_reworked
{
    class Variables
    {
        public static int currentSkin = 0;

        public static bool bought = false;

        public static int ticks = 0;

        public static bool VayneUltiIsActive { get; set; }

        public static SpellSlot FlashSlot;

        public static float lastaa, lastaaclick;

        public static bool stopmove;

        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }

        public static string[] DangerSliderValues = { "Low", "Medium", "High" };

        public static int[] AbilitySequence;

        public static int QOff = 0, WOff = 0, EOff = 0, ROff = 0;

        public static List<Vector2> Points = new List<Vector2>();

        public static IEnumerable<AIHeroClient> EnemiesClose
        {
            get
            {
                return
                    EntityManager.Heroes.Enemies.FindAll(
                        m =>
                            m.Distance(ObjectManager.Player, true) <= Math.Pow(1000, 2) && m.IsValidTarget(1500, false) &&
                            m.CountEnemiesInRange(m.IsMelee ? m.AttackRange * 1.5f : m.AttackRange + 20 * 1.5f) > 0);
            }
        }

        public static IEnumerable<AIHeroClient> MeleeEnemiesTowardsMe
        {
            get
            {
                return
                    EntityManager.Heroes.Enemies.FindAll(
                        m => m.IsMelee && m.Distance(ObjectManager.Player) <= _Player.GetAutoAttackRange(m)
                            && (m.ServerPosition.To2D() + (m.BoundingRadius + 25f) * m.Direction.To2D().Perpendicular()).Distance(ObjectManager.Player.ServerPosition.To2D()) <= m.ServerPosition.Distance(ObjectManager.Player.ServerPosition)
                            && m.IsValidTarget(1200, false));
            }
        }

        public static SoundPlayer Welcomemsg;
    }
}
