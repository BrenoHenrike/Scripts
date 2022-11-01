//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Pirates
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
        if (Core.isCompletedBefore(1550) && !Core.IsMember)
            return;

        Story.PreLoad(this);

        //Shape of the Ship? 1534
        Story.MapItemQuest(1534, "pirates", 769, 3);

        //Skeleton Crew, Skeleton Key 1535
        Story.KillQuest(1535, "pirates", "Undead Pirate");

        //Dead Journal 1536
        Story.MapItemQuest(1536, "pirates", 770);

        //Sold Out! 1537
        Story.KillQuest(1537, "pirates", "Undead Pirate");

        //Abandon All Hope... 1538
        Story.KillQuest(1538, "pirates", "Undead Pirate");

        //In The Drink 1539
        Story.KillQuest(1539, "pirates", "Undead Pirate");

        //Below Decks 1540
        Story.MapItemQuest(1540, "pirates", 771, 10);

        //Blow Stuff Up! 1541
        Story.MapItemQuest(1541, "pirates", 772, 10);
        Story.KillQuest(1541, "pirates", "Undead Pirate");

        //Pack Of Spices 1550
        Story.KillQuest(1550, "pirates", "Capt. Beard");

    }
}
