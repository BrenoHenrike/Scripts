//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/WarfuryTraining.cs
using Skua.Core.Interfaces;

public class WarfuryEmblem
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new();
    public WarTraining WFT = new();


    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        WarfuryEmblemFarm();

        Core.SetOptions(false);
    }

    public void WarfuryEmblemFarm(int EmblemQuant = 60)
    {
        if (Core.CheckInventory("Warfury Emblem", EmblemQuant))
            return;

        WFT.StoryLine();

        Core.AddDrop("Warfury Emblem");
        Adv.BestGear(GearBoost.Human);
        Core.RegisterQuests(8204);
        while (!Bot.ShouldExit && !Core.CheckInventory("Warfury Emblem", EmblemQuant))
            Core.HuntMonster("wartraining", "Warfury Soldier", "Warfury Training", 30);
        Bot.Wait.ForPickup("Warfury Emblem");

        Core.CancelRegisteredQuests();
    }
}