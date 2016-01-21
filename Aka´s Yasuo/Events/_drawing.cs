using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace AkaYasuo.Events
{
    class _drawing
    {
        public static void Drawings(EventArgs args)
        {
            if (Variables._Player.IsDead) { return; }
            if (MenuManager.DrawingMenu["DrawQ"].Cast<CheckBox>().CurrentValue && Program.Q.IsReady())
            {
                Drawing.DrawCircle(Variables._Player.Position, Program.Q.Range, System.Drawing.Color.Green);
            }
            if (MenuManager.DrawingMenu["DrawQ3"].Cast<CheckBox>().CurrentValue && Program.Q.IsReady())
            {
                Drawing.DrawCircle(Variables._Player.Position, 1100, System.Drawing.Color.Green);
            }
            if (MenuManager.DrawingMenu["DrawE"].Cast<CheckBox>().CurrentValue && Program.E.IsReady())
            {
                Drawing.DrawCircle(Variables._Player.Position, Program.E.Range, System.Drawing.Color.Green);
            }
            if (MenuManager.DrawingMenu["DrawR"].Cast<CheckBox>().CurrentValue && Program.R.IsReady())
            {
                Drawing.DrawCircle(Variables._Player.Position, Program.R.Range, System.Drawing.Color.Green);
            }
            if (MenuManager.DrawingMenu["DrawQ"].Cast<CheckBox>().CurrentValue && !Program.Q.IsReady())
            {
                Drawing.DrawCircle(Variables._Player.Position, Program.Q.Range, System.Drawing.Color.Red);
            }
            if (MenuManager.DrawingMenu["DrawQ3"].Cast<CheckBox>().CurrentValue && !Program.Q.IsReady())
            {
                Drawing.DrawCircle(Variables._Player.Position, 1100, System.Drawing.Color.Red);
            }
            if (MenuManager.DrawingMenu["DrawE"].Cast<CheckBox>().CurrentValue && !Program.E.IsReady())
            {
                Drawing.DrawCircle(Variables._Player.Position, Program.E.Range, System.Drawing.Color.Red);
            }
            if (MenuManager.DrawingMenu["DrawR"].Cast<CheckBox>().CurrentValue && !Program.R.IsReady())
            {
                Drawing.DrawCircle(Variables._Player.Position, Program.R.Range, System.Drawing.Color.Red);
            }
            if (MenuManager.DrawingMenu["DrawSpots"].Cast<CheckBox>().CurrentValue)
            {
                Drawing.DrawCircle(Yasuo.spotA.To3D(), 150, System.Drawing.Color.Red);
                Drawing.DrawCircle(Yasuo.spotB.To3D(), 150, System.Drawing.Color.Red);
                Drawing.DrawCircle(Yasuo.spotC.To3D(), 150, System.Drawing.Color.Red);
                Drawing.DrawCircle(Yasuo.spotD.To3D(), 150, System.Drawing.Color.Red);
                Drawing.DrawCircle(Yasuo.spotE.To3D(), 150, System.Drawing.Color.Red);
                Drawing.DrawCircle(Yasuo.spotF.To3D(), 150, System.Drawing.Color.Red);
                Drawing.DrawCircle(Yasuo.spotG.To3D(), 150, System.Drawing.Color.Red);
                Drawing.DrawCircle(Yasuo.spotH.To3D(), 150, System.Drawing.Color.Red);
                Drawing.DrawCircle(Yasuo.spotI.To3D(), 150, System.Drawing.Color.Red);
                Drawing.DrawCircle(Yasuo.spotJ.To3D(), 150, System.Drawing.Color.Red);
                Drawing.DrawCircle(Yasuo.spotK.To3D(), 150, System.Drawing.Color.Red);
                Drawing.DrawCircle(Yasuo.spotL.To3D(), 150, System.Drawing.Color.Red);
                Drawing.DrawCircle(Yasuo.spotM.To3D(), 150, System.Drawing.Color.Red);
                Drawing.DrawCircle(Yasuo.spotN.To3D(), 150, System.Drawing.Color.Red);
                Drawing.DrawCircle(Yasuo.spotK.To3D(), 150, System.Drawing.Color.Red);
                Drawing.DrawCircle(Yasuo.spotO.To3D(), 150, System.Drawing.Color.Red);
            }
        }
    }
}
