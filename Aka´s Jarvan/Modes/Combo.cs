using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using SharpDX;

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
            var qtarget = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
            if (Settings.UseE && E.IsReady() &&
                (qtarget.IsValidTarget(Q.Range) && Q.IsReady()))
            {
                var vec = qtarget.ServerPosition - ObjectManager.Player.Position;
                var castBehind = E.GetPrediction(qtarget).CastPosition + Vector3.Normalize(vec)*100;
                E.Cast(castBehind);
                Q.Cast(castBehind);
            }


            if (Settings.UseW)
            {
                var target = TargetSelector.GetTarget(W.Range, DamageType.Physical);
                if (W.IsReady() && target != null)
                {
                    W.Cast();
                }
            }

            if (Settings.UseQ && Q.IsReady() && !E.IsReady())
            {
                var wtarget = TargetSelector.GetTarget(W.Range, DamageType.Physical);
                var prede = Q.GetPrediction(wtarget);
                if (prede.HitChance >= HitChance.High)
                {
                    if (wtarget != null)
                    {
                        Q.Cast(prede.CastPosition);
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




