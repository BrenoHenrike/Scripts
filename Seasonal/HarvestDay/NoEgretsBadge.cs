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
        if (!Core.isCompletedBefore(8993) || !Core.isSeasonalMapActive("birdswithharms"))
            {
                Core.Logger($"Quest [8992] \"No Egrets Badge\", has yet to be completed, please run \"Seasonal/HarvestDay/16BirdsWithHarmsStory.cs\"", messageBox: true);
                return;
            }
        // 8992 No Egrets Badge
        Story.KillQuest(8992, "birdswithharms", "Turkonian");
    }
}