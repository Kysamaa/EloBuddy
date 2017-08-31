using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace Aka_s_Vayne_reworked.Functions.Modes
{
    internal class Combo
    {
        public static bool AfterAttack
        {
            get
            {
                if (Game.Time * 1000 <
                    Variables.lastaa + Variables._Player.AttackDelay * 1000 - Variables._Player.AttackDelay * 1000 / 1.5 &&
                    Game.Time * 1000 > Variables.lastaa + Variables._Player.AttackCastDelay * 1000)
                {
                    return true;
                }
                return false;
            }
        }

        public static bool BeforeAttack
        {
            get
            {
                if (Game.Time * 1000 >
                    Variables.lastaa + Variables._Player.AttackDelay * 1000 - Variables._Player.AttackDelay * 1000 / 2 &&
                    Game.Time * 1000 <
                    Variables.lastaa + Variables._Player.AttackDelay * 1000 - Variables._Player.AttackDelay * 1000 / 4)
                {
                    return true;
                }
                return false;
            }
        }


        public static void ComboUltimateLogic()
        {
            if (Variables._Player.CountEnemiesInRange(1000) >= MenuManager.ComboMenu["comboRSlider"].Cast<Slider>().CurrentValue)
            {
                Program.R.Cast();
            }
        }
    }
}

