using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace Aka_s_Vayne_reworked
{
    public class Activator
    {
        public static Menu AMenu, Offensive, Defensive, Summoners, Potions, Qss;

        public Activator(Menu attachToMenu)
        {
            AMenu = attachToMenu;

            AMenu = MainMenu.AddMenu("Aka´s Activator", "AkasActivator");
            AMenu.Add("Combo", new CheckBox("Use Items in Combo"));
            AMenu.Add("Harass", new CheckBox("Use Items in Harass(wip)"));
            AMenu.Add("LaneClear", new CheckBox("Use Items in LaneClear(wip)"));
            AMenu.Add("JungleClear", new CheckBox("Use Items in JungleClear(wip)"));
            AMenu.Add("Flee", new CheckBox("Use Items in Flee(wip)"));
            Offensive = AMenu.AddSubMenu("Offensive", "Offensive");
            Offensive.AddGroupLabel("Offensive Items");
            Offensive.AddLabel("Blade Of The Ruined King");
            Offensive.Add("botrkManager", new CheckBox("Blade Of The Ruined King"));
            Offensive.Add("botrkManagerMinMeHP", new Slider("Self HP %", 80));
            Offensive.Add("botrkManagerMinEnemyHP", new Slider("Enemy HP HP %", 80));
            Offensive.AddLabel("Cutlass");
            Offensive.Add("cutlassManager", new CheckBox("Cutlass"));
            Offensive.Add("cutlassManagerMinMeHP", new Slider("Self HP %", 80));
            Offensive.Add("cutlassManagerMinEnemyHP", new Slider("Enemy HP HP %", 80));

            if (Player.Instance.IsMelee)
            {
                Offensive.AddLabel("Tiamat");
                Offensive.Add("tiamatManager", new CheckBox("Use Tiamat"));
                Offensive.Add("tiamatManagerMinMeHP", new Slider("Self HP %", 99));
                Offensive.Add("tiamatManagerMinEnemyHP", new Slider("Enemy HP HP %", 99));
                Offensive.AddLabel("Hydra");
                Offensive.Add("hydraManager", new CheckBox("Use Hydra"));
                Offensive.Add("hydraManagerMinMeHP", new Slider("Self HP %", 99));
                Offensive.Add("hydraManagerMinEnemyHP", new Slider("Enemy HP HP %", 99));
            }

            Offensive.AddLabel("Gunblade");
            Offensive.Add("gunbladeManager", new CheckBox("Use Gunblade"));
            Offensive.Add("gunbladeManagerMinMeHP", new Slider("Self HP %", 99));
            Offensive.Add("gunbladeManagerMinEnemyHP", new Slider("Enemy HP HP %", 99));
            Offensive.AddLabel("GhostBlade");
            Offensive.Add("ghostbladeManager", new CheckBox("Use GhostBlade"));
            Offensive.Add("ghostbladeManagerMinMeHP", new Slider("Self HP %", 99));
            Offensive.Add("ghostbladeManagerMinEnemyHP", new Slider("Enemy HP HP %", 99));

            Potions = AMenu.AddSubMenu("Potions", "potions");
            Potions.AddGroupLabel("Potion Items");
            Potions.Add("healthPotionManager", new CheckBox("Health Potion"));
            Potions.Add("healthPotionManagerMinMeHP", new Slider("Min HP %", 65));
            Potions.AddSeparator();
            Potions.Add("biscuitPotionManager", new CheckBox("Biscuit"));
            Potions.Add("biscuitPotionManagerMinMeHP", new Slider("Min HP %", 65));
            Potions.AddSeparator();
            Potions.Add("refillPotManager", new CheckBox("Refill Potion"));
            Potions.Add("refillPotManagerMinMeHP", new Slider("Min HP %", 60));
            Potions.AddSeparator();
            Potions.Add("corruptpotManager", new CheckBox("Corrupt Potion"));
            Potions.Add("corruptpotManagerMinMeHP", new Slider("Min HP %", 60));
            Potions.Add("corruptpotManagerMinMeMana", new Slider("Min Mana %", 30));
            Potions.AddSeparator();
            Potions.Add("huntersPotManager", new CheckBox("Hunter's Potion"));
            Potions.Add("huntersPotManagerMinMeHP", new Slider("Min HP %", 60));
            Potions.Add("huntersPotManagerMinMeMana", new Slider("Min Mana %", 30));

            Qss = AMenu.AddSubMenu("Qss", "qss");
            Qss.AddGroupLabel("Qss Settings");
            Qss.Add("Polymorph", new CheckBox("Polymorph"));
            Qss.Add("Stun", new CheckBox("Stun"));
            Qss.Add("Taunt", new CheckBox("Taunt"));
            Qss.Add("Knockup", new CheckBox("Knock-up"));
            Qss.Add("Fear", new CheckBox("Fear"));
            Qss.Add("Snare", new CheckBox("Snare"));
            Qss.Add("Slow", new CheckBox("Slow"));
            Qss.Add("Blind", new CheckBox("Blind"));
            Qss.Add("Silence", new CheckBox("Silence"));
            Qss.Add("Charm", new CheckBox("Charm"));
            Qss.Add("Suppression", new CheckBox("Suppression"));
            Qss.Add("delay", new Slider("Activation Delay", 1000, 0, 2000));
            Qss.AddSeparator();
            Qss.AddLabel("Cleanse Items / Summoner Spell");
            Qss.Add("mikaelsCleanser", new CheckBox("Mikael's Cruicble"));
            Qss.Add("mercurialScimitarCleanser", new CheckBox("Mercurial Scimitar"));
            Qss.Add("quicksilverSashCleanser", new CheckBox("Quicksilver Sash"));
            Qss.Add("summonerSpellCleanse", new CheckBox("Summoner Cleanse"));

            Defensive = AMenu.AddSubMenu("Defensive Items(wip)", "defmenuactiv");

            Defensive.AddGroupLabel("Shield/Heal Items (self)");
            Defensive.Add("Archangels_Staff", new CheckBox("Serahph's Embrace"));

            Defensive.AddGroupLabel("Shield/Heal Items (ally/self)");
            Defensive.Add("Mikaels_Crucible_Heal", new CheckBox("Mikaels Crucible"));
            Defensive.AddLabel("Locket of the Iron Solari");
            Defensive.Add("Locket_of_the_Iron_Solari", new CheckBox("Locket of the Iron Solari"));
            Defensive.AddSeparator(0);
            Defensive.Add("Locket_of_the_Iron_Solari_ally", new CheckBox("Ally"));
            Defensive.Add("Locket_of_the_Iron_Solari_self", new CheckBox("Self"));
            Defensive.AddLabel("Face of the Mountain");
            Defensive.Add("Face_of_the_Mountain", new CheckBox("Face of the Mountain"));
            Defensive.AddSeparator(0);
            Defensive.Add("Face_of_the_Mountain_ally", new CheckBox("Ally"));
            Defensive.Add("Face_of_the_Mountain_self", new CheckBox("Self"));

            Game.OnUpdate += GameOnOnUpdate;
            Obj_AI_Base.OnBuffGain += Obj_AI_Base_OnBuffGain;
        }


        private static void GameOnOnUpdate(EventArgs args)
        {
            AkaActivator.LoadSpells();
            AutoPotions();

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
            {
                //Mode.Harass();
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
            {
                //Mode.JungleClear();
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
            {
                //Mode.Flee();
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                Combo();
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                //Mode.LaneClear();
            }
        }

        private static void Obj_AI_Base_OnBuffGain(Obj_AI_Base sender, Obj_AI_BaseBuffGainEventArgs buff)
        {
            if (!sender.IsMe) return;

            if (buff.Buff.Type == BuffType.Taunt && Qss["Taunt"].Cast<CheckBox>().CurrentValue)
            {
                DoQSS();
            }
            if (buff.Buff.Type == BuffType.Stun && Qss["Stun"].Cast<CheckBox>().CurrentValue)
            {
                DoQSS();
            }
            if (buff.Buff.Type == BuffType.Snare && Qss["Snare"].Cast<CheckBox>().CurrentValue)
            {
                DoQSS();
            }
            if (buff.Buff.Type == BuffType.Polymorph && Qss["Polymorph"].Cast<CheckBox>().CurrentValue)
            {
                DoQSS();
            }
            if (buff.Buff.Type == BuffType.Blind && Qss["Blind"].Cast<CheckBox>().CurrentValue)
            {
                DoQSS();
            }
            if (buff.Buff.Type == BuffType.Flee && Qss["Fear"].Cast<CheckBox>().CurrentValue)
            {
                DoQSS();
            }
            if (buff.Buff.Type == BuffType.Charm && Qss["Charm"].Cast<CheckBox>().CurrentValue)
            {
                DoQSS();
            }
            if (buff.Buff.Type == BuffType.Suppression && Qss["Suppression"].Cast<CheckBox>().CurrentValue)
            {
                DoQSS();
            }
            if (buff.Buff.Type == BuffType.Silence && Qss["Silence"].Cast<CheckBox>().CurrentValue)
            {
                DoQSS();
            }
            if (buff.Buff.Type == BuffType.Silence && Qss["Knockup"].Cast<CheckBox>().CurrentValue)
            {
                DoQSS();
            }
            if (buff.Buff.Type == BuffType.Silence && Qss["Slow"].Cast<CheckBox>().CurrentValue)
            {
                DoQSS();
            }
            if (buff.Buff.Name == "zedulttargetmark")
            {
                UltQSS();
            }
            if (buff.Buff.Name == "VladimirHemoplague")
            {
                UltQSS();
            }
            if (buff.Buff.Name == "FizzMarinerDoom")
            {
                UltQSS();
            }
            if (buff.Buff.Name == "MordekaiserChildrenOfTheGrave")
            {
                UltQSS();
            }
            if (buff.Buff.Name == "PoppyDiplomaticImmunity")
            {
                UltQSS();
            }
        }

        private static void DoQSS()
        {
            var delay = Qss["delay"].Cast<Slider>().CurrentValue;
            if (AkaActivator.Qss.IsOwned() && AkaActivator.Qss.IsReady())
            {
                Core.DelayAction(() => { AkaActivator.Qss.Cast(); }, delay);
            }

            if (AkaActivator.Mercurial.IsOwned() && AkaActivator.Mercurial.IsReady())
            {
                Core.DelayAction(() => { AkaActivator.Mercurial.Cast(); }, delay);
            }
            if (AkaActivator.Cleanse.IsReady())
            {
                Core.DelayAction(() => { AkaActivator.Cleanse.Cast(); }, delay);
            }
        }

        private static void UltQSS()
        {
            if (AkaActivator.Qss.IsOwned() && AkaActivator.Qss.IsReady())
            {
                Core.DelayAction(() => { AkaActivator.Qss.Cast(); }, 1000);
            }

            if (AkaActivator.Mercurial.IsOwned() && AkaActivator.Mercurial.IsReady())
            {
                Core.DelayAction(() => { AkaActivator.Mercurial.Cast(); }, 1000);
            }
            if (AkaActivator.Cleanse.IsReady())
            {
                Core.DelayAction(() => { AkaActivator.Cleanse.Cast(); }, 1000);
            }
        }

        private static void Combo()
        {
            var target = TargetSelector.GetTarget(1000, DamageType.Physical);
            if (target == null)
            {
                return;

            }
            if (Offensive["botrkManager"].Cast<CheckBox>().CurrentValue &&
                Player.Instance.HealthPercent >= Offensive["botrkManagerMinMeHP"].Cast<Slider>().CurrentValue &&
                target.HealthPercent <= Offensive["botrkManagerMinEnemyHP"].Cast<Slider>().CurrentValue)
            {
                AkaActivator.Botrk.Cast(target);
            }

            if (Offensive["gunbladeManager"].Cast<CheckBox>().CurrentValue &&
    Player.Instance.HealthPercent >= Offensive["gunbladeManagerMinMeHP"].Cast<Slider>().CurrentValue &&
    target.HealthPercent <= Offensive["gunbladeManagerMinEnemyHP"].Cast<Slider>().CurrentValue)
            {
                AkaActivator.GunBlade.Cast(target);
            }

            if (Offensive["cutlassManager"].Cast<CheckBox>().CurrentValue &&
    Player.Instance.HealthPercent >= Offensive["cutlassManagerMinMeHP"].Cast<Slider>().CurrentValue &&
    target.HealthPercent <= Offensive["cutlassManagerMinEnemyHP"].Cast<Slider>().CurrentValue)
            {
                AkaActivator.Bilgewater.Cast(target);
            }

            if (Offensive["ghostbladeManager"].Cast<CheckBox>().CurrentValue &&
Player.Instance.HealthPercent >= Offensive["ghostbladeManagerMinMeHP"].Cast<Slider>().CurrentValue &&
target.HealthPercent <= Offensive["ghostbladeManagerMinEnemyHP"].Cast<Slider>().CurrentValue)
            {
                AkaActivator.Youmus.Cast();
            }
            if (Offensive["tiamatManager"].Cast<CheckBox>().CurrentValue &&
Player.Instance.HealthPercent >= Offensive["tiamatManagerMinMeHP"].Cast<Slider>().CurrentValue &&
target.HealthPercent <= Offensive["tiamatManagerMinEnemyHP"].Cast<Slider>().CurrentValue)
            {
                AkaActivator.Tiamat.Cast();
            }
            if (Offensive["hydraManager"].Cast<CheckBox>().CurrentValue &&
Player.Instance.HealthPercent >= Offensive["hydraManagerMinMeHP"].Cast<Slider>().CurrentValue &&
target.HealthPercent <= Offensive["hydraMinEnemyHP"].Cast<Slider>().CurrentValue)
            {
                AkaActivator.Hydra.Cast();
            }


        }

        private static void AutoPotions()
        {
            if (!Player.Instance.IsInShopRange() &&
                !(Player.HasBuff("RegenerationPotion") || Player.HasBuff("ItemCrystalFlaskJungle") ||
                  Player.HasBuff("ItemMiniRegenPotion") || Player.HasBuff("ItemCrystalFlask") ||
                  Player.HasBuff("ItemDarkCrystalFlask")))
            {
                if (Potions["huntersPotManager"].Cast<CheckBox>().CurrentValue && Player.Instance.HealthPercent < Potions["huntersPotManagerMinMeHP"].Cast<Slider>().CurrentValue &&  Player.Instance.ManaPercent < Potions["huntersPotManagerMinMeMana"].Cast<Slider>().CurrentValue && AkaActivator.HuntersPot.IsReady() && AkaActivator.HuntersPot.IsOwned())
                {
                    AkaActivator.HuntersPot.Cast();
                }
                if (Potions["biscuitPotionManager"].Cast<CheckBox>().CurrentValue && Player.Instance.HealthPercent < Potions["biscuitPotionManagerMinMeHP"].Cast<Slider>().CurrentValue && AkaActivator.Biscuit.IsReady() && AkaActivator.Biscuit.IsOwned())
                {
                    AkaActivator.Biscuit.Cast();
                }
                if (Potions["healthPotionManager"].Cast<CheckBox>().CurrentValue && Player.Instance.HealthPercent < Potions["healthPotionManagerMinMeHP"].Cast<Slider>().CurrentValue && AkaActivator.HPPot.IsReady() && AkaActivator.HPPot.IsOwned())
                {
                    AkaActivator.HPPot.Cast();
                }
                if (Potions["refillPotManager"].Cast<CheckBox>().CurrentValue && Player.Instance.HealthPercent < Potions["refillPotManagerMinMeHP"].Cast<Slider>().CurrentValue && AkaActivator.RefillPot.IsReady() && AkaActivator.RefillPot.IsOwned())
                {
                    AkaActivator.RefillPot.Cast();
                }
                if (Potions["corruptpotManager"].Cast<CheckBox>().CurrentValue && Player.Instance.HealthPercent < Potions["corruptpotManagerMinMeHP"].Cast<Slider>().CurrentValue && Player.Instance.ManaPercent < Potions["corruptpotManagerMinMeMana"].Cast<Slider>().CurrentValue && AkaActivator.CorruptPot.IsReady() && AkaActivator.CorruptPot.IsOwned())
                { 
                    AkaActivator.CorruptPot.Cast();
                }
            }
        }
    }
}
