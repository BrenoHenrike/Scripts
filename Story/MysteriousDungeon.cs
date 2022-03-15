//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class MysteriousDungeon
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        DoAll();

        Core.SetOptions(false);
    }

    public void DoAll()
    {
        Storyline();
        Core.Logger("MysteriousDungeon Finished");
    }

    public void Storyline()
    {
        if (Core.isCompletedBefore(5451))
            return;

        Story.PreLoad();

        //cursed artifact shop - 5428
        Story.MapItemQuest(5428, "cursedshop", MapItemID: 4803);

        //lamps, painting and chairs, oh my!
        Story.KillQuest(5429, "cursedshop", "Antique Chair");

        //the (un)Dresser
        Story.KillQuest(5430, "cursedshop", "UnDresser");

        //ghost stories
        Story.KillQuest(5431, "cursedshop", "Writing Desk");

        //you can't tell time
        Story.KillQuest(5432, "cursedshop", "Grandfather Clock");

        //Dr. Darkwood's Robe
        Story.MapItemQuest(5433, "cursedshop", MapItemID: 4804);
        Story.MapItemQuest(5433, "cursedshop", MapItemID: 4805);

        //defeat the arcane sentinel
        Story.KillQuest(5434, "cursedshop", "Arcane Sentinel");
        Story.MapItemQuest(5434, "cursedshop", MapItemID: 4806);

        //Get Out Of Jail Free?
        Story.MapItemQuest(5438, "MysteriousDungeon", 4818);

        //Sorry, Skudly
        Story.KillQuest(5439, "MysteriousDungeon", "Skudly");

        //A Friend In Need
        Story.MapItemQuest(5440, "MysteriousDungeon", MapItemID: 4808);

        //A Cryptic Messaage
        Story.MapItemQuest(5441, "MysteriousDungeon", MapItemID: 4809);

        //seeking answers
        Story.MapItemQuest(5442, "MysteriousDungeon", MapItemID: 4810);
        Story.MapItemQuest(5442, "MysteriousDungeon", MapItemID: 4811);
        Story.MapItemQuest(5442, "MysteriousDungeon", MapItemID: 4812);
        Story.MapItemQuest(5442, "MysteriousDungeon", MapItemID: 4813);
        Story.MapItemQuest(5442, "MysteriousDungeon", MapItemID: 4814);
        Story.MapItemQuest(5442, "MysteriousDungeon", MapItemID: 4815);
        Story.MapItemQuest(5442, "MysteriousDungeon", MapItemID: 4816);

        //Curses!        
        Story.MapItemQuest(5443, "MysteriousDungeon", MapItemID: 4817);

        //Skudly, Staaaahp!        
        Story.KillQuest(5444, "MysteriousDungeon", "Skudly");

        // Not So Mysterious After All
        Story.KillQuest(5445, "MysteriousDungeon", "Mysterious Stranger");

        // Shut Up And Listen, Vaden!
        Story.KillQuest(5446, "MysteriousDungeon", "Vaden", AutoCompleteQuest: false);

        // Shut Up And Listen, Xeight!
        Story.KillQuest(5447, "MysteriousDungeon", "Xeight", AutoCompleteQuest: false);

        // Shut Up And Listen, Ziri
        Story.KillQuest(5448, "MysteriousDungeon", "Ziri", AutoCompleteQuest: false);

        // Shut Up And Listen, Pax!
        Story.KillQuest(5449, "MysteriousDungeon", "Pax", AutoCompleteQuest: false);

        // Shut Up And Listen, Sekt!
        Story.KillQuest(5450, "MysteriousDungeon", "Sekt", AutoCompleteQuest: false);

        // Shut Up And Listen, Groglurk!
        Story.KillQuest(5451, "MysteriousDungeon", "Scarletta", AutoCompleteQuest: false);
    }

}