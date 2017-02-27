using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;

namespace AkaCore.AkaLib
{
    class Item
    {
        public static EloBuddy.SDK.Item Totem, Botrk, Bilge, Hydra, Titanic, Tiamat, Queens, Hextech, HextechGLP, HextechPro, You, Glory, Talis, Rot, Mountain, Omen, Serpah, Solari, Zhonyas, Qss, Mercurial, HpPot, Biscuit, CorruptPot, HunterPot, RefillPot;

        public static Spell.Active Heal, Cleanse, Barrier;
        public static Spell.Targeted Ignite, Exhaust, Smite;

        public static void Load()
        {
            Totem = new EloBuddy.SDK.Item((int)ItemId.Warding_Totem_Trinket);
            Botrk = new EloBuddy.SDK.Item((int)ItemId.Blade_of_the_Ruined_King);
            Bilge = new EloBuddy.SDK.Item((int)ItemId.Bilgewater_Cutlass);
            Hydra = new EloBuddy.SDK.Item((int)ItemId.Ravenous_Hydra_Melee_Only);
            Titanic = new EloBuddy.SDK.Item((int)ItemId.Titanic_Hydra);
            Tiamat = new EloBuddy.SDK.Item((int)ItemId.Tiamat_Melee_Only);
            Queens = new EloBuddy.SDK.Item((int)ItemId.Frost_Queens_Claim);
            Hextech = new EloBuddy.SDK.Item((int)ItemId.Hextech_Gunblade);
            HextechGLP = new EloBuddy.SDK.Item((int)3030);
            HextechPro = new EloBuddy.SDK.Item((int)3152);
            You = new EloBuddy.SDK.Item((int)ItemId.Youmuus_Ghostblade);
            Glory = new EloBuddy.SDK.Item((int)ItemId.Righteous_Glory);
            Talis = new EloBuddy.SDK.Item((int)ItemId.Talisman_of_Ascension);
            Rot = new EloBuddy.SDK.Item((int)ItemId.ZzRot_Portal);
            Mountain = new EloBuddy.SDK.Item((int)ItemId.Face_of_the_Mountain);
            Omen = new EloBuddy.SDK.Item((int)ItemId.Randuins_Omen);
            Serpah = new EloBuddy.SDK.Item((int)ItemId.Seraphs_Embrace);
            Solari = new EloBuddy.SDK.Item((int)ItemId.Locket_of_the_Iron_Solari);
            Zhonyas = new EloBuddy.SDK.Item((int)ItemId.Zhonyas_Hourglass);
            Mercurial = new EloBuddy.SDK.Item((int)ItemId.Mercurial_Scimitar);
            Qss = new EloBuddy.SDK.Item((int)ItemId.Quicksilver_Sash);
            HpPot = new EloBuddy.SDK.Item((int)ItemId.Health_Potion);
            CorruptPot = new EloBuddy.SDK.Item((int)ItemId.Corrupting_Potion);
            HunterPot = new EloBuddy.SDK.Item((int)ItemId.Hunters_Potion);
            RefillPot = new EloBuddy.SDK.Item((int)ItemId.Refillable_Potion);
            Biscuit = new EloBuddy.SDK.Item((int)ItemId.Total_Biscuit_of_Rejuvenation);

            //summoners
            var slot1 = ObjectManager.Player.GetSpellSlotFromName("summonerheal");
            if (slot1 != SpellSlot.Unknown)
            {
                Heal = new Spell.Active(slot1, 600);
            }
            var slot2 = ObjectManager.Player.GetSpellSlotFromName("summonerbarrier");
            if (slot2 != SpellSlot.Unknown)
            {
                Barrier = new Spell.Active(slot2);
            }
            var slot3 = ObjectManager.Player.GetSpellSlotFromName("summonerdot");
            if (slot3 != SpellSlot.Unknown)
            {
                Ignite = new Spell.Targeted(slot3, 600);
            }
            var slot4 = ObjectManager.Player.GetSpellSlotFromName("summonerboost");
            if (slot4 != SpellSlot.Unknown)
            {
                Cleanse = new Spell.Active(slot4);
            }
            var slot5 = ObjectManager.Player.GetSpellSlotFromName("summonerexhaust");
            if (slot5 != SpellSlot.Unknown)
            {
                Exhaust = new Spell.Targeted(slot5, 650);
            }
            var slot6 = ObjectManager.Player.Spellbook.Spells.FirstOrDefault(x => x.Name.ToLower().Contains("smite"));
            if (slot6 != null)
            {
                Smite = new Spell.Targeted(slot6.Slot, 570);
            }
        }
    }
}
