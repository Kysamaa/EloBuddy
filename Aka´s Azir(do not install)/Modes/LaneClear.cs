using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AddonTemplate.Utility;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;
using Settings = AddonTemplate.Config.Modes.LaneClear;
namespace AddonTemplate.Modes
{
    public sealed class LaneClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            // Only execute this mode when the orbwalker is on laneclear mode
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear);
        }

        public override void Execute()
        {
            var minions = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.Instance.Position, 1000.0f).ToList();
            if (Orbwalker.AzirSoldiers.Count <= Settings.UseSmax && Settings.UseW && W.IsReady() && Player.Instance.ManaPercent >= Settings.Mana)
            {
                var farmLoc = EntityManager.MinionsAndMonsters.GetCircularFarmLocation(minions, W.Width, (int)W.Range);
                if (farmLoc.HitNumber >= 3)
                {
                    W.Cast(farmLoc.CastPosition);
                }
            }

            if (Orbwalker.AzirSoldiers.Count > 0 && Settings.UseQ && Q.IsReady() && Player.Instance.ManaPercent >= Settings.Mana)
            {
                foreach (var minion in minions)
                {
                    var qCasted = false;
                    foreach (var soldier in Orbwalker.AzirSoldiers)
                    {
                        var pred = Prediction.Position.PredictLinearMissile(soldier, Q.Range, (int)soldier.BoundingRadius * 2, Q.CastDelay,
                            Q.Speed, Int32.MaxValue, soldier.Position);
                        var cols = pred.CollisionObjects.Count();
                        if (cols >= 3)
                        {
                            Q.Cast(minion);
                            qCasted = true;
                            break;
                        }
                    }
                    if (qCasted)
                    {
                        break;
                    }
                }
            }
        }
    }
}
