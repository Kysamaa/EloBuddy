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
                    var predHealth = Prediction.Health.GetPrediction(minion, (int)(Variables._Player.Distance(minion.Position) * 1000 / 2000));
                    if (predHealth <= Variables._Player.GetSpellDamage(minion, SpellSlot.Q))
                    {
                        Program.Q.Cast(minion.ServerPosition);
                    }
                    else if (!Variables.Q3READY(Variables._Player))
                    {
                        Program.Q.Cast(minion.ServerPosition);
                    }
                }
                if (!minion.IsDead && MenuManager.LaneClearMenu["Q3"].Cast<CheckBox>().CurrentValue && Program.Q.IsReady() && minion.IsValidTarget() && Variables.Q3READY(Variables._Player))
                {
                    var predHealth = Prediction.Health.GetPrediction(minion, (int)(Variables._Player.Distance(minion.Position) * 1000 / 2000));
                    if (predHealth <= Variables._Player.GetSpellDamage(minion, SpellSlot.Q))
                    {
                        Program.Q.Cast(minion.ServerPosition);
                    }
                    else if (Variables.Q3READY(Variables._Player))
                    {
                        Program.Q.Cast(minion.ServerPosition);
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
                        var predHealth = Prediction.Health.GetPrediction(minion, (int)(Variables._Player.Distance(minion.Position) * 1000 / 2000));
                        if (predHealth <= Variables._Player.GetSpellDamage(minion, SpellSlot.E))
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
