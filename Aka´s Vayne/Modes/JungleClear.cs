using EloBuddy;
using Aka_s_Vayne_reworked.Logic;
using AddonTemplate.Logic;
using EloBuddy.SDK.Menu.Values;

namespace Aka_s_Vayne_reworked.Modes
{
    internal class JungleClear
    {
        public static void Load()
        {
            UseQ();
            UseE();
        }

        public static void UseQ()
        {
            if (MenuManager.JungleClearMenu["JCQ"].Cast<CheckBox>().CurrentValue && Program.Q.IsReady() &&
                !Game.CursorPos.IsSafe())
            {
                //QLogic.JungleClear();
            }
        }

        public static void UseE()
        {
            if (MenuManager.JungleClearMenu["JCE"].Cast<CheckBox>().CurrentValue && Program.E.IsReady())
            {
                Jungle.JungleCondemn();
            }
        }
    }
}
