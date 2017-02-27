using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Menu;

namespace AkaCore.Manager
{
    class MenuManager
    {
        private static Menu HMenu, ActivatorMenu, UtilityMenu, OrbwalkerMenu, EvadeMenu, FPS;

        public static void Load()
        {
            Hauptmenu();
            Activatormenu();
            Utilitymenu();
            Orbwalkermenu();
            Evademenu();
            FPSMenu();
        }

        private static void Hauptmenu()
        {
            HMenu = MainMenu.AddMenu("AkaCore", "akacdsore");
            HMenu.AddGroupLabel("Extension for my addons.");
        }

        private static void Activatormenu()
        {
            ActivatorMenu = HMenu.AddSubMenu("Activator", "Activator");
            ActivatorMenu.AddGroupLabel("Aggressive Items");
            ActivatorMenu.Add("AItems", new CheckBox("Use Items"));
            ActivatorMenu.Add("Botrk", new CheckBox("Use Botrk/Bilge"));
            ActivatorMenu.Add("Hydra", new CheckBox("Use Hydra/Tiamat"));
            ActivatorMenu.Add("Titanic", new CheckBox("Use Titanic"));
            ActivatorMenu.Add("HGB", new CheckBox("Use Hextech Gunblade"));
            ActivatorMenu.Add("HGLP", new CheckBox("Use Hextech GLP"));
            ActivatorMenu.Add("HPB", new CheckBox("Use Hextech Protobelt"));
            ActivatorMenu.Add("Queens", new CheckBox("Use Frost Queens"));
            ActivatorMenu.Add("QueensDis", new Slider("Enemies distance =>", 1500, 0, 3000));
            ActivatorMenu.Add("Glory", new CheckBox("Use Rightenous Glory"));
            ActivatorMenu.Add("GloryDis", new Slider("Enemies distance =>", 600, 0, 1000));
            ActivatorMenu.Add("Talis", new CheckBox("Use Talisman"));
            ActivatorMenu.Add("TalisDis", new Slider("Enemies distance =>", 1000, 0, 2000));
            ActivatorMenu.Add("You", new CheckBox("Use Ghostblade"));
            ActivatorMenu.AddGroupLabel("Defensive Items");
            ActivatorMenu.Add("DItems", new CheckBox("Use Items"));
            ActivatorMenu.Add("FaceMe", new CheckBox("Use Face of the Mountain Me")); // The identifier tho kappa
            ActivatorMenu.Add("FaceMeHp", new Slider("If my hp <=", 20, 0, 100));
            ActivatorMenu.Add("FaceAlly", new CheckBox("Use Face of the Mountain Ally"));
            ActivatorMenu.Add("FaceAllyHp", new Slider("If allies hp <=", 20, 0, 100));
            ActivatorMenu.Add("SolariMe", new CheckBox("Use Iron Solari Me"));
            ActivatorMenu.Add("SolariMeHp", new Slider("If my hp <=", 20, 0, 100));
            ActivatorMenu.Add("SolariAlly", new CheckBox("Use Iron Solari Ally"));
            ActivatorMenu.Add("SolariAllyHp", new Slider("If allies hp <=", 20, 0, 100));
            ActivatorMenu.Add("Omen", new CheckBox("Use Randuins"));
            ActivatorMenu.Add("OmenHp", new Slider("If my hp <=", 20, 0, 100));
            ActivatorMenu.Add("Seraphs", new CheckBox("Use Seraphs"));
            ActivatorMenu.Add("SeraphsHp", new Slider("If my hp <=", 20, 0, 100));
            ActivatorMenu.Add("Talis2", new CheckBox("Use Talisman"));
            ActivatorMenu.Add("Talis2Hp", new Slider("If my hp <=", 20, 0, 100));
            ActivatorMenu.Add("Glory2", new CheckBox("Use Rightenous Glory"));
            ActivatorMenu.Add("Glory2Hp", new Slider("If my hp <=", 20, 0, 100));
            ActivatorMenu.Add("Zhonyas", new CheckBox("Use Zhonyas"));
            ActivatorMenu.Add("ZhonyasHp", new Slider("If my hp <=", 20, 0, 100));
            ActivatorMenu.AddGroupLabel("Pots");
            ActivatorMenu.Add("PItems", new CheckBox("Use Pots"));
            ActivatorMenu.Add("HPPot", new CheckBox("Use Health potion"));
            ActivatorMenu.Add("HPPotHp", new Slider("If my hp <=", 40, 0, 100));
            ActivatorMenu.Add("Biscuit", new CheckBox("Use Biscuits"));
            ActivatorMenu.Add("BiscuitHp", new Slider("If my hp <=", 40, 0, 100));
            ActivatorMenu.Add("RefillPot", new CheckBox("Use Refillable potion"));
            ActivatorMenu.Add("RefillPotHp", new Slider("If my hp <=", 60, 0, 100));
            ActivatorMenu.Add("HunterPot", new CheckBox("Use Hunter potion"));
            ActivatorMenu.Add("HunterPotHp", new Slider("If my hp <=", 60, 0, 100));
            ActivatorMenu.Add("CorruptPot", new CheckBox("Use Corrupting potion"));
            ActivatorMenu.Add("CourrptPotHp", new Slider("If my hp <=", 60, 0, 100));
            ActivatorMenu.AddGroupLabel("Summoners");
            ActivatorMenu.Add("Heal", new CheckBox("Heal"));
            ActivatorMenu.Add("HealHp", new Slider("Heal if my HP <=", 20, 0, 100));
            ActivatorMenu.Add("HealAlly", new CheckBox("Heal ally"));
            ActivatorMenu.Add("HealAllyHp", new Slider("Heal if ally HP <=", 20, 0, 100));
            ActivatorMenu.Add("Barrier", new CheckBox("Barrier"));
            ActivatorMenu.Add("BarrierHp", new Slider("Barrier if my HP <=", 20, 0, 100));
            ActivatorMenu.Add("Ignite", new CheckBox("Use Ignite"));
            ActivatorMenu.Add("IgniteHp", new Slider("0 KS otherwise if Hp <=", 0, 0, 100));
            ActivatorMenu.Add("Exhaust", new CheckBox("Use Exhaust"));
            ActivatorMenu.Add("ExhaustHp", new Slider("Exhaust if enemies Hp <=", 40, 0, 100));
            ActivatorMenu.AddGroupLabel("Smite");
            ActivatorMenu.Add("SmiteActive", new KeyBind("Smite Active", true, KeyBind.BindTypes.PressToggle));
            ActivatorMenu.Add("SStatus", new CheckBox("Draw Status"));
            ActivatorMenu.Add("SDamage", new CheckBox("Draw Damage"));
            ActivatorMenu.Add("SBaron", new CheckBox("Baron"));
            ActivatorMenu.Add("SHerald", new CheckBox("Herald"));
            ActivatorMenu.Add("SWDragon", new CheckBox("Water Dragon"));
            ActivatorMenu.Add("SFDragon", new CheckBox("Fire Dragon"));
            ActivatorMenu.Add("SEDragon", new CheckBox("Earth Dragon"));
            ActivatorMenu.Add("SADragon", new CheckBox("Air Dragon"));
            ActivatorMenu.Add("SElder", new CheckBox("Elder Dragon"));
            ActivatorMenu.Add("SBlue", new CheckBox("Blue Buff", false));
            ActivatorMenu.Add("SRed", new CheckBox("Red Buff", false));
            ActivatorMenu.Add("SRaptor", new CheckBox("Raptor Camp", false));
            ActivatorMenu.Add("SWolf", new CheckBox("Wolf Camp", false));
            ActivatorMenu.Add("SGromp", new CheckBox("Gromp Camp", false));
            ActivatorMenu.Add("SKrug", new CheckBox("Krug Camp", false));
            ActivatorMenu.Add("SCrap", new CheckBox("Scuttler", false));
            ActivatorMenu.AddGroupLabel("Cleanse");
            ActivatorMenu.Add("Qss", new CheckBox("Use Qss"));
            ActivatorMenu.Add("Mecurial", new CheckBox("Use Mecurial"));
            ActivatorMenu.Add("Cleanser", new CheckBox("Use Cleanse"));
            ActivatorMenu.Add("QssDelay", new Slider("Delay", 100, 0, 2000));
            ActivatorMenu.Add("Blind",
                new CheckBox("Blind", false));
            ActivatorMenu.Add("Charm",
                new CheckBox("Charm"));
            ActivatorMenu.Add("Fear",
                new CheckBox("Fear"));
            ActivatorMenu.Add("Polymorph",
                new CheckBox("Polymorph"));
            ActivatorMenu.Add("Stun",
                new CheckBox("Stun"));
            ActivatorMenu.Add("Snare",
                new CheckBox("Snare"));
            ActivatorMenu.Add("Silence",
                new CheckBox("Silence", false));
            ActivatorMenu.Add("Taunt",
                new CheckBox("Taunt"));
            ActivatorMenu.Add("Suppression",
                new CheckBox("Suppression"));
        }

