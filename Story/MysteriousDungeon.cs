//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs

//cs_include Scripts/CoreFile(Or folder)/Filename.cs

using RBot;

public class MysteriousDungeon
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreDailys Dailys = new CoreDailys();

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

        //cursed artifact shop - 5428
        Core.MapItemQuest(5428, "cursedshop", MapItemID: 4803, AutoCompleteQuest: false);

        //lamps, painting and chairs, oh my!
        Core.KillQuest(5429, "cursedshop", "Antique Chair");

        //the (un)Dresser
        Core.KillQuest(5430, "cursedshop", "UnDresser");

        //ghost stories
        Core.KillQuest(5431, "cursedshop", "Writing Desk");

        //you can't tell time
        Core.KillQuest(5432, "cursedshop", "Grandfather Clock");

        //Dr. Darkwood's Robe
        Core.MapItemQuest(5433, "cursedshop", MapItemID: 4804, AutoCompleteQuest: false);
        Core.MapItemQuest(5433, "cursedshop", MapItemID: 4805, AutoCompleteQuest: false);

        //defeat the arcane sentinel
        Core.KillQuest(5434, "cursedshop", "Arcane Sentinel");
        Core.MapItemQuest(5434, "cursedshop", MapItemID: 4806);

        //Get Out Of Jail Free?
        Core.MapItemQuest(5438, "MysteriousDungeon", 4818);

        //Sorry, Skudly
        Core.KillQuest(5439, "MysteriousDungeon", "Skudly");

        //A Friend In Need
        Core.MapItemQuest(5440, "MysteriousDungeon", MapItemID: 4808);

        //A Cryptic Messaage
        Core.MapItemQuest(5441, "MysteriousDungeon", MapItemID: 4809);

        //seeking answers
        Core.MapItemQuest(5442, "MysteriousDungeon", MapItemID: 4810);
        Core.MapItemQuest(5442, "MysteriousDungeon", MapItemID: 4811);
        Core.MapItemQuest(5442, "MysteriousDungeon", MapItemID: 4812);
        Core.MapItemQuest(5442, "MysteriousDungeon", MapItemID: 4813);
        Core.MapItemQuest(5442, "MysteriousDungeon", MapItemID: 4814);
        Core.MapItemQuest(5442, "MysteriousDungeon", MapItemID: 4815);
        Core.MapItemQuest(5442, "MysteriousDungeon", MapItemID: 4816);

        //Curses!        
        Core.MapItemQuest(5443, "MysteriousDungeon", MapItemID: 4817);

        //Skudly, Staaaahp!        
        Core.KillQuest(5444, "MysteriousDungeon", "Skudly");

        // Not So Mysterious After All
        Core.KillQuest(5445, "MysteriousDungeon", "Mysterious Stranger");

        // Shut Up And Listen, Vaden!
        Core.KillQuest(5446, "MysteriousDungeon", "Vaden");

        // Shut Up And Listen, Xeight!
        Core.KillQuest(5447, "MysteriousDungeon", "Xeight");

        // Shut Up And Listen, Ziri
        Core.KillQuest(5448, "MysteriousDungeon", "Ziri");

        // Shut Up And Listen, Pax!
        Core.KillQuest(5449, "MysteriousDungeon", "Pax");

        // Shut Up And Listen, Sekt!
        Core.KillQuest(5450, "MysteriousDungeon", "Sekt");

        // Shut Up And Listen, Groglurk!
        Core.KillQuest(5451, "MysteriousDungeon", "Scarletta");
    }

}