
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace AkaYasuo.Modes
{
    class Items
    {
        public static void UseItems(Obj_AI_Base unit)
        {
            if (Item.HasItem((int)ItemId.Blade_of_the_Ruined_King, Variables._Player) &&
                Item.CanUseItem((int)ItemId.Blade_of_the_Ruined_King)
                && MenuManager.ItemMenu["Items"].Cast<CheckBox>().CurrentValue &&
                Variables.HealthPercent <= MenuManager.ComboMenu["myhp"].Cast<Slider>().CurrentValue)
            {
                Item.UseItem((int)ItemId.Blade_of_the_Ruined_King, unit);
            }
            if (Item.HasItem((int)ItemId.Bilgewater_Cutlass, Variables._Player) &&
                Item.CanUseItem((int)ItemId.Bilgewater_Cutlass)
                && unit.IsValidTarget())
            {
                Item.UseItem((int)ItemId.Bilgewater_Cutlass, unit);
            }
            if (Item.HasItem((int)ItemId.Youmuus_Ghostblade, Variables._Player) &&
                Item.CanUseItem((int)ItemId.Youmuus_Ghostblade)
                && Variables._Player.Distance(unit.Position) <= Variables._Player.GetAutoAttackRange())
            {
                Item.UseItem((int)ItemId.Youmuus_Ghostblade);
            }
            if (Item.HasItem((int)ItemId.Ravenous_Hydra_Melee_Only, Variables._Player) &&
                Item.CanUseItem((int)ItemId.Ravenous_Hydra_Melee_Only)
                && Variables._Player.Distance(unit.Position) <= 400)
            {
                Item.UseItem((int)ItemId.Ravenous_Hydra_Melee_Only);
            }
            if (Item.HasItem((int)ItemId.Tiamat_Melee_Only, Variables._Player) && Item.CanUseItem((int)ItemId.Tiamat_Melee_Only)
                && Variables._Player.Distance(unit.Position) <= 400)
            {
                Item.UseItem((int)ItemId.Tiamat_Melee_Only);
            }
            if (Item.HasItem((int)ItemId.Randuins_Omen, Variables._Player) && Item.CanUseItem((int)ItemId.Randuins_Omen)
                && Variables._Player.Distance(unit.Position) <= 400)
            {
                Item.UseItem((int)ItemId.Randuins_Omen);
            }
        }
    }
}
