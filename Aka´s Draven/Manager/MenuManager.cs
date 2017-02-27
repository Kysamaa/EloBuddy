using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace Aka_s_Draven.Manager
{
    class MenuManager
    {
        public static Menu YMenu,
    ComboMenu,
    HarassMenu,
    LaneClearMenu,
    MiscMenu,
    FleeMenu,
    DrawingMenu;

        public static void Load()
        {
            Mainmenu();
            Combomenu();
            Harassmenu();
            Fleemenu();
            LaneClearmenu();
            Miscmenu();
            Drawingmenu();
        }

        private static void Mainmenu()
        {
            YMenu = MainMenu.AddMenu("Aka´s Draven", "akasdraven");
            YMenu.AddGroupLabel("Welcome to my Draaaaaven Addon have fun! :)");
        }

        private static void Combomenu()
        {
            ComboMenu = YMenu.AddSubMenu("Combo", "Combo");
            ComboMenu.AddGroupLabel("Combo");
            ComboMenu.Add("Q", new CheckBox("Use Q"));
            ComboMenu.Add("MaxQ", new Slider("Max Axes", 2, 1, 3));
            ComboMenu.Add("W", new CheckBox("Use W"));
            ComboMenu.Add("E", new CheckBox("Use E"));
            ComboMenu.Add("R", new CheckBox("Use R"));
        }

        private static void Harassmenu()
        {
            HarassMenu = YMenu.AddSubMenu("Harass", "Harass");
            HarassMenu.Add("E", new CheckBox("Use E"));
            HarassMenu.Add("EPred", new Slider("E Hitchance %", 50));
            HarassMenu.Add("AutoE", new KeyBind("Auto Harass Toggle", true, KeyBind.BindTypes.PressToggle, 'G'));
        }

        private static void Fleemenu()
        {
            FleeMenu = YMenu.AddSubMenu("Flee", "Flee");
            FleeMenu.Add("E", new CheckBox("Use E"));
            FleeMenu.Add("W", new CheckBox("Use W"));
        }

        private static void LaneClearmenu()
        {
            LaneClearMenu = YMenu.AddSubMenu("LaneClear", "LaneClear");
            LaneClearMenu.Add("Q", new CheckBox("Use Q"));
            LaneClearMenu.Add("W", new CheckBox("Use W", false));
            LaneClearMenu.Add("Mana", new Slider("Mana Manager", 50));
        }

        private static void Miscmenu()
        {
            MiscMenu = YMenu.AddSubMenu("Misc", "Misc");
            MiscMenu.Add("KsE", new CheckBox("KillSteal E"));
            MiscMenu.Add("antigapE", new CheckBox("Antigap E"));
            MiscMenu.Add("UseEInterrupt", new CheckBox("Use E to Interrupt"));
            MiscMenu.Add("UseWInstant", new CheckBox("Use W Instant(if ready)", false));
            MiscMenu.Add("UseWSlow", new CheckBox("Use W if slowed"));
            MiscMenu.Add("WMana", new Slider("W Mana Manager", 50));
        }

        private static void Drawingmenu()
        {
            DrawingMenu = YMenu.AddSubMenu("Drawing", "Drawing");
            DrawingMenu.Add("DrawE", new CheckBox("Draw E"));
            DrawingMenu.Add("DrawAxe", new CheckBox("Draw Axe"));
            DrawingMenu.Add("DrawAxeRange", new CheckBox("Draw Axe catch Range"));
        }

        #region checkvalues
        #region Combo
        public static bool UseQCombo
        {
            get { return (ComboMenu["Q"].Cast<CheckBox>().CurrentValue); }
        }
        public static int MaxAxes
        {
            get { return (ComboMenu["MaxQ"].Cast<Slider>().CurrentValue); }
        }
        public static bool UseECombo
        {
            get { return (ComboMenu["E"].Cast<CheckBox>().CurrentValue); }
        }
        public static bool UseWCombo
        {
            get { return (ComboMenu["W"].Cast<CheckBox>().CurrentValue); }
        }
        public static bool UseRCombo
        {
            get { return (ComboMenu["R"].Cast<CheckBox>().CurrentValue); }
        }
        #endregion
        #region Harass
        public static bool UseEHarass
        {
            get { return (HarassMenu["E"].Cast<CheckBox>().CurrentValue); }
        }
        public static int UseEHarassPred
        {
            get { return (HarassMenu["EPred"].Cast<Slider>().CurrentValue); }
        }
        public static bool AutoHarass
        {
            get { return (HarassMenu["AutoE"].Cast<KeyBind>().CurrentValue); }
        }
        #endregion
        #region Flee
        public static bool UseEFlee
        {
            get { return (FleeMenu["E"].Cast<CheckBox>().CurrentValue); }
        }
        public static bool UseWFlee
        {
            get { return (FleeMenu["W"].Cast<CheckBox>().CurrentValue); }
        }
        #endregion
        #region LaneClear
        public static bool UseQLC
        {
            get { return (LaneClearMenu["Q"].Cast<CheckBox>().CurrentValue); }
        }
        public static bool UseWLC
        {
            get { return (LaneClearMenu["W"].Cast<CheckBox>().CurrentValue); }
        }
        public static int ManaLC
        {
            get { return (LaneClearMenu["Mana"].Cast<Slider>().CurrentValue); }
        }
        #endregion
        #region Misc
        public static bool KSE
        {
            get { return (MiscMenu["KsE"].Cast<CheckBox>().CurrentValue); }
        }
        public static bool AntigapE
        {
            get { return (MiscMenu["antigapE"].Cast<CheckBox>().CurrentValue); }
        }
        public static bool UseEInterrupt
        {
            get { return (MiscMenu["UseEInterrupt"].Cast<CheckBox>().CurrentValue); }
        }
        public static bool UseWEverytime
        {
            get { return (MiscMenu["UseWInstant"].Cast<CheckBox>().CurrentValue); }
        }
        public static bool UseWSlow
        {
            get { return (MiscMenu["UseWSlow"].Cast<CheckBox>().CurrentValue); }
        }
        public static int UseWMana
        {
            get { return (MiscMenu["WMana"].Cast<Slider>().CurrentValue); }
        }
        #endregion
        #region Drawing
        public static bool DrawE
        {
            get { return (DrawingMenu["DrawE"].Cast<CheckBox>().CurrentValue); }
        }
        public static bool DrawAxe
        {
            get { return (DrawingMenu["DrawAxe"].Cast<CheckBox>().CurrentValue); }
        }
        public static bool DrawAxeCatchRange
        {
            get { return (DrawingMenu["DrawAxeRange"].Cast<CheckBox>().CurrentValue); }
        }
        #endregion
        #endregion
    }
}
