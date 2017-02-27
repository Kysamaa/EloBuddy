using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;

namespace Aka_s_Draven.Features.Modes
{
    class LaneClear
    {
        public static void Execute()
        {
            if (Variables._Player.ManaPercent < Manager.MenuManager.ManaLC)
            {
                return;
            }

            foreach (var minion in EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy,
                Variables._Player.ServerPosition, Variables._Player.GetAutoAttackRange()))
            {
                if (minion == null) return;
                if (Manager.MenuManager.UseQLC && Variables.QCount < Manager.MenuManager.MaxAxes - 1 &&
                    Manager.SpellManager.Q.IsReady() && !Orbwalker.IsAutoAttacking)
                {
                    Manager.SpellManager.Q.Cast();
                }
            }

            if (Manager.MenuManager.UseWLC && Manager.MenuManager.UseWLC
                && Variables._Player.ManaPercent > Manager.MenuManager.UseWMana)
            {
                if (Manager.MenuManager.UseWEverytime)
                {
                    Manager.SpellManager.W.Cast();
                }
                else
                {
                    if (!Player.HasBuff("dravenfurybuff"))
                    {
                        Manager.SpellManager.W.Cast();
                    }
                }
            }
        }
    }
}
