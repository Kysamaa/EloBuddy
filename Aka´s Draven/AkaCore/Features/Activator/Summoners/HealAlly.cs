using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK;
using EloBuddy;

namespace AkaCore.Features.Activator.Summoners
{
    class HealAlly : IModule
    {
        public void OnLoad()
        {

        }

        public ModuleType GetModuleType()
        {
            return ModuleType.OnUpdate;
        }

        public bool ShouldGetExecuted()
        {
            return AkaCore.Manager.MenuManager.HealAlly && AkaLib.Item.Heal != null && AkaLib.Item.Heal.IsReady();
        }

        public void OnExecute()
        {
            foreach (var ally in EntityManager.Heroes.Allies.Where(a => !a.IsDead && a.Position.Distance(ObjectManager.Player) < AkaLib.Item.Heal.Range && a.IsValid && a.HealthPercent <= AkaCore.Manager.MenuManager.HealAllyHp))
            {
                if (ally.CountEnemiesInRange(800) >= 1)
                {
                    AkaLib.Item.Heal.Cast();
                }
                if (ally.HasBuff("summonerdot"))
                {
                    AkaLib.Item.Heal.Cast();
                }
            }
        }
    }
}
