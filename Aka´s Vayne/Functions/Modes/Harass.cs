
using System.Linq;
using AddonTemplate.Logic;
using EloBuddy;
using EloBuddy.SDK;

namespace Aka_s_Vayne_reworked.Functions.Modes
{
    class Harass
    {
        public static void SilverStackQ()
        {
            foreach (AIHeroClient qTarget in EntityManager.Heroes.Enemies.Where(x => x.IsValidTarget(550)))
            {
                if (qTarget.Buffs.Any(buff => buff.Name == "vaynesilvereddebuff" && buff.Count == 2) && !Game.CursorPos.IsSafe())
                {
                    Player.CastSpell(SpellSlot.Q, Game.CursorPos);
                }
            }
        }
        public static void SilverStackE()
        {
            foreach (AIHeroClient qTarget in EntityManager.Heroes.Enemies.Where(x => x.IsValidTarget(550)))
            {
                if (qTarget.Buffs.Any(buff => buff.Name == "vaynesilvereddebuff" && buff.Count == 2))
                {
                    Program.E.Cast(qTarget);
                }
            }
        }

        public static void SilverStackC()
        {
            foreach (AIHeroClient qTarget in EntityManager.Heroes.Enemies.Where(x => x.IsValidTarget(550)))
            {
                if (qTarget.Buffs.Any(buff => buff.Name == "vaynesilvereddebuff" && buff.Count == 1))
                {
                    Player.CastSpell(SpellSlot.Q, Game.CursorPos);
                }

                if (qTarget.Buffs.Any(buff => buff.Name == "vaynesilvereddebuff" && buff.Count == 2))
                {
                    Program.E.Cast(qTarget);
                }
            }
        }
    }
}

