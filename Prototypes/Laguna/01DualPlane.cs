//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;
public class DualPlane
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
        if (Core.isCompletedBefore(7571))
        {
            Core.Logger("You have already completed this storyline");
            return;
        }
            

        Story.PreLoad();

        //Check their Teeth 7561
        Story.KillQuest(7561, "dualplane", "Terrarsite");

        //More Investigation Needed 7562
        Story.KillQuest(7562, "dualplane", "Droognax");

        //Find Xiang 7563
        Story.MapItemQuest(7563, "dualplane", 7459);

        //Destroy her Creatures 7564
        Story.KillQuest(7564, "dualplane", "Terrarsite");

        //Time to un-decorate 7565
        Story.KillQuest(7565, "dualplane", new[] { "SpiderWing", "Terrarsite" });

        //Interrogation Time 7566
        Story.KillQuest(7566, "dualplane", "Droognax");

        //Get rid of it! 7567
        Story.KillQuest(7567, "dualplane", "Netherpit Lackey");

        //"Convince" the Bruiser 7568
        Story.KillQuest(7568, "dualplane", "Netherpit Bruiser");

        //They Never Learn 7569
        Story.KillQuest(7569, "dualplane", new[] { "Terrarsite", "Netherpit Lackey" });

        //Get the Mirror! 7570
        Story.MapItemQuest(7570, "dualplane", 7460);

        //Get Her! 7571
        Story.KillQuest(7571, "dualplane", "Xiang");

    }
}
