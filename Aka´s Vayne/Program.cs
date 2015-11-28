using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AddonTemplate.Logic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;
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
            Obj_AI_Base.OnProcessSpellCast += Events.OnProcessSpell;
            Obj_AI_Base.OnBuffGain += Events.Obj_AI_Base_OnBuffGain;
            Obj_AI_Base.OnBasicAttack += Events.Obj_AI_Base_OnBasicAttack;
            Player.OnIssueOrder += Events.Player_OnIssueOrder;
            Game.OnUpdate += Events.Game_OnTick;
            Obj_AI_Base.OnSpellCast += Events.Obj_AI_Base_OnSpellCast;

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
            Cache.Load();
            Config.Initialize();
            SpellManager.Initialize();
            ModeManager.Initialize();
            ELogic.LoadFlash();
            // Listen to events we need
            Drawing.OnDraw += OnDraw;
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



    }
}
