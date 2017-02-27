using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AkaCore.Features.Orbwalk.AutoCatch;
using EloBuddy;

namespace AkaCore.Features.Orbwalk
{
    class OInitialize
    {
        private static List<IModule> moduleList = new List<IModule>()
        {
            new Events()
        };

        public static void OnGameLoad()
        {
            if (ObjectManager.Player.ChampionName == "Draven")
            {
                foreach (var module in moduleList)
                {
                    module.OnLoad();
                }
            }
        }
    }
}
