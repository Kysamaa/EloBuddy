using System.Runtime.InteropServices;
using AddonTemplate.Utility;
using EloBuddy;
using EloBuddy.SDK;
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
            if (Settings.UseR)
            {
                var etarget = TargetSelector.GetTarget(R.Range, DamageType.Physical);
                if (Damages.RDamage(etarget) + Damages.QDamage(etarget) + Damages.EDamage(etarget) + Damages.WDamage(etarget) > etarget.Health)
                {
                        R.Cast();
                    }
                }
            


            if (Settings.UseE)
            {
                var etarget = TargetSelector.GetTarget(E.Range, DamageType.Magical);
                var eprediction = E.GetPrediction(etarget);
                if (eprediction.HitChance >= HitChance.High)
                {
                    if (E.IsReady() && etarget != null && !ObjectManager.Player.HasBuff("SionR"))
                    {
                        E.Cast(eprediction.CastPosition);
                    }
                }
            }
            if (Settings.UseW)
            {
                var target = TargetSelector.GetTarget(W.Range, DamageType.Physical);
                if (W.IsReady() && target != null && !ObjectManager.Player.HasBuff("SionR"))
                {
                    W.Cast();
                }
            }

            if (Settings.UseQ)
            {
                var target = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
                var eprediction = Q.GetPrediction(target);
                if (eprediction.HitChance >= HitChance.High && !ObjectManager.Player.HasBuff("SionR"))
                {
                    if (Q.IsReady() && target != null && Player.Instance.IsFacing(target))
                    {
                        Q.Cast(eprediction.CastPosition);
                    }
                }
            }

            if (activatedP)
            {
                var target = TargetSelector.GetTarget(2000, DamageType.Physical);
                if (Q.IsReady() && Player.Instance.Position.Distance(target) > target.GetAutoAttackRange())
                {
                    Q.Cast(target);
                }
            }

        }


        private static bool activatedP
        {
            get { return Player.Instance.Spellbook.GetSpell(SpellSlot.Q).Name == "sionpassivespeed"; }
        }
    }
}
                
            

        
    



