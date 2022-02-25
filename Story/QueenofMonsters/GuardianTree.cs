//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class GuardianTree
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GuardianTreeQuests();

        Core.SetOptions(false);
    }

    public void GuardianTreeQuests()
    {
        if (Core.isCompletedBefore(6286))
            return;

        //Connect to the Earth
        Story.KillQuest(6276, "guardiantree", "Blossoming Treeant");
        Story.MapItemQuest(6276, "guardiantree", 5769, 5);
        //Help the Hedgies
        Story.MapItemQuest(6277, "guardiantree", 5776, 5);
        Story.MapItemQuest(6277, "guardiantree", 5770);
        //Cleanse the Corrupted Zards
        Story.KillQuest(6278, "guardiantree", "Corrupted Zard");
        //Plant the Seed
        Story.KillQuest(6279, "guardiantree", "Seed Spitter");
        Story.MapItemQuest(6279, "guardiantree", 5771);
        //Reach the Top
        Story.MapItemQuest(6280, "guardiantree", 5772);
        //Cointain the Pollen
        Story.KillQuest(6281, "guardiantree", "Blossoming Treeant");
        //Pass Through the Pollen
        Story.KillQuest(6282, "guardiantree", "Pollen Cloud");
        Story.MapItemQuest(6282, "guardiantree", 5773);
        //Reinvigorate the Sprout
        if (!Story.QuestProgression(6283))
        {
            Core.EnsureAccept(6283);
            Core.KillMonster("guardiantree", "r8", "Left", "*", "Life Energy", 8);
            Core.EnsureComplete(6283);
        }
        //Up We Go!
        Story.MapItemQuest(6284, "guardiantree", 5774);
        //Grow a Bridge
        Story.KillQuest(6285, "guardiantree", "Myconid");
        Story.MapItemQuest(6285, "guardiantree", 5775, 2);
        //Take Down Terrane
        Story.KillQuest(6286, "guardiantree", "Terrane");
    }
}
