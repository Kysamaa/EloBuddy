using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;

namespace Aka_s_Draven.Features.Modules
{
    class KSE : IModule
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
            return Manager.SpellManager.E.IsReady();
        }

        public void OnExecute()
        {
            foreach (AIHeroClient enemy in EntityManager.Heroes.Enemies)
            {
                if (enemy.IsValidTarget(Manager.SpellManager.E.Range))
                {
                    if (Manager.MenuManager.KSE && (Variables._Player.GetSpellDamage(enemy, SpellSlot.E) >= enemy.Health))
                    {
                        var predHealth = Prediction.Health.GetPrediction(enemy, (int)(Variables._Player.Distance(enemy.Position) * 1000 / 2000));
                        if (predHealth <= Variables._Player.GetSpellDamage(enemy, SpellSlot.E))
                        {
                            var PredE = Manager.SpellManager.E.GetPrediction(enemy);
                            if (PredE.HitChance >= EloBuddy.SDK.Enumerations.HitChance.Medium)
                            {
                                Manager.SpellManager.E.Cast(enemy);
                            }
                        }
                    }
                }
            }
        }
    }
}

