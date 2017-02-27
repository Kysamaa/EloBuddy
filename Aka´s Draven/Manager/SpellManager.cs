using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace Aka_s_Draven.Manager
{
    class SpellManager
    {
        public static Spell.Active Q;
        public static Spell.Active W;
        public static Spell.Skillshot E;
        public static Spell.Skillshot R;

        private static void Spellssitems()
        {
            Q = new Spell.Active(SpellSlot.Q, (uint)Variables._Player.GetAutoAttackRange());
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Skillshot(SpellSlot.E, 1050, SkillShotType.Linear, 250, null, 130)
            {
                AllowedCollisionCount = int.MaxValue
            };
            R = new Spell.Skillshot(SpellSlot.R, 2000, SkillShotType.Linear, 250, null, 160);
        }

        public static void Load()
        {
            Spellssitems();
        }

    }
}
