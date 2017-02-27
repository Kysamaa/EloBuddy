using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK;
using EloBuddy;

namespace AkaCore.Features.Activator.DItems
{
    class Omen : IModule
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
            return AkaCore.Manager.MenuManager.DItems && AkaCore.Manager.MenuManager.Omen && AkaLib.Item.Omen != null && AkaLib.Item.Omen.IsReady();
        }

        public void OnExecute()
        {
            if (ObjectManager.Player.CountEnemiesInRange(800) >= 1 && ObjectManager.Player.HealthPercent <= AkaCore.Manager.MenuManager.OmenHp)
            {
                AkaLib.Item.Omen.Cast();
            }
        }
    }
}
