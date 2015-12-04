using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

// Using the config like this makes your life easier, trust me
using Settings = AddonTemplate.Config.Modes.Combo;

namespace AddonTemplate.Modes
{
    public sealed class Combo : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            // Only execute this mode when the orbwalker is on combo mode
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
        }

        public override void Execute()
        {
            if (Settings.UseQ1)
            {
                var etarget = TargetSelector.GetTarget(E.Range, DamageType.Physical);
                {
                    if (Q.IsReady() && etarget != null && ObjectManager.Player.CountEnemiesInRange(2000f) <= Settings.UseQ1Slider)
                    {
                        var pos = QLogic.GetQPos(etarget, true);
                        Q.Cast(pos);
                    }
                }
            }

            if (Settings.UseQ)
            {
                var etarget = TargetSelector.GetTarget(1000f, DamageType.Physical);
                {
                    if (Q.IsReady() && etarget != null && ObjectManager.Player.CountEnemiesInRange(2000f) >= Settings.UseQ1Slider)
                    {
                        Q2.Cast(etarget.Position);
                    }
                    if (Q.IsReady() && etarget != null && !Settings.UseQ1)
                    {
                        Q2.Cast(etarget.Position);
                    }
                }
            }

            if (Settings.UseR && R.IsReady())
                {
                    var target = TargetSelector.GetTarget(E.Range, DamageType.Magical);
                    if (target != null && ObjectManager.Player.Distance(target) < 400 &&
                    ObjectManager.Player.HasBuff("Deceive"))
                {
                    R.Cast(target);
                }

            }
            if (Settings.UseW)
            {
                var wtarget = TargetSelector.GetTarget(W.Range, DamageType.Physical);
                {
                    if (W.IsReady() && wtarget != null)
                    {
                        var pos = QLogic.GetQPos(wtarget, true, 100);
                        W.Cast(pos);
                    }
                }
            }
            if (Settings.UseE)
            {
                var wtarget = TargetSelector.GetTarget(E.Range, DamageType.Physical);
                {
                    if (E.IsReady() && wtarget != null)
                    {
                        E.Cast(wtarget);

                    }
                }
            }
            if (!Config.Modes.MiscMenu.CloneOrbwalk) return;
            if (!PermaActive.hasClone()) return;
            Program.clone = Flee.getClone();


        }
    }
        }
    












