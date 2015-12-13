using System;
using System.Linq;
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

            if (Settings.UseQ && Q.IsReady() && Player.Instance.ManaPercent >= Settings.Mana)
            {
                foreach (var soldier in Orbwalker.AzirSoldiers)
                {
                    var pred = Prediction.Position.PredictLinearMissile(harassTarget, Q.Range, Q.Width, Q.CastDelay,
                        Q.Speed, Int32.MaxValue, soldier.Position, true);
                    if (pred.HitChance >= HitChance.Medium)
                    {
                        Q.Cast(pred.CastPosition.Extend(pred.UnitPosition, 115.0f).To3D());
                    }
                }
            }

            if (Config.Modes.Combo.UseW && W.Handle.Ammo > 0 && Player.Instance.ManaPercent >= Settings.Mana && Orbwalker.AzirSoldiers.Count <= Settings.UseSmax)
            {
                var p = ObjectManager.Player.Distance(harassTarget, true) > W.RangeSquared
                    ? ObjectManager.Player.Position.To2D().Extend(harassTarget.Position.To2D(), W.Range)
                    : harassTarget.Position.To2D();
                W.Cast((Vector3)p);
            }

            if (!Config.Modes.Combo.UseE && Player.Instance.ManaPercent >= Settings.Mana &&
                HeroManager.Enemies.Count(e => e.IsValidTarget(1000)) <= 2 && E.IsReady())

            {
                foreach (
                    var soldier in
                        Orbwalker.AzirSoldiers.Where(
                            s => ObjectManager.Player.Distance(s) < E.RangeSquared))


                {
                    if (harassTarget.Position.Between(Player.Instance.Position, soldier.ServerPosition))
                    {
                        E.Cast(soldier.ServerPosition);
                    }
                }

            }
        }
    }
}
    

