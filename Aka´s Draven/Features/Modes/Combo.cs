using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;

namespace Aka_s_Draven.Features.Modes
{
    class Combo
    {
        public static void Execute()
        {
            var target = TargetSelector.GetTarget(Manager.SpellManager.E.Range, DamageType.Physical);

            if (!target.IsValidTarget())
            {
                return;
            }

            if (Manager.MenuManager.UseQCombo && Variables.QCount < Manager.MenuManager.MaxAxes - 1 && Manager.SpellManager.Q.IsReady()
                    && Variables._Player.IsInAutoAttackRange(target) && !Variables._Player.Spellbook.IsAutoAttacking)
            {
                Manager.SpellManager.Q.Cast();
            }

            if (Manager.MenuManager.UseWCombo && Manager.SpellManager.W.IsReady()
                    && Variables._Player.ManaPercent > Manager.MenuManager.UseWMana)
            {
                if (Manager.MenuManager.UseWEverytime)
                {
                    Manager.SpellManager.W.Cast();
                }
                else
                {
                    if (!Player.HasBuff("dravenfurybuff"))
                    {
                        Manager.SpellManager.W.Cast();
                    }
                }
            }

            if (Manager.MenuManager.UseECombo && Manager.SpellManager.E.IsReady())
            {
                var EPred = Manager.SpellManager.E.GetPrediction(target);
                if (EPred.HitChance >= EloBuddy.SDK.Enumerations.HitChance.Medium)
                {
                    Manager.SpellManager.E.Cast(EPred.UnitPosition);

                }
            }

            if (Manager.MenuManager.UseRCombo && Manager.SpellManager.R.IsReady())
            {
                var killableTarget =
    EntityManager.Heroes.Enemies.Where(x => x.IsValidTarget(2000))
        .FirstOrDefault(
            x =>
            Variables._Player.GetSpellDamage(x, SpellSlot.R) * 2 > x.Health
            && (!Variables._Player.IsInAutoAttackRange(x) || Variables._Player.CountEnemiesInRange(Manager.SpellManager.E.Range) > 2));

                var RPred = Manager.SpellManager.R.GetPrediction(target);

                if (killableTarget != null && RPred.HitChance >= EloBuddy.SDK.Enumerations.HitChance.Medium)
                {
                    Manager.SpellManager.R.Cast(killableTarget);
                }
            }
        }
    }
}
 