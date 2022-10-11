//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class AriaPet
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
            Core.Logger("Aria Pet Storyline Is Member Only. Skipping this Script");
            return;
        }
            
        if (Core.isCompletedBefore(46))
            return;

        Story.PreLoad(this);

        //Pet Food Delivery 10
        Story.KillQuest(10, "farm", "Scarecrow");

        //Starving Pets 41
        Story.KillQuest(41, "sewer", "Greenrat");

        //Picky Eaters 42
        Story.KillQuest(42, "river", "River Fishman");

        //Missing Crate 43
        Story.KillQuest(43, "pirates", "Shark Bait");

        //Wilderness 44
        Story.KillQuest(44, "guru", "Trobble");

        //Trobble Bath 45
        Story.KillQuest(45, "swordhaven", "Slime");

        //Home Sick 46
        Story.KillQuest(46, "marsh2", "Soulseeker");

    }
}
