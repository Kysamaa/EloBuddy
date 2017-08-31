using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;

namespace AkaDraven
{
    internal class EventManager
    {
        public static void load()
        {
            Drawing.OnDraw += OnDraw;
            Game.OnUpdate += Game_OnUpdate;
            GameObject.OnCreate += GameObjectOnCreate;
            GameObject.OnDelete += GameObjectOnDelete;
            Obj_AI_Base.OnBuffGain += Obj_AI_Base_OnBuffGain;
            Interrupter.OnInterruptableSpell += Interrupter_OnInterruptableSpell;
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            Modes.PermaActive.KillSteal();
            Events._game.OnUpdate();
            Events._game.LevelUpSpells();

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass)) Modes.Harass.Execute();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee)) Modes.Flee.Execute();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear)) Modes.LaneClear.Execite();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo)) Modes.Combo.Execute();
        }

        private static void OnDraw(EventArgs args)
        {
            Events._drawing.Drawings(args);
        }

        private static void Obj_AI_Base_OnBuffGain(Obj_AI_Base sender, Obj_AI_BaseBuffGainEventArgs buff)
        {
            Events._objaibase.BuffGain(sender, buff);
        }

        private static void GameObjectOnDelete(GameObject sender, EventArgs args)
        {
            Events._gameobject.Delete(sender, args);
        }

        private static void GameObjectOnCreate(GameObject sender, EventArgs args)
        {
            Events._gameobject.Create(sender, args);
        }

        private static void Interrupter_OnInterruptableSpell(Obj_AI_Base sender,
            Interrupter.InterruptableSpellEventArgs e)
        {
            Events._interrupter.Interrupter(sender, e);
        }
    }
}
