//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Mazumi
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        MazumiQuests();

        Core.SetOptions(false);
    }

    public void MazumiQuests()
    {
        if (Core.isCompletedBefore(92))
            return;

        Story.PreLoad(this);

        // Ninja Grudge 90
        Story.KillQuest(90, "pirates", "Fishman Soldier");

        // Without a Trace 91
        Story.KillQuest(91, "greenguardwest", new[] { "Kittarian", "River Fishman", "Slime", "River Fishman", "Big Bad Boar" });

        // Hit Job 92
        Story.KillQuest(92, "greenguardwest", new[] { "Breken the Vile", "Ogug Stoneaxe" });

    }
}