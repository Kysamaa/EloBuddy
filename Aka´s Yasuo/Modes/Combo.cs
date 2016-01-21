using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using SharpDX;


namespace AkaYasuo.Modes
{
    internal class Combo
    {
        public static void Load()
        {
            UseQ();
            TestE();
            UseItemsaW();
            UseR();
        }

        public static bool dashing;

        public static void TestE()
        {
            var TsTarget = TargetSelector.GetTarget(1300, DamageType.Physical);

            if (TsTarget == null)
            {
                return;
            }
            if (MenuManager.ComboMenu["EC"].Cast<CheckBox>().CurrentValue && TsTarget != null)
            {
                var dmg = ((float)Variables._Player.GetSpellDamage(TsTarget, SpellSlot.Q) + (float)Variables._Player.GetSpellDamage(TsTarget, SpellSlot.E) + (float)Variables._Player.GetSpellDamage(TsTarget, SpellSlot.R));
                if (Program.E.IsReady() && TsTarget.Distance(Variables._Player) >= (MenuManager.ComboMenu["E1"].Cast<Slider>().CurrentValue) && dmg >= TsTarget.Health && Variables.UnderTower((Vector3)Variables.PosAfterE(TsTarget)) && Variables.CanCastE(TsTarget) && Variables._Player.IsFacing(TsTarget))
                {
                    Program.E.Cast(TsTarget);
                }
                else if (TsTarget.Distance(Variables._Player) >= (MenuManager.ComboMenu["E1"].Cast<Slider>().CurrentValue) && dmg <= TsTarget.Health && Variables.CanCastE(TsTarget) && Variables._Player.IsFacing(TsTarget))
                {
                    Functions.Events._game.useENormal(TsTarget);
                }
                else if (Program.Q.IsReady() && Variables.IsDashing && Variables._Player.Distance(TsTarget) <= 275 * 275)
                {
                    Core.DelayAction(() => { Program.Q.Cast(TsTarget); }, 200);
                    Core.DelayAction(Orbwalker.ResetAutoAttack, 250);
                }
                else if (Program.Q3.IsReady() && Variables._Player.Distance(TsTarget) <= Program.E.Range && Variables.Q3READY(Variables._Player) && TsTarget != null && Program.E.IsReady() && Variables.CanCastE(TsTarget))
                {
                    Program.E.Cast(TsTarget);
                }
                else if (Program.Q3.IsReady() && Variables.IsDashing && Variables._Player.Distance(TsTarget) <= 275 * 275 && Variables.Q3READY(Variables._Player))
                {
                    Core.DelayAction(() => { Program.Q.Cast(TsTarget); }, 200);
                    Core.DelayAction(Orbwalker.ResetAutoAttack, 250);
                }

                if (MenuManager.ComboMenu["E3"].Cast<CheckBox>().CurrentValue && Program.E.IsReady())
                {
                    var bestMinion =
                    ObjectManager.Get<Obj_AI_Base>()
                    .Where(x => x.IsValidTarget(Program.E.Range))
                    .Where(x => x.Distance(TsTarget) < Variables._Player.Distance(TsTarget))
                    .OrderByDescending(x => x.Distance(Variables._Player))
                    .FirstOrDefault();

                    var dmg2 = ((float)Variables._Player.GetSpellDamage(TsTarget, SpellSlot.Q) + (float)Variables._Player.GetSpellDamage(TsTarget, SpellSlot.E) + (float)Variables._Player.GetSpellDamage(TsTarget, SpellSlot.R));
                    if (bestMinion != null && TsTarget != null && dmg2 >= TsTarget.Health && !Variables.UnderTower((Vector3)Variables.PosAfterE(bestMinion)) && Variables._Player.IsFacing(bestMinion) && TsTarget.Distance(Variables._Player) >= (MenuManager.ComboMenu["E2"].Cast<Slider>().CurrentValue) && Variables.CanCastE(bestMinion) && Variables._Player.IsFacing(bestMinion))
                    {
                        Functions.Events._game.useENormal(TsTarget);
                    }
                    else if (bestMinion != null && TsTarget != null && dmg2 <= TsTarget.Health && Variables._Player.IsFacing(bestMinion) && TsTarget.Distance(Variables._Player) >= (MenuManager.ComboMenu["E2"].Cast<Slider>().CurrentValue) && Variables.CanCastE(bestMinion) && Variables._Player.IsFacing(bestMinion))
                    {
                        Program.E.Cast(bestMinion);
                    }
                }
                if (!MenuManager.ComboMenu["E3"].Cast<CheckBox>().CurrentValue && Program.E.IsReady())
                {
                    var bestMinion =
                    ObjectManager.Get<Obj_AI_Base>()
                    .Where(x => x.IsValidTarget(Program.E.Range))
                    .Where(x => x.Distance(Game.CursorPos) < Variables._Player.Distance(TsTarget))
                    .OrderByDescending(x => x.Distance(Variables._Player))
                    .FirstOrDefault();

                    var dmg3 = ((float)Variables._Player.GetSpellDamage(TsTarget, SpellSlot.Q) + (float)Variables._Player.GetSpellDamage(TsTarget, SpellSlot.E) + (float)Variables._Player.GetSpellDamage(TsTarget, SpellSlot.R));
                    if (bestMinion != null && TsTarget != null && dmg3 >= TsTarget.Health && !Variables.UnderTower((Vector3)Variables.PosAfterE(bestMinion)) && Variables._Player.IsFacing(bestMinion) && TsTarget.Distance(Variables._Player) >= (MenuManager.ComboMenu["E2"].Cast<Slider>().CurrentValue) && Variables.CanCastE(bestMinion) && Variables._Player.IsFacing(bestMinion))
                    {
                        Functions.Events._game.useENormal(TsTarget);
                    }
                    else if (bestMinion != null && TsTarget != null && dmg3 <= TsTarget.Health && Variables._Player.IsFacing(bestMinion) && TsTarget.Distance(Variables._Player) >= (MenuManager.ComboMenu["E2"].Cast<Slider>().CurrentValue) && Variables.CanCastE(bestMinion) && Variables._Player.IsFacing(bestMinion))
                    {
                        Program.E.Cast(bestMinion);
                    }
                }
            }
        }

