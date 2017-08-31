using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

namespace AkaDraven.Events
{
    class _objaibase
    {
        public static void BuffGain(Obj_AI_Base sender, Obj_AI_BaseBuffGainEventArgs buff)
        { 
            if (!sender.IsMe) return;

            if (buff.Buff.Type == BuffType.Taunt && MenuManager.ItemMenu["Taunt"].Cast<CheckBox>().CurrentValue)
            {
                Functions.Events._objaibase.DoQSS();
            }
            if (buff.Buff.Type == BuffType.Stun && MenuManager.ItemMenu["Stun"].Cast<CheckBox>().CurrentValue)
            {
                Functions.Events._objaibase.DoQSS();
            }
            if (buff.Buff.Type == BuffType.Snare && MenuManager.ItemMenu["Snare"].Cast<CheckBox>().CurrentValue)
            {
                Functions.Events._objaibase.DoQSS();
            }
            if (buff.Buff.Type == BuffType.Polymorph && MenuManager.ItemMenu["Polymorph"].Cast<CheckBox>().CurrentValue)
            {
                Functions.Events._objaibase.DoQSS();
            }
            if (buff.Buff.Type == BuffType.Blind && MenuManager.ItemMenu["Blind"].Cast<CheckBox>().CurrentValue)
            {
                Functions.Events._objaibase.DoQSS();
            }
            if (buff.Buff.Type == BuffType.Flee && MenuManager.ItemMenu["Fear"].Cast<CheckBox>().CurrentValue)
            {
                Functions.Events._objaibase.DoQSS();
            }
            if (buff.Buff.Type == BuffType.Charm && MenuManager.ItemMenu["Charm"].Cast<CheckBox>().CurrentValue)
            {
                Functions.Events._objaibase.DoQSS();
            }
            if (buff.Buff.Type == BuffType.Suppression && MenuManager.ItemMenu["Suppression"].Cast<CheckBox>().CurrentValue)
            {
                Functions.Events._objaibase.DoQSS();
            }
            if (buff.Buff.Type == BuffType.Silence && MenuManager.ItemMenu["Silence"].Cast<CheckBox>().CurrentValue)
            {
                Functions.Events._objaibase.DoQSS();
            }
            if (buff.Buff.Name == "zedulttargetmark")
            {
                Functions.Events._objaibase.UltQSS();
            }
            if (buff.Buff.Name == "VladimirHemoplague")
            {
                Functions.Events._objaibase.UltQSS();
            }
            if (buff.Buff.Name == "FizzMarinerDoom")
            {
                Functions.Events._objaibase.UltQSS();
            }
            if (buff.Buff.Name == "MordekaiserChildrenOfTheGrave")
            {
                Functions.Events._objaibase.UltQSS();
            }
            if (buff.Buff.Name == "PoppyDiplomaticImmunity")
            {
                Functions.Events._objaibase.UltQSS();
            }
        }

        public static void OnNewPath(Obj_AI_Base sender, GameObjectNewPathEventArgs args)
        {
            if (!sender.IsMe)
            {
                return;
            }

            Modes.PermaActive.CatchAxe();
        }
    }
}

    

