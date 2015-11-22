using System.Data.SqlTypes;
using AddonTemplate;
using EloBuddy;
using EloBuddy.SDK;

namespace AddonTemplate
{
    class Damages
    {  
        public static float FaaRawDamage()
        {
            var target = TargetSelector.GetTarget(SpellManager.E.Range, DamageType.Physical);
            return
                (int)
                    (new int[] { 0, 0, 0, 0, 0 }[SpellManager.Q.Level - 1] +
                     0.1 * (target.Health));
        }

        public static float FaaDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical, FaaRawDamage()) *
                   (Player.Instance.HasBuff("SummonerExhaustSlow") ? 0.1f : 1);
        }

        public static float QRawDamage()
        {
            return
                (int)
                    (new int[] { 75, 115, 160, 205, 250 }[SpellManager.Q.Level - 1] +
                     1.2 * (ObjectManager.Player.TotalAttackDamage));
        }

        public static float QDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical, QRawDamage()) *
                   (Player.Instance.HasBuff("SummonerExhaustSlow") ? 1.2f : 1);
        }

        public static float ERawDamage()
        {
            return
                (int)
                    (new int[] { 60, 105, 150, 195, 240 }[SpellManager.E.Level - 1] +
                     0.8 * (ObjectManager.Player.TotalMagicalDamage));
        }

        public static float EDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, ERawDamage()) *
                   (Player.Instance.HasBuff("SummonerExhaustSlow") ? 0.8f : 1);
        }

        public static float RRawDamage()
        {
            return
                (int)
                    (new int[] { 200, 325, 450 }[SpellManager.R.Level - 1] +
                     1.5 * (ObjectManager.Player.TotalMagicalDamage));
        }

        public static float RDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical, RRawDamage()) *
                   (Player.Instance.HasBuff("SummonerExhaustSlow") ? 1.5f : 1);
        }

    }
}