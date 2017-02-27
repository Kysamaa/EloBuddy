using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkaCore.Manager
{
    class Load
    {
        public static void Execute()
        {
            //AkaLib *_*
            AkaLib.Initialize.Execute();
            //MenuManager
            MenuManager.Load();
            //EventManager
            EventManager.Load();
        }
    }
}
