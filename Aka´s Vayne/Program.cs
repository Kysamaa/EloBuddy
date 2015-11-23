using System;
using System.Diagnostics;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using SharpDX;
using SettingsMisc = AddonTemplate.Config.Modes.MiscMenu;
using SettingsDraw = AddonTemplate.Config.Modes.Drawing;

namespace AddonTemplate
{
    public static class Program
    {
        // Change this line to the champion you want to make the addon for,
        // watch out for the case being correct!
        public const string ChampName = "Vayne";

        public static void Main(string[] args)
        {
            // Wait till the loading screen has passed
            Loading.OnLoadingComplete += OnLoadingComplete;
            Gapcloser.OnGapcloser += Events.Gapcloser_OnGapCloser;
            Interrupter.OnInterruptableSpell += Events.Interrupter_OnInterruptableSpell;
            Obj_AI_Base.OnBasicAttack += Events.ObjAiBaseOnOnBasicAttack;
            GameObject.OnCreate += Events.GameObject_OnCreate;
        }

        private static void OnLoadingComplete(EventArgs args)
        {
            // Verify the champion we made this addon for
            if (Player.Instance.ChampionName != ChampName)
            {
                // Champion is not the one we made this addon for,
                // therefore we return
                return;
            }

            // Initialize the classes that we need
            Config.Initialize();
            SpellManager.Initialize();
            ModeManager.Initialize();

            // Listen to events we need
            Drawing.OnDraw += OnDraw;
            Obj_AI_Base.OnSpellCast += Obj_AI_Base_OnSpellCast;
        }

        private static void OnDraw(EventArgs args)
        {
            if (SettingsDraw.UseE)
            {
                if (!(SettingsDraw.DrawOnlyReady && !SpellManager.E.IsReady()))
                {
                    Circle.Draw(Color.Red, SpellManager.E.Range, Player.Instance.Position);
                }
            }
            if (SettingsDraw.UseQ)
            {
                if (!(SettingsDraw.DrawOnlyReady && !SpellManager.Q.IsReady()))
                {
                    Circle.Draw(Color.Red, SpellManager.Q.Range, Player.Instance.Position);
                }
            }

        }
        private static void Obj_AI_Base_OnSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            var target = (Obj_AI_Base)args.Target;

            if ((Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit) &&
                 Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear) &&
                 Config.Modes.LaneClear.UseQ) && SpellManager.Q.IsReady())
            {
                var source =
                    EntityManager.MinionsAndMonsters.EnemyMinions
                        .Where(
                            a =>
                                a.NetworkId != target.NetworkId &&
                                a.Distance(Player.Instance) < 300 + Player.Instance.GetAutoAttackRange(a) &&
                                Prediction.Health.GetPrediction(a, (int)Player.Instance.AttackDelay) <
                                Player.Instance.GetAutoAttackDamage(a, true) + Damages.QDamage(a))
                        .OrderBy(a => a.Health)
                        .FirstOrDefault();

                if (source == null || Player.Instance.Position.Extend(Game.CursorPos, 300).Distance(source) >
                    Player.Instance.GetAutoAttackRange(source))
                    return;
                Orbwalker.ForcedTarget = source;
                Player.CastSpell(SpellSlot.Q,
                    Player.Instance.Position.Extend(Game.CursorPos, 300).Distance(source) <=
                    Player.Instance.GetAutoAttackRange(source)
                        ? Game.CursorPos
                        : source.Position);
            }
        }
    }
}
