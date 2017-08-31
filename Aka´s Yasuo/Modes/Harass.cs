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
    partial class Harass
    {
        public static void Harassmode()
        {
            var bestMinion =
                ObjectManager.Get<Obj_AI_Minion>()
                    .Where(x => x.IsValidTarget(Program.E.Range))
                    .Where(x => x.Distance(Game.CursorPos) < Variables._Player.Distance(Game.CursorPos))
                    .OrderByDescending(x => x.Distance(Variables._Player))
                    .FirstOrDefault();

            if (bestMinion != null && Variables._Player.IsFacing(bestMinion) && Variables.CanCastE(bestMinion) &&
                (Program.E.IsReady() && MenuManager.HarassMenu["E"].Cast<CheckBox>().CurrentValue))
            {
                Program.E.Cast(bestMinion);
            }
            var TsTarget = TargetSelector.GetTarget(1300, DamageType.Physical);

            if (TsTarget == null)
            {
                return;
            }
            if (Program.Q.IsReady() && MenuManager.HarassMenu["Q3"].Cast<CheckBox>().CurrentValue)
            {
                PredictionResult QPred = Program.Q.GetPrediction(TsTarget);
                if (!Variables.isDashing && QPred.HitChance >= EloBuddy.SDK.Enumerations.HitChance.Medium)
                {
                    Program.Q.Cast(QPred.CastPosition);
                    Core.DelayAction(Orbwalker.ResetAutoAttack, 250);
                }
                else if (Variables.Q3READY(Variables._Player) && Variables.isDashing &&
                         Variables._Player.Distance(TsTarget) <= 250*250)
                {
                    Program.Q.Cast(QPred.CastPosition);
                    Core.DelayAction(Orbwalker.ResetAutoAttack, 250);
                }
            }
            PredictionResult QPred2 = Program.Q.GetPrediction(TsTarget);
            if (!Variables.Q3READY(Variables._Player) && QPred2.HitChance >= EloBuddy.SDK.Enumerations.HitChance.Medium)
            {
                Program.Q.Cast(QPred2.CastPosition);
                Core.DelayAction(Orbwalker.ResetAutoAttack, 250);
            }
        }
    }
}

