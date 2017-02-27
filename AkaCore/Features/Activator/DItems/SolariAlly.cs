using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK;
using EloBuddy;

namespace AkaCore.Features.Activator.DItems
{
    class SolariAlly : IModule
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
            return AkaCore.Manager.MenuManager.DItems && AkaCore.Manager.MenuManager.SolariAlly && AkaLib.Item.Solari != null && AkaLib.Item.Solari.IsReady();
        }

        public void OnExecute()
        {
            foreach (var ally in EntityManager.Heroes.Allies.Where(a => !a.IsDead && a.Position.Distance(ObjectManager.Player) < AkaLib.Item.Solari.Range && a.IsValid && a.HealthPercent <= AkaCore.Manager.MenuManager.SolraiAllyHp))
            {
                if (ally.CountEnemiesInRange(800) >= 1)
                {
                    AkaLib.Item.Solari.Cast(ally);
                }
                if (ally.HasBuff("summonerdot"))
                {
                    AkaLib.Item.Solari.Cast(ally);
                }
            }
        }
    }
}
