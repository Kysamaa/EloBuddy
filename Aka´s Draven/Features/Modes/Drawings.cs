using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using SharpDX;
using EloBuddy.SDK.Rendering;

namespace Aka_s_Draven.Features.Modes
{
    class Drawings
    {
        public static void Draw(EventArgs args)
        {
            if (Manager.MenuManager.DrawE)
            {
                Circle.Draw(Color.Aqua,
                    Manager.SpellManager.E.Range, ObjectManager.Player.Position);
            }
        }
    }
}
