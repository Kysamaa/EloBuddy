using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace AkaYasuo.Events
{
    class _game
    {
        public static void AutoQ()
        {
            if (MenuManager.HarassMenu["AutoQ"].Cast<KeyBind>().CurrentValue &&
               (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.None)))
            {
                var TsTarget = TargetSelector.GetTarget(Program.Q.Range, DamageType.Physical);

                if (TsTarget == null)
                {
                    return;
                }

                if (MenuManager.HarassMenu["QunderTower"].Cast<CheckBox>().CurrentValue)
                {
                    PredictionResult QPred = Program.Q.GetPrediction(TsTarget);

                    if (Program.Q3.IsReady() && Variables.Q3READY(Variables._Player) &&
                        MenuManager.HarassMenu["AutoQ3"].Cast<CheckBox>().CurrentValue && !Variables.isDashing)
                    {
                        Program.Q.Cast(TsTarget.Position);
                        Core.DelayAction(Orbwalker.ResetAutoAttack, 250);
                    }
                    else if (!Variables.Q3READY(Variables._Player) && Program.Q.IsReady() && !Variables.Q3READY(Variables._Player) &&
                             MenuManager.HarassMenu["Q"].Cast<CheckBox>().CurrentValue && !Variables.isDashing)
                    {
                        Program.Q.Cast(TsTarget.Position);
                        Core.DelayAction(Orbwalker.ResetAutoAttack, 250);
                    }
                }
                else if (!MenuManager.HarassMenu["QunderTower"].Cast<CheckBox>().CurrentValue)
                {
                    if (!Variables.UnderTower(Variables._Player.ServerPosition) && Program.Q3.IsReady() && Variables.Q3READY(Variables._Player) &&
                        MenuManager.HarassMenu["AutoQ3"].Cast<CheckBox>().CurrentValue && !Variables.isDashing)
                    {
                        Program.Q.Cast(TsTarget.Position);
                        Core.DelayAction(Orbwalker.ResetAutoAttack, 250);
                    }
                    if (!Variables.Q3READY(Variables._Player) && Program.Q.IsReady() && !Variables.Q3READY(Variables._Player) &&
                        MenuManager.HarassMenu["Q"].Cast<CheckBox>().CurrentValue && !Variables.IsDashing &&
                        !Variables.UnderTower(Variables._Player.ServerPosition))
                    {
                        Program.Q.Cast(TsTarget.Position);
                        Core.DelayAction(Orbwalker.ResetAutoAttack, 250);
                    }
                }
            }
        }

        public static void AutoQMinion()
        {
            if (MenuManager.HarassMenu["AutoQMinion"].Cast<KeyBind>().CurrentValue &&
               (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.None)))
            {
                foreach (Obj_AI_Base TsTarget in EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Variables._Player.Position, Program.Q.Range, true).OrderByDescending(m => m.Health))
                {

                    if (TsTarget == null)
                    {
                        return;
                    }
                    if (!Variables.Q3READY(Variables._Player) && Program.Q.IsReady() && !Variables.Q3READY(Variables._Player) &&
                             MenuManager.HarassMenu["Q"].Cast<CheckBox>().CurrentValue && !Variables.isDashing)
                    {
                        Program.Q.Cast(TsTarget.Position);
                        Core.DelayAction(Orbwalker.ResetAutoAttack, 250);
                    }
                }
            }
        }

        public static void LevelUpSpells()
        {
            if (!MenuManager.MiscMenu["autolvl"].Cast<CheckBox>().CurrentValue) return;

            var qL = Variables._Player.Spellbook.GetSpell(SpellSlot.Q).Level + Variables.QOff;
            var wL = Variables._Player.Spellbook.GetSpell(SpellSlot.W).Level + Variables.WOff;
            var eL = Variables._Player.Spellbook.GetSpell(SpellSlot.E).Level + Variables.EOff;
            var rL = Variables._Player.Spellbook.GetSpell(SpellSlot.R).Level + Variables.ROff;
            if (qL + wL + eL + rL >= Variables._Player.Level) return;
            int[] level = { 0, 0, 0, 0 };
            for (var i = 0; i < Variables._Player.Level; i++)
            {
                level[Variables.abilitySequence[i] - 1] = level[Variables.abilitySequence[i] - 1] + 1;
            }
            if (qL < level[0]) Variables._Player.Spellbook.LevelSpell(SpellSlot.Q);
            if (wL < level[1]) Variables._Player.Spellbook.LevelSpell(SpellSlot.W);
            if (eL < level[2]) Variables._Player.Spellbook.LevelSpell(SpellSlot.E);
            if (rL < level[3]) Variables._Player.Spellbook.LevelSpell(SpellSlot.R);
        }

        public static void Startdash()
        {
            if (Variables.startDash + 470000 / ((700 + Variables._Player.MoveSpeed)) < Environment.TickCount && Variables.isDashing)
            {
                Variables.IsDashing = false;
            }
        }

        public static void Skillshotdetector()
        {
            Variables.EvadeDetectedSkillshots.RemoveAll(skillshot => !skillshot.IsActive());

            foreach (var mis in Variables.EvadeDetectedSkillshots)
            {
                if (MenuManager.DogeMenu["smartW"].Cast<CheckBox>().CurrentValue && !Functions.Events._game.isSafePoint(Variables._Player.Position.To2D(), true).IsSafe)
                {
                    Functions.Events._game.useWSmart(mis);
                }

                if (MenuManager.DogeMenu["smartEDogue"].Cast<CheckBox>().CurrentValue && !Functions.Events._game.isSafePoint(Variables._Player.Position.To2D(), true).IsSafe)
                {
                    Functions.Events._game.useEtoSafe(mis);
                }
            }
        }

        public static void updateSkillshots()
        {
            foreach (var ss in Variables.EvadeDetectedSkillshots)
            {
                ss.Game_OnGameUpdate();
            }
        }
    }
}