        public static void UseE()
        {
            var TsTarget = TargetSelector.GetTarget(1300, DamageType.Physical);

            if (TsTarget == null)
            {
                return;
            }

            if (MenuManager.ComboMenu["E"].Cast<CheckBox>().CurrentValue && Program.E.IsReady())
            {
                if (Extensions.Distance(Functions.Modes.Combo.GetDashingEnd(TsTarget), TsTarget) <= Variables._Player.GetAutoAttackRange()
                    //wont e unless in AA range after
                    && TsTarget.Distance(Variables._Player) <= Program.E.Range
                    && TsTarget.Distance(Variables._Player) >= Variables._Player.GetAutoAttackRange())
                {
                    Program.E.Cast(TsTarget);
                    dashing = true;
                    if (MenuManager.ComboMenu["EQ"].Cast<CheckBox>().CurrentValue && Program.Q.IsReady())
                        Program.Q.Cast(TsTarget.Position);
                }
                else if (TsTarget.Distance(Variables._Player) > Player.Instance.GetAutoAttackRange(TsTarget))
                    //use minions to get to champ
                {
                    var minion =
                        ObjectManager.Get<Obj_AI_Minion>()
                            .OrderByDescending(m => m.Distance(Variables._Player))
                            .FirstOrDefault(
                                m =>
                                    m.IsValidTarget(Program.E.Range)
                                    && (m.Distance(TsTarget) < Variables._Player.Distance(TsTarget))
                                    && Variables._Player.IsFacing(m) && Variables.CanCastE(m) &&
                                    (!Variables.UnderTower((Vector3) Variables.PosAfterE(m))));

                    if (minion != null &&
                        (Variables.PosAfterE(minion).Distance(TsTarget) < Player.Instance.Distance(TsTarget)))
                    {
                        Console.Write("E Cast minions");
                        Program.E.Cast(minion);
                    }
                }
            }

            if (Orbwalker.CanAutoAttack)
            {
                var TsTargetaa = TargetSelector.GetTarget(Variables._Player.GetAutoAttackRange(), DamageType.Physical);

                if (TsTargetaa != null)
                    Orbwalker.ForcedTarget = TsTargetaa;
            }
            dashing = false;
        }

