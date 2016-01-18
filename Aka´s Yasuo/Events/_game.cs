using System;
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
               (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.None)) ||
               (!Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo)) || (!Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee)))
            {
                var TsTarget = TargetSelector.GetTarget(Program.Q.Range, DamageType.Physical);
                Orbwalker.ForcedTarget = TsTarget;

                if (TsTarget == null)
                {
                    return;
                }

                if (MenuManager.HarassMenu["QunderTower"].Cast<CheckBox>().CurrentValue)
                {
                    PredictionResult QPred = Program.Q.GetPrediction(TsTarget);

                    if (Program.Q.IsReady() && Program.Q.Range == 1000 && QPred.HitChance >= EloBuddy.SDK.Enumerations.HitChance.High &&
                        MenuManager.HarassMenu["Q3"].Cast<CheckBox>().CurrentValue && !Variables.isDashing)
                    {
                        Program.Q.Cast(QPred.CastPosition);
                    }
                    else if (!Variables.Q3READY(Variables._Player) && Program.Q.IsReady() && Program.Q.Range == 475 && QPred.HitChance >= EloBuddy.SDK.Enumerations.HitChance.Medium &&
                             MenuManager.HarassMenu["Q"].Cast<CheckBox>().CurrentValue && !Variables.isDashing)
                    {
                        Program.Q.Cast(QPred.CastPosition);
                    }
                }
                else if (!MenuManager.HarassMenu["QunderTower"].Cast<CheckBox>().CurrentValue)
                {
                    PredictionResult QPred = Program.Q.GetPrediction(TsTarget);

                    if (!Variables.UnderTower(Variables._Player.ServerPosition) && Program.Q.IsReady() && Program.Q.Range == 1000 && QPred.HitChance >= EloBuddy.SDK.Enumerations.HitChance.High &&
                        MenuManager.HarassMenu["Q3"].Cast<CheckBox>().CurrentValue && !Variables.isDashing)
                    {
                        Program.Q.Cast(QPred.CastPosition);
                    }
                    if (!Variables.Q3READY(Variables._Player) && Program.Q.IsReady() && Program.Q.Range == 475 && QPred.HitChance >= EloBuddy.SDK.Enumerations.HitChance.Medium &&
                        MenuManager.HarassMenu["Q"].Cast<CheckBox>().CurrentValue && !Variables.IsDashing &&
                        !Variables.UnderTower(Variables._Player.ServerPosition))
                    {
                        Program.Q.Cast(QPred.CastPosition);
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
