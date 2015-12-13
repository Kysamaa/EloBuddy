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
        private const string MenuName = "Aka´s Azir";

        private static readonly Menu Menu;


        static Config()
        {
            // Initialize the menu
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("Welcome to my Azir Addon have fun! :)");
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
                private static readonly CheckBox _useQ2;
                private static readonly CheckBox _useW;
                private static readonly CheckBox _useE;
                private static readonly CheckBox _useR;
                private static readonly CheckBox _useII;
                private static readonly KeyBind _useI;
                private static readonly Slider _useIm;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }
                
                public static bool UseQ2
                {
                    get { return _useQ2.CurrentValue; }
                }
                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }
                public static bool UseIgnite
                {
                    get { return _useII.CurrentValue; }
                }

                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }
                public static bool UseInsec
                {
                    get { return _useI.CurrentValue; }
                }
                public static int UseInsecMode
                {
                    get { return _useIm.CurrentValue; }
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
                    _useQ2 = Menu.Add("comboUseQ2", new CheckBox("Use Q only if >= 2 Soldiers"));
                    _useW = Menu.Add("comboUseW", new CheckBox("Use W"));
                    _useE = Menu.Add("comboUseE", new CheckBox("Use E(if combo => target health"));
                    _useR = Menu.Add("comboUseR", new CheckBox("Use R(same)"));
                    _useII = Menu.Add("UseIgnite", new CheckBox("Use Ignite(same)"));
                    Menu.AddSeparator();
                    _useI = Menu.Add("Insec", new KeyBind("Insec", false, KeyBind.BindTypes.HoldActive, 'H'));
                    Menu.AddLabel("1: To Allys 2: To Tower 3: To Mouse");
                    _useIm = Menu.Add("Insecmode", new Slider("Insec Mode", 1, 1, 3));
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
                private static readonly Slider _useSm;
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

                public static int UseSmax
                {
                    get { return _useSm.CurrentValue; }
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
                   _UseQ = Menu.Add("UseQHarrass", new CheckBox("Use Q"));
                    _useW = Menu.Add("UseWHarass", new CheckBox("Use W"));
                    _useE = Menu.Add("UseEHarass", new CheckBox("Use E"));
                    _useSm = Menu.Add("UseSm", new Slider("Max soldiers at the same time =", 2, 1, 3));
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
                private static readonly CheckBox _useJ;


                public static bool UseJ
                {
                    get { return _useJ.CurrentValue; }
                }


                static Flee()
                {
                    // Initialize the menu values
                    Menu.AddGroupLabel("Flee");
                    _useJ = Menu.Add("jump", new CheckBox("Jump to mouse!"));
                }

                public static void Initialize()
                {
                }
            }

            public static class JungleClear
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly Slider _Mana;
                private static readonly Slider _useSmm;

                public static int UseSmax
                {
                    get { return _useSmm.CurrentValue; }
                }

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }

                public static int Mana
                {
                    get { return _Mana.CurrentValue; }
                }

                static JungleClear()
                {
                    // Initialize the menu values
                    Menu.AddGroupLabel("JungleClear");
                    _useQ = Menu.Add("JCQ", new CheckBox("Use Q"));
                    _useW = Menu.Add("JCW", new CheckBox("Use W"));
                    _useSmm = Menu.Add("UseSmn", new Slider("Max soldiers at the same time =", 2, 1, 3));
                    _Mana = Menu.Add("ManaHarassdsdsdsdww", new Slider("Maximum mana usage in percent ({0}%)", 40));
                }

                public static void Initialize()
                {
                }
            }


            public static class LaneClear
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly Slider _Mana;
                private static readonly Slider _useSm;

                public static int UseSmax
                {
                    get { return _useSm.CurrentValue; }
                }

                public static int Mana
                {
                    get { return _Mana.CurrentValue; }
                }
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
                    _useSm = Menu.Add("UseSmm", new Slider("Max soldiers at the same time =", 2, 1, 3));
                    _Mana = Menu.Add("ManaHarasssdsd", new Slider("Maximum mana usage in percent ({0}%)", 40));
                }

                public static void Initialize()
                {
                }
            }

            public class MiscMenu
            {
                private static readonly CheckBox _InterruptE;
                private static readonly CheckBox _KSQ;
                private static readonly CheckBox _KSE;
                private static readonly CheckBox _KSI;

                public static bool InterruptE
                {
                    get { return _InterruptE.CurrentValue; }
                }

                public static bool KSQ
                {
                    get { return _KSQ.CurrentValue; }
                }

                public static bool KSI
                {
                    get { return _KSI.CurrentValue; }
                }

                public static bool KSE
                {
                    get { return _KSE.CurrentValue; }
                }


                static MiscMenu()
                {

                    Menu.AddGroupLabel("Misc");
                    _KSQ = Menu.Add("KSQ", new CheckBox("Ks Q"));
                    _KSE = Menu.Add("KSE", new CheckBox("Ks E"));
                    _KSI = Menu.Add("KSI", new CheckBox("Ks Ignite"));
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

            }
        }
    }

            
        
    

