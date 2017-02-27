using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AkaCore.Features.Utility.Modules;

namespace AkaCore.Features.Utility
{
    class UInitialize
    {
        private static List<IModule> moduleList = new List<IModule>()
        {
            new AutoBuyStarters(),
            new AutoBuyTrinkets(),
            new AutoLantern(),
            new Autolvl(),
            new Skinhack(),
            new FPSProtection(),
        };

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
