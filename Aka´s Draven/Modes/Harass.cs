using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace AkaDraven.Modes
{
    class Harass
    {
        public static void Execute()
        {
            var target = TargetSelector.GetTarget(Program.E.Range, DamageType.Physical);

            if (!target.IsValidTarget())
            {
                return;
            }

            if (MenuManager.HarassMenu["E"].Cast<CheckBox>().CurrentValue && Program.E.IsReady())
            {
                Program.E.Cast(target);
            }
        }
    }
}

