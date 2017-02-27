using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK;
using EloBuddy;

namespace AkaCore.Features.Activator.Summoners
{
    class Exhaust : IModule
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
            return AkaCore.Manager.MenuManager.Exhaust && AkaLib.Item.Exhaust != null && AkaLib.Item.Exhaust.IsReady();
        }

        public void OnExecute()
        {
            var unit = TargetSelector.GetTarget(AkaLib.Item.Exhaust.Range, DamageType.Physical);

            if (unit != null && unit.HealthPercent <= AkaCore.Manager.MenuManager.ExhaustHp)
            {
                AkaLib.Item.Exhaust.Cast(unit);
            }
        }
    }
}
