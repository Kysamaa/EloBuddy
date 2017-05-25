using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace AddonTemplate
{
    public static class SpellManager
    {
        // You will need to edit the types of spells you have for each champ as they
        // don't have the same type for each champ, for example Xerath Q is chargeable,
        // right now it's  set to Active.
        public static Spell.Skillshot Q { get; private set; }
        public static Spell.Skillshot W { get; private set; }
        public static Spell.Active E { get; private set; }
        public static Spell.Skillshot R { get; private set; }

        static SpellManager()
        {
            // Initialize spells
            Q = new Spell.Skillshot(SpellSlot.Q, 1100, SkillShotType.Linear);
            Q.AllowedCollisionCount = Int32.MaxValue;
            W = new Spell.Skillshot(SpellSlot.W, 450, SkillShotType.Circular, 250, 0, 200);
            W.AllowedCollisionCount = Int32.MaxValue;
            E = new Spell.Active(SpellSlot.E, 1100);
            R = new Spell.Skillshot(SpellSlot.R, 250, SkillShotType.Linear);
            R.AllowedCollisionCount = Int32.MaxValue;

        }

        public static void Initialize()
        {
            // Let the static initializer do the job, this way we avoid multiple init calls aswell
        }
    }
}
