
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK.Menu;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace AkaDraven
{
    internal class MenuManager
    {
        public static Menu YMenu,
            ComboMenu,
            AxeMenu,
            HarassMenu,
            LaneClearMenu,
            MiscMenu,
            FleeMenu,
            KillStealMenu,
            DrawingMenu,
            ItemMenu;

        public static void Load()
        {
            Mainmenu();
            Axemenu();
            Combomenu();
            Harassmenu();
            Fleemenu();
            LaneClearmenu();
            Miscmenu();
            KillStealmenu();
            Drawingmenu();
            Itemmenu();
        }

        public static void Mainmenu()
        {
            YMenu = MainMenu.AddMenu("Aka´s Draven", "akasdraven");
            YMenu.AddGroupLabel("Welcome to my Draaaaaven Addon have fun! :)");
        }

        public static void Combomenu()
        {
            ComboMenu = YMenu.AddSubMenu("Combo", "Combo");
            ComboMenu.AddGroupLabel("Combo");
            ComboMenu.Add("Q", new CheckBox("Use Q"));
            ComboMenu.Add("W", new CheckBox("Use W"));
            ComboMenu.Add("E", new CheckBox("Use E"));
            ComboMenu.Add("R", new CheckBox("Use R"));
        }

        public static void Axemenu()
        {
            AxeMenu = YMenu.AddSubMenu("Axe Settings", "Axesettings");
            AxeMenu.AddGroupLabel("Axe Settings");
            AxeMenu.AddLabel("1: Combo 2: Any 3: Always");
            AxeMenu.Add("Qmode", new Slider("Axe Catch Mode:", 3, 1, 3));
            AxeMenu.Add("Qrange", new Slider("Catch Axe Range:", 800, 120, 1500));
            AxeMenu.Add("Qmax", new Slider("Maximum Axes:", 2, 1, 3));
            AxeMenu.Add("WforQ", new CheckBox("Use W if axe to far away"));
            AxeMenu.Add("Qunderturret", new CheckBox("Don´t catch under turret"));
        }

        public static void Harassmenu()
        {
            HarassMenu = YMenu.AddSubMenu("Harass", "Harass");
            HarassMenu.Add("E", new CheckBox("Use E"));
            HarassMenu.Add("AutoE", new KeyBind("Auto Harass Toggle", true, KeyBind.BindTypes.PressToggle, 'G'));
        }

        public static void Fleemenu()
        {
            FleeMenu = YMenu.AddSubMenu("Flee", "Flee");
            FleeMenu.Add("E", new CheckBox("Use E"));
            FleeMenu.Add("W", new CheckBox("Use W"));
        }

        public static void LaneClearmenu()
        {
            LaneClearMenu = YMenu.AddSubMenu("LaneClear", "LaneClear");
            LaneClearMenu.Add("Q", new CheckBox("Use Q"));
            LaneClearMenu.Add("W", new CheckBox("Use W"));
            LaneClearMenu.Add("Mana", new Slider("Mana Manager", 50));
        }

        public static void KillStealmenu()
        {
            KillStealMenu = YMenu.AddSubMenu("KillSteal", "KillSteal");
            KillStealMenu.Add("KsE", new CheckBox("Use E"));
            KillStealMenu.Add("KsIgnite", new CheckBox("Use Ignite"));
        }

        public static void Miscmenu()
        {
            MiscMenu = YMenu.AddSubMenu("Misc", "Misc");
            MiscMenu.Add("UseEInterrupt", new CheckBox("Use E to Interrupt"));
            MiscMenu.Add("UseWInstant", new CheckBox("Use W Instant(if ready)", false));
            MiscMenu.Add("UseWSlow", new CheckBox("Use W if slowed"));
            MiscMenu.Add("WMana", new Slider("W Mana Manager", 50));
            MiscMenu.Add("autolvl", new CheckBox("Activate Auto level"));
        }

        public static void Drawingmenu()
        {
            DrawingMenu = YMenu.AddSubMenu("Drawing", "Drawing");
            DrawingMenu.Add("DrawE", new CheckBox("Draw E"));
            DrawingMenu.Add("DrawAxe", new CheckBox("Draw Axe"));
            DrawingMenu.Add("DrawAxeRange", new CheckBox("Draw Axe catch Range"));
        }

        public static void Itemmenu()
        {
            ItemMenu = YMenu.AddSubMenu("Items", "QSS");
            ItemMenu.Add("Items", new CheckBox("Use Items"));
            ItemMenu.Add("myhp", new Slider("Use BOTRK if my HP is <=", 70, 0, 101));
            ItemMenu.AddSeparator();
            ItemMenu.Add("use", new KeyBind("Use QSS/Mercurial", true, KeyBind.BindTypes.PressToggle, 'K'));
            ItemMenu.Add("delay", new Slider("Activation Delay", 1000, 0, 2000));
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
    }
}
