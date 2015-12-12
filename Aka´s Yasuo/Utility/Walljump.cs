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
        public static void WallJump()
        {
            if (Program.myHero.Distance(spot1) <= 150)
            {
                MoveToLimited(spot1.To3D());
                //foreach (Obj_AI_Base minion in EntityManager.MinionsAndMonsters.GetJungleMonsters(Program.myHero.ServerPosition, Program.E.Range, true))
                var jminions = EntityManager.MinionsAndMonsters.GetJungleMonsters(Program.myHero.ServerPosition, 1000, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.Q3.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if (Player.Instance.Distance(spot1.To3D()) == 0 && jungleMobs.IsVisible && Program.E.IsReady() && jungleMobs.IsValidTarget(Program.E.Range) && Program.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
                if (Player.Instance.Distance(spot1.To3D()) == 0 && Program.W.IsReady())
                {
                    Program.W.Cast(new Vector2(7110, 5612).To3D());
                }
            }
            if (Program.myHero.Distance(spot2) <= 150)
            {
                MoveToLimited(spot2.To3D());
                
                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Program.myHero.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.Q3.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if (Player.Instance.Distance(spot2.To3D()) == 0 && jungleMobs.IsVisible && Program.E.IsReady()  && jungleMobs.IsValidTarget(Program.E.Range) && Program.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
                if (Player.Instance.Distance(spot2.To3D()) == 0 && Program.W.IsReady())
                {
                    Program.W.Cast(new Vector2(8372, 2908).To3D());
                }
            }
            if (Program.myHero.Distance(spot3) <= 150)
            {
                MoveToLimited(spot3.To3D());
                
                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Program.myHero.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.Q3.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if (Player.Instance.Distance(spot3.To3D()) == 0 && jungleMobs.IsVisible && Program.E.IsReady()  && jungleMobs.IsValidTarget(Program.E.Range) && Program.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
                if (Player.Instance.Distance(spot3.To3D()) == 0 && Program.W.IsReady())
                {
                    Program.W.Cast(new Vector2(3674, 6708).To3D());
                }
            }
            if (Program.myHero.Distance(spot4) <= 150)
            {
                MoveToLimited(spot4.To3D());
                
                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Program.myHero.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.Q3.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if (Player.Instance.Distance(spot4.To3D()) == 0 && jungleMobs.IsVisible && Program.E.IsReady()  && jungleMobs.IsValidTarget(Program.E.Range) && Program.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
                if (Player.Instance.Distance(spot4.To3D()) == 0 && Program.W.IsReady())
                {
                    Program.W.Cast(new Vector2(3774, 7706).To3D());
                }
            }
            if (Program.myHero.Distance(spot5) <= 150)
            {
                MoveToLimited(spot5.To3D());
                
                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Program.myHero.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.Q3.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if (Player.Instance.Distance(spot5.To3D()) == 0 && jungleMobs.IsVisible && Program.E.IsReady()  && jungleMobs.IsValidTarget(Program.E.Range) && Program.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
                if (Player.Instance.Distance(spot5.To3D()) == 0 && Program.W.IsReady())
                {
                    Program.W.Cast(new Vector2(7923, 9351).To3D());
                }
            }
            if (Program.myHero.Distance(spot6) <= 150)
            {
                MoveToLimited(spot6.To3D());
                if (Player.Instance.Distance(spot6.To3D()) == 0 && Program.W.IsReady())
                {
                    Program.W.Cast(new Vector2(6426, 12138).To3D());
                }
                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Program.myHero.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.Q3.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if (Player.Instance.Distance(spot6.To3D()) == 0 && jungleMobs.IsVisible && Program.E.IsReady()  && jungleMobs.IsValidTarget(Program.E.Range) && Program.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
                if (Player.Instance.Distance(spot6.To3D()) == 0 && Program.W.IsReady())
                {
                    Program.W.Cast(new Vector2(6426, 12138).To3D());
                }
            }
            if (Program.myHero.Distance(spot7) <= 150)
            {
                MoveToLimited(spot7.To3D());
                
                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Program.myHero.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.Q3.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if (Player.Instance.Distance(spot7.To3D()) == 0 && jungleMobs.IsVisible && Program.E.IsReady()  && jungleMobs.IsValidTarget(Program.E.Range) && Program.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
                if (Player.Instance.Distance(spot7.To3D()) == 0 && Program.W.IsReady())
                {
                    Program.W.Cast(new Vector2(2050, 8416).To3D());
                }
            }
            if (Program.myHero.Distance(spot8) <= 150)
            {
                MoveToLimited(spot8.To3D());
                
                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Program.myHero.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.Q3.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if (Player.Instance.Distance(spot8.To3D()) == 0 && jungleMobs.IsVisible && Program.E.IsReady()  && jungleMobs.IsValidTarget(Program.E.Range) && Program.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
                if (Player.Instance.Distance(spot8.To3D()) == 0 && Program.W.IsReady())
                {
                    Program.W.Cast(new Vector2(10894, 7192).To3D());
                }
            }
            if (Program.myHero.Distance(spot9) <= 150)
            {
                MoveToLimited(spot9.To3D());
                
                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Program.myHero.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.Q3.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if (Player.Instance.Distance(spot9.To3D()) == 0 && jungleMobs.IsVisible && Program.E.IsReady()  && jungleMobs.IsValidTarget(Program.E.Range) && Program.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
                if (Player.Instance.Distance(spot9.To3D()) == 0 && Program.W.IsReady())
                {
                    Program.W.Cast(new Vector2(11172, 7208).To3D());
                }
            }
            if (Program.myHero.Distance(spot10) <= 150)
            {
                MoveToLimited(spot10.To3D());
                
                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Program.myHero.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.Q3.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if (Player.Instance.Distance(spot10.To3D()) == 0 && jungleMobs.IsVisible && Program.E.IsReady() && jungleMobs.IsValidTarget(Program.E.Range) && Program.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
                if (Player.Instance.Distance(spot10.To3D()) == 0 && Program.W.IsReady())
                {
                    Program.W.Cast(new Vector2(6824, 5308).To3D());
                }
            }
            if (Program.myHero.Distance(spot11) <= 150)
            {
                MoveToLimited(spot11.To3D());
                
                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Program.myHero.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.Q3.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if (Player.Instance.Distance(spot11.To3D()) == 0 && jungleMobs.IsVisible && Program.E.IsReady()  && jungleMobs.IsValidTarget(Program.E.Range) && Program.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
                if (Player.Instance.Distance(spot11.To3D()) == 0 && Program.W.IsReady())
                {
                    Program.W.Cast(new Vector2(12772, 6458).To3D());
                }
            }
            if (Program.myHero.Distance(spot12) <= 150)
            {
                MoveToLimited(spot12.To3D());
                
                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Program.myHero.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.Q3.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if (Player.Instance.Distance(spot12.To3D()) == 0 && jungleMobs.IsVisible && Program.E.IsReady()  && jungleMobs.IsValidTarget(Program.E.Range) && Program.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
                if (Player.Instance.Distance(spot12.To3D()) == 0 && Program.W.IsReady())
                {
                    Program.W.Cast(new Vector2(11072, 8156).To3D());
                }
            }
            if (Program.myHero.Distance(spot13) <= 150)
            {
                MoveToLimited(spot13.To3D());
                
                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Program.myHero.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.Q3.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if ((Player.Instance.Distance(spot13.To3D()) == 0) && jungleMobs.IsVisible && Program.E.IsReady()  && jungleMobs.IsValidTarget(Program.E.Range) && Program.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
                if ((Player.Instance.Distance(spot13.To3D()) == 0) && Program.W.IsReady())
                {
                    Program.W.Cast(new Vector2(10772, 8456).To3D());
                }
            }
            if (Program.myHero.Distance(spot14) <= 150)
            {
                MoveToLimited(spot14.To3D());
                
                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Program.myHero.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.Q3.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if ((Player.Instance.Distance(spot14.To3D()) == 0) && jungleMobs.IsVisible && Program.E.IsReady()  && jungleMobs.IsValidTarget(Program.E.Range) && Program.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
                if ((Player.Instance.Distance(spot14.To3D()) == 0) && Program.W.IsReady())
                {
                    Program.W.Cast(new Vector2(4024, 6358).To3D());
                }
            }
            if (Program.myHero.Distance(spot15) <= 150)
            {
                MoveToLimited(spot15.To3D());
                
                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Program.myHero.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.Q3.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if ((Player.Instance.Distance(spot15.To3D()) == 0) && jungleMobs.IsVisible && Program.E.IsReady()  && jungleMobs.IsValidTarget(Program.E.Range) && Program.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
                if ((Player.Instance.Distance(spot15.To3D()) == 0) && Program.W.IsReady())
                {
                    Program.W.Cast(new Vector2(66986, 10910).To3D());
                }
            }
            if (Program.myHero.Distance(spot16) <= 150)
            {
                MoveToLimited(spot16.To3D());
                
                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Program.myHero.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.Q3.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if ((Player.Instance.Distance(spot16.To3D()) == 0) && jungleMobs.IsVisible && Program.E.IsReady()  && jungleMobs.IsValidTarget(Program.E.Range) && Program.CanCastE(jungleMobs))
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

        public static void WallDash()
        {
            if (Program.myHero.Distance(spotA) <= 600)
            {
                MoveToLimited(spotA.To3D());

                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Program.myHero.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.myHero.AttackRange)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if ((Player.Instance.Distance(spotA.To3D()) == 0) && jungleMobs.BaseSkinName == "SRU_Blue" && jungleMobs.IsVisible && Program.E.IsReady()  && jungleMobs.IsValidTarget(Program.E.Range) && Program.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
            }
            if (Program.myHero.Distance(spotB) <= 600)
            {
                MoveToLimited(spotB.To3D());

                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Program.myHero.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.myHero.AttackRange)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if ((Player.Instance.Distance(spotB.To3D()) == 0) && jungleMobs.BaseSkinName != "SRU_Red" && jungleMobs.BaseSkinName != "SRU_RedMini4.1.3" && jungleMobs.IsVisible && Program.E.IsReady()  && jungleMobs.IsValidTarget(Program.E.Range) && Program.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
            }
            if (Program.myHero.Distance(spotC) <= 600)
            {
                MoveToLimited(spotC.To3D());

                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Program.myHero.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.E.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if ((Player.Instance.Distance(spotC.To3D()) == 0) && jungleMobs.IsVisible && Program.E.IsReady()  && jungleMobs.IsValidTarget(Program.E.Range) && Program.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
            }
            if (Program.myHero.Distance(spotD) <= 600)
            {
                MoveToLimited(spotD.To3D());

                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Program.myHero.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(100)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    //Player.Instance.Distance(spotD.To3D()) && 
                    if ((Player.Instance.Distance(spotD.To3D()) == 0) && jungleMobs.BaseSkinName != "SRU_Razorbreak" && jungleMobs.BaseSkinName != "SRU_RazorbreakMini3.1.2" && jungleMobs.BaseSkinName != "SRU_RazorbreakMini3.1.4" && jungleMobs.IsVisible && Program.E.IsReady()  && jungleMobs.IsValidTarget(Program.E.Range) && Program.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
            }
            if (Program.myHero.Distance(spotE) <= 600)
            {
                MoveToLimited(spotE.To3D());

                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Program.myHero.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.E.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if ((Player.Instance.Distance(spotE.To3D()) == 0) && jungleMobs.BaseSkinName == "SRU_KrugMini" && jungleMobs.IsVisible && Program.E.IsReady()  && jungleMobs.IsValidTarget(Program.E.Range) && Program.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
            }
            if (Program.myHero.Distance(spotF) <= 400)
            {
                MoveToLimited(spotF.To3D());

                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Program.myHero.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.E.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if ((Player.Instance.Distance(spotF.To3D()) == 0) && jungleMobs.BaseSkinName != "SRU_Blue" && jungleMobs.BaseSkinName != "SRU_BlueMini1.1.2" && jungleMobs.IsVisible && Program.E.IsReady()  && jungleMobs.IsValidTarget(Program.E.Range) && Program.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
            }
            if (Program.myHero.Distance(spotG) <= 600)
            {
                MoveToLimited(spotG.To3D());

                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Program.myHero.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.myHero.AttackRange)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if ((Player.Instance.Distance(spotG.To3D()) == 0) && jungleMobs.BaseSkinName != "SRU_Murkwolf" && jungleMobs.BaseSkinName != "SRU_MurkwolfMini2.1.3" && jungleMobs.IsVisible && Program.E.IsReady()  && jungleMobs.IsValidTarget(Program.E.Range) && Program.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
            }
            if (Program.myHero.Distance(spotH) <= 600)
            {
                MoveToLimited(spotH.To3D());

                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Program.myHero.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.E.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if ((Player.Instance.Distance(spotH.To3D()) == 0) && jungleMobs.IsVisible && Program.E.IsReady()  && jungleMobs.IsValidTarget(Program.E.Range) && Program.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
            }
            if (Program.myHero.Distance(spotI) <= 120)
            {
                MoveToLimited(spotI.To3D());

                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Program.myHero.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(100)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if ((Player.Instance.Distance(spotI.To3D()) == 0) && jungleMobs.BaseSkinName != "SRU_Murkwolf" && jungleMobs.BaseSkinName != "SRU_MurkwolfMini8.1.3" && jungleMobs.IsVisible && Program.E.IsReady()  && jungleMobs.IsValidTarget(Program.E.Range) && Program.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
            }
            if (Program.myHero.Distance(spotJ) <= 120)
            {
                MoveToLimited(spotJ.To3D());

                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Program.myHero.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.E.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if ((Player.Instance.Distance(spotJ.To3D()) == 0) && jungleMobs.BaseSkinName != "SRU_Murkwolf" && jungleMobs.BaseSkinName != "SRU_MurkwolfMini8.1.2" && jungleMobs.IsVisible && Program.E.IsReady()  && jungleMobs.IsValidTarget(Program.E.Range) && Program.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
            }
            if (Program.myHero.Distance(spotL) <= 600)
            {
                MoveToLimited(spotL.To3D());

                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Program.myHero.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.E.Range)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if ((Player.Instance.Distance(spotL.To3D()) == 0) && jungleMobs.BaseSkinName == "SRU_KrugMini" && jungleMobs.IsVisible && Program.E.IsReady()  && jungleMobs.IsValidTarget(Program.E.Range) && Program.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
            }
            if (Program.myHero.Distance(spotM) <= 200)
            {
                MoveToLimited(spotM.To3D());

                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Program.myHero.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.myHero.AttackRange)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if ((Player.Instance.Distance(spotM.To3D()) == 0) && jungleMobs.BaseSkinName != "SRU_Red" && jungleMobs.BaseSkinName != "SRU_RedMini10.1.3" && jungleMobs.IsVisible && Program.E.IsReady()  && jungleMobs.IsValidTarget(Program.E.Range) && Program.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
            }
            if (Program.myHero.Distance(spotN) <= 600)
            {
                MoveToLimited(spotN.To3D());

                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Program.myHero.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(100)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if ((Player.Instance.Distance(spotN.To3D()) == 0) && jungleMobs.BaseSkinName != "SRU_RazorbreakMini9.1.2" && jungleMobs.BaseSkinName != "SRU_RazorbreakMini9.1.4" && jungleMobs.BaseSkinName != "SRU_Razorbreak" && jungleMobs.IsVisible && Program.E.IsReady()  && Program.CanCastE(jungleMobs))
                    {
                        Program.E.Cast(jungleMobs);
                    }
                }
            }
            if (Program.myHero.Distance(spotO) <= 200)
            {
                MoveToLimited(spotO.To3D());

                var jminions =  EntityManager.MinionsAndMonsters.GetJungleMonsters(Program.myHero.ServerPosition, Program.E.Range, true);
                foreach (var jungleMobs in jminions.Where(x => x.IsValidTarget(Program.myHero.AttackRange)))
                {
                    if (jungleMobs == null)
                    {
                        return;
                    }
                    if ((Player.Instance.Distance(spotO.To3D()) == 0) && jungleMobs.BaseSkinName != "SRU_Red" && jungleMobs.BaseSkinName != "SRU_RedMini10.1.2" && jungleMobs.IsVisible && Program.E.IsReady()  && jungleMobs.IsValidTarget(Program.E.Range) && Program.CanCastE(jungleMobs))
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

            if (Player.Instance.ServerPosition.Equals(where))
            {
                Orbwalker.DisableMovement = false;
            }
        }
    }
}
