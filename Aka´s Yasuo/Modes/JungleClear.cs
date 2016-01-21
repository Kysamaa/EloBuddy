using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace AkaYasuo.Modes
{
    partial class JungleClear
    {
        public static void JungleClearmode()
        {
            foreach (Obj_AI_Base minion in EntityManager.MinionsAndMonsters.GetJungleMonsters(Variables._Player.ServerPosition, Program.Q.Range, true))
            {
                if (minion == null)
                {
                    return;
                }

                if (MenuManager.JungleClearMenu["Q"].Cast<CheckBox>().CurrentValue && Program.Q.IsReady() && minion.IsValidTarget())
                {
                    if (minion.Health <= DamageManager.QDamage(minion))
                    {
                        Program.Q.Cast(minion.ServerPosition);
                        Core.DelayAction(Orbwalker.ResetAutoAttack, 250);
                    }
                    else if (Program.Q.IsReady())
                    {
                        Program.Q.Cast(minion.ServerPosition);
                        Core.DelayAction(Orbwalker.ResetAutoAttack, 250);
                    }
                }
                if (MenuManager.JungleClearMenu["E"].Cast<CheckBox>().CurrentValue && Program.E.IsReady() && minion.IsValidTarget(Program.E.Range) && Variables.CanCastE(minion))
                {
                    if (minion.Health <= DamageManager.EDamage(minion))
                    {
                        Program.E.Cast(minion);
                    }
                    else
                    {
                        Program.E.Cast(minion);
                    }
                }
                if (MenuManager.JungleClearMenu["Items"].Cast<CheckBox>().CurrentValue)
                {
                    Items.UseItems(minion);
                }
            }
        }
    }
}
