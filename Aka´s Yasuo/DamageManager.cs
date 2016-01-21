using EloBuddy;
using EloBuddy.SDK;
using System;

namespace AkaYasuo
{
    class DamageManager
    {
        public static float QDamage(Obj_AI_Base target)
        {
            return Variables._Player.CalculateDamageOnUnit(target, DamageType.Physical,
                (float)(new double[] { 20, 40, 60, 80, 100 }[Player.GetSpell(SpellSlot.Q).Level - 1]
                         + 1 * (Variables._Player.TotalAttackDamage)));
        }
        public static float EDamage(Obj_AI_Base target)
        {
            var stacksPassive = Variables._Player.Buffs.Find(b => b.DisplayName.Equals("YasuoDashScalar"));
            var stacks = 1 + 0.25 * ((stacksPassive != null) ? stacksPassive.Count : 0);
            return Variables._Player.CalculateDamageOnUnit(target, DamageType.Magical,
                (float)(new double[] { 70, 90, 110, 130, 150 }[Player.GetSpell(SpellSlot.E).Level - 1] * stacks
                         + 0.6 * (Variables._Player.FlatMagicDamageMod)));
        }

        public static float RDamage(Obj_AI_Base target)
        {
            return Variables._Player.CalculateDamageOnUnit(target, DamageType.Physical,
                (float)(new double[] { 200, 300, 400 }[Player.GetSpell(SpellSlot.R).Level - 1]
                         + 1.5 * (Variables._Player.FlatPhysicalDamageMod)));
        }

        public static double GetQDmg(Obj_AI_Base target)
        {
            var dmgItem = 0d;
            if (Item.HasItem(3057) && (Item.CanUseItem(3057) || Variables._Player.HasBuff("Sheen")))
            {
                dmgItem = Variables._Player.BaseAttackDamage;
            }
            if (Item.HasItem(3078) && (Item.CanUseItem(3078) || Variables._Player.HasBuff("Sheen")))
            {
                dmgItem = Variables._Player.BaseAttackDamage * 2;
            }
            var damageModifier = 1d;
            var reduction = 0d;
            var result = dmgItem
                         + Variables._Player.TotalAttackDamage * (Variables._Player.Crit >= 0.85f ? (Item.HasItem(3031) ? 1.875 : 1.5) : 1);
            if (Item.HasItem(3153))
            {
                var dmgBotrk = Math.Max(0.08 * target.Health, 10);
                result += target is Obj_AI_Minion ? Math.Min(dmgBotrk, 60) : dmgBotrk;
            }
            var targetHero = target as AIHeroClient;
            if (targetHero != null)
            {
                if (Item.HasItem(3047, targetHero))
                {
                    damageModifier *= 0.9d;
                }
                if (targetHero.ChampionName == "Fizz")
                {
                    reduction += 4 + (targetHero.Level - 1 / 3) * 2;
                }
                var mastery = targetHero.Masteries.Find(i => i.Page == MasteryPage.Defense && i.Id == 68);
                if (mastery != null && mastery.Points >= 1)
                {
                    reduction += 1 * mastery.Points;
                }
            }
            return Variables._Player.CalculateDamageOnUnit(
                target,
                DamageType.Physical,
                20 * Program.Q.Level + (float)(result - reduction) * (float)damageModifier)
                   + (HaveStatik
                          ? Variables._Player.CalculateDamageOnUnit(
                              target,
                              DamageType.Magical,
                              100 * (float)(Variables._Player.Crit >= 0.85f ? (Item.HasItem(3031) ? 2.25 : 1.8) : 1))
                          : 0);
        }

        private static bool HaveStatik
        {
            get
            {
                return Variables._Player.GetBuffCount("ItemStatikShankCharge") == 100;
            }
        }
    }
}
