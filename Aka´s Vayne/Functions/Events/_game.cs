
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;

namespace Aka_s_Vayne_reworked.Functions.Events
{
    class _game
    {
        public static AIHeroClient FocusWTarget
        {
            get
            {
                return ObjectManager.Get<AIHeroClient>()
                    .Where(
                        enemy =>
                            !enemy.IsDead &&
                            enemy.IsValidTarget((Program.Q.IsReady() ? Program.Q.Range : 0) + Variables._Player.AttackRange))
                    .FirstOrDefault(
                        enemy => enemy.Buffs.Any(buff => buff.Name == "vaynesilvereddebuff" && buff.Count > 0));
            }
        }

        public static bool CanUseBotrk
        {
            get
            {
                if (Game.Time * 1000 >=
                    Variables.lastaa + Variables._Player.AttackDelay * 1000 - Variables._Player.AttackDelay * 1000 / 1.5 &&
                    Game.Time * 1000 <
                    Variables.lastaa + Variables._Player.AttackDelay * 1000 - Variables._Player.AttackDelay * 1000 / 1.7 &&
                    Game.Time * 1000 > Variables.lastaa + Variables._Player.AttackCastDelay * 1000)
                {
                    return true;
                }
                return false;
            }
        }

        public static void Botrk(Obj_AI_Base unit)
        {
            if (Item.HasItem(3144) && Item.CanUseItem(3144) && CanUseBotrk)
                Item.UseItem(3144, unit);
            if (Item.HasItem(3153) && Item.CanUseItem(3153) && CanUseBotrk)
                Item.UseItem(3153, unit);
        }
    }
}
