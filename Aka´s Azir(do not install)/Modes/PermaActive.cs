using System;
using System.Diagnostics;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using SharpDX;
using Settings = AddonTemplate.Config.Modes.MiscMenu;

namespace AddonTemplate.Modes
{
    public sealed class PermaActive : ModeBase
    {
        private Vector2 InsecLocation;


        public override bool ShouldBeExecuted()
        {
            // Since this is permaactive mode, always execute the loop
            return true;
        }

        public override void Execute()
        {
            if (Config.Modes.Combo.UseInsec)
            {
                var qTarget = TargetSelector.GetTarget(E.Range, DamageType.Magical);
                Program.Jump(qTarget.Position);
                Rinsec();
                buildTower();
                Orbwalker.OrbwalkTo(Game.CursorPos);


            }

            if (Config.Modes.Combo.UseMechanic)
            {
                var qTarget = TargetSelector.GetTarget(E.Range, DamageType.Magical);
                Rmechanic();
                buildTower();
                Orbwalker.OrbwalkTo(Game.CursorPos);


            }

            if (Settings.KSQ || Settings.KSE || Settings.KSI && SpellManager.Ignite.IsReady())
            {
                var enemies = EntityManager.Heroes.Enemies.Where(e => e.IsValidTarget(1500.0f));
                if (Settings.KSQ && Q.IsReady())
                {
                    var target = enemies.FirstOrDefault(e => Q.IsInRange(e) && e.Health < ObjectManager.Player.GetSpellDamage(e, SpellSlot.Q));
                    if (target != null)
                    {
                        if (Orbwalker.AzirSoldiers.Count > 0)
                        {
                            foreach (var soldier in Orbwalker.AzirSoldiers) // Q KS
                            {
                                var pred = Prediction.Position.PredictLinearMissile(target, Q.Range, Q.Width,
                                Q.CastDelay, Q.Speed, Int32.MaxValue, soldier.Position, true);
                                if (pred.HitChance >= HitChance.Medium)
                                {
                                    Q.Cast(pred.CastPosition.Extend(pred.UnitPosition, 115.0f).To3D());
                                    break;
                                }
                            }
                        }
                        else if (Orbwalker.AzirSoldiers.Count == 0 && W.IsReady() && Player.Instance.Mana >= 110)
                        {
                            var wCastPos = Player.Instance.Position.Extend(target, W.Range).To3D();
                            var pred = Prediction.Position.PredictLinearMissile(target, Q.Range, Q.Width,
                                Q.CastDelay, Q.Speed, Int32.MaxValue, wCastPos, true);
                            if (pred.HitChance >= HitChance.Medium)
                            {
                                W.Cast(wCastPos);
                                Q.Cast(pred.CastPosition.Extend(pred.UnitPosition, 115.0f).To3D());
                            }
                        }
                    }
                }
                else if (Settings.KSE && E.IsReady())
                {
                    var target = enemies.FirstOrDefault(e => E.IsInRange(e) && e.Health < ObjectManager.Player.GetSpellDamage(e, SpellSlot.E));
                    if (target != null)
                    {
                        if (Orbwalker.AzirSoldiers.Count > 0)
                        {
                            foreach (var soldier in Orbwalker.AzirSoldiers) // E KS
                            {
                                if (target.Position.Between(Player.Instance.Position, soldier.Position))
                                {
                                    E2.Cast();
                                    break;
                                }
                            }
                        }
                        else if (Orbwalker.AzirSoldiers.Count == 0 && W.IsReady() && Player.Instance.Position.Distance(target) <= W.Range - 50 && Player.Instance.Mana >= 100) 
                        {
                            var wCastPos = Player.Instance.Position.Extend(target, W.Range).To3D();
                            W.Cast(wCastPos);
                            E2.Cast();
                        }
                    }
                }
                else if (Settings.KSI && SpellManager.Ignite.IsReady())
                {
                    var target =
                        enemies.FirstOrDefault(
                            e =>
                                SpellManager.Ignite.IsInRange(e) &&
                                e.Health <
                                ObjectManager.Player.GetSummonerSpellDamage(e, DamageLibrary.SummonerSpells.Ignite));
                    if (target != null)
                    {
                        SpellManager.Ignite.Cast(target);
                    }
                }
            }

        }

        private static void buildTower()
        {
            var turret =
                EntityManager.Turrets.AllTurrets
                    .Where(x => x.IsDead && x.HasBuff("AzirPassive") && x.IsInRange(Player.Instance.Position, SpellManager.Shurimaaa.Range))
                    .OrderByDescending(x => x.Distance(Player.Instance.Position))
                    .LastOrDefault();
            if (turret != null && SpellManager.Shurimaaa.IsReady())
            {
                Console.Write("Check");
                SpellManager.Shurimaaa.Cast(turret);
            }
        }

