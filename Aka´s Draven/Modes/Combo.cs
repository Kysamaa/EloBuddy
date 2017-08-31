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
    internal class Combo
    {
        public static void Execute()
        {
            var target = TargetSelector.GetTarget(Program.E.Range, DamageType.Physical);

            if (!target.IsValidTarget())
            {
                return;
            }

            var useQ = MenuManager.ComboMenu["Q"].Cast<CheckBox>().CurrentValue;
            var useW = MenuManager.ComboMenu["W"].Cast<CheckBox>().CurrentValue;
            var useE = MenuManager.ComboMenu["E"].Cast<CheckBox>().CurrentValue;
            var useR = MenuManager.ComboMenu["R"].Cast<CheckBox>().CurrentValue;

            if (useQ && Variables.QCount < MenuManager.AxeMenu["Qmax"].Cast<Slider>().CurrentValue - 1 && Program.Q.IsReady()
                && Variables._Player.IsInAutoAttackRange(target) && !Variables._Player.Spellbook.IsAutoAttacking)
            {
                Program.Q.Cast();
            }

            if (useW && Program.W.IsReady()
                && Variables._Player.ManaPercent > MenuManager.MiscMenu["WMana"].Cast<Slider>().CurrentValue)
            {
                if (MenuManager.MiscMenu["UseWInstant"].Cast<CheckBox>().CurrentValue)
                {
                    Program.W.Cast();
                }
                else
                {
                    if (!Player.HasBuff("dravenfurybuff"))
                    {
                        Program.W.Cast();
                    }
                }
            }

            if (useE && Program.E.IsReady())
            {
                Program.E.Cast(target);
            }

            if (!useR || !Program.R.IsReady())
            {
                return;
            }

            // Patented Advanced Algorithms D321987
            var killableTarget =
                EntityManager.Heroes.Enemies.Where(x => x.IsValidTarget(2000))
                    .FirstOrDefault(
                        x =>
                        Variables._Player.GetSpellDamage(x, SpellSlot.R) * 2 > x.Health
                        && (!Variables._Player.IsInAutoAttackRange(x) || Variables._Player.CountEnemiesInRange(Program.E.Range) > 2));

            if (killableTarget != null)
            {
                Program.R.Cast(killableTarget);
            }

            if (MenuManager.ItemMenu["Items"].Cast<CheckBox>().CurrentValue && target.IsValidTarget())
            {
                Items.UseItems(target);
            }
        }
    }
}

