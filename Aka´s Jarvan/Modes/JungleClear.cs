using System.Configuration;
using System.Linq;
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
            var monsters =
                EntityManager.MinionsAndMonsters.GetJungleMonsters(Player.Instance.Position, E.Range)
                    .Where(t => !t.IsDead && t.IsValid && !t.IsInvulnerable);
            foreach (var m in monsters)
            {
                if (Settings.UseQ)
                {
                    E.Cast(m);
                }
                if (Settings.UseQ)
                {
                    Q.Cast(m);
                }
                if (Settings.UseW)
                {
                    W.Cast();
                }
                Items.UseItems(m);
            }
        }
    }
}
