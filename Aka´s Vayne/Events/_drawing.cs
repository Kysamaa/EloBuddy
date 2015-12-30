using System;
using EloBuddy;
using EloBuddy.SDK.Menu.Values;
using SharpDX;
using EloBuddy.SDK.Rendering;

namespace Aka_s_Vayne_reworked.Events
{
    class _drawing
    {
        public static void Drawing()
        {
            if (MenuManager.DrawingMenu["DrawE"].Cast<CheckBox>().CurrentValue &&
                !MenuManager.MiscMenu["fpsdrop"].Cast<CheckBox>().CurrentValue)
            {
                if (!(MenuManager.DrawingMenu["DrawOnlyReady"].Cast<CheckBox>().CurrentValue && !Program.E.IsReady()))
                {
                    Circle.Draw(Color.Red, Program.E.Range, Variables._Player.Position);
                }
            }
            if (MenuManager.DrawingMenu["DrawQ"].Cast<CheckBox>().CurrentValue &&
                !MenuManager.MiscMenu["fpsdrop"].Cast<CheckBox>().CurrentValue)
            {
                if (!(MenuManager.DrawingMenu["DrawOnlyReady"].Cast<CheckBox>().CurrentValue && !Program.Q.IsReady()))
                {
                    Circle.Draw(Color.Red, Program.Q.Range, Variables._Player.Position);
                }
            }
        }
    }
}
