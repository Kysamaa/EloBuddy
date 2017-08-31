using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Input;
using SharpDX;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;

namespace AddonTemplate.Utility
{
    public static class Interrupter2
    {
        public delegate void InterruptableTargetHandler(AIHeroClient sender, InterruptableTargetEventArgs args);

        public enum DangerLevel
        {
            Low,
            Medium,
            High
        }

        static Interrupter2()
        {
            // Initialize Properties
            InterruptableSpells = new Dictionary<string, List<InterruptableSpell>>();
            CastingInterruptableSpell = new Dictionary<int, InterruptableSpell>();

            InitializeSpells();

            // Trigger LastCastedSpell
            ObjectManager.Player.LastCastedspell();

            // Listen to required events
            Game.OnUpdate += Game_OnGameUpdate;
            Obj_AI_Base.OnProcessSpellCast += Obj_AI_Base_OnProcessSpellCast;
            Spellbook.OnStopCast += Spellbook_OnStopCast;
        }

        private static Dictionary<string, List<InterruptableSpell>> InterruptableSpells { get; set; }
        private static Dictionary<int, InterruptableSpell> CastingInterruptableSpell { get; set; }

        public static event InterruptableTargetHandler OnInterruptableTarget;

        private static void InitializeSpells()
        {
            #region Spells

            RegisterSpell("Caitlyn", new InterruptableSpell(SpellSlot.R, DangerLevel.High));
            RegisterSpell("FiddleSticks", new InterruptableSpell(SpellSlot.W, DangerLevel.Medium));
            RegisterSpell("FiddleSticks", new InterruptableSpell(SpellSlot.R, DangerLevel.High));
            RegisterSpell("Galio", new InterruptableSpell(SpellSlot.R, DangerLevel.High));
            RegisterSpell("Janna", new InterruptableSpell(SpellSlot.R, DangerLevel.Low));
            RegisterSpell("Karthus", new InterruptableSpell(SpellSlot.R, DangerLevel.High));
            RegisterSpell("Katarina", new InterruptableSpell(SpellSlot.R, DangerLevel.High));
            RegisterSpell("Lucian", new InterruptableSpell(SpellSlot.R, DangerLevel.High, false));
            RegisterSpell("Malzahar", new InterruptableSpell(SpellSlot.R, DangerLevel.High));
            RegisterSpell("MasterYi", new InterruptableSpell(SpellSlot.W, DangerLevel.Low));
            RegisterSpell("MissFortune", new InterruptableSpell(SpellSlot.R, DangerLevel.High));
            RegisterSpell("Nunu", new InterruptableSpell(SpellSlot.R, DangerLevel.High));
            RegisterSpell("Pantheon", new InterruptableSpell(SpellSlot.E, DangerLevel.Low));
            RegisterSpell("Pantheon", new InterruptableSpell(SpellSlot.R, DangerLevel.High));
            RegisterSpell("RekSai", new InterruptableSpell(SpellSlot.R, DangerLevel.High));
            RegisterSpell("Sion", new InterruptableSpell(SpellSlot.R, DangerLevel.Low));
            RegisterSpell("Shen", new InterruptableSpell(SpellSlot.R, DangerLevel.Low));
            RegisterSpell("TwistedFate", new InterruptableSpell(SpellSlot.R, DangerLevel.Medium));
            RegisterSpell("Urgot", new InterruptableSpell(SpellSlot.R, DangerLevel.High));
            RegisterSpell("Velkoz", new InterruptableSpell(SpellSlot.R, DangerLevel.High));
            RegisterSpell("Warwick", new InterruptableSpell(SpellSlot.R, DangerLevel.High));
            RegisterSpell("Xerath", new InterruptableSpell(SpellSlot.R, DangerLevel.High));
            RegisterSpell("Varus", new InterruptableSpell(SpellSlot.Q, DangerLevel.Low, false));

            #endregion
        }

