using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using SharpDX;

namespace AddonTemplate
{
    public static class Events
    {
        public static void Load()
        {
            GameObject.OnCreate += OnCreateObj;
            GameObject.OnDelete += OnDeleteObj;
        }

        private static void OnCreateObj(GameObject sender, EventArgs args)
        {
            if (!(sender is Obj_GeneralParticleEmitter)) return;
            var obj = (Obj_GeneralParticleEmitter) sender;
            if (sender.Name == "JarvanDemacianStandard_buf_green.troy")
            {
                SpellManager.Epos = sender.Position;
            }

        }

        private static void OnDeleteObj(GameObject sender, EventArgs args)
        {
            if (!(sender is Obj_GeneralParticleEmitter)) return;
            if (sender.Name == "JarvanDemacianStandard_buf_green.troy")
            {
                SpellManager.Epos = default(Vector3);
            }
        }
    }
}