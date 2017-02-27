using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK;
using EloBuddy;

namespace AkaCore.Features.Activator.Summoners
{
    class Barrier : IModule
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
            return AkaCore.Manager.MenuManager.Barrier && AkaLib.Item.Barrier != null && AkaLib.Item.Barrier.IsReady();
        }

        public void OnExecute()
        {
            if (ObjectManager.Player.CountEnemiesInRange(800) >= 1 && ObjectManager.Player.HealthPercent <= AkaCore.Manager.MenuManager.BarrierHp)
            {
                AkaLib.Item.Barrier.Cast();
            }
            if (ObjectManager.Player.HasBuff("summonerdot") && ObjectManager.Player.HealthPercent <= AkaCore.Manager.MenuManager.BarrierHp)
            {
                AkaLib.Item.Barrier.Cast();
            }
        }
    }
}
