using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddonTemplate.Logic;
using AddonTemplate.Utility;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;
using SharpDX;
using Gapcloser = EloBuddy.SDK.Events.Gapcloser;
using Settings = AddonTemplate.Config.Modes.MiscMenu;

namespace AddonTemplate
{
    class Events
    {

        public static AIHeroClient myHero { get { return ObjectManager.Player; } }

        public static bool VayneUltiIsActive { get; set; }

        public static BuffType[] buffs;

        public static SpellSlot FlashSlot;

        static float lastaa, lastaaclick;

        static bool stopmove;

        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }

        public static void Gapcloser_OnGapCloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (sender == null || sender.IsAlly || !Settings.Gapclose) return;

            if ((sender.IsAttackingPlayer || e.End.Distance(_Player) <= 70))
            {
                SpellManager.E.Cast(sender);
            }
            
        }


        public static void GameObject_OnCreate(GameObject sender, EventArgs args)
        {
            var rengar = EntityManager.Heroes.Enemies.FirstOrDefault(a => a.Hero == Champion.Rengar);
            if (Settings.AntiRengar && sender.Name == "Rengar_LeapSound.troy" &&
                ObjectManager.Player.Distance(Player.Instance.Position) <= SpellManager.E.Range && rengar != null)
            {
                SpellManager.E.Cast(rengar);
                Console.WriteLine("fuck rengar");
            }
        }

        public static void ObjAiBaseOnOnBasicAttack(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!(sender is AIHeroClient)) return;
            var target = (AIHeroClient) sender;
            if (Settings.AntiKalista && target.IsEnemy && target.Hero == Champion.Kalista && SpellManager.Q.IsReady())
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
        }


        public static void Interrupter_OnInterruptableSpell(Obj_AI_Base sender,
            Interrupter.InterruptableSpellEventArgs e)
        {
            if (!Settings.InterruptE) return;
            var dangerLevel =
                new[]
                {
                    DangerLevel.Low, DangerLevel.Medium,
                    DangerLevel.High,
                }[Settings.Dangerlvl - 1];

            if (dangerLevel == DangerLevel.Medium && e.DangerLevel.HasFlag(DangerLevel.High) ||
                dangerLevel == DangerLevel.Low && e.DangerLevel.HasFlag(DangerLevel.High) &&
                e.DangerLevel.HasFlag(DangerLevel.Medium))
                return;

            if (Extensions.IsValidTarget(e.Sender))
            {
                SpellManager.E.Cast(e.Sender);
            }
        }

        public static void OnProcessSpell(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!sender.IsMe) return;
            if (args.SData.Name.ToLower().Contains("vaynetumble"))
            {
                Core.DelayAction(Orbwalking.ResetAutoAttackTimer, 250);
            }

            if (!sender.IsMe) return;
            var mousePos = myHero.Position.Extend2(Game.CursorPos, SpellManager.Q.Range);
            if (args.SData.Name.ToLower().Contains("attack"))
            {
                Core.DelayAction(Orbwalking.ResetAutoAttackTimer, 250);
            }

            if (sender is AIHeroClient)
            {
                var pant = (AIHeroClient)sender;
                if (pant.IsValidTarget(myHero.GetAutoAttackRange()) && pant.ChampionName == "Pantheon" && pant.GetSpellSlotFromName(args.SData.Name) == SpellSlot.W)
                {
                    if (Settings.AntiPanth && args.Target.IsMe)
                    {
                        if (pant.IsValidTarget(SpellManager.E.Range))
                        {
                            SpellManager.E.Cast(pant);
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
                lastaa = Game.Time * 1000;
            }

        }

        public static bool AfterAttack
        {
            get
            {
                if (Game.Time * 1000 <
                    lastaa + ObjectManager.Player.AttackDelay * 1000 - ObjectManager.Player.AttackDelay * 1000 / 1.5)
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
                if (Game.Time * 1000 > lastaa + ObjectManager.Player.AttackCastDelay * 1000)
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
                lastaaclick = Game.Time * 1000;
            }

            if (sender.IsMe
     && (args.Order == GameObjectOrder.AttackUnit || args.Order == GameObjectOrder.AttackTo)
     && (Config.Modes.Combo.RnoAA && ObjectManager.Player.CountEnemiesInRange(1000f) > Config.Modes.Combo.RnoAAs)
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

                var target = TargetSelector.GetTarget((int) ObjectManager.Player.GetAutoAttackRange(),
                    DamageType.Physical);
                EloBuddyOrbDisabler();

                if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
                {
                    if (AfterAttack && Config.Modes.Combo.UseQa)
                    {
                        if (Config.Modes.Combo.UseQp)
                        {
                            Player.CastSpell(SpellSlot.Q, QLogic.GetTumblePos(target));
                        }
                        if (Config.Modes.Combo.UseQ)
                        {
                            Player.CastSpell(SpellSlot.Q, Game.CursorPos);
                        }
                    }
                    if (BeforeAttack && Config.Modes.Combo.UseQb)
                    {
                        if (Config.Modes.Combo.UseQp)
                        {
                            Player.CastSpell(SpellSlot.Q, QLogic.GetTumblePos(target));
                        }
                        if (Config.Modes.Combo.UseQ)
                        {
                            Player.CastSpell(SpellSlot.Q, Game.CursorPos);
                        }
                    }
                    if (stopmove && Game.Time*1000 > lastaaclick + ObjectManager.Player.AttackCastDelay*1000)
                    {
                        stopmove = false;
                    }
                    if (!stopmove)
                    {
                        if (Game.Time*1000 >
                            lastaa + ObjectManager.Player.AttackCastDelay*1000 - Game.Ping/2)
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


                    var positions = ELogic.GetRotatedFlashPositions();

                    foreach (var p in positions)
                    {
                        var condemnUnit = ELogic.CondemnCheck(p);
                        if (condemnUnit != null && Config.Modes.Condemn.FlashE)
                        {
                            SpellManager.E.Cast(condemnUnit);

                            ObjectManager.Player.Spellbook.CastSpell(FlashSlot, p);

                        }
                    }

                }
            }

            if (Config.Modes.Items.Qss)
            {
                for (int i = 0; i < buffs.Length; i++)
                {
                    if (myHero.HasBuffOfType(buffs[i]) && Config.Modes.Items.QssActivated && myHero.CountEnemiesInRange(800) > 0)
                    {
                        var delay = Config.Modes.Items.Qssdelay;
                        if (Item.CanUseItem(3140))
                        {
                            Core.DelayAction(() => { Item.UseItem(3140); }, delay);
                        }
                        else if (Item.CanUseItem(3139))
                        {
                            Core.DelayAction(() => { Item.UseItem(3139); }, delay);
                        }
                    }
                }
            }

            if (Game.MapId == GameMapId.SummonersRift)
            {
                if (myHero.IsInShopRange() && Settings.autoBuy &&
                    myHero.Level > 6 && Item.HasItem((int)ItemId.Warding_Totem_Trinket))
                {
                    Shop.BuyItem(ItemId.Scrying_Orb_Trinket);
                }
                if (myHero.IsInShopRange() && Settings.autoBuy &&
                    !Item.HasItem((int)ItemId.Oracles_Lens_Trinket, myHero) && myHero.Level > 6 &&
                    EntityManager.Heroes.Enemies.Any(
                        h =>
                            h.BaseSkinName == "Rengar" || h.BaseSkinName == "Talon" ||
                            h.BaseSkinName == "Vayne"))
                {
                    Shop.BuyItem(ItemId.Sweeping_Lens_Trinket);
                }
                if (myHero.IsInShopRange() && Settings.autoBuy &&
                    myHero.Level >= 9 && Item.HasItem((int)ItemId.Sweeping_Lens_Trinket))
                {
                    Shop.BuyItem(ItemId.Oracles_Lens_Trinket);
                }
            }

            if (Config.Modes.Combo.Focus)
            {
                if (FocusWTarget == null && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo) ||
                    Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
                {
                    return;
                }
                if (FocusWTarget.IsValidTarget(myHero.GetAutoAttackRange()) && !FocusWTarget.IsDead && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo) ||
                    Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
                {
                    TargetSelector2.GetPriority(FocusWTarget);
                    Console.WriteLine("Focus W");
                }
                else
                {
                    TargetSelector2.GetPriority(
                        TargetSelector2.GetTarget(myHero.AttackRange, TargetSelector2.DamageType.Physical));
                }
            }
        
    }

        static void EloBuddyOrbDisabler()
        {
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass) || Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear) || Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit) || Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear) || Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
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
                return ObjectManager.Get<AIHeroClient>().Where(enemy => !enemy.IsDead && enemy.IsValidTarget((SpellManager.Q.IsReady() ? SpellManager.Q.Range : 0) + myHero.AttackRange + 300))
                       .FirstOrDefault(
                           enemy => enemy.Buffs.Any(buff => buff.Name == "vaynesilvereddebuff" && buff.Count > 0));
            }
        }

        public static void Obj_AI_Base_OnSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!sender.IsMe) return;
            var target = (Obj_AI_Base)args.Target;

            if ((Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear) && Config.Modes.LaneClear.UseQ) &&
                    SpellManager.Q.IsReady())
                {
                    var source =
                        EntityManager.MinionsAndMonsters.EnemyMinions
                            .Where(
                                a =>
                                    a.NetworkId != target.NetworkId &&
                                    a.Distance(Player.Instance) < 300 + Player.Instance.GetAutoAttackRange(a) &&
                                    Prediction.Health.GetPrediction(a, (int) Player.Instance.AttackDelay) <
                                    Player.Instance.GetAutoAttackDamage(a, true) + Damages.QDamage(a))
                            .OrderBy(a => a.Health)
                            .FirstOrDefault();

                    if (source == null || Player.Instance.Position.Extend(Game.CursorPos, 300).Distance(source) >
                        Player.Instance.GetAutoAttackRange(source))
                        return;
                    Orbwalker.ForcedTarget = source;
                    Player.CastSpell(SpellSlot.Q,
                        Player.Instance.Position.Extend(Game.CursorPos, 300).Distance(source) <=
                        Player.Instance.GetAutoAttackRange(source)
                            ? Game.CursorPos
                            : source.Position);
                }
            var LastHitE = myHero;

            foreach (var Etarget in EntityManager.Heroes.Enemies.Where(Etarget => Etarget.IsValidTarget(SpellManager.E.Range) && Etarget.Path.Count() < 2))
            {
                if (Config.Modes.Combo.Ekill && SpellManager.E.IsReady() && myHero.CountEnemiesInRange2(600) <= 1)
                {
                    var dmgE = myHero.GetSpellDamage2(Etarget, SpellSlot.E);
                    if (dmgE > Etarget.Health || (Damages.WTarget(Etarget) == 2 && dmgE + Damages.Wdmg(Etarget) > Etarget.Health))
                    {
                        LastHitE = Etarget;

                    }
                }

                if (LastHitE != myHero)
                {
                    SpellManager.E.Cast(LastHitE);
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

        static AIHeroClient Target
        {
            get
            {
                foreach (var unit in EntityManager.Heroes.Enemies.
                    OrderBy(
                        x =>
                            x.Health * (x.Armor / (x.Armor + 100)) - x.TotalAttackDamage * x.AttackSpeedMod -
                            x.TotalMagicalDamage).Where(x =>
                                Extensions.IsValidTarget(x, ObjectManager.Player.AttackRange + ObjectManager.Player.BoundingRadius +
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

    }
    }

