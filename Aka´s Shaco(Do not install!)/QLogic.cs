#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AddonTemplate.Utility;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

#endregion

namespace AddonTemplate
{
    internal class QLogic
    {
        public static Vector3 GetQPos(AIHeroClient target, bool serverPos, int distance = 150)
        {
            var enemyPos = serverPos ? target.ServerPosition : target.Position;
            var myPos = serverPos ? ObjectManager.Player.ServerPosition : ObjectManager.Player.Position;

            return enemyPos + Vector3.Normalize(enemyPos - myPos) * distance;
        }

        public static Vector2 GetShortestWayPoint(List<Vector2> waypoints)
        {
            return waypoints.MinOrDefault(x => x.Distance(ObjectManager.Player));
        }
    }
}