        private static void Utilitymenu()
        {
            UtilityMenu = HMenu.AddSubMenu("Utility", "kappa");
            UtilityMenu.AddGroupLabel("Utility");
            UtilityMenu.Add("Skinhack", new CheckBox("Activate Skin hack", false));
            UtilityMenu.Add("SkinID", new Slider("SkinID", 0, 0, 15));
            UtilityMenu.Add("Autolvl", new CheckBox("Activate Auto level"));
            UtilityMenu.Add("AutolvlS", new ComboBox("Level Mode", 0, "Max Q", "Max W", "Max E"));
            UtilityMenu.Add("Autobuy", new CheckBox("Autobuy Starters"));
            UtilityMenu.Add("AutobuyS", new ComboBox("Starter Item", 0, "Dorans Blade", "Dorans Ring", "Dorans Shield", "Corrupting Potion", "Hunters Machete", "Hunters Talisman", "Ancient Coin", "SpellThiefs", "Relic Shield"));
            UtilityMenu.Add("Autobuyt", new CheckBox("Upgrade Trinkets"));
            UtilityMenu.Add("AutobuytS", new ComboBox("Upgrade to", 0, "Farsight Alteration", "Oracle Alteration"));
            UtilityMenu.Add("Autolantern", new CheckBox("Auto Lantern"));
            UtilityMenu.Add("AutolanternHP", new Slider("Auto Lantern if Hp =>", 40));
        }

