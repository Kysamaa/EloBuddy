using System.Data;
using System.Linq;
using AddonTemplate.Logic;
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
                QLogic2.Cast(Game.CursorPos);
            }
            if (Settings.UseQp && SpellManager.Q.IsReady())
            {
                QLogic2.Cast(target.GetTumblePos());
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
            if (Config.Modes.Condemn.trinket)
            {
                usetrinket(target);
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

        public static void usetrinket(Obj_AI_Base target)
        {
            if (ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Trinket).IsReady &&
                ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Trinket).SData.Name.ToLower().Contains("totem"))
            {
                Core.DelayAction(delegate
                {
                    if (Config.Modes.Condemn.trinket)
                    {
                        var pos = ELogic.GetFirstNonWallPos(ObjectManager.Player.Position.To2D(), target.Position.To2D());
                        if (NavMesh.GetCollisionFlags(pos).HasFlag(CollisionFlags.Grass))
                        {
                            Player.CastSpell(SpellSlot.Trinket,
                                pos.To3D());
                        }
                    }
                }, 200);
            }
        }

    }

}

                
            

        
    



