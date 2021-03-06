//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class SuperSLAYINBadge
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
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

        //Preload Quests
        Story.PreLoad();

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