
using AddonTemplate.Logic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace Aka_s_Vayne_reworked.Modes
{
    internal class Flee
    {

        public static void Load()
        {
            UseE();
            UseQ();
        }

        public static void UseE()
        {
            if (MenuManager.FleeMenu["FleeUseE"].Cast<CheckBox>().CurrentValue)
            {
                var target = TargetSelector.GetTarget(Program.E.Range, DamageType.Physical);
                if (Program.E.IsReady() && target != null)
                {
                    Program.E.Cast(target);
                }
            }
        }

        public static void UseQ()
        {
            if (MenuManager.FleeMenu["FleeUseQ"].Cast<CheckBox>().CurrentValue)
            {
                if (Program.Q.IsReady() && !(Game.CursorPos.IsSafe()))
                {
                    Player.CastSpell(SpellSlot.Q, Game.CursorPos);
                }
            }

        }
    }
}
