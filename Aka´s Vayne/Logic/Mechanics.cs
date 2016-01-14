
using System.Linq;
using AddonTemplate.Logic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

namespace Aka_s_Vayne_reworked.Logic
{
    internal class Mechanics
    {
        public static void FlashE()
        {
            var positions = ELogic.GetRotatedFlashPositions();

            foreach (var p in positions)
            {
                var condemnUnit = ELogic.CondemnCheck(p);
                if (condemnUnit != null && MenuManager.MechanicMenu["flashe"].Cast<KeyBind>().CurrentValue)
                {
                    Program.E.Cast(condemnUnit);

                    Variables._Player.Spellbook.CastSpell(Variables.FlashSlot, p);

                }
            }
        }

        public static void Insec()
        {
            if (!MenuManager.MechanicMenu["insece"].Cast<KeyBind>().CurrentValue) return;

            var mode = (MenuManager.MechanicMenu["insecmodes"].Cast<Slider>().CurrentValue);
            var target = Orbwalker.GetTarget() as AIHeroClient;
            if (target != null)
            {
                //var targetfuturepos = Prediction.GetPrediction(target, 0.1f).UnitPosition;
                bool caninsec = Variables._Player.Distance(target) <= 400;
                switch (mode)
                {
                    case 1:
                        var hero =
                            HeroManager.Allies.Where(x => !x.IsMe && !x.IsDead)
                                .OrderByDescending(x => x.Distance(Variables._Player.Position))
                                .LastOrDefault();
                        if (hero != null && caninsec &&
                            Variables._Player.ServerPosition.Distance(hero.Position) + 100 >=
                            target.Distance(hero.Position))
                        {
                            var ePred = Program.E2.GetPrediction(target);
                            int pushDist = 550;
                            for (int i = 0; i < pushDist; i += (int) target.BoundingRadius)
                            {
                                Vector3 loc3 =
                                    ePred.UnitPosition.To2D().Extend(ELogic.GetFlashPos(target, true).To2D(), -i).To3D();
                                if (loc3.Distance(hero) < hero.Position.Distance(target))
                                {
                                    Variables._Player.Spellbook.CastSpell(Variables.FlashSlot, ELogic.GetFlashPos(target, true));
                                    Program.E.Cast(target);
                                }
                            }
                        }
                        break;
                    case 2:
                        var turret =
                            ObjectManager.Get<Obj_AI_Turret>()
                                .Where(x => x.IsAlly && !x.IsDead)
                                .OrderByDescending(x => x.Distance(Variables._Player.Position))
                                .LastOrDefault();
                        if (turret != null && caninsec &&
                            Variables._Player.ServerPosition.Distance(turret.Position) + 100 >=
                            target.Distance(turret.Position))
                        {
                            var ePred = Program.E2.GetPrediction(target);
                            int pushDist = 550;
                            for (int i = 0; i < pushDist; i += (int) target.BoundingRadius)
                            {
                                Vector3 loc3 =
                                    ePred.UnitPosition.To2D().Extend(ELogic.GetFlashPos(target, true).To2D(), -i).To3D();
                                if (loc3.Distance(turret) < turret.Position.Distance(target))
                                {
                                    Variables._Player.Spellbook.CastSpell(Variables.FlashSlot, ELogic.GetFlashPos(target, true));
                                    Program.E.Cast(target);
                                }
                            }
                        }
                        break;
                    case 3:
                        if (caninsec &&
                            Variables._Player.ServerPosition.Distance(Game.CursorPos) + 100 >=
                            target.Distance(Game.CursorPos))
                        {
                            var ePred = Program.E2.GetPrediction(target);
                            int pushDist = 550;
                            for (int i = 0; i < pushDist; i += (int) target.BoundingRadius)
                            {
                                Vector3 loc3 =
                                    ePred.UnitPosition.To2D().Extend(ELogic.GetFlashPos(target, true).To2D(), -i).To3D();
                                if (loc3.Distance(Game.CursorPos) < Game.CursorPos.Distance(target))
                                {
                                    Variables._Player.Spellbook.CastSpell(Variables.FlashSlot, ELogic.GetFlashPos(target, true));
                                    Program.E.Cast(target);
                                }
                            }
                        }
                        break;

                }
            }
        }
    }
}
