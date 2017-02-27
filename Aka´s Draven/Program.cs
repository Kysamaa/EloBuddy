using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK.Events;

namespace Aka_s_Draven
{
    class Program
    {
        private static void Main(string[] args1)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Variables._Player.ChampionName != "Draven") return;
            AkaCore.Program.Load(args);
            Manager.Manager.Load();
            Chat.Print("Aka´s Vayne loaded! Made by Aka.");
        }
    }
}
