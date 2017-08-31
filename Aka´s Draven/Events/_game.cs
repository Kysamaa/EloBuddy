using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace AkaDraven.Events
{
    class _game
    {
        public static void OnUpdate()
        {
            Variables.QReticles.RemoveAll(x => x.Object.IsDead);
            var canMove = false;
            var canAttack = false;
            foreach (var a in Variables.QReticles)
            {
                if (!a.CanOrbwalkWithUserDelay)
                {
                    canMove = true;
                }
                if (!a.CanAttack)
                {
                    canAttack = true;
                }
            }
            
            Orbwalker.DisableAttacking = canAttack;
            Orbwalker.DisableMovement = canMove;
            if (canMove)
            {
                Modes.PermaActive.CatchAxe();
            }
            if (Program.W.IsReady() && MenuManager.MiscMenu["UseWSlow"].Cast<CheckBox>().CurrentValue && Variables._Player.HasBuffOfType(BuffType.Slow))
            {
                Program.W.Cast();
            }

            if (MenuManager.HarassMenu["AutoE"].Cast<KeyBind>().CurrentValue)
            {
                Modes.Harass.Execute();
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
    }
}
