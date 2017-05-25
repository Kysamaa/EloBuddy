﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;
using SharpDX;
using AddonTemplate.Utility;
using Aka_s_Vayne_reworked;

namespace AddonTemplate.Logic
{


    class ELogic
    {

        public static void Condemn1()
        {
            foreach (var target in HeroManager.Enemies.Where(h => h.IsValidTarget(Program.E.Range)))
            {
                if (Program.CondemnMenu["condemnmethod1"].Cast<CheckBox>().CurrentValue)
                {
                    var pushDistance = Program.CondemnMenu["pushDistance"].Cast<Slider>().CurrentValue;
                    var targetPosition = Program.E2.GetPrediction(target).UnitPosition;
                    var pushDirection = (targetPosition - ObjectManager.Player.ServerPosition).Normalized();
                    float checkDistance = pushDistance/40f;
                    for (int i = 0; i < 40; i++)
                    {
                        Vector3 finalPosition = targetPosition + (pushDirection*checkDistance*i);
                        var collFlags = NavMesh.GetCollisionFlags(finalPosition);
                        if (collFlags.HasFlag(CollisionFlags.Wall) || collFlags.HasFlag(CollisionFlags.Building))
                            Program.E.Cast(target);
                    }
                }
            }
        }




        public static
            bool AsunasAllyFountain(Vector3 position)
        {
            float fountainRange = 750;
            var map = (Game.MapId == GameMapId.SummonersRift);
            if (map == (Game.MapId == GameMapId.SummonersRift))
            {
                fountainRange = 1050;
            }
            return
                ObjectManager.Get<GameObject>()
                    .Where(spawnPoint => spawnPoint is Obj_SpawnPoint && spawnPoint.IsAlly)
                    .Any(spawnPoint => Vector2.Distance(position.To2D(), spawnPoint.Position.To2D()) < fountainRange);
        }

        public static void Condemn2()
        {


            foreach (
                var En in
                    HeroManager.Enemies.Where(
                        hero =>
                            hero.IsValidTarget(Program.E.Range) && !hero.HasBuffOfType(BuffType.SpellShield) &&
                            !hero.HasBuffOfType(BuffType.SpellImmunity)))
            {
                if (Program.CondemnMenu["condemnmethod2"].Cast<CheckBox>().CurrentValue)
                {
                    var EPred = Program.E2.GetPrediction(En);
                    int pushDist = Program.CondemnMenu["pushDistance"].Cast<Slider>().CurrentValue;
                    var FinalPosition =
                        EPred.UnitPosition.To2D().Extend(ObjectManager.Player.ServerPosition.To2D(), -pushDist).To3D();

                    for (int i = 1; i < pushDist; i += (int) En.BoundingRadius)
                    {
                        Vector3 finalPosition =
                            EPred.UnitPosition.To2D().Extend(ObjectManager.Player.ServerPosition.To2D(), -i).To3D();
                        var collFlags = NavMesh.GetCollisionFlags(finalPosition);
                        var enemiesCount = ObjectManager.Player.CountEnemiesInRange(1200);
                        if (collFlags.HasFlag(CollisionFlags.Wall) || collFlags.HasFlag(CollisionFlags.Building) ||
                            AsunasAllyFountain(FinalPosition))
                            Program.E.Cast(En);
                    }
                }
            }
        }

   


    public static
            void Condemn3()
        {
        foreach (
            var enemy in
                HeroManager.Enemies.Where(
                    x =>
                        x.IsValidTarget(Program.E.Range) && !x.HasBuffOfType(BuffType.SpellShield) &&
                        !x.HasBuffOfType(BuffType.SpellImmunity) &&
                        IsCondemable(x)))
        {
            Program.E.Cast(enemy);
        }
        }

        public static
            long LastCheck;

