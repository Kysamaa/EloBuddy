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
        public static Spell.Targeted Q { get; private set; }
        public static Spell.Targeted Q2 { get; private set; }
        public static Spell.Targeted W { get; private set; }
        public static Spell.Targeted E { get; private set; }
        public static Spell.Targeted R { get; private set; }
        public static int cloneAct = 0;

        static SpellManager()
        {
            // Initialize spells
            Q = new Spell.Targeted(SpellSlot.Q, 400);
            Q2 = new Spell.Targeted(SpellSlot.Q, 1100);
            W = new Spell.Targeted(SpellSlot.W, 425);
            E = new Spell.Targeted(SpellSlot.E, 625);
            R = new Spell.Targeted(SpellSlot.R, 200);

        }

        public static void Initialize()
        {
            // Let the static initializer do the job, this way we avoid multiple init calls aswell
        }
    }
}
