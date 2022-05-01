//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;
public class LagunaBeach
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
        if (Core.isCompletedBefore(7700))
        {
            Core.Logger("You have already completed this storyline");
            return;
        }
            

        Story.PreLoad();

        //Eyes on You 7690
        Story.KillQuest(7690, "lagunabeach", "Flying Fisheye");

        //Behind the Tentacles 7691
        Story.MapItemQuest(7691, "lagunabeach", 7636);

        //Overwhelming Minions 7692
        Story.KillQuest(7692, "lagunabeach", "ShadowChaos Brigand");

        //Ground Kelp? 7693
        Story.MapItemQuest(7693, "lagunabeach", 7637, 5);
        Story.KillQuest(7693, "lagunabeach", "Chaos Kelp");

        //Slug 'em! 7694
        Story.KillQuest(7694, "lagunabeach", "ShadowChaos Brigand");

        //Shadows and Chaos 7695
        Story.KillQuest(7695, "lagunabeach", "Flying Fisheye");

        //Follow the Trail 7696
        Story.MapItemQuest(7696, "lagunabeach", 7638, 3);
        Story.MapItemQuest(7696, "lagunabeach", 7639);

        //Clear the Tunnel 7697
        Story.KillQuest(7697, "lagunabeach", "ShadowChaos Brigand");

        //Blow it up! 7698
        Story.KillQuest(7698, "lagunabeach", "ShadowChaos Gunner");
        Story.MapItemQuest(7698, "lagunabeach", 7640);

        //The Heart of the Matter 7699
        Story.KillQuest(7697, "lagunabeach", "Heart of Chaos");

        // A Closer Look 7700
        Story.KillQuest(7700, "lagunabeach", "Flying Fisheye");

    }
}
