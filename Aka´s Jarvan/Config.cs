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
        private const string MenuName = "Aka´s Jarvan";

        private static readonly Menu Menu;

        static Config()
        {
            // Initialize the menu
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("Welcome to my Jarvan Addon have fun! :)");
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
                private static readonly CheckBox _useW;
                private static readonly CheckBox _useE;
                private static readonly CheckBox _useRk;
                private static readonly CheckBox _useRi;

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
                public static bool UseRk
                {
                    get { return _useRk.CurrentValue; }
                }
                public static bool UseRi
                {
                    get { return _useRi.CurrentValue; }
                }
                static Combo()
                {
                    // Initialize the menu values
                    Menu.AddGroupLabel("Combo");
                    _useQ = Menu.Add("comboUseQ", new CheckBox("Use Q"));
                    _useW = Menu.Add("comboUseW", new CheckBox("Use W"));
                    _useE = Menu.Add("comboUseE", new CheckBox("Use E"));
                    Menu.AddLabel("Only activate one of the Use R at the same time!");
                    _useRk = Menu.Add("comboUseRk", new CheckBox("Use R(if killable with Combo"));
                    _useRi = Menu.Add("comboUseRi", new CheckBox("Use R(Engage", false));
                }
            

            public static void Initialize()
                {
                }
            }

            public static class Harass
            {
                public static bool UseQ
                {
                    get { return Menu["harassUseQ"].Cast<CheckBox>().CurrentValue; }
                }

                public static bool UseW
                {
                    get { return Menu["harassUseW"].Cast<CheckBox>().CurrentValue; }
                }

                public static bool UseE
                {
                    get { return Menu["harassUseE"].Cast<CheckBox>().CurrentValue; }
                }

                public static bool UseR
                {
                    get { return Menu["harassUseR"].Cast<CheckBox>().CurrentValue; }
                }

                public static int Mana
                {
                    get { return Menu["harassMana"].Cast<Slider>().CurrentValue; }
                }

                static Harass()
                {
                    // Here is another option on how to use the menu, but I prefer the
                    // way that I used in the combo class
                    Menu.AddGroupLabel("Harass");
                    Menu.Add("harassUseQ", new CheckBox("Use Q"));
                    Menu.Add("harassUseW", new CheckBox("Use W"));
                    Menu.Add("harassUseE", new CheckBox("Use E"));

                    // Adding a slider, we have a little more options with them, using {0} {1} and {2}
                    // in the display name will replace it with 0=current 1=min and 2=max value
                    Menu.Add("harassMana", new Slider("Maximum mana usage in percent ({0}%)", 40));
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
                    _useQ = Menu.Add("fleeUseQ", new CheckBox("Use Q"));
                    _useW = Menu.Add("fleeUseW", new CheckBox("Use W"));
                    _useE = Menu.Add("fleeUseE", new CheckBox("Use E"));
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
                    _useQ = Menu.Add("JCUseQ", new CheckBox("Use Q"));
                    _useW = Menu.Add("JCUseW", new CheckBox("Use W"));
                    _useE = Menu.Add("JCUseE", new CheckBox("Use E"));
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
                    _useQ = Menu.Add("LCUseQ", new CheckBox("Use Q"));
                    _useW = Menu.Add("LCUseW", new CheckBox("Use W"));
                    _useE = Menu.Add("LCUseE", new CheckBox("Use E"));
                }

                public static void Initialize()
                {
                }
            }

            public class MiscMenu
            {
                private static readonly CheckBox _InterruptEQ;
                private static readonly CheckBox _KSQ;
                private static readonly CheckBox _KSE;
                private static readonly CheckBox _KSR;

                public static bool InterruptEQ
                {
                    get { return _InterruptEQ.CurrentValue; }
                }

                public static bool KSQ
                {
                    get { return _KSQ.CurrentValue; }
                }

                public static bool KSR
                {
                    get { return _KSR.CurrentValue; }
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
                    _KSR = Menu.Add("KSR", new CheckBox("Ks R"));
                    _InterruptEQ = Menu.Add("InterruptEQ", new CheckBox("Interrupt Spells using EQ?"));
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
        public static void AddStringList(this Menu m, string uniqueId, string displayName, string[] values, int defaultValue = 0)
        {
            var mode = m.Add(uniqueId, new Slider(displayName, defaultValue, 0, values.Length - 1));
            mode.DisplayName = displayName + ": " + values[mode.CurrentValue];
            mode.OnValueChange += delegate (ValueBase<int> sender, ValueBase<int>.ValueChangeArgs args)
            {
                sender.DisplayName = displayName + ": " + values[args.NewValue];
            };
        }
    }
    }

            
        
    

