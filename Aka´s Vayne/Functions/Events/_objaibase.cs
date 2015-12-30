
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace Aka_s_Vayne_reworked.Functions.Events
{
    class _objaibase
    {
        public static void DoQSS()
        {
            if (Program.Qss.IsOwned() && Program.Qss.IsReady() && Variables._Player.CountEnemiesInRange(1800) > 0)
            {
                Core.DelayAction(() => Program.Qss.Cast(), MenuManager.ItemMenu["delay"].Cast<Slider>().CurrentValue);
            }

            if (Program.Mercurial.IsOwned() && Program.Mercurial.IsReady() && Variables._Player.CountEnemiesInRange(1800) > 0)
            {
                Core.DelayAction(() => Program.Mercurial.Cast(), MenuManager.ItemMenu["delay"].Cast<Slider>().CurrentValue);
            }
        }

        public static void UltQSS()
        {
            if (Program.Qss.IsOwned() && Program.Qss.IsReady())
            {
                Core.DelayAction(() => Program.Qss.Cast(), MenuManager.ItemMenu["delay"].Cast<Slider>().CurrentValue);
            }

            if (Program.Mercurial.IsOwned() && Program.Mercurial.IsReady())
            {
                Core.DelayAction(() => Program.Mercurial.Cast(), MenuManager.ItemMenu["delay"].Cast<Slider>().CurrentValue);
            }
        }
    }
}
