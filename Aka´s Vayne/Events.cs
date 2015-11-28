using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddonTemplate.Logic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;
using SharpDX;
using Settings = AddonTemplate.Config.Modes.MiscMenu;

namespace AddonTemplate
{
    class Events
    {
        static float lastaa, lastaaclick;

        static bool stopmove;

        public static SpellSlot FlashSlot;

        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }

        public static void Gapcloser_OnGapCloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (!Settings.Gapclose) return;
            if (e.End.Distance(_Player) < 200 && sender.IsValidTarget())
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

                if (pos.IsValid())
                {
                    Player.CastSpell(SpellSlot.Q, pos);
                }
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

            if (e.Sender.IsValidTarget())
            {
                SpellManager.E.Cast(e.Sender);
            }
        }

        public static void OnProcessSpell(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!sender.IsMe) return;
            if (args.SData.Name.ToLower().Contains("vaynetumble"))
            {
                Orbwalker.ResetAutoAttack();
            }


        }

        public static void Obj_AI_Base_OnBuffGain(Obj_AI_Base sender, Obj_AI_BaseBuffGainEventArgs args)
        {
            if (sender.IsMe && args.Buff.Name == "vaynetumblebonus")
            {
                lastaa = 0;
            }
        }

        public static void Player_OnIssueOrder(Obj_AI_Base sender, PlayerIssueOrderEventArgs args)
        {
            if (sender.IsMe && args.Order.HasFlag(GameObjectOrder.AttackUnit))
            {
                lastaaclick = Game.Time*1000;
            }
        }

        public static void Game_OnTick(EventArgs args)
        {
            EloBuddyOrbDisabler();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                if (AfterAndBeforeAttack)
                {
                    Player.CastSpell(SpellSlot.Q, Game.CursorPos);
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
                if (Target != null && Game.Time*1000 > lastaa + ObjectManager.Player.AttackDelay*1000 - Game.Ping/2*4.3)
                {
                    stopmove = true;
                    Player.IssueOrder(GameObjectOrder.AttackUnit, Target);
                }
                if (Target != null &&
                    (Target.Distance(ObjectManager.Player) > 500f ||
                     (ObjectManager.Player.Health/ObjectManager.Player.MaxHealth)*100 <= 95))
                {
                    Botrk(Target);
                }
            }

            var positions = ELogic.GetRotatedFlashPositions();

            foreach (var p in positions)
            {
                var condemnUnit = ELogic.CondemnCheck( p);
                if (condemnUnit != null && Config.Modes.Condemn.FlashE)
                {
                    SpellManager.E.Cast(condemnUnit);

                        ObjectManager.Player.Spellbook.CastSpell(FlashSlot, p);
                   
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

        public static bool AfterAndBeforeAttack
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

        public static bool CanUseBotrk
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

        static AIHeroClient Target
        {
            get
            {
                foreach (var unit in EntityManager.Heroes.Enemies.
                    OrderBy(
                        x =>
                            x.Health*(x.Armor/(x.Armor + 100)) - x.TotalAttackDamage*x.AttackSpeedMod -
                            x.TotalMagicalDamage).Where(x =>
                                x.IsValidTarget(ObjectManager.Player.AttackRange + ObjectManager.Player.BoundingRadius +
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

        static void Botrk(Obj_AI_Base unit)
        {
            if (Item.HasItem(3144) && Item.CanUseItem(3144) && CanUseBotrk)
                Item.UseItem(3144, unit);
            if (Item.HasItem(3153) && Item.CanUseItem(3153) && CanUseBotrk)
                Item.UseItem(3153, unit);
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
            }
        }
    }

