using System;
using System.Linq;
using AddonTemplate.Utility;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu;
using SharpDX;
using HitChance = EloBuddy.SDK.Enumerations.HitChance;

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
            var qTarget = TargetSelector.GetTarget(Q.Range + 200, DamageType.Magical);
            if (qTarget == null)
            {
                return;
            }

            if (Settings.UseQ && Q.IsReady() && !Settings.UseQ2)
            {
                foreach (var soldier in Orbwalker.AzirSoldiers)
                {
                    var pred = Prediction.Position.PredictLinearMissile(qTarget, Q.Range, Q.Width, Q.CastDelay,
                        Q.Speed, Int32.MaxValue, soldier.Position, true);
                    if (pred.HitChance >= HitChance.High)
                    {
                        Q.Cast(pred.CastPosition.Extend(pred.UnitPosition, 115.0f).To3D());
                    }
                }
            }

            if (Settings.UseQ && Q.IsReady() && Settings.UseQ2 && Orbwalker.AzirSoldiers.Count == 2)
            {
                foreach (var soldier in Orbwalker.AzirSoldiers)
                {
                    var pred = Prediction.Position.PredictLinearMissile(qTarget, Q.Range, Q.Width, Q.CastDelay,
                        Q.Speed, Int32.MaxValue, soldier.Position, true);
                    if (pred.HitChance >= HitChance.High)
                    {
                        Q.Cast(pred.CastPosition.Extend(pred.UnitPosition, 115.0f).To3D());
                    }
                }
            }

            if (Settings.UseW && W.Handle.Ammo > 0)
            {
                var p = ObjectManager.Player.Distance(qTarget, true) > W.RangeSquared
                    ? ObjectManager.Player.Position.To2D().Extend(qTarget.Position.To2D(), W.Range)
                    : qTarget.Position.To2D();
                W.Cast((Vector3) p);
            }

            if (!Settings.UseE &&
                (HeroManager.Enemies.Count(e => e.IsValidTarget(1000)) <= 2 &&
                 Damages.GetComboDamage(qTarget) >= qTarget.Health) && E.IsReady())

            {
                foreach (
                    var soldier in
                        Orbwalker.AzirSoldiers.Where(
                            s => ObjectManager.Player.Distance(s) < E.RangeSquared))


                {
                    if (qTarget.Position.Between(Player.Instance.Position, soldier.ServerPosition))
                    {
                        E.Cast(soldier.ServerPosition);
                    }
                }

            }



            if (Damages.GetComboDamage(qTarget) >= qTarget.Health)
            {
                if (Settings.UseR && R.IsReady())
                {
                    R.Cast(qTarget.Position);
                }
                if (Settings.UseIgnite && SpellManager.Ignite.IsReady())
                {
                    SpellManager.Ignite.Cast(qTarget);

                }
            }

        }
    }
}

                
            

        
    



