using System;
using System.Collections.Generic;
using System.Linq;
using Aka_s_Vayne_reworked.Functions;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

namespace Aka_s_Vayne_reworked.Logic
{
    public static class NewELogic
    {
        public static void Execute()
        {
            if (!Program.E.IsReady())
            {
                return;
            }

            var CondemnTarget = GetCondemnTarget(ObjectManager.Player.ServerPosition);
            if (CondemnTarget.IsValidTarget())
            {
                // var AAForE = MenuExtensions.GetItemValue<Slider>("dz191.vhr.misc.condemn.noeaa").Value;

                // if (CondemnTarget.Health / ObjectManager.Player.GetAutoAttackDamage(CondemnTarget, true) < AAForE)
                // {
                //     return;
                // }

                Program.E.Cast(CondemnTarget);
            }
        }

        public static void Spellbook_OnCastSpell(Spellbook sender, SpellbookCastSpellEventArgs args)
        {
            if (sender != null && sender.Owner != null && sender.Owner.IsMe && args.Slot == SpellSlot.E && (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo)))
            {
                if (!(args.Target is AIHeroClient))
                {
                    args.Process = false;
                    return;
                }

                if (GetCondemnTarget(ObjectManager.Player.ServerPosition).IsValidTarget())
                {
                    if (!Shine.GetTarget(ObjectManager.Player.ServerPosition).IsValidTarget())
                    {
                        args.Process = false;
                    }
                }
            }
        }

