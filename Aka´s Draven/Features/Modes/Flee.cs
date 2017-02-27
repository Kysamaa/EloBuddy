using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;

namespace Aka_s_Draven.Features.Modes
{
    class Flee
    {
        public static void Execute()
        {
            var target = TargetSelector.GetTarget(Manager.SpellManager.E.Range, DamageType.Physical);

            if (target.IsValidTarget() && Manager.MenuManager.UseEFlee && Manager.SpellManager.E.IsReady())
            {
                var EPred = Manager.SpellManager.E.GetPrediction(target);
                if (EPred.HitChance >= EloBuddy.SDK.Enumerations.HitChance.Low)
                {
                    Manager.SpellManager.E.Cast(EPred.UnitPosition);

                }
            }

            if (Manager.MenuManager.UseWFlee && Manager.SpellManager.W.IsReady())
            {
                Manager.SpellManager.W.Cast();
            }
        }
    }
}
