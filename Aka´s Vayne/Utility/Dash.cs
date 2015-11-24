using System;
using System.Collections.Generic;
using System.Linq;
using SharpDX;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
//using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;

namespace AddonTemplate.Utility
{
    public static class Dash
    {
        private static readonly Dictionary<int, DashItem> DetectedDashes = new Dictionary<int, DashItem>();

        static Dash()
        {
            AIHeroClient.OnNewPath += ObjAiHeroOnOnNewPath;
        }

        private static void ObjAiHeroOnOnNewPath(Obj_AI_Base sender, GameObjectNewPathEventArgs args)
        {
            if (sender.IsValid<AIHeroClient>())
            {
                if (!DetectedDashes.ContainsKey(sender.NetworkId))
                {
                    DetectedDashes.Add(sender.NetworkId, new DashItem());
                }

                if (args.IsDash)
                {
                    var path = new List<Vector2> { sender.ServerPosition.To2D2() };
                    path.AddRange(args.Path.ToList().To2D2());

                    DetectedDashes[sender.NetworkId].StartTick = Utils.TickCount;
                    DetectedDashes[sender.NetworkId].Speed = args.Speed;
                    DetectedDashes[sender.NetworkId].StartPos = sender.ServerPosition.To2D2();
                    DetectedDashes[sender.NetworkId].Unit = sender;
                    DetectedDashes[sender.NetworkId].Path = path;
                    DetectedDashes[sender.NetworkId].EndPos = DetectedDashes[sender.NetworkId].Path.Last();
                    DetectedDashes[sender.NetworkId].EndTick = DetectedDashes[sender.NetworkId].StartTick +
                                                           (int)
                                                               (1000 *
                                                                (DetectedDashes[sender.NetworkId].EndPos.Distance7(
                                                                    DetectedDashes[sender.NetworkId].StartPos) / DetectedDashes[sender.NetworkId].Speed));
                    DetectedDashes[sender.NetworkId].Duration = DetectedDashes[sender.NetworkId].EndTick - DetectedDashes[sender.NetworkId].StartTick;

                    CustomEvents.Unit.TriggerOnDash(DetectedDashes[sender.NetworkId].Unit, DetectedDashes[sender.NetworkId]);
                }
                else
                {
                    DetectedDashes[sender.NetworkId].EndTick = 0;
                }
            }
        }

        /// <summary>
        /// Returns true if the unit is dashing.
        /// </summary>
        public static bool IsDashing2(this Obj_AI_Base unit)
        {
            if (DetectedDashes.ContainsKey(unit.NetworkId) && unit.Path.Length != 0)
            {
                return DetectedDashes[unit.NetworkId].EndTick != 0;
            }
            return false;
        }

        /// <summary>
        /// Gets the speed of the dashing unit if it is dashing.
        /// </summary>
        public static DashItem GetDashInfo(this Obj_AI_Base unit)
        {
            return DetectedDashes.ContainsKey(unit.NetworkId) ? DetectedDashes[unit.NetworkId] : new DashItem();
        }

        public class DashItem
        {
            public int Duration;
            public Vector2 EndPos;
            public int EndTick;
            public List<Vector2> Path;
            public float Speed;
            public Vector2 StartPos;
            public int StartTick;
            public Obj_AI_Base Unit;
            public bool IsBlink;
        }
    }
}