        private static void RegisterSpell(string champName, InterruptableSpell spell)
        {
            if (!InterruptableSpells.ContainsKey(champName))
            {
                InterruptableSpells.Add(champName, new List<InterruptableSpell>());
            }

            InterruptableSpells[champName].Add(spell);
        }

        private static void Game_OnGameUpdate(EventArgs args)
        {
            // Remove heros that have finished casting their interruptable spell
            EntityManager.Heroes.AllHeroes.ForEach(
                hero =>
                {
                    if (CastingInterruptableSpell.ContainsKey(hero.NetworkId) &&
                        //!hero.Spellbook.IsCastingSpell &&
                        !hero.Spellbook.IsChanneling &&
                        !hero.Spellbook.IsCharging)
                    {
                        CastingInterruptableSpell.Remove(hero.NetworkId);
                    }
                });

            // Trigger OnInterruptableTarget event if needed
            if (OnInterruptableTarget != null)
            {
                EntityManager.Heroes.Enemies.ForEach(
                    enemy =>
                    {
                        var newArgs = GetInterruptableTargetData(enemy);
                        if (newArgs != null)
                        {
                            OnInterruptableTarget(enemy, newArgs);
                        }
                    });
            }
        }

        private static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            var target = sender as AIHeroClient;
            if (target != null && !CastingInterruptableSpell.ContainsKey(target.NetworkId))
            {
                // Check if the target is known to have interruptable spells
                if (InterruptableSpells.ContainsKey(target.ChampionName))
                {
                    // Get the interruptable spell
                    var spell =
                        InterruptableSpells[target.ChampionName].Find(
                            s => s.Slot == target.GetSpellSlot(args.SData.Name));
                    if (spell != null)
                    {
                        // Mark champ as casting interruptable spell
                        CastingInterruptableSpell.Add(target.NetworkId, spell);
                    }
                }
            }
        }

        private static void Spellbook_OnStopCast(Obj_AI_Base sender, SpellbookStopCastEventArgs args)
        {
            var target = sender.Spellbook.Owner as AIHeroClient;
            if (target != null)
            {
                // Check if the spell itself stopped casting (interrupted)
                if (!target.Spellbook.IsCastingSpell && !target.Spellbook.IsChanneling && !target.Spellbook.IsCharging)
                {
                    CastingInterruptableSpell.Remove(target.NetworkId);
                }
            }
        }

        public static bool IsCastingInterruptableSpell(this AIHeroClient target, bool checkMovementInterruption = false)
        {
            var data = GetInterruptableTargetData(target);
            return data != null && (!checkMovementInterruption || data.MovementInterrupts);
        }

        public static InterruptableTargetEventArgs GetInterruptableTargetData(AIHeroClient target)
        {
            if (target.IsValid<AIHeroClient>())
            {
                if (CastingInterruptableSpell.ContainsKey(target.NetworkId))
                {
                    // Return the args with spell end time
                    return new InterruptableTargetEventArgs(
                        CastingInterruptableSpell[target.NetworkId].DangerLevel, target.Spellbook.CastEndTime, CastingInterruptableSpell[target.NetworkId].MovementInterrupts);
                }
            }

            return null;
        }

        public class InterruptableTargetEventArgs
        {
            public InterruptableTargetEventArgs(DangerLevel dangerLevel, float endTime, bool movementInterrupts)
            {
                DangerLevel = dangerLevel;
                EndTime = endTime;
                MovementInterrupts = movementInterrupts;
            }

            public DangerLevel DangerLevel { get; private set; }
            public float EndTime { get; private set; }
            public bool MovementInterrupts { get; private set; }
        }

        private class InterruptableSpell
        {
            public InterruptableSpell(SpellSlot slot, DangerLevel dangerLevel, bool movementInterrupts = true)
            {
                Slot = slot;
                DangerLevel = dangerLevel;
                MovementInterrupts = movementInterrupts;
            }

            public SpellSlot Slot { get; private set; }
            public DangerLevel DangerLevel { get; private set; }
            public bool MovementInterrupts { get; private set; }
        }
    }
}