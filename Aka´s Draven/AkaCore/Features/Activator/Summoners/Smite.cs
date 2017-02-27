using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK.Rendering;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;
using System.Globalization;

namespace AkaCore.Features.Activator.Summoners
{
    class Smite : IModule
    {
        public void OnLoad()
        {
            Drawing.OnDraw += OnDraw;
        }

        public ModuleType GetModuleType()
        {
            return ModuleType.OnUpdate;
        }

        public bool ShouldGetExecuted()
        {
            return AkaCore.Manager.MenuManager.SmiteActive && AkaLib.Item.Smite != null && AkaLib.Item.Smite.IsReady();
        }

        public void OnExecute()
        {
            foreach (var mob in EntityManager.MinionsAndMonsters.GetJungleMonsters(ObjectManager.Player.Position, 950f)
    .Where(m => !m.Name.Contains("Mini") && !m.Name.Contains("Respawn")))
            {
                if (mob.Distance(ObjectManager.Player) - (ObjectManager.Player.BoundingRadius)
                    > 500)
                {
                    continue;
                }

                if (AkaCore.Manager.MenuManager.SBaron && mob.BaseSkinName == "SRU_Baron")
                {
                    if (mob.Health < ObjectManager.Player.GetSummonerSpellDamage(mob, DamageLibrary.SummonerSpells.Smite))
                    {
                        AkaLib.Item.Smite.Cast(mob);
                    }
                }

                if (AkaCore.Manager.MenuManager.SHerald && mob.BaseSkinName == "SRU_RiftHerald")
                {
                    if (mob.Health < ObjectManager.Player.GetSummonerSpellDamage(mob, DamageLibrary.SummonerSpells.Smite))
                    {
                        AkaLib.Item.Smite.Cast(mob);
                    }
                }

                if (AkaCore.Manager.MenuManager.SBlue && mob.BaseSkinName == "SRU_Blue")
                {
                    if (mob.Health < ObjectManager.Player.GetSummonerSpellDamage(mob, DamageLibrary.SummonerSpells.Smite))
                    {
                        AkaLib.Item.Smite.Cast(mob);
                    }
                }

                if (AkaCore.Manager.MenuManager.SWDragon && mob.BaseSkinName == "SRU_Dragon_Water")
                {
                    if (mob.Health < ObjectManager.Player.GetSummonerSpellDamage(mob, DamageLibrary.SummonerSpells.Smite))
                    {
                        AkaLib.Item.Smite.Cast(mob);
                    }
                }

                if (AkaCore.Manager.MenuManager.SFDragon && mob.BaseSkinName == "SRU_Dragon_Fire")
                {
                    if (mob.Health < ObjectManager.Player.GetSummonerSpellDamage(mob, DamageLibrary.SummonerSpells.Smite))
                    {
                        AkaLib.Item.Smite.Cast(mob);
                    }
                }

                if (AkaCore.Manager.MenuManager.SEDragon && mob.BaseSkinName == "SRU_Dragon_Earth")
                {
                    if (mob.Health < ObjectManager.Player.GetSummonerSpellDamage(mob, DamageLibrary.SummonerSpells.Smite))
                    {
                        AkaLib.Item.Smite.Cast(mob);
                    }
                }

                if (AkaCore.Manager.MenuManager.SADragon && mob.BaseSkinName == "SRU_Dragon_Air")
                {
                    if (mob.Health < ObjectManager.Player.GetSummonerSpellDamage(mob, DamageLibrary.SummonerSpells.Smite))
                    {
                        AkaLib.Item.Smite.Cast(mob);
                    }
                }

                if (AkaCore.Manager.MenuManager.Elder && mob.BaseSkinName == "SRU_Dragon_Elder")
                {
                    if (mob.Health < ObjectManager.Player.GetSummonerSpellDamage(mob, DamageLibrary.SummonerSpells.Smite))
                    {
                        AkaLib.Item.Smite.Cast(mob);
                    }
                }

                if (AkaCore.Manager.MenuManager.SRed && mob.BaseSkinName == "SRU_Red")
                {
                    if (mob.Health < ObjectManager.Player.GetSummonerSpellDamage(mob, DamageLibrary.SummonerSpells.Smite))
                    {
                        AkaLib.Item.Smite.Cast(mob);
                    }
                }

                if (AkaCore.Manager.MenuManager.SRaptor && mob.BaseSkinName == "SRU_Razorbeak")
                {
                    if (mob.Health < ObjectManager.Player.GetSummonerSpellDamage(mob, DamageLibrary.SummonerSpells.Smite))
                    {
                        AkaLib.Item.Smite.Cast(mob);
                    }
                }

                if (AkaCore.Manager.MenuManager.SWolf && mob.BaseSkinName == "SRU_Murkwolf")
                {
                    if (mob.Health < ObjectManager.Player.GetSummonerSpellDamage(mob, DamageLibrary.SummonerSpells.Smite))
                    {
                        AkaLib.Item.Smite.Cast(mob);
                    }
                }

                if (AkaCore.Manager.MenuManager.SGromp && mob.BaseSkinName == "SRU_Gromp")
                {
                    if (mob.Health < ObjectManager.Player.GetSummonerSpellDamage(mob, DamageLibrary.SummonerSpells.Smite))
                    {
                        AkaLib.Item.Smite.Cast(mob);
                    }
                }

                if (AkaCore.Manager.MenuManager.SGrap && mob.BaseSkinName == "Sru_Crab")
                {
                    if (mob.Health < ObjectManager.Player.GetSummonerSpellDamage(mob, DamageLibrary.SummonerSpells.Smite))
                    {
                        AkaLib.Item.Smite.Cast(mob);
                    }
                }

                if (AkaCore.Manager.MenuManager.SKrug && mob.BaseSkinName == "SRU_Krug")
                {
                    if (mob.Health < ObjectManager.Player.GetSummonerSpellDamage(mob, DamageLibrary.SummonerSpells.Smite))
                    {
                        AkaLib.Item.Smite.Cast(mob);
                    }
                }
            }
        }

