using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AkaCore.Features.Activator.AItems;
using AkaCore.Features.Activator.DItems;
using AkaCore.Features.Activator.Pots;
using AkaCore.Features.Activator.Summoners;

namespace AkaCore.Features.Activator
{
    class AInitialize
    {
        private static List<IModule> moduleList = new List<IModule>()
        {
            new Bilge(),
            new Botrk(),
            new Glory(),
            new Hydra(),
            new Qss(),
            new Queens(),
            new Talis(),
            new Tiamat(),
            new Titanic(),
            new You(),
            new FaceAlly(),
            new FaceMe(),
            new Glory2(),
            new Omen(),
            new Seraph(),
            new SolariAlly(),
            new SolariMe(),
            new Talis2(),
            new Zhonyas(),
            new Biscuit(),
            new CorruptPot(),
            new HpPot(),
            new HunterPot(),
            new RefillPot(),
            new Barrier(),
            new Ignite(),
            new Exhaust(),
            new HealAlly(),
            new Healme(),
            new Smite(),
            new HextechGLP(),
            new Hextech(),
            new HextechPB(),
        };

        public static void OnGameLoad()
        {
            foreach (var module in moduleList)
            {
                module.OnLoad();
            }
        }

        public static void OnUpdate()
        {
            foreach (var module in moduleList.Where(module => module.GetModuleType() == ModuleType.OnUpdate
&& module.ShouldGetExecuted()))
            {
                module.OnExecute();
            }
        }
    }
}
