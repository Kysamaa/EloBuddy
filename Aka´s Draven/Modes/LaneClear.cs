using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

namespace AkaDraven.Modes
{
    class LaneClear
    {
        public static void Execite()
        {
            var useQ = MenuManager.LaneClearMenu["Q"].Cast<CheckBox>().CurrentValue;
            var useW = MenuManager.LaneClearMenu["W"].Cast<CheckBox>().CurrentValue;

            if (Variables._Player.ManaPercent < MenuManager.LaneClearMenu["Mana"].Cast<Slider>().CurrentValue)
            {
                return;
            }

            foreach (var minion in EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy,
                Variables._Player.ServerPosition, Variables._Player.GetAutoAttackRange()))
            {
                if (minion == null) return;
                if (useQ && Variables.QCount < MenuManager.AxeMenu["Qmax"].Cast<Slider>().CurrentValue - 1 &&
                    Program.Q.IsReady() && !Orbwalker.IsAutoAttacking)
                {
                    Program.Q.Cast();
                }
            }

            if (useW && Program.W.IsReady()
                && Variables._Player.ManaPercent > MenuManager.MiscMenu["WMana"].Cast<Slider>().CurrentValue)
            {
                if (MenuManager.MiscMenu["UseWInstant"].Cast<CheckBox>().CurrentValue)
                {
                    Program.W.Cast();
                }
                else
                {
                    if (!Player.HasBuff("dravenfurybuff"))
                    {
                        Program.W.Cast();
                    }
                }
            }
        }
    }
}
