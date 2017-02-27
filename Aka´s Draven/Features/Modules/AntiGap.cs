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
    class Antigap : IModule
    {
        public void OnLoad()
        {
            EloBuddy.SDK.Events.Gapcloser.OnGapcloser += Gapcloser_OnGapCloser;
        }

        private static void Gapcloser_OnGapCloser(AIHeroClient sender, EloBuddy.SDK.Events.Gapcloser.GapcloserEventArgs e)
        {
            if (sender == null || sender.IsAlly) return;

            if ((e.End.Distance(Variables._Player) <= sender.GetAutoAttackRange()) && Manager.MenuManager.AntigapE && Manager.SpellManager.E.IsReady())
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