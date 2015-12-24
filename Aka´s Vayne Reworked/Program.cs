using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddonTemplate;
using AddonTemplate.Logic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;
using AddonTemplate.Utility;
using Gapcloser = EloBuddy.SDK.Events.Gapcloser;

namespace Aka_s_Vayne_reworked
{
    class Program
    {
        public static AIHeroClient _player
        {
            get { return ObjectManager.Player; }
        }


        public static string[] DangerSliderValues = {"Low", "Medium", "High"};

        public static Spell.Active Q;
        public static Spell.Active W;
        public static Spell.Skillshot E2;
        public static Spell.Targeted E;
        public static Spell.Active R;
        public static Spell.Active Heal;
        public static List<Vector2> Points = new List<Vector2>();
        public static Item totem, Qss, Mercurial;
        public static int[] AbilitySequence;
        public static int QOff = 0, WOff = 0, EOff = 0, ROff = 0;

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

        static void Main(string[] args1)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != "Vayne") return;

            Q = new Spell.Active(SpellSlot.Q, 300);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Targeted(SpellSlot.E, 590);
            E2 = new Spell.Skillshot(SpellSlot.E, 590, SkillShotType.Linear, 250, 1250);
            R = new Spell.Active(SpellSlot.R);
            var slot = ObjectManager.Player.GetSpellSlotFromName("summonerheal");
            if (slot != SpellSlot.Unknown)
            {
                Heal = new Spell.Active(slot, 600);
            }
            totem = new Item((int)ItemId.Warding_Totem_Trinket);
            Qss = new Item((int)ItemId.Quicksilver_Sash);
            Mercurial = new Item((int)ItemId.Mercurial_Scimitar);

            VMenu = MainMenu.AddMenu("Aka´s Vayne", "akavayne");
            VMenu.AddGroupLabel("Welcome to my Vayne Addon have fun! :)");

            ComboMenu = VMenu.AddSubMenu("Combo", "Combo");
            ComboMenu.AddGroupLabel("Combo");
            ComboMenu.AddLabel("Only tick one Q mouse/Q prada");
            ComboMenu.Add("UseQc", new CheckBox("Use Q Mouse", false));
            ComboMenu.Add("UseQp", new CheckBox("Use Q Prada"));
            ComboMenu.Add("UseQb", new CheckBox("Use Q before AA?", false));
            ComboMenu.Add("UseQa", new CheckBox("Use Q after AA?"));
            ComboMenu.Add("focusw", new CheckBox("Focus W", false));
            ComboMenu.Add("Ekill", new CheckBox("Use E if killable?"));
            ComboMenu.Add("comboUseE", new CheckBox("Use E"));
            ComboMenu.Add("comboUseR", new CheckBox("Use R", false));
            ComboMenu.Add("RnoAA", new CheckBox("No AA while stealth", false));
            ComboMenu.Add("RnoAAs", new Slider("No AA stealth when >= enemy in range", 2, 0, 5));
            ComboMenu.Add("comboRSlider", new Slider("Use R if", 2, 1, 5));
            ComboMenu.Add("AACancel", new Slider("AA Cancel", 0, 0, 20));
            ComboMenu.Add("fpsdrop", new CheckBox("Anti Fps Drop", false));

            HarassMenu = VMenu.AddSubMenu("Harass", "Harass");
            HarassMenu.AddGroupLabel("Harass");
            HarassMenu.AddLabel("I would prefer to tick only 1 of the Options, I prefer the Q one.");
            HarassMenu.Add("UseQHarass", new CheckBox("Use Q(if 2 W stacks)"));
            HarassMenu.Add("UseEHarass", new CheckBox("Use E(if 2 W stacks)", false));
            HarassMenu.Add("UseCHarass", new CheckBox("Use Combo: AA -> Q+AA -> E(broken)", false));
            HarassMenu.Add("ManaHarass", new Slider("Maximum mana usage in percent ({0}%)", 40));

