using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

// Using the config like this makes your life easier, trust me
using Settings = AddonTemplate.Config.Modes.Combo;

namespace AddonTemplate.Modes
{
    public sealed class Combo : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            // Only execute this mode when the orbwalker is on combo mode
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
        }

        public override void Execute()
        {
            if (UseQ && this.Q.IsReady())
            {
                mySpellcast.DashToCursor(
                    Target, this.Q, false, Math.Min(Vector3.Distance(Player.ServerPosition, target.ServerPosition), this.Q.Range + target.BoundingRadius),
                    this.E.IsReady() ? this.E.Range : Orbwalking.GetAttackRange(Player));
            }

                
            

        
    



