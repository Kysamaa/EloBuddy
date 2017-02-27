using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aka_s_Draven.Features.Modules
{
    class Init
    {
        private static List<IModule> moduleList = new List<IModule>()
        {
            new Antigap(),
            new Interrupte(),
            new KSE(),
            new AutoE(),
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
