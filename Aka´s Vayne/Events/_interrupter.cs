
using EloBuddy;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK;

namespace Aka_s_Vayne_reworked.Events
{
    internal class _interrupter
    {
        public static void Interrupt(Obj_AI_Base sender,
            Interrupter.InterruptableSpellEventArgs e)
        {
            if (!MenuManager.MiscMenu["InterruptE"].Cast<CheckBox>().CurrentValue) return;
            var dangerLevel =
                new[]
                {
                    DangerLevel.Low, DangerLevel.Medium,
                    DangerLevel.High,
                }[MenuManager.MiscMenu["dangerLevel"].Cast<Slider>().CurrentValue - 1];

            if (dangerLevel == DangerLevel.Medium && e.DangerLevel.HasFlag(DangerLevel.High) ||
                dangerLevel == DangerLevel.Low && e.DangerLevel.HasFlag(DangerLevel.High) &&
                e.DangerLevel.HasFlag(DangerLevel.Medium))
                return;

            if (Extensions.IsValidTarget(e.Sender))
            {
                Program.E.Cast(e.Sender);
            }
        }
    }
}
