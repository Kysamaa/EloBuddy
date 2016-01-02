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

                    var predHealth = Prediction.Health.GetPrediction(minion, (int)(Variables._Player.Distance(minion.Position) * 1000 / 2000));
                    if (predHealth <= Variables._Player.GetSpellDamage(minion, SpellSlot.Q))
                    {
                        Program.Q.Cast(minion.ServerPosition);
                    }
                    else if (Program.Q.IsReady())
                    {
                        Program.Q.Cast(minion.ServerPosition);
                    }
                }
                if (MenuManager.JungleClearMenu["E"].Cast<CheckBox>().CurrentValue && Program.E.IsReady() && minion.IsValidTarget(Program.E.Range) && Variables.CanCastE(minion))
                {
                    var predHealth = Prediction.Health.GetPrediction(minion, (int)(Variables._Player.Distance(minion.Position) * 1000 / 2000));
                    if (predHealth <= Variables._Player.GetSpellDamage(minion, SpellSlot.E))
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