            FleeMenu = VMenu.AddSubMenu("Flee", "Flee");
            FleeMenu.AddGroupLabel("Flee");
            FleeMenu.Add("FleeUseQ", new CheckBox("Use Q"));
            FleeMenu.Add("FleeUseE", new CheckBox("Use E"));

            JungleClearMenu = VMenu.AddSubMenu("JungleClear", "JungleClear");
            JungleClearMenu.AddGroupLabel("JungleClear");
            JungleClearMenu.Add("JCQ", new CheckBox("Use Q"));
            JungleClearMenu.Add("JCE", new CheckBox("Use E"));

            LaneClearMenu = VMenu.AddSubMenu("LaneClear", "LaneClear");
            LaneClearMenu.AddGroupLabel("LaneClear");
            LaneClearMenu.Add("LCQ", new CheckBox("Use Q"));
            LaneClearMenu.Add("LCQMana", new Slider("Maximum mana usage in percent ({0}%)", 40));

            MiscMenu = VMenu.AddSubMenu("Misc", "Misc");
            MiscMenu.AddGroupLabel("Misc");
            MiscMenu.AddLabel("Credits to Fluxy:");
            MiscMenu.Add("GapcloseE", new CheckBox("Gapclose E"));
            MiscMenu.Add("AntiRengar", new CheckBox("Anti Rengar"));
            MiscMenu.Add("AntiKalista", new CheckBox("Anti Kalista"));
            MiscMenu.Add("AntiPanth", new CheckBox("Anti Pantheon"));
            MiscMenu.Add("InterruptE", new CheckBox("Interrupt Spells using E?"));
            var dangerSlider = MiscMenu.Add("dangerLevel", new Slider("Set Your Danger Level: ", 3, 1, 3));
            var dangerSliderDisplay = MiscMenu.Add("dangerLevelDisplay",
                new Label("Danger Level: " + DangerSliderValues[dangerSlider.Cast<Slider>().CurrentValue - 1]));
            dangerSlider.Cast<Slider>().OnValueChange += delegate
            {
                dangerSliderDisplay.Cast<Label>().DisplayName =
                    "Danger Level: " + DangerSliderValues[dangerSlider.Cast<Slider>().CurrentValue - 1];
            };

            CondemnMenu = VMenu.AddSubMenu("Condemn", "Condemn");
            CondemnMenu.AddGroupLabel("Condemn");
            CondemnMenu.AddLabel(
                "Only Activate 1 at the same time!, Fluxy is 360° condemn => the best, the others are less obvious");
            CondemnMenu.Add("condemnmethod1", new CheckBox("Condemn 1(Hiki)", false));
            CondemnMenu.Add("condemnmethod2", new CheckBox("Condemn 2(VHR)", false));
            CondemnMenu.Add("condemnmethod3", new CheckBox("Condemn 3(Fluxy)"));
            CondemnMenu.Add("condemnPercent", new Slider("Condemn 3(Fluxy) Hitchance %", 33, 1));
            CondemnMenu.Add("trinket", new CheckBox("Use trinket bush?"));
            CondemnMenu.Add("pushDistance", new Slider("Condemn Push Distance", 410, 350, 420));

            MechanicMenu = VMenu.AddSubMenu("Extras", "Extras");
            MechanicMenu.AddGroupLabel("Mechanics");
            MechanicMenu.Add("flashe", new KeyBind("Flash Condemn!", false, KeyBind.BindTypes.HoldActive, 'Y'));
            MechanicMenu.Add("insece", new KeyBind("Flash Insec!", false, KeyBind.BindTypes.HoldActive, 'Z'));
            MechanicMenu.AddLabel("1: To Allys 2: To Tower 3: To Mouse");
            MechanicMenu.Add("insecmodes", new Slider("Insec Mode", 1, 1, 3));
            MechanicMenu.AddGroupLabel("Utility");
            MechanicMenu.Add("skinhack", new CheckBox("Activate Skin hack(disabled atm)", false));
            MechanicMenu.Add("skinId", new Slider("Skin Hack", 1, 1, 9));
            MechanicMenu.Add("autobuy", new CheckBox("Autobuy Starters/Trinkets"));
            MechanicMenu.AddLabel("1: Max W 2: Max Q(my style :3)");
            MechanicMenu.Add("autolvl", new CheckBox("Activate Auto level"));
            MechanicMenu.Add("autolvls", new Slider("Level Mode", 1, 1, 2));

