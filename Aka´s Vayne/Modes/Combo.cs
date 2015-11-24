using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
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
            var target = TargetSelector.GetTarget(E.Range, DamageType.Physical);
            var myPosition = Game.CursorPos;
            if (Settings.UseQa  && SpellManager.Q.IsReady())
            {
                QLogic.CastTumble(myPosition, target);
                Qisoutofrange(target);
            }
            if (Settings.UseQs && SpellManager.Q.IsReady())
            {
                QLogic.QCombo(target);
                Qisoutofrange(target);
            }
            if (Config.Modes.Condemn.Condemn1 && SpellManager.E.IsReady())
            {
                ELogic.Condemn1();
            }
            if (Config.Modes.Condemn.Condemn2 && SpellManager.E.IsReady())
            {
                ELogic.Condemn2();
            }
            if (Config.Modes.Condemn.Condemn3 && SpellManager.E.IsReady())
            {
                ELogic.Condemn3();
            }
            if (Settings.UseR && R.IsReady())
            {
                ComboUltimateLogic();
            }
            if (Settings.Ekill && E.IsReady())
            {
                Ekill();
            }
        }
        public void ComboUltimateLogic()
        {
            if (ObjectManager.Player.CountEnemiesInRange(1000) >= Settings.UseRSlider)
            {
                SpellManager.R.Cast();
            }
        }

        public static void Ekill()
        {
            var target = TargetSelector.GetTarget((int)ObjectManager.Player.GetAutoAttackRange(), DamageType.Physical);

            if (target.Health <
                Player.Instance.GetSpellDamage(target, SpellSlot.E))
            {
                SpellManager.E.Cast(target);
            }
        }

        public static void Qisoutofrange(Obj_AI_Base target)
        {
            if (SpellManager.Q.IsReady() && Player.Instance.Distance(target) > Player.Instance.GetAutoAttackRange(target) &&
                Player.Instance.Distance(target) < Player.Instance.GetAutoAttackRange(target) + 300)
            {
                Player.CastSpell(SpellSlot.Q, Game.CursorPos);
                return;
            }
        }
    }

}

                
            

        
    



