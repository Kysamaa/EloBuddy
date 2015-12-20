using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;

namespace AkaYasuo
{
    class Mode
    {

        public static bool dashing;

        public static void AutoR()
        {
            if (!Program.R.IsReady())
            {
                return;
            }

            var useR = Program.ComboMenu["AutoR"].Cast<CheckBox>().CurrentValue;
            var autoREnemies = Program.ComboMenu["AutoR2"].Cast<Slider>().CurrentValue;
            var MyHP = Program.ComboMenu["AutoR2HP"].Cast<Slider>().CurrentValue;
            var enemyInRange = Program.ComboMenu["AutoR2Enemies"].Cast<Slider>().CurrentValue;
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

            if (enemies.Count() >= autoREnemies && Program.myHero.Health >= MyHP && Program.myHero.CountEnemiesInRange(1500) <= enemyInRange)
            {
                Program.R.Cast();
            }
        }

        public static void Combo()
        {
            var TsTarget = TargetSelector2.GetTarget(1300, DamageType.Physical);

            if (TsTarget == null)
            {
                return;
            }

            if (Program.ComboMenu["E"].Cast<CheckBox>().CurrentValue && Program.E.IsReady())
            {
                AIHeroClient enemy = (AIHeroClient) GetEnemy(1300, GameObjectType.AIHeroClient);
                if (enemy != null
                    && Extensions.Distance(GetDashingEnd(enemy), enemy) <= ObjectManager.Player.GetAutoAttackRange()
                    //wont e unless in AA range after
                    && enemy.Distance(ObjectManager.Player) <= Program.E.Range
                    && enemy.Distance(ObjectManager.Player) >= ObjectManager.Player.GetAutoAttackRange())
                {
                    Program.E.Cast(enemy);
                    dashing = true;
                    if (Program.ComboMenu["EQ"].Cast<CheckBox>().CurrentValue && Program.SteelTempest.IsReady())
                        Program.SteelTempest.Cast(enemy.Position);
                }
                else if (enemy != null
                         && enemy.Distance(ObjectManager.Player) > Player.Instance.GetAutoAttackRange(enemy)) //use minions to get to champ
                {
                    var minion =
                        ObjectManager.Get<Obj_AI_Minion>()
                            .OrderByDescending(m => m.Distance(ObjectManager.Player))
                            .FirstOrDefault(
                                m =>
                                    m.IsValidTarget(Program.E.Range)
                                    && (m.Distance(TsTarget) < ObjectManager.Player.Distance(TsTarget))
                                    && ObjectManager.Player.IsFacing(m) && Program.CanCastE(m) && (!Program.UnderEnemyTower(Program.PosAfterE(m))));

                    if (minion != null && (Program.PosAfterE(minion).Distance(enemy) < Player.Instance.Distance(enemy)))
                    {
                        Console.Write("E Cast minions");
                        Program.E.Cast(minion);
                    }
                }

                }


                if (Program.SteelTempest.IsReady() && Program.ComboMenu["Q"].Cast<CheckBox>().CurrentValue)
            {
                PredictionResult QPred = Program.SteelTempest.GetPrediction(TsTarget);
                if (!Program.isDashing() && Program.SteelTempest.Range == 1000)
                {
                    Program.SteelTempest.Cast(QPred.CastPosition); Core.DelayAction(Orbwalker.ResetAutoAttack, 250);
                }
                else if (Program.SteelTempest.Range == 1000 && Program.Q3READY(Program.myHero) && Program.isDashing() && Program.myHero.Distance(TsTarget) <= 250 * 250)
                {
                    Program.SteelTempest.Cast(QPred.CastPosition); Core.DelayAction(Orbwalker.ResetAutoAttack, 250);
                }
                else if (!Program.Q3READY(Program.myHero) && Program.SteelTempest.Range == 475)
                {
                    Program.SteelTempest.Cast(QPred.CastPosition); Core.DelayAction(Orbwalker.ResetAutoAttack, 250);
                }
            }

            if (Orbwalker.CanAutoAttack)
                {
                    AIHeroClient enemy =
                        (AIHeroClient) GetEnemy(ObjectManager.Player.GetAutoAttackRange(), GameObjectType.AIHeroClient);

                    if (enemy != null)
                        Orbwalker.ForcedTarget = enemy;
                }
                dashing = false;



                if (TsTarget != null)
                {

                    if (Program.ItemMenu["Items"].Cast<CheckBox>().CurrentValue && TsTarget.IsValidTarget())
                    {
                        Program.UseItems(TsTarget);
                    }
                    if (Program.E.IsReady() && Program.ComboMenu["E"].Cast<CheckBox>().CurrentValue)
                    {
                        if (Program.DogeMenu["smartW"].Cast<CheckBox>().CurrentValue)
                        {
                            Program.putWallBehind(TsTarget);
                        }
                        if (Program.DogeMenu["smartW"].Cast<CheckBox>().CurrentValue && Program.wallCasted &&
                            Program.myHero.Distance(TsTarget.Position) < 300)
                        {
                            Program.eBehindWall(TsTarget);
                        }
                        if (Program.Ignite != null && Program.Ignite.IsReady() &&
                            Program.ComboMenu["Ignite"].Cast<CheckBox>().CurrentValue)
                        {
                            if (TsTarget.Distance(Program.myHero) <= (600) &&
                                Program.myHero.GetSummonerSpellDamage(TsTarget, DamageLibrary.SummonerSpells.Ignite) >=
                                TsTarget.Health)
                            {
                                Program.Ignite.Cast(TsTarget);
                            }
                        }
                    }

                    if (Program.R.IsReady() && Program.ComboMenu["R"].Cast<CheckBox>().CurrentValue)
                    {
                        List<AIHeroClient> enemies = EntityManager.Heroes.Enemies;
                        foreach (AIHeroClient enemy in enemies)
                        {
                            if (Program.myHero.Distance(enemy) <= 1200)
                            {
                                var enemiesKnockedUp =
                                    ObjectManager.Get<AIHeroClient>()
                                        .Where(x => x.IsValidTarget(Program.R.Range))
                                        .Where(x => x.HasBuffOfType(BuffType.Knockup));

                                var enemiesKnocked = enemiesKnockedUp as IList<AIHeroClient> ??
                                                     enemiesKnockedUp.ToList();
                                if (enemy.IsValidTarget(Program.R.Range) &&
                                    Program.ComboMenu[TsTarget.ChampionName].Cast<CheckBox>().CurrentValue &&
                                    Program.CanCastDelayR(enemy) &&
                                    enemiesKnocked.Count() >=
                                    (Program.ComboMenu["R3"].Cast<Slider>().CurrentValue))
                                {
                                    Program.R.Cast();
                                }
                            }
                            if (enemy.IsValidTarget(Program.R.Range))
                            {
                                if (Program.IsKnockedUp(enemy) && Program.CanCastDelayR(enemy) &&
                                    (enemy.Health/enemy.MaxHealth*100 <=
                                     (Program.ComboMenu["R2"].Cast<Slider>().CurrentValue)))
                                {
                                    Program.R.Cast();
                                }
                                else if (Program.IsKnockedUp(enemy) &&
                                         Program.ComboMenu[TsTarget.ChampionName].Cast<CheckBox>().CurrentValue &&
                                         Program.CanCastDelayR(enemy) &&
                                         enemy.HealthPercent >=
                                         (Program.ComboMenu["R2"].Cast<Slider>().CurrentValue) &&
                                         (Program.ComboMenu["R4"].Cast<CheckBox>().CurrentValue))
                                {
                                    if (Program.AlliesNearTarget(TsTarget, 600))
                                    {
                                        Program.R.Cast();
                                    }
                                }
                            }
                        }
                    }
                }
            }

