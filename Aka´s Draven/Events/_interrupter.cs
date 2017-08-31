
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace AkaDraven.Events
{
    class _interrupter
    {
        public static void Interrupter(
            Obj_AI_Base sender,
            Interrupter.InterruptableSpellEventArgs e)
        {
            if (!MenuManager.MiscMenu["UseEInterrupt"].Cast<CheckBox>().CurrentValue || !Program.E.IsReady() ||
                !sender.IsValidTarget(Program.E.Range))
            {
                return;
            }

            if (e.DangerLevel == DangerLevel.Medium || e.DangerLevel == DangerLevel.High)
            {
                Program.E.Cast(sender);
            }
        }
    }
}
