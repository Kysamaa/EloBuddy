using System;
using System.Diagnostics;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu;
using Settings = AddonTemplate.Config.Modes.MiscMenu;
using AddonTemplate.Utility;

namespace AddonTemplate.Modes
{
    public sealed class PermaActive : ModeBase
    {
        int currentSkin = 0;

        public override bool ShouldBeExecuted()
        {
            // Since this is permaactive mode, always execute the loop
            return true;
        }

        public override void Execute()
        {
            skinChanger();

            if (Player.Instance.IsDead)
            {
                return;
            }

            var enemies =
                EntityManager.Heroes.Enemies.Where(
                    e => e.IsEnemy && e.IsVisible && !e.IsDead && !e.IsZombie && !e.IsInvulnerable && e.Health > 0)
                    .ToList();
            foreach (var enemy in enemies)
            {
                if (Settings.KSQ && SpellManager.Q.IsReady() && Damages.QDamage(enemy) > enemy.Health &&
                    SpellManager.Q.IsInRange(enemy))
                {
                    if (enemy.HasBuffOfType(BuffType.SpellImmunity) || enemy.HasBuffOfType(BuffType.SpellShield))
                    {
                        continue;
                    }
                    if (Settings.KSQ)
                    {
                        Q.Cast(enemy.Position);
                        break;
                    }
                }

                if (Settings.KSW && SpellManager.W.IsReady() && Damages.WDamage(enemy) > enemy.Health &&
                    SpellManager.W.IsInRange(enemy))
                {
                    if (enemy.HasBuffOfType(BuffType.SpellImmunity) || enemy.HasBuffOfType(BuffType.SpellShield))
                    {
                        continue;
                    }
                    if (Settings.KSW)
                    {
                        W.Cast(enemy.Position);
                        break;
                    }
                }
                if (Settings.KSE && SpellManager.E.IsReady() && ObjectManager.Player.GetSpellDamage(enemy, SpellSlot.E) > enemy.Health &&
                    SpellManager.E.IsInRange(enemy))
                {
                    if (enemy.HasBuffOfType(BuffType.SpellImmunity) || enemy.HasBuffOfType(BuffType.SpellShield))
                    {
                        continue;
                    }
                    if (Settings.KSE)
                    {
                        E.Cast(enemy);
                        break;
                    }
                }
                if (Settings.KSR && SpellManager.R.IsReady() && Damages.RDamage(enemy) > enemy.Health &&
                    SpellManager.R.IsInRange(enemy))
                {
                    if (enemy.HasBuffOfType(BuffType.SpellImmunity) || enemy.HasBuffOfType(BuffType.SpellShield))
                    {
                        continue;
                    }
                    if (Settings.KSR)
                    {
                        R.Cast(enemy);
                        break;
                    }
                }

                if (!Settings.CloneOrbwalk) return;
                if (!hasClone()) return;
                var target = TargetSelector.GetTarget(E.Range, DamageType.Physical);

                    if (target != null)
                    {
                    Core.DelayAction(() => R.Cast(target), 500);

                }
                    else
                    {
                    Core.DelayAction(() => R.Cast(Game.CursorPos), 500);
                }
                }


            }
        private void skinChanger()
        {
            if (Settings.skinId != currentSkin)
            {
                Player.Instance.SetSkinId(Settings.skinId);
                currentSkin = Settings.skinId;
            }
        }



        public static bool hasClone()
        {
            return Player.GetSpell(SpellSlot.R).Name.Equals("hallucinateguide");
        }
    }
    }


