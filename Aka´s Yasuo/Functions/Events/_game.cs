
using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

namespace AkaYasuo.Functions.Events
{
    class _game
    {
        public struct IsSafeResult
        {
            public bool IsSafe;
            public List<Skillshot> SkillshotList;
            public List<Obj_AI_Base> casters;
        }


        public static IsSafeResult isSafePoint(Vector2 point, bool igonre = false)
        {
            var result = new IsSafeResult();
            result.SkillshotList = new List<Skillshot>();
            result.casters = new List<Obj_AI_Base>();


            bool safe = (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo)) ||
                            point.To3D().CountEnemiesInRange(500) > Variables._Player.HealthPercent % 65;
            if (!safe)
            {
                result.IsSafe = false;
                return result;
            }

            foreach (var skillshot in Variables.EvadeDetectedSkillshots)
            {
                if (skillshot.IsDanger(point) && skillshot.IsAboutToHit(500, Variables._Player))
                {
                    result.SkillshotList.Add(skillshot);
                    result.casters.Add(skillshot.Unit);
                }
            }

            result.IsSafe = (result.SkillshotList.Count == 0);
            return result;
        }

        public static void useWSmart(Skillshot skillShot)
        {
            //try doge with E if cant windWall
            var delay = MenuManager.DogeMenu["smartWD"].Cast<Slider>().CurrentValue;
            if (skillShot.IsAboutToHit(delay, Variables._Player))
            {
                if (!Program.W.IsReady() || skillShot.SpellData.Type == SkillShotType.SkillshotRing)
                    return;

                var sd = SpellDatabase.GetByMissileName(skillShot.SpellData.MissileSpellName);
                if (sd == null)
                    return;

                //If enabled
                if (!Variables.EvadeSpellEnabled(sd.MenuItemName))
                    return;

                //if only dangerous
                if (MenuManager.DogeMenu["wwDanger"].Cast<CheckBox>().CurrentValue &&
                    !Variables.skillShotIsDangerous(sd.MenuItemName))
                    return;

                Variables._Player.Spellbook.CastSpell(SpellSlot.W, skillShot.Start.To3D(), skillShot.End.To3D());
            }
        }

        public static void useEtoSafe(Skillshot skillShot)
        {
            if (!Program.E.IsReady())
                return;
            float closest = float.MaxValue;
            Obj_AI_Base closestTarg = null;
            float currentDashSpeed = 700 + Variables._Player.MoveSpeed;
            foreach (
                Obj_AI_Base enemy in
                    ObjectManager.Get<Obj_AI_Base>()
                        .Where(
                            ob =>
                                ob.NetworkId != skillShot.Unit.NetworkId && Variables.enemyIsJumpable(ob) &&
                                ob.Distance(Variables._Player) < Program.E.Range)
                        .OrderBy(ene => ene.Distance(Game.CursorPos, true)))
            {
                var pPos = Variables._Player.Position.To2D();
                Vector2 posAfterE = Variables.V2E(Variables._Player.Position, enemy.Position, 475);
                Vector2 dashDir = (posAfterE - Variables._Player.Position.To2D()).Normalized();

                if (isSafePoint(posAfterE).IsSafe && Variables.wontHitOnDash(skillShot, enemy, skillShot, dashDir)
                    /*&& skillShot.IsSafePath(new List<Vector2>() { posAfterE }, 0, (int)currentDashSpeed, 0).IsSafe*/)
                {
                    float curDist = Vector2.DistanceSquared(Game.CursorPos.To2D(), posAfterE);
                    if (curDist < closest)
                    {
                        closestTarg = enemy;
                        closest = curDist;
                    }
                }
            }
            if (closestTarg != null)
                useENormal(closestTarg);
        }


        public static bool useENormal(Obj_AI_Base target)
        {
            if (!Program.E.IsReady() || target.Distance(Variables._Player) > 470)
                return false;
            Vector2 posAfter = Variables.V2E(Variables._Player.Position, target.Position, 475);
            if (!MenuManager.MiscMenu["noEturret"].Cast<CheckBox>().CurrentValue)
            {
                if (isSafePoint(posAfter).IsSafe)
                {
                    Program.E.Cast(target);
                }
                return true;
            }
            else
            {
                Vector2 pPos = Variables._Player.ServerPosition.To2D();
                Vector2 posAfterE = pPos + (Vector2.Normalize(target.Position.To2D() - pPos) * Program.E.Range);
                if (!Variables.UnderTower((Vector3) Variables.PosAfterE(target)))
                {
                    Console.WriteLine("use gap?");
                    if (isSafePoint(posAfter, true).IsSafe)
                    {
                        Program.E.Cast(target);
                    }
                    return true;
                }
            }
            return false;

        }
    }
}
