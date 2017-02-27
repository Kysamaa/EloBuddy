using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;

namespace AkaCore.Features.Utility.Modules
{
    class AutoBuyTrinkets : IModule
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
            return Manager.MenuManager.Autobuytrinkets && ObjectManager.Player.Level >= 9 && ObjectManager.Player.IsInShopRange();
        }

        public void OnExecute()
        {
            if (Game.MapId == GameMapId.SummonersRift)
            {
                switch (Manager.MenuManager.AutobuytrinketsSlider)
                {
                    case 0:
                        Shop.BuyItem(ItemId.Farsight_Alteration);
                        break;
                    case 1:
                        Shop.BuyItem(ItemId.Oracle_Alteration);
                        break;
                }
            }
        }
    }
}