        public static
            Vector2 GetDashingEnd(Obj_AI_Base target)
        {
            if (!target.IsValidTarget())
            {
                return Vector2.Zero;
            }

            var baseX = ObjectManager.Player.Position.X;
            var baseY = ObjectManager.Player.Position.Y;
            var targetX = target.Position.X;
            var targetY = target.Position.Y;

            var vector = new Vector2(targetX - baseX, targetY - baseY);
            var sqrt = Math.Sqrt(vector.X*vector.X + vector.Y*vector.Y);

            var x = (float) (baseX + (Program.E.Range*(vector.X/sqrt)));
            var y = (float) (baseY + (Program.E.Range*(vector.Y/sqrt)));

            return new Vector2(x, y);
        }

        public static void Flee()
        {
            foreach (Obj_AI_Base minion in EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Program.myHero.ServerPosition, Program.E.Range, true))
            {
                var bestMinion =
                   ObjectManager.Get<Obj_AI_Base>()
                       .Where(x => x.IsValidTarget(Program.E.Range))
                       .Where(x => x.Distance(Game.CursorPos) < Program.myHero.Distance(Game.CursorPos))
                       .OrderByDescending(x => x.Distance(Program.myHero))
                       .FirstOrDefault();

                if (bestMinion != null && Program.myHero.IsFacing(bestMinion) && Program.CanCastE(bestMinion) && (Program.E.IsReady() && Program.FleeMenu["EscE"].Cast<CheckBox>().CurrentValue))
                {
                    Program.E.Cast(bestMinion);
                }
                if (Program.SteelTempest.IsReady() && Program.FleeMenu["EscQ"].Cast<CheckBox>().CurrentValue)
                {
                    if (!Program.Q3READY(Program.myHero) && Program.SteelTempest.Range == 475)
                    {
                        Program.SteelTempest.Cast(minion);
                    }
                }
            }
            if (Program.FleeMenu["WJ"].Cast<CheckBox>().CurrentValue)
            {
                Yasuo.WallJump();
            }
            foreach (AIHeroClient enemy in EntityManager.Heroes.Enemies)
            {
                var bestMinion =
                   ObjectManager.Get<AIHeroClient>()
                       .Where(x => x.IsValidTarget(Program.E.Range))
                       .Where(x => x.Distance(Game.CursorPos) < Program.myHero.Distance(Game.CursorPos))
                       .OrderByDescending(x => x.Distance(Program.myHero))
                       .FirstOrDefault();

                if (bestMinion != null && Program.myHero.IsFacing(bestMinion) && (Program.E.IsReady() && Program.FleeMenu["EscE"].Cast<CheckBox>().CurrentValue) && Program.myHero.IsFacing(bestMinion))
                {
                    Program.E.Cast(bestMinion);
                }
            }
        }

