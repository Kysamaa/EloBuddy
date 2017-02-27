using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK;
using EloBuddy;

namespace AkaCore.Features.Activator.Pots
{
    class HunterPot : IModule
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
            return AkaCore.Manager.MenuManager.PItems && AkaCore.Manager.MenuManager.HunterPot && AkaLib.Item.HunterPot != null && AkaLib.Item.HunterPot.IsReady();
        }

        public void OnExecute()
        {
            if (!ObjectManager.Player.IsInShopRange() && !(Player.HasBuff("ItemCrystalFlaskJungle")) && ObjectManager.Player.HealthPercent <= AkaCore.Manager.MenuManager.HunterPotHp)
            {
                AkaLib.Item.HunterPot.Cast();
            }
        }
    }
}