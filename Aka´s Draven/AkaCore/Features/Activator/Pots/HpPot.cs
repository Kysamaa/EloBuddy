using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK;
using EloBuddy;

namespace AkaCore.Features.Activator.Pots
{
    class HpPot : IModule
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
            return AkaCore.Manager.MenuManager.PItems && AkaCore.Manager.MenuManager.HPPot && AkaLib.Item.HpPot != null && AkaLib.Item.HpPot.IsReady();
        }

        public void OnExecute()
        {
            if (!ObjectManager.Player.IsInShopRange() && !(Player.HasBuff("RegenerationPotion")) && ObjectManager.Player.HealthPercent <= AkaCore.Manager.MenuManager.HPPotHp)
            {
                AkaLib.Item.HpPot.Cast();
            }
        }
    }
}
