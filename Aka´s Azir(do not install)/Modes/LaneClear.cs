using System.Collections.Generic;
using System.Linq;
using Addontemplate;
using AddonTemplate.Utility;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;
using Settings = AddonTemplate.Config.Modes.LaneClear;
namespace AddonTemplate.Modes
{
    public sealed class LaneClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            // Only execute this mode when the orbwalker is on laneclear mode
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear);
        }

        public override void Execute()
        {
    }
    }
}
