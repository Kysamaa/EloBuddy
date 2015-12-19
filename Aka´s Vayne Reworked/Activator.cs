using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public static Menu AMenu, Offensive, Defensive, Summoners, Potions, Qss, Sums, SmiteMenu;
        public static Spell.Targeted Smite;

        public Activator(Menu attachToMenu)
        {
            AMenu = attachToMenu;

            AMenu = MainMenu.AddMenu("Aka´s Activator", "AkasActivator");
            AMenu.Add("Combo", new CheckBox("Use Items in Combo"));
            AMenu.Add("Harass", new CheckBox("Use Items in Harass"));
            AMenu.Add("LaneClear", new CheckBox("Use Items in LaneClear"));
            AMenu.Add("JungleClear", new CheckBox("Use Items in JungleClear"));
            AMenu.Add("Flee", new CheckBox("Use Items in Flee"));
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

            Defensive = AMenu.AddSubMenu("Defensive Items", "defmenuactiv");
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

            Sums = AMenu.AddSubMenu("Summoners", "sums");
            Sums.AddLabel("Heal");
            Sums.Add("healManager", new CheckBox("Use Gunblade"));
            Sums.Add("healManagerMinMeHP", new Slider("Self HP %", 30));
            Sums.Add("healManagerMinEnemyHP", new Slider("Enemy HP HP %", 30));
            Sums.AddLabel("Ignite");
            Sums.Add("igniteManager", new CheckBox("Use GhostBlade"));
            Sums.AddLabel("Barrier");
            Sums.Add("barrierManager", new CheckBox("Use GhostBlade"));
            Sums.Add("barrierManagerMinMeHP", new Slider("Self HP %", 30));
            Sums.Add("barrierManagerMinEnemyHP", new Slider("Enemy HP HP %", 30));

            SmiteMenu = AMenu.AddSubMenu("Smite Settings");
            SmiteMenu.AddGroupLabel("Camps");
            SmiteMenu.AddLabel("Epics");
            SmiteMenu.Add("SRU_Baron", new CheckBox("Baron"));
            SmiteMenu.Add("SRU_Dragon", new CheckBox("Dragon"));
            SmiteMenu.AddLabel("Buffs");
            SmiteMenu.Add("SRU_Blue", new CheckBox("Blue"));
            SmiteMenu.Add("SRU_Red", new CheckBox("Red"));
            SmiteMenu.AddLabel("Small Camps");
            SmiteMenu.Add("SRU_Gromp", new CheckBox("Gromp", false));
            SmiteMenu.Add("SRU_Murkwolf", new CheckBox("Murkwolf", false));
            SmiteMenu.Add("SRU_Krug", new CheckBox("Krug", false));
            SmiteMenu.Add("SRU_Razorbeak", new CheckBox("Razerbeak", false));
            SmiteMenu.Add("Sru_Crab", new CheckBox("Skuttles", false));
            SmiteMenu.AddSeparator();
            SmiteMenu.Add("smiteActive",
                new KeyBind("Smite Active (toggle)", true, KeyBind.BindTypes.PressToggle, 'M'));
            SmiteMenu.AddSeparator();
            SmiteMenu.Add("useSlowSmite", new CheckBox("KS with Slow Smite"));
            SmiteMenu.Add("comboWithDuelSmite", new CheckBox("Combo With Duel Smite"));

            Smite = new Spell.Targeted(ObjectManager.Player.GetSpellSlotFromName("summonersmite"), 500);
            Game.OnUpdate += GameOnOnUpdate;
            Obj_AI_Base.OnBuffGain += Obj_AI_Base_OnBuffGain;
        }


        private static void GameOnOnUpdate(EventArgs args)
        {
            AkaActivator.LoadSpells();
            AutoPotions();
            Sams();
            foreach (
                var ally in EntityManager.Heroes.Allies.Where(a => !a.IsDead))
            {
                if (
                    ally.HealthPercent <= 15 && ally.CountEnemiesInRange(850) >= 2
                    )
                {
                    yelpallys(ally);
                }
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
            {
                Harass();
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
            {
                JungleClear();
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
            {
                Flee();
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                Combo();
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                LaneClear();
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
            if (Offensive["botrkManager"].Cast<CheckBox>().CurrentValue && Player.Instance.ServerPosition.Distance(target) <= 550 && AkaActivator.Botrk.IsReady() &&
                Player.Instance.HealthPercent >= Offensive["botrkManagerMinMeHP"].Cast<Slider>().CurrentValue &&
                target.HealthPercent <= Offensive["botrkManagerMinEnemyHP"].Cast<Slider>().CurrentValue)
            {
                AkaActivator.Botrk.Cast(target);
            }

            if (Offensive["gunbladeManager"].Cast<CheckBox>().CurrentValue && Player.Instance.ServerPosition.Distance(target) <= 700 && AkaActivator.GunBlade.IsReady() &&
    Player.Instance.HealthPercent >= Offensive["gunbladeManagerMinMeHP"].Cast<Slider>().CurrentValue &&
    target.HealthPercent <= Offensive["gunbladeManagerMinEnemyHP"].Cast<Slider>().CurrentValue)
            {
                AkaActivator.GunBlade.Cast(target);
            }

            if (Offensive["cutlassManager"].Cast<CheckBox>().CurrentValue && Player.Instance.ServerPosition.Distance(target) <= 550 && AkaActivator.Bilgewater.IsReady() &&
    Player.Instance.HealthPercent >= Offensive["cutlassManagerMinMeHP"].Cast<Slider>().CurrentValue &&
    target.HealthPercent <= Offensive["cutlassManagerMinEnemyHP"].Cast<Slider>().CurrentValue)
            {
                AkaActivator.Bilgewater.Cast(target);
            }

            if (Offensive["ghostbladeManager"].Cast<CheckBox>().CurrentValue && 
Player.Instance.HealthPercent >= Offensive["ghostbladeManagerMinMeHP"].Cast<Slider>().CurrentValue && AkaActivator.Youmus.IsReady() &&
target.HealthPercent <= Offensive["ghostbladeManagerMinEnemyHP"].Cast<Slider>().CurrentValue)
            {
                AkaActivator.Youmus.Cast();
            }
            if (Offensive["tiamatManager"].Cast<CheckBox>().CurrentValue && Player.Instance.ServerPosition.Distance(target) <= 400 && AkaActivator.Tiamat.IsReady() &&
Player.Instance.HealthPercent >= Offensive["tiamatManagerMinMeHP"].Cast<Slider>().CurrentValue &&
target.HealthPercent <= Offensive["tiamatManagerMinEnemyHP"].Cast<Slider>().CurrentValue)
            {
                AkaActivator.Tiamat.Cast();
            }
            if (Offensive["hydraManager"].Cast<CheckBox>().CurrentValue && Player.Instance.ServerPosition.Distance(target) <= 400 && AkaActivator.Hydra.IsReady() &&
Player.Instance.HealthPercent >= Offensive["hydraManagerMinMeHP"].Cast<Slider>().CurrentValue &&
target.HealthPercent <= Offensive["hydraMinEnemyHP"].Cast<Slider>().CurrentValue)
            {
                AkaActivator.Hydra.Cast();
            }


        }

        private static void Sams()
        {
            var target = TargetSelector.GetTarget(1000, DamageType.Physical);
            if (target == null)
            {
                return;

            }

            if (Sums["healManager"].Cast<CheckBox>().CurrentValue && AkaActivator.Heal.IsReady() &&
                Player.Instance.HealthPercent >= Sums["healManagerMinMeHP"].Cast<Slider>().CurrentValue &&
                target.HealthPercent <= Sums["healManagerMinEnemyHP"].Cast<Slider>().CurrentValue)
            {
                AkaActivator.Heal.Cast();
            }
            if (Sums["barrierManager"].Cast<CheckBox>().CurrentValue && AkaActivator.Barrier.IsReady() &&
                Player.Instance.HealthPercent >= Sums["barrierManagerMinMeHP"].Cast<Slider>().CurrentValue &&
                target.HealthPercent <= Sums["barrierManagerMinEnemyHP"].Cast<Slider>().CurrentValue)
            {
                AkaActivator.Barrier.Cast();
            }
            if (Sums["igniteManager"].Cast<CheckBox>().CurrentValue && AkaActivator.Ignite.IsReady())
            {
                foreach (
                    var source in
                        EntityManager.Heroes.Enemies
                            .Where(
                                a => a.IsValidTarget(AkaActivator.Ignite.Range) &&
                                     a.Health < 50 + 20*Player.Instance.Level - (a.HPRegenRate/5*3)))
                {
                    AkaActivator.Ignite.Cast(source);
                }
            }
        }

        private static void AutoPotions()
        {
            if (!Player.Instance.IsInShopRange() &&
                !(Player.HasBuff("RegenerationPotion") || Player.HasBuff("ItemCrystalFlaskJungle") ||
                  Player.HasBuff("ItemMiniRegenPotion") || Player.HasBuff("ItemCrystalFlask") ||
                  Player.HasBuff("ItemDarkCrystalFlask")))
            {
                if (Potions["huntersPotManager"].Cast<CheckBox>().CurrentValue &&
                    Player.Instance.HealthPercent < Potions["huntersPotManagerMinMeHP"].Cast<Slider>().CurrentValue &&
                    Player.Instance.ManaPercent < Potions["huntersPotManagerMinMeMana"].Cast<Slider>().CurrentValue &&
                    AkaActivator.HuntersPot.IsReady() && AkaActivator.HuntersPot.IsOwned())
                {
                    AkaActivator.HuntersPot.Cast();
                }
                if (Potions["biscuitPotionManager"].Cast<CheckBox>().CurrentValue &&
                    Player.Instance.HealthPercent < Potions["biscuitPotionManagerMinMeHP"].Cast<Slider>().CurrentValue &&
                    AkaActivator.Biscuit.IsReady() && AkaActivator.Biscuit.IsOwned())
                {
                    AkaActivator.Biscuit.Cast();
                }
                if (Potions["healthPotionManager"].Cast<CheckBox>().CurrentValue &&
                    Player.Instance.HealthPercent < Potions["healthPotionManagerMinMeHP"].Cast<Slider>().CurrentValue &&
                    AkaActivator.HPPot.IsReady() && AkaActivator.HPPot.IsOwned())
                {
                    AkaActivator.HPPot.Cast();
                }
                if (Potions["refillPotManager"].Cast<CheckBox>().CurrentValue &&
                    Player.Instance.HealthPercent < Potions["refillPotManagerMinMeHP"].Cast<Slider>().CurrentValue &&
                    AkaActivator.RefillPot.IsReady() && AkaActivator.RefillPot.IsOwned())
                {
                    AkaActivator.RefillPot.Cast();
                }
                if (Potions["corruptpotManager"].Cast<CheckBox>().CurrentValue &&
                    Player.Instance.HealthPercent < Potions["corruptpotManagerMinMeHP"].Cast<Slider>().CurrentValue &&
                    Player.Instance.ManaPercent < Potions["corruptpotManagerMinMeMana"].Cast<Slider>().CurrentValue &&
                    AkaActivator.CorruptPot.IsReady() && AkaActivator.CorruptPot.IsOwned())
                {
                    AkaActivator.CorruptPot.Cast();
                }
            }
        }

        private static void yelpallys(AIHeroClient ally)
        {
            if (ally == null || ally.IsDead || ally.Health <= 0 || !ally.IsValid) return;

            if (ally.IsMe && Defensive["Archangels_Staff"].Cast<CheckBox>().CurrentValue) 
            {
                if (AkaActivator.Archangles.IsReady() && AkaActivator.Archangles.IsOwned())
                {
                    AkaActivator.Archangles.Cast();
                }
            }
            if (Defensive["Face_of_the_Mountain"].Cast<CheckBox>().CurrentValue && (ally.IsMe && Defensive["Face_of_the_Mountain_self"].Cast<CheckBox>().CurrentValue || !ally.IsMe) && ally.Distance(Player.Instance) < 700) 
            {
                if (AkaActivator.Mountain.IsReady() && AkaActivator.Mountain.IsOwned())
                {
                    AkaActivator.Mountain.Cast(ally);
                }
            }
            if (Defensive["Locket_of_the_Iron_Solari"].Cast<CheckBox>().CurrentValue && (ally.IsMe && Defensive["Locket_of_the_Iron_Solari_self"].Cast<CheckBox>().CurrentValue || !ally.IsMe && Defensive["Locket_of_the_Iron_Solari_ally"].Cast<CheckBox>().CurrentValue) && ally.Distance(Player.Instance) < 600)
            {
                if (AkaActivator.Solari.IsReady() && AkaActivator.Solari.IsOwned())
                {
                    AkaActivator.Solari.Cast();
                }
            }
            if (Defensive["Mikaels_Crucible_Heal"].Cast<CheckBox>().CurrentValue && ally.Distance(Player.Instance) < 750)
            {
                if (AkaActivator.Solari.IsReady() && AkaActivator.Solari.IsOwned())
                {
                    AkaActivator.Mikeals.Cast(ally);

                }
            }

        }

        private static void Harass()
        {
            var target = TargetSelector.GetTarget(1000, DamageType.Physical);
            if (target == null)
            {
                return;

            }
            if (Offensive["botrkManager"].Cast<CheckBox>().CurrentValue && Player.Instance.ServerPosition.Distance(target) <= 550 && AkaActivator.Botrk.IsReady() &&
                Player.Instance.HealthPercent >= Offensive["botrkManagerMinMeHP"].Cast<Slider>().CurrentValue &&
                target.HealthPercent <= Offensive["botrkManagerMinEnemyHP"].Cast<Slider>().CurrentValue)
            {
                AkaActivator.Botrk.Cast(target);
            }

            if (Offensive["gunbladeManager"].Cast<CheckBox>().CurrentValue && Player.Instance.ServerPosition.Distance(target) <= 700 && AkaActivator.GunBlade.IsReady() &&
    Player.Instance.HealthPercent >= Offensive["gunbladeManagerMinMeHP"].Cast<Slider>().CurrentValue &&
    target.HealthPercent <= Offensive["gunbladeManagerMinEnemyHP"].Cast<Slider>().CurrentValue)
            {
                AkaActivator.GunBlade.Cast(target);
            }

            if (Offensive["cutlassManager"].Cast<CheckBox>().CurrentValue && Player.Instance.ServerPosition.Distance(target) <= 550 && AkaActivator.Bilgewater.IsReady() &&
    Player.Instance.HealthPercent >= Offensive["cutlassManagerMinMeHP"].Cast<Slider>().CurrentValue &&
    target.HealthPercent <= Offensive["cutlassManagerMinEnemyHP"].Cast<Slider>().CurrentValue)
            {
                AkaActivator.Bilgewater.Cast(target);
            }

            if (Offensive["ghostbladeManager"].Cast<CheckBox>().CurrentValue && AkaActivator.Youmus.IsReady() &&
Player.Instance.HealthPercent >= Offensive["ghostbladeManagerMinMeHP"].Cast<Slider>().CurrentValue &&
target.HealthPercent <= Offensive["ghostbladeManagerMinEnemyHP"].Cast<Slider>().CurrentValue)
            {
                AkaActivator.Youmus.Cast();
            }
            if (Offensive["tiamatManager"].Cast<CheckBox>().CurrentValue && Player.Instance.ServerPosition.Distance(target) <= 400 && AkaActivator.Tiamat.IsReady() &&
Player.Instance.HealthPercent >= Offensive["tiamatManagerMinMeHP"].Cast<Slider>().CurrentValue &&
target.HealthPercent <= Offensive["tiamatManagerMinEnemyHP"].Cast<Slider>().CurrentValue)
            {
                AkaActivator.Tiamat.Cast();
            }
            if (Offensive["hydraManager"].Cast<CheckBox>().CurrentValue && Player.Instance.ServerPosition.Distance(target) <= 400 && AkaActivator.Hydra.IsReady() &&
Player.Instance.HealthPercent >= Offensive["hydraManagerMinMeHP"].Cast<Slider>().CurrentValue &&
target.HealthPercent <= Offensive["hydraMinEnemyHP"].Cast<Slider>().CurrentValue)
            {
                AkaActivator.Hydra.Cast();
            }


        }

        private static void LaneClear()
        {
            var minions =
                EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.Instance.Position,
                    500).Where(
                        m => !m.IsDead && m.IsValid && !m.IsInvulnerable);
            foreach (var target in minions)
            {
                if (Offensive["ghostbladeManager"].Cast<CheckBox>().CurrentValue && AkaActivator.Youmus.IsReady() &&
                    Player.Instance.HealthPercent >= Offensive["ghostbladeManagerMinMeHP"].Cast<Slider>().CurrentValue &&
                    target.HealthPercent <= Offensive["ghostbladeManagerMinEnemyHP"].Cast<Slider>().CurrentValue)
                {
                    AkaActivator.Youmus.Cast();
                }
                if (Offensive["tiamatManager"].Cast<CheckBox>().CurrentValue &&
                    Player.Instance.ServerPosition.Distance(target) <= 400 && AkaActivator.Tiamat.IsReady() &&
                    Player.Instance.HealthPercent >= Offensive["tiamatManagerMinMeHP"].Cast<Slider>().CurrentValue &&
                    target.HealthPercent <= Offensive["tiamatManagerMinEnemyHP"].Cast<Slider>().CurrentValue)
                {
                    AkaActivator.Tiamat.Cast();
                }
                if (Offensive["hydraManager"].Cast<CheckBox>().CurrentValue &&
                    Player.Instance.ServerPosition.Distance(target) <= 400 && AkaActivator.Hydra.IsReady() &&
                    Player.Instance.HealthPercent >= Offensive["hydraManagerMinMeHP"].Cast<Slider>().CurrentValue &&
                    target.HealthPercent <= Offensive["hydraMinEnemyHP"].Cast<Slider>().CurrentValue)
                {
                    AkaActivator.Hydra.Cast();
                }
            }

        }

        private static void JungleClear()
        {
            var minions =
                EntityManager.MinionsAndMonsters.GetJungleMonsters(Player.Instance.Position, 500)
                    .Where(t => !t.IsDead && t.IsValid && !t.IsInvulnerable);
            foreach (var target in minions)
            {
                if (Offensive["ghostbladeManager"].Cast<CheckBox>().CurrentValue && AkaActivator.Youmus.IsReady() &&
                    Player.Instance.HealthPercent >= Offensive["ghostbladeManagerMinMeHP"].Cast<Slider>().CurrentValue &&
                    target.HealthPercent <= Offensive["ghostbladeManagerMinEnemyHP"].Cast<Slider>().CurrentValue)
                {
                    AkaActivator.Youmus.Cast();
                }
                if (Offensive["tiamatManager"].Cast<CheckBox>().CurrentValue && AkaActivator.Tiamat.IsReady() &&
                    Player.Instance.ServerPosition.Distance(target) <= 400 &&
                    Player.Instance.HealthPercent >= Offensive["tiamatManagerMinMeHP"].Cast<Slider>().CurrentValue &&
                    target.HealthPercent <= Offensive["tiamatManagerMinEnemyHP"].Cast<Slider>().CurrentValue)
                {
                    AkaActivator.Tiamat.Cast();
                }
                if (Offensive["hydraManager"].Cast<CheckBox>().CurrentValue && AkaActivator.Hydra.IsReady() &&
                    Player.Instance.ServerPosition.Distance(target) <= 400 &&
                    Player.Instance.HealthPercent >= Offensive["hydraManagerMinMeHP"].Cast<Slider>().CurrentValue &&
                    target.HealthPercent <= Offensive["hydraMinEnemyHP"].Cast<Slider>().CurrentValue)
                {
                    AkaActivator.Hydra.Cast();
                }
            }

        }

        private static void Flee()
        {
            var target = TargetSelector.GetTarget(1000, DamageType.Physical);
            if (target == null)
            {
                return;

            }
            if (Offensive["botrkManager"].Cast<CheckBox>().CurrentValue &&
                Player.Instance.ServerPosition.Distance(target) <= 550 && AkaActivator.Botrk.IsReady() &&
                Player.Instance.HealthPercent >= Offensive["botrkManagerMinMeHP"].Cast<Slider>().CurrentValue &&
                target.HealthPercent <= Offensive["botrkManagerMinEnemyHP"].Cast<Slider>().CurrentValue)
            {
                AkaActivator.Botrk.Cast(target);
            }

            if (Offensive["gunbladeManager"].Cast<CheckBox>().CurrentValue &&
                Player.Instance.ServerPosition.Distance(target) <= 700 && AkaActivator.GunBlade.IsReady() &&
                Player.Instance.HealthPercent >= Offensive["gunbladeManagerMinMeHP"].Cast<Slider>().CurrentValue &&
                target.HealthPercent <= Offensive["gunbladeManagerMinEnemyHP"].Cast<Slider>().CurrentValue)
            {
                AkaActivator.GunBlade.Cast(target);
            }

            if (Offensive["cutlassManager"].Cast<CheckBox>().CurrentValue &&
                Player.Instance.ServerPosition.Distance(target) <= 550 && AkaActivator.Bilgewater.IsReady() &&
                Player.Instance.HealthPercent >= Offensive["cutlassManagerMinMeHP"].Cast<Slider>().CurrentValue &&
                target.HealthPercent <= Offensive["cutlassManagerMinEnemyHP"].Cast<Slider>().CurrentValue)
            {
                AkaActivator.Bilgewater.Cast(target);
            }

            if (Offensive["ghostbladeManager"].Cast<CheckBox>().CurrentValue && AkaActivator.Youmus.IsReady() &&
                Player.Instance.HealthPercent >= Offensive["ghostbladeManagerMinMeHP"].Cast<Slider>().CurrentValue &&
                target.HealthPercent <= Offensive["ghostbladeManagerMinEnemyHP"].Cast<Slider>().CurrentValue)
            {
                AkaActivator.Youmus.Cast();
            }
            if (Offensive["tiamatManager"].Cast<CheckBox>().CurrentValue &&
                Player.Instance.ServerPosition.Distance(target) <= 400 && AkaActivator.Tiamat.IsReady() &&
                Player.Instance.HealthPercent >= Offensive["tiamatManagerMinMeHP"].Cast<Slider>().CurrentValue &&
                target.HealthPercent <= Offensive["tiamatManagerMinEnemyHP"].Cast<Slider>().CurrentValue)
            {
                AkaActivator.Tiamat.Cast();
            }
            if (Offensive["hydraManager"].Cast<CheckBox>().CurrentValue &&
                Player.Instance.ServerPosition.Distance(target) <= 400 && AkaActivator.Hydra.IsReady() &&
                Player.Instance.HealthPercent >= Offensive["hydraManagerMinMeHP"].Cast<Slider>().CurrentValue &&
                target.HealthPercent <= Offensive["hydraMinEnemyHP"].Cast<Slider>().CurrentValue)
            {
                AkaActivator.Hydra.Cast();
            }
        }

        private static void SmiteEvent(EventArgs args)
        {
            if (!Smite.IsReady() || Player.Instance.IsDead) return;
            if (SmiteMenu["smiteActive"].Cast<KeyBind>().CurrentValue)
            {
                var unit =
                    EntityManager.MinionsAndMonsters.Monsters
                        .Where(
                            a =>
                                AkaActivator.SmiteableUnits.Contains(a.BaseSkinName) && a.Health < AkaActivator.GetSmiteDamage() &&
                                SmiteMenu[a.BaseSkinName].Cast<CheckBox>().CurrentValue)
                        .OrderByDescending(a => a.MaxHealth)
                        .FirstOrDefault();

                if (unit != null)
                {
                    Smite.Cast(unit);
                }
            }
            if (SmiteMenu["useSlowSmite"].Cast<CheckBox>().CurrentValue &&
                Smite.Handle.Name == "s5_summonersmiteplayerganker")
            {
                foreach (
                    var target in
                        EntityManager.Heroes.Enemies
                            .Where(h => h.IsValidTarget(Smite.Range) && h.Health <= 20 + 8 * Player.Instance.Level))
                {
                    Smite.Cast(target);
                }
            }
            if (SmiteMenu["comboWithDuelSmite"].Cast<CheckBox>().CurrentValue &&
                Smite.Handle.Name == "s5_summonersmiteduel" &&
                Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                foreach (
                    var target in
                        EntityManager.Heroes.Enemies
                            .Where(h => h.IsValidTarget(Smite.Range)).OrderByDescending(TargetSelector.GetPriority))
                {
                    AkaActivator.Smite.Cast(target);
                }
            }
        }


    }

}