        private static void Orbwalkermenu()
        {
            OrbwalkerMenu = HMenu.AddSubMenu("Orbwalk", "asdasf");
            OrbwalkerMenu.AddGroupLabel("Orbwalker Extensions");
            if (ObjectManager.Player.ChampionName == "Draven")
            {
                OrbwalkerMenu.AddGroupLabel("Auto Catch");
                OrbwalkerMenu.Add("Qmode", new ComboBox("Axe Catch Mode", 1, "Combo", "Always", "Never"));
                OrbwalkerMenu.Add("Qrange", new Slider("Catch Axe Range:", 800, 120, 1500));
                OrbwalkerMenu.Add("WforQ", new CheckBox("Use W if axe to far away"));
                OrbwalkerMenu.AddGroupLabel("Safety Options");
                OrbwalkerMenu.Add("Qturret", new CheckBox("Don´t catch under turret"));
                OrbwalkerMenu.Add("Qenemies", new CheckBox("Don´t catch in enemy group"));
                OrbwalkerMenu.Add("Qkill", new CheckBox("Don´t catch if can kill"));
                OrbwalkerMenu.Add("Qmelee", new CheckBox("Don´t catch near melees"));
                OrbwalkerMenu.AddGroupLabel("Drawings");
                OrbwalkerMenu.Add("DrawAxe", new CheckBox("Draw Axe"));
                OrbwalkerMenu.Add("DrawAxeRange", new CheckBox("Draw Axe catch Range"));

            }
            else
            {
                OrbwalkerMenu.AddGroupLabel("Only for Draaaven");
            }
        }

