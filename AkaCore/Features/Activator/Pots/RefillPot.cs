using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK;
using EloBuddy;

namespace AkaCore.Features.Activator.Pots
{
    class RefillPot : IModule
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
            return AkaCore.Manager.MenuManager.PItems && AkaCore.Manager.MenuManager.RefillPot && AkaLib.Item.RefillPot != null && AkaLib.Item.RefillPot.IsReady();
        }

        public void OnExecute()
        {
            if (!ObjectManager.Player.IsInShopRange() && !(Player.HasBuff("ItemCrystalFlask")) && ObjectManager.Player.HealthPercent <= AkaCore.Manager.MenuManager.RefillPotHp)
            {
                AkaLib.Item.RefillPot.Cast();
            }
        }
    }
}