//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/ChaosQueenBeleen.cs
using RBot;

public class BeleensChaosFuzzyShop
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public ChaosQueenBeleen Beleen = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetChaosFuzzies();

        Core.SetOptions(false);
    }

    public void GetChaosFuzzies()
    {
        Core.AddDrop("Chaos Fuzzies");

        Beleen.BeleenQuests(true);

        while (!Core.CheckInventory("Chaos Fuzzies", 300))
            Core.GetMapItem(3481, map: "Citadel");
    }
}