        private static void Evademenu()
        {
            EvadeMenu = HMenu.AddSubMenu("Evade", "asddsf");
            EvadeMenu.AddGroupLabel("Evade");
            EvadeMenu.AddGroupLabel("Nothing here yet :(");
        }

        private static void FPSMenu()
        {
            FPS = HMenu.AddSubMenu("FPS Protection", "asd");
            //FPS.AddGroupLabel("This is only working with my addons :/");
            
            FPS.Add("minfps", new Slider("Min Fps", 45, 1, 350));
            FPS.Add("calcps", new Slider("Calculations per Sec", 35, 1, 350));
            FPS.Add("enablefps", new CheckBox("Enable FPS Protection"));
        }

        #region checkvalues
        #region checkvalues:activator
        //pots
        public static bool HPPot
        {
            get { return (ActivatorMenu["HPPot"].Cast<CheckBox>().CurrentValue); }
        }

        public static int HPPotHp
        {
            get { return (ActivatorMenu["HPPotHp"].Cast<Slider>().CurrentValue); }
        }

        public static bool Biscuit
        {
            get { return (ActivatorMenu["Biscuit"].Cast<CheckBox>().CurrentValue); }
        }

        public static int BiscuitHp
        {
            get { return (ActivatorMenu["BiscuitHp"].Cast<Slider>().CurrentValue); }
        }

        public static bool RefillPot
        {
            get { return (ActivatorMenu["RefillPot"].Cast<CheckBox>().CurrentValue); }
        }

        public static int RefillPotHp
        {
            get { return (ActivatorMenu["RefillPotHp"].Cast<Slider>().CurrentValue); }
        }

        public static bool HunterPot
        {
            get { return (ActivatorMenu["HunterPot"].Cast<CheckBox>().CurrentValue); }
        }

        public static int HunterPotHp
        {
            get { return (ActivatorMenu["HunterPotHp"].Cast<Slider>().CurrentValue); }
        }

        public static bool CorruptPot
        {
            get { return (ActivatorMenu["CorruptPot"].Cast<CheckBox>().CurrentValue); }
        }

