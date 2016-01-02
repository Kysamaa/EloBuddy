using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;

namespace AkaYasuo
{
    internal class Program
    {

        public static Spell.Skillshot Q;
        public static Spell.Targeted E, Q3;
        public static Spell.Skillshot W;
        public static Spell.Active R;
        public static Spell.Targeted Ignite;
        public static Item Qss, Mercurial;

        public static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        public static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Variables._Player.Hero != Champion.Yasuo)
            {
                return;
            }

            Q = new Spell.Skillshot(SpellSlot.Q, 475, EloBuddy.SDK.Enumerations.SkillShotType.Linear, (int) 250f,
                (int) 8700f, (int) 15f);
            Q3 = new Spell.Targeted(SpellSlot.Q, 1000);
            W = new Spell.Skillshot(SpellSlot.W, 400, EloBuddy.SDK.Enumerations.SkillShotType.Cone);
            E = new Spell.Targeted(SpellSlot.E, 475);
            R = new Spell.Active(SpellSlot.R, 1200);

            var slot = Variables._Player.GetSpellSlotFromName("summonerdot");
            if (slot != SpellSlot.Unknown)
            {
                Ignite = new Spell.Targeted(slot, 600);
            }

            Qss = new Item((int) ItemId.Quicksilver_Sash);
            Mercurial = new Item((int) ItemId.Mercurial_Scimitar);

                Variables.abilitySequence = new int[] {1, 3, 2, 1, 1, 4, 1, 3, 1, 3, 4, 3, 3, 2, 2, 4, 2, 2};

            EventManager.load();
            MenuManager.Load();
        }
    }
}