using System;
using System.Collections.Generic;
using System.Linq;
using SharpDX;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK.Constants;
using EloBuddy.SDK.Utils;
using EloBuddy.SDK.Rendering;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;


namespace AddonTemplate.Utility
{
    public class TargetSelector2
    {
        #region Main

        static TargetSelector2()
        {
            Game.OnWndProc += GameOnOnWndProc;
            Drawing.OnDraw += DrawingOnOnDraw;
        }

        #endregion

        #region Enum

        public enum DamageType
        {
            Magical,
            Physical,
            True
        }

        public enum TargetingMode
        {
            AutoPriority,
            LowHP,
            MostAD,
            MostAP,
            Closest,
            NearMouse,
            LessAttack,
            LessCast
        }

        #endregion

        #region Vars

        public static TargetingMode Mode = TargetingMode.AutoPriority;
        private static Menu _configMenu;
        private static AIHeroClient _selectedTargetObjAiHero;

        private static bool UsingCustom;

        public static bool CustomTS
        {
            get { return UsingCustom; }
            set
            {
                UsingCustom = value;
                if (value)
                {
                    Game.OnWndProc -= GameOnOnWndProc;
                    Drawing.OnDraw -= DrawingOnOnDraw;
                }
                else
                {
                    Game.OnWndProc += GameOnOnWndProc;
                    Drawing.OnDraw += DrawingOnOnDraw;
                }
            }
        }

        #endregion

        #region EventArgs

        private static void DrawingOnOnDraw(EventArgs args)
        {
            if (_selectedTargetObjAiHero.IsValidTarget() && _configMenu != null &&
                _configMenu["FocusSelected"].Cast<CheckBox>().CurrentValue && _configMenu["DrawTarget"].Cast<CheckBox>().CurrentValue)
            {
                Circle.Draw(Color.Red, 150, _selectedTargetObjAiHero.Position);
            }
        }

        private static void GameOnOnWndProc(WndEventArgs args)
        {
            if (args.Msg != (uint)WindowsMessages.WM_LBUTTONDOWN)
            {
                return;
            }
            _selectedTargetObjAiHero =
                EntityManager.Heroes.Enemies
                    .FindAll(hero => hero.IsValidTarget() && hero.Distance(Game.CursorPos, true) < 40000) // 200 * 200
                    .OrderBy(h => h.Distance(Game.CursorPos, true)).FirstOrDefault();
        }

        #endregion

        #region Functions

        public static AIHeroClient SelectedTarget
        {
            get
            {
                return (_configMenu != null && _configMenu["FocusSelected"].Cast<CheckBox>().CurrentValue
                    ? _selectedTargetObjAiHero
                    : null);
            }
        }

        /// <summary>
        ///     Sets the priority of the hero
        /// </summary>
        public static void SetPriority(AIHeroClient hero, int newPriority)
        {
            if (_configMenu == null || _configMenu["TargetSelector2" + hero.ChampionName + "Priority"] == null)
            {
                return;
            }
            var p = _configMenu["TargetSelector2" + hero.ChampionName + "Priority"].Cast<Slider>();
            p.CurrentValue = Math.Max(1, Math.Min(5, newPriority));
            _configMenu["TargetSelector2" + hero.ChampionName + "Priority"].Cast<Slider>().CurrentValue = p.MaxValue;
        }

        /// <summary>
        ///     Returns the priority of the hero
        /// </summary>
        public static float GetPriority(AIHeroClient hero)
        {
            var p = 1;
            if (_configMenu != null && _configMenu["TargetSelector2" + hero.ChampionName + "Priority"] != null)
            {
                p = _configMenu["TargetSelector2" + hero.ChampionName + "Priority"].Cast<Slider>().CurrentValue;
            }

            switch (p)
            {
                case 2:
                    return 1.5f;
                case 3:
                    return 1.75f;
                case 4:
                    return 2f;
                case 5:
                    return 2.5f;
                default:
                    return 1f;
            }
        }

