//cs_include Scripts/CoreBots.cs
using RBot;

public class GuardianTree
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

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
        Core.KillQuest(6276, "guardiantree", "Blossoming Treeant");
        Core.MapItemQuest(6276, "guardiantree", 5769, 5);
        //Help the Hedgies
        Core.MapItemQuest(6277, "guardiantree", 5776, 5);
        Core.MapItemQuest(6277, "guardiantree", 5770);
        //Cleanse the Corrupted Zards
        Core.KillQuest(6278, "guardiantree", "Corrupted Zard");
        //Plant the Seed
        Core.KillQuest(6279, "guardiantree", "Seed Spitter");
        Core.MapItemQuest(6279, "guardiantree", 5771);
        //Reach the Top
        Core.MapItemQuest(6280, "guardiantree", 5772);
        //Cointain the Pollen
        Core.KillQuest(6281, "guardiantree", "Blossoming Treeant");
        //Pass Through the Pollen
        Core.KillQuest(6282, "guardiantree", "Pollen Cloud");
        Core.MapItemQuest(6282, "guardiantree", 5773);
        //Reinvigorate the Sprout
        if (!Core.QuestProgression(6283))
        {
            Core.EnsureAccept(6283);
            Core.KillMonster("guardiantree", "r8", "Left", "Seed Spitter", "Life Energy", 8);
            Core.EnsureComplete(6283);
        }
        //Up We Go!
        Core.MapItemQuest(6284, "guardiantree", 5774);
        //Grow a Bridge
        Core.KillQuest(6285, "guardiantree", "Myconid");
        Core.MapItemQuest(6285, "guardiantree", 5775, 2);
        //Take Down Terrane
        Core.KillQuest(6286, "guardiantree", "Terrane");
    }
}
