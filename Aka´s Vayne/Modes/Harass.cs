using System.Linq;
using AddonTemplate.Logic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu.Values;

// Using the config like this makes your life easier, trust me
using Settings = AddonTemplate.Config.Modes.Harass;

namespace AddonTemplate.Modes
{
    public sealed class Harass : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            // Only execute this mode when the orbwalker is on harass mode
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass);
        }

        public override void Execute()
        {
            if (ObjectManager.Player.ManaPercent < Settings.Mana)
                return;
            if (Settings.UseQ && SpellManager.Q.IsReady())
            {
                SilverStackQ();
            }
            if (Settings.UseE && SpellManager.E.IsReady())
            {
                SilverStackE();
            }
        }
        public void SilverStackQ()
        {
                foreach (AIHeroClient qTarget in HeroManager.Enemies.Where(x => x.IsValidTarget(550)))
                {
                    if (qTarget.Buffs.Any(buff => buff.Name == "vaynesilvereddebuff" && buff.Count == 2) && !Game.CursorPos.IsDangerousPosition())
                    {
                    Player.CastSpell(SpellSlot.Q, Game.CursorPos);
                }
                }
            }
        public void SilverStackE()
        {
                foreach (AIHeroClient qTarget in HeroManager.Enemies.Where(x => x.IsValidTarget(550)))
                {
                    if (qTarget.Buffs.Any(buff => buff.Name == "vaynesilvereddebuff" && buff.Count == 2))
                    {
                        SpellManager.E.Cast(qTarget);
                    }
                }
            }

        }
    }
    


