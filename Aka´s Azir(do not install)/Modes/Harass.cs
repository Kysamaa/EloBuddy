using System;
using System.Linq;
using Addontemplate;
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
            var harassTarget = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
            if (harassTarget == null)
            {
                return;
            }

            if (Orbwalker.AzirSoldiers.Count == 0)
            {
                var p = ObjectManager.Player.Position.To2D().Extend(harassTarget.Position.To2D(), W.Range);
                if (Q.IsReady() || HeroManager.Enemies.Any(h => h.IsValidTarget(W.Range + 200)))
                {
                    W.Cast((Vector3) p);
                }
                return;
            }

            if (Q.IsReady())
            {
                var qTarget = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
                if (qTarget != null)
                {
                    foreach (var soldier in SoldiersManager.AllSoldiers)
                    {
                        var pred = Prediction.Position.PredictLinearMissile(qTarget, Q.Range, Q.Width, Q.CastDelay,
                            Q.Speed, Int32.MaxValue, soldier.Position, true);
                        if (pred.HitChance >= HitChance.Medium)
                        {
                            Q.Cast(pred.CastPosition.Extend(pred.UnitPosition, 115.0f).To3D());
                        }
                    }
                }
            }
        }
    }
}
    

