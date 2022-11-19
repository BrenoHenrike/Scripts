//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/HarvestDay/CoreHarvestDay.cs
using Skua.Core.Interfaces;

public class NoEgretsbadge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    CoreHarvestDay HarvestDay = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Badge();

        Core.SetOptions(false);
    }

    public void Badge()
    {
        if (!Bot.Quests.IsAvailable(8992) && !Bot.Quests.IsUnlocked(8992) || !Core.isSeasonalMapActive("birdswithharms"))
            HarvestDay.BirdsWithHarms();

        Core.EnsureAccept(8992);
        Core.HuntMonster("birdswithharms", "Turkonian", "Ruffled Feather", 1000, false);
        Core.EnsureComplete(8992);
    }
}
