using System.Diagnostics;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using Settings = AddonTemplate.Config.Modes.MiscMenu;

namespace AddonTemplate.Modes
{
    public sealed class PermaActive : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            // Since this is permaactive mode, always execute the loop
            return true;
        }

        public override void Execute()
        {

            if (Player.Instance.IsDead)
            {
                return;
            }

            var enemies =
                EntityManager.Heroes.Enemies.Where(
                    e => e.IsEnemy && e.IsVisible && !e.IsDead && !e.IsZombie && !e.IsInvulnerable && e.Health > 0)
                    .ToList();
            foreach (var enemy in enemies)
            {
                if (Settings.KSQ && SpellManager.Q.IsReady() && Damages.QDamage(enemy) > enemy.Health &&
                    SpellManager.Q.IsInRange(enemy))
                {
                    if (enemy.HasBuffOfType(BuffType.SpellImmunity) || enemy.HasBuffOfType(BuffType.SpellShield))
                    {
                        continue;
                    }
                    if (Settings.KSQ)
                    {
                        Q.Cast();
                        break;
                    }
                }

                if (Settings.KSW && SpellManager.W.IsReady() && Damages.WDamage(enemy) > enemy.Health &&
                    SpellManager.W.IsInRange(enemy))
                {
                    if (enemy.HasBuffOfType(BuffType.SpellImmunity) || enemy.HasBuffOfType(BuffType.SpellShield))
                    {
                        continue;
                    }
                    if (Settings.KSW)
                    {
                        W.Cast();
                        break;
                    }
                }
                if (Settings.KSR && R.IsReady())
                {

                    if (enemy.IsValidTarget(R.Range) && !enemy.IsZombie && !enemy.IsInvulnerable && !enemy.IsDead)
                    {
                        int passiveCounter = enemy.GetBuffCount("dariushemo") <= 0
                            ? 0
                            : enemy.GetBuffCount("dariushemo");
                        if (!enemy.HasBuffOfType(BuffType.Invulnerability) && !enemy.HasBuffOfType(BuffType.SpellShield))
                        {
                            if (Damages.RDamage(enemy, passiveCounter) >= enemy.Health + Damages.PassiveDmg(enemy, 1))
                            {
                                if (!enemy.HasBuffOfType(BuffType.Invulnerability)
                                       && !enemy.HasBuffOfType(BuffType.SpellShield)
                                       && !enemy.HasBuff("kindredrnodeathbuff")
                                       && !enemy.HasUndyingBuff())
                                {
                                    R.Cast(enemy);
                                }
                            }
                        }
                    }
                }
                if (Config.Modes.Harass.UseQ && Player.Instance.ManaPercent > Config.Modes.Harass.Mana &&
                    Q.IsReady())
                    {
                        var target = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
                        if (target != null)
                        {
                            Q.Cast();
                        }
                    }
                    if (Config.Modes.Harass.UseE && Player.Instance.ManaPercent > Config.Modes.Harass.Mana &&
                        E.IsReady())
                    {
                        var target = TargetSelector.GetTarget(E.Range, DamageType.Physical);
                        var eprediction = E.GetPrediction(target);
                        if (eprediction.HitChance >= HitChance.High)
                        {
                            if (E.IsReady() && target != null)
                            {
                                E.Cast(eprediction.CastPosition);
                            }
                        }
                    }
                }
            }
        }
    }


