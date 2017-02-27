using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK;
using EloBuddy;

namespace AkaCore.Features.Utility.Modules
{
    class FPSProtection : IModule
    {
        private static float lastTick, fps, delay, lastTickdelay, autoFpsBalancer, frameRate;

        public void OnLoad()
        {

        }

        public ModuleType GetModuleType()
        {
            return ModuleType.OnUpdate;
        }

        public bool ShouldGetExecuted()
        {
            return true;
        }

        public void OnExecute()
        {
            if (System.Environment.TickCount - lastTick >= 1000)
            {
                fps = frameRate;
                frameRate = 0;
                lastTick = System.Environment.TickCount;
            }
            frameRate++;
            delay += System.Environment.TickCount - lastTickdelay;
            lastTickdelay = System.Environment.TickCount;
        }

        public static bool CheckFps()
        {
            if (!Manager.MenuManager.EnableFPS)
            {
                return false;
            }
            var rate = Manager.MenuManager.CalcPerSecond;
            if (fps < Manager.MenuManager.MinFps)
            {
                rate = Math.Min(10, Manager.MenuManager.CalcPerSecond);
            }
            if (delay > 1000f / rate)
            {
                delay = 0;
                return false;
            }
            return true;
        }
    }
}
