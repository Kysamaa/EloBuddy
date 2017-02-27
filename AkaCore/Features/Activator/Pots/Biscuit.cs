using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK;
using EloBuddy;

namespace AkaCore.Features.Activator.Pots
{
    class Biscuit : IModule
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
            return AkaCore.Manager.MenuManager.PItems && AkaCore.Manager.MenuManager.Biscuit && AkaLib.Item.Biscuit != null && AkaLib.Item.Biscuit.IsReady();
        }

        public void OnExecute()
        {
            if (!ObjectManager.Player.IsInShopRange() && !(Player.HasBuff("ItemMiniRegenPotion")) && ObjectManager.Player.HealthPercent <= AkaCore.Manager.MenuManager.BiscuitHp)
            {
                AkaLib.Item.Biscuit.Cast();
            }
        }
    }
}