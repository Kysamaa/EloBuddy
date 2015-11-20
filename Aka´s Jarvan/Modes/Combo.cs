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
            if (Settings.UseE)
            {
                var etarget = TargetSelector.GetTarget(E.Range, DamageType.Magical);
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
                    var prediction = Q.GetPrediction(target);
                    if (prediction.HitChance >= HitChance.High)
                    {
                        if (Q.IsReady() && target != null)
                        {
                            Q.Cast(prediction.CastPosition);
                        }


                    }
                }
                if (Settings.UseR && R.IsReady())
                {
                    var target = TargetSelector.GetTarget(R.Range, DamageType.Physical);
                    if (R.IsReady() && target != null)
                    {
                        R.Cast(target);
                    }
                }
            }

        }
    }