        private static void Rinsec()
        {
            var mode = Config.Modes.Combo.UseInsecMode;
            var target = TargetSelector.GetTarget(1000, DamageType.Magical);
            if (target != null)
            {
                //var targetfuturepos = Prediction.GetPrediction(target, 0.1f).UnitPosition;
                bool caninsec = Player.Instance.Distance(target) <= 300;
                switch (mode)
                {
                    case 1:
                       
                        var hero = HeroManager.Allies.Where(x => !x.IsMe && !x.IsDead).OrderByDescending(x => x.Distance(Player.Instance.Position)).LastOrDefault();
                        if (hero != null && caninsec && Player.Instance.ServerPosition.Distance(hero.Position) + 100 >= target.Distance(hero.Position))
                        {
                            var pos = Player.Instance.ServerPosition.Extend(hero.Position, 250);
                            R.Cast((Vector3) pos);
                        }
                        break;
                    case 2:
                        var turret = ObjectManager.Get<Obj_AI_Turret>().Where(x => x.IsAlly && !x.IsDead).OrderByDescending(x => x.Distance(Player.Instance.Position)).LastOrDefault();
                        if (turret != null && caninsec && Player.Instance.ServerPosition.Distance(turret.Position) + 100 >= target.Distance(turret.Position))
                        {
                            var pos = Player.Instance.Position.Extend(turret.Position, 250);
                            R.Cast((Vector3) pos);
                        }
                        break;
                    case 3:
                        if (caninsec && Player.Instance.ServerPosition.Distance(Game.CursorPos) + 100 >= target.Distance(Game.CursorPos))
                        {
                            var pos = Player.Instance.Position.Extend(Game.CursorPos, 250);
                            R.Cast((Vector3) pos);
                        }
                        break;
                }
            }
        }

        private static void Rmechanic()
        {
            var oldpos = Player.Instance.ServerPosition;
            var mode = Config.Modes.Combo.UseMechanicMode;
            var target = TargetSelector.GetTarget(1000, DamageType.Magical);
            if (target != null)
            {
                //var targetfuturepos = Prediction.GetPrediction(target, 0.1f).UnitPosition;
                bool caninsec = Player.Instance.Distance(target) <= 1200;
                switch (mode)
                {
                    case 1:
                        var hero = HeroManager.Allies.Where(x => !x.IsMe && !x.IsDead).OrderByDescending(x => x.Distance(Player.Instance.Position)).LastOrDefault();
                        if (hero != null && caninsec && Player.Instance.ServerPosition.Distance(hero.Position) + 100 >= target.Distance(hero.Position))
                        {
                            var pos = Player.Instance.ServerPosition.Extend(hero.Position, 1200);


                            Vector3 wVec = ObjectManager.Player.ServerPosition +
                               Vector3.Normalize(target.Position - ObjectManager.Player.ServerPosition) * SpellManager.W.Range;

                            if ((SpellManager.E.IsReady()) && Player.Instance.ServerPosition.Distance(pos) < SpellManager.Q.Range)
                            {
                                if (SpellManager.W.IsReady())
                                {
                                    SpellManager.W.Cast(wVec);
                                    return;
                                }
                                SpellManager.E2.Cast();
                                SpellManager.R.Cast(oldpos);
                                if (SpellManager.Q.IsReady() && Modes.Flee.GetNearestSoldierToMouse(oldpos).Position.Distance(oldpos) > 300)
                                {
                                    SpellManager.Q.Cast(oldpos);
                                    return;
                                }
                            }
                        
                }
                        break;
                    case 2:
                        var turret = ObjectManager.Get<Obj_AI_Turret>().Where(x => x.IsAlly && !x.IsDead).OrderByDescending(x => x.Distance(Player.Instance.Position)).LastOrDefault();
                        if (turret != null && caninsec && Player.Instance.ServerPosition.Distance(turret.Position) + 100 >= target.Distance(turret.Position))
                        {
                            var pos = Player.Instance.ServerPosition.Extend(turret.Position, 1200);


                            Vector3 wVec = ObjectManager.Player.ServerPosition +
                               Vector3.Normalize(target.Position - ObjectManager.Player.ServerPosition) * SpellManager.W.Range;

                            if ((SpellManager.E.IsReady()) && Player.Instance.ServerPosition.Distance(pos) < SpellManager.Q.Range)
                            {
                                if (SpellManager.W.IsReady())
                                {
                                    SpellManager.W.Cast(wVec);
                                    return;
                                }
                                SpellManager.E2.Cast();
                                SpellManager.R.Cast(oldpos);
                                if (SpellManager.Q.IsReady() && Modes.Flee.GetNearestSoldierToMouse(oldpos).Position.Distance(oldpos) > 300)
                                {
                                    SpellManager.Q.Cast(oldpos);
                                    return;
                                }
                            }
                        }
                        break;
                    case 3:
                        if (caninsec && Player.Instance.ServerPosition.Distance(Game.CursorPos) + 100 >= target.Distance(Game.CursorPos))
                        {
                            var pos = Player.Instance.ServerPosition.Extend(Game.CursorPos, 1200);


                            Vector3 wVec = ObjectManager.Player.ServerPosition +
                               Vector3.Normalize(target.Position - ObjectManager.Player.ServerPosition) * SpellManager.W.Range;

                            if ((SpellManager.E.IsReady()) && Player.Instance.ServerPosition.Distance(pos) < SpellManager.Q.Range)
                            {
                                if (SpellManager.W.IsReady())
                                {
                                    SpellManager.W.Cast(wVec);
                                    return;
                                }
                                SpellManager.E2.Cast();
                                SpellManager.R.Cast(oldpos);
                                if (SpellManager.Q.IsReady() && Modes.Flee.GetNearestSoldierToMouse(oldpos).Position.Distance(oldpos) > 300)
                                {
                                    SpellManager.Q.Cast(oldpos);
                                    return;
                                }
                            }
                        }
                        
                        break;
                }
            }
        }
    }
}



