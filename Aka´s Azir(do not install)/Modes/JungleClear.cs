using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using Settings = AddonTemplate.Config.Modes.JungleClear;
namespace AddonTemplate.Modes
{
    public sealed class JungleClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            // Only execute this mode when the orbwalker is on jungleclear mode
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear);
        }

        public override void Execute()
        { 
            var monsters = EntityManager.MinionsAndMonsters.GetJungleMonsters(ObjectManager.Player.Position, 1000.0f).ToList();
            if (Orbwalker.AzirSoldiers.Count<=Settings.UseSmax && Settings.UseW && W.IsReady() && Player.Instance.ManaPercent >= Settings.Mana)
            {
                var farmLoc = EntityManager.MinionsAndMonsters.GetCircularFarmLocation(monsters, W.Width, (int)W.Range);
                if (farmLoc.HitNumber >= 1)
                {
                    W.Cast(farmLoc.CastPosition);
                }
}

            if (Orbwalker.AzirSoldiers.Count > 0 && Settings.UseQ && Q.IsReady() && Player.Instance.ManaPercent >= Settings.Mana)
            {
                foreach (var monster in monsters)
                {
                    var qCasted = false;
                    foreach (var soldier in Orbwalker.AzirSoldiers)
                    {
                        var pred = Prediction.Position.PredictLinearMissile(soldier, Q.Range, (int)soldier.BoundingRadius * 2, Q.CastDelay,
                            Q.Speed, Int32.MaxValue, soldier.Position);
var cols = pred.CollisionObjects.Count();
                        if (cols >= 2)
                        {
                            Q.Cast(monster);
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
