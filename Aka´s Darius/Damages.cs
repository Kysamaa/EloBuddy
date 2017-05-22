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
                    (new int[] { 20, 35, 50, 65, 80 }[SpellManager.Q.Level - 1] +
                     0.7 * (ObjectManager.Player.TotalAttackDamage));
        }

        public static float QDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical, QRawDamage()) *
                   (Player.Instance.HasBuff("SummonerExhaustSlow") ? 0.7f : 1);
        }

        public static float WRawDamage()
        {
            return
                (int)
                    (new int[] { 0, 0, 0, 0, 0 }[SpellManager.E.Level - 1] +
                     0.4 * (ObjectManager.Player.TotalAttackDamage));
        }

        public static float WDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical, WRawDamage()) *
                   (Player.Instance.HasBuff("SummonerExhaustSlow") ? 0.4f : 1);
        }

        public static float RDamage(Obj_AI_Base unit, int stackcount)
        {
            var bonus =
                stackcount *
                    (new[] { 20, 20, 40, 60 }[SpellManager.R.Level] + (0.20 * ObjectManager.Player.FlatPhysicalDamageMod));

            return
                (float)(bonus + (ObjectManager.Player.CalculateDamageOnUnit(unit, DamageType.True,
                        new[] { 100, 100, 200, 300 }[SpellManager.R.Level] + (float)(0.75 * ObjectManager.Player.FlatPhysicalDamageMod))));
        }

        public static float PassiveDmg(Obj_AI_Base unit, int stackcount)
        {
            if (stackcount <= 0)
                stackcount = 1;

            return
                (float)
                    ObjectManager.Player.CalculateDamageOnUnit(unit, DamageType.Physical,
                        (9 + ObjectManager.Player.Level) + (float)(0.3 * ObjectManager.Player.FlatPhysicalDamageMod)) * stackcount;
        }

    }
}