        public static void Harass()
        {
            var bestMinion =
                  ObjectManager.Get<Obj_AI_Minion>()
                      .Where(x => x.IsValidTarget(Program.E.Range))
                      .Where(x => x.Distance(Game.CursorPos) < Program.myHero.Distance(Game.CursorPos))
                      .OrderByDescending(x => x.Distance(Program.myHero))
                      .FirstOrDefault();

            if (bestMinion != null && Program.myHero.IsFacing(bestMinion) && Program.CanCastE(bestMinion) && (Program.E.IsReady() && Program.HarassMenu["E"].Cast<CheckBox>().CurrentValue))
            {
                Program.E.Cast(bestMinion);
            }
            var TsTarget = TargetSelector2.GetTarget(1300, DamageType.Physical);
            Orbwalker.ForcedTarget = TsTarget;

            if (TsTarget == null)
            {
                return;
            }

            if (TsTarget != null)
            {
                if (Program.SteelTempest.IsReady() && Program.HarassMenu["Q3"].Cast<CheckBox>().CurrentValue)
                {
                    PredictionResult QPred = Program.SteelTempest.GetPrediction(TsTarget);
                    if (!Program.isDashing())
                    {
                        Program.SteelTempest.Cast(QPred.CastPosition); Core.DelayAction(Orbwalking.ResetAutoAttackTimer, 250);
                    }
                    else if (Program.Q3READY(Program.myHero) && Program.isDashing() && Program.myHero.Distance(TsTarget) <= 250 * 250)
                    {
                        Program.SteelTempest.Cast(QPred.CastPosition); Core.DelayAction(Orbwalking.ResetAutoAttackTimer, 250);
                    }
                }
                if (TsTarget == null)
                {
                    return;
                }
                PredictionResult QPred2 = Program.SteelTempest.GetPrediction(TsTarget);
                if (!Program.Q3READY(Program.myHero) && Program.HarassMenu["Q"].Cast<CheckBox>().CurrentValue)
                {
                    Program.SteelTempest.Cast(QPred2.CastPosition); Core.DelayAction(Orbwalking.ResetAutoAttackTimer, 250);
                }
            }
        }

