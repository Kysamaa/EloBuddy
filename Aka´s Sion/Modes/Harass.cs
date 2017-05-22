using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

// Using the config like this makes your life easier, trust me
using Settings = AddonTemplate.Config.Modes.Harass;

namespace AddonTemplate.Modes
{
    public sealed class Harass : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            // Only execute this mode when the orbwalker is on harass mode
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass);
        }

        public override void Execute()
        {
            // TODO: Add harass logic here
            // See how I used the Settings.UseQ and Settings.Mana here, this is why I love
            // my way of using the menu in the Config class!
            if (Settings.UseE && Player.Instance.ManaPercent > Settings.Mana && E.IsReady())
            {
                var target = TargetSelector.GetTarget(E.Range, DamageType.Physical);
                var prede = E.GetPrediction(target);
                if (prede.HitChance >= HitChance.Medium)
                {
                    if (target != null)
                    {
                        E.Cast(prede.CastPosition);
                    }
                }
            }
            if (Settings.UseQ && Player.Instance.ManaPercent > Settings.Mana && Q.IsReady())
            {
                var target = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
                var eprediction = Q.GetPrediction(target);
                if (eprediction.HitChance >= HitChance.High)
                {
                    if (target != null)
                    {
                        Q.Cast(eprediction.CastPosition);
                    }

                }
            }
            if (Settings.UseW && Player.Instance.ManaPercent > Settings.Mana && W.IsReady())
            {
                var target = TargetSelector.GetTarget(W.Range, DamageType.Physical);
                    if (target != null)
                    {
                        W.Cast();
                    }
                }
            }
        }
    }



