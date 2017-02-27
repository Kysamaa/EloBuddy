using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;

namespace Aka_s_Draven.Features.Modules
{
    class AutoE : IModule
    {
        public void OnLoad()
        {

        }

        public ModuleType GetModuleType()
        {
            return ModuleType.OnUpdate;
        }

        public bool ShouldGetExecuted()
        {
            return Manager.SpellManager.E.IsReady() && Manager.MenuManager.AutoHarass;
        }

        public void OnExecute()
        {
            var target = TargetSelector.GetTarget(Manager.SpellManager.E.Range, DamageType.Physical);

            if (!target.IsValidTarget())
            {
                return;
            }

            var EPred = Manager.SpellManager.E.GetPrediction(target);
            if (EPred.HitChancePercent >= Manager.MenuManager.UseEHarassPred)
            {
                Manager.SpellManager.E.Cast(EPred.UnitPosition);

            }

        }
    }
}

