using EloBuddy;
using AddonTemplate.Logic;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace Aka_s_Vayne_reworked.Modes
{
    internal class Combo
    {

        public static void Load()
        {
            var target = TargetSelector.GetTarget((int) Variables._Player.GetAutoAttackRange() + 300,
                DamageType.Physical);

            UseQ();
            UseE();
            UseR();
            UseTrinket(target);
            Events._game.FastBotrk();
            if (MenuManager.ComboMenu["AAReset"].Cast<CheckBox>().CurrentValue) Events._game.AAReset();
        }

        public static void UseQ()
        {
            var mode = MenuManager.ComboMenu["Qmode"].Cast<Slider>().CurrentValue;
            var target = TargetSelector.GetTarget((int) Variables._Player.GetAutoAttackRange() + 300,
                DamageType.Physical);

            if (Functions.Modes.Combo.AfterAttack && MenuManager.ComboMenu["UseQa"].Cast<CheckBox>().CurrentValue)
            {
                if (target == null) return;
                var tumblePosition = Game.CursorPos;
                switch (mode)
                {
                    case 2:
                        tumblePosition = target.GetTumblePos();
                        break;
                    case 1:
                        tumblePosition = Game.CursorPos;
                        break;
                }
                QLogic.Cast(tumblePosition);
            }

            if (Functions.Modes.Combo.BeforeAttack && MenuManager.ComboMenu["UseQb"].Cast<CheckBox>().CurrentValue)
            {
                if (target == null) return;
                var tumblePosition = Game.CursorPos;
                switch (mode)
                {
                    case 2:
                        tumblePosition = target.GetTumblePos();
                        break;
                    case 1:
                        tumblePosition = Game.CursorPos;
                        break;
                }
                QLogic.Cast(tumblePosition);
            }
        }

        public static void UseE()
        {
            var mode = MenuManager.CondemnMenu["Condemnmode"].Cast<Slider>().CurrentValue;
            var target = TargetSelector.GetTarget((int) Variables._Player.GetAutoAttackRange() + 300,
                DamageType.Physical);

            if (target == null) return;
            if (Functions.Modes.Combo.AfterAttack && MenuManager.CondemnMenu["UseEa"].Cast<CheckBox>().CurrentValue)
            {
                switch (mode)
                {
                    case 1:
                        ELogic.Condemn1();
                        break;
                    case 2:
                        ELogic.Condemn2();
                        break;
                    case 3:
                        ELogic.Condemn3();
                        break;
                }
            }
            if (Functions.Modes.Combo.BeforeAttack && MenuManager.CondemnMenu["UseEb"].Cast<CheckBox>().CurrentValue)
            {
                switch (mode)
                {
                    case 1:
                        ELogic.Condemn1();
                        break;
                    case 2:
                        ELogic.Condemn2();
                        break;
                    case 3:
                        ELogic.Condemn3();
                        break;
                }
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
                        var pos = ELogic.GetFirstNonWallPos(Variables._Player.Position.To2D(), target.Position.To2D());
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

