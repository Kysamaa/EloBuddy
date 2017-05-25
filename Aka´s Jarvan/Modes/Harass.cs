using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using SharpDX;

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
            // my way of using the menu in the Config class
                var qtarget = TargetSelector.GetTarget(E.Range, DamageType.Physical);
                if (Settings.UseE && E.IsReady() && Player.Instance.ManaPercent > Settings.Mana &&
                    (qtarget.IsValidTarget(Q.Range) && Q.IsReady()))
                {
                    var vec = qtarget.ServerPosition - ObjectManager.Player.Position;
                    var castBehind = E.GetPrediction(qtarget).CastPosition + Vector3.Normalize(vec) * 100;
                    E.Cast(castBehind);
                    Q.Cast(castBehind);
                }

            if (Config.Modes.Combo.UseQ && Q.IsReady() && Player.Instance.ManaPercent > Settings.Mana)
            {
                var wtarget = TargetSelector.GetTarget(W.Range, DamageType.Physical);
                var prede = Q.GetPrediction(wtarget);
                if (prede.HitChance >= HitChance.Medium)
                {
                    if (wtarget != null)
                    {
                        Q.Cast(prede.CastPosition);
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

