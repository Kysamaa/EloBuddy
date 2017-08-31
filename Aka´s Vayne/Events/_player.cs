
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace Aka_s_Vayne_reworked.Events
{
    class _player
    {
        public static void IssueOrder(Obj_AI_Base sender, PlayerIssueOrderEventArgs args)
        {
            if (sender.IsMe && args.Order.HasFlag(GameObjectOrder.AttackUnit))
            {
                Variables.lastaaclick = Game.Time * 1000;
            }

            if (sender.IsMe
                && (args.Order == GameObjectOrder.AttackUnit || args.Order == GameObjectOrder.AttackTo)
                &&
                (MenuManager.ComboMenu["RnoAA"].Cast<CheckBox>().CurrentValue &&
                 Variables._Player.CountEnemiesInRange(1000f) >
                 MenuManager.ComboMenu["RnoAAs"].Cast<Slider>().CurrentValue)
                && Functions.Events._player.UltActive() || Variables._Player.HasBuffOfType(BuffType.Invisibility)
                && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                args.Process = false;
            }
        }
    }
}
