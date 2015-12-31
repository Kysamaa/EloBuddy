using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK.Menu;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace Aka_s_Vayne_reworked
{
    class MenuManager
    {
        public static Menu VMenu,
            ComboMenu,
            CondemnMenu,
            HarassMenu,
            FleeMenu,
            LaneClearMenu,
            JungleClearMenu,
            MiscMenu,
            ItemMenu,
            MechanicMenu,
            DrawingMenu;

        public static void Load()
        {
            Mainmenu();
            Combomenu();
            Condemnmenu();
            Harassmenu();
            Fleemenu();
            LaneClearmenu();
            JungleClearmenu();
            Miscmenu();
            Itemmenu();
            Mechanicmenu();
            Drawingmenu();
        }

        public static void Mainmenu()
        {
            VMenu = MainMenu.AddMenu("Aka´s Vayne", "akavayne");
            VMenu.AddGroupLabel("Welcome to my Vayne Addon have fun! :)");
            VMenu.AddGroupLabel("Made by Aka *-*");
        }

        public static void Combomenu()
        {
            ComboMenu = VMenu.AddSubMenu("Combo", "Combo");
            ComboMenu.AddGroupLabel("Combo");
            ComboMenu.AddGroupLabel("Q Settings");
            ComboMenu.AddLabel("1: Mouse 2: Prada");
            ComboMenu.Add("Qmode", new Slider("Q Mode", 2, 1, 2));
            ComboMenu.Add("QDistance", new Slider("min Q Distance from target", 375, 325, 525));
            ComboMenu.Add("UseQb", new CheckBox("Use Q before AA?", false));
            ComboMenu.Add("UseQa", new CheckBox("Use Q after AA?"));
            ComboMenu.AddLabel("Once you untick´d the AA Reset you have to reload[F5]");
            ComboMenu.Add("AAReset", new CheckBox("Use my AA Reset"));
            ComboMenu.AddLabel("If your AA´s Cancel use this, or deactivate my AA Reset.");
            ComboMenu.Add("AACancel", new Slider("AA Cancel", 0, 0, 20));
            ComboMenu.AddGroupLabel("W Settings");
            ComboMenu.Add("focusw", new CheckBox("Focus W", false));
            ComboMenu.AddGroupLabel("E Settings");
            ComboMenu.Add("Ekill", new CheckBox("Use E if killable?"));
            ComboMenu.Add("comboUseE", new CheckBox("Use E"));
            ComboMenu.AddGroupLabel("R Settings");
            ComboMenu.Add("comboUseR", new CheckBox("Use R", false));
            ComboMenu.Add("RnoAA", new CheckBox("No AA while stealth", false));
            ComboMenu.Add("RnoAAs", new Slider("No AA stealth when >= enemy in range", 2, 0, 5));
            ComboMenu.Add("comboRSlider", new Slider("Use R if", 2, 1, 5));
        }

        public static void Condemnmenu()
        {
            CondemnMenu = VMenu.AddSubMenu("Condemn", "Condemn");
            CondemnMenu.AddGroupLabel("Condemn");
            CondemnMenu.AddLabel("1: Perfect 2: Smart 3: Sharpshooter 4: Gosu 5: VHR");
            CondemnMenu.AddLabel("6: Fastest 7: Legacy 8: Marksman 9: Old 10: Hiki 11: VHR2 12: Fluxys");
            //CondemnMenu.Add("Condemnmode", new Slider("Condemn Mode", 3, 1, 3));
            CondemnMenu.Add("Condemnmode", new Slider("Condemn Mode", 4, 1, 12));
            //CondemnMenu.Add("condemnmethod1", new CheckBox("Condemn 1(Hiki)", false));
            //CondemnMenu.Add("condemnmethod2", new CheckBox("Condemn 2(VHR)", false));
            //CondemnMenu.Add("condemnmethod3", new CheckBox("Condemn 3(Fluxy)"));
            CondemnMenu.Add("UseEb", new CheckBox("Use Condemn before AA?", false));
            CondemnMenu.Add("UseEa", new CheckBox("Use Condemn after AA?"));
            CondemnMenu.Add("condemnPercent", new Slider("Condemn Hitchance %", 33, 1));
            CondemnMenu.Add("trinket", new CheckBox("Use trinket bush?"));
            CondemnMenu.Add("pushDistance", new Slider("Condemn Push Distance", 410, 350, 420));
        }

        public static void Harassmenu()
        {
            HarassMenu = VMenu.AddSubMenu("Harass", "Harass");
            HarassMenu.AddGroupLabel("Harass");
            HarassMenu.AddLabel("I would prefer to tick only 1 of the Options, I prefer the Q one.");
            HarassMenu.Add("UseQHarass", new CheckBox("Use Q(if 2 W stacks)"));
            HarassMenu.Add("UseEHarass", new CheckBox("Use E(if 2 W stacks)", false));
            HarassMenu.Add("UseCHarass", new CheckBox("Use Combo: AA -> Q+AA -> E(broken)", false));
            HarassMenu.Add("ManaHarass", new Slider("Maximum mana usage in percent ({0}%)", 40));
        }

        public static void Fleemenu()
        {
            FleeMenu = VMenu.AddSubMenu("Flee", "Flee");
            FleeMenu.AddGroupLabel("Flee");
            FleeMenu.Add("FleeUseQ", new CheckBox("Use Q"));
            FleeMenu.Add("FleeUseE", new CheckBox("Use E"));
        }

        public static void LaneClearmenu()
        {
            LaneClearMenu = VMenu.AddSubMenu("LaneClear", "LaneClear");
            LaneClearMenu.AddGroupLabel("LaneClear");
            LaneClearMenu.Add("LCQ", new CheckBox("Use Q"));
            LaneClearMenu.Add("LCQMana", new Slider("Maximum mana usage in percent ({0}%)", 40));
        }

        public static void JungleClearmenu()
        {
            JungleClearMenu = VMenu.AddSubMenu("JungleClear", "JungleClear");
            JungleClearMenu.AddGroupLabel("JungleClear");
            JungleClearMenu.Add("JCQ", new CheckBox("Use Q"));
            JungleClearMenu.Add("JCE", new CheckBox("Use E"));
        }

        public static void Miscmenu()
        {
            MiscMenu = VMenu.AddSubMenu("Misc", "Misc");
            MiscMenu.AddGroupLabel("Misc");
            MiscMenu.AddLabel("Credits to Fluxy:");
            MiscMenu.Add("GapcloseE", new CheckBox("Gapclose E"));
            MiscMenu.Add("AntiRengar", new CheckBox("Anti Rengar"));
            MiscMenu.Add("AntiKalista", new CheckBox("Anti Kalista"));
            MiscMenu.Add("AntiPanth", new CheckBox("Anti Pantheon"));
            MiscMenu.Add("fpsdrop", new CheckBox("Anti Fps Drop", false));
            MiscMenu.Add("InterruptE", new CheckBox("Interrupt Spells using E?"));
            var dangerSlider = MiscMenu.Add("dangerLevel", new Slider("Set Your Danger Level: ", 3, 1, 3));
            var dangerSliderDisplay = MiscMenu.Add("dangerLevelDisplay",
                new Label("Danger Level: " + Variables.DangerSliderValues[dangerSlider.Cast<Slider>().CurrentValue - 1]));
            dangerSlider.Cast<Slider>().OnValueChange += delegate
            {
                dangerSliderDisplay.Cast<Label>().DisplayName =
                    "Danger Level: " + Variables.DangerSliderValues[dangerSlider.Cast<Slider>().CurrentValue - 1];
            };
        }

        public static void Itemmenu()
        {
            ItemMenu = VMenu.AddSubMenu("Activator", "Activator");
            ItemMenu.AddGroupLabel("Items");
            ItemMenu.AddLabel("Ask me if you need more Items.");
            ItemMenu.Add("botrk", new CheckBox("Use Botrk & Bilge"));
            ItemMenu.Add("you", new CheckBox("Use Yoummmus"));
            ItemMenu.Add("yous", new Slider("if distance >", 1000, 0, 1500));
            ItemMenu.Add("autopotion", new CheckBox("Auto Healpotion"));
            ItemMenu.Add("autopotionhp", new Slider("HpPot if hp =>", 60));
            ItemMenu.AddGroupLabel("Summoners");
            ItemMenu.AddLabel("Ask me if you need more Summoners.");
            ItemMenu.Add("heal", new CheckBox("Heal"));
            ItemMenu.Add("hp", new Slider("Heal if my HP <=", 20, 0, 100));
            ItemMenu.Add("healally", new CheckBox("Heal ally"));
            ItemMenu.Add("hpally", new Slider("Heal if ally HP <=", 20, 0, 100));
            ItemMenu.AddGroupLabel("Qss");
            ItemMenu.Add("qss", new CheckBox("Use Qss"));
            ItemMenu.Add("delay", new Slider("Delay", 1000, 0, 2000));
            ItemMenu.Add("Blind",
                new CheckBox("Blind", false));
            ItemMenu.Add("Charm",
                new CheckBox("Charm"));
            ItemMenu.Add("Fear",
                new CheckBox("Fear"));
            ItemMenu.Add("Polymorph",
                new CheckBox("Polymorph"));
            ItemMenu.Add("Stun",
                new CheckBox("Stun"));
            ItemMenu.Add("Snare",
                new CheckBox("Snare"));
            ItemMenu.Add("Silence",
                new CheckBox("Silence", false));
            ItemMenu.Add("Taunt",
                new CheckBox("Taunt"));
            ItemMenu.Add("Suppression",
                new CheckBox("Suppression"));
        }

        public static void Mechanicmenu()
        {
            MechanicMenu = VMenu.AddSubMenu("Extras", "Extras");
            MechanicMenu.AddGroupLabel("Mechanics");
            MechanicMenu.Add("flashe", new KeyBind("Flash Condemn!", false, KeyBind.BindTypes.HoldActive, 'Y'));
            MechanicMenu.Add("insece", new KeyBind("Flash Insec!", false, KeyBind.BindTypes.HoldActive, 'Z'));
            MechanicMenu.AddLabel("1: To Allys 2: To Tower 3: To Mouse");
            MechanicMenu.Add("insecmodes", new Slider("Insec Mode", 1, 1, 3));
            MechanicMenu.AddGroupLabel("Utility");
            MechanicMenu.Add("skinhack", new CheckBox("Activate Skin hack"));
            MechanicMenu.Add("skinId", new Slider("Skin Hack", 0, 0, 9));
            MechanicMenu.Add("autobuy", new CheckBox("Autobuy Starters/Trinkets"));
            MechanicMenu.AddLabel("1: Max W 2: Max Q(my style :3)");
            MechanicMenu.Add("autolvl", new CheckBox("Activate Auto level"));
            MechanicMenu.Add("autolvls", new Slider("Level Mode", 1, 1, 2));
            switch (MechanicMenu["autolvls"].Cast<Slider>().CurrentValue)
            {
                case 1:
                    Variables.AbilitySequence = new[] { 1, 3, 2, 2, 2, 4, 2, 1, 2, 1, 4, 1, 1, 3, 3, 4, 3, 3 };
                    break;
                case 2:
                    Variables.AbilitySequence = new[] { 1, 3, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    break;
            }
        }

        public static void Drawingmenu()
        {
            DrawingMenu = VMenu.AddSubMenu("Drawings", "Drawings");
            DrawingMenu.AddGroupLabel("Drawings");
            DrawingMenu.Add("DrawQ", new CheckBox("Draw Q", false));
            DrawingMenu.Add("DrawE", new CheckBox("Draw E", false));
            DrawingMenu.Add("DrawOnlyReady", new CheckBox("Draw Only if Spells are ready"));
        }

    }
}
