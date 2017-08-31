
using EloBuddy;

namespace AkaYasuo.Events
{
    class _spellbook
    {
        public static void StopCast(Obj_AI_Base sender, SpellbookStopCastEventArgs args)
        {
            if (sender.IsMe)
            {
                if (sender.IsValid && args.DestroyMissile && args.StopAnimation)
                {
                    Variables.IsDashing = false;
                }
            }
        }
    }
}

