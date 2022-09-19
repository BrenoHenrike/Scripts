//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;
public class DarkonGarden
{
    public IScriptInterface Bot => IScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        StoryLine();

        Core.SetOptions(false);
    }

    public void StoryLine()
    {
        if (Core.isCompletedBefore(7494))
            return;

        Story.PreLoad(this);

        //Picking Flowers 7485
        Story.MapItemQuest(7485, "garden", 7324, 10);

        //Bunny Food 7486
        Story.KillQuest(7486, "garden", "Creature 343");

        //Feed the Bunnies 7487
        Story.MapItemQuest(7487, "garden", 7325, 10);

        //Control the Bunnies 7488
        Story.KillQuest(7488, "garden", "Creature 83");

        //Time for Tea 7489
        Story.KillQuest(7489, "garden", "Creature 343");

        //Needs Somethinig Else... 7490
        Story.KillQuest(7490, "garden", "Creature 72");

        //Snacktime 7491
        Story.KillQuest(7491, "garden", "Creature 83");

        //Big, MEAN Bunnies 7492
        Story.KillQuest(7492, "garden", "Creature 35");
        Story.MapItemQuest(7492, "garden", 7326);

        //What Crack? 7493
        Story.MapItemQuest(7493, "garden", 7327);

        //OMG What is THAT? 7494
        Story.KillQuest(7494, "garden", "Creature 12");
    }
}
