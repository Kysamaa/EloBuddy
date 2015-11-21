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
        public static Spell.Active Q2 { get; private set; }
        public static Spell.Active W { get; private set; }
        public static Spell.Skillshot E { get; private set; }
        public static Spell.Active R { get; private set; }

        static SpellManager()
        {
            // Initialize spells
            Q = new Spell.Skillshot(SpellSlot.Q, 740, SkillShotType.Cone, 250, 100, 500);
            Q2 = new Spell.Active(SpellSlot.Q, 680);
            W = new Spell.Active(SpellSlot.W, 490);
            E = new Spell.Skillshot(SpellSlot.E, 755, SkillShotType.Linear);
            R = new Spell.Active(SpellSlot.R, 800);

        }

        public static void Initialize()
        {
            // Let the static initializer do the job, this way we avoid multiple init calls aswell
        }
    }
}
