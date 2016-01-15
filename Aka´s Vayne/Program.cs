using System;
using System.IO;
using System.Media;
using System.Net;
using EloBuddy;
using EloBuddy.Sandbox;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;

namespace Aka_s_Vayne_reworked
{
    internal class Program
    {
        public static Spell.Active Q;
        public static Spell.Active W;
        public static Spell.Skillshot E2;
        public static Spell.Targeted E;
        public static Spell.Targeted E3;
        public static Spell.Active R;
        public static Spell.Active Heal;
        public static Item totem, Qss, Mercurial, HPPot;

        private static void Main(string[] args1)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Variables._Player.ChampionName != "Vayne") return;

            Q = new Spell.Active(SpellSlot.Q, 300);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Targeted(SpellSlot.E, 590);
            E2 = new Spell.Skillshot(SpellSlot.E, 590, SkillShotType.Linear, 250, 1250);
            R = new Spell.Active(SpellSlot.R);
            var slot = ObjectManager.Player.GetSpellSlotFromName("summonerheal");
            if (slot != SpellSlot.Unknown)
            {
                Heal = new Spell.Active(slot, 600);
            }
            totem = new Item((int) ItemId.Warding_Totem_Trinket);
            Qss = new Item((int) ItemId.Quicksilver_Sash);
            Mercurial = new Item((int) ItemId.Mercurial_Scimitar);
            HPPot = new Item(2003);

            EventManager.Load();
            MenuManager.Load();

            // Made by KarmaPanda
            try
            {

                var sandBox = SandboxConfig.DataDirectory + @"\AkasVayne\";

                if (!Directory.Exists(sandBox))
                {
                    Directory.CreateDirectory(sandBox);
                }

                if (!File.Exists(sandBox + "AkasVayne.wav"))
                {
                    var client = new WebClient();
                    client.DownloadFile("http://download1322.mediafire.com/sff2ed0lc86g/4uqhdtdl1q6edid/AkasVayne.wav",
                        sandBox + "AkasVayne.wav");
                    client.DownloadFileCompleted += Events._client.DownloadComplete;
                }

                if (File.Exists(sandBox + "AkasVayne.wav"))
                {
                    Variables.Welcomemsg = new SoundPlayer
                    {
                        SoundLocation =
                            SandboxConfig.DataDirectory + @"\AkasVayne\" + "AkasVayne.wav"
                    };
                    Variables.Welcomemsg.Load();
                }

            }
            catch (Exception e)
            {
                Chat.Print("Failed to load Welcomesmsg: " + e.ToString());
            }

            if (Variables.Welcomemsg != null)
            {
                Core.DelayAction(() => Variables.Welcomemsg.Play(), 5000);
            }
        }
    }
}