        public static Obj_AI_Base GetCondemnTarget(Vector3 fromPosition)
        {
            switch (MenuManager.CondemnMenu["Condemnmode"].Cast<Slider>().CurrentValue)
            {
                case 1:
                    //VH Revolution
                    return Shine.GetTarget(fromPosition);
                case 2:
                    //VH Reborn
                    return VHReborn.GetTarget(fromPosition);
                case 3:
                    //Marksman / Gosu
                    return Marksman.GetTarget(fromPosition);
                case 4:
                    //Shine#
                    return VHRevolution.GetTarget(fromPosition);
            }
            return null;
        }
    }

    internal class Marksman
    {
        public static AIHeroClient GetTarget(Vector3 fromPosition)
        {
            foreach (var target in EntityManager.Heroes.Enemies.Where(h => h.IsValidTarget(Program.E.Range)))
            {
                var pushDistance = MenuManager.CondemnMenu["pushDistance"].Cast<Slider>().CurrentValue;
                var targetPosition = Program.E2.GetPrediction(target).UnitPosition;
                var finalPosition = targetPosition.Extend(fromPosition, -pushDistance);
                var finalPosition2 = targetPosition.Extend(fromPosition, -(pushDistance / 2f));
                var collFlags = NavMesh.GetCollisionFlags(finalPosition);
                var collFlags2 = NavMesh.GetCollisionFlags(finalPosition2);
                if (collFlags.HasFlag(CollisionFlags.Wall) || collFlags.HasFlag(CollisionFlags.Building) || collFlags2.HasFlag(CollisionFlags.Wall) || collFlags2.HasFlag(CollisionFlags.Building))
                {
                    if (MenuManager.CondemnMenu["UseEc"].Cast<CheckBox>().CurrentValue &&
                        TargetSelector.GetTarget((int)Variables._Player.GetAutoAttackRange(),
    DamageType.Physical) != null &&
                        !target.NetworkId.Equals(TargetSelector.GetTarget((int)Variables._Player.GetAutoAttackRange(),
    DamageType.Physical).NetworkId))
                    {
                        return null;
                    }

                    if (target.Health + 10 <=
                        ObjectManager.Player.GetAutoAttackDamage(target) *
                        MenuManager.CondemnMenu["noeaa"].Cast<Slider>().CurrentValue)
                    {
                        return null;
                    }

                    return target;
                }
            }
            return null;
        }
    }

    class Shine
    {
        public static Obj_AI_Base GetTarget(Vector3 fromPosition)
        {
            foreach (var target in EntityManager.Heroes.Enemies.Where(h => h.IsValidTarget(Program.E.Range)))
            {
                var pushDistance = MenuManager.CondemnMenu["pushDistance"].Cast<Slider>().CurrentValue;
                var targetPosition = Program.E2.GetPrediction(target).UnitPosition;
                var pushDirection = (targetPosition - ObjectManager.Player.ServerPosition).Normalized();
                float checkDistance = pushDistance / 40f;
                for (int i = 0; i < 40; i++)
                {
                    Vector3 finalPosition = targetPosition + (pushDirection * checkDistance * i);
                    var collFlags = NavMesh.GetCollisionFlags(finalPosition);
                    if (collFlags.HasFlag(CollisionFlags.Wall) || collFlags.HasFlag(CollisionFlags.Building)) //not sure about building, I think its turrets, nexus etc
                    {
                        if (MenuManager.CondemnMenu["UseEc"].Cast<CheckBox>().CurrentValue && TargetSelector.GetTarget((int)Variables._Player.GetAutoAttackRange(),DamageType.Physical) != null &&
                                        !target.NetworkId.Equals(TargetSelector.GetTarget((int)Variables._Player.GetAutoAttackRange(),DamageType.Physical).NetworkId))
                        {
                            return null;
                        }

                        if (target.Health + 10 <=
                                        ObjectManager.Player.GetAutoAttackDamage(target) *
                                        MenuManager.CondemnMenu["noeaa"].Cast<Slider>().CurrentValue)
                        {
                            return null;
                        }

                        return target;
                    }
                }
            }

            return null;
        }
    }

    class VHReborn
    {
        public static AIHeroClient GetTarget(Vector3 fromPosition)
        {
            if (other.UnderEnemyTower((Vector2)Variables._Player.ServerPosition))
            {
                return null;
            }

            var pushDistance = MenuManager.CondemnMenu["pushDistance"].Cast<Slider>().CurrentValue;

            foreach (var target in EntityManager.Heroes.Enemies.Where(h => h.IsValidTarget(Program.E.Range) && !h.HasBuffOfType(BuffType.SpellShield) && !h.HasBuffOfType(BuffType.SpellImmunity)))
            {
                var targetPosition = target.ServerPosition;
                var finalPosition = targetPosition.Extend(fromPosition, -pushDistance);
                var numberOfChecks = (float)Math.Ceiling(pushDistance / 30f);


                if (MenuManager.CondemnMenu["UseEc"].Cast<CheckBox>().CurrentValue && TargetSelector.GetTarget((int)Variables._Player.GetAutoAttackRange(),DamageType.Physical) != null &&
                            !target.NetworkId.Equals(TargetSelector.GetTarget((int)Variables._Player.GetAutoAttackRange(),DamageType.Physical).NetworkId))
                {
                    continue;
                }

                for (var i = 1; i <= 30; i++)
                {
                    var v3 = (targetPosition - fromPosition).Normalized();
                    var extendedPosition = targetPosition + v3 * (numberOfChecks * i);
                    //var underTurret = MenuExtensions.GetItemValue<bool>("dz191.vhr.misc.condemn.condemnturret") && (Helpers.UnderAllyTurret_Ex(finalPosition) || Helpers.IsFountain(finalPosition));
                    var collFlags = NavMesh.GetCollisionFlags(extendedPosition);
                    if ((collFlags.HasFlag(CollisionFlags.Wall) || collFlags.HasFlag(CollisionFlags.Building)) && (target.Path.Count() < 2) && !target.IsDashing())
                    {

                        if (target.Health + 10 <=
                            ObjectManager.Player.GetAutoAttackDamage(target) *
                            MenuManager.CondemnMenu["noeaa"].Cast<Slider>().CurrentValue)
                        {
                            return null;
                        }

                        return target;
                    }
                }
            }
            return null;
        }
    }

    class VHRevolution_Old
    {
        public static Obj_AI_Base GetTarget(Vector3 fromPosition)
        {
            var HeroList = EntityManager.Heroes.Enemies.Where(
                                    h =>
                                        h.IsValidTarget(Program.E.Range) &&
                                        !h.HasBuffOfType(BuffType.SpellShield) &&
                                        !h.HasBuffOfType(BuffType.SpellImmunity));
            //dz191.vhr.misc.condemn.rev.accuracy
            //dz191.vhr.misc.condemn.rev.nextprediction
            var MinChecksPercent = MenuManager.CondemnMenu["condemnPercent"].Cast<Slider>().CurrentValue;
            var PushDistance = MenuManager.CondemnMenu["pushDistance"].Cast<Slider>().CurrentValue;

            if (other.UnderEnemyTower((Vector2)Variables._Player.ServerPosition))
            {
                return null;
            }

            foreach (var Hero in HeroList)
            {
                var prediction = Program.E2.GetPrediction(Hero);

                if (MenuManager.CondemnMenu["UseEc"].Cast<CheckBox>().CurrentValue &&
                    Hero.NetworkId != TargetSelector.GetTarget((int)Variables._Player.GetAutoAttackRange(),DamageType.Physical).NetworkId)
                {
                    continue;
                }

                if (Hero.Health + 10 <=
                    ObjectManager.Player.GetAutoAttackDamage(Hero) *
                    MenuManager.CondemnMenu["noeaa"].Cast<Slider>().CurrentValue)
                {
                    continue;
                }

                var PredictionsList = new List<Vector3>
                {
                    Hero.ServerPosition,
                    Hero.Position,
                    prediction.CastPosition,
                    prediction.UnitPosition
                };

                if (Hero.IsDashing())
                {
                    PredictionsList.Add(Hero.GetDashInfo().EndPos);
                }

                var wallsFound = 0;
                foreach (var position in PredictionsList)
                {
                    for (var i = 0; i < PushDistance; i += (int)Hero.BoundingRadius)
                    {
                        var cPos = position.Extend(fromPosition, -i);
                        var collFlags = NavMesh.GetCollisionFlags(cPos);
                        if (collFlags.HasFlag(CollisionFlags.Wall) || collFlags.HasFlag(CollisionFlags.Building))
                        {
                            wallsFound++;
                            break;
                        }
                    }
                }

                if ((wallsFound / PredictionsList.Count) >= MinChecksPercent / 100f)
                {
                    return Hero;
                }
            }
            return null;
        }
    }

    class VHRevolution
    {
        public static Obj_AI_Base GetTarget(Vector3 fromPosition)
        {
            var HeroList = EntityManager.Heroes.Enemies.Where(
                                    h =>
                                        h.IsValidTarget(Program.E.Range) &&
                                        !h.HasBuffOfType(BuffType.SpellShield) &&
                                        !h.HasBuffOfType(BuffType.SpellImmunity));
            //dz191.vhr.misc.condemn.rev.accuracy
            //dz191.vhr.misc.condemn.rev.nextprediction
            var MinChecksPercent = MenuManager.CondemnMenu["condemnPercent"].Cast<Slider>().CurrentValue;
            var PushDistance = MenuManager.CondemnMenu["pushDistance"].Cast<Slider>().CurrentValue;

            if (PushDistance >= 410)
            {
                var PushEx = PushDistance;
                PushDistance -= (10 + (PushEx - 410) / 2);
            }

            if (other.UnderEnemyTower((Vector2)Variables._Player.ServerPosition))
            {
                return null;
            }

            foreach (var Hero in HeroList)
            {
                if (MenuManager.CondemnMenu["UseEc"].Cast<CheckBox>().CurrentValue &&
                    Hero.NetworkId != TargetSelector.GetTarget((int)Variables._Player.GetAutoAttackRange(),DamageType.Physical).NetworkId)
                {
                    continue;
                }

                if (Hero.Health + 10 <=
                    ObjectManager.Player.GetAutoAttackDamage(Hero) *
                    MenuManager.CondemnMenu["noeaa"].Cast<Slider>().CurrentValue)
                {
                    continue;
                }


                var targetPosition = Program.E2.GetPrediction(Hero).UnitPosition;
                var finalPosition = targetPosition.Extend(ObjectManager.Player.ServerPosition, -PushDistance);
                var finalPosition_ex = Hero.ServerPosition.Extend(ObjectManager.Player.ServerPosition, -PushDistance);

                var condemnRectangle = new VHRPolygon(VHRPolygon.Rectangle(targetPosition.To2D(), finalPosition, Hero.BoundingRadius));
                var condemnRectangle_ex = new VHRPolygon(VHRPolygon.Rectangle(Hero.ServerPosition.To2D(), finalPosition_ex, Hero.BoundingRadius));

                if (IsBothNearWall(Hero))
                {
                    return null;
                }

                if (condemnRectangle.Points.Count(point => NavMesh.GetCollisionFlags(point.X, point.Y).HasFlag(CollisionFlags.Wall)) >= condemnRectangle.Points.Count() * (MinChecksPercent / 100f)
                    && condemnRectangle_ex.Points.Count(point => NavMesh.GetCollisionFlags(point.X, point.Y).HasFlag(CollisionFlags.Wall)) >= condemnRectangle_ex.Points.Count() * (MinChecksPercent / 100f))
                {
                    return Hero;
                }
            }
            return null;
        }

        private static bool IsBothNearWall(Obj_AI_Base target)
        {
            var positions =
                GetWallQPositions(target, 110).ToList().OrderBy(pos => pos.Distance(target.ServerPosition, true));
            var positions_ex =
            GetWallQPositions(ObjectManager.Player, 110).ToList().OrderBy(pos => pos.Distance(ObjectManager.Player.ServerPosition, true));

            if (positions.Any(p => NavMesh.GetCollisionFlags(p).HasFlag(CollisionFlags.Wall) || NavMesh.GetCollisionFlags(p).HasFlag(CollisionFlags.Building)) && positions_ex.Any(p => NavMesh.GetCollisionFlags(p).HasFlag(CollisionFlags.Wall) || NavMesh.GetCollisionFlags(p).HasFlag(CollisionFlags.Building)))
            {
                return true;
            }
            return false;
        }

        private static Vector3[] GetWallQPositions(Obj_AI_Base player, float Range)
        {
            Vector3[] vList =
            {
                (player.ServerPosition.To2D() + Range * player.Direction.To2D()).To3D(),
                (player.ServerPosition.To2D() - Range * player.Direction.To2D()).To3D()

            };

            return vList;
        }
    }
    class Jungle
    {
        public static void JungleCondemn()
        {
            foreach (
                var jungleMobs in
                    ObjectManager.Get<Obj_AI_Minion>()
                        .Where(
                            o =>
                                o.IsValidTarget(Program.E.Range) && o.Team == GameObjectTeam.Neutral && o.IsVisible &&
                                !o.IsDead))
            {
                if (jungleMobs.BaseSkinName == "SRU_Razorbeak" || jungleMobs.BaseSkinName == "SRU_Red" ||
                    jungleMobs.BaseSkinName == "SRU_Blue" || jungleMobs.BaseSkinName == "SRU_Dragon" ||
                    jungleMobs.BaseSkinName == "SRU_Krug" || jungleMobs.BaseSkinName == "SRU_Gromp" ||
                    jungleMobs.BaseSkinName == "Sru_Crab")
                {
                    var pushDistance = MenuManager.CondemnMenu["pushDistance"].Cast<Slider>().CurrentValue;
                    var targetPosition = Program.E2.GetPrediction(jungleMobs).UnitPosition;
                    var pushDirection = (targetPosition - Variables._Player.ServerPosition).Normalized();
                    float checkDistance = pushDistance / 40f;
                    for (int i = 0; i < 40; i++)
                    {
                        var finalPosition = targetPosition + (pushDirection * checkDistance * i);
                        var collFlags = NavMesh.GetCollisionFlags(finalPosition);
                        if (collFlags.HasFlag(CollisionFlags.Wall) || collFlags.HasFlag(CollisionFlags.Building))
                        {
                            Program.E.Cast(jungleMobs);
                        }
                    }

                }
            }
        }
    }
}


