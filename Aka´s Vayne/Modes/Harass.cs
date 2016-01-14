
using AddonTemplate.Logic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace Aka_s_Vayne_reworked.Modes
{
    class Harass
    {
        public static void Load()
        {
            UseE();
            UseQ();
            UseC();
        }

        public static void UseQ()
        {
            if (Variables._Player.ManaPercent < MenuManager.HarassMenu["ManaHarass"].Cast<Slider>().CurrentValue)
                return;

            if (MenuManager.HarassMenu["UseQHarass"].Cast<CheckBox>().CurrentValue && Program.Q.IsReady())
            {
                Functions.Modes.Harass.SilverStackQ();
            }
        }

        public static void UseE()
        {
            if (Variables._Player.ManaPercent < MenuManager.HarassMenu["ManaHarass"].Cast<Slider>().CurrentValue)
                return;

            if (MenuManager.HarassMenu["UseEHarass"].Cast<CheckBox>().CurrentValue && Program.E.IsReady())
            {
                Functions.Modes.Harass.SilverStackE();
            }

        }

        public static void UseC()
        {
            if (MenuManager.HarassMenu["UseCHarass"].Cast<CheckBox>().CurrentValue && Program.E.IsReady() && Program.Q.IsReady())
            {
                Functions.Modes.Harass.SilverStackC();
            }
        }
    }
}
