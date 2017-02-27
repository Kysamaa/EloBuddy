using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK;
using EloBuddy;

namespace AkaCore.Features.Activator.DItems
{
    class Seraph : IModule
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
            return AkaCore.Manager.MenuManager.DItems && AkaCore.Manager.MenuManager.Seraphs && AkaLib.Item.Serpah != null && AkaLib.Item.Serpah.IsReady();
        }

        public void OnExecute()
        {
            if (ObjectManager.Player.CountEnemiesInRange(800) >= 1 && ObjectManager.Player.HealthPercent <= AkaCore.Manager.MenuManager.SeraphsHp)
            {
                AkaLib.Item.Serpah.Cast();
            }
            if (ObjectManager.Player.HasBuff("summonerdot") && ObjectManager.Player.HealthPercent <= AkaCore.Manager.MenuManager.SeraphsHp)
            {
                AkaLib.Item.Mountain.Cast();
            }
        }
    }
}