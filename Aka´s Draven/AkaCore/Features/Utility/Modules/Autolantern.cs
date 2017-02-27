using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;

namespace AkaCore.Features.Utility.Modules
{
    class AutoLantern : IModule
    {
        public static bool ThreshInGame()
        {
            return ObjectManager.Get<AIHeroClient>().Any(h => h.IsAlly && !h.IsMe && h.ChampionName == "Thresh");
        }

        private const String LanternName = "ThreshLantern";

        public void OnLoad()
        {

        }

        public ModuleType GetModuleType()
        {
            return ModuleType.OnUpdate;
        }

        public bool ShouldGetExecuted()
        {
            return Manager.MenuManager.Autolantern && Manager.MenuManager.AutlanternHp >= ObjectManager.Player.HealthPercent && ThreshInGame();
        }

        public void OnExecute()
        {
            var lantern =
                ObjectManager.Get<Obj_AI_Base>().FirstOrDefault(o => o.IsValid && o.IsAlly && o.Name.Equals(LanternName));

            if (lantern != null && ObjectManager.Player.Distance(lantern) <= 500 && ObjectManager.Player.Spellbook.GetSpell((SpellSlot)62).Name.Equals("LanternWAlly"))
            {
                ObjectManager.Player.Spellbook.CastSpell((SpellSlot)62, lantern);
            }
        }
    }
}
