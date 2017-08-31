using EloBuddy;
using AddonTemplate.Logic;
using Aka_s_Vayne_reworked.Logic;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

namespace Aka_s_Vayne_reworked.Modes
{
    internal class Combo
    {

        public static void Load()
        {
            var target = TargetSelector.GetTarget((int)Variables._Player.GetAutoAttackRange(),
    DamageType.Physical);

            UseQ2();
            UseE3();
            UseR();
            Events._game.FastBotrk();
            UseTrinket(target);
            if (MenuManager.ComboMenu["AAReset"].Cast<CheckBox>().CurrentValue) Events._game.AAReset();
        }

        public static void UseQ2()
        {
            var target = TargetSelector.GetTarget((int)Variables._Player.GetAutoAttackRange(), DamageType.Physical);

            if (Functions.Modes.Combo.AfterAttack && MenuManager.ComboMenu["UseQa"].Cast<CheckBox>().CurrentValue)
            {
                if (target == null) return;
                QLogic.PreCastTumble(target);

            }

            if (Functions.Modes.Combo.BeforeAttack && MenuManager.ComboMenu["UseQb"].Cast<CheckBox>().CurrentValue)
            {
                if (target == null) return;
                QLogic.PreCastTumble(target);
            }
        }

        public static void UseE3()
        {
            if (Functions.Modes.Combo.AfterAttack && MenuManager.ComboMenu["comboUseE"].Cast<CheckBox>().CurrentValue)
            {
                NewELogic.Execute();
            }
        }

        public static void UseR()
        {
            if (MenuManager.ComboMenu["comboUseR"].Cast<CheckBox>().CurrentValue && Program.R.IsReady())
            {
                Functions.Modes.Combo.ComboUltimateLogic();
            }
        }

        public static void UseTrinket(Obj_AI_Base target)
        {
            if (target == null)
            {
                return;
            }
            if (Variables._Player.Spellbook.GetSpell(SpellSlot.Trinket).IsReady &&
                Variables._Player.Spellbook.GetSpell(SpellSlot.Trinket).SData.Name.ToLower().Contains("totem"))
            {
                Core.DelayAction(delegate
                {
                    if (MenuManager.CondemnMenu["trinket"].Cast<CheckBox>().CurrentValue)
                    {
                        var pos = Mechanics.GetFirstNonWallPos(Variables._Player.Position.To2D(), target.Position.To2D());
                        if (NavMesh.GetCollisionFlags(pos).HasFlag(CollisionFlags.Grass))
                        {
                            Program.totem.Cast(pos.To3D());
                        }
                    }
                }, 200);
            }

        }
    }
}

