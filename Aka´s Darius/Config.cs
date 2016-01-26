using System;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

// ReSharper disable InconsistentNaming
// ReSharper disable MemberHidesStaticFromOuterClass

namespace AddonTemplate
{
    // I can't really help you with my layout of a good config class
    // since everyone does it the way they like it most, go checkout my
    // config classes I make on my GitHub if you wanna take over the
    // complex way that I use
    public static class Config
    {
        private const string MenuName = "Aka´s Darius";

        private static readonly Menu Menu;

        static Config()
        {
            // Initialize the menu
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("Welcome to my Darius Addon have fun! :)");
            Menu.AddLabel("To see/change the Settings");
            Menu.AddLabel("Click on Modes :)");

            // Initialize the modes
            Modes.Initialize();
        }

        public static void Initialize()
        {
        }

        public static class Modes
        {
            private static readonly Menu Menu;

            static Modes()
            {
                // Initialize the menu
                Menu = Config.Menu.AddSubMenu("Modes");

                Combo.Initialize();
                Menu.AddSeparator();
                Harass.Initialize();
                Menu.AddSeparator();
                Flee.Initialize();
                Menu.AddSeparator();
                LaneClear.Initialize();
                Menu.AddSeparator();
                JungleClear.Initialize();
                Menu.AddSeparator();
                MiscMenu.Initialize();
                Menu.AddSeparator();
                Drawing.Initialize();
                Menu.AddSeparator();
                Items.Initialize();

            }

            public static void Initialize()
            {
            }

            public static class Combo
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly CheckBox _useE;
                private static readonly CheckBox _useR;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }

                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }

                public static bool UseR
                {
                    get { return _useR.CurrentValue; }
                }

                static Combo()
                {
                    // Initialize the menu values
                    Menu.AddGroupLabel("Combo");
                    _useQ = Menu.Add("comboUseQ", new CheckBox("Use Q"));
                    _useW = Menu.Add("comboUseW", new CheckBox("Use W"));
                    _useE = Menu.Add("comboUseE", new CheckBox("Use E"));
                    _useR = Menu.Add("comboUseR", new CheckBox("Use R"));
                }

