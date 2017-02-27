using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkaCore.Features.Utility
{
    interface IModule
    {
        void OnLoad();

        bool ShouldGetExecuted();

        ModuleType GetModuleType();

        void OnExecute();
    }

    enum ModuleType
    {
        OnUpdate, Other
    }
}
