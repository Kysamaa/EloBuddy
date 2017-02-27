using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK;
using EloBuddy;

namespace AkaCore.Features.Activator.Summoners
{
    class FaceMe : IModule
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
            return AkaCore.Manager.MenuManager.DItems && AkaCore.Manager.MenuManager.MountainMe && AkaLib.Item.Mountain != null && AkaLib.Item.Mountain.IsReady();
        }

        public void OnExecute()
        {
            if (ObjectManager.Player.CountEnemiesInRange(800) >= 1 && ObjectManager.Player.HealthPercent <= AkaCore.Manager.MenuManager.MountainMeHp)
            {
                AkaLib.Item.Mountain.Cast(ObjectManager.Player);
            }
            if (ObjectManager.Player.HasBuff("summonerdot") && ObjectManager.Player.HealthPercent <= AkaCore.Manager.MenuManager.MountainMeHp)
            {
                AkaLib.Item.Mountain.Cast(ObjectManager.Player);
            }
        }
    }
}
