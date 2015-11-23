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
        public static Spell.Active Q { get; private set; }
        public static Spell.Active W { get; private set; }
        public static Spell.Targeted E { get; private set; }
        public static Spell.Active R { get; private set; }
        public static int cloneAct = 0;

        static SpellManager()
        {
            // Initialize spells
            Q = new Spell.Active(SpellSlot.Q, 400);
            W = new Spell.Active(SpellSlot.W, 425);
            E = new Spell.Targeted(SpellSlot.E, 625);
            R = new Spell.Active(SpellSlot.R, 200);

        }

        public static void Initialize()
        {
            // Let the static initializer do the job, this way we avoid multiple init calls aswell
        }
    }
}
