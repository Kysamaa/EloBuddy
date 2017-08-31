
using EloBuddy;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK;

namespace AkaYasuo.Events
{
    class _interrupter
    {
        public static void Interrupt(Obj_AI_Base sender,
            Interrupter.InterruptableSpellEventArgs e)
        {
            if (e != null && Program.Q3.IsReady() && Variables.Q3READY(Variables._Player) && sender.IsValidTarget(Program.Q3.Range) && MenuManager.MiscMenu["InterruptQ"].Cast<CheckBox>().CurrentValue)
            {
                Program.Q3.Cast(sender);
            }
        }
    }
}
