using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace AkaYasuo
{
    class Yasuo
    {
        public static Vector2 spot1 = new Vector2(7274, 5908);
        public static Vector2 spot2 = new Vector2(8222, 3158);
        public static Vector2 spot3 = new Vector2(3674, 7058);
        public static Vector2 spot4 = new Vector2(3788, 7422);
        public static Vector2 spot5 = new Vector2(8372, 9606);
        public static Vector2 spot6 = new Vector2(6650, 11766);
        public static Vector2 spot7 = new Vector2(1678, 8428);
        public static Vector2 spot8 = new Vector2(10832, 7446);
        public static Vector2 spot9 = new Vector2(11160, 7504);
        public static Vector2 spot10 = new Vector2(6424, 5208);
        public static Vector2 spot11 = new Vector2(13172, 6508);
        public static Vector2 spot12 = new Vector2(11222, 7856);
        public static Vector2 spot13 = new Vector2(10372, 8456);
        public static Vector2 spot14 = new Vector2(4324, 6258);
        public static Vector2 spot15 = new Vector2(6488, 11192);
        public static Vector2 spot16 = new Vector2(7672, 8906);


        public static float LastMoveC;
        public static void WallJump()
        {
            if (Variables._Player.Distance(spot1) <= 150)
            {
                MoveToLimited(spot1.To3D());
                //foreach (Obj_AI_Base minion in EntityManager.MinionsAndMonsters.GetJungleMonsters(Variables._Player.ServerPosition, Program.E.Range, true))
                var jminions = EntityManager.MinionsAndMonsters.GetJungleMonsters(Variables._Player.ServerPosition, 1000, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.Q3.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if (Player.Instance.Distance(spot1.To3D()) == 0 && jungleMobs.IsVisible && Program.E.IsReady() && jungleMobs.IsValidTarget(Program.E.Range) && Variables.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
                if (Player.Instance.Distance(spot1.To3D()) == 0 && Program.W.IsReady())
                {
                    Program.W.Cast(new Vector2(7110, 5612).To3D());
                }
            }
            if (Variables._Player.Distance(spot2) <= 150)
            {
                MoveToLimited(spot2.To3D());
                
                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Variables._Player.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.Q3.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if (Player.Instance.Distance(spot2.To3D()) == 0 && jungleMobs.IsVisible && Program.E.IsReady()  && jungleMobs.IsValidTarget(Program.E.Range) && Variables.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
                if (Player.Instance.Distance(spot2.To3D()) == 0 && Program.W.IsReady())
                {
                    Program.W.Cast(new Vector2(8372, 2908).To3D());
                }
            }
            if (Variables._Player.Distance(spot3) <= 150)
            {
                MoveToLimited(spot3.To3D());
                
                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Variables._Player.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.Q3.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if (Player.Instance.Distance(spot3.To3D()) == 0 && jungleMobs.IsVisible && Program.E.IsReady()  && jungleMobs.IsValidTarget(Program.E.Range) && Variables.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
                if (Player.Instance.Distance(spot3.To3D()) == 0 && Program.W.IsReady())
                {
                    Program.W.Cast(new Vector2(3674, 6708).To3D());
                }
            }
            if (Variables._Player.Distance(spot4) <= 150)
            {
                MoveToLimited(spot4.To3D());
                
                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Variables._Player.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.Q3.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if (Player.Instance.Distance(spot4.To3D()) == 0 && jungleMobs.IsVisible && Program.E.IsReady()  && jungleMobs.IsValidTarget(Program.E.Range) && Variables.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
                if (Player.Instance.Distance(spot4.To3D()) == 0 && Program.W.IsReady())
                {
                    Program.W.Cast(new Vector2(3774, 7706).To3D());
                }
            }
            if (Variables._Player.Distance(spot5) <= 150)
            {
                MoveToLimited(spot5.To3D());
                
                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Variables._Player.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.Q3.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if (Player.Instance.Distance(spot5.To3D()) == 0 && jungleMobs.IsVisible && Program.E.IsReady()  && jungleMobs.IsValidTarget(Program.E.Range) && Variables.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
                if (Player.Instance.Distance(spot5.To3D()) == 0 && Program.W.IsReady())
                {
                    Program.W.Cast(new Vector2(7923, 9351).To3D());
                }
            }
            if (Variables._Player.Distance(spot6) <= 150)
            {
                MoveToLimited(spot6.To3D());
                if (Player.Instance.Distance(spot6.To3D()) == 0 && Program.W.IsReady())
                {
                    Program.W.Cast(new Vector2(6426, 12138).To3D());
                }
                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Variables._Player.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.Q3.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if (Player.Instance.Distance(spot6.To3D()) == 0 && jungleMobs.IsVisible && Program.E.IsReady()  && jungleMobs.IsValidTarget(Program.E.Range) && Variables.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
                if (Player.Instance.Distance(spot6.To3D()) == 0 && Program.W.IsReady())
                {
                    Program.W.Cast(new Vector2(6426, 12138).To3D());
                }
            }
            if (Variables._Player.Distance(spot7) <= 150)
            {
                MoveToLimited(spot7.To3D());
                
                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Variables._Player.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.Q3.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if (Player.Instance.Distance(spot7.To3D()) == 0 && jungleMobs.IsVisible && Program.E.IsReady()  && jungleMobs.IsValidTarget(Program.E.Range) && Variables.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
                if (Player.Instance.Distance(spot7.To3D()) == 0 && Program.W.IsReady())
                {
                    Program.W.Cast(new Vector2(2050, 8416).To3D());
                }
            }
            if (Variables._Player.Distance(spot8) <= 150)
            {
                MoveToLimited(spot8.To3D());
                
                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Variables._Player.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.Q3.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if (Player.Instance.Distance(spot8.To3D()) == 0 && jungleMobs.IsVisible && Program.E.IsReady()  && jungleMobs.IsValidTarget(Program.E.Range) && Variables.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
                if (Player.Instance.Distance(spot8.To3D()) == 0 && Program.W.IsReady())
                {
                    Program.W.Cast(new Vector2(10894, 7192).To3D());
                }
            }
            if (Variables._Player.Distance(spot9) <= 150)
            {
                MoveToLimited(spot9.To3D());
                
                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Variables._Player.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.Q3.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if (Player.Instance.Distance(spot9.To3D()) == 0 && jungleMobs.IsVisible && Program.E.IsReady()  && jungleMobs.IsValidTarget(Program.E.Range) && Variables.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
                if (Player.Instance.Distance(spot9.To3D()) == 0 && Program.W.IsReady())
                {
                    Program.W.Cast(new Vector2(11172, 7208).To3D());
                }
            }
            if (Variables._Player.Distance(spot10) <= 150)
            {
                MoveToLimited(spot10.To3D());
                
                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Variables._Player.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.Q3.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if (Player.Instance.Distance(spot10.To3D()) == 0 && jungleMobs.IsVisible && Program.E.IsReady() && jungleMobs.IsValidTarget(Program.E.Range) && Variables.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
                if (Player.Instance.Distance(spot10.To3D()) == 0 && Program.W.IsReady())
                {
                    Program.W.Cast(new Vector2(6824, 5308).To3D());
                }
            }
            if (Variables._Player.Distance(spot11) <= 150)
            {
                MoveToLimited(spot11.To3D());
                
                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Variables._Player.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.Q3.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if (Player.Instance.Distance(spot11.To3D()) == 0 && jungleMobs.IsVisible && Program.E.IsReady()  && jungleMobs.IsValidTarget(Program.E.Range) && Variables.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
                if (Player.Instance.Distance(spot11.To3D()) == 0 && Program.W.IsReady())
                {
                    Program.W.Cast(new Vector2(12772, 6458).To3D());
                }
            }
            if (Variables._Player.Distance(spot12) <= 150)
            {
                MoveToLimited(spot12.To3D());
                
                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Variables._Player.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.Q3.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if (Player.Instance.Distance(spot12.To3D()) == 0 && jungleMobs.IsVisible && Program.E.IsReady()  && jungleMobs.IsValidTarget(Program.E.Range) && Variables.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
                if (Player.Instance.Distance(spot12.To3D()) == 0 && Program.W.IsReady())
                {
                    Program.W.Cast(new Vector2(11072, 8156).To3D());
                }
            }
            if (Variables._Player.Distance(spot13) <= 150)
            {
                MoveToLimited(spot13.To3D());
                
                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Variables._Player.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.Q3.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if ((Player.Instance.Distance(spot13.To3D()) == 0) && jungleMobs.IsVisible && Program.E.IsReady()  && jungleMobs.IsValidTarget(Program.E.Range) && Variables.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
                if ((Player.Instance.Distance(spot13.To3D()) == 0) && Program.W.IsReady())
                {
                    Program.W.Cast(new Vector2(10772, 8456).To3D());
                }
            }
            if (Variables._Player.Distance(spot14) <= 150)
            {
                MoveToLimited(spot14.To3D());
                
                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Variables._Player.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.Q3.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if ((Player.Instance.Distance(spot14.To3D()) == 0) && jungleMobs.IsVisible && Program.E.IsReady()  && jungleMobs.IsValidTarget(Program.E.Range) && Variables.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
                if ((Player.Instance.Distance(spot14.To3D()) == 0) && Program.W.IsReady())
                {
                    Program.W.Cast(new Vector2(4024, 6358).To3D());
                }
            }
            if (Variables._Player.Distance(spot15) <= 150)
            {
                MoveToLimited(spot15.To3D());
                
                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Variables._Player.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.Q3.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if ((Player.Instance.Distance(spot15.To3D()) == 0) && jungleMobs.IsVisible && Program.E.IsReady()  && jungleMobs.IsValidTarget(Program.E.Range) && Variables.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
                if ((Player.Instance.Distance(spot15.To3D()) == 0) && Program.W.IsReady())
                {
                    Program.W.Cast(new Vector2(66986, 10910).To3D());
                }
            }
            if (Variables._Player.Distance(spot16) <= 150)
            {
                MoveToLimited(spot16.To3D());
                
                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Variables._Player.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.Q3.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if ((Player.Instance.Distance(spot16.To3D()) == 0) && jungleMobs.IsVisible && Program.E.IsReady()  && jungleMobs.IsValidTarget(Program.E.Range) && Variables.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
                if ((Player.Instance.Distance(spot16.To3D()) == 0) && Program.W.IsReady())
                {
                    Program.W.Cast(new Vector2(7822, 9306).To3D());
                }
            }
        }
        private static void MoveToLimited(Vector3 where)
        {
            if (Environment.TickCount - LastMoveC < 80)
            {
                return;
            }
            LastMoveC = Environment.TickCount;
            Player.IssueOrder(GameObjectOrder.MoveTo, where);

            if (Player.Instance.ServerPosition.Equals(where))
            {
                Orbwalker.DisableMovement = false;
            }
        }
    }
}