        public static void OnDraw(EventArgs args)
        {
            var smiteactive = AkaCore.Manager.MenuManager.SmiteActive;
            var pos = ObjectManager.Player.Position.WorldToScreen();

            if (smiteactive && AkaLib.Item.Smite != null)
            {
                var smiterdy = AkaLib.Item.Smite.IsReady();
                var text = $"Smite: {(smiteactive ? (smiterdy ? "Ready" : "Not Ready") : "Off")}";

                if (AkaCore.Manager.MenuManager.SStatus)
                {
                    Drawing.DrawText(pos.X - (float)Drawing.GetTextEntent(text, 2).Width / 2, pos.Y + 20, smiteactive ? System.Drawing.Color.White : System.Drawing.Color.Gray, text);
                }

                if (AkaCore.Manager.MenuManager.SDamage && smiterdy)
                {
                    var minions =
                        ObjectManager.Get<Obj_AI_Minion>()
                            .Where(
                                m =>
                                m.Team == GameObjectTeam.Neutral && m.IsValidTarget());

                    foreach (var minion in minions.Where(m => m.IsHPBarRendered))
                    {
                        var hpBarPosition = minion.HPBarPosition;
                        var maxHealth = minion.MaxHealth;
                        var sDamage = ObjectManager.Player.GetSummonerSpellDamage(minion, DamageLibrary.SummonerSpells.Smite);
 
                        var x = sDamage / maxHealth;
                        var barWidth = 0;


                        switch (minion.CharData.BaseSkinName)
                        {
                            case "SRU_RiftHerald":
                                barWidth = 145;
                                Drawing.DrawLine(
                                    new Vector2(hpBarPosition.X + 3 + barWidth * x, hpBarPosition.Y + 17),
                                    new Vector2(hpBarPosition.X + 3 + barWidth * x, hpBarPosition.Y + 30),
                                    2f,
                                    System.Drawing.Color.Chartreuse);
                                Drawing.DrawText(
                                    hpBarPosition.X - 22 + barWidth * x,
                                    hpBarPosition.Y - 5,
                                    System.Drawing.Color.Chartreuse,
                                    sDamage.ToString(CultureInfo.InvariantCulture));
                                break;

                            case "SRU_Dragon_Air":
                            case "SRU_Dragon_Water":
                            case "SRU_Dragon_Fire":
                            case "SRU_Dragon_Elder":
                            case "SRU_Dragon_Earth":
                                barWidth = 145;
                                Drawing.DrawLine(
                                    new Vector2(hpBarPosition.X + 3 + barWidth * x, hpBarPosition.Y + 22),
                                    new Vector2(hpBarPosition.X + 3 + barWidth * x, hpBarPosition.Y + 30),
                                    2f,
                                    System.Drawing.Color.Orange);
                                Drawing.DrawText(
                                    hpBarPosition.X - 22 + barWidth * x,
                                    hpBarPosition.Y - 5,
                                    System.Drawing.Color.Chartreuse,
                                    sDamage.ToString(CultureInfo.InvariantCulture));
                                break;

                            case "SRU_Red":
                            case "SRU_Blue":
                                barWidth = 145;
                                Drawing.DrawLine(
                                    new Vector2(hpBarPosition.X + 3 + barWidth * x, hpBarPosition.Y + 20),
                                    new Vector2(hpBarPosition.X + 3 + barWidth * x, hpBarPosition.Y + 30),
                                    2f,
                                    System.Drawing.Color.Orange);
                                Drawing.DrawText(
                                    hpBarPosition.X - 22 + barWidth * x,
                                    hpBarPosition.Y - 5,
                                    System.Drawing.Color.Chartreuse,
                                    sDamage.ToString(CultureInfo.InvariantCulture));
                                break;

                            case "SRU_Baron":
                                barWidth = 194;
                                Drawing.DrawLine(
                                    new Vector2(hpBarPosition.X + 18 + barWidth * x, hpBarPosition.Y + 20),
                                    new Vector2(hpBarPosition.X + 18 + barWidth * x, hpBarPosition.Y + 35),
                                    2f,
                                    System.Drawing.Color.Chartreuse);
                                Drawing.DrawText(
                                    hpBarPosition.X - 22 + barWidth * x,
                                    hpBarPosition.Y - 3,
                                    System.Drawing.Color.Chartreuse,
                                    sDamage.ToString(CultureInfo.InvariantCulture));
                                break;

                            case "SRU_Gromp":
                                barWidth = 87;
                                Drawing.DrawLine(
                                    new Vector2(hpBarPosition.X + barWidth * x, hpBarPosition.Y + 11),
                                    new Vector2(hpBarPosition.X + barWidth * x, hpBarPosition.Y + 4),
                                    2f,
                                    System.Drawing.Color.Chartreuse);
                                Drawing.DrawText(
                                    hpBarPosition.X + barWidth * x,
                                    hpBarPosition.Y - 15,
                                    System.Drawing.Color.Chartreuse,
                                    sDamage.ToString(CultureInfo.InvariantCulture));
                                break;

                            case "SRU_Murkwolf":
                                barWidth = 75;
                                Drawing.DrawLine(
                                    new Vector2(hpBarPosition.X + barWidth * x, hpBarPosition.Y + 11),
                                    new Vector2(hpBarPosition.X + barWidth * x, hpBarPosition.Y + 4),
                                    2f,
                                    System.Drawing.Color.Chartreuse);
                                Drawing.DrawText(
                                    hpBarPosition.X + barWidth * x,
                                    hpBarPosition.Y - 15,
                                    System.Drawing.Color.Chartreuse,
                                    sDamage.ToString(CultureInfo.InvariantCulture));
                                break;

                            case "Sru_Crab":
                                barWidth = 61;
                                Drawing.DrawLine(
                                    new Vector2(hpBarPosition.X + barWidth * x, hpBarPosition.Y + 8),
                                    new Vector2(hpBarPosition.X + barWidth * x, hpBarPosition.Y + 4),
                                    2f,
                                    System.Drawing.Color.Chartreuse);
                                Drawing.DrawText(
                                    hpBarPosition.X + barWidth * x,
                                    hpBarPosition.Y - 15,
                                    System.Drawing.Color.Chartreuse,
                                    sDamage.ToString(CultureInfo.InvariantCulture));
                                break;

                            case "SRU_Razorbeak":
                                barWidth = 75;
                                Drawing.DrawLine(
                                    new Vector2(hpBarPosition.X + barWidth * x, hpBarPosition.Y + 11),
                                    new Vector2(hpBarPosition.X + barWidth * x, hpBarPosition.Y + 4),
                                    2f,
                                    System.Drawing.Color.Chartreuse);
                                Drawing.DrawText(
                                    hpBarPosition.X + barWidth * x,
                                    hpBarPosition.Y - 15,
                                    System.Drawing.Color.Chartreuse,
                                    sDamage.ToString(CultureInfo.InvariantCulture));
                                break;

                            case "SRU_Krug":
                                barWidth = 81;
                                Drawing.DrawLine(
                                    new Vector2(hpBarPosition.X + barWidth * x, hpBarPosition.Y + 11),
                                    new Vector2(hpBarPosition.X + barWidth * x, hpBarPosition.Y + 4),
                                    2f,
                                    System.Drawing.Color.Chartreuse);
                                Drawing.DrawText(
                                    hpBarPosition.X + barWidth * x,
                                    hpBarPosition.Y - 15,
                                    System.Drawing.Color.Chartreuse,
                                    sDamage.ToString(CultureInfo.InvariantCulture));
                                break;
                        }
                    }
                }
            }
        }
    }
}
