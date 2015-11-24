using System.Configuration;
using System.Linq;
using AddonTemplate.Logic;
using EloBuddy;
using EloBuddy.SDK;
using Settings = AddonTemplate.Config.Modes.JungleClear;
namespace AddonTemplate.Modes
{
    public sealed class JungleClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            // Only execute this mode when the orbwalker is on jungleclear mode
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear);
        }

        public override void Execute()
        {
            if (Settings.UseQ && SpellManager.Q.IsReady() && !Game.CursorPos.IsDangerousPosition())
            {
                QLogic.QJungleClear();
            }
            if (Settings.UseE && SpellManager.E.IsReady())
            {
                ELogic.JungleCondemn();
            }

        }
        }
    }

