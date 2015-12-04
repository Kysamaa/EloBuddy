using EloBuddy.SDK;

namespace AddonTemplate.Modes
{
    public abstract class ModeBase
    {
        // Change the spell type to whatever type you used in the SpellManager
        // here to have full features of that spells, if you don't need that,
        // just change it to Spell.SpellBase, this way it's dynamic with still
        // the most needed functions
        protected Spell.Targeted Q
        {
            get { return SpellManager.Q; }
        }
        protected Spell.Targeted Q2
        {
            get { return SpellManager.Q2; }
        }
        protected Spell.Targeted W
        {
            get { return SpellManager.W; }
        }
        protected Spell.Targeted E
        {
            get { return SpellManager.E; }
        }
        protected Spell.Targeted R
        {
            get { return SpellManager.R; }
        }

        public abstract bool ShouldBeExecuted();

        public abstract void Execute();
    }
}
