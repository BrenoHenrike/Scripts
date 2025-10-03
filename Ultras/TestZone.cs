//cs_include Scripts/Ultras/CoreUltras.cs

using System;
using System.Dynamic;
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class TestZone
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreUltras Core = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.Boot();

        Test();

        Bot.Stop();
    }

    void Test()
    {
        // priority => ...
        //Core.ChooseBestEnhancementFor("Weapon", "Valiance", "Spiral Carve", "Fighter");
        //Core.ChooseBestEnhancementFor("Helm", "Pneuma", "Wizard");
        //Core.ChooseBestEnhancementFor("Cape", "Vainglory", "Wizard");

        //Core.ChooseBestGear("*"); // for the most common race in the map
        //Core.ChooseBestGear("Random Monster"); // for a specific monster

        //Core.BuyItem("Shriekward Potion", 774, "mirrorportal", 30);
        Core.ForItem("Boss Dummy", "classhall", "Celestial Staff", useBestGear: true);
    }
}
