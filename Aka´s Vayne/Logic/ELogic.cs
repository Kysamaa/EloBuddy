using System;
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
using Settings = AddonTemplate.Config.Modes.Condemn;

namespace AddonTemplate.Logic
{
    class ELogic
    {
        public static void Condemn1()
        {
            foreach (var target in HeroManager.Enemies.Where(h => h.IsValidTarget(SpellManager.E.Range)))
            {
                if (Settings.Condemn1)
                {
                    var pushDistance = Settings.Condemndistance;
                    var targetPosition = SpellManager.E2.GetPrediction(target).UnitPosition;
                    var pushDirection = (targetPosition - ObjectManager.Player.ServerPosition).Normalized();
                    float checkDistance = pushDistance/40f;
                    for (int i = 0; i < 40; i++)
                    {
                        Vector3 finalPosition = targetPosition + (pushDirection*checkDistance*i);
                        var collFlags = NavMesh.GetCollisionFlags(finalPosition);
                        if (collFlags.HasFlag(CollisionFlags.Wall) || collFlags.HasFlag(CollisionFlags.Building))
                            SpellManager.E.Cast(target);
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
                            hero.IsValidTarget(SpellManager.E.Range) && !hero.HasBuffOfType(BuffType.SpellShield) &&
                            !hero.HasBuffOfType(BuffType.SpellImmunity)))
            {
                if (Settings.Condemn2)
                {
                    var EPred = SpellManager.E2.GetPrediction(En);
                    int pushDist = Settings.Condemndistance;
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
                            SpellManager.E.Cast(En);
                    }
                }
            }
        }

        public static void Condemn4(Obj_AI_Base hero)
        {

            if (!hero.IsValidTarget(550) || hero.HasBuffOfType(BuffType.SpellShield) ||
    hero.HasBuffOfType(BuffType.SpellImmunity) || hero.IsDashing())
                return;

            var pP = Heroes.Player.ServerPosition;
            var p = hero.ServerPosition;
            var pD = Settings.Condemndistance;

            if ((!p.Extend(pP, -pD).IsWall() && !p.Extend(pP, -pD/2f).IsWall() && !p.Extend(pP, -pD/3f).IsWall()))
                return;
            var angle = 0.20*50;
            const float travelDistance = 0.5f;
            var alpha = new Vector2((float) (p.X + travelDistance*Math.Cos(Math.PI/180*angle)),
                (float) (p.X + travelDistance*Math.Sin(Math.PI/180*angle)));
            var beta = new Vector2((float) (p.X - travelDistance*Math.Cos(Math.PI/180*angle)),
                (float) (p.X - travelDistance*Math.Sin(Math.PI/180*angle)));

            for (var i = 15; i < pD; i += 100)
            {
                if (pP.To2D().Extend(alpha,
                    i)
                    .To3D().IsWall() && pP.To2D().Extend(beta, i).To3D().IsWall())
                {
                    SpellManager.E.Cast(hero);
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
                        x.IsValidTarget(SpellManager.E.Range) && !x.HasBuffOfType(BuffType.SpellShield) &&
                        !x.HasBuffOfType(BuffType.SpellImmunity) &&
                        IsCondemable(x)))
        {
            SpellManager.E.Cast(enemy);
        }
        }

        public static
            long LastCheck;

        public static bool IsCondemable(AIHeroClient unit, Vector2 pos = new Vector2())
        {
            if (unit.HasBuffOfType(BuffType.SpellImmunity) || unit.HasBuffOfType(BuffType.SpellShield) || LastCheck + 50 > Environment.TickCount || ObjectManager.Player.IsDashing()) return false;
            var prediction = SpellManager.E2.GetPrediction(unit);
            var predictionsList = pos.IsValid() ? new List<Vector3>() { pos.To3D() } : new List<Vector3>
                        {
                            unit.ServerPosition,
                            unit.Position,
                            prediction.CastPosition,
                            prediction.UnitPosition
                        };

            var wallsFound = 0;
            SpellManager.Points = new List<Vector2>();
            foreach (var position in predictionsList)
            {
                for (var i = 0; i < Settings.Condemndistance; i += (int)unit.BoundingRadius)
                {
                    var cPos = ObjectManager.Player.Position.Extend(position, ObjectManager.Player.Distance(position) + i).To3D();
                    SpellManager.Points.Add(cPos.To2D());
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
                                o.IsValidTarget(SpellManager.E.Range) && o.Team == GameObjectTeam.Neutral && o.IsVisible &&
                                !o.IsDead))
            {
                if (jungleMobs.BaseSkinName == "SRU_Razorbeak" || jungleMobs.BaseSkinName == "SRU_Red" ||
                    jungleMobs.BaseSkinName == "SRU_Blue" || jungleMobs.BaseSkinName == "SRU_Dragon" ||
                    jungleMobs.BaseSkinName == "SRU_Krug" || jungleMobs.BaseSkinName == "SRU_Gromp" ||
                    jungleMobs.BaseSkinName == "Sru_Crab")
                {
                    var pushDistance = Settings.Condemndistance;
                    var targetPosition = SpellManager.E2.GetPrediction(jungleMobs).UnitPosition;
                    var pushDirection = (targetPosition - ObjectManager.Player.ServerPosition).Normalized();
                    float checkDistance = pushDistance/40f;
                    for (int i = 0; i < 40; i++)
                    {
                        var finalPosition = targetPosition + (pushDirection*checkDistance*i);
                        var collFlags = NavMesh.GetCollisionFlags(finalPosition);
                        if (collFlags.HasFlag(CollisionFlags.Wall) || collFlags.HasFlag(CollisionFlags.Building))
                        {
                            SpellManager.E.Cast(jungleMobs);
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
    }
}
