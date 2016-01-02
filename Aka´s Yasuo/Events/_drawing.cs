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
                Drawing.DrawCircle(Yasuo.spot1.To3D(), 150, System.Drawing.Color.Red);
                Drawing.DrawCircle(Yasuo.spot2.To3D(), 150, System.Drawing.Color.Red);
                Drawing.DrawCircle(Yasuo.spot3.To3D(), 150, System.Drawing.Color.Red);
                Drawing.DrawCircle(Yasuo.spot4.To3D(), 150, System.Drawing.Color.Red);
                Drawing.DrawCircle(Yasuo.spot5.To3D(), 150, System.Drawing.Color.Red);
                Drawing.DrawCircle(Yasuo.spot6.To3D(), 150, System.Drawing.Color.Red);
                Drawing.DrawCircle(Yasuo.spot7.To3D(), 150, System.Drawing.Color.Red);
                Drawing.DrawCircle(Yasuo.spot8.To3D(), 150, System.Drawing.Color.Red);
                Drawing.DrawCircle(Yasuo.spot9.To3D(), 150, System.Drawing.Color.Red);
                Drawing.DrawCircle(Yasuo.spot10.To3D(), 150, System.Drawing.Color.Red);
                Drawing.DrawCircle(Yasuo.spot11.To3D(), 150, System.Drawing.Color.Red);
                Drawing.DrawCircle(Yasuo.spot12.To3D(), 150, System.Drawing.Color.Red);
                Drawing.DrawCircle(Yasuo.spot13.To3D(), 150, System.Drawing.Color.Red);
                Drawing.DrawCircle(Yasuo.spot14.To3D(), 150, System.Drawing.Color.Red);
                Drawing.DrawCircle(Yasuo.spot15.To3D(), 150, System.Drawing.Color.Red);
                Drawing.DrawCircle(Yasuo.spot16.To3D(), 150, System.Drawing.Color.Red);
            }
        }
    }
}
