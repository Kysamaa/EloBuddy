using System.Diagnostics;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using Settings = AddonTemplate.Config.Modes.MiscMenu;

namespace AddonTemplate.Modes
{
    public sealed class PermaActive : ModeBase
    {
        int currentSkin = 0;
        bool bought = false;
        int ticks = 0;

        public override bool ShouldBeExecuted()
        {
            // Since this is permaactive mode, always execute the loop
            return true;
        }

        public override void Execute()
        {
            skinChanger();
            autoBuy();


        }

        private void skinChanger()
        {
            if (Settings.skinId != currentSkin)
            {
                Player.Instance.SetSkinId(Settings.skinId);
                currentSkin = Settings.skinId;
            }
        }

        private void autoBuy()
        {

            if (bought || ticks / Game.TicksPerSecond < 3)
            {
                ticks++;
                return;
            }

            bought = true;
            if (Settings.autoBuy)
            {
                if (Game.MapId == GameMapId.SummonersRift)
                {
                    Shop.BuyItem(ItemId.Dorans_Blade);
                    Shop.BuyItem(ItemId.Health_Potion);
                    Shop.BuyItem(ItemId.Warding_Totem_Trinket);
                }
            }
        }
    }
}


