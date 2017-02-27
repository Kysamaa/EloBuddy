using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK;
using EloBuddy;

namespace AkaCore.Features.Activator.AItems
{
    class Bilge : IModule
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
            return AkaCore.Manager.MenuManager.AItems && AkaCore.Manager.MenuManager.Botrk && AkaLib.Item.Bilge != null && AkaLib.Item.Bilge.IsReady() && Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Combo;
        }

        public void OnExecute()
        {
            var unit = TargetSelector.GetTarget(550, DamageType.Magical);

            if (unit != null && (ObjectManager.Player.Health / ObjectManager.Player.MaxHealth) * 100 <= 95)
            {
                AkaLib.Item.Bilge.Cast(unit);
            }
        }
    }
}
