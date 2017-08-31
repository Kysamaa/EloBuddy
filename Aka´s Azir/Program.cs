﻿using System;
using System.Diagnostics;
using AddonTemplate.Utility;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
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
        public const string ChampName = "Azir";

        public static void Main(string[] args)
        {
            // Wait till the loading screen has passed
            Loading.OnLoadingComplete += OnLoadingComplete;
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
            Game.OnTick += Game_OnTick;

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



        public static void Game_OnTick(EventArgs args)
        {
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
            {
                Jump(Game.CursorPos);
            }
        }

        public static void Jump(Vector3 pos)
        {
            Vector3 wVec = ObjectManager.Player.ServerPosition +
               Vector3.Normalize(pos - ObjectManager.Player.ServerPosition) * SpellManager.W.Range;

            if ((SpellManager.E.IsReady()) && Player.Instance.ServerPosition.Distance(pos) < SpellManager.Q.Range)
            {
                if (SpellManager.W.IsReady())
                {
                    SpellManager.W.Cast(wVec);
                    return;
                }
                if (SpellManager.Q.IsReady() && Modes.Flee.GetNearestSoldierToMouse(pos).Position.Distance(pos) > 300)
                {
                    SpellManager.Q.Cast(pos);
                    return;
                }
                SpellManager.E2.Cast();
            }
        }

        public static bool Between(this Vector3 checkPos, Vector3 source, Vector3 destination)
        {
            return Math.Abs(((source.X * checkPos.Y) + (source.Y * destination.X) + (checkPos.X * destination.Y)) - ((checkPos.Y * destination.X) + (source.X * destination.Y) + (source.Y * checkPos.X))) < 5;
        }


        public static Vector3 GetQPos(AIHeroClient target, bool serverPos, int distance = 150)
        {
            var enemyPos = serverPos ? target.ServerPosition : target.Position;
            var myPos = serverPos ? ObjectManager.Player.ServerPosition : ObjectManager.Player.Position;

            return enemyPos + Vector3.Normalize(enemyPos - myPos) * distance;
        }

    }
}
