//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Summer2015AdventureMap/CoreSummer.cs
using Skua.Core.Interfaces;

public class BeleensChaosFuzzyShop
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreSummer Beleen = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetChaosFuzzies();

        Core.SetOptions(false);
    }

    public void GetChaosFuzzies()
    {
        Core.AddDrop("Chaos Fuzzies");

        Beleen.Beleen(true);

        while (!Bot.ShouldExit && !Core.CheckInventory("Chaos Fuzzies", 300))
            Core.GetMapItem(3481, map: "Citadel");
    }
}