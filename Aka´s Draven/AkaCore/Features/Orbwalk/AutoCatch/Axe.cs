using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace AkaCore.Features.Orbwalk.AutoCatch
{
    class Axe
    {
        private static Orbwalker.OrbwalkPositionDelegate GetReticlePosDelegate()
        {
            return () => bestreticlepos;
        }

        private static Orbwalker.OrbwalkPositionDelegate GetMousePos()
        {
            return () => Game.CursorPos;
        }

        private int LastAxeMoveTime { get; set; }
        public static List<QRecticle> QReticles { get; set; }
        private static Vector3 bestreticlepos = Vector3.Zero;
        private static bool Meleesafe = true;
        private static bool enemysafe = true;
        private static bool killsafe = true;

        public static void CatchAxe()
        {
            if (Manager.MenuManager.AxeMode == 2)
            {
                return;
            }

            if ((Manager.MenuManager.AxeMode == 0 && Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Combo)
                 || Manager.MenuManager.AxeMode == 1)
            {
                var bestReticle =
                    QReticles.Where(
                        x =>
                        x.Object.Position.Distance(Game.CursorPos)
                        < Manager.MenuManager.AxeCatchRange)
                        .OrderBy(x => x.Position.Distance(ObjectManager.Player.ServerPosition))
                        .ThenBy(x => x.ExpireTime)
                        .FirstOrDefault();

                if (bestReticle != null && bestReticle.Object.Position.Distance(ObjectManager.Player.ServerPosition) > 80)
                {
                    bestreticlepos = bestReticle.Position;
                    var eta = 1000 * (ObjectManager.Player.Distance(bestReticle.Position) / ObjectManager.Player.MoveSpeed);
                    var expireTime = bestReticle.ExpireTime - Environment.TickCount;

                    if (eta >= expireTime && Manager.MenuManager.AxeW)
                    {
                        Player.CastSpell(SpellSlot.W);
                    }

                    if (Manager.MenuManager.CatchMelees)
                    {
                        if (EntityManager.Heroes.Enemies.Count(
                            e => e.IsHPBarRendered && e.IsMelee && e.ServerPosition.Distance(bestReticle.Object.Position) < 350) >= 1)
                        {
                            Meleesafe = false;
                        }
                        else
                        {
                            Meleesafe = true;
                        }
                    }

                    if (Manager.MenuManager.CatchEnemies)
                    {
                        if (bestReticle.Object.Position.CountEnemiesInRange(500) > 2)
                        {
                            enemysafe = false;
                        }
                        else
                        {
                            enemysafe = true;
                        }
                    }

                    if (Manager.MenuManager.CatchKill)
                    {
                        var t = TargetSelector.GetTarget(800, DamageType.Physical);

                        if (t.IsValidTarget() && ObjectManager.Player.Distance(t.Position) > 400 && ObjectManager.Player.GetAutoAttackDamage(t) * 2 > t.Health)
                        {
                            killsafe = false;
                        }
                        else
                        {
                            killsafe = true;
                        }
                    }

                    if (Manager.MenuManager.CatchTower)
                    {
                        if (ObjectManager.Player.IsUnderEnemyturret() && UnderEnemyTurret(bestReticle.Object.Position) && Meleesafe && enemysafe && killsafe)
                        {
                            if (Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.None)
                            {
                                Player.IssueOrder(GameObjectOrder.MoveTo, bestReticle.Position);
                            }
                            else
                            {
                                Orbwalker.OverrideOrbwalkPosition = GetReticlePosDelegate();
                            }
                        }
                        else if (!UnderEnemyTurret(bestReticle.Object.Position) && Meleesafe && enemysafe && killsafe)
                        {
                            if (Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.None)
                            {
                                Player.IssueOrder(GameObjectOrder.MoveTo, bestReticle.Position);
                            }
                            else
                            {
                                Orbwalker.OverrideOrbwalkPosition = GetReticlePosDelegate();
                            }
                        }
                    }
                    else
                    {
                        if (Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.None && Meleesafe && enemysafe && killsafe)
                        {
                            Player.IssueOrder(GameObjectOrder.MoveTo, bestReticle.Position);
                        }
                        else
                        {
                            Orbwalker.OverrideOrbwalkPosition = GetReticlePosDelegate();
                        }
                    }
                }
                else
                {
                    Orbwalker.OverrideOrbwalkPosition = GetMousePos();
                }
            }
            else
            {
                Orbwalker.OverrideOrbwalkPosition = GetMousePos();
            }
        }

        private static bool UnderEnemyTurret(Vector3 pos)
        {
            return ObjectManager.Get<Obj_AI_Turret>().Any(t => t.IsEnemy && !t.IsDead && pos.Distance(t) <= 900);
        }

        internal class QRecticle
        {

            public QRecticle(GameObject rectice, int expireTime)
            {
                Object = rectice;
                ExpireTime = expireTime;
            }


            //Time
            public int ExpireTime { get; set; }


            //Object
            public GameObject Object { get; set; }

            //Position
            public Vector3 Position
            {
                get
                {
                    return Object.Position;
                }
            }
        }
    }
}