        private static int GetPriorityFromDb(string championName)
        {
            string[] p1 =
            {
                "Alistar", "Amumu", "Bard", "Blitzcrank", "Braum", "Cho'Gath", "Dr. Mundo", "Garen", "Gnar",
                "Hecarim", "Janna", "Jarvan IV", "Leona", "Lulu", "Malphite", "Nami", "Nasus", "Nautilus", "Nunu",
                "Olaf", "Rammus", "Renekton", "Sejuani", "Shen", "Shyvana", "Singed", "Sion", "Skarner", "Sona",
                "Soraka", "Taric", "Thresh", "Volibear", "Warwick", "MonkeyKing", "Yorick", "Zac", "Zyra"
            };

            string[] p2 =
            {
                "Aatrox", "Darius", "Elise", "Evelynn", "Galio", "Gangplank", "Gragas", "Irelia", "Jax",
                "Lee Sin", "Maokai", "Morgana", "Nocturne", "Pantheon", "Poppy", "Rengar", "Rumble", "Ryze", "Swain",
                "Trundle", "Tryndamere", "Udyr", "Urgot", "Vi", "XinZhao", "RekSai"
            };

            string[] p3 =
            {
                "Akali", "Diana", "Ekko", "Fiddlesticks", "Fiora", "Fizz", "Heimerdinger", "Jayce", "Kassadin",
                "Kayle", "Kha'Zix", "Lissandra", "Mordekaiser", "Nidalee", "Riven", "Shaco", "Vladimir", "Yasuo",
                "Zilean"
            };

            string[] p4 =
            {
                "Ahri", "Anivia", "Annie", "Ashe", "Azir", "Brand", "Caitlyn", "Cassiopeia", "Corki", "Draven",
                "Ezreal", "Graves", "Jinx", "Kalista", "Karma", "Karthus", "Katarina", "Kennen", "KogMaw", "Leblanc",
                "Lucian", "Lux", "Malzahar", "MasterYi", "MissFortune", "Orianna", "Quinn", "Sivir", "Syndra", "Talon",
                "Teemo", "Tristana", "TwistedFate", "Twitch", "Varus", "Vayne", "Veigar", "VelKoz", "Viktor", "Xerath",
                "Zed", "Ziggs"
            };

            if (p1.Contains(championName))
            {
                return 1;
            }
            if (p2.Contains(championName))
            {
                return 2;
            }
            if (p3.Contains(championName))
            {
                return 3;
            }
            return p4.Contains(championName) ? 4 : 1;
        }

        public static Menu config;
        internal static void Initialize()
        {
            Loading.OnLoadingComplete += args =>
            {
                config = MainMenu.AddMenu("Target Selector2", "TargetSelector2");

                _configMenu = config;

                config.Add("FocusSelected", new CheckBox("Focus selected target", true));
                config.Add
                    ("ForceFocusSelected", new CheckBox("Only attack selected target", false));
                config.Add
                    ("DrawTarget", new CheckBox("Draw Circle on Selected Target", true));
                config.AddSeparator(10);
                var autoPriorityItem =
                    config.Add("AutoPriority", new CheckBox("Auto arrange priorities", false));
                autoPriorityItem.OnValueChange += AutoPriorityItem_OnValueChange;

                foreach (var enemy in EntityManager.Heroes.Enemies)
                {
                    config.Add
                       ("TargetSelector2" + enemy.ChampionName + "Priority", new Slider(enemy.ChampionName, autoPriorityItem.Cast<CheckBox>().CurrentValue ? GetPriorityFromDb(enemy.ChampionName) : 1, 1, 5));

                    if (autoPriorityItem.Cast<CheckBox>().CurrentValue)
                    {
                        config["TargetSelector2" + enemy.ChampionName + "Priority"].Cast<Slider>().CurrentValue = GetPriorityFromDb(enemy.ChampionName);
                    }
                }
                //config.AddSubMenu(autoPriorityItem);
                config.AddStringList("TargetingMode", "Target Mode", (Enum.GetNames(typeof(TargetingMode))), 0);
                config["TargetingMode"].Cast<Slider>().CurrentValue = 0;
                //config.AddStringList("TargetingMode", "Target Mode", new[] { "AutoPriority", "LowHP", "MostAD", "MostAP", "Closest", "NearMouse", "LessAttack" , "LessCast" }, 0);
                //config["TargetingMode"].Cast<Slider>().CurrentValue = 0;

                //CommonMenu.Config.AddSubMenu(config);
            };
        }

