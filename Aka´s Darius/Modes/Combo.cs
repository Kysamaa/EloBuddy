using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

// Using the config like this makes your life easier, trust me
using Settings = AddonTemplate.Config.Modes.Combo;

namespace AddonTemplate.Modes
{
    public sealed class Combo : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            // Only execute this mode when the orbwalker is on combo mode
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
        }

        public override void Execute()
        {
            var itarget = TargetSelector.GetTarget(1000, DamageType.Physical);
            Items.UseItems(itarget);

            if (Settings.UseE)
            {
                var etarget = TargetSelector.GetTarget(E.Range, DamageType.Physical);
                var eprediction = E.GetPrediction(etarget);
                if (eprediction.HitChance >= HitChance.High)
                {
                    if (E.IsReady() && etarget != null)
                    {
                        E.Cast(eprediction.CastPosition);
                    }
                }
            }

            if (Settings.UseW)
            {
                var target = TargetSelector.GetTarget(W.Range, DamageType.Physical);
                if (W.IsReady() && target != null)
                {
                    W.Cast();
                }
            }

            if (Settings.UseQ)
            {
                var target = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
                    if (Q.IsReady() && target != null)
                    {
                        Q.Cast();
                    }              
            }

            if (Settings.UseR && R.IsReady())
            {
                var target = TargetSelector.GetTarget(E.Range, DamageType.Physical);

                if (target.IsValidTarget(R.Range) && !target.IsZombie && !target.IsInvulnerable && !target.IsDead)
                {
                    int passiveCounter = target.GetBuffCount("dariushemo") <= 0 ? 0 : target.GetBuffCount("dariushemo");
                    if (!target.HasBuffOfType(BuffType.Invulnerability) && !target.HasBuffOfType(BuffType.SpellShield))
                    {
                        if (Damages.RDamage(target, passiveCounter) >=
                            target.Health + Damages.PassiveDmg(target, 1))
                        {
                            if (!target.HasBuffOfType(BuffType.Invulnerability)
                                && !target.HasBuffOfType(BuffType.SpellShield)
                                && !target.HasBuff("kindredrnodeathbuff")
                                && !target.HasUndyingBuff())
                            {
                                R.Cast(target);
                            }
                        }
                    }
                }
            }
        }
    }
}
                
            

        
    



