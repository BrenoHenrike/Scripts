//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class DeadFly
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
            Core.Logger("Deadfly Storyline Is Member Only. Skipping this Script");
            return;
        }

        if (Core.isCompletedBefore(8232))
            return;

        //[[ DeadFly Map ]]

        //The Flies 8217
        Story.KillQuest(8217, "DeadFly", "Grave Flies");

        //Dem Bones 8218
        Story.MapItemQuest(8218, "DeadFly", 8766, 8);

        //Gather the Reagents 8219
        Story.MapItemQuest(8219, "DeadFly", 8767, 7);
        Story.KillQuest(8219, "DeadFly", "Skeletal Minion");

        //Rip it Out 8220
        Story.KillQuest(8220, "DeadFly", "Gutripper");

        //Steal the Wands 8221
        Story.KillQuest(8221, "DeadFly", "Skeletal Mage");

        //Get to Emily 8222
        Story.MapItemQuest(8222, "DeadFly", 8768);

        //More of the Flies 8223
        Story.KillQuest(8223, "DeadFly", "Grave Flies");

        //Follow the Swarm 8224
        Story.MapItemQuest(8224, "DeadFly", new[] { 8769, 8770 });

        //Find Emily 8225
        Story.MapItemQuest(8225, "DeadFly", 8771);
        Story.KillQuest(8225, "DeadFly", "Skeletal Mage");

        //The Deadfly 8226
        Story.KillQuest(8226, "DeadFly", "Deadfly");

        //Gossip Time 8227
        Story.KillQuest(8227, "DeadFly", "Skeletal Minion");

        //Souls for Offering 8228
        Story.MapItemQuest(8228, "DeadFly", 8772);
        Story.KillQuest(8228, "DeadFly", "Skeletal Mage");

        //[[ RotFinger Map ]]

        //Clue by 8 8229
        Story.KillQuest(8229, "RotFinger", "Lost Soul");

        //Cloth n' Coal 8230
        Story.MapItemQuest(8230, "RotFinger", 8773, 5);
        Story.KillQuest(8230, "RotFinger", "Blade Master");

        //Bait the Hook 8231
        Story.MapItemQuest(8231, "RotFinger", 8774, 10);

        //Rotfinger's Reckoning 8232
        Story.KillQuest(8232, "RotFinger", "Rotfinger");


    }
}
