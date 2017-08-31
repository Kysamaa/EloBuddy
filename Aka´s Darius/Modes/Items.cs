
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using Settings = AddonTemplate.Config.Modes.Items;

namespace AddonTemplate.Modes
{
    class Items
    {
        public static void UseItems(Obj_AI_Base unit)
        {
            if (Item.HasItem((int)ItemId.Blade_of_the_Ruined_King, ObjectManager.Player) &&
                Item.CanUseItem((int)ItemId.Blade_of_the_Ruined_King)
                && Settings.items &&
                ObjectManager.Player.HealthPercent <= Settings.myHp)
            {
                Item.UseItem((int)ItemId.Blade_of_the_Ruined_King, unit);
            }
            if (Item.HasItem((int)ItemId.Bilgewater_Cutlass, ObjectManager.Player) &&
                Item.CanUseItem((int)ItemId.Bilgewater_Cutlass)
                && unit.IsValidTarget())
            {
                Item.UseItem((int)ItemId.Bilgewater_Cutlass, unit);
            }
            if (Item.HasItem((int)ItemId.Youmuus_Ghostblade, ObjectManager.Player) &&
                Item.CanUseItem((int)ItemId.Youmuus_Ghostblade)
                && ObjectManager.Player.Distance(unit.Position) <= ObjectManager.Player.GetAutoAttackRange())
            {
                Item.UseItem((int)ItemId.Youmuus_Ghostblade);
            }
            if (Item.HasItem((int)ItemId.Ravenous_Hydra_Melee_Only, ObjectManager.Player) &&
                Item.CanUseItem((int)ItemId.Ravenous_Hydra_Melee_Only)
                && ObjectManager.Player.Distance(unit.Position) <= 400)
            {
                Item.UseItem((int)ItemId.Ravenous_Hydra_Melee_Only);
            }
            if (Item.HasItem((int)ItemId.Tiamat_Melee_Only, ObjectManager.Player) && Item.CanUseItem((int)ItemId.Tiamat_Melee_Only)
                && ObjectManager.Player.Distance(unit.Position) <= 400)
            {
                Item.UseItem((int)ItemId.Tiamat_Melee_Only);
            }
            if (Item.HasItem((int)ItemId.Randuins_Omen, ObjectManager.Player) && Item.CanUseItem((int)ItemId.Randuins_Omen)
                && ObjectManager.Player.Distance(unit.Position) <= 400)
            {
                Item.UseItem((int)ItemId.Randuins_Omen);
            }
        }
    }
}
