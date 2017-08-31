
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AkaYasuo;
using EloBuddy.SDK.Menu;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace AkaYasuo
{
    internal class MenuManager
    {
        public static Menu YMenu,
            ComboMenu,
            HarassMenu,
            LaneClearMenu,
            LastHitMenu,
            JungleClearMenu,
            MiscMenu,
            FleeMenu,
            KillStealMenu,
            DrawingMenu,
            DogeMenu,
            ItemMenu;

        public static string[] gapcloser;
        public static string[] interrupt;
        public static string[] notarget;
        public static Dictionary<string, Menu> SubMenu = new Dictionary<string, Menu>() { };

        public static void Load()
        {
            Mainmenu();
            Combomenu();
            Harassmenu();
            Fleemenu();
            LaneClearmenu();
            LastHitmenu();
            JungleClearmenu();
            Miscmenu();
            KillStealmenu();
            Drawingmenu();
            Dogemenu();
            Itemmenu();
        }

        public static void Mainmenu()
        {
            YMenu = MainMenu.AddMenu("Aka´s Yasuo", "akasyasuo");
            YMenu.AddGroupLabel("Welcome to my Yasuo Addon have fun! :)");
        }

        public static void Combomenu()
        {
            ComboMenu = YMenu.AddSubMenu("Combo", "Combo");
            ComboMenu.AddGroupLabel("Combo");
            ComboMenu.Add("Q", new CheckBox("Use Q"));
            ComboMenu.Add("EC", new CheckBox("Use E"));
            ComboMenu.Add("E1", new Slider("when enemy range >=", 375, 475, 1));
            ComboMenu.Add("E2", new Slider("Use E-GapCloser when enemy range >=", 230, 1300, 1));
            ComboMenu.Add("E3", new CheckBox("Mode: On = ToTarget / OFF = ToMouse"));
            ComboMenu.Add("R", new CheckBox("Use R"));
            ComboMenu.Add("Ignite", new CheckBox("Use Ignite"));
            ComboMenu.AddGroupLabel("R Combo Settings");
            foreach (var hero in EntityManager.Heroes.Enemies.Where(x => x.IsEnemy))
            {
                ComboMenu.Add(hero.ChampionName, new CheckBox("Use R if target is " + hero.ChampionName));
            }
            ComboMenu.AddSeparator();
            ComboMenu.Add("R4", new CheckBox("Use R Instantly when >= 1 ally is in Range"));
            ComboMenu.Add("R2", new Slider("when enemy hp <=", 50, 0, 101));
            ComboMenu.Add("R3", new Slider("when x enemy is knocked", 2, 0, 5));
            ComboMenu.AddGroupLabel("Auto R Settings");
            ComboMenu.Add("AutoR", new CheckBox("Use Auto R"));
            ComboMenu.Add("AutoR2", new Slider("when x enemy is knocked", 3, 0, 5));
            ComboMenu.Add("AutoR2HP", new Slider("and my HP is >=", 101, 0, 101));
            ComboMenu.Add("AutoR2Enemies", new Slider("and Enemies in range <=", 2, 0, 5));
        }

        public static void Harassmenu()
        {
            HarassMenu = YMenu.AddSubMenu("Harass", "Harass");
            HarassMenu.Add("AutoQ", new KeyBind("Auto Q Toggle", true, KeyBind.BindTypes.PressToggle, 'T'));
            HarassMenu.Add("AutoQMinion", new KeyBind("Auto Q Minion Toggle", true, KeyBind.BindTypes.PressToggle, 'H'));
            HarassMenu.Add("AutoQ3", new CheckBox("Use Q3 Auto?"));
            HarassMenu.Add("Q", new CheckBox("Use Q"));
            HarassMenu.Add("Q3", new CheckBox("Use Q3"));
            HarassMenu.Add("E", new CheckBox("Use E"));
            HarassMenu.Add("QunderTower", new CheckBox("Auto Q UnderTower"));
        }

        public static void Fleemenu()
        {
            FleeMenu = YMenu.AddSubMenu("Flee", "Flee");
            FleeMenu.Add("EscQ", new CheckBox("Use Q"));
            FleeMenu.Add("EscE", new CheckBox("Use E"));
            FleeMenu.Add("WJ", new KeyBind("Walljump in Flee mode", false, KeyBind.BindTypes.HoldActive, 'G'));
        }

        public static void LaneClearmenu()
        {
            LaneClearMenu = YMenu.AddSubMenu("LaneClear", "LaneClear");
            LaneClearMenu.Add("Q", new CheckBox("Use Q"));
            LaneClearMenu.Add("Q3", new CheckBox("Use Q3"));
            LaneClearMenu.Add("E", new CheckBox("Use E"));
            LaneClearMenu.Add("Items", new CheckBox("Use Items"));
        }

        public static void JungleClearmenu()
        {
            JungleClearMenu = YMenu.AddSubMenu("JungleClear", "JungleClear");
            JungleClearMenu.Add("Q", new CheckBox("Use Q"));
            JungleClearMenu.Add("E", new CheckBox("Use E"));
            JungleClearMenu.Add("Items", new CheckBox("Use Items"));
        }

        public static void LastHitmenu()
        {
            LastHitMenu = YMenu.AddSubMenu("LastHit", "LastHit");
            LastHitMenu.Add("Q", new CheckBox("Use Q"));
            LastHitMenu.Add("Q3", new CheckBox("Use Q3"));
            LastHitMenu.Add("E", new CheckBox("Use E"));
        }

        public static void KillStealmenu()
        {
            KillStealMenu = YMenu.AddSubMenu("KillSteal", "KillSteal");
            KillStealMenu.Add("KsQ", new CheckBox("Use Q"));
            KillStealMenu.Add("KsE", new CheckBox("Use E"));
            KillStealMenu.Add("KsIgnite", new CheckBox("Use Ignite"));
        }

        public static void Miscmenu()
        {
            MiscMenu = YMenu.AddSubMenu("Misc", "Misc");
            MiscMenu.Add("InterruptQ", new CheckBox("Use Q3 to Interrupt"));
            MiscMenu.Add("noEturret", new CheckBox("Dont Jump Turret"));
            MiscMenu.AddSeparator();
            MiscMenu.AddLabel("1: Q 2: E");
            MiscMenu.Add("autolvl", new CheckBox("Activate Auto level"));
            MiscMenu.Add("autolvls", new Slider("Level Mode", 1, 1, 2));
            switch (MiscMenu["autolvls"].Cast<Slider>().CurrentValue)
            {
                case 1:
                    Variables.abilitySequence = new[] { 1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2 };
                    break;
                case 2:
                    Variables.abilitySequence = new[] { 3, 1, 2, 3, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
                    break;
            }
            var skin = MiscMenu.Add("sID", new Slider("Skin", 0, 0, 2));
            var sID = new[] { "Classic", "High-Noon Yasuo", "Project Yasuo" };
            skin.DisplayName = sID[skin.CurrentValue];

            skin.OnValueChange +=
                delegate (ValueBase<int> sender, ValueBase<int>.ValueChangeArgs changeArgs)
                {
                    sender.DisplayName = sID[changeArgs.NewValue];
                };
        }

        public static void Drawingmenu()
        {
            DrawingMenu = YMenu.AddSubMenu("Drawing", "Drawing");
            DrawingMenu.Add("DrawQ", new CheckBox("Draw Q"));
            DrawingMenu.Add("DrawQ3", new CheckBox("Draw Q3"));
            DrawingMenu.Add("DrawE", new CheckBox("Draw E"));
            DrawingMenu.Add("DrawR", new CheckBox("Draw R"));
            DrawingMenu.Add("DrawTarget", new CheckBox("Draw Target"));
            DrawingMenu.Add("DrawSpots", new CheckBox("Draw Walljump spots"));
        }

        public static void Dogemenu()
        {
            DogeMenu = YMenu.AddSubMenu("Doge", "Doge");
            DogeMenu.Add("smartW", new CheckBox("Smart WindWall"));
            DogeMenu.Add("smartWD",
                new Slider("Smart WindWall Delay(cast when SPELL is about to hit in x milliseconds)", 3000, 0, 3000));
            DogeMenu.Add("smartEDogue", new CheckBox("E use Doge"));
            DogeMenu.Add("wwDanger", new CheckBox("WindWall only dangerous"));
            var skillShots = MainMenu.AddMenu("Enemy Skillshots", "aShotsSkills");

            foreach (var hero in ObjectManager.Get<AIHeroClient>())
            {
                if (hero.Team != ObjectManager.Player.Team)
                {
                    foreach (var spell in SpellDatabase.Spells)
                    {
                        if (spell.ChampionName == hero.ChampionName)
                        {
                            SubMenu["SMIN"] = skillShots.AddSubMenu(spell.MenuItemName, spell.MenuItemName);

                            SubMenu["SMIN"].Add
                                ("DangerLevel" + spell.MenuItemName,
                                    new Slider("Danger level", spell.DangerValue, 5, 1));

                            SubMenu["SMIN"].Add
                                ("IsDangerous" + spell.MenuItemName,
                                    new CheckBox("Is Dangerous", spell.IsDangerous));

                            //SubMenu["SMIN"].Add("Draw" + spell.MenuItemName, new CheckBox("Draw"));
                            SubMenu["SMIN"].Add("Enabled" + spell.MenuItemName, new CheckBox("Enabled"));
                        }
                    }
                }
            }
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