        public static void UseQ()
        {
            var TsTarget = TargetSelector.GetTarget(1300, DamageType.Physical);

            if (TsTarget == null)
            {
                return;
            }

            if (MenuManager.DogeMenu["smartW"].Cast<CheckBox>().CurrentValue)
            {
                Functions.Modes.Combo.putWallBehind(TsTarget);
            }
            if (MenuManager.DogeMenu["smartW"].Cast<CheckBox>().CurrentValue && Variables.wallCasted &&
                Variables._Player.Distance(TsTarget.Position) < 300)
            {
                Functions.Modes.Combo.eBehindWall(TsTarget);
            }

            if (Program.Q.IsReady() && MenuManager.ComboMenu["Q"].Cast<CheckBox>().CurrentValue)
            {
                PredictionResult QPred = Program.Q.GetPrediction(TsTarget);
                if (!Variables.isDashing && Variables.Q3READY(Variables._Player) && QPred.HitChance >= EloBuddy.SDK.Enumerations.HitChance.High)
                {
                    Program.Q.Cast(QPred.CastPosition);
                    Core.DelayAction(Orbwalker.ResetAutoAttack, 250);
                }
                else if (Variables.Q3READY(Variables._Player) && Variables.isDashing && QPred.HitChance >= EloBuddy.SDK.Enumerations.HitChance.High &&
                         Variables._Player.Distance(TsTarget) <= 250)
                {
                    Program.Q.Cast(QPred.CastPosition);
                    Core.DelayAction(Orbwalker.ResetAutoAttack, 250);
                }
                else if (!Variables.Q3READY(Variables._Player) && QPred.HitChance >= EloBuddy.SDK.Enumerations.HitChance.Medium)
                {
                    Program.Q.Cast(QPred.CastPosition);
                    Core.DelayAction(Orbwalker.ResetAutoAttack, 250);
                }
            }
        }

        public static void UseItemsaW()
        {
            var TsTarget = TargetSelector.GetTarget(1300, DamageType.Physical);

            if (TsTarget == null)
            {
                return;
            }

            if (MenuManager.ItemMenu["Items"].Cast<CheckBox>().CurrentValue && TsTarget.IsValidTarget())
            {
                Items.UseItems(TsTarget);
            }
            if (Program.Ignite != null && Program.Ignite.IsReady() &&
                MenuManager.ComboMenu["Ignite"].Cast<CheckBox>().CurrentValue)
            {
                if (TsTarget.Distance(Variables._Player) <= (600) &&
                    Variables._Player.GetSummonerSpellDamage(TsTarget, DamageLibrary.SummonerSpells.Ignite) >=
                    TsTarget.Health)
                {
                    Program.Ignite.Cast(TsTarget);
                }
            }
        }
        

        public static void UseR()
        {
            var TsTarget = TargetSelector.GetTarget(1300, DamageType.Physical);

            if (TsTarget == null)
            {
                return;
            }

            if (Program.R.IsReady() && MenuManager.ComboMenu["R"].Cast<CheckBox>().CurrentValue)
            {
                List<AIHeroClient> enemies = EntityManager.Heroes.Enemies;
                foreach (AIHeroClient enemy in enemies)
                {
                    if (Variables._Player.Distance(enemy) <= 1200)
                    {
                        var enemiesKnockedUp =
                            ObjectManager.Get<AIHeroClient>()
                                .Where(x => x.IsValidTarget(Program.R.Range))
                                .Where(x => x.HasBuffOfType(BuffType.Knockup));

                        var enemiesKnocked = enemiesKnockedUp as IList<AIHeroClient> ??
                                             enemiesKnockedUp.ToList();
                        if (enemy.IsValidTarget(Program.R.Range) &&
                            MenuManager.ComboMenu[TsTarget.ChampionName].Cast<CheckBox>().CurrentValue &&
                            Variables.CanCastDelayR(enemy) &&
                            enemiesKnocked.Count() >=
                            (MenuManager.ComboMenu["R3"].Cast<Slider>().CurrentValue))
                        {
                            Program.R.Cast();
                        }
                    }
                    if (enemy.IsValidTarget(Program.R.Range))
                    {
                        if (Variables.IsKnockedUp(enemy) && Variables.CanCastDelayR(enemy) &&
                            (enemy.Health/enemy.MaxHealth*100 <=
                             (MenuManager.ComboMenu["R2"].Cast<Slider>().CurrentValue)))
                        {
                            Program.R.Cast();
                        }
                        else if (Variables.IsKnockedUp(enemy) &&
                                 MenuManager.ComboMenu[TsTarget.ChampionName].Cast<CheckBox>().CurrentValue &&
                                 Variables.CanCastDelayR(enemy) &&
                                 enemy.HealthPercent >=
                                 (MenuManager.ComboMenu["R2"].Cast<Slider>().CurrentValue) &&
                                 (MenuManager.ComboMenu["R4"].Cast<CheckBox>().CurrentValue))
                        {
                            if (Variables.AlliesNearTarget(TsTarget, 600))
                            {
                                Program.R.Cast();
                            }
                        }
                    }
                }
            }
        }
    }
}

