using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK;
using EloBuddy;

namespace AkaCore.Features.Activator.AItems
{
    class HextechGLP : IModule
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
            return AkaCore.Manager.MenuManager.AItems && AkaCore.Manager.MenuManager.HextechGLP && AkaLib.Item.HextechGLP != null && AkaLib.Item.HextechGLP.IsReady() && Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Combo;
        }

        public void OnExecute()
        {
            var unit = TargetSelector.GetTarget(500, DamageType.Magical);

            if (unit != null)
            {
                AkaLib.Item.HextechGLP.Cast(unit.Position);
            }
        }
    }
}
