using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;

namespace AkaCore.Features.Utility.Modules
{
    class AutoBuyStarters : IModule
    {
        public static bool bought = false;

        public static int ticks = 0;

        public void OnLoad()
        {

        }

        public ModuleType GetModuleType()
        {
            return ModuleType.OnUpdate;
        }

        public bool ShouldGetExecuted()
        {
            return Manager.MenuManager.Autobuy && ObjectManager.Player.Level == 1;
        }

        public void OnExecute()
        {
            if (bought || ticks / Game.TicksPerSecond < 3)
            {
                ticks++;
                return;
            }

            bought = true;
            if (Game.MapId == GameMapId.SummonersRift)
            {
                switch (Manager.MenuManager.AutobuySlider)
                {
                    case 0:
                        Shop.BuyItem(ItemId.Dorans_Blade);
                        Shop.BuyItem(ItemId.Health_Potion);
                        Shop.BuyItem(ItemId.Warding_Totem_Trinket);
                        break;
                    case 1:
                        Shop.BuyItem(ItemId.Dorans_Ring);
                        Shop.BuyItem(ItemId.Health_Potion);
                        Shop.BuyItem(ItemId.Health_Potion);
                        Shop.BuyItem(ItemId.Warding_Totem_Trinket);
                        break;
                    case 2:
                        Shop.BuyItem(ItemId.Dorans_Shield);
                        Shop.BuyItem(ItemId.Health_Potion);
                        Shop.BuyItem(ItemId.Warding_Totem_Trinket);
                        break;
                    case 3:
                        Shop.BuyItem(ItemId.Corrupting_Potion);
                        Shop.BuyItem(ItemId.Warding_Totem_Trinket);
                        break;
                    case 4:
                        Shop.BuyItem(ItemId.Hunters_Machete);
                        Shop.BuyItem(ItemId.Refillable_Potion);
                        Shop.BuyItem(ItemId.Warding_Totem_Trinket);
                        break;
                    case 5:
                        Shop.BuyItem(ItemId.Hunters_Talisman);
                        Shop.BuyItem(ItemId.Refillable_Potion);
                        Shop.BuyItem(ItemId.Warding_Totem_Trinket);
                        break;
                    case 6:
                        Shop.BuyItem(ItemId.Ancient_Coin);
                        Shop.BuyItem(ItemId.Health_Potion);
                        Shop.BuyItem(ItemId.Health_Potion);
                        Shop.BuyItem(ItemId.Health_Potion);
                        Shop.BuyItem(ItemId.Warding_Totem_Trinket);
                        break;
                    case 7:
                        Shop.BuyItem(ItemId.Spellthiefs_Edge);
                        Shop.BuyItem(ItemId.Health_Potion);
                        Shop.BuyItem(ItemId.Health_Potion);
                        Shop.BuyItem(ItemId.Health_Potion);
                        Shop.BuyItem(ItemId.Warding_Totem_Trinket);
                        break;
                    case 8:
                        Shop.BuyItem(ItemId.Relic_Shield);
                        Shop.BuyItem(ItemId.Health_Potion);
                        Shop.BuyItem(ItemId.Health_Potion);
                        Shop.BuyItem(ItemId.Health_Potion);
                        Shop.BuyItem(ItemId.Warding_Totem_Trinket);
                        break;
                }
            }
        }
    }
}
