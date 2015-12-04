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
        private const string MenuName = "Aka´s Shaco";

        private static readonly Menu Menu;



        static Config()
        {
            // Initialize the menu
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("Welcome to my Shaco Addon have fun! :)");
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

                // Initialize all modes
                // Combo
                Combo.Initialize();
                Menu.AddSeparator();

                // Harass
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

            }

            public static void Initialize()
            {
            }

            public static class Combo
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useQ1;
                private static readonly Slider _useQ1Slider;
                private static readonly CheckBox _useW;
                private static readonly CheckBox _useE;
                private static readonly CheckBox _useR;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }


                public static bool UseQ1
                {
                    get { return _useQ1.CurrentValue; }
                }
                public static int UseQ1Slider
                {
                    get { return _useQ1Slider.CurrentValue; }
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
                    _useQ1 = Menu.Add("comboUseQ1", new CheckBox("Use Q VS Mode if enemies surrond ≤"));
                    _useQ1Slider = Menu.Add("comboQ1Slider", new Slider("Q vs Mode if ", 2, 1, 5));
                    _useW = Menu.Add("comboUseW", new CheckBox("Use W"));
                    _useE = Menu.Add("comboUseE", new CheckBox("Use E"));
                    _useR = Menu.Add("comboUseR", new CheckBox("Use R", false));
                }

                public static void Initialize()
                {
                }
            }

            public static class Harass
            {

                private static readonly CheckBox _useE;
                private static readonly Slider _Mana;

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
                    _useE = Menu.Add("UseEHarass", new CheckBox("Use E"));
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

                static Flee()
                {
                    // Initialize the menu values
                    Menu.AddGroupLabel("Flee");
                    _useQ = Menu.Add("FleeUseQ", new CheckBox("Use Q"));
                    _useW = Menu.Add("FleeUseW", new CheckBox("Use W"));
                    _useE = Menu.Add("FleeUseE", new CheckBox("Use E"));
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

                static LaneClear()
                {
                    // Initialize the menu values
                    Menu.AddGroupLabel("LaneClear");
                    _useQ = Menu.Add("LCQ", new CheckBox("Use Q"));
                    _useW = Menu.Add("LCW", new CheckBox("Use W"));
                    _useE = Menu.Add("LCE", new CheckBox("Use E"));
                }

                public static void Initialize()
                {
                }
            }

            public class MiscMenu
            {
                private static readonly CheckBox _InterruptW;
                private static readonly CheckBox _KSQ;
                private static readonly CheckBox _KSW;
                private static readonly CheckBox _KSE;
                private static readonly CheckBox _KSR;
                private static readonly CheckBox _dR;
                private static readonly CheckBox _CO;
                private static readonly Slider _skinId;

                public static bool CloneOrbwalk
                {
                    get { return _CO.CurrentValue; }
                }
                public static bool InterruptW
                {
                    get { return _InterruptW.CurrentValue; }
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

                public static bool KSE
                {
                    get { return _KSE.CurrentValue; }
                }
                public static bool dR
                {
                    get { return _dR.CurrentValue; }
                }
                public static int skinId
                {
                    get { return _skinId.CurrentValue; }
                }

                static MiscMenu()
                {

                    Menu.AddGroupLabel("Misc");
                    _KSQ = Menu.Add("KSQ", new CheckBox("Ks Q"));
                    _KSW = Menu.Add("KSW", new CheckBox("Ks W"));
                    _KSE = Menu.Add("KSE", new CheckBox("Ks E"));
                    _KSR = Menu.Add("KSR", new CheckBox("Ks R"));
                    _CO = Menu.Add("CO", new CheckBox("Clone Orbwalk?"));
                    _dR = Menu.Add("dR", new CheckBox("Dodge Spells using R?"));
                    _skinId = Menu.Add("skinId", new Slider("Skin Hack", 7, 1, 9));
                    _InterruptW = Menu.Add("InterruptW", new CheckBox("Interrupt Spells using W?"));
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

            }
        }
    }

            
        
    

