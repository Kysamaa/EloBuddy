using System.Linq;
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
        }

        public static GameObject GetNearestSoldierToMouse(Vector3 pos)
        {
            var soldier = Orbwalker.AzirSoldiers.ToList().OrderBy(x => pos.Distance(x.Position));

            if (soldier.FirstOrDefault() != null)
                return soldier.FirstOrDefault();

            return null;
        }
    }
}
