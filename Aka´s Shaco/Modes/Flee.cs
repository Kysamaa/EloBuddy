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
            Q.Cast(Game.CursorPos);

            Program.clone = getClone();

            if (Program.clone != null)
            {

                var pos = Game.CursorPos.Extend(Program.clone.Position, Program.clone.Distance(Game.CursorPos) + 2000);
                R.Cast((Vector3) pos);

            }


        }
        public static Obj_AI_Base getClone()
        {
            Obj_AI_Base Clone = null;
            foreach (var unit in ObjectManager.Get<Obj_AI_Base>().Where(clone => !clone.IsMe && clone.Name == ObjectManager.Player.Name))
            {
                Clone = unit;
            }

            return Clone;

        }


    }
}