            DrawingMenu = VMenu.AddSubMenu("Drawings", "Drawings");
            DrawingMenu.AddGroupLabel("Drawings");
            DrawingMenu.Add("DrawQ", new CheckBox("Draw Q", false));
            DrawingMenu.Add("DrawE", new CheckBox("Draw E", false));
            DrawingMenu.Add("DrawOnlyReady", new CheckBox("Draw Only if Spells are ready"));

            ItemMenu = VMenu.AddSubMenu("Activator", "Activator");
            ItemMenu.AddGroupLabel("Items");
            ItemMenu.AddLabel("Ask me if you need more Items.");
            ItemMenu.Add("botrk", new CheckBox("Use Botrk & Bilge"));
            ItemMenu.Add("you", new CheckBox("Use Yoummmus"));
            ItemMenu.Add("yous", new Slider("if distance >", 1000, 0, 1500));
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

            switch (MechanicMenu["autolvls"].Cast<Slider>().CurrentValue)
            {
                case 1:
                    AbilitySequence = new[] { 1, 3, 2, 2, 2, 4, 2, 1, 2, 1, 4, 1, 1, 3, 3, 4, 3, 3 };
                    break;
                case 2:
                    AbilitySequence = new[] { 1, 3, 2, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    break;
            }


            Gapcloser.OnGapcloser += Events.Gapcloser_OnGapCloser;
            Interrupter.OnInterruptableSpell += Events.Interrupter_OnInterruptableSpell;
            Obj_AI_Base.OnBasicAttack += Events.ObjAiBaseOnOnBasicAttack;
            GameObject.OnCreate += Events.GameObject_OnCreate;
            Obj_AI_Base.OnProcessSpellCast += Events.OnProcessSpell;
            Obj_AI_Base.OnBuffGain += Events.Obj_AI_Base_OnBuffGain;
            Obj_AI_Base.OnBasicAttack += Events.Obj_AI_Base_OnBasicAttack;
            Player.OnIssueOrder += Events.Player_OnIssueOrder;
            Game.OnUpdate += Events.Game_OnTick;
            Obj_AI_Base.OnSpellCast += Events.Obj_AI_Base_OnSpellCast;
            Orbwalker.OnPostAttack += Events.Orbwalker_OnPostAttack;

            ELogic.LoadFlash();
            Drawing.OnDraw += OnDraw;
            Traps.Load();

        }

        private static void OnDraw(EventArgs args)
        {

            if (DrawingMenu["DrawE"].Cast<CheckBox>().CurrentValue && !ComboMenu["fpsdrop"].Cast<CheckBox>().CurrentValue)
            {
                if (!(DrawingMenu["DrawOnlyReady"].Cast<CheckBox>().CurrentValue && !E.IsReady()))
                {
                    Circle.Draw(Color.Red, E.Range, Player.Instance.Position);
                }
            }
            if (DrawingMenu["DrawQ"].Cast<CheckBox>().CurrentValue && !ComboMenu["fpsdrop"].Cast<CheckBox>().CurrentValue)
            {
                if (!(DrawingMenu["DrawOnlyReady"].Cast<CheckBox>().CurrentValue && !Q.IsReady()))
                {
                    Circle.Draw(Color.Red, Q.Range, Player.Instance.Position);
                }
            }

        }

