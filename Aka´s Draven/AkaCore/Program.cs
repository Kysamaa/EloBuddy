using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK;
using EloBuddy;
using EloBuddy.SDK.Events;

namespace AkaCore
{
    class Program
    {
        public static void Load(EventArgs args)
        {
            Manager.Load.Execute();
            Chat.Print("AkaCore Loaded.");
        }
    }
}
