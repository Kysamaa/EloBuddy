using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using Aka_s_Draven.Features.Modules;
using Aka_s_Draven.Features.Modes;
using AkaCore.Features.Utility.Modules;

namespace Aka_s_Draven.Manager
{
    class EventManager
    {
        public static void Load()
        {
            Game.OnUpdate += Game_OnUpdate;
            Drawing.OnDraw += Drawings.Draw;
            Init.OnGameLoad();
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if (FPSProtection.CheckFps())
            {
                return;
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass)) Features.Modes.Harass.Execute();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee)) Features.Modes.Flee.Execute();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear)) Features.Modes.LaneClear.Execute();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo)) Features.Modes.Combo.Execute();

            Init.OnUpdate();
        }
    }
}
