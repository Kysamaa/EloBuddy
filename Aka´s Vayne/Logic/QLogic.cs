using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

namespace AddonTemplate.Logic
{
    static class QLogic
    {
        public static void CastTumble(Vector3 position, Obj_AI_Base target)
        {
            if (!SpellManager.Q.IsReady())
            {
                return;
            }

            var positionAfter = ObjectManager.Player.ServerPosition.To2D().Extend(Game.CursorPos.To2D(), 300f).To3D();
            var distanceAfterTumble = Vector3.DistanceSquared(positionAfter, target.ServerPosition);

            if (distanceAfterTumble <= 550 * 550 && distanceAfterTumble >= 100 * 100 && (!positionAfter.IsDangerousPosition()))
            {
                Player.CastSpell(SpellSlot.Q, position);
            }
            if (Config.Modes.Combo.Kite &&
                EntityManager.Heroes.Enemies.Any(
                    a => a.IsMelee && a.Distance(Player.Instance) < a.GetAutoAttackRange(Player.Instance)))
            {
                Player.CastSpell(SpellSlot.Q,
                    target.Position.Extend(Player.Instance.Position,
                        target.Position.Distance(Player.Instance) + 300).To3D());
            }
        }
        public static void QCombo(Obj_AI_Base target)
        {
            foreach (AIHeroClient qTarget in HeroManager.Enemies.Where(x => x.IsValidTarget(550)))
            {
                if (!Game.CursorPos.IsDangerousPosition())
                {
                    Player.CastSpell(SpellSlot.Q, Game.CursorPos);
                }
            }
            if (Config.Modes.Combo.Kite &&
                EntityManager.Heroes.Enemies.Any(
                    a => a.IsMelee && a.Distance(Player.Instance) < a.GetAutoAttackRange(Player.Instance)))
            {
                Player.CastSpell(SpellSlot.Q,
                    target.Position.Extend(Player.Instance.Position,
                        target.Position.Distance(Player.Instance) + 300).To3D());
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
