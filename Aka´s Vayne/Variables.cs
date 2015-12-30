
using System.Collections.Generic;
using EloBuddy;
using SharpDX;

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
    }
}
