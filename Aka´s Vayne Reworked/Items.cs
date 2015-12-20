using System.Linq;
using EloBuddy;
using EloBuddy.SDK;

namespace Aka_s_Vayne_reworked
{
    internal static class AkaActivator
    {
        public static Spell.Targeted Ignite, Smite;

        public static readonly string[] SmiteableUnits =
{
            "SRU_Red", "SRU_Blue", "SRU_Dragon", "SRU_Baron",
            "SRU_Gromp", "SRU_Murkwolf", "SRU_Razorbeak",
            "SRU_Krug",  "Sru_Crab"
        };


        private static readonly int[] SmiteBlue = { 3706, 1403, 1402, 1401, 1400 };
        private static readonly int[] SmiteRed = { 3715, 1415, 1414, 1413, 1412 };

        public static Item Youmus,
            Botrk,
            Bilgewater,
            CorruptPot,
            HuntersPot,
            RefillPot,
            Biscuit,
            HPPot,
            Qss,
            Mercurial,
            Hydra,
            Tiamat,
            GunBlade,
            Mikeals,
            Solari,
            Mountain,
            Dervish,
            Zhonyas,
            Archangles,
        Witchcap ;

        public static Spell.Active Heal, Barrier, Cleanse;
        public static void LoadSpells()
        {
            SpellSlot smiteSlot;
            if (ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Summoner1).Name.Contains("dot"))
                Ignite = new Spell.Targeted(SpellSlot.Summoner1, 580);
            else if (ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Summoner2).Name.Contains("dot"))
                Ignite = new Spell.Targeted(SpellSlot.Summoner2, 580);
            if (ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Summoner1).Name.Contains("barrier"))
                Barrier = new Spell.Active(SpellSlot.Summoner1);
            else if (ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Summoner2).Name.Contains("barrier"))
                Barrier = new Spell.Active(SpellSlot.Summoner2);
            if (ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Summoner1).Name.Contains("heal"))
                Heal = new Spell.Active(SpellSlot.Summoner1);
            else if (ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Summoner2).Name.Contains("heal"))
                Heal = new Spell.Active(SpellSlot.Summoner2);
            if (ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Summoner1).Name.Contains("summonerboost"))
                Cleanse = new Spell.Active(SpellSlot.Summoner1);
            else if (ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Summoner2).Name.Contains("summonerboost"))
                Cleanse = new Spell.Active(SpellSlot.Summoner2);
            if (SmiteBlue.Any(x => ObjectManager.Player.InventoryItems.FirstOrDefault(a => a.Id == (ItemId)x) != null))
                smiteSlot = ObjectManager.Player.GetSpellSlotFromName("s5_summonersmiteplayerganker");
            else if (SmiteRed.Any(x => ObjectManager.Player.InventoryItems.FirstOrDefault(a => a.Id == (ItemId)x) != null))
                smiteSlot = ObjectManager.Player.GetSpellSlotFromName("s5_summonersmiteduel");
            else
                smiteSlot = ObjectManager.Player.GetSpellSlotFromName("summonersmite");
            Smite = new Spell.Targeted(smiteSlot, 500);
            Youmus = new Item((int)ItemId.Youmuus_Ghostblade);
            Botrk = new Item((int)ItemId.Blade_of_the_Ruined_King);
            Bilgewater = new Item((int)ItemId.Bilgewater_Cutlass);
            Qss = new Item((int)ItemId.Quicksilver_Sash);
            Mercurial = new Item((int) ItemId.Mercurial_Scimitar);
            Hydra = new Item((int)ItemId.Ravenous_Hydra_Melee_Only);
            Tiamat = new Item((int)ItemId.Tiamat_Melee_Only);
            GunBlade = new Item((int)ItemId.Hextech_Gunblade);
            Mikeals = new Item((int)ItemId.Mikaels_Crucible);
            Solari = new Item((int)ItemId.Locket_of_the_Iron_Solari);
            Mountain = new Item((int)ItemId.Face_of_the_Mountain);
            Dervish = new Item((int) ItemId.Dervish_Blade);
            Zhonyas = new Item((int)ItemId.Zhonyas_Hourglass);
            Witchcap = new Item((int)ItemId.Wooglets_Witchcap);
            Archangles = new Item(3040);
            HPPot = new Item(2003);
            Biscuit = new Item(2010);
            RefillPot = new Item(2031);
            HuntersPot = new Item(2032);
            CorruptPot = new Item(2033);

        }


        public static int GetSmiteDamage()
        {
            if (Smite == null || !Smite.IsReady())
            {
                return 0;
            }
            int level = ObjectManager.Player.Level;
            int[] smitedamage =
            {
                20*level + 370,
                30*level + 330,
                40*level + 240,
                50*level + 100
            };
            return smitedamage.Max();
        }
    }
}