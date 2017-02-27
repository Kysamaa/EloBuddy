using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aka_s_Draven.Manager
{
    class Manager
    {
        public static void Load()
        {
            SpellManager.Load();
            MenuManager.Load();
            EventManager.Load();
        }
    }
}
