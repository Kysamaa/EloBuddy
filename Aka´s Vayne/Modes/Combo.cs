using System.Data;
using System.Linq;
using AddonTemplate.Logic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu.Values;
using SharpDX;
using AddonTemplate.Utility;
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
            if (Config.Modes.Items.Useitems)
            {
                if (SpellManager.Heal != null && Config.Modes.Items.UseHeal && SpellManager.Heal.IsReady() && Player.Instance.HealthPercent <= Config.Modes.Items.UseHealhp
                     && ObjectManager.Player.CountEnemiesInRange2(600) > 0)
                {
                    SpellManager.Heal.Cast();
                }
                if (Config.Modes.Items.Useitems)
                {
                    if (Item.HasItem((int)ItemId.Blade_of_the_Ruined_King, ObjectManager.Player) && Config.Modes.Items.Usebotrk && Item.CanUseItem((int)ItemId.Blade_of_the_Ruined_King)
                        && Player.Instance.HealthPercent <= Config.Modes.Items.Usebotrkhp)
                    {
                        Item.UseItem((int)ItemId.Blade_of_the_Ruined_King, target);
                    }
                    if (Item.HasItem((int)ItemId.Bilgewater_Cutlass, ObjectManager.Player) && Config.Modes.Items.Usebilge && Item.CanUseItem((int)ItemId.Bilgewater_Cutlass)
                       && target.IsValidTarget(ObjectManager.Player.GetAutoAttackRange()))
                    {
                        Item.UseItem((int)ItemId.Bilgewater_Cutlass, target);
                    }
                    if (Item.HasItem((int)ItemId.Youmuus_Ghostblade, ObjectManager.Player) && Config.Modes.Items.UseYoumuus && Item.CanUseItem((int)ItemId.Youmuus_Ghostblade)
                       && ObjectManager.Player.Distance4(target.Position) <= ObjectManager.Player.GetAutoAttackRange())
                    {
                        Item.UseItem((int)ItemId.Youmuus_Ghostblade);
                    }
                }
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
        }

        public void ComboUltimateLogic()
        {
            if (ObjectManager.Player.CountEnemiesInRange(1000) >= Settings.UseRSlider)
            {
                SpellManager.R.Cast();
            }
        }

        public static void Qisoutofrange(Obj_AI_Base target)
        {
            if (SpellManager.Q.IsReady() &&
                Player.Instance.Distance(target) > Player.Instance.GetAutoAttackRange(target) &&
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

                
            

        
    



