using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

// Using the config like this makes your life easier, trust me
using Settings = AddonTemplate.Config.Modes.Combo;

namespace AddonTemplate.Modes
{
    public sealed class Combo : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            // Only execute this mode when the orbwalker is on combo mode
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
        }

        public override void Execute()
        {
            if (Settings.UseQ  && SpellManager.Q.IsReady())
            {
                QLogic.QCombo();
            }
            if (Config.Modes.Condemn.Condemn1 && SpellManager.E.IsReady())
            {
                ELogic.Condemn1();
            }
            if (Config.Modes.Condemn.Condemn2 && SpellManager.E.IsReady())
            {
                ELogic.Condemn2();
            }
            if (Config.Modes.Condemn.Condemn3 && SpellManager.E.IsReady())
            {
                ELogic.Condemn3();
            }
            if (Settings.UseR && R.IsReady())
            {
                ComboUltimateLogic();
            }
        }
        public void ComboUltimateLogic()
        {
            if (ObjectManager.Player.CountEnemiesInRange(1000) >= Settings.UseRSlider)
            {
                SpellManager.R.Cast();
            }
        }

    }
}

                
            

        
    



