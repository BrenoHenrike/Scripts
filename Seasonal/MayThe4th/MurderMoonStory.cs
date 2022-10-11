//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class MurderMoon
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        MurderMoonStory();

        Core.SetOptions(false);
    }

    public void MurderMoonStory()
    {
        if (!Core.isSeasonalMapActive("murdermoon"))
            return;
        if (Core.isCompletedBefore(8064))
            return;

        //That Is The Way
        Story.KillQuest(8062, "murdermoon", "Tempest Soldier");

        //Murder Moon Plans
        Story.KillQuest(8063, "murdermoon", "Tempest Soldier");
        Story.MapItemQuest(8063, "murdermoon", 8373, 5);

        //Revenge of the Fifth
        Story.KillQuest(8064, "murdermoon", "Fifth Sepulchure");
    }
}
