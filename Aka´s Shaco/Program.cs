using System;
using System.Diagnostics;
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
        public const string ChampName = "Shaco";

        public static Obj_AI_Base clone;

        public static void Main(string[] args)
        {
            // Wait till the loading screen has passed
            Loading.OnLoadingComplete += OnLoadingComplete;
            Obj_AI_Base.OnProcessSpellCast += Events.OnProcessSpellCast;
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

        }

        private static void OnDraw(EventArgs args)
        {
            if (SettingsDraw.UseQ)
            {
                if (!(SettingsDraw.DrawOnlyReady && !SpellManager.Q.IsReady()))
                {
                        Circle.Draw(Color.Red, SpellManager.Q.Range, Player.Instance.Position);
                }
            }
            if (SettingsDraw.UseW)
            {
                if (!(SettingsDraw.DrawOnlyReady && !SpellManager.W.IsReady()))
                {
                        Circle.Draw(Color.Red, SpellManager.W.Range, Player.Instance.Position);                    
                }
            }
            if (SettingsDraw.UseE)
            {
                if (!(SettingsDraw.DrawOnlyReady && !SpellManager.E.IsReady()))
                {
                    Circle.Draw(Color.Red, SpellManager.E.Range, Player.Instance.Position);                  
                }
            }
            if (SettingsDraw.UseR)
            {
                if (!(SettingsDraw.DrawOnlyReady && !SpellManager.R.IsReady()))
                {
                    Circle.Draw(Color.Red, SpellManager.R.Range, Player.Instance.Position);
                }
            }
        }
        private static void InterrupterOnOnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs interruptableSpellEventArgs)
        {
            if (!sender.IsEnemy || !(sender is AIHeroClient) || Player.Instance.IsRecalling())
            {
                return;
            }
            if (SettingsMisc.InterruptW && SpellManager.W.IsReady() && SpellManager.W.IsInRange(sender))
            {
                SpellManager.W.Cast(sender);            }
        }
    }
}
