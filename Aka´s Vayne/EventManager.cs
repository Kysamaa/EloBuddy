using System;
using AddonTemplate;
using AddonTemplate.Logic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using Gapcloser = EloBuddy.SDK.Events.Gapcloser;
using Aka_s_Vayne_reworked.Events;
using Aka_s_Vayne_reworked.Logic;
using EloBuddy.SDK.Menu.Values;

namespace Aka_s_Vayne_reworked
{
    internal class EventManager
    {

        public static void Load()
        {
            Gapcloser.OnGapcloser += Gapcloser_OnGapCloser;
            Interrupter.OnInterruptableSpell += Interrupter_OnInterruptableSpell;
            Orbwalker.OnPostAttack += Orbwalker_OnPostAttack;
            Obj_AI_Base.OnBasicAttack += Obj_Ai_Base_OnBasicAttack;
            Obj_AI_Base.OnProcessSpellCast += Obj_AI_Base_OnProcessSpell;
            Obj_AI_Base.OnBuffGain += Obj_AI_Base_OnBuffGain;
            Obj_AI_Base.OnSpellCast += Obj_AI_Base_OnSpellCast;
            //Spellbook.OnCastSpell += Spellbook_OnCastSpell;
            GameObject.OnCreate += GameObject_OnCreate;
            Game.OnTick += Game_OnTick;
            Game.OnUpdate += Game_OnUpdate;
            Player.OnIssueOrder += Player_OnIssueOrder;
            Drawing.OnDraw += OnDraw;
            Mechanics.LoadFlash();
            Traps.Load();
            Turrets.Load();
            Evade.EvadeHelper.OnLoad();
        }

        public static void Game_OnUpdate(EventArgs args)
        {
            _game.heal();
            _game.Skinhack();
            _game.QKs();
        }

        public static void Game_OnTick(EventArgs args)
        {
            _game.AutoBuyTrinkets();
            _game.FocusW();
            _game.autoBuy();
            _game.LevelUpSpells();
            _game.AutoPotions();
            _game.LowlifeE();
            _game.QKs();

            AutoE.OnExecute();

            Mechanics.FlashE();
            Mechanics.Insec();

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass)) Modes.Harass.Load();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear)) Modes.JungleClear.Load();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee)) Modes.Flee.Load();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo)) Modes.Combo.Load();
          //if ((Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear)) ||
          //(Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit))) Modes.LCLH.Load();
          if (MenuManager.ComboMenu["AAReset"].Cast<CheckBox>().CurrentValue) _game.EloBuddyOrbDisabler();
        }

        public static void GameObject_OnCreate(GameObject sender, EventArgs args)
        {
            _gameObject.AntiRenger(sender, args);
        }

        public static void Gapcloser_OnGapCloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            _Gapclose.GapcloseE(sender, e);
        }

        public static void Interrupter_OnInterruptableSpell(Obj_AI_Base sender,
            Interrupter.InterruptableSpellEventArgs e)
        {
            _interrupter.Interrupt(sender, e);
        }

        public static void Obj_Ai_Base_OnBasicAttack(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            _objaibase.AutoAttack(sender, args);
            _objaibase.AutoAttack2(sender, args);
        }

        public static void Obj_AI_Base_OnBuffGain(Obj_AI_Base sender, Obj_AI_BaseBuffGainEventArgs args)
        {
            _objaibase.BuffGain(sender, args);
        }

        public static void Obj_AI_Base_OnProcessSpell(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            _objaibase.ProcessSpell(sender, args);
        }

        public static void Player_OnIssueOrder(Obj_AI_Base sender, PlayerIssueOrderEventArgs args)
        {
            _player.IssueOrder(sender, args);
        }

        public static void Obj_AI_Base_OnSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            _objaibase.SpellCast(sender, args);
        }

        public static void Spellbook_OnCastSpell(Spellbook sender, SpellbookCastSpellEventArgs args)
        {
            NewELogic.Spellbook_OnCastSpell(sender, args);
        }

        public static void OnDraw(EventArgs args)
        {
            _drawing.Drawing();
        }

        public static void Orbwalker_OnPostAttack(AttackableUnit target, EventArgs args)
        {
            _orbwalker.Condemnexecute(target, args);
        }
    }
}
