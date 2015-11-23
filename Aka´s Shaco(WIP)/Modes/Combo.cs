using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu;

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
            if (Settings.UseQ)
            {
                var etarget = TargetSelector.GetTarget(E.Range, DamageType.Magical);
                var pos = QLogic.GetQPos(etarget, true);
                if (Q.IsReady() && etarget != null)
                {
                    Q.Cast(pos);
                }
            }

            if (Settings.UseR)
            {
                var target = TargetSelector.GetTarget(E.Range, DamageType.Magical);
                if (R.IsReady() && target != null && ObjectManager.Player.Distance(target) < 400 &&
                    Player.HasBuff("Deceive"))
                {
                    R.Cast();
                }
            }

            if (Settings.UseW)
            {
                var etarget = TargetSelector.GetTarget(E.Range, DamageType.Magical);
                var pos = QLogic.GetQPos(etarget, true, 100);
                if (W.IsReady() && etarget != null)
                {
                    W.Cast(pos);
                }
            }

            if (Settings.UseE)
            {
                var target = TargetSelector.GetTarget(E.Range, DamageType.Magical);
                if (E.IsReady() && target != null)
                {
                    E.Cast(target);
                }



            }
            if (!Config.Modes.MiscMenu.CloneOrbwalk) return;
            if (!hasClone()) return;
            Obj_AI_Base clone = getClone();
            var ctarget = TargetSelector.GetTarget(E.Range, DamageType.Magical);
            if (Environment.TickCount > SpellManager.cloneAct + 200)
            {
                if (ctarget != null)
                {
                    R.Cast(ctarget);
                }
                else
                {
                    R.Cast(Game.CursorPos);
                }
                SpellManager.cloneAct = Environment.TickCount;
            }
        }

        public static bool hasClone()
        {
            return Player.GetSpell(SpellSlot.R).Name.Equals("hallucinateguide");
        }

        public static Obj_AI_Base getClone()
        {
            Obj_AI_Base Clone = null;
            foreach (var unit in ObjectManager.Get<Obj_AI_Base>().Where(clone => !clone.IsMe && clone.Name == ObjectManager.Player.Name))
            {
                Clone = unit;
            }

            return Clone;

        }
    }
    }

                
            

        
    



