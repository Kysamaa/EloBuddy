using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

namespace AkaDraven.Modes
{
    partial class PermaActive
    {
        public static void CatchAxe()
        {
            var catchOption = MenuManager.AxeMenu["Qmode"].Cast<Slider>().CurrentValue;

            if (((catchOption == 1 && (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo)))
                 || (catchOption == 2 && (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.None))))
                || catchOption == 3)
            {
                var bestReticle =
                    Variables.QReticles.Where(
                        x =>
                        x.Object.Position.Distance(Game.CursorPos)
                        < MenuManager.AxeMenu["Qrange"].Cast<Slider>().CurrentValue)
                        .OrderBy(x => x.Position.Distance(Variables._Player.ServerPosition))
                        .ThenBy(x => x.Position.Distance(Game.CursorPos))
                        .ThenBy(x => x.ExpireTime)
                        .FirstOrDefault();

                if (bestReticle != null && bestReticle.Object.Position.Distance(Variables._Player.ServerPosition) > 100)
                {
                    var eta = 1000 * (Variables._Player.Distance(bestReticle.Position) / Variables._Player.MoveSpeed);
                    var expireTime = bestReticle.ExpireTime - Environment.TickCount;

                    if (eta >= expireTime && MenuManager.AxeMenu["WforQ"].Cast<CheckBox>().CurrentValue)
                    {
                        Program.W.Cast();
                    }
                    if (MenuManager.AxeMenu["Qunderturret"].Cast<CheckBox>().CurrentValue)
                    {
                        // If we're under the turret as well as the axe, catch the axe
                        if (UnderEnemyTower(Variables._Player.ServerPosition) && UnderEnemyTower(bestReticle.Object.Position))
                        {
                            if ((Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo)) || (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass)) || (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear)) || (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear)) || (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee)) || (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.None)))
                            {
                                Orbwalker.DisableMovement = false;
                                Player.IssueOrder(GameObjectOrder.MoveTo, bestReticle.Position);
                                Orbwalker.DisableMovement = true;
                            }
                            else
                            {
                                Orbwalker.DisableMovement = false;
                                Orbwalker.OrbwalkTo(bestReticle.Position);
                                Orbwalker.DisableMovement = true;
                            }
                        }

                        else if (!UnderEnemyTower(bestReticle.Object.Position))
                        {
                            if ((Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo)) || (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass)) || (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear)) || (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear)) || (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee)) || (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.None)))
                            {
                                Orbwalker.DisableMovement = false;
                                Player.IssueOrder(GameObjectOrder.MoveTo, bestReticle.Position);
                                Orbwalker.DisableMovement = true;
                            }
                            else
                            {
                                Orbwalker.DisableMovement = false;
                                Orbwalker.OrbwalkTo(bestReticle.Position);
                                Orbwalker.DisableMovement = true;
                            }
                        }
                        else
                        {
                            if ((Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo)) || (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass)) || (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear)) || (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear)) || (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee)) || (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.None)))
                            {
                                Orbwalker.DisableMovement = false;
                                Player.IssueOrder(GameObjectOrder.MoveTo, bestReticle.Position);
                                Orbwalker.DisableMovement = true;
                            }
                            else
                            {
                                Orbwalker.DisableMovement = false;
                                Orbwalker.OrbwalkTo(bestReticle.Position);
                                Orbwalker.DisableMovement = true;
                            }
                        }
                    }
                }
            }
        }
            
        
                                                             
        public static bool UnderEnemyTower(Vector3 pos)
        {
            return EntityManager.Turrets.Enemies.Where(a => a.Health > 0 && !a.IsDead).Any(a => a.Distance(pos) < 950);
        }

        public static void KillSteal()
        {
            foreach (AIHeroClient enemy in EntityManager.Heroes.Enemies)
            {
                if (enemy.IsValidTarget(Program.Q.Range))
                {
                    if (Program.E.IsReady() && MenuManager.KillStealMenu["KsE"].Cast<CheckBox>().CurrentValue && (Variables._Player.GetSpellDamage(enemy, SpellSlot.E) >= enemy.Health))
                    {
                        var predHealth = Prediction.Health.GetPrediction(enemy, (int)(Variables._Player.Distance(enemy.Position) * 1000 / 2000));
                        if (predHealth <= Variables._Player.GetSpellDamage(enemy, SpellSlot.E))
                        {
                            Program.E.Cast(enemy);
                        }
                    }
                    if (Program.Ignite != null && MenuManager.KillStealMenu["KsIgnite"].Cast<CheckBox>().CurrentValue && Program.Ignite.IsReady())
                    {
                        var predHealth = Prediction.Health.GetPrediction(enemy, (int)(Variables._Player.Distance(enemy.Position) * 1000 / 2000));
                        if (predHealth <= Variables._Player.GetSummonerSpellDamage(enemy, DamageLibrary.SummonerSpells.Ignite))
                        {
                            Program.Ignite.Cast(enemy);
                        }
                    }
                }
            }
        }

    }
}
