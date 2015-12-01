using AddonTemplate;
using EloBuddy;
using EloBuddy.SDK;

namespace AddonTemplate
{
    class Damages
    {
        public static double QDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical,
                (float)
                    (new[] { 0.3, 0.35, 0.4, 0.45, 0.5 }[
                        Player.Instance.Spellbook.GetSpell(SpellSlot.Q).Level - 1]) *
                (Player.Instance.TotalAttackDamage));
        }
        public static float ERawDamage()
        {
            return
                (int)
                    (new int[] { 45, 80, 115, 150, 185 }[SpellManager.Q.Level - 1] +
                     0.5 * (ObjectManager.Player.TotalAttackDamage));
        }

        public static float EDamage(Obj_AI_Base target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical, ERawDamage()) *
                   (Player.Instance.HasBuff("SummonerExhaustSlow") ? 0.5f : 1);
        }

        public static float Wdmg(Obj_AI_Base target)
        {
            var dmg = (SpellManager.W.Level * 10 + 10) + ((0.03 + (SpellManager.W.Level * 0.01)) * target.MaxHealth);
            return (float)dmg;

        }

        public static int WTarget(Obj_AI_Base target)
        {
            foreach (var buff in target.Buffs)
            {
                if (buff.Name == "vaynesilvereddebuff")
                    return buff.Count;
            }
            return -1;
        }
    }
}