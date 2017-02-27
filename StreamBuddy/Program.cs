#region

using System;
using System.Linq;
using System.Runtime.InteropServices;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using Color = System.Drawing.Color;
using SharpDX;

#endregion

namespace FakeClicks
{
    class FakeClick
    {
        private static float lastclick;
        private static readonly Random r = new Random();

        private static Menu Menu;

        private static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }

        private static bool Enabled
        {
            get { return Menu["Enable"].Cast<CheckBox>().CurrentValue; }
        }

        private static bool Stream
        {
            get { return Menu["Stream"].Cast<KeyBind>().CurrentValue; }
        }

        private static int Random
        {
            get { return Menu["Random"].Cast<Slider>().CurrentValue; }
        }

        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += OnLoadComplete;
        }

        static void OnLoadComplete(EventArgs args)
        {
            Orbwalker.OnPostAttack += AfterAttack;
            Player.OnIssueOrder += OnIssueOrder;
            Game.OnUpdate += GameOnUpdate;

            Menu = MainMenu.AddMenu("StreamBuddy", "streambufdydyd");
            Menu.Add("Enable", new CheckBox("Enable"));
            Menu.Add("Random", new Slider("Random Modifier", 100, 0, 1000));
            Menu.AddLabel("Note: The menu will be disabled too!");
            Menu.Add("Stream", new KeyBind("Stream", false, KeyBind.BindTypes.PressToggle, 'H'));
        }

        private static void GameOnUpdate(EventArgs args)
        {
            if (Stream)
            {
                Hacks.DisableDrawings = true;
                Hacks.IngameChat = false;
                Hacks.RenderWatermark = false;
            }
            if (!Stream)
            {
                Hacks.DisableDrawings = false;
                Hacks.IngameChat = true;
                Hacks.RenderWatermark = true;
            }
        }

        private static void ShowClick(Vector3 position, ClickType type)
        {
            if (!Enabled)
            {
                return;
            }

            Hud.ShowClick(type, position);
        }

        private static void AfterAttack(AttackableUnit target, EventArgs args)
        {
            var t = target as AIHeroClient;
            if (t != null)
            {
                ShowClick(Randomize(t.Position), ClickType.Move);
            }
        }

        private static void OnIssueOrder(Obj_AI_Base sender, PlayerIssueOrderEventArgs args)
        {
            if (sender.IsMe &&
                (args.Order == GameObjectOrder.MoveTo || args.Order == GameObjectOrder.AttackUnit ||
                 args.Order == GameObjectOrder.AttackTo) &&
                lastclick + r.NextFloat(0.2f, 0.2f + .2f) < Game.Time)
            {
                var clickpos = args.TargetPosition;
                if (args.Order == GameObjectOrder.AttackUnit || args.Order == GameObjectOrder.AttackTo)
                {
                    ShowClick(Randomize(clickpos), ClickType.Attack);
                }
                else
                {
                    ShowClick(clickpos, ClickType.Move);
                }

                lastclick = Game.Time;
            }
        }

        private static Vector3 Randomize(Vector3 input)
        {
            if (r.Next(2) == 0)
            {
                input.X += r.Next(Random);
            }
            else
            {
                input.Y += r.Next(Random);
            }

            return input;
        }
    }
}