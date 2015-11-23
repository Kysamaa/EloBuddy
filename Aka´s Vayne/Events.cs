using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;
using SharpDX;
using Settings = AddonTemplate.Config.Modes.MiscMenu;

namespace AddonTemplate
{
    class Events
    {
        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }

        public static void Gapcloser_OnGapCloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (!Settings.Gapclose) return;
            if (e.End.Distance(_Player) < 200 && sender.IsValidTarget())
            {
                SpellManager.E.Cast(sender);
            }

        }

        public static void GameObject_OnCreate(GameObject sender, EventArgs args)
        {
            var rengar = EntityManager.Heroes.Enemies.FirstOrDefault(a => a.Hero == Champion.Rengar);
            if (Settings.AntiRengar && sender.Name == "Rengar_LeapSound.troy" &&
                ObjectManager.Player.Distance(Player.Instance.Position) <= SpellManager.E.Range && rengar != null)
            {
                SpellManager.E.Cast(rengar);
                Console.WriteLine("fuck rengar");
            }
        }

        public static void ObjAiBaseOnOnBasicAttack(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!(sender is AIHeroClient)) return;
            var target = (AIHeroClient) sender;
            if (Settings.AntiKalista && target.IsEnemy && target.Hero == Champion.Kalista && SpellManager.Q.IsReady())
            {
                var pos = (_Player.Position.Extend(Game.CursorPos, 300).Distance(target) <=
                           _Player.GetAutoAttackRange(target) &&
                           _Player.Position.Extend(Game.CursorPos, 300).Distance(target) > 100
                    ? Game.CursorPos
                    : (_Player.Position.Extend(target.Position, 300).Distance(target) < 100)
                        ? target.Position
                        : new Vector3());

                if (pos.IsValid())
                {
                    Player.CastSpell(SpellSlot.Q, pos);
                }
            }
        }

        public static void Interrupter_OnInterruptableSpell(Obj_AI_Base sender,
            Interrupter.InterruptableSpellEventArgs e)
        {
            if (!Settings.InterruptE) return;
            var dangerLevel =
                new[]
                {
                    DangerLevel.Low, DangerLevel.Medium,
                    DangerLevel.High,
                }[Settings.Dangerlvl - 1];

            if (dangerLevel == DangerLevel.Medium && e.DangerLevel.HasFlag(DangerLevel.High) ||
                dangerLevel == DangerLevel.Low && e.DangerLevel.HasFlag(DangerLevel.High) &&
                e.DangerLevel.HasFlag(DangerLevel.Medium))
                return;

            if (e.Sender.IsValidTarget())
            {
                SpellManager.E.Cast(e.Sender);
            }
        }
   
    }
}
