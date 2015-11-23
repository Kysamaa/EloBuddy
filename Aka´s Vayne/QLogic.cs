using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;

namespace AddonTemplate
{
    class QLogic
    {
        public static void QCombo()
        {
            foreach (AIHeroClient qTarget in HeroManager.Enemies.Where(x => x.IsValidTarget(550)))
            {
                Player.CastSpell(SpellSlot.Q, Game.CursorPos);
            }
        }

        public static void QJungleClear()
        {
            var mob =
                EntityManager.MinionsAndMonsters.GetJungleMonsters(ObjectManager.Player.ServerPosition,
                    SpellManager.E.Range + 100).Where(t => !t.IsDead && t.IsValid && !t.IsInvulnerable);
            foreach (var m in mob)
            {
                Player.CastSpell(SpellSlot.Q, Game.CursorPos);
            }

        }
    }
}
