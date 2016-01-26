using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using SharpDX;

namespace AddonTemplate
{
    public static class SpellManager
    {
        // You will need to edit the types of spells you have for each champ as they
        // don't have the same type for each champ, for example Xerath Q is chargeable,
        // right now it's  set to Active.
        public static Spell.Skillshot Q { get; private set; }
        public static Spell.Active W { get; private set; }
        public static Spell.Skillshot E { get; private set; }
        public static Spell.Targeted R { get; private set; }
        public static Vector3 Epos = default(Vector3);
        public static Item Qss, Mercurial;

        static SpellManager()
        {
            // Initialize spells
            Q = new Spell.Skillshot(SpellSlot.Q, 830, SkillShotType.Linear);
            W = new Spell.Active(SpellSlot.W, 520);
            E = new Spell.Skillshot(SpellSlot.E, 860,SkillShotType.Circular);
            R = new Spell.Targeted(SpellSlot.R, 650);

            Qss = new Item((int)ItemId.Quicksilver_Sash);
            Mercurial = new Item((int)ItemId.Mercurial_Scimitar);

        }

        public static void Initialize()
        {
            // Let the static initializer do the job, this way we avoid multiple init calls aswell
        }
    }
}