        private static void AutoPriorityItem_OnValueChange(ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs args)
        {
            if (!args.NewValue)
            {
                return;
            }
            foreach (var enemy in EntityManager.Heroes.Enemies)
            {
                _configMenu["TargetSelector2" + enemy.ChampionName + "Priority"].Cast<Slider>().CurrentValue = GetPriorityFromDb(enemy.ChampionName);
            }
        }

        public static void AddToMenu(Menu config)
        {
            config.AddSubMenu("Alert", "----Use TS in Common Menu----");
        }

        public static bool IsInvulnerable(Obj_AI_Base target, DamageType damageType, bool ignoreShields = true)
        {
            //Kindred's Lamb's Respite(R)

            if (target.HasBuff("kindrednodeathbuff"))
            {
                return true;
            }

            // Tryndamere's Undying Rage (R)
            if (target.HasBuff("Undying Rage") && target.Health <= target.MaxHealth * 0.10f)
            {
                return true;
            }

            // Kayle's Intervention (R)
            if (target.HasBuff("JudicatorIntervention"))
            {
                return true;
            }

            // Poppy's Diplomatic Immunity (R)
            if (target.HasBuff("DiplomaticImmunity") && !ObjectManager.Player.HasBuff("poppyulttargetmark"))
            {
                //TODO: Get the actual target mark buff name
                return true;
            }

            if (ignoreShields)
            {
                return false;
            }

            // Morgana's Black Shield (E)
            if (damageType.Equals(DamageType.Magical) && target.HasBuff("BlackShield"))
            {
                return true;
            }

            // Banshee's Veil (PASSIVE)
            if (damageType.Equals(DamageType.Magical) && target.HasBuff("BansheesVeil"))
            {
                // TODO: Get exact Banshee's Veil buff name.
                return true;
            }

            // Sivir's Spell Shield (E)
            if (damageType.Equals(DamageType.Magical) && target.HasBuff("SivirShield"))
            {
                // TODO: Get exact Sivir's Spell Shield buff name
                return true;
            }

            // Nocturne's Shroud of Darkness (W)
            if (damageType.Equals(DamageType.Magical) && target.HasBuff("ShroudofDarkness"))
            {
                // TODO: Get exact Nocturne's Shourd of Darkness buff name
                return true;
            }

            return false;
        }


        public static void SetTarget(AIHeroClient hero)
        {
            if (hero.IsValidTarget())
            {
                _selectedTargetObjAiHero = hero;
            }
        }

        public static AIHeroClient GetSelectedTarget()
        {
            return SelectedTarget;
        }

        public static AIHeroClient GetTarget(float range,
            DamageType damageType,
            bool ignoreShield = true,
            IEnumerable<AIHeroClient> ignoredChamps = null,
            Vector3? rangeCheckFrom = null)
        {
            return GetTarget(ObjectManager.Player, range, damageType, ignoreShield, ignoredChamps, rangeCheckFrom);
        }

        public static AIHeroClient GetTargetNoCollision(Spell2 spell,
            bool ignoreShield = true,
            IEnumerable<AIHeroClient> ignoredChamps = null,
            Vector3? rangeCheckFrom = null)
        {
            var t = GetTarget(ObjectManager.Player, spell.Range,
                spell.DamageType, ignoreShield, ignoredChamps, rangeCheckFrom);

            if (spell.Collision && spell.GetPrediction(t).Hitchance != HitChance.Collision)
            {
                return t;
            }

            return null;
        }

        private static bool IsValidTarget(Obj_AI_Base target,
            float range,
            DamageType damageType,
            bool ignoreShieldSpells = true,
            Vector3? rangeCheckFrom = null)
        {
            return target.IsValidTarget() &&
                   target.Distance4(rangeCheckFrom ?? ObjectManager.Player.ServerPosition, true) <
                   Math.Pow(range <= 0 ? Orbwalking.GetRealAutoAttackRange(target) : range, 2) &&
                   !IsInvulnerable(target, damageType, ignoreShieldSpells);
        }

