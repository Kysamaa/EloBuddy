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
        public static Spell.Skillshot E { get; private set; }
        public static Spell.Active E2 { get; private set; }
        public static Spell.Skillshot R { get; private set; }
        public static Spell.Targeted Ignite { get; private set; }
        public static Spell.Targeted Shurimaaa { get; private set; }

        static SpellManager()
        {
            // Initialize spells
            Q = new Spell.Skillshot(SpellSlot.Q, 1100, SkillShotType.Linear);
            Q.AllowedCollisionCount = Int32.MaxValue;
            W = new Spell.Skillshot(SpellSlot.W, 450, SkillShotType.Circular, 250, 0, 200);
            W.AllowedCollisionCount = Int32.MaxValue;
            E = new Spell.Skillshot(SpellSlot.E, 1250, SkillShotType.Linear);
            E2 = new Spell.Active(SpellSlot.E, 1250);
            R = new Spell.Skillshot(SpellSlot.R, 250, SkillShotType.Linear);
            R.AllowedCollisionCount = Int32.MaxValue;
            var slot = ObjectManager.Player.GetSpellSlotFromName("summonerdot");
            if (slot != SpellSlot.Unknown)
            {
                Ignite = new Spell.Targeted(slot, 600);
            }
            var slot2 = ObjectManager.Player.GetSpellSlotFromName("azirpassive");
            if (slot2 != SpellSlot.Unknown)
            {
                Shurimaaa = new Spell.Targeted(slot, 400);
            }

        }

        public static void Initialize()
        {
            // Let the static initializer do the job, this way we avoid multiple init calls aswell
        }
    }
}
