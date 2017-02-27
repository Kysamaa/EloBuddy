using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK;
using EloBuddy;
using AkaCore.Features.Orbwalk.AutoCatch;


namespace Aka_s_Draven
{
    class Variables
    {
        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }

        public static int QCount
        {
            get
            {
                return (_Player.HasBuff("dravenspinning") ? 1 : 0)
                       + (_Player.HasBuff("dravenspinningleft") ? 1 : 0) + Axe.QReticles.Count;
            }
        }
    }
}
