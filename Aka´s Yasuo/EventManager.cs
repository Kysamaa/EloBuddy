using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace AkaYasuo
{
    internal class EventManager
    {
        public static void load()
        {
            Drawing.OnDraw += OnDraw;
            Game.OnUpdate += Game_OnUpdate;
            SkillshotDetector.OnDetectSkillshot += Evade_OnDetectSkillshot;
            SkillshotDetector.OnDeleteMissile += Evade_OnDeleteMissile;
            Obj_AI_Base.OnCreate += Obj_AI_Base_OnCreate;
            Obj_AI_Base.OnDelete += Obj_AI_Base_OnDelete;
            Spellbook.OnStopCast += Spellbook_OnStopCast;
            Obj_AI_Base.OnBuffGain += Obj_AI_Base_OnBuffGain;
            Obj_AI_Base.OnBuffLose += Obj_AI_Base_OnBuffLose;
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            Modes.PermaActive.AutoR();
            Modes.PermaActive.KillSteal();
            Modes.PermaActive.sChoose();

            Events._game.updateSkillshots();
            Events._game.Startdash();
            Events._game.Skillshotdetector();
            Events._game.AutoQ();
            Events._game.AutoQMinion();
            Events._game.LevelUpSpells();

            if (MenuManager.FleeMenu["WJ"].Cast<KeyBind>().CurrentValue)
            {
                Yasuo.WallDash();
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass)) Modes.Harass.Harassmode();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear)) Modes.JungleClear.JungleClearmode();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee)) Modes.Flee.Fleemode();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear)) Modes.LaneClear.LaneClearmode();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit)) Modes.LastHit.LastHitmode();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo)) Modes.Combo.Load();
        }

        private static void OnDraw(EventArgs args)
        {
            Events._drawing.Drawings(args);
        }

        private static void Obj_AI_Base_OnBuffGain(Obj_AI_Base sender, Obj_AI_BaseBuffGainEventArgs buff)
        {
            Events._objaibase.BuffGain(sender, buff);
        }

        private static void Obj_AI_Base_OnBuffLose(Obj_AI_Base sender, Obj_AI_BaseBuffLoseEventArgs buff)
        {
            Events._objaibase.BuffLose(sender, buff);
        }

        private static void Obj_AI_Base_OnDelete(GameObject sender, EventArgs args)
        {
            Events._objaibase.Delete(sender, args);
        }

        private static void Obj_AI_Base_OnCreate(GameObject sender, EventArgs args)
        {
            Events._objaibase.Create(sender, args);
        }

        private static void Evade_OnDetectSkillshot(Skillshot skillshot)
        {
            Events._skillshotdetector.DetectSkillshot(skillshot);
        }

        private static void Evade_OnDeleteMissile(Skillshot skillshot, MissileClient missile)
        {
            Events._skillshotdetector.DeleteMissile(skillshot, missile);
        }

        private static void Spellbook_OnStopCast(Obj_AI_Base sender, SpellbookStopCastEventArgs args)
        {
            Events._spellbook.StopCast(sender, args);
        }
    }
}
