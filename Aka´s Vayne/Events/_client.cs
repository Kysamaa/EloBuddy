using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.Sandbox;

namespace Aka_s_Vayne_reworked.Events
{
    class _client
    {
        public static void DownloadComplete(object sender, AsyncCompletedEventArgs e)
        {
            Variables.Welcomemsg = new SoundPlayer
            {
                SoundLocation = SandboxConfig.DataDirectory + @"\AkasVayne\" + "AkasVayne.wav"
            };
            Variables.Welcomemsg.Load();
        }
    }
}
