using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;

namespace AkaDraven.Events
{
    class _gameobject
    {
        public static void Create(GameObject sender, EventArgs args)
        {
            if (!sender.Name.Contains("Draven_Base_Q_reticle_self.troy"))
            {
                return;
            }

            Variables.QReticles.Add(new Variables.QRecticle(sender, Environment.TickCount + 1800));
            Core.DelayAction(() => Variables.QReticles.RemoveAll(x => x.Object.NetworkId == sender.NetworkId), 1800);
        }

        public static void Delete(GameObject sender, EventArgs args)
        {
            if (!sender.Name.Contains("Draven_Base_Q_reticle_self.troy"))
            {
                return;
            }

            Variables.QReticles.RemoveAll(x => x.Object.NetworkId == sender.NetworkId);
        }

    }
}