        public static AIHeroClient GetTarget(Obj_AI_Base champion,
            float range,
            DamageType type,
            bool ignoreShieldSpells = true,
            IEnumerable<AIHeroClient> ignoredChamps = null,
            Vector3? rangeCheckFrom = null)
        {
            try
            {
                if (ignoredChamps == null)
                {
                    ignoredChamps = new List<AIHeroClient>();
                }

                var damageType = (Damage.DamageType)Enum.Parse(typeof(Damage.DamageType), type.ToString());

                if (_configMenu != null && IsValidTarget(
                    SelectedTarget, _configMenu["ForceFocusSelected"].Cast<CheckBox>().CurrentValue ? float.MaxValue : range,
                    type, ignoreShieldSpells, rangeCheckFrom))
                {
                    return SelectedTarget;
                }

                if (_configMenu != null && _configMenu["TargetingMode"] != null &&
                    Mode == TargetingMode.AutoPriority)
                {
                    var menuItem = _configMenu["TargetingMode"].Cast<Slider>().CurrentValue;
                }

                var targets =
                    EntityManager.Heroes.Enemies
                        .FindAll(
                            hero =>
                                ignoredChamps.All(ignored => ignored.NetworkId != hero.NetworkId) &&
                                IsValidTarget(hero, range, type, ignoreShieldSpells, rangeCheckFrom));

                switch (Mode)
                {
                    case TargetingMode.LowHP:
                        return targets.MinOrDefault(hero => hero.Health);

                    case TargetingMode.MostAD:
                        return targets.MaxOrDefault(hero => hero.BaseAttackDamage + hero.FlatPhysicalDamageMod);

                    case TargetingMode.MostAP:
                        return targets.MaxOrDefault(hero => hero.BaseAbilityDamage + hero.FlatMagicDamageMod);

                    case TargetingMode.Closest:
                        return
                            targets.MinOrDefault(
                                hero =>
                                    (rangeCheckFrom.HasValue ? rangeCheckFrom.Value : champion.ServerPosition).Distance6(
                                        hero.ServerPosition, true));

                    case TargetingMode.NearMouse:
                        return targets.Find(hero => hero.Distance(Game.CursorPos, true) < 22500); // 150 * 150

                    case TargetingMode.AutoPriority:
                        return
                            targets.MaxOrDefault(
                                hero =>
                                    champion.CalcDamage(hero, damageType, 100) / (1 + hero.Health) * GetPriority(hero));

                    case TargetingMode.LessAttack:
                        return
                            targets.MaxOrDefault(
                                hero =>
                                    champion.CalcDamage(hero, Damage.DamageType.Physical, 100) / (1 + hero.Health) *
                                    GetPriority(hero));

                    case TargetingMode.LessCast:
                        return
                            targets.MaxOrDefault(
                                hero =>
                                    champion.CalcDamage(hero, Damage.DamageType.Magical, 100) / (1 + hero.Health) *
                                    GetPriority(hero));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }

        #endregion
    }

    /// <summary>
    ///     This TS attempts to always lock the same target, useful for people getting targets for each spell, or for champions
    ///     that have to burst 1 target.
    /// </summary>
    public class LockedTargetSelector
    {
        public static AIHeroClient _lastTarget;
        private static TargetSelector2.DamageType _lastDamageType;

        public static AIHeroClient GetTarget(float range,
            TargetSelector2.DamageType damageType,
            bool ignoreShield = true,
            IEnumerable<AIHeroClient> ignoredChamps = null,
            Vector3? rangeCheckFrom = null)
        {
            if (_lastTarget == null || !_lastTarget.IsValidTarget() || _lastDamageType != damageType)
            {
                var newTarget = TargetSelector2.GetTarget(range, damageType, ignoreShield, ignoredChamps, rangeCheckFrom);

                _lastTarget = newTarget;
                _lastDamageType = damageType;

                return newTarget;
            }

            if (_lastTarget.IsValidTarget(range) && damageType == _lastDamageType)
            {
                return _lastTarget;
            }

            var newTarget2 = TargetSelector2.GetTarget(range, damageType, ignoreShield, ignoredChamps, rangeCheckFrom);

            _lastTarget = newTarget2;
            _lastDamageType = damageType;

            return newTarget2;
        }

        /// <summary>
        ///     Unlocks the currently locked target.
        /// </summary>
        public static void UnlockTarget()
        {
            _lastTarget = null;
        }

        public static void AddToMenu(Menu menu)
        {
            TargetSelector2.AddToMenu(menu);
        }
    }
}