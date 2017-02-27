using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK;
using EloBuddy;

namespace AkaCore.Features.Utility.Modules
{
    class Autolvl : IModule
    {
        private static int[] AbilitySequence;

        private static int QOff = 0, WOff = 0, EOff = 0, ROff = 0;

        public void OnLoad()
        {

        }

        public ModuleType GetModuleType()
        {
            return ModuleType.OnUpdate;
        }

        public bool ShouldGetExecuted()
        {
            return Manager.MenuManager.Autolvl;
        }

        public void OnExecute()
        {
            if (ObjectManager.Player.Level < 3) return;

            switch (Manager.MenuManager.AutolvlSlider)
            {
                case 0:
                    AbilitySequence = new[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    break;
                case 1:
                    AbilitySequence = new[] { 1, 2, 3, 2, 2, 4, 2, 1, 2, 1, 4, 1, 1, 3, 3, 4, 3, 3 };
                    break;
                case 2:
                    AbilitySequence = new[] { 1, 2, 3, 3, 3, 4, 3, 2, 3, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                    break;
            }

            var qL = ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Q).Level + QOff;
            var wL = ObjectManager.Player.Spellbook.GetSpell(SpellSlot.W).Level + WOff;
            var eL = ObjectManager.Player.Spellbook.GetSpell(SpellSlot.E).Level + EOff;
            var rL = ObjectManager.Player.Spellbook.GetSpell(SpellSlot.R).Level + ROff;
            if (qL + wL + eL + rL >= ObjectManager.Player.Level) return;
            int[] level = { 0, 0, 0, 0 };
            for (var i = 0; i < ObjectManager.Player.Level; i++)
            {
                level[AbilitySequence[i] - 1] = level[AbilitySequence[i] - 1] + 1;
            }
            if (qL < level[0]) ObjectManager.Player.Spellbook.LevelSpell(SpellSlot.Q);
            if (wL < level[1]) ObjectManager.Player.Spellbook.LevelSpell(SpellSlot.W);
            if (eL < level[2]) ObjectManager.Player.Spellbook.LevelSpell(SpellSlot.E);
            if (rL < level[3]) ObjectManager.Player.Spellbook.LevelSpell(SpellSlot.R);
        }
    }
}