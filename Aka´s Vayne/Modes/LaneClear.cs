using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
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

            if ((Settings.UseQ) && SpellManager.Q.IsReady())
            {
                var minions =
                    EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy,
                        Player.Instance.Position,
                        Q.Range).Where(
                            m => !m.IsDead && m.IsValid && !m.IsInvulnerable && Player.Instance.GetAutoAttackDamage(m) + Damages.QDamage(m) > m.Health);
                {
                    foreach (var m in minions)
                    {
                        if (Player.Instance.Position.Extend(Game.CursorPos, 300).Distance(m) >
                            Player.Instance.GetAutoAttackRange(m))
                            return;
                        var cursorPos = Game.CursorPos;
                        Orbwalker.ForcedTarget = m;
                        if (!QLogic.IsDangerousPosition(cursorPos))
                            Player.CastSpell(SpellSlot.Q,
                                Player.Instance.Position.Extend(Game.CursorPos, 300).Distance(m) <=
                                Player.Instance.GetAutoAttackRange(m)
                                    ? Game.CursorPos
                                    : m.Position);
                    }
                }
            }
        }
    }
}
