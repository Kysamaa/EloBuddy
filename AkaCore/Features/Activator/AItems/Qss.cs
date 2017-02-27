using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;

namespace AkaCore.Features.Activator.AItems
{
    class Qss : IModule
    {
        public void OnLoad()
        {
            Obj_AI_Base.OnBuffGain += Obj_AI_Base_OnBuffGain;
        }

        public ModuleType GetModuleType()
        {
            return ModuleType.Other;
        }

        private static void Obj_AI_Base_OnBuffGain(Obj_AI_Base sender, Obj_AI_BaseBuffGainEventArgs args)
        {
            if (!sender.IsMe) return;

            if (args.Buff.Type == BuffType.Taunt && AkaCore.Manager.MenuManager.QssTaunt)
            {
                DoQSS();
            }
            if (args.Buff.Type == BuffType.Stun && AkaCore.Manager.MenuManager.QssStun)
            {
                DoQSS();
            }
            if (args.Buff.Type == BuffType.Snare && AkaCore.Manager.MenuManager.QssSnare)
            {
                DoQSS();
            }
            if (args.Buff.Type == BuffType.Polymorph && AkaCore.Manager.MenuManager.QssPolymorph)
            {
                DoQSS();
            }
            if (args.Buff.Type == BuffType.Blind && AkaCore.Manager.MenuManager.QssBlind)
            {
                DoQSS();
            }
            if (args.Buff.Type == BuffType.Flee && AkaCore.Manager.MenuManager.QssFear)
            {
                DoQSS();
            }
            if (args.Buff.Type == BuffType.Charm && AkaCore.Manager.MenuManager.QssCharm)
            {
                DoQSS();
            }
            if (args.Buff.Type == BuffType.Suppression && AkaCore.Manager.MenuManager.QssSupression)
            {
                DoQSS();
            }
            if (args.Buff.Type == BuffType.Silence && AkaCore.Manager.MenuManager.QssSilence)
            {
                DoQSS();
            }
            if (args.Buff.Name == "zedulttargetmark")
            {
                UltQSS();
            }
            if (args.Buff.Name == "VladimirHemoplague")
            {
                UltQSS();
            }
            if (args.Buff.Name == "FizzMarinerDoom")
            {
                UltQSS();
            }
            if (args.Buff.Name == "MordekaiserChildrenOfTheGrave")
            {
                UltQSS();
            }
            if (args.Buff.Name == "PoppyDiplomaticImmunity")
            {
                UltQSS();
            }
        }

        private static void DoQSS()
        {
            if (AkaLib.Item.Qss.IsOwned() && AkaLib.Item.Qss.IsReady() && ObjectManager.Player.CountEnemiesInRange(1800) > 0 && AkaCore.Manager.MenuManager.Qss)
            {
                Core.DelayAction(() => AkaLib.Item.Qss.Cast(), AkaCore.Manager.MenuManager.QssDelay);
            }

            if (AkaLib.Item.Mercurial.IsOwned() && AkaLib.Item.Mercurial.IsReady() && ObjectManager.Player.CountEnemiesInRange(1800) > 0 && AkaCore.Manager.MenuManager.Mecurial)
            {
                Core.DelayAction(() => AkaLib.Item.Mercurial.Cast(), AkaCore.Manager.MenuManager.QssDelay);
            }

            if (AkaLib.Item.Cleanse != null && AkaLib.Item.Cleanse.IsReady() && ObjectManager.Player.CountEnemiesInRange(1800) > 0 && AkaCore.Manager.MenuManager.Cleanse)
            {
                Core.DelayAction(() => AkaLib.Item.Cleanse.Cast(), AkaCore.Manager.MenuManager.QssDelay);
            }
        }

        private static void UltQSS()
        {
            if (AkaLib.Item.Qss.IsOwned() && AkaLib.Item.Qss.IsReady() && AkaCore.Manager.MenuManager.Qss)
            {
                Core.DelayAction(() => AkaLib.Item.Qss.Cast(), AkaCore.Manager.MenuManager.QssDelay);
            }

            if (AkaLib.Item.Mercurial.IsOwned() && AkaLib.Item.Mercurial.IsReady() && AkaCore.Manager.MenuManager.Mecurial)
            {
                Core.DelayAction(() => AkaLib.Item.Mercurial.Cast(), AkaCore.Manager.MenuManager.QssDelay);
            }

            if (AkaLib.Item.Cleanse != null && AkaLib.Item.Cleanse.IsReady() && ObjectManager.Player.CountEnemiesInRange(1800) > 0 && AkaCore.Manager.MenuManager.Cleanse)
            {
                Core.DelayAction(() => AkaLib.Item.Cleanse.Cast(), AkaCore.Manager.MenuManager.QssDelay);
            }
        }

        public bool ShouldGetExecuted()
        {
            return false;
        }

        public void OnExecute() { }
    }
}
