using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK;
using EloBuddy;

namespace Aka_s_Draven.Features.Modes
{
    class Harass
    {
        public static void Execute()
        {
            var target = TargetSelector.GetTarget(Manager.SpellManager.E.Range, DamageType.Physical);

            if (!target.IsValidTarget())
            {
                return;
            }

            if (Manager.MenuManager.UseEHarass && Manager.SpellManager.E.IsReady())
            {
                var EPred = Manager.SpellManager.E.GetPrediction(target);
                if (EPred.HitChancePercent >= Manager.MenuManager.UseEHarassPred)
                {
                    Manager.SpellManager.E.Cast(EPred.UnitPosition);

                }
            }
        }
    }
}
