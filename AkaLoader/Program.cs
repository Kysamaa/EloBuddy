﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK.Events;
using System.IO;
using System.Reflection;
using System.Net;
using System.Drawing;

namespace AkaLoader
{
    class Program
    {
       //private static string dllpath = @"C:\Users\" + Environment.UserName + @"\AppData\Roaming\EloBuddy\Addons\Libraries\aka.dll";

        private static string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static string dllpath = Path.Combine(appdata, @"EloBuddy\Addons\Libraries\aka.dll");

        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += eventArgs =>
            {
                if (!File.Exists(dllpath))
                {
                    Chat.Print("Updating..", Color.WhiteSmoke);
                    DownloadDll().GetAwaiter().GetResult();
                    InvokeScript();
                }
                else
                {
                    try
                    {
                        Assembly Assembly = Assembly.Load(File.ReadAllBytes(dllpath));
                        WebClient client = new WebClient();
                        String webData = client.DownloadString("https://raw.githubusercontent.com/Kysamaa/EloBuddy/master/AkaLoader/Paid/Auth.txt");
                        var Vver = Assembly.GetName().Version.ToString();
                        var Lver = webData.Substring(webData.IndexOf("Version") + 8, 7);

                        if (Vver != Lver)
                        {
                            Chat.Print("Updating..", Color.SkyBlue);
                            DownloadDll().GetAwaiter().GetResult();
                        }

                        InvokeScript();
                    }
                    catch
                    {
                        Chat.Print("Update error please clean eb root folder.");
                    }
                }
            };
        }

        private static void InvokeScript()
        {
            Assembly Assembly = Assembly.LoadFile(dllpath);

            Type myType = Assembly.GetType("aka.Load");

            if (myType != null)
            {
                var main = myType.GetMethod("OnLoad");
                if (main != null)
                {
                    try
                    {
                        main.Invoke(null, null);
                    }
                    catch (TargetInvocationException e)
                    {
                        Chat.Print("Failed to initialize.");
                        Console.Write(e);
                    }
                }
            }
        }

        private static async Task DownloadDll()
        {
            if (File.Exists(dllpath)) File.Delete(dllpath);

            try
            {
                WebClient client = new WebClient();
                await Task.Run(() => client.DownloadFile("https://github.com/Kysamaa/EloBuddy/raw/master/AkaLoader/Paid/aka.dll", dllpath));
            }
            catch (WebException e)
            {
                Chat.Print("Check if you have an safe connection.");
                Console.Write(e);
            }
        }
    }
}