        public static int CorruptPotHp
        {
            get { return (ActivatorMenu["CorruptPotHp"].Cast<Slider>().CurrentValue); }
        }
        //Aggressiveive
        public static bool Botrk
        {
            get { return (ActivatorMenu["Botrk"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool Hextech
        {
            get { return (ActivatorMenu["HGB"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool HextechGLP
        {
            get { return (ActivatorMenu["HGLP"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool HextechPB
        {
            get { return (ActivatorMenu["HPB"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool Hydra
        {
            get { return (ActivatorMenu["Hydra"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool Titanic
        {
            get { return (ActivatorMenu["Titanic"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool Queens
        {
            get { return (ActivatorMenu["Queens"].Cast<CheckBox>().CurrentValue); }
        }

        public static int QueensDistance
        {
            get { return (ActivatorMenu["QueensDis"].Cast<Slider>().CurrentValue); }
        }

        public static bool Glory
        {
            get { return (ActivatorMenu["Glory"].Cast<CheckBox>().CurrentValue); }
        }

        public static int GloryDistance
        {
            get { return (ActivatorMenu["GloryDis"].Cast<Slider>().CurrentValue); }
        }

        public static bool Talis
        {
            get { return (ActivatorMenu["Talis"].Cast<CheckBox>().CurrentValue); }
        }

        public static int TalisDistance
        {
            get { return (ActivatorMenu["TalisDis"].Cast<Slider>().CurrentValue); }
        }

        public static bool You
        {
            get { return (ActivatorMenu["You"].Cast<CheckBox>().CurrentValue); }
        }
        //Defensive
        public static bool MountainMe
        {
            get { return (ActivatorMenu["FaceMe"].Cast<CheckBox>().CurrentValue); }
        }

        public static int MountainMeHp
        {
            get { return (ActivatorMenu["FaceMeHp"].Cast<Slider>().CurrentValue); }
        }

        public static bool MountainAlly
        {
            get { return (ActivatorMenu["FaceAlly"].Cast<CheckBox>().CurrentValue); }
        }

        public static int MoutaiAllyHp
        {
            get { return (ActivatorMenu["FaceAllyHp"].Cast<Slider>().CurrentValue); }
        }

        public static bool SolariMe
        {
            get { return (ActivatorMenu["SolariMe"].Cast<CheckBox>().CurrentValue); }
        }

        public static int SolraiMeHp
        {
            get { return (ActivatorMenu["SolariMeHp"].Cast<Slider>().CurrentValue); }
        }

        public static bool SolariAlly
        {
            get { return (ActivatorMenu["SolariAlly"].Cast<CheckBox>().CurrentValue); }
        }

        public static int SolraiAllyHp
        {
            get { return (ActivatorMenu["SolariAllyHp"].Cast<Slider>().CurrentValue); }
        }

        public static bool Omen
        {
            get { return (ActivatorMenu["Omen"].Cast<CheckBox>().CurrentValue); }
        }

        public static int OmenHp
        {
            get { return (ActivatorMenu["OmenHp"].Cast<Slider>().CurrentValue); }
        }

        public static bool Seraphs
        {
            get { return (ActivatorMenu["Seraphs"].Cast<CheckBox>().CurrentValue); }
        }

        public static int SeraphsHp
        {
            get { return (ActivatorMenu["SerahpsHp"].Cast<Slider>().CurrentValue); }
        }

        public static bool TalisDef
        {
            get { return (ActivatorMenu["Talis2"].Cast<CheckBox>().CurrentValue); }
        }

        public static int TalisDefHp
        {
            get { return (ActivatorMenu["Talis2Hp"].Cast<Slider>().CurrentValue); }
        }

        public static bool GloryDef
        {
            get { return (ActivatorMenu["Glory2"].Cast<CheckBox>().CurrentValue); }
        }

        public static int GloryDefHp
        {
            get { return (ActivatorMenu["Glory2Hp"].Cast<Slider>().CurrentValue); }
        }

        public static bool Zhonyas
        {
            get { return (ActivatorMenu["Zhonyas"].Cast<CheckBox>().CurrentValue); }
        }

        public static int ZhonyasHp
        {
            get { return (ActivatorMenu["ZhonyasHp"].Cast<Slider>().CurrentValue); }
        }

        //Summoososos
        public static bool Heal
        {
            get { return (ActivatorMenu["Heal"].Cast<CheckBox>().CurrentValue); }
        }

        public static int HealHp
        {
            get { return (ActivatorMenu["HealHp"].Cast<Slider>().CurrentValue); }
        }

        public static bool Barrier
        {
            get { return (ActivatorMenu["Barrier"].Cast<CheckBox>().CurrentValue); }
        }

        public static int BarrierHp
        {
            get { return (ActivatorMenu["BarrierHp"].Cast<Slider>().CurrentValue); }
        }

        public static bool HealAlly
        {
            get { return (ActivatorMenu["HealAlly"].Cast<CheckBox>().CurrentValue); }
        }

        public static int HealAllyHp
        {
            get { return (ActivatorMenu["HealAllyHp"].Cast<Slider>().CurrentValue); }
        }

        public static bool Ignite
        {
            get { return (ActivatorMenu["Ignite"].Cast<CheckBox>().CurrentValue); }
        }

        public static int IgniteHp
        {
            get { return (ActivatorMenu["IgniteHp"].Cast<Slider>().CurrentValue); }
        }

        public static bool Exhaust
        {
            get { return (ActivatorMenu["Exhaust"].Cast<CheckBox>().CurrentValue); }
        }

        public static int ExhaustHp
        {
            get { return (ActivatorMenu["ExhaustHp"].Cast<Slider>().CurrentValue); }
        }
        //smite
        public static bool SmiteActive
        {
            get { return (ActivatorMenu["SmiteActive"].Cast<KeyBind>().CurrentValue); }
        }
        public static bool SStatus
        {
            get { return (ActivatorMenu["SStatus"].Cast<CheckBox>().CurrentValue); }
        }
        public static bool SDamage
        {
            get { return (ActivatorMenu["SDamage"].Cast<CheckBox>().CurrentValue); }
        }
        public static bool SBaron
        {
            get { return (ActivatorMenu["SBaron"].Cast<CheckBox>().CurrentValue); }
        }
        public static bool SHerald
        {
            get { return (ActivatorMenu["SHerald"].Cast<CheckBox>().CurrentValue); }
        }
        public static bool SWDragon
        {
            get { return (ActivatorMenu["SWDragon"].Cast<CheckBox>().CurrentValue); }
        }
        public static bool SEDragon
        {
            get { return (ActivatorMenu["SEDragon"].Cast<CheckBox>().CurrentValue); }
        }
        public static bool SADragon
        {
            get { return (ActivatorMenu["SADragon"].Cast<CheckBox>().CurrentValue); }
        }
        public static bool SFDragon
        {
            get { return (ActivatorMenu["SFDragon"].Cast<CheckBox>().CurrentValue); }
        }
        public static bool Elder
        {
            get { return (ActivatorMenu["SElder"].Cast<CheckBox>().CurrentValue); }
        }
        public static bool SBlue
        {
            get { return (ActivatorMenu["SBlue"].Cast<CheckBox>().CurrentValue); }
        }
        public static bool SRaptor
        {
            get { return (ActivatorMenu["SRaptor"].Cast<CheckBox>().CurrentValue); }
        }
        public static bool SRed
        {
            get { return (ActivatorMenu["SRed"].Cast<CheckBox>().CurrentValue); }
        }
        public static bool SWolf
        {
            get { return (ActivatorMenu["SWolf"].Cast<CheckBox>().CurrentValue); }
        }
        public static bool SKrug
        {
            get { return (ActivatorMenu["SKrug"].Cast<CheckBox>().CurrentValue); }
        }
        public static bool SGromp
        {
            get { return (ActivatorMenu["SGromp"].Cast<CheckBox>().CurrentValue); }
        }
        public static bool SGrap
        {
            get { return (ActivatorMenu["SCrap"].Cast<CheckBox>().CurrentValue); }
        }
        //Qssss
        public static bool Qss
        {
            get { return (ActivatorMenu["Qss"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool Mecurial
        {
            get { return (ActivatorMenu["Mecurial"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool Cleanse
        {
            get { return (ActivatorMenu["Cleanser"].Cast<CheckBox>().CurrentValue); }
        }

        public static int QssDelay
        {
            get { return (ActivatorMenu["QssDelay"].Cast<Slider>().CurrentValue); }
        }

        public static bool QssBlind
        {
            get { return (ActivatorMenu["Blind"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool QssCharm
        {
            get { return (ActivatorMenu["Charm"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool QssFear
        {
            get { return (ActivatorMenu["Fear"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool QssPolymorph
        {
            get { return (ActivatorMenu["Polymorph"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool QssStun
        {
            get { return (ActivatorMenu["Stun"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool QssSnare
        {
            get { return (ActivatorMenu["Snare"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool QssSilence
        {
            get { return (ActivatorMenu["Silence"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool QssTaunt
        {
            get { return (ActivatorMenu["Taunt"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool QssSupression
        {
            get { return (ActivatorMenu["Suppression"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool AItems
        {
            get { return (ActivatorMenu["AItems"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool DItems
        {
            get { return (ActivatorMenu["DItems"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool PItems
        {
            get { return (ActivatorMenu["PItems"].Cast<CheckBox>().CurrentValue); }
        }
        #endregion
        #region checkvalues:utility
        public static bool Skinhack
        {
            get { return (UtilityMenu["Skinhack"].Cast<CheckBox>().CurrentValue); }
        }
        public static bool Autolvl
        {
            get { return (UtilityMenu["Autolvl"].Cast<CheckBox>().CurrentValue); }
        }
        public static bool Autobuy
        {
            get { return (UtilityMenu["Autobuy"].Cast<CheckBox>().CurrentValue); }
        }
        public static bool Autobuytrinkets
        {
            get { return (UtilityMenu["Autobuyt"].Cast<CheckBox>().CurrentValue); }
        }
        public static bool Autolantern
        {
            get { return (UtilityMenu["Autolantern"].Cast<CheckBox>().CurrentValue); }
        }
        public static int SkinID
        {
            get { return (UtilityMenu["SkinID"].Cast<Slider>().CurrentValue); }
        }
        public static int AutolvlSlider
        {
            get { return (UtilityMenu["AutolvlS"].Cast<ComboBox>().CurrentValue); }
        }
        public static int AutobuySlider
        {
            get { return (UtilityMenu["AutobuyS"].Cast<ComboBox>().CurrentValue); }
        }
        public static int AutobuytrinketsSlider
        {
            get { return (UtilityMenu["AutobuytS"].Cast<ComboBox>().CurrentValue); }
        }
        public static int AutlanternHp
        {
            get { return (UtilityMenu["AutolanternHP"].Cast<Slider>().CurrentValue); }
        }
        #endregion
        #region checkvalues:orbwalk
        #region autocatch
        #region Axe
        public static int AxeMode
        {
            get { return (OrbwalkerMenu["Qmode"].Cast<ComboBox>().CurrentValue); }
        }
        public static int AxeCatchRange
        {
            get { return (OrbwalkerMenu["Qrange"].Cast<Slider>().CurrentValue); }
        }
        public static bool AxeW
        {
            get { return (OrbwalkerMenu["WforQ"].Cast<CheckBox>().CurrentValue); }
        }
        public static bool DrawAxe
        {
            get { return (OrbwalkerMenu["DrawAxe"].Cast<CheckBox>().CurrentValue); }
        }
        public static bool DrawAxeCatchRange
        {
            get { return (OrbwalkerMenu["DrawAxeRange"].Cast<CheckBox>().CurrentValue); }
        }
        #endregion
        #region safety
        public static bool CatchTower
        {
            get { return (OrbwalkerMenu["Qturret"].Cast<CheckBox>().CurrentValue); }
        }
        public static bool CatchEnemies
        {
            get { return (OrbwalkerMenu["Qenemies"].Cast<CheckBox>().CurrentValue); }
        }
        public static bool CatchKill
        {
            get { return (OrbwalkerMenu["Qkill"].Cast<CheckBox>().CurrentValue); }
        }
        public static bool CatchMelees
        {
            get { return (OrbwalkerMenu["Qmelee"].Cast<CheckBox>().CurrentValue); }
        }
        #endregion
        #endregion
        #endregion
        #region checkvalues:fps

        public static int MinFps
        {
            get { return (FPS["minfps"].Cast<Slider>().CurrentValue); }
        }

        public static int CalcPerSecond
        {
            get { return (FPS["calcps"].Cast<Slider>().CurrentValue); }
        }

        public static bool EnableFPS
        {
            get { return (FPS["enablefps"].Cast<CheckBox>().CurrentValue); }
        }
        #endregion
        #endregion
    }
}