        public static void JungleClear()
        {
            if (JungleClearMenu["JCQ"].Cast<CheckBox>().CurrentValue && Q.IsReady() &&
                !Game.CursorPos.IsDangerousPosition())
            {
                QLogic.JungleClear();
            }
            if (JungleClearMenu["JCE"].Cast<CheckBox>().CurrentValue && E.IsReady())
            {
                ELogic.JungleCondemn();
            }
        }

        public static void Flee()
        {
            if (FleeMenu["FleeUseE"].Cast<CheckBox>().CurrentValue)
            {
                var target = TargetSelector.GetTarget(E.Range, DamageType.Physical);
                if (E.IsReady() && target != null)
                {
                    E.Cast(target);
                }
            }
            if (FleeMenu["FleeUseQ"].Cast<CheckBox>().CurrentValue)
            {
                if (Q.IsReady() && !(Game.CursorPos.IsDangerousPosition()))
                {
                    Player.CastSpell(SpellSlot.Q, Game.CursorPos);
                }
            }

        }

        public static void Harass()
        {
            var target = TargetSelector.GetTarget((int) ObjectManager.Player.GetAutoAttackRange(),
                DamageType.Physical);

            if (target == null)
            {
                return;
            }

            if (ObjectManager.Player.ManaPercent < HarassMenu["ManaHarass"].Cast<Slider>().CurrentValue)
                return;
            if (HarassMenu["UseQHarass"].Cast<CheckBox>().CurrentValue && Q.IsReady())
            {
                SilverStackQ();
            }
            if (HarassMenu["UseEHarass"].Cast<CheckBox>().CurrentValue && E.IsReady())
            {
                SilverStackE();
            }
            if (Events.AfterAttack && HarassMenu["UseCHarass"].Cast<CheckBox>().CurrentValue && Q.IsReady() && E.IsReady())
            {
                Orbwalker.ForcedTarget = target;
                Player.CastSpell(SpellSlot.Q, QLogic.GetTumblePos(target));
                E2.Cast(target.Position);
            }

        }

        public static void SilverStackQ()
        {
            foreach (AIHeroClient qTarget in HeroManager.Enemies.Where(x => x.IsValidTarget(550)))
            {
                if (qTarget.Buffs.Any(buff => buff.Name == "vaynesilvereddebuff" && buff.Count == 2) && !Game.CursorPos.IsDangerousPosition())
                {
                    Player.CastSpell(SpellSlot.Q, Game.CursorPos);
                }
            }
        }
        public static void SilverStackE()
        {
            foreach (AIHeroClient qTarget in HeroManager.Enemies.Where(x => x.IsValidTarget(550)))
            {
                if (qTarget.Buffs.Any(buff => buff.Name == "vaynesilvereddebuff" && buff.Count == 2))
                {
                    E.Cast(qTarget);
                }
            }
        }
        public static void Combo()
        {

            var target = TargetSelector.GetTarget(E.Range, DamageType.Physical);
            usetrinket(target);
            if (Program.ComboMenu["comboUseR"].Cast<CheckBox>().CurrentValue && R.IsReady())
            {
                ComboUltimateLogic();
            }
        }

        public static void ComboUltimateLogic()
        {
            if (ObjectManager.Player.CountEnemiesInRange(1000) >= ComboMenu["comboRSlider"].Cast<Slider>().CurrentValue)
            {
                R.Cast();
            }
        }


        public static void usetrinket(Obj_AI_Base target)
        {
            if (target == null)
            {
                return;
            }
            if (ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Trinket).IsReady &&
                ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Trinket).SData.Name.ToLower().Contains("totem"))
            {
                Core.DelayAction(delegate
                {
                    if (Program.CondemnMenu["trinket"].Cast<CheckBox>().CurrentValue)
                    {
                        var pos = ELogic.GetFirstNonWallPos(ObjectManager.Player.Position.To2D(), target.Position.To2D());
                        if (NavMesh.GetCollisionFlags(pos).HasFlag(CollisionFlags.Grass))
                        {
                            totem.Cast(pos.To3D());
                        }
                    }
                }, 200);
            }

        }

    }

}
