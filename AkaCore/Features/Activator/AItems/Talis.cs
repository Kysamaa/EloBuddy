using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK;
using EloBuddy;

namespace AkaCore.Features.Activator.AItems
{
    class Talis : IModule
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
            return AkaCore.Manager.MenuManager.AItems && AkaCore.Manager.MenuManager.Talis && AkaLib.Item.Talis != null && AkaLib.Item.Talis.IsReady() && Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Combo;
        }

        public void OnExecute()
        {
            var unit = TargetSelector.GetTarget(2000, DamageType.Magical);

            if (unit != null && ObjectManager.Player.Position.Distance(unit.Position) >= AkaCore.Manager.MenuManager.TalisDistance)
            {
                AkaLib.Item.Talis.Cast();
            }
        }
    }
}
