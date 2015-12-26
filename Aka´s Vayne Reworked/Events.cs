using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddonTemplate;
using AddonTemplate.Utility;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;
using SharpDX;
using Gapcloser = EloBuddy.SDK.Events.Gapcloser;
using AddonTemplate.Logic;

namespace Aka_s_Vayne_reworked
{
    internal class Events
    {
        private static int currentSkin = 0;
        private static bool bought = false;
        private static int ticks = 0;

        public static AIHeroClient myHero
        {
            get { return ObjectManager.Player; }
        }

        public static bool VayneUltiIsActive { get; set; }

        public static SpellSlot FlashSlot;

        private static float lastaa, lastaaclick;

        private static bool stopmove;

        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }

        public static void Gapcloser_OnGapCloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (sender == null || sender.IsAlly || !Program.MiscMenu["GapcloseE"].Cast<CheckBox>().CurrentValue) return;

            if ((sender.IsAttackingPlayer || e.End.Distance(_Player) <= 70))
            {
                Program.E.Cast(sender);
            }

        }

        public static void GameObject_OnCreate(GameObject sender, EventArgs args)
        {
            var rengar = EntityManager.Heroes.Enemies.FirstOrDefault(a => a.Hero == Champion.Rengar);
            if (Program.MiscMenu["AntiRengar"].Cast<CheckBox>().CurrentValue && sender.Name == "Rengar_LeapSound.troy" &&
                ObjectManager.Player.Distance(Player.Instance.Position) <= Program.E.Range && rengar != null)
            {
                Program.E.Cast(rengar);
                Console.WriteLine("fuck rengar");
            }
        }

        public static void ObjAiBaseOnOnBasicAttack(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!(sender is AIHeroClient)) return;
            var target = (AIHeroClient) sender;
            if (Program.MiscMenu["AntiKalista"].Cast<CheckBox>().CurrentValue && target.IsEnemy &&
                target.Hero == Champion.Kalista && Program.Q.IsReady())
            {
                var pos = (_Player.Position.Extend(Game.CursorPos, 300).Distance(target) <=
                           _Player.GetAutoAttackRange(target) &&
                           _Player.Position.Extend(Game.CursorPos, 300).Distance(target) > 100
                    ? Game.CursorPos
                    : (_Player.Position.Extend(target.Position, 300).Distance(target) < 100)
                        ? target.Position
                        : new Vector3());

                if (Extensions.IsValid(pos))
                {
                    Player.CastSpell(SpellSlot.Q, pos);
                }
            }
        }

        public static void Obj_AI_Base_OnBuffGain(Obj_AI_Base sender, Obj_AI_BaseBuffGainEventArgs args)
        {
            if (sender.IsMe && args.Buff.Name == "vaynetumblebonus")
            {
                lastaa = 0;
            }

            if (!sender.IsMe) return;

            if (args.Buff.Type == BuffType.Taunt && Program.ItemMenu["Taunt"].Cast<CheckBox>().CurrentValue)
            {
                DoQSS();
            }
            if (args.Buff.Type == BuffType.Stun && Program.ItemMenu["Stun"].Cast<CheckBox>().CurrentValue)
            {
                DoQSS();
            }
            if (args.Buff.Type == BuffType.Snare && Program.ItemMenu["Snare"].Cast<CheckBox>().CurrentValue)
            {
                DoQSS();
            }
            if (args.Buff.Type == BuffType.Polymorph && Program.ItemMenu["Polymorph"].Cast<CheckBox>().CurrentValue)
            {
                DoQSS();
            }
            if (args.Buff.Type == BuffType.Blind && Program.ItemMenu["Blind"].Cast<CheckBox>().CurrentValue)
            {
                DoQSS();
            }
            if (args.Buff.Type == BuffType.Flee && Program.ItemMenu["Fear"].Cast<CheckBox>().CurrentValue)
            {
                DoQSS();
            }
            if (args.Buff.Type == BuffType.Charm && Program.ItemMenu["Charm"].Cast<CheckBox>().CurrentValue)
            {
                DoQSS();
            }
            if (args.Buff.Type == BuffType.Suppression && Program.ItemMenu["Suppression"].Cast<CheckBox>().CurrentValue)
            {
                DoQSS();
            }
            if (args.Buff.Type == BuffType.Silence && Program.ItemMenu["Silence"].Cast<CheckBox>().CurrentValue)
            {
                DoQSS();
            }
            if (args.Buff.Name == "zedulttargetmark")
            {
                UltQSS();
            }
            if (args.Buff.Name == "VladimirHemoplague")
            {
                UltQSS();
            }
            if (args.Buff.Name == "FizzMarinerDoom")
            {
                UltQSS();
            }
            if (args.Buff.Name == "MordekaiserChildrenOfTheGrave")
            {
                UltQSS();
            }
            if (args.Buff.Name == "PoppyDiplomaticImmunity")
            {
                UltQSS();
            }

        }

        private static void DoQSS()
        {
            if (Program.Qss.IsOwned() && Program.Qss.IsReady() && _Player.CountEnemiesInRange(1800) > 0)
            {
                Core.DelayAction(() => Program.Qss.Cast(), Program.ItemMenu["delay"].Cast<Slider>().CurrentValue);
            }

            if (Program.Mercurial.IsOwned() && Program.Mercurial.IsReady() && _Player.CountEnemiesInRange(1800) > 0)
            {
                Core.DelayAction(() => Program.Mercurial.Cast(), Program.ItemMenu["delay"].Cast<Slider>().CurrentValue);
            }
        }

        private static void UltQSS()
        {
            if (Program.Qss.IsOwned() && Program.Qss.IsReady())
            {
                Core.DelayAction(() => Program.Qss.Cast(), Program.ItemMenu["delay"].Cast<Slider>().CurrentValue);
            }

            if (Program.Mercurial.IsOwned() && Program.Mercurial.IsReady())
            {
                Core.DelayAction(() => Program.Mercurial.Cast(), Program.ItemMenu["delay"].Cast<Slider>().CurrentValue);
            }
        }

        public static void Interrupter_OnInterruptableSpell(Obj_AI_Base sender,
            Interrupter.InterruptableSpellEventArgs e)
        {
            if (!Program.MiscMenu["InterruptE"].Cast<CheckBox>().CurrentValue) return;
            var dangerLevel =
                new[]
                {
                    DangerLevel.Low, DangerLevel.Medium,
                    DangerLevel.High,
                }[Program.MiscMenu["dangerLevel"].Cast<Slider>().CurrentValue - 1];

            if (dangerLevel == DangerLevel.Medium && e.DangerLevel.HasFlag(DangerLevel.High) ||
                dangerLevel == DangerLevel.Low && e.DangerLevel.HasFlag(DangerLevel.High) &&
                e.DangerLevel.HasFlag(DangerLevel.Medium))
                return;

            if (Extensions.IsValidTarget(e.Sender))
            {
                Program.E.Cast(e.Sender);
            }
        }

        public static void OnProcessSpell(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!sender.IsMe) return;
            if (args.SData.Name.ToLower().Contains("vaynetumble"))
            {
                Core.DelayAction(Orbwalker.ResetAutoAttack, 250);
            }

            if (!sender.IsMe) return;
            var mousePos = myHero.Position.Extend2(Game.CursorPos, Program.Q.Range);
            if (args.SData.Name.ToLower().Contains("attack"))
            {
                Core.DelayAction(Orbwalker.ResetAutoAttack, 250);
            }

            if (sender is AIHeroClient)
            {
                var pant = (AIHeroClient) sender;
                if (pant.IsValidTarget(myHero.GetAutoAttackRange()) && pant.ChampionName == "Pantheon" &&
                    pant.GetSpellSlotFromName(args.SData.Name) == SpellSlot.W)
                {
                    if (Program.MiscMenu["AntiPanth"].Cast<CheckBox>().CurrentValue && args.Target.IsMe)
                    {
                        if (pant.IsValidTarget(Program.E.Range))
                        {
                            Program.E.Cast(pant);
                        }
                    }
                }
            }
            if (args.SData.Name.ToLower() == "zedult" && args.Target.IsMe)
            {
                if (Item.CanUseItem(3140))
                {
                    Core.DelayAction(() => { Item.UseItem(3140); }, 1000);
                }
                else if (Item.CanUseItem(3139))
                {
                    Core.DelayAction(() => { Item.UseItem(3139); }, 1000);
                }
            }
        }


        public static void Obj_AI_Base_OnBasicAttack(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsMe)
            {
                stopmove = false;
                lastaa = Game.Time*1000;
            }

        }

        public static void Orbwalker_OnPostAttack(AttackableUnit target, EventArgs args)
        {
            if (Program.CondemnMenu["condemnmethod1"].Cast<CheckBox>().CurrentValue && Program.E.IsReady())
            {
                ELogic.Condemn1();
            }
            if (Program.CondemnMenu["condemnmethod2"].Cast<CheckBox>().CurrentValue && Program.E.IsReady())
            {
                ELogic.Condemn2();
            }
            if (Program.CondemnMenu["condemnmethod3"].Cast<CheckBox>().CurrentValue && Program.E.IsReady())
            {
                ELogic.Condemn3();
            }
        }

        public static bool AfterAttack
        {
            get
            {
                if (Game.Time*1000 <
                    lastaa + ObjectManager.Player.AttackDelay*1000 - ObjectManager.Player.AttackDelay*1000/1.5 &&
                    Game.Time*1000 > lastaa + ObjectManager.Player.AttackCastDelay*1000)
                {
                    return true;
                }
                return false;
            }
        }

        public static bool BeforeAttack
        {
            get
            {
                if (Game.Time*1000 >
                    lastaa + ObjectManager.Player.AttackDelay*1000 - ObjectManager.Player.AttackDelay*1000/2 &&
                    Game.Time*1000 <
                    lastaa + ObjectManager.Player.AttackDelay*1000 - ObjectManager.Player.AttackDelay*1000/4)
                {
                    return true;
                }
                return false;
            }
        }

        public static void Player_OnIssueOrder(Obj_AI_Base sender, PlayerIssueOrderEventArgs args)
        {
            if (sender.IsMe && args.Order.HasFlag(GameObjectOrder.AttackUnit))
            {
                lastaaclick = Game.Time*1000;
            }

            if (sender.IsMe
                && (args.Order == GameObjectOrder.AttackUnit || args.Order == GameObjectOrder.AttackTo)
                &&
                (Program.ComboMenu["RnoAA"].Cast<CheckBox>().CurrentValue &&
                 ObjectManager.Player.CountEnemiesInRange(1000f) >
                 Program.ComboMenu["RnoAAs"].Cast<Slider>().CurrentValue)
                && UltActive() || ObjectManager.Player.HasBuffOfType(BuffType.Invisibility)
                && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                args.Process = false;
            }
        }

        public static bool UltActive()
        {
            return (ObjectManager.Player.HasBuff("vaynetumblefade") && !ObjectManager.Player.UnderTurret());
        }

        public static void Game_OnTick(EventArgs args)
        {
            {
                heal();

                if (Program.MechanicMenu["autobuy"].Cast<CheckBox>().CurrentValue)
                {
                    autoBuy();
                }

                if (Program.MechanicMenu["autolvl"].Cast<CheckBox>().CurrentValue)
                {
                    LevelUpSpells();
                }

                if (Program.MechanicMenu["skinhack"].Cast<CheckBox>().CurrentValue)
                {
                    //skinChanger();
                }

                if (Program.MechanicMenu["insece"].Cast<KeyBind>().CurrentValue)
                {
                    Insec();
                }

                var target = TargetSelector.GetTarget((int) ObjectManager.Player.GetAutoAttackRange() + 300,
                    DamageType.Physical);
                EloBuddyOrbDisabler();

                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
                {
                    if (AfterAttack && Program.ComboMenu["UseQa"].Cast<CheckBox>().CurrentValue)
                    {
                        if (target == null) return;
                        var mode = Program.ComboMenu["Qmode"].Cast<Slider>().CurrentValue;
                        var tumblePosition = Game.CursorPos;
                        switch (mode)
                        {
                            case 2:
                                tumblePosition = target.GetTumblePos();
                                break;
                            case 1:
                                tumblePosition = Game.CursorPos;
                                break;
                        }
                        QLogic.Cast(tumblePosition);
                    }
                    if (BeforeAttack && Program.ComboMenu["UseQb"].Cast<CheckBox>().CurrentValue)
                    {
                        if (target == null) return;
                        var mode = Program.ComboMenu["Qmode"].Cast<Slider>().CurrentValue;
                        var tumblePosition = Game.CursorPos;
                        switch (mode)
                        {
                            case 2:
                                tumblePosition = target.GetTumblePos();
                                break;
                            case 1:
                                tumblePosition = Game.CursorPos;
                                break;
                        }
                        QLogic.Cast(tumblePosition);
                    }
                    if (stopmove && Game.Time*1000 > lastaaclick + ObjectManager.Player.AttackCastDelay*1000)
                    {
                        stopmove = false;
                    }
                    if (!stopmove)
                    {
                        if (Game.Time*1000 >
                            lastaa + ObjectManager.Player.AttackCastDelay*1000 - Game.Ping/2 +
                            Program.ComboMenu["AACancel"].Cast<Slider>().CurrentValue)
                        {
                            Player.IssueOrder(GameObjectOrder.MoveTo, Game.CursorPos);
                        }
                    }
                    if (Target != null &&
                        Game.Time*1000 > lastaa + ObjectManager.Player.AttackDelay*1000 - Game.Ping/2*4.3)
                    {
                        stopmove = true;
                        Player.IssueOrder(GameObjectOrder.AttackUnit, Target);
                    }
                    if (Program.ItemMenu["botrk"].Cast<CheckBox>().CurrentValue && Target != null &&
                        (Target.Distance(ObjectManager.Player) > 500f ||
                         (ObjectManager.Player.Health/ObjectManager.Player.MaxHealth)*100 <= 95))
                    {
                        Botrk(Target);
                    }
                    if (Program.ItemMenu["you"].Cast<CheckBox>().CurrentValue && Target != null &&
                        (Target.Distance(ObjectManager.Player) > Program.ItemMenu["yous"].Cast<Slider>().CurrentValue))
                    {
                        You();
                    }

                }
                var positions = ELogic.GetRotatedFlashPositions();

                foreach (var p in positions)
                {
                    var condemnUnit = ELogic.CondemnCheck(p);
                    if (condemnUnit != null && Program.MechanicMenu["flashe"].Cast<KeyBind>().CurrentValue)
                    {
                        Program.E.Cast(condemnUnit);

                        ObjectManager.Player.Spellbook.CastSpell(FlashSlot, p);

                    }
                }

                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
                {
                    Program.Harass();
                }

                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
                {
                    Program.JungleClear();
                }

                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
                {
                    Program.Flee();
                }
                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
                {
                    Program.Combo();
                }


                if (Game.MapId == GameMapId.SummonersRift)
                {
                    if (myHero.IsInShopRange() && Program.MiscMenu["autobuy"].Cast<CheckBox>().CurrentValue &&
                        myHero.Level > 6 && Item.HasItem((int) ItemId.Warding_Totem_Trinket))
                    {
                        Shop.BuyItem(ItemId.Scrying_Orb_Trinket);
                    }
                    if (myHero.IsInShopRange() && Program.MiscMenu["autobuy"].Cast<CheckBox>().CurrentValue &&
                        !Item.HasItem((int) ItemId.Oracles_Lens_Trinket, myHero) && myHero.Level > 6 &&
                        EntityManager.Heroes.Enemies.Any(
                            h =>
                                h.BaseSkinName == "Rengar" || h.BaseSkinName == "Talon" ||
                                h.BaseSkinName == "Vayne"))
                    {
                        Shop.BuyItem(ItemId.Sweeping_Lens_Trinket);
                    }
                    if (myHero.IsInShopRange() && Program.MiscMenu["autobuy"].Cast<CheckBox>().CurrentValue &&
                        myHero.Level >= 9 && Item.HasItem((int) ItemId.Sweeping_Lens_Trinket))
                    {
                        Shop.BuyItem(ItemId.Oracles_Lens_Trinket);
                    }
                }

                if (Program.ComboMenu["focusw"].Cast<CheckBox>().CurrentValue)
                {
                    if (FocusWTarget == null && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo) ||
                        Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
                    {
                        return;
                    }
                    if (FocusWTarget.IsValidTarget(myHero.GetAutoAttackRange()) && !FocusWTarget.IsDead &&
                        Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo) ||
                        Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
                    {
                        TargetSelector2.GetPriority(FocusWTarget);
                    }
                    else
                    {
                        TargetSelector2.GetPriority(
                            TargetSelector2.GetTarget(myHero.AttackRange, TargetSelector2.DamageType.Physical));
                    }
                }


            }
        }

        private static bool CanUseBotrk
        {
            get
            {
                if (Game.Time*1000 >=
                    lastaa + ObjectManager.Player.AttackDelay*1000 - ObjectManager.Player.AttackDelay*1000/1.5 &&
                    Game.Time*1000 <
                    lastaa + ObjectManager.Player.AttackDelay*1000 - ObjectManager.Player.AttackDelay*1000/1.7 &&
                    Game.Time*1000 > lastaa + ObjectManager.Player.AttackCastDelay*1000)
                {
                    return true;
                }
                return false;
            }
        }

        private static
            void EloBuddyOrbDisabler()

        {
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass) ||
                Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear) ||
                Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit) ||
                Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear) ||
                Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
            {
                if (Orbwalker.DisableAttacking)
                {
                    Orbwalker.DisableAttacking = false;
                }
                if (Orbwalker.DisableMovement)
                {
                    Orbwalker.DisableMovement = false;
                }
            }
            else
            {
                if (!Orbwalker.DisableAttacking)
                {
                    Orbwalker.DisableAttacking = true;
                }
                if (!Orbwalker.DisableMovement)
                {
                    Orbwalker.DisableMovement = true;
                }
            }
        }

        public static AIHeroClient FocusWTarget
        {
            get
            {
                return ObjectManager.Get<AIHeroClient>()
                    .Where(
                        enemy =>
                            !enemy.IsDead &&
                            enemy.IsValidTarget((Program.Q.IsReady() ? Program.Q.Range : 0) + myHero.AttackRange + 300))
                    .FirstOrDefault(
                        enemy => enemy.Buffs.Any(buff => buff.Name == "vaynesilvereddebuff" && buff.Count > 0));
            }
        }

        public static void Obj_AI_Base_OnSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!sender.IsMe) return;
            var target = (Obj_AI_Base) args.Target;

            if ((Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear) &&
                 Program.LaneClearMenu["LCQ"].Cast<CheckBox>().CurrentValue) &&
                Program.Q.IsReady())
            {
                if (Orbwalker.CanAutoAttack)
                {
                    return;
                }
                foreach (var minion in EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy,
                    Player.Instance.ServerPosition, ObjectManager.Player.GetAutoAttackRange()))
                {
                    if (minion == null) return;
                    var dmg = Player.Instance.GetSpellDamage(minion, SpellSlot.Q) +
                              Player.Instance.GetAutoAttackDamage(minion);
                    if (Prediction.Health.GetPrediction(minion, (int) (Player.Instance.AttackDelay*1000)) <= dmg/2 &&
                        (Orbwalker.LastTarget == null || Orbwalker.LastTarget.NetworkId != minion.NetworkId))
                    {

                        Player.CastSpell(SpellSlot.Q, Game.CursorPos);
                    }
                }
            }
            var LastHitE = myHero;

            foreach (
                var Etarget in
                    EntityManager.Heroes.Enemies.Where(
                        Etarget => Etarget.IsValidTarget(Program.E.Range) && Etarget.Path.Count() < 2))
            {
                if (Program.ComboMenu["Ekill"].Cast<CheckBox>().CurrentValue && Program.E.IsReady() &&
                    myHero.CountEnemiesInRange2(600) <= 1)
                {
                    var dmgE = myHero.GetSpellDamage2(Etarget, SpellSlot.E);
                    if (dmgE > Etarget.Health ||
                        (Damages.WTarget(Etarget) == 2 && dmgE + Damages.Wdmg(Etarget) > Etarget.Health))
                    {
                        LastHitE = Etarget;

                    }
                }

                if (LastHitE != myHero)
                {
                    Program.E.Cast(LastHitE);
                }
            }

            if (sender.Spellbook.Owner.IsMe)
            {
                if (args.Slot == SpellSlot.Q)
                {
                    if (QLogic.TumbleOrderPos != Vector3.Zero)
                    {
                        if (QLogic.TumbleOrderPos.IsDangerousPosition())
                        {
                            QLogic.TumbleOrderPos = Vector3.Zero;
                            args.Process = false;
                        }
                        else
                        {
                            QLogic.TumbleOrderPos = Vector3.Zero;
                        }
                    }
                }
            }

        }

        private static AIHeroClient Target
        {
            get
            {
                foreach (var unit in EntityManager.Heroes.Enemies.
                    OrderBy(
                        x =>
                            x.Health*(x.Armor/(x.Armor + 100)) - x.TotalAttackDamage*x.AttackSpeedMod -
                            x.TotalMagicalDamage).Where(x =>
                                Extensions.IsValidTarget(x,
                                    ObjectManager.Player.AttackRange + ObjectManager.Player.BoundingRadius +
                                    x.BoundingRadius)
                                && x.Health > 0
                                && !x.IsDead
                                && x.IsVisible
                                && !x.IsZombie
                                && x.IsTargetable
                                && !x.HasBuff("JudicatorIntervention") //kayle R
                                && !x.HasBuff("AlphaStrike") //Master Yi Q
                                && !x.HasBuff("zhonyasringshield") //zhonya
                                && !x.HasBuff("VladimirSanguinePool") //vladimir W
                                && !x.HasBuff("ChronoShift") //zilean R
                                && !x.HasBuff("yorickrazombie") //yorick R
                                && !x.HasBuff("mordekaisercotgself") //mordekaiser R
                                && !x.HasBuff("UndyingRage") //tryndamere R
                                && !x.HasBuff("sionpassivezombie") //sion Passive
                                && !x.HasBuff("elisespidere") //elise not visible
                                && !x.HasBuff("KarthusDeathDefiedBuff") //karthus passive
                                && !x.HasBuff("kogmawicathiansurprise") //kog'maw passive
                                && !x.HasBuff("zyrapqueenofthorns") //zyra passive
                                && !x.HasBuff("monkeykingdecoystealth") //wukong W not visible
                                && !x.HasBuff("JaxCounterStrike") //Jax E
                                && !x.HasBuff("Deceive") //Shaco not visible
                                && !ObjectManager.Player.HasBuff("BlindingDart") //Me Teemo Q
                                && !x.HasBuff("camouflagestealth") //Teemo not visible
                                && !x.HasBuff("khazixrstealth") //Kha'Zix not visible
                                && !x.HasBuff("evelynnstealthmarker") //Evelynn not visible
                                && !x.HasBuff("akaliwstealth"))) //Akali not visible


                {
                    return unit;
                }
                return null;
            }

        }

        private static void Botrk(Obj_AI_Base unit)
        {
            if (Item.HasItem(3144) && Item.CanUseItem(3144) && CanUseBotrk)
                Item.UseItem(3144, unit);
            if (Item.HasItem(3153) && Item.CanUseItem(3153) && CanUseBotrk)
                Item.UseItem(3153, unit);
        }

        private static void You()
        {
            if (Item.HasItem(3142) && Item.CanUseItem(3142))
                Item.UseItem(3142);
        }

        private static void skinChanger()
        {
            if (Program.MechanicMenu["skinId"].Cast<Slider>().CurrentValue != currentSkin)
            {
                Player.Instance.SetSkinId(Program.MiscMenu["skinId"].Cast<Slider>().CurrentValue);
                currentSkin = Program.MiscMenu["skinId"].Cast<Slider>().CurrentValue;
            }
        }

        private static void autoBuy()
        {

            if (bought || ticks/Game.TicksPerSecond < 3)
            {
                ticks++;
                return;
            }

            bought = true;
            if (Program.MechanicMenu["autobuy"].Cast<CheckBox>().CurrentValue)
            {
                if (Game.MapId == GameMapId.SummonersRift)
                {
                    Shop.BuyItem(ItemId.Dorans_Blade);
                    Shop.BuyItem(ItemId.Health_Potion);
                    Shop.BuyItem(ItemId.Warding_Totem_Trinket);
                }
            }
        }

        private static void heal()
        {

            if (Program.ItemMenu["heal"].Cast<CheckBox>().CurrentValue &&
                _Player.HealthPercent <= Program.ItemMenu["hp"].Cast<Slider>().CurrentValue)
            {
                Program.Heal.Cast();
            }
            foreach (
                var ally in EntityManager.Heroes.Allies.Where(a => !a.IsDead))
            {
                if (Program.ItemMenu["healally"].Cast<CheckBox>().CurrentValue && _Player.Position.Distance(ally) < 600 &&
                    ally.HealthPercent <= Program.ItemMenu["hpally"].Cast<Slider>().CurrentValue)
                {
                    Program.Heal.Cast();
                }
            }

        }

        private static void Insec()
        {
            var mode = (Program.MechanicMenu["insecmodes"].Cast<Slider>().CurrentValue);
            var target = TargetSelector.GetTarget(1000, DamageType.Magical);
            if (target != null)
            {
                //var targetfuturepos = Prediction.GetPrediction(target, 0.1f).UnitPosition;
                bool caninsec = Player.Instance.Distance(target) <= 400;
                switch (mode)
                {
                    case 1:
                        var hero =
                            HeroManager.Allies.Where(x => !x.IsMe && !x.IsDead)
                                .OrderByDescending(x => x.Distance(Player.Instance.Position))
                                .LastOrDefault();
                        if (hero != null && caninsec &&
                            Player.Instance.ServerPosition.Distance(hero.Position) + 100 >=
                            target.Distance(hero.Position))
                        {
                            var ePred = Program.E2.GetPrediction(target);
                            int pushDist = 550;
                            for (int i = 0; i < pushDist; i += (int)target.BoundingRadius)
                            {
                                Vector3 loc3 = ePred.UnitPosition.To2D().Extend(ELogic.GetFlashPos(target, true).To2D(), -i).To3D();
                                if (loc3.Distance(hero) < hero.Position.Distance(target))
                                {
                                    ObjectManager.Player.Spellbook.CastSpell(FlashSlot, ELogic.GetFlashPos(target, true));
                                    Program.E.Cast(target);
                                }
                            }
                        }
                        break;
                    case 2:
                        var turret =
                            ObjectManager.Get<Obj_AI_Turret>()
                                .Where(x => x.IsAlly && !x.IsDead)
                                .OrderByDescending(x => x.Distance(Player.Instance.Position))
                                .LastOrDefault();
                        if (turret != null && caninsec &&
                            Player.Instance.ServerPosition.Distance(turret.Position) + 100 >=
                            target.Distance(turret.Position))
                        {
                            var ePred = Program.E2.GetPrediction(target);
                            int pushDist = 550;
                            for (int i = 0; i < pushDist; i += (int)target.BoundingRadius)
                            {
                                Vector3 loc3 = ePred.UnitPosition.To2D().Extend(ELogic.GetFlashPos(target, true).To2D(), -i).To3D();
                                if (loc3.Distance(turret) < turret.Position.Distance(target))
                                {
                                    ObjectManager.Player.Spellbook.CastSpell(FlashSlot, ELogic.GetFlashPos(target, true));
                                    Program.E.Cast(target);
                                }
                            }
                        }
                        break;
                    case 3:
                        if (caninsec &&
                            Player.Instance.ServerPosition.Distance(Game.CursorPos) + 100 >=
                            target.Distance(Game.CursorPos))
                        {
                            var ePred = Program.E2.GetPrediction(target);
                            int pushDist = 550;
                            for (int i = 0; i < pushDist; i += (int)target.BoundingRadius)
                            {
                                Vector3 loc3 = ePred.UnitPosition.To2D().Extend(ELogic.GetFlashPos(target, true).To2D(), -i).To3D();
                                if (loc3.Distance(Game.CursorPos) < Game.CursorPos.Distance(target))
                                {
                                    ObjectManager.Player.Spellbook.CastSpell(FlashSlot, ELogic.GetFlashPos(target, true));
                                    Program.E.Cast(target);
                                }
                            }
                        }
                        break;

                }
            }
        }
        // iRaxe <3
        private static void LevelUpSpells()
        {
            var qL = ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Q).Level + Program.QOff;
            var wL = ObjectManager.Player.Spellbook.GetSpell(SpellSlot.W).Level + Program.WOff;
            var eL = ObjectManager.Player.Spellbook.GetSpell(SpellSlot.E).Level + Program.EOff;
            var rL = ObjectManager.Player.Spellbook.GetSpell(SpellSlot.R).Level + Program.ROff;
            if (qL + wL + eL + rL >= ObjectManager.Player.Level) return;
            int[] level = { 0, 0, 0, 0 };
            for (var i = 0; i < ObjectManager.Player.Level; i++)
            {
                level[Program.AbilitySequence[i] - 1] = level[Program.AbilitySequence[i] - 1] + 1;
            }
            if (qL < level[0]) ObjectManager.Player.Spellbook.LevelSpell(SpellSlot.Q);
            if (wL < level[1]) ObjectManager.Player.Spellbook.LevelSpell(SpellSlot.W);
            if (eL < level[2]) ObjectManager.Player.Spellbook.LevelSpell(SpellSlot.E);
            if (rL < level[3]) ObjectManager.Player.Spellbook.LevelSpell(SpellSlot.R);
        }

    }
}

