using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;
using EloBuddy.SDK.Rendering;

namespace AkaCore.Features.Orbwalk.AutoCatch
{
    class Events : IModule
    {
        public void OnLoad()
        {
            Axe.QReticles = new List<Axe.QRecticle>();
            Game.OnUpdate += Game_OnUpdate;
            Obj_AI_Base.OnNewPath += Obj_AI_Base_OnNewPath;
            GameObject.OnCreate += GameObject_OnCreate;
            GameObject.OnDelete += GameObject_OnDelete;
            Drawing.OnDraw += Drawing_OnDraw;
        }

        private void Drawing_OnDraw(EventArgs args)
        {
            if (Manager.MenuManager.DrawAxe)
            {
                var bestAxe =
                    Axe.QReticles.Where(
                        x =>
                        x.Position.Distance(Game.CursorPos) < Manager.MenuManager.AxeCatchRange)
                        .OrderBy(x => x.Position.Distance(ObjectManager.Player.ServerPosition))
                        .ThenBy(x => x.Position.Distance(Game.CursorPos))
                        .FirstOrDefault();

                if (bestAxe != null)
                {
                    Circle.Draw(Color.LimeGreen, 120, bestAxe.Position);
                }

                foreach (var axe in
                    Axe.QReticles.Where(x => x.Object.NetworkId != (bestAxe == null ? 0 : bestAxe.Object.NetworkId)))
                {
                    Circle.Draw(Color.GreenYellow, 120, axe.Position);
                }
            }

            if (Manager.MenuManager.DrawAxeCatchRange)
            {
                Circle.Draw(Color.DodgerBlue,
                    Manager.MenuManager.AxeCatchRange, Game.CursorPos);
            }
        }

        private void GameObject_OnDelete(GameObject sender, EventArgs args)
        {
            if (!sender.Name.Contains("Draven_Base_Q_reticle_self.troy"))
            {
                return;
            }

            Axe.QReticles.RemoveAll(x => x.Object.NetworkId == sender.NetworkId);
        }

        private void GameObject_OnCreate(GameObject sender, EventArgs args)
        {
            if (!sender.Name.Contains("Draven_Base_Q_reticle_self.troy"))
            {
                return;
            }

            Axe.QReticles.Add(new Axe.QRecticle(sender, Environment.TickCount + 1800));
            Core.DelayAction(() => Axe.QReticles.RemoveAll(x => x.Object.NetworkId == sender.NetworkId), 1800);
        }

        private void Game_OnUpdate(EventArgs args)
        {
            Axe.QReticles.RemoveAll(x => x.Object.IsDead);

            Axe.CatchAxe();
        }

        private void Obj_AI_Base_OnNewPath(Obj_AI_Base sender, GameObjectNewPathEventArgs args)
        {
            if (!sender.IsMe)
            {
                return;
            }

            Axe.CatchAxe();
        }

        public ModuleType GetModuleType()
        {
            return ModuleType.OnUpdate;
        }

        public bool ShouldGetExecuted()
        {
            return false;
        }

        public void OnExecute()
        {
        }
    }
}
