//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class WorldSoul
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        WorldSoulQuests();

        Core.SetOptions(false);
    }

    public void WorldSoulQuests()
    {
        if (Core.isCompletedBefore(6245))
            return;

        Story.PreLoad();

        if (!Core.isCompletedBefore(6245))
        {
            Core.EnsureAccept(6238);
            Core.HuntMonster("worldsoul", "Dwakel Infiltrator", "Void Cortex");
            Core.HuntMonster("worldsoul", "Dwakel Infiltrator", "Paradox Processor");
            Core.HuntMonster("worldsoul", "Dwakel Infiltrator", "Thermal Vent");
            Core.HuntMonster("worldsoul", "Dwakel Infiltrator", "Dwakel Defeated", 6);
            Core.EnsureComplete(6238);
        }
        Story.KillQuest(6239, "worldsoul", "Divine Water Elemental");
        Story.KillQuest(6240, "worldsoul", "Divine Fire Elemental");
        Story.KillQuest(6241, "worldsoul", "Skeletal Squatter");
        Story.MapItemQuest(6241, "worldsoul", 5681, 3);
        Story.KillQuest(6242, "worldsoul", "Radioactive Hydra");
        Story.KillQuest(6243, "worldsoul", "Legion Dreadmarch");
        Story.MapItemQuest(6243, "worldsoul", 5680);
        Story.KillQuest(6244, "worldsoul", "Legion Dreadmarch");
        Story.MapItemQuest(6244, "worldsoul", 5682);
        Story.KillQuest(6245, "worldsoul", "Core Guardian");
    }
}