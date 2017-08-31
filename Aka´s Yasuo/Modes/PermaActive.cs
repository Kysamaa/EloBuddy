using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace AkaYasuo.Modes
{
    partial class PermaActive
    {
        public static void AutoR()
        {
            if (!Program.R.IsReady())
            {
                return;
            }

            var useR = MenuManager.ComboMenu["AutoR"].Cast<CheckBox>().CurrentValue;
            var autoREnemies = MenuManager.ComboMenu["AutoR2"].Cast<Slider>().CurrentValue;
            var MyHP = MenuManager.ComboMenu["AutoR2HP"].Cast<Slider>().CurrentValue;
            var enemyInRange = MenuManager.ComboMenu["AutoR2Enemies"].Cast<Slider>().CurrentValue;
            //var useRDown = ComboMenu["AutoR3"].Cast<Slider>().CurrentValue;

            if (!useR)
            {
                return;
            }

            var enemiesKnockedUp =
                ObjectManager.Get<AIHeroClient>()
                    .Where(x => x.IsValidTarget(Program.R.Range))
                    .Where(x => x.HasBuffOfType(BuffType.Knockup));

            var enemies = enemiesKnockedUp as IList<AIHeroClient> ?? enemiesKnockedUp.ToList();

            if (enemies.Count() >= autoREnemies && Variables._Player.Health >= MyHP && Variables._Player.CountEnemiesInRange(1500) <= enemyInRange)
            {
                Program.R.Cast();
            }
        }

        public static void KillSteal()
        {
            foreach (AIHeroClient enemy in EntityManager.Heroes.Enemies)
            {
                if (enemy.IsValidTarget(Program.Q.Range))
                {
                    if (MenuManager.KillStealMenu["KsQ"].Cast<CheckBox>().CurrentValue && Program.Q.IsReady())
                    {
                        if (enemy.Health <= DamageManager.GetQDmg(enemy))
                        {
                            Program.Q.Cast(enemy.ServerPosition);
                        }
                    }
                    if (!Program.Q.IsReady() && Program.E.IsReady() && MenuManager.KillStealMenu["KsE"].Cast<CheckBox>().CurrentValue && (Variables._Player.GetSpellDamage(enemy, SpellSlot.E) >= enemy.Health) && Variables.CanCastE(enemy))
                    {
                        if (enemy.Health <= DamageManager.GetEDmg(enemy))
                        {
                            Program.E.Cast(enemy);
                        }
                    }
                    if (Program.Ignite != null && MenuManager.KillStealMenu["KsIgnite"].Cast<CheckBox>().CurrentValue && Program.Ignite.IsReady())
                    {
                        if (enemy.Health <= Variables._Player.GetSummonerSpellDamage(enemy, DamageLibrary.SummonerSpells.Ignite))
                        {
                            Program.Ignite.Cast(enemy);
                        }
                    }
                }
            }
        }

        public static void sChoose()
        {
            var style = MenuManager.MiscMenu["sID"].DisplayName;

            switch (style)
            {
                case "Classic":
                    Player.SetSkinId(0);
                    break;
                case "High-Noon Yasuo":
                    Player.SetSkinId(1);
                    break;
                case "Project Yasuo":
                    Player.SetSkinId(2);
                    break;
            }
        }
    }
}
