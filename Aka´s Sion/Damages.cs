using AddonTemplate;
using EloBuddy;
using EloBuddy.SDK;

namespace AddonTemplate
{
    class Damages
    {
       public static float QRawDamage()
        {
            return
                (int)
                    (new int[] { 20, 40, 60, 80, 100 }[SpellManager.Q.Level - 1] +
                     0.65 * (ObjectManager.Player.TotalAttackDamage));
        }

        public static float QDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical, QRawDamage()) *
                   (Player.Instance.HasBuff("SummonerExhaustSlow") ? 0.65f : 1);
        }

        public static float WRawDamage()
        {
            return
                (int)
                    (new int[] { 40, 65, 90, 115, 140 }[SpellManager.W.Level - 1] +
                     0 * (ObjectManager.Player.TotalAttackDamage));
        }

        public static float WDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical, WRawDamage()) *
                   (Player.Instance.HasBuff("SummonerExhaustSlow") ? 0f : 1);
        }

        public static float ERawDamage()
        {
            return
                (int)
                    (new int[] { 70, 105, 140, 175, 210 }[SpellManager.E.Level - 1] +
                     0.4 * (ObjectManager.Player.TotalMagicalDamage));
        }

        public static float EDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, WRawDamage())*
                   (Player.Instance.HasBuff("SummonerExhaustSlow") ? 0.4f : 1);

        }
        public static float RRawDamage()
        {
            return
                (int)
                    (new int[] { 150, 300, 450, }[SpellManager.R.Level - 1] +
                     0.4 * (ObjectManager.Player.TotalAttackDamage));
        }

        public static float RDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical, WRawDamage()) *
                   (Player.Instance.HasBuff("SummonerExhaustSlow") ? 0.4f : 1);

        }
    }
    }