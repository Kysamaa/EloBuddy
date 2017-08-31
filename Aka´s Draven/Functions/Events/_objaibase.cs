
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

namespace AkaDraven.Functions.Events
{
    class _objaibase
    {
        public static void DoQSS()
        {
            var delay = MenuManager.ItemMenu["delay"].Cast<Slider>().CurrentValue;
            if (Program.Qss.IsOwned() && Program.Qss.IsReady())
            {
                Core.DelayAction(() => { Program.Qss.Cast(); }, delay);
            }

            if (Program.Mercurial.IsOwned() && Program.Mercurial.IsReady())
            {
                Core.DelayAction(() => { Program.Mercurial.Cast(); }, delay);
            }
        }
        public static void UltQSS()
        {
            if (Program.Qss.IsOwned() && Program.Qss.IsReady())
            {
                Core.DelayAction(() => { Program.Qss.Cast(); }, 1000);
            }

            if (Program.Mercurial.IsOwned() && Program.Mercurial.IsReady())
            {
                Core.DelayAction(() => { Program.Mercurial.Cast(); }, 1000);
            }
        }
    }
}