        public static bool IsCondemable(AIHeroClient unit, Vector2 pos = new Vector2())
        {
            if (unit.HasBuffOfType(BuffType.SpellImmunity) || unit.HasBuffOfType(BuffType.SpellShield) || LastCheck + 50 > Environment.TickCount || ObjectManager.Player.IsDashing()) return false;
            var prediction = Program.E2.GetPrediction(unit);
            var predictionsList = pos.IsValid() ? new List<Vector3>() { pos.To3D() } : new List<Vector3>
                        {
                            unit.ServerPosition,
                            unit.Position,
                            prediction.CastPosition,
                            prediction.UnitPosition
                        };

            var wallsFound = 0;
            Program.Points = new List<Vector2>();
            foreach (var position in predictionsList)
            {
                for (var i = 0; i < Program.CondemnMenu["pushDistance"].Cast<Slider>().CurrentValue; i += (int)unit.BoundingRadius)
                {
                    var cPos = ObjectManager.Player.Position.Extend(position, ObjectManager.Player.Distance(position) + i).To3D();
                    Program.Points.Add(cPos.To2D());
                    if (NavMesh.GetCollisionFlags(cPos).HasFlag(CollisionFlags.Wall) || NavMesh.GetCollisionFlags(cPos).HasFlag(CollisionFlags.Building))
                    {
                        wallsFound++;
                        break;
                    }
                }
            }
            if ((wallsFound / predictionsList.Count) >= 33 / 100f)
            {
                return true;
            }

            return false;
        }

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
                    var pushDistance = Program.CondemnMenu["pushDistance"].Cast<Slider>().CurrentValue;
                    var targetPosition = Program.E2.GetPrediction(jungleMobs).UnitPosition;
                    var pushDirection = (targetPosition - ObjectManager.Player.ServerPosition).Normalized();
                    float checkDistance = pushDistance/40f;
                    for (int i = 0; i < 40; i++)
                    {
                        var finalPosition = targetPosition + (pushDirection*checkDistance*i);
                        var collFlags = NavMesh.GetCollisionFlags(finalPosition);
                        if (collFlags.HasFlag(CollisionFlags.Wall) || collFlags.HasFlag(CollisionFlags.Building))
                        {
                            Program.E.Cast(jungleMobs);
                        }
                    }

                }
            }
        }

        public static int CountHerosInRange(AIHeroClient target, bool checkteam, float range = 1200f)
        {
            var objListTeam =
                ObjectManager.Get<AIHeroClient>()
                    .Where(
                        x => x.IsValidTarget(range, false));

            return objListTeam.Count(hero => checkteam ? hero.Team != target.Team : hero.Team == target.Team);
        }

        public static Vector2 GetFirstNonWallPos(Vector2 startPos, Vector2 endPos)
        {
            int distance = 0;
            for (int i = 0; i < Program.CondemnMenu["pushDistance"].Cast<Slider>().CurrentValue; i += 20)
            {
                var cell = startPos.Extend(endPos, endPos.Distance(startPos) + i);
                if (NavMesh.GetCollisionFlags(cell).HasFlag(CollisionFlags.Wall) ||
                            NavMesh.GetCollisionFlags(cell).HasFlag(CollisionFlags.Building))
                {
                    distance = i - 20;
                }
            }
            return startPos.Extend(endPos, distance + endPos.Distance(startPos));
        }

        public static List<Vector3> GetRotatedFlashPositions()
        {
            const int currentStep = 30;
            var direction = ObjectManager.Player.Direction.To2D().Perpendicular();

            var list = new List<Vector3>();
            for (var i = -90; i <= 90; i += currentStep)
            {
                var angleRad = Geometry.DegreeToRadian(i);
                var rotatedPosition = ObjectManager.Player.Position.To2D() + (425f * direction.Rotated(angleRad));
                list.Add(rotatedPosition.To3D());
            }
            return list;
        }

        public static void LoadFlash()
        {
            var testSlot = ObjectManager.Player.GetSpellSlot("summonerflash");
            if (testSlot != SpellSlot.Unknown)
            {
                Console.WriteLine("Flash Slot: {0}", testSlot);
                Events.FlashSlot = testSlot;
            }
            else
            {
                Console.WriteLine("Error loading Flash! Not found!");
            }
        }

        public static AIHeroClient CondemnCheck(Vector3 fromPosition)
        {
            var HeroList = HeroManager.Enemies.Where(
                                    h =>
                                        h.IsValidTarget(Program.E.Range) &&
                                        !h.HasBuffOfType(BuffType.SpellShield) &&
                                        !h.HasBuffOfType(BuffType.SpellImmunity));
            foreach (var Hero in HeroList)
            {
                var ePred = Program.E2.GetPrediction(Hero);
                int pushDist = Program.CondemnMenu["pushDistance"].Cast<Slider>().CurrentValue;
                for (int i = 0; i < pushDist; i += (int)Hero.BoundingRadius)
                {
                    Vector3 loc3 = ePred.UnitPosition.To2D().Extend(fromPosition.To2D(), -i).To3D();
                    if (loc3.IsWall())
                    {
                        return Hero;
                    }
                }
            }
            return null;
        }

    }
}
