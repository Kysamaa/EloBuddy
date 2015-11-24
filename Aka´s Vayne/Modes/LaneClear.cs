using System;
using System.Linq;
using AddonTemplate.Logic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
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

            if (Q.IsReady() && Settings.UseQ && ObjectManager.Player.ManaPercent > Settings.UseQSlider && !Game.CursorPos.IsDangerousPosition())
            {
                var Minions = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, ObjectManager.Player.Position, Q.Range, true);
                foreach (var minions in
                    Minions.Where(
                        minions => minions.Health < ObjectManager.Player.GetSpellDamage(minions, SpellSlot.Q)))
                {
                    if (minions.IsValidTarget(E.Range) && minions.IsVisible)
                    {
                        Player.CastSpell(SpellSlot.Q, ObjectManager.Player.GetTumblePos());
                        Orbwalker.ForcedTarget = minions;
                    }
                }
            }
        }
    }
}
