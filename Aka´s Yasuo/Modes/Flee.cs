using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

namespace AkaYasuo.Modes
{
    partial class Flee
    {
        public static void Fleemode()
        {
            foreach (Obj_AI_Base minion in EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Variables._Player.ServerPosition, Program.E.Range, true))
            {
                var bestMinion =
                   ObjectManager.Get<Obj_AI_Base>()
                       .Where(x => x.IsValidTarget(Program.E.Range))
                       .Where(x => x.Distance(Game.CursorPos) < Variables._Player.Distance(Game.CursorPos))
                       .OrderByDescending(x => x.Distance(Variables._Player))
                       .FirstOrDefault();

                if (bestMinion != null && Variables._Player.IsFacing(bestMinion) && Variables.CanCastE(bestMinion) && (Program.E.IsReady() && MenuManager.FleeMenu["EscE"].Cast<CheckBox>().CurrentValue))
                {
                    Program.E.Cast(bestMinion);
                }
                if (Program.Q.IsReady() && MenuManager.FleeMenu["EscQ"].Cast<CheckBox>().CurrentValue)
                {
                    if (!Variables.Q3READY(Variables._Player) && Program.Q.Range == 475)
                    {
                        Program.Q.Cast(minion);
                    }
                }
            }
            foreach (AIHeroClient enemy in EntityManager.Heroes.Enemies)
            {
                var bestMinion =
                   ObjectManager.Get<AIHeroClient>()
                       .Where(x => x.IsValidTarget(Program.E.Range))
                       .Where(x => x.Distance(Game.CursorPos) < Variables._Player.Distance(Game.CursorPos))
                       .OrderByDescending(x => x.Distance(Variables._Player))
                       .FirstOrDefault();

                if (bestMinion != null && Variables._Player.IsFacing(bestMinion) && (Program.E.IsReady() && MenuManager.FleeMenu["EscE"].Cast<CheckBox>().CurrentValue) && Variables._Player.IsFacing(bestMinion))
                {
                    Program.E.Cast(bestMinion);
                }
            }
        }
    }
}
