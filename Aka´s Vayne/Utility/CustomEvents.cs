using System;
using System.Collections.Generic;
using System.Linq;
using SharpDX;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using Dash = EloBuddy.SDK.Events.Dash;

namespace AddonTemplate.Utility
{
    public static class CustomEvents
    {
        public class Game
        {
            public delegate void OnGameEnded(EventArgs args);

            public delegate void OnGameLoaded(EventArgs args);

            private static readonly List<Delegate> NotifiedSubscribers = new List<Delegate>();
            private static readonly List<Obj_HQ> NexusList = new List<Obj_HQ>();
            private static bool _endGameCalled;

            static Game()
            {
                Utility2.DelayAction.Add(0, Initialize);
            }

            public static void Initialize()
            {

                foreach (var hq in ObjectManager.Get<Obj_HQ>().Where(hq => hq.IsValid))
                {
                    NexusList.Add(hq);
                }

                if (EloBuddy.Game.Mode == GameMode.Running)
                {
                    //Otherwise the .ctor didn't return yet and no callback will occur
                    Utility2.DelayAction.Add(500, () =>
                    {
                        Game_OnGameStart(new EventArgs());
                    });
                }
                else
                {
                    EloBuddy.Game.OnLoad += Game_OnGameStart;
                }
            }

            private static void Game_OnGameUpdate(EventArgs args)
            {
                if (OnGameLoad != null)
                {
                    foreach (var subscriber in OnGameLoad.GetInvocationList()
                        .Where(s => !NotifiedSubscribers.Contains(s)))
                    {
                        NotifiedSubscribers.Add(subscriber);
                        subscriber.DynamicInvoke(new EventArgs());
                    }
                }

                if (NexusList.Count == 0 || _endGameCalled)
                {
                    return;
                }

                foreach (var nexus in NexusList)
                {
                    if (nexus != null && nexus.IsValid && nexus.Health <= 0)
                    {
                        if (OnGameEnd != null)
                        {
                            OnGameEnd(new EventArgs());
                            _endGameCalled = true; // Don't spam the event.
                        }
                    }
                }
            }

            /// <summary>
            ///     OnGameLoad is getting called when you get ingame (doesn't matter if started or restarted while game is already
            ///     running) and when reloading an assembly
            /// </summary>
            public static event OnGameLoaded OnGameLoad;

            /// <summary>
            ///     OnGameEnd is getting called when the game ends. Same as Game.OnEnd but this one works :^).
            /// </summary>
            public static event OnGameEnded OnGameEnd;

            private static void Game_OnGameStart(EventArgs args)
            {
                EloBuddy.Game.OnUpdate += Game_OnGameUpdate;

                if (OnGameLoad != null)
                {
                    NotifiedSubscribers.AddRange(OnGameLoad.GetInvocationList());
                    OnGameLoad(new EventArgs());
                }
            }
        }

        public class Unit
        {
            public delegate void OnDashed(Obj_AI_Base sender, Dash.DashItem args);

            public delegate void OnLeveledUp(Obj_AI_Base sender, OnLevelUpEventArgs args);

            public delegate void OnLeveledUpSpell(Obj_AI_Base sender, OnLevelUpSpellEventArgs args);

            static Unit()
            {
                EloBuddy.Game.OnProcessPacket += PacketHandler;

                //Initializes ondash class:
                ObjectManager.Player.IsDashing();
            }

            /// <summary>
            ///     OnLevelUpSpell gets called after you leveled a spell
            /// </summary>
            //public static event OnLeveledUpSpell OnLevelUpSpell;

            private static void PacketHandler(GamePacketEventArgs args) { }

            /// <summary>
            ///     Gets called when a unit gets a level up
            /// </summary>
            //public static event OnLeveledUp OnLevelUp;

            /// <summary>
            ///     OnDash is getting called when a unit dashes.
            /// </summary>
            public static event OnDashed OnDash;

            public static void TriggerOnDash(Obj_AI_Base sender, Dash.DashItem args)
            {
                var dashHandler = OnDash;
                if (dashHandler != null)
                {
                    dashHandler(sender, args);
                }
            }

            public class OnLevelUpEventArgs : EventArgs
            {
                public int NewLevel;
                public int RemainingPoints;
            }

            public class OnLevelUpSpellEventArgs : EventArgs
            {
                public int Remainingpoints;
                public int SpellId;
                public int SpellLevel;
                internal OnLevelUpSpellEventArgs() { }
            }
        }
    }
}