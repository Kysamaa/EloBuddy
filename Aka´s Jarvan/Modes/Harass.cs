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
            var itarget = TargetSelector.GetTarget(1000, DamageType.Physical);
            Items.UseItems(itarget);


            if (Settings.Harassmode && !Q.IsReady() && E.IsReady() && Player.Instance.ManaPercent > Settings.Mana)
            {
                var wtarget = TargetSelector.GetTarget(E.Range, DamageType.Physical);
                //var predq = Q.GetPrediction(wtarget);
                var prede = E.GetPrediction(wtarget);
                if (prede.HitChance >= HitChance.Medium)
                {
                    if (wtarget != null)
                    {
                        E.Cast(prede.CastPosition);
                    }
                }
            }

            if (Settings.Harassmode && Q.IsReady() && Player.Instance.ManaPercent > Settings.Mana)
            {
                var wtarget = TargetSelector.GetTarget(E.Range, DamageType.Physical);
                var predq = Q.GetPrediction(wtarget);
                //var prede = E.GetPrediction(wtarget);
                if (predq.HitChance >= HitChance.Medium)
                {
                    if (wtarget != null)
                    {
                        Q.Cast(predq.CastPosition);
                    }
                }
            }

            if (!Settings.Harassmode && Q.IsReady() && SpellManager.Epos != default(Vector3) && Player.Instance.ManaPercent > Settings.Mana)
            {
                var wtarget = TargetSelector.GetTarget(E.Range, DamageType.Physical);
                var predq = Q.GetPrediction(wtarget);
                //var prede = E.GetPrediction(wtarget);
                if (predq.HitChance >= HitChance.Medium)
                {
                    if (wtarget != null)
                    {
                        Q.Cast(SpellManager.Epos);
                    }
                }
            }

            if (!Settings.Harassmode && E.IsReady() && Player.Instance.ManaPercent > Settings.Mana)
            {
                var wtarget = TargetSelector.GetTarget(E.Range, DamageType.Physical);
                //var predq = Q.GetPrediction(wtarget);
                var prede = E.GetPrediction(wtarget);
                if (prede.HitChance >= HitChance.Medium)
                {
                    if (wtarget != null)
                    {
                        E.Cast(prede.CastPosition);
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

