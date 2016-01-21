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
    partial class LastHit
    {
        public static void LastHitmode()
        {
            foreach (Obj_AI_Base minion in EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Variables._Player.ServerPosition, Program.Q.Range, true).OrderByDescending(m => m.Health))
            {
                if (minion == null)
                {
                    return;
                }

                if (!minion.IsDead && MenuManager.LastHitMenu["Q"].Cast<CheckBox>().CurrentValue && Program.Q.IsReady() && minion.IsValidTarget() && !Variables.Q3READY(Variables._Player))
                {
                    if (minion.Health <= DamageManager.QDamage(minion))
                    {
                        Program.Q.Cast(minion.ServerPosition);
                    }
                }
                if (!minion.IsDead && MenuManager.LastHitMenu["Q3"].Cast<CheckBox>().CurrentValue && Program.Q.IsReady() && minion.IsValidTarget() && Variables.Q3READY(Variables._Player))
                {
                    if (minion.Health <= DamageManager.QDamage(minion))
                    {
                        Program.Q.Cast(minion.ServerPosition);
                    }
                }
                if (MenuManager.LastHitMenu["E"].Cast<CheckBox>().CurrentValue && Program.E.IsReady() && minion.IsValidTarget())
                {
                    if (!Variables.UnderTower((Vector3) Variables.PosAfterE(minion)))
                    {
                        if (minion.Health <= DamageManager.EDamage(minion))
                        {
                            Program.E.Cast(minion);
                        }
                    }
                }
            }
        }
    }
}
