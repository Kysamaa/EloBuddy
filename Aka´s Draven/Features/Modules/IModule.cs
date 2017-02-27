using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aka_s_Draven.Features.Modules
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
        OnUpdate, OnAfterAA, Other
    }
}
