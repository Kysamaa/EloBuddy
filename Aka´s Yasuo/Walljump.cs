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
        public static Vector2 spotA = new Vector2(10922, 6908);
        public static Vector2 spotB = new Vector2(7616, 4074);
        public static Vector2 spotC = new Vector2(2232, 8412);
        public static Vector2 spotD = new Vector2(7046, 5426);
        public static Vector2 spotE = new Vector2(8322, 2658);
        public static Vector2 spotF = new Vector2(3676, 7968);
        public static Vector2 spotG = new Vector2(3892, 6466);
        public static Vector2 spotH = new Vector2(12582, 6402);
        public static Vector2 spotI = new Vector2(11072, 8306);
        public static Vector2 spotJ = new Vector2(10882, 8416);
        public static Vector2 spotK = new Vector2(3730, 8080);
        public static Vector2 spotL = new Vector2(6574, 12256);
        public static Vector2 spotM = new Vector2(7244, 10890);
        public static Vector2 spotN = new Vector2(7784, 9494);
        public static Vector2 spotO = new Vector2(6984, 10980);

        public static float LastMoveC;

        public static void WallDash()
        {
            if (Variables._Player.Distance(spotA) <= 600)
            {
                MoveToLimited(spotA.To3D());


                var jminions = EntityManager.MinionsAndMonsters.GetJungleMonsters(Variables._Player.ServerPosition, 1000, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Variables._Player.AttackRange)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if (Variables._Player.Distance(spotA.To3D()) == 0 && jungleMobs.BaseSkinName == "SRU_Blue" && jungleMobs.IsVisible && Program.E.IsReady() && jungleMobs != null && jungleMobs.IsValidTarget(Program.E.Range) && Variables.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
            }
            if (Variables._Player.Distance(spotB) <= 600)
            {
                MoveToLimited(spotB.To3D());


                var jminions = EntityManager.MinionsAndMonsters.GetJungleMonsters(Variables._Player.ServerPosition, 1000, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Variables._Player.AttackRange)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if (Variables._Player.Distance(spotB.To3D()) == 0  && jungleMobs.BaseSkinName != "SRU_Red" && jungleMobs.BaseSkinName != "SRU_RedMini4.1.3" && jungleMobs.IsVisible && Program.E.IsReady() && jungleMobs != null && jungleMobs.IsValidTarget(Program.E.Range) && Variables.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
            }
            if (Variables._Player.Distance(spotC) <= 600)
            {
                MoveToLimited(spotC.To3D());


                var jminions = EntityManager.MinionsAndMonsters.GetJungleMonsters(Variables._Player.ServerPosition, 1000, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.E.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if (Variables._Player.Distance(spotC.To3D()) == 0  && jungleMobs.IsVisible && Program.E.IsReady() && jungleMobs != null && jungleMobs.IsValidTarget(Program.E.Range) && Variables.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
            }
            if (Variables._Player.Distance(spotD) <= 600)
            {
                MoveToLimited(spotD.To3D());


                var jminions = EntityManager.MinionsAndMonsters.GetJungleMonsters(Variables._Player.ServerPosition, 1000, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(100)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if (Variables._Player.Distance(spotD.To3D()) == 0  && jungleMobs.BaseSkinName != "SRU_Razorbreak" && jungleMobs.BaseSkinName != "SRU_RazorbreakMini3.1.2" && jungleMobs.BaseSkinName != "SRU_RazorbreakMini3.1.4" && jungleMobs.IsVisible && Program.E.IsReady() && jungleMobs != null && jungleMobs.IsValidTarget(Program.E.Range) && Variables.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
            }
            if (Variables._Player.Distance(spotE) <= 600)
            {
                MoveToLimited(spotE.To3D());


                var jminions = EntityManager.MinionsAndMonsters.GetJungleMonsters(Variables._Player.ServerPosition, 1000, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.E.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if (Variables._Player.Distance(spotE.To3D()) == 0  && jungleMobs.BaseSkinName == "SRU_KrugMini" && jungleMobs.IsVisible && Program.E.IsReady() && jungleMobs != null && jungleMobs.IsValidTarget(Program.E.Range) && Variables.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
            }
            if (Variables._Player.Distance(spotF) <= 400)
            {
                MoveToLimited(spotF.To3D());


                var jminions = EntityManager.MinionsAndMonsters.GetJungleMonsters(Variables._Player.ServerPosition, 1000, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.E.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if (Variables._Player.Distance(spotF.To3D()) == 0  && jungleMobs.BaseSkinName != "SRU_Blue" && jungleMobs.BaseSkinName != "SRU_BlueMini1.1.2" && jungleMobs.IsVisible && Program.E.IsReady() && jungleMobs != null && jungleMobs.IsValidTarget(Program.E.Range) && Variables.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
            }
            if (Variables._Player.Distance(spotG) <= 600)
            {
                MoveToLimited(spotG.To3D());


                var jminions = EntityManager.MinionsAndMonsters.GetJungleMonsters(Variables._Player.ServerPosition, 1000, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Variables._Player.AttackRange)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if (Variables._Player.Distance(spotG.To3D()) == 0  && jungleMobs.BaseSkinName != "SRU_Murkwolf" && jungleMobs.BaseSkinName != "SRU_MurkwolfMini2.1.3" && jungleMobs.IsVisible && Program.E.IsReady() && jungleMobs != null && jungleMobs.IsValidTarget(Program.E.Range) && Variables.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
            }
            if (Variables._Player.Distance(spotH) <= 600)
            {
                MoveToLimited(spotH.To3D());


                var jminions = EntityManager.MinionsAndMonsters.GetJungleMonsters(Variables._Player.ServerPosition, 1000, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.E.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if (Variables._Player.Distance(spotH.To3D()) == 0  && jungleMobs.IsVisible && Program.E.IsReady() && jungleMobs != null && jungleMobs.IsValidTarget(Program.E.Range) && Variables.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
            }
            if (Variables._Player.Distance(spotI) <= 120)
            {
                MoveToLimited(spotI.To3D());


                var jminions = EntityManager.MinionsAndMonsters.GetJungleMonsters(Variables._Player.ServerPosition, 1000, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(100)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if (Variables._Player.Distance(spotI.To3D()) == 0  && jungleMobs.BaseSkinName != "SRU_Murkwolf" && jungleMobs.BaseSkinName != "SRU_MurkwolfMini8.1.3" && jungleMobs.IsVisible && Program.E.IsReady() && jungleMobs != null && jungleMobs.IsValidTarget(Program.E.Range) && Variables.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
            }
            if (Variables._Player.Distance(spotJ) <= 120)
            {
                MoveToLimited(spotJ.To3D());


                var jminions = EntityManager.MinionsAndMonsters.GetJungleMonsters(Variables._Player.ServerPosition, 1000, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.E.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if (Variables._Player.Distance(spotJ.To3D()) == 0  && jungleMobs.BaseSkinName != "SRU_Murkwolf" && jungleMobs.BaseSkinName != "SRU_MurkwolfMini8.1.2" && jungleMobs.IsVisible && Program.E.IsReady() && jungleMobs != null && jungleMobs.IsValidTarget(Program.E.Range) && Variables.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
            }
            if (Variables._Player.Distance(spotL) <= 600)
            {
                MoveToLimited(spotL.To3D());


                var jminions = EntityManager.MinionsAndMonsters.GetJungleMonsters(Variables._Player.ServerPosition, 1000, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.E.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if (Variables._Player.Distance(spotL.To3D()) == 0  && jungleMobs.BaseSkinName == "SRU_KrugMini" && jungleMobs.IsVisible && Program.E.IsReady() && jungleMobs != null && jungleMobs.IsValidTarget(Program.E.Range) && Variables.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
            }
            if (Variables._Player.Distance(spotM) <= 200)
            {
                MoveToLimited(spotM.To3D());


                var jminions = EntityManager.MinionsAndMonsters.GetJungleMonsters(Variables._Player.ServerPosition, 1000, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Variables._Player.AttackRange)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if (Variables._Player.Distance(spotM.To3D()) == 0  && jungleMobs.BaseSkinName != "SRU_Red" && jungleMobs.BaseSkinName != "SRU_RedMini10.1.3" && jungleMobs.IsVisible && Program.E.IsReady() && jungleMobs != null && jungleMobs.IsValidTarget(Program.E.Range) && Variables.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
            }
            if (Variables._Player.Distance(spotN) <= 600)
            {
                MoveToLimited(spotN.To3D());


                var jminions = EntityManager.MinionsAndMonsters.GetJungleMonsters(Variables._Player.ServerPosition, 1000, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(100)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if (Variables._Player.Distance(spotN.To3D()) == 0  && jungleMobs.BaseSkinName != "SRU_RazorbreakMini9.1.2" && jungleMobs.BaseSkinName != "SRU_RazorbreakMini9.1.4" && jungleMobs.BaseSkinName != "SRU_Razorbreak" && jungleMobs.IsVisible && Program.E.IsReady() && jungleMobs != null && Variables.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
            }
            if (Variables._Player.Distance(spotO) <= 200)
            {
                MoveToLimited(spotO.To3D());


                var jminions = EntityManager.MinionsAndMonsters.GetJungleMonsters(Variables._Player.ServerPosition, 1000, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Variables._Player.AttackRange)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if (Variables._Player.Distance(spotO.To3D()) == 0  && jungleMobs.BaseSkinName != "SRU_Red" && jungleMobs.BaseSkinName != "SRU_RedMini10.1.2" && jungleMobs.IsVisible && Program.E.IsReady() && jungleMobs != null && jungleMobs.IsValidTarget(Program.E.Range) && Variables.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
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
        }
    }
}