
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace AkaDraven.Modes
{
    class Flee
    {
        public static void Execute()
        {
            var target = TargetSelector.GetTarget(Program.E.Range, DamageType.Physical);

            if (!target.IsValidTarget())
            {
                return;
            }

            if (MenuManager.FleeMenu["E"].Cast<CheckBox>().CurrentValue && Program.E.IsReady())
            {
                Program.E.Cast(target);
            }

            if (MenuManager.FleeMenu["W"].Cast<CheckBox>().CurrentValue && Program.W.IsReady())
            {
                Program.W.Cast();
            }
        }

    }
}
