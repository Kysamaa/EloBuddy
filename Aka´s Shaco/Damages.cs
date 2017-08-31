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
                    (new int[] { -60, -40, -20, 0, 50 }[SpellManager.Q.Level - 1] +
                     2.0 * (ObjectManager.Player.TotalAttackDamage));
        }

        public static float QDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical, QRawDamage()) *
                   (Player.Instance.HasBuff("SummonerExhaustSlow") ? 2.0f : 1);
        }

        public static float WRawDamage()
        {
            return
                (int)
                    (new int[] {35, 50, 65, 80, 95}[SpellManager.W.Level - 1] +
                     0.2*(ObjectManager.Player.TotalMagicalDamage));
        }

        public static float WDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, WRawDamage()) *
                   (Player.Instance.HasBuff("SummonerExhaustSlow") ? 0.2f : 1);
        }


        public static float RRawDamage()
        {
            return
                (int)
                    (new int[] { 300, 450, 600}[SpellManager.R.Level - 1] +
                     1.0 * (ObjectManager.Player.TotalMagicalDamage));
        }

        public static float RDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, RRawDamage()) *
                   (Player.Instance.HasBuff("SummonerExhaustSlow") ? 1.0f : 1);
        }




    }
}