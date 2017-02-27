using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK.Events;
using System.IO;
using System.Reflection;
using System.Net;

namespace Aka_s_Vayne
{
    class Program
    {
        private static string dllpath = @"C:\Users\" + Environment.UserName + @"\AppData\Roaming\EloBuddy\Addons\Libraries\Aka´s Vayne.dll";

        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += eventArgs =>
            {
                if (ObjectManager.Player.ChampionName != "Vayne") return;

                if (!File.Exists(dllpath))
                {
                    DownloadDll().GetAwaiter().GetResult();
                    InvokeScript();
                }
                else
                {
                    Assembly Assembly = Assembly.Load(File.ReadAllBytes(dllpath));
                    WebClient client = new WebClient();
                    String webData = client.DownloadString("https://raw.githubusercontent.com/Kysamaa/EloBuddy/master/Paid/Auth.txt");
                    var Vver = Assembly.GetName().Version.ToString();
                    var Lver = webData.Substring(webData.IndexOf("VayneVersion") + 13, 7);
                                      
                    if (Vver != Lver)
                    {
                        DownloadDll().GetAwaiter().GetResult();
                    }
                    
                    //Console.Write(Vver);
                    InvokeScript();
                }
            };
        }

        private static void InvokeScript()
        {
            Assembly Assembly = Assembly.LoadFile(dllpath);

            Type myType = Assembly.GetType("Aka_s_Vayne.Program");

            if (myType != null)
            {
                var main = myType.GetMethod("Main");
                if (main != null)
                {
                    main.Invoke(null, null);
                }
            }
        }

        private static async Task DownloadDll()
        {
            WebClient client = new WebClient();
            client.DownloadFile("https://raw.githubusercontent.com/Kysamaa/EloBuddy/master/Paid/Aka´s Vayne.dll", dllpath);
        }
    }
}
