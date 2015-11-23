using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using SharpDX;
using Settings = AddonTemplate.Config.Modes.Flee;

namespace AddonTemplate.Modes
{
    public sealed class Flee : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            // Only execute this mode when the orbwalker is on flee mode
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee);
        }

        public override void Execute()
        {
            Q.Cast(Game.CursorPos);

            var clone = Combo.getClone();

            if (clone != null)
            {

                var pos = Game.CursorPos.Extend(clone.Position, clone.Distance(Game.CursorPos) + 2000);
                R.Cast((Vector3) pos);

            }

        }
    }
}
