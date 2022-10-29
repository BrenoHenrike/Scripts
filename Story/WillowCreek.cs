//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class WillowCreek
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        StoryLine();

        Core.SetOptions(false);
    }

    public void StoryLine()
    {
        Story.PreLoad(this);

        // Willow Creek 27
        Story.KillQuest(27, "forest", new[] { "Zardman Grunt", "Boss Zardman" });

        // Can you do it? 59
        Story.KillQuest(59, "willowcreek", new[] { "Mosquito", "Frogdrake" });

        // Backyard Zards 22
        Story.KillQuest(22, "willowcreek", "Frogzard");

        // Attic Spiders 24
        Story.KillQuest(24, "willowcreek", "Spider");

        // Storage Speyeders 58
        Story.KillQuest(58, "willowcreek", "Speyeder");

        // Garden Snails 23
        Story.KillQuest(23, "willowcreek", "Snail");

        // Dwakel Info 1219
        Story.MapItemQuest(1219, "willowcreek", 14);

        // Found Clue in Farmer's House 1599
        Story.MapItemQuest(1599, "willowcreek", 15);

        // Tempestuous Times 26
        Story.MapItemQuest(26, "willowcreek", 16);

        // The Bone? 25
        Story.KillQuest(25, "willowcreek", "Werewolf");
    }
}