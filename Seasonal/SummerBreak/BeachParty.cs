//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class BeachPartyStory
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Storyline();

        Core.SetOptions(false);
    }

    public void Storyline()
    {
        if (Core.isCompletedBefore(7010))
            return;

        Story.PreLoad();

        //7000 | Sweat - tastic
        Story.KillQuest(7000, "beachparty", "Solar Elemental");

        //7001 | Cool 'em Down
        Story.KillQuest(7001, "beachparty", "Boiling Elemental");

        //7002 | Poor Trees.Pour Trees?
        Story.KillQuest(7002, "beachparty", "Water Elemental");
        Story.MapItemQuest(7002, "beachparty", 6563, 7);

        //7003 | Put out the Flames
        Story.KillQuest(7003, "beachparty", "Sun Flare");
        Story.MapItemQuest(7003, "beachparty", 6564, 8);

        //7004 | Sunscream
        Story.KillQuest(7004, "beachparty", "Solar Elemental");

        //7005 | Whoops!
        Story.KillQuest(7005, "beachparty", "Frozen Water");

        //7006 | Milk From Nuts?
        Story.KillQuest(7006, "beachparty", "Palm Treenat");

        //7007 | Free Drinks
        Story.MapItemQuest(7007, "beachparty", 6565, 8);

        //7008 | Ice, Ice Maybe?
        Story.KillQuest(7008, "beachparty", "Frozen Water");
        Story.MapItemQuest(7008, "beachparty", 6566, 8);

        //7009 | Chill Out!
        Story.KillQuest(7009, "beachparty", "Steaming Dragon");

        //7010 | Find Tokens! Win A Prize!
        if (Story.QuestProgression(7010))
        {
            Core.EnsureAccept(7010);
            Core.KillMonster("beachparty", "r3", "Left", "*", "Tiki Tokens", 5, false);
            Core.EnsureComplete(7010);
        }
    }
}