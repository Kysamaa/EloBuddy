using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace Aka_s_Vayne_reworked.Events
{
    class _gameObject
    {
        public static void AntiRenger(GameObject sender, EventArgs args)
        {
            var rengar = EntityManager.Heroes.Enemies.FirstOrDefault(a => a.Hero == Champion.Rengar);
            if (MenuManager.MiscMenu["AntiRengar"].Cast<CheckBox>().CurrentValue && sender.Name == "Rengar_LeapSound.troy" &&
                Variables._Player.Distance(Variables._Player.Position) <= Program.E.Range && rengar != null)
            {
                Program.E.Cast(rengar);
                Console.WriteLine("fuck rengar");
            }
        }
    }
}
