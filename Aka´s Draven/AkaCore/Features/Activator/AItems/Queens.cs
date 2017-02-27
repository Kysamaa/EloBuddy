using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK;
using EloBuddy;

namespace AkaCore.Features.Activator.AItems
{
    class Queens : IModule
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
            return AkaCore.Manager.MenuManager.AItems && AkaCore.Manager.MenuManager.Queens && AkaLib.Item.Queens != null && AkaLib.Item.Queens.IsReady() && Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Combo;
        }

        public void OnExecute()
        {
            var unit = TargetSelector.GetTarget(2500, DamageType.Magical);

            if (unit != null && ObjectManager.Player.Position.Distance(unit.Position) >= AkaCore.Manager.MenuManager.QueensDistance)
            {
                AkaLib.Item.Queens.Cast();
            }
        }
    }
}
