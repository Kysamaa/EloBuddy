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
            //UseE3();
            UseR();
            Events._game.FastBotrk();
            UseTrinket(target);
            //if (MenuManager.ComboMenu["AAReset"].Cast<CheckBox>().CurrentValue) Events._game.AAReset();
        }

        public static void UseQ()
        {
            var mode = MenuManager.ComboMenu["Qmode"].Cast<Slider>().CurrentValue;
            var target = Orbwalker.LastTarget as AIHeroClient;

            if (Functions.Modes.Combo.AfterAttack && MenuManager.ComboMenu["UseQa"].Cast<CheckBox>().CurrentValue)
            {
                if (target == null) return;
                var tumblePosition = Game.CursorPos;
                switch (mode)
                {
                    case 2:
                        tumblePosition = QLogic.GetSafeTumblePos(target);
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
                        tumblePosition = QLogic.GetSafeTumblePos(target);
                        break;
                    case 1:
                        tumblePosition = Game.CursorPos;
                        break;
                }
                QLogic.Cast(tumblePosition);
            }
        }

        public static void UseQ2()
        {
            var target = Orbwalker.LastTarget as Obj_AI_Base;

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

            if (Functions.Modes.Combo.AfterAttack && MenuManager.ComboMenu["UseEa"].Cast<CheckBox>().CurrentValue)
            {
                NewELogic.Execute();

            }

            if (Functions.Modes.Combo.BeforeAttack && MenuManager.ComboMenu["UseEb"].Cast<CheckBox>().CurrentValue)
            {
                NewELogic.Execute();
            }
        }

        public static void UseE()
        {
            var mode = MenuManager.CondemnMenu["Condemnmode"].Cast<Slider>().CurrentValue;
            var target = Orbwalker.LastTarget as Obj_AI_Base;

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

        public static void UseE2()
        {
            var mode = MenuManager.CondemnMenu["Condemnmode"].Cast<Slider>().CurrentValue;
            var target = Orbwalker.LastTarget as Obj_AI_Base;

            if (target == null || !Program.E.IsReady()) return;
            if (Functions.Modes.Combo.AfterAttack && MenuManager.CondemnMenu["UseEa"].Cast<CheckBox>().CurrentValue)
            {
                switch (mode)
                {
                    case 1:
                        ELogic.Perfect();
                        break;
                    case 2:
                        ELogic.Smart();
                        break;
                    case 3:
                        ELogic.Sharpshooter();
                        break;
                    case 4:
                        ELogic.Gosu();
                        break;
                    case 5:
                        ELogic.VHR();
                        break;
                    case 6:
                        ELogic.Fastest();
                        break;
                    case 7:
                        ELogic.Legacy();
                        break;
                    case 8:
                        ELogic.Marksman();
                        break;
                    case 9:
                        ELogic.Old();
                        break;
                    case 10:
                        ELogic.Condemn1();
                        break;
                    case 11:
                        ELogic.Condemn2();
                        break;
                    case 12:
                        ELogic.Condemn3();
                        break;
                }
            }
            if (Functions.Modes.Combo.BeforeAttack && MenuManager.CondemnMenu["UseEb"].Cast<CheckBox>().CurrentValue)
            {
                switch (mode)
                {
                    case 1:
                        ELogic.Perfect();
                        break;
                    case 2:
                        ELogic.Smart();
                        break;
                    case 3:
                        ELogic.Sharpshooter();
                        break;
                    case 4:
                        ELogic.Gosu();
                        break;
                    case 5:
                        ELogic.VHR();
                        break;
                    case 6:
                        ELogic.Fastest();
                        break;
                    case 7:
                        ELogic.Legacy();
                        break;
                    case 8:
                        ELogic.Marksman();
                        break;
                    case 9:
                        ELogic.Old();
                        break;
                    case 10:
                        ELogic.Condemn1();
                        break;
                    case 11:
                        ELogic.Condemn2();
                        break;
                    case 12:
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

