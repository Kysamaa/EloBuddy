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
                    var pred = Q.GetPrediction(enemy);
                    if (pred.HitChance >= HitChance.High)
                    {
                        Q.Cast(pred.CastPosition);
                        break;
                    }
                }

                if (Settings.KSE && SpellManager.E.IsReady() && Damages.EDamage(enemy) > enemy.Health &&
                    SpellManager.E.IsInRange(enemy))
                {
                    if (enemy.HasBuffOfType(BuffType.SpellImmunity) || enemy.HasBuffOfType(BuffType.SpellShield))
                    {
                        continue;
                    }
                    var pred = E.GetPrediction(enemy);
                    if (pred.HitChance >= HitChance.High)
                    {
                        E.Cast(pred.CastPosition);
                        break;
                    }
                }
                if (Settings.KSR && SpellManager.R.IsReady() && Damages.RDamage(enemy) > enemy.Health &&
     SpellManager.R.IsInRange(enemy))
                {
                    if (enemy.HasBuffOfType(BuffType.SpellImmunity) || enemy.HasBuffOfType(BuffType.SpellShield))
                    {
                        continue;
                    }
                    R.Cast(enemy);
                    break;
                }
            }
        }
    }
}

