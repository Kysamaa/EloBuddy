using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

namespace AkaYasuo.Modes
{
    partial class LaneClear
    {
        public static void LaneClearmode()
        {
            foreach (Obj_AI_Base minion in EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Variables._Player.Position, Program.Q.Range, true).OrderByDescending(m => m.Health))
            {
                if (minion == null)
                {
                    return;
                }

                if (!minion.IsDead && MenuManager.LaneClearMenu["Q"].Cast<CheckBox>().CurrentValue && Program.Q.IsReady() && minion.IsValidTarget() && !Variables.Q3READY(Variables._Player))
                {
                    if (minion.Health <= DamageManager.GetQDmg(minion))
                    {
                        Program.Q.Cast(minion.ServerPosition);
                        Core.DelayAction(Orbwalker.ResetAutoAttack, 250);
                    }
                    else if (!Variables.Q3READY(Variables._Player) && minion.Health + DamageManager.EDamage(minion) >= DamageManager.GetQDmg(minion) && Program.E.IsReady())
                    {
                        Program.Q.Cast(minion.ServerPosition);
                        Core.DelayAction(Orbwalker.ResetAutoAttack, 250);
                    }
                }
                if (!minion.IsDead && MenuManager.LaneClearMenu["Q3"].Cast<CheckBox>().CurrentValue && Program.Q.IsReady() && minion.IsValidTarget() && Variables.Q3READY(Variables._Player))
                {
                    if (minion.Health <= DamageManager.GetQDmg(minion))
                    {
                        Program.Q.Cast(minion.ServerPosition);
                        Core.DelayAction(Orbwalker.ResetAutoAttack, 250);
                    }
                    else if (Variables.Q3READY(Variables._Player) && minion.Health + DamageManager.EDamage(minion) >= DamageManager.GetQDmg(minion) && Program.E.IsReady())
                    {
                        Program.Q.Cast(minion.ServerPosition);
                        Core.DelayAction(Orbwalker.ResetAutoAttack, 250);
                    }
                }
            }
            var allMinionsE = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Variables._Player.Position, Program.E.Range, true);
            foreach (var minion in allMinionsE.Where(Variables.CanCastE))
            {
                if (MenuManager.LaneClearMenu["E"].Cast<CheckBox>().CurrentValue && Program.E.IsReady() && minion.IsValidTarget(Program.E.Range))
                {
                    if (!Variables.UnderTower((Vector3) Variables.PosAfterE(minion)))
                    {
                        if (minion.Health <= DamageManager.EDamage(minion))
                        {
                            Program.E.Cast(minion);
                        }
                    }
                }
                if (MenuManager.LaneClearMenu["Items"].Cast<CheckBox>().CurrentValue)
                {
                    Items.UseItems(minion);
                }
            }
        }
    }
}
