
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using Gapcloser = EloBuddy.SDK.Events.Gapcloser;



namespace Aka_s_Vayne_reworked.Events
{
    internal class _Gapclose
    {
        public static void GapcloseE(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (sender == null || sender.IsAlly || !MenuManager.MiscMenu["GapcloseE"].Cast<CheckBox>().CurrentValue) return;

            if ((sender.IsAttackingPlayer || e.End.Distance(Variables._Player) <= 70))
            {
                Program.E.Cast(sender);
            }
        }
    }
}
