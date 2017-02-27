using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;

namespace Aka_s_Draven.Features.Modules
{
    class Interrupte : IModule
    {
        public void OnLoad()
        {
            Interrupter.OnInterruptableSpell += Interrupter_OnInterruptableSpell;
        }

        private void Interrupter_OnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs e)
        {
            if (!Manager.MenuManager.UseEInterrupt || !Manager.SpellManager.E.IsReady() ||
                !sender.IsValidTarget(Manager.SpellManager.E.Range))
            {
                return;
            }

            if (e.DangerLevel == EloBuddy.SDK.Enumerations.DangerLevel.Medium || e.DangerLevel == EloBuddy.SDK.Enumerations.DangerLevel.High)
            {
                Manager.SpellManager.E.Cast(sender.Position);
            }
        }

        public ModuleType GetModuleType()
        {
            return ModuleType.OnUpdate;
        }

        public bool ShouldGetExecuted()
        {
            return false;
        }

        public void OnExecute()
        {
        }
    }
}

