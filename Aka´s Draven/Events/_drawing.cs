using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;

namespace AkaDraven.Events
{
    class _drawing
    {
        public static void Drawings(EventArgs args)
        {
            var drawE = MenuManager.DrawingMenu["DrawE"].Cast<CheckBox>().CurrentValue;
            var drawAxeLocation = MenuManager.DrawingMenu["DrawAxe"].Cast<CheckBox>().CurrentValue;
            var drawAxeRange = MenuManager.DrawingMenu["DrawAxeRange"].Cast<CheckBox>().CurrentValue;

            if (drawE)
            {
                Circle.Draw(Color.Aqua,
                    Program.E.Range, ObjectManager.Player.Position);
            }

            if (drawAxeLocation)
            {
                var bestAxe =
                    Variables.QReticles.Where(
                        x =>
                        x.Object.Position.Distance(Game.CursorPos) < MenuManager.AxeMenu["Qrange"].Cast<Slider>().CurrentValue)
                        .OrderBy(x => x.Object.Position.Distance(Variables._Player.ServerPosition))
                        .ThenBy(x => x.Object.Position.Distance(Game.CursorPos))
                        .FirstOrDefault();

                if (bestAxe != null)
                {
                    Circle.Draw(Color.LimeGreen, 120, bestAxe.Position);
                }

                foreach (var axe in
                    Variables.QReticles.Where(x => x.Object.NetworkId != (bestAxe == null ? 0 : bestAxe.Object.NetworkId)))
                {
                    Circle.Draw(Color.GreenYellow, 120, axe.Position);
                }
            }

            if (drawAxeRange)
            {
                Circle.Draw(Color.DodgerBlue,
                    MenuManager.AxeMenu["Qrange"].Cast<Slider>().CurrentValue, Game.CursorPos);
            }
        }
    }
}
