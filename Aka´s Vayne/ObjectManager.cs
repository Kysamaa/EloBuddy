using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using SharpDX;

// ReSharper disable InconsistentNaming

namespace AddonTemplate
{
    public static class Traps
    {
        private static List<GameObject> _traps;

        private static readonly List<string> _trapNames = new List<string> { "teemo", "shroom", "trap", "mine", "ziggse_red" };

        public static List<GameObject> EnemyTraps
        {
            get { return _traps.FindAll(t => t.IsValid && t.IsEnemy); }
        }

        public static void OnCreate(GameObject sender, EventArgs args)
        {
            foreach (var trapName in _trapNames)
            {
                if (sender.Name.ToLower().Contains(trapName)) _traps.Add(sender);
            }
        }

        public static void OnDelete(GameObject sender, EventArgs args)
        {
            _traps.RemoveAll(trap => trap.NetworkId == sender.NetworkId);
        }

        public static void Load()
        {
            _traps = new List<GameObject>();
            GameObject.OnCreate += OnCreate;
            GameObject.OnDelete += OnDelete;
        }
    }

    public static class Turrets
    {
        private static List<Obj_AI_Turret> _turrets;

        public static List<Obj_AI_Turret> AllyTurrets
        {
            get { return _turrets.FindAll(t => t.IsAlly); }
        }
        public static List<Obj_AI_Turret> EnemyTurrets
        {
            get { return _turrets.FindAll(t => t.IsEnemy); }
        }

        public static void Load()
        {
            _turrets = ObjectManager.Get<Obj_AI_Turret>().ToList();
            Obj_AI_Turret.OnCreate += OnCreate;
            Obj_AI_Turret.OnDelete += OnDelete;
        }

        private static void OnCreate(GameObject sender, EventArgs args)
        {
            if (sender is Obj_AI_Turret)
            {
                _turrets.Add((Obj_AI_Turret)sender);
            }
        }

        private static void OnDelete(GameObject sender, EventArgs args)
        {
            _turrets.RemoveAll(turret => turret.NetworkId == sender.NetworkId);
        }
    }

    public static class HeadQuarters
    {
        private static List<Obj_HQ> _headQuarters;

        public static Obj_HQ AllyHQ
        {
            get { return _headQuarters.FirstOrDefault(t => t.IsAlly); }
        }
        public static Obj_HQ EnemyHQ
        {
            get { return _headQuarters.FirstOrDefault(t => t.IsEnemy); }
        }

        public static void Load()
        {
            _headQuarters = ObjectManager.Get<Obj_HQ>().ToList();
        }
    }

    public static class Heroes
    {
        private static List<AIHeroClient> _heroes;

        public static AIHeroClient Player = ObjectManager.Player;

        public static List<AIHeroClient> AllyHeroes
        {
            get { return _heroes.FindAll(h => h.IsAlly); }
        }

        public static List<AIHeroClient> EnemyHeroes
        {
            get { return _heroes.FindAll(h => h.IsEnemy); }
        }

        public static void Load()
        {
            Player = ObjectManager.Player;
            _heroes = ObjectManager.Get<AIHeroClient>().ToList();
        }
    }

    public static class Cache
    {
        public static void Load()
        {
            Traps.Load();
            Turrets.Load();
            HeadQuarters.Load();
            Heroes.Load();
        }
    }
}
