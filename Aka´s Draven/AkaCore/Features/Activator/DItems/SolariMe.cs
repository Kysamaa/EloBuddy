using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK;
using EloBuddy;

namespace AkaCore.Features.Activator.DItems
{
    class SolariMe : IModule
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
            return AkaCore.Manager.MenuManager.DItems && AkaCore.Manager.MenuManager.SolariMe && AkaLib.Item.Solari != null && AkaLib.Item.Solari.IsReady();
        }

        public void OnExecute()
        {
            if (ObjectManager.Player.CountEnemiesInRange(800) >= 1 && ObjectManager.Player.HealthPercent <= AkaCore.Manager.MenuManager.SolraiMeHp)
            {
                AkaLib.Item.Solari.Cast(ObjectManager.Player);
            }
            if (ObjectManager.Player.HasBuff("summonerdot") && ObjectManager.Player.HealthPercent <= AkaCore.Manager.MenuManager.SolraiMeHp)
            {
                AkaLib.Item.Solari.Cast(ObjectManager.Player);
            }
        }
    }
}
