
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace Aka_s_Vayne_reworked.Logic
{
    class AutoE
    {
        public static void OnExecute()
        {
            if (MenuManager.CondemnMenu["UseEauto"].Cast<CheckBox>().CurrentValue || !Program.E.IsReady())
            {
                return;
            }

            var CondemnTarget = NewELogic.GetCondemnTarget(ObjectManager.Player.ServerPosition);
            if (CondemnTarget.IsValidTarget())
            {
                Program.E.Cast(CondemnTarget);
            }
        }
    }
}
