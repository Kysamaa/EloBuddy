using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK;
using EloBuddy;

namespace AkaCore.Features.Activator.DItems
{
    class Zhonyas : IModule
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
            return AkaCore.Manager.MenuManager.DItems && AkaCore.Manager.MenuManager.Zhonyas && AkaLib.Item.Zhonyas != null && AkaLib.Item.Zhonyas.IsReady();
        }

        public void OnExecute()
        {
            if (ObjectManager.Player.CountEnemiesInRange(800) >= 1 && ObjectManager.Player.HealthPercent <= AkaCore.Manager.MenuManager.ZhonyasHp)
            {
                AkaLib.Item.Zhonyas.Cast();
            }
            if (ObjectManager.Player.HasBuff("summonerdot") && ObjectManager.Player.HealthPercent <= AkaCore.Manager.MenuManager.ZhonyasHp)
            {
                AkaLib.Item.Zhonyas.Cast();
            }
        }
    }
}