                public static void Initialize()
                {
                }
            }

            public static class Harass
            {

                private static readonly CheckBox _UseQ;
                private static readonly CheckBox _useW;
                private static readonly CheckBox _useE;
                private static readonly Slider _Mana;

                public static bool UseQ
                {
                    get { return _UseQ.CurrentValue; }
                }

                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }

                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }

                public static int Mana
                {
                    get { return _Mana.CurrentValue; }
                }

                static Harass()
                {
                    // Here is another option on how to use the menu, but I prefer the
                    // way that I used in the combo class
                    Menu.AddGroupLabel("Harass");
                    _UseQ = Menu.Add("UseQHarrass", new CheckBox("Use Q(Auto)"));
                    _useW = Menu.Add("UseWHarass", new CheckBox("Use W"));
                    _useE = Menu.Add("UseEHarass", new CheckBox("Use E(Auto)"));
                    // Adding a slider, we have a little more options with them, using {0} {1} and {2}
                    // in the display name will replace it with 0=current 1=min and 2=max value
                    _Mana = Menu.Add("ManaHarass", new Slider("Maximum mana usage in percent ({0}%)", 40));
                }

                public static void Initialize()
                {
                }
            }

            public static class Flee
            {
                private static readonly CheckBox _useW;


                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }


                static Flee()
                {
                    // Initialize the menu values
                    Menu.AddGroupLabel("Flee");
                    _useW = Menu.Add("FleeUseW", new CheckBox("Use W"));
                }

                public static void Initialize()
                {
                }
            }

            public static class JungleClear
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly CheckBox _useE;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }

                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }

                static JungleClear()
                {
                    // Initialize the menu values
                    Menu.AddGroupLabel("JungleClear");
                    _useQ = Menu.Add("JCQ", new CheckBox("Use Q"));
                    _useW = Menu.Add("JCW", new CheckBox("Use W"));
                    _useE = Menu.Add("JCE", new CheckBox("Use E"));
                }

                public static void Initialize()
                {
                }
            }


            public static class LaneClear
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }


                static LaneClear()
                {
                    // Initialize the menu values
                    Menu.AddGroupLabel("LaneClear");
                    _useQ = Menu.Add("LCQ", new CheckBox("Use Q"));
                    _useW = Menu.Add("LCW", new CheckBox("Use W"));
                }

                public static void Initialize()
                {
                }
            }

            public class MiscMenu
            {
                private static readonly CheckBox _InterruptE;
                private static readonly CheckBox _KSQ;
                private static readonly CheckBox _KSW;
                private static readonly CheckBox _KSR;

                public static bool InterruptE
                {
                    get { return _InterruptE.CurrentValue; }
                }

                public static bool KSQ
                {
                    get { return _KSQ.CurrentValue; }
                }

                public static bool KSR
                {
                    get { return _KSR.CurrentValue; }
                }

                public static bool KSW
                {
                    get { return _KSW.CurrentValue; }
                }


                static MiscMenu()
                {

                    Menu.AddGroupLabel("Misc");
                    _KSQ = Menu.Add("KSQ", new CheckBox("Ks Q"));
                    _KSW = Menu.Add("KSE", new CheckBox("Ks W"));
                    _KSR = Menu.Add("KSR", new CheckBox("Ks R"));
                    _InterruptE = Menu.Add("InterruptEQ", new CheckBox("Interrupt Spells using E?"));
                }

                public static void Initialize()
                {
                }
            }

            public static class Drawing
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly CheckBox _useE;
                private static readonly CheckBox _useR;
                private static readonly CheckBox _DrawonlyReady;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }

                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }

                public static bool UseR
                {
                    get { return _useR.CurrentValue; }
                }

                public static bool DrawOnlyReady
                {
                    get { return _DrawonlyReady.CurrentValue; }
                }

                static Drawing()
                {
                    // Initialize the menu values
                    Menu.AddGroupLabel("Drawings?");
                    _useQ = Menu.Add("DrawQ", new CheckBox("Draw Q"));
                    _useW = Menu.Add("DrawW", new CheckBox("Draw W"));
                    _useE = Menu.Add("DrawE", new CheckBox("Draw E"));
                    _useR = Menu.Add("DrawR", new CheckBox("Draw R"));
                    _DrawonlyReady = Menu.Add("DrawOnlyReady", new CheckBox("Draw Only if Spells are ready"));
                }

                public static void Initialize()
                {
                }
            }

            public static class Items
            {
                private static readonly CheckBox _Items;
                private static readonly Slider _myHp;
                private static readonly KeyBind _Qss;
                private static readonly Slider _Delay;
                private static readonly CheckBox _Blind;
                private static readonly CheckBox _Charm;
                private static readonly CheckBox _Fear;
                private static readonly CheckBox _Polymorph;
                private static readonly CheckBox _Stun;
                private static readonly CheckBox _Silence;
                private static readonly CheckBox _Taunt;
                private static readonly CheckBox _Supression;
                private static readonly CheckBox _Snare;

                public static bool items
                {
                    get { return _Items.CurrentValue; }
                }

                public static int myHp
                {
                    get { return _myHp.CurrentValue; }
                }

                public static bool Qss
                {
                    get { return _Qss.CurrentValue; }
                }

                public static int Delay
                {
                    get { return _Delay.CurrentValue; }
                }

                public static bool Blind
                {
                    get { return _Blind.CurrentValue; }
                }

                public static bool Charm
                {
                    get { return _Charm.CurrentValue; }
                }

                public static bool Fear
                {
                    get { return _Fear.CurrentValue; }
                }

                public static bool Polymorph
                {
                    get { return _Polymorph.CurrentValue; }
                }

                public static bool Stun
                {
                    get { return _Stun.CurrentValue; }
                }

                public static bool Silence
                {
                    get { return _Silence.CurrentValue; }
                }

                public static bool Taunt
                {
                    get { return _Taunt.CurrentValue; }
                }

                public static bool Supression
                {
                    get { return _Supression.CurrentValue; }
                }

                public static bool Snare
                {
                    get { return _Snare.CurrentValue; }
                }

                static Items()
                {
                    Menu.AddGroupLabel("Items");
                    _Items = Menu.Add("Items", new CheckBox("Use Items"));
                    _myHp = Menu.Add("myhp", new Slider("Use BOTRK if my HP is <=", 70, 0, 101));
                    Menu.AddSeparator();
                    _Qss = Menu.Add("use", new KeyBind("Use QSS/Mercurial", true, KeyBind.BindTypes.PressToggle, 'K'));
                    _Delay = Menu.Add("delay", new Slider("Activation Delay", 1000, 0, 2000));
                    _Blind = Menu.Add("Blind",
                        new CheckBox("Blind", false));
                    _Charm = Menu.Add("Charm",
                        new CheckBox("Charm"));
                    _Fear = Menu.Add("Fear",
                        new CheckBox("Fear"));
                    _Polymorph = Menu.Add("Polymorph",
                        new CheckBox("Polymorph"));
                    _Stun = Menu.Add("Stun",
                        new CheckBox("Stun"));
                    _Snare = Menu.Add("Snare",
                        new CheckBox("Snare"));
                    _Silence = Menu.Add("Silence",
                        new CheckBox("Silence", false));
                    _Taunt = Menu.Add("Taunt",
                        new CheckBox("Taunt"));
                    _Supression = Menu.Add("Suppression",
                        new CheckBox("Suppression"));
                }

                public static void Initialize()
                {
                }
            }

        }
    }
}

            
        
    

