//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class SuperSLAYINBadge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetBadgeANDDoStory();

        Core.SetOptions(false);
    }

    public void GetBadgeANDDoStory()
    {
        //Progress Check
        if (Core.isCompletedBefore(8006))
            return;
        if (!Core.isSeasonalMapActive("gardenquest"))
            return;

        //Preload Quests
        Story.PreLoad(this);

        Bot.Drops.Start();

        //BATTLE: Broccoli vs Hero
        Story.KillQuest(8002, "gardenquest", "Angry Broccoli");

        //BATTLE: Radish vs Hero
        Story.KillQuest(8003, "gardenquest", "Overconfident Radish");

        //BATTLE: Karrot vs Hero
        Story.KillQuest(8004, "gardenquest", "Silly Karrot");

        //BATTLE: Vegetable Prince vs Hero
        Story.KillQuest(8005, "gardenquest", "Vegetable Prince");

        //BATTLE: SUPER Vegetable Prince vs Hero
        Story.KillQuest(8006, "gardenquest", "Super Prince");
    }
}