using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddonTemplate.Logic;
using EloBuddy;
using EloBuddy.SDK;

namespace Aka_s_Vayne_reworked.Modes
{
    internal class LCLH
    {
        public static void Load()
        {
            Execute();
        }

        public static void Execute()
        {
            if (!Program.Q.IsReady())
            {
                return;
            }

            var minionsInRange =
                EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy,
                    ObjectManager.Player.ServerPosition, ObjectManager.Player.AttackRange + 65)
                    .Where(
                        m =>
                            m.Health <=
                            ObjectManager.Player.GetAutoAttackDamage(m) +
                            Variables._Player.GetSpellDamage(m, SpellSlot.Q))
                    .ToList();

            if (minionsInRange.Count() > 1)
            {
                var firstMinion = minionsInRange.OrderBy(m => m.HealthPercent).First();
                var afterTumblePosition = QLogic.GetAfterTumblePosition(Game.CursorPos);
                if (afterTumblePosition.Distance(firstMinion.ServerPosition) <= Variables._Player.GetAutoAttackRange())
                {
                    QLogic.DefaultQCast(Game.CursorPos, firstMinion);
                    Orbwalker.ForcedTarget = firstMinion;
                }
            }

        }
    }
}
    
