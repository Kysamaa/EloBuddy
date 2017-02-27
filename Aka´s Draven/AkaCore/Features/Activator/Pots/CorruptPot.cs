using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK;
using EloBuddy;

namespace AkaCore.Features.Activator.Pots
{
    class CorruptPot : IModule
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
            return AkaCore.Manager.MenuManager.PItems && AkaCore.Manager.MenuManager.CorruptPot && AkaLib.Item.CorruptPot != null && AkaLib.Item.CorruptPot.IsReady();
        }

        public void OnExecute()
        {
            if (!ObjectManager.Player.IsInShopRange() && !(Player.HasBuff("ItemDarkCrystalFlask")) && ObjectManager.Player.HealthPercent <= AkaCore.Manager.MenuManager.CorruptPotHp)
            {
                AkaLib.Item.CorruptPot.Cast();
            }
        }
    }
}