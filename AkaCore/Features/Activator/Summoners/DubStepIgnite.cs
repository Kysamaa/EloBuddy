using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK;
using EloBuddy;

namespace AkaCore.Features.Activator.Summoners
{
    class Ignite : IModule
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
            return AkaCore.Manager.MenuManager.Ignite && AkaLib.Item.Ignite != null && AkaLib.Item.Ignite.IsReady();
        }

        public void OnExecute()
        {
            var unit = TargetSelector.GetTarget(AkaLib.Item.Ignite.Range, DamageType.Physical);

            var ksunit = EntityManager.Heroes.Enemies.FirstOrDefault(
                        hero =>
                        hero.IsValidTarget(600) && !hero.IsZombie
                        && ObjectManager.Player.GetSummonerSpellDamage(hero, DamageLibrary.SummonerSpells.Ignite) > hero.Health);


            if (AkaCore.Manager.MenuManager.IgniteHp > 0 && unit != null && unit.HealthPercent <= AkaCore.Manager.MenuManager.ExhaustHp)
            {
                AkaLib.Item.Ignite.Cast(unit);
            }

            if (AkaCore.Manager.MenuManager.IgniteHp == 0 && ksunit != null)
            {
                AkaLib.Item.Ignite.Cast(ksunit);
            }
        }
    }
}
