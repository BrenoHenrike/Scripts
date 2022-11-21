//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/HarvestDay/CoreHarvestDay.cs
using Skua.Core.Interfaces;

public class NoEgretsbadge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
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
            {
                Core.Logger($"Quest [8992] \"No Egrets Badge\", has yet to be completed, please run \"Seasonal/HarvestDay/16BirdsWithHarmsStory.cs\"", messageBox: true);
                return;
            }
        
        Story.KillQuest(8992, "birdswithharms", "Turkonian");
    }
}