        public static void LastHit()
        {
            foreach (Obj_AI_Base minion in EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Program.myHero.ServerPosition, Program.SteelTempest.Range, true).OrderByDescending(m => m.Health))
            {
                if (minion == null)
                {
                    return;
                }

                if (!minion.IsDead && minion != null && Program.LastHitMenu["Q"].Cast<CheckBox>().CurrentValue && Program.SteelTempest.IsReady() && minion.IsValidTarget() && !Program.Q3READY(Program.myHero))
                {
                    var predHealth = Prediction.Health.GetPrediction(minion, (int)(Program.myHero.Distance(minion.Position) * 1000 / 2000));
                    if (predHealth <= Program.myHero.GetSpellDamage(minion, SpellSlot.Q))
                    {
                        Program.SteelTempest.Cast(minion.ServerPosition);
                    }
                }
                if (!minion.IsDead && minion != null && Program.LastHitMenu["Q3"].Cast<CheckBox>().CurrentValue && Program.SteelTempest.IsReady() && minion.IsValidTarget() && Program.Q3READY(Program.myHero))
                {
                    var predHealth = Prediction.Health.GetPrediction(minion, (int)(Program.myHero.Distance(minion.Position) * 1000 / 2000));
                    if (predHealth <= Program.myHero.GetSpellDamage(minion, SpellSlot.Q))
                    {
                        Program.SteelTempest.Cast(minion.ServerPosition);
                    }
                }
                if (Program.LastHitMenu["E"].Cast<CheckBox>().CurrentValue && Program.E.IsReady() && minion.IsValidTarget())
                {
                    if (!Program.UnderEnemyTower(Program.PosAfterE(minion)))
                    {
                        var predHealth = Prediction.Health.GetPrediction(minion, (int)(Program.myHero.Distance(minion.Position) * 1000 / 2000));
                        if (predHealth <= Program.myHero.GetSpellDamage(minion, SpellSlot.E))
                        {
                            Program.E.Cast(minion);
                        }
                    }
                }
            }
        }

        public static void LaneClear()
        {
            foreach (Obj_AI_Base minion in EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Program.myHero.Position, Program.SteelTempest.Range, true).OrderByDescending(m => m.Health))
            {
                if (minion == null)
                {
                    return;
                }

                if (!minion.IsDead && minion != null && Program.LaneClearMenu["Q"].Cast<CheckBox>().CurrentValue && Program.SteelTempest.IsReady() && minion.IsValidTarget() && !Program.Q3READY(Program.myHero))
                {
                    var predHealth = Prediction.Health.GetPrediction(minion, (int)(Program.myHero.Distance(minion.Position) * 1000 / 2000));
                    if (predHealth <= Program.myHero.GetSpellDamage(minion, SpellSlot.Q))
                    {
                        Program.SteelTempest.Cast(minion.ServerPosition);
                    }
                    else if (!Program.Q3READY(Program.myHero))
                    {
                        Program.SteelTempest.Cast(minion.ServerPosition);
                    }
                }
                if (!minion.IsDead && minion != null && Program.LaneClearMenu["Q3"].Cast<CheckBox>().CurrentValue && Program.SteelTempest.IsReady() && minion.IsValidTarget() && Program.Q3READY(Program.myHero))
                {
                    var predHealth = Prediction.Health.GetPrediction(minion, (int)(Program.myHero.Distance(minion.Position) * 1000 / 2000));
                    if (predHealth <= Program.myHero.GetSpellDamage(minion, SpellSlot.Q))
                    {
                        Program.SteelTempest.Cast(minion.ServerPosition);
                    }
                    else if (Program.Q3READY(Program.myHero))
                    {
                        Program.SteelTempest.Cast(minion.ServerPosition);
                    }
                }
            }
            var allMinionsE = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Program.myHero.Position, Program.E.Range, true);
            foreach (var minion in allMinionsE.Where(Program.CanCastE))
            {
                if (Program.LaneClearMenu["E"].Cast<CheckBox>().CurrentValue && Program.E.IsReady() && minion.IsValidTarget(Program.E.Range))
                {
                    if (!Program.UnderEnemyTower(Program.PosAfterE(minion)))
                    {
                        var predHealth = Prediction.Health.GetPrediction(minion, (int)(Program.myHero.Distance(minion.Position) * 1000 / 2000));
                        if (predHealth <= Program.myHero.GetSpellDamage(minion, SpellSlot.E))
                        {
                            Program.E.Cast(minion);
                        }
                    }
                }
                if (Program.LaneClearMenu["Items"].Cast<CheckBox>().CurrentValue)
                {
                    Program.UseItems(minion);
                }
            }
        }

        public static void JungleClear()
        {
            foreach (Obj_AI_Base minion in EntityManager.MinionsAndMonsters.GetJungleMonsters(Program.myHero.ServerPosition, Program.SteelTempest.Range, true))
            {
                if (minion == null)
                {
                    return;
                }

                if (Program.JungleClearMenu["Q"].Cast<CheckBox>().CurrentValue && Program.SteelTempest.IsReady() && minion.IsValidTarget())
                {

                    var predHealth = Prediction.Health.GetPrediction(minion, (int)(Program.myHero.Distance(minion.Position) * 1000 / 2000));
                    if (predHealth <= Program.myHero.GetSpellDamage(minion, SpellSlot.Q))
                    {
                        Program.SteelTempest.Cast(minion.ServerPosition);
                    }
                    else if (Program.SteelTempest.IsReady())
                    {
                        Program.SteelTempest.Cast(minion.ServerPosition);
                    }
                }
                if (Program.JungleClearMenu["E"].Cast<CheckBox>().CurrentValue && Program.E.IsReady() && minion.IsValidTarget(Program.E.Range) && Program.CanCastE(minion))
                {
                    var predHealth = Prediction.Health.GetPrediction(minion, (int)(Program.myHero.Distance(minion.Position) * 1000 / 2000));
                    if (predHealth <= Program.myHero.GetSpellDamage(minion, SpellSlot.E))
                    {
                        Program.E.Cast(minion);
                    }
                    else
                    {
                        Program.E.Cast(minion);
                    }
                }
                if (Program.JungleClearMenu["Items"].Cast<CheckBox>().CurrentValue)
                {
                    Program.UseItems(minion);
                }
            }
        }

        public static void KillSteal()
        {
            foreach (AIHeroClient enemy in EntityManager.Heroes.Enemies)
            {
                if (enemy.IsValidTarget(Program.SteelTempest.Range))
                {
                    if (Program.KillStealMenu["KsQ"].Cast<CheckBox>().CurrentValue && Program.SteelTempest.IsReady())
                    {
                        var predHealth = Prediction.Health.GetPrediction(enemy, (int)(Program.myHero.Distance(enemy.Position) * 1000 / 2000));
                        if (predHealth <= Program.myHero.GetSpellDamage(enemy, SpellSlot.Q))
                        {
                            Program.SteelTempest.Cast(enemy.ServerPosition);
                        }
                    }
                    if (!Program.SteelTempest.IsReady() && Program.E.IsReady() && Program.KillStealMenu["KsE"].Cast<CheckBox>().CurrentValue && (Program.myHero.GetSpellDamage(enemy, SpellSlot.E) >= enemy.Health) && Program.CanCastE(enemy))
                    {
                        var predHealth = Prediction.Health.GetPrediction(enemy, (int)(Program.myHero.Distance(enemy.Position) * 1000 / 2000));
                        if (predHealth <= Program.myHero.GetSpellDamage(enemy, SpellSlot.E))
                        {
                            Program.E.Cast(enemy);
                        }
                    }
                    if (Program.Ignite != null && Program.KillStealMenu["KsIgnite"].Cast<CheckBox>().CurrentValue && Program.Ignite.IsReady())
                    {
                        var predHealth = Prediction.Health.GetPrediction(enemy, (int)(Program.myHero.Distance(enemy.Position) * 1000 / 2000));
                        if (predHealth <= Program.myHero.GetSummonerSpellDamage(enemy, DamageLibrary.SummonerSpells.Ignite))
                        {
                            Program.Ignite.Cast(enemy);
                        }
                    }
                }
            }
        }

        public static Obj_AI_Base GetEnemy(float range, GameObjectType type)
        {
            return ObjectManager.Get<Obj_AI_Base>().Where(a => a.IsEnemy
            && a.Type == type
            && a.Distance(ObjectManager.Player) <= range
            && !a.IsDead
            && !a.IsInvulnerable
            && a.IsValidTarget(range)).FirstOrDefault();
        }

        public static void sChoose()
        {
            var style = Program.MiscMenu["sID"].DisplayName;

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