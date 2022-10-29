//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Tower
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        StoryLine();

        Core.SetOptions(false);
    }

    public void StoryLine()
    {
        if (!Core.IsMember)
        {
            Core.Logger("You must be a Member to complete Tower quests!");
            return;
        }

        if (Core.isCompletedBefore(1610))
            return;

        Story.PreLoad(this);

        // Stone Cold Monster 1603
        Story.KillQuest(1603, "trunk", "Greenguard Basilisk");

        // Interview The Troops 1604
        Story.MapItemQuest(1604, "tower", 836, 4);
        
        // The Drop Spot 1605
        Story.MapItemQuest(1605, "tower", 837);
        
        // The Spider's Gem 1606
        Story.KillQuest(1606, "greenguardeast", "Spider");

        // Enemies on the Other Side 1607
        Story.KillQuest(1607, "tower", "Strange Knight");

        // Get Some Answers 1608
        Story.MapItemQuest(1608, "tower", 838);

        // Recovery of Stolen Goods 1609
        Story.MapItemQuest(1609, "tower", 839, 12);

        // Face the Traitor! 1610
        Story.KillQuest(1610, "tower", "Guardian Baxter");
    }
}
