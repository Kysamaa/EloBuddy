
using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Rendering;
using SharpDX;

namespace AddonTemplate
{
    public static class Events
    {
        public static void OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!Config.Modes.MiscMenu.dR) return;
            if (sender.IsAlly) return;
            if (!sender.IsEnemy) return;


            if (DangerDB.TargetedList.Contains(args.SData.Name))
            {
                if (args.Target.IsMe)
                    SpellManager.R.Cast(sender);
            }

            if (DangerDB.CircleSkills.Contains(args.SData.Name))
            {
                if (ObjectManager.Player.Distance(args.End) < args.SData.LineWidth)
                    SpellManager.R.Cast(sender);
            }

            if (DangerDB.Skillshots.Contains(args.SData.Name))
            {
                if (new Geometry.Polygon.Rectangle(args.Start, args.End, args.SData.LineWidth).IsInside(ObjectManager.Player))
                {
                    SpellManager.R.Cast(sender);
                }
            }
        }
    }
}
