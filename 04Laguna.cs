//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;
public class Laguna
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        StoryLine();

        Core.SetOptions(false);
    }

    public void StoryLine()
    {
        if (Core.isCompletedBefore(7712))
        {
            Core.Logger("You have already completed this storyline");
            return;
        }
            

        Story.PreLoad();

        //Fight the Crew 7702
        Story.KillQuest(7702, "laguna", "ShadowChaos Brigand");

        //Reach the Ship 7703
        Story.MapItemQuest(7703, "lagunabeach", 7675);

        //Get the Bombs 7704
        Story.KillQuest(7704, "laguna", "ShadowChaos Gunner");

        //Blow 'er Up! 7705
        Story.KillQuest(7705, "laguna", "ShadowChaos Gunner");
        Story.MapItemQuest(7705, "laguna", 7676);

        //Defeat the Captain 7706
        Story.KillQuest(7706, "laguna", "Captain Laguna");

        //More Pirates 7707
        Story.KillQuest(7707, "laguna", "ShadowChaos Brigand");

        //Steal for Scouts 7708
        Story.KillQuest(7708, "laguna", "Chaos Roe");

        //Make them Work 7709
        Story.KillQuest(7709, "laguna", "Chaos Burrower");

        //Find the Amulet 7710
        Story.KillQuest(7710, "laguna", "ShadowChaos Brigand");
        Story.MapItemQuest(7710, "laguna", 7678);

        //Retrieve the Amulet 7711
        Story.KillQuest(7711, "laguna", "Writhing Chaos");

        //Snack Time 7712
        Story.KillQuest(7712, "laguna", "Chaos Roe");
    }
}
