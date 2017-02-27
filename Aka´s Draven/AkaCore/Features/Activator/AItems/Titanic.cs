using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK;
using EloBuddy;

namespace AkaCore.Features.Activator.AItems
{
    class Titanic : IModule
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
            return AkaCore.Manager.MenuManager.AItems && AkaCore.Manager.MenuManager.Titanic && AkaLib.Item.Titanic != null && AkaLib.Item.Titanic.IsReady() && Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Combo;
        }

        public void OnExecute()
        {
            var unit = TargetSelector.GetTarget(ObjectManager.Player.GetAutoAttackRange(), DamageType.Physical);

            if (unit != null)
            {
                AkaLib.Item.Titanic.Cast();
            }
        }
    }
}
