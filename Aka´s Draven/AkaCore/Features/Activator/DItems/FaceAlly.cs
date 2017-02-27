using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK;
using EloBuddy;

namespace AkaCore.Features.Activator.DItems
{
    class FaceAlly : IModule
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
            return AkaCore.Manager.MenuManager.DItems && AkaCore.Manager.MenuManager.MountainAlly && AkaLib.Item.Mountain != null && AkaLib.Item.Mountain.IsReady();
        }

        public void OnExecute()
        {
            foreach (var ally in EntityManager.Heroes.Allies.Where(a => !a.IsDead && a.Position.Distance(ObjectManager.Player) < AkaLib.Item.Mountain.Range && a.IsValid && a.HealthPercent <= AkaCore.Manager.MenuManager.MoutaiAllyHp))
            {
                if (ally.CountEnemiesInRange(800) >= 1)
                {
                    AkaLib.Item.Mountain.Cast(ally);
                }
                if (ally.HasBuff("summonerdot"))
                {
                    AkaLib.Item.Mountain.Cast(ally);
                }
            }
        }
    }
}
