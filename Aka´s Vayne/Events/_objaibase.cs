
using System;
using System.Linq;
using AddonTemplate;
using AddonTemplate.Logic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

namespace Aka_s_Vayne_reworked.Events
{
    internal class _objaibase
    {
        private static float LastCondemnTick = 0f;

        public static void AutoAttack(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!(sender is AIHeroClient)) return;
            var target = (AIHeroClient) sender;
            if (MenuManager.MiscMenu["AntiKalista"].Cast<CheckBox>().CurrentValue && target.IsEnemy &&
                target.Hero == Champion.Kalista && Program.Q.IsReady())
            {
                var pos = (Variables._Player.Position.Extend(Game.CursorPos, 300).Distance(target) <=
                           Variables._Player.GetAutoAttackRange(target) &&
                           Variables._Player.Position.Extend(Game.CursorPos, 300).Distance(target) > 100
                    ? Game.CursorPos
                    : (Variables._Player.Position.Extend(target.Position, 300).Distance(target) < 100)
                        ? target.Position
                        : new Vector3());

                if (Extensions.IsValid(pos))
                {
                    Player.CastSpell(SpellSlot.Q, pos);
                }
            }
        }

        public static void BuffGain(Obj_AI_Base sender, Obj_AI_BaseBuffGainEventArgs args)
        {
            if (sender.IsMe && args.Buff.Name == "vaynetumblebonus")
            {
                Variables.lastaa = 0;
            }

            if (!sender.IsMe) return;

            if (args.Buff.Type == BuffType.Taunt && MenuManager.ItemMenu["Taunt"].Cast<CheckBox>().CurrentValue)
            {
                Functions.Events._objaibase.DoQSS();
            }
            if (args.Buff.Type == BuffType.Stun && MenuManager.ItemMenu["Stun"].Cast<CheckBox>().CurrentValue)
            {
                Functions.Events._objaibase.DoQSS();
            }
            if (args.Buff.Type == BuffType.Snare && MenuManager.ItemMenu["Snare"].Cast<CheckBox>().CurrentValue)
            {
                Functions.Events._objaibase.DoQSS();
            }
            if (args.Buff.Type == BuffType.Polymorph && MenuManager.ItemMenu["Polymorph"].Cast<CheckBox>().CurrentValue)
            {
                Functions.Events._objaibase.DoQSS();
            }
            if (args.Buff.Type == BuffType.Blind && MenuManager.ItemMenu["Blind"].Cast<CheckBox>().CurrentValue)
            {
                Functions.Events._objaibase.DoQSS();
            }
            if (args.Buff.Type == BuffType.Flee && MenuManager.ItemMenu["Fear"].Cast<CheckBox>().CurrentValue)
            {
                Functions.Events._objaibase.DoQSS();
            }
            if (args.Buff.Type == BuffType.Charm && MenuManager.ItemMenu["Charm"].Cast<CheckBox>().CurrentValue)
            {
                Functions.Events._objaibase.DoQSS();
            }
            if (args.Buff.Type == BuffType.Suppression && MenuManager.ItemMenu["Suppression"].Cast<CheckBox>().CurrentValue)
            {
                Functions.Events._objaibase.DoQSS();
            }
            if (args.Buff.Type == BuffType.Silence && MenuManager.ItemMenu["Silence"].Cast<CheckBox>().CurrentValue)
            {
                Functions.Events._objaibase.DoQSS();
            }
            if (args.Buff.Name == "zedulttargetmark")
            {
                Functions.Events._objaibase.UltQSS();
            }
            if (args.Buff.Name == "VladimirHemoplague")
            {
                Functions.Events._objaibase.UltQSS();
            }
            if (args.Buff.Name == "FizzMarinerDoom")
            {
                Functions.Events._objaibase.UltQSS();
            }
            if (args.Buff.Name == "MordekaiserChildrenOfTheGrave")
            {
                Functions.Events._objaibase.UltQSS();
            }
            if (args.Buff.Name == "PoppyDiplomaticImmunity")
            {
                Functions.Events._objaibase.UltQSS();
            }

        }

        public static void ProcessSpell(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!sender.IsMe) return;
            if (args.SData.Name.ToLower().Contains("VayneTumble"))
            {
                Core.DelayAction(Orbwalker.ResetAutoAttack, 250);
            }
            var mousePos = Variables._Player.Position.Extend(Game.CursorPos, Program.Q.Range);
            if (args.SData.Name.ToLower().Contains("Attack"))
            {
                Core.DelayAction(Orbwalker.ResetAutoAttack, 250);
            }

            if (sender is AIHeroClient)
            {
                var pant = (AIHeroClient) sender;
                if (pant.IsValidTarget(Variables._Player.GetAutoAttackRange()) && pant.ChampionName == "Pantheon" &&
                    pant.GetSpellSlotFromName(args.SData.Name) == SpellSlot.W)
                {
                    if (MenuManager.MiscMenu["AntiPanth"].Cast<CheckBox>().CurrentValue && args.Target.IsMe)
                    {
                        if (pant.IsValidTarget(Program.E.Range))
                        {
                            Program.E.Cast(pant);
                        }
                    }
                }
            }
        }

        public static void AutoAttack2(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsMe)
            {
                Variables.stopmove = false;
                Variables.lastaa = Game.Time*1000;
            }
        }

        public static void SpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!sender.IsMe) return;
            if ((Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear) &&
                 MenuManager.LaneClearMenu["LCQ"].Cast<CheckBox>().CurrentValue) &&
                Program.Q.IsReady())
            {
                if (Orbwalker.CanAutoAttack)
                {
                    return;
                }
                foreach (var minion in EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy,
                    Variables._Player.ServerPosition, Variables._Player.GetAutoAttackRange()))
                {
                    if (minion == null) return;
                    var dmg = Variables._Player.GetSpellDamage(minion, SpellSlot.Q) +
                              Variables._Player.GetAutoAttackDamage(minion);
                    if (Prediction.Health.GetPrediction(minion, (int)(Variables._Player.AttackDelay * 1000)) <= dmg / 2 &&
                        (Orbwalker.LastTarget == null || Orbwalker.LastTarget.NetworkId != minion.NetworkId))
                    {

                        Player.CastSpell(SpellSlot.Q, Game.CursorPos);
                    }
                }
            }
            var LastHitE = Variables._Player;

            foreach (
                var Etarget in
                    EntityManager.Heroes.Enemies.Where(
                        Etarget => Etarget.IsValidTarget(Program.E.Range) && Etarget.Path.Count() < 2))
            {
                if (MenuManager.ComboMenu["Ekill"].Cast<CheckBox>().CurrentValue && Program.E.IsReady() &&
                    Variables._Player.CountEnemiesInRange(600) <= 1)
                {
                    var dmgE = Variables._Player.GetSpellDamage(Etarget, SpellSlot.E);
                    if (dmgE > Etarget.Health ||
                        (Damages.WTarget(Etarget) == 2 && dmgE + Damages.Wdmg(Etarget) > Etarget.Health))
                    {
                        LastHitE = Etarget;

                    }
                }

                if (LastHitE != Variables._Player)
                {
                    Program.E.Cast(LastHitE);
                }
            }
        }
    }
}
