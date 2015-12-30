
using System;
using AddonTemplate.Logic;
using EloBuddy;
using EloBuddy.SDK.Menu.Values;

namespace Aka_s_Vayne_reworked.Events
{
    class _orbwalker
    {
        public static void Condemnexecute(AttackableUnit target, EventArgs args)
        {
            if (MenuManager.CondemnMenu["condemnmethod1"].Cast<CheckBox>().CurrentValue && Program.E.IsReady())
            {
                ELogic.Condemn1();
            }
            if (MenuManager.CondemnMenu["condemnmethod2"].Cast<CheckBox>().CurrentValue && Program.E.IsReady())
            {
                ELogic.Condemn2();
            }
            if (MenuManager.CondemnMenu["condemnmethod3"].Cast<CheckBox>().CurrentValue && Program.E.IsReady())
            {
                ELogic.Condemn3();
            }
        }
    }
}
