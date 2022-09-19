//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class FiendPast
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DoAll();

        Core.SetOptions(false);
    }

    public void DoAll()
    {
        if (Core.isCompletedBefore(8495))
            return;

        Story.PreLoad(this);


        //Test the Newborn Fiends 8478
        Story.KillQuest(8478, "fiendpast", "Newborn Fiend");

        //Best the Hex 8479
        Story.KillQuest(8479, "fiendpast", "Hex Fiend");

        //Grunt Work 8480
        Story.KillQuest(8480, "fiendpast", "Hex Fiend");

        //Defeat the Fiend Champion 8481
        Story.KillQuest(8481, "fiendpast", "Fiend Champion");

        //Bring Their Doom 8482
        Story.KillQuest(8482, "fiendpast", "DoomBringer");

        //Form A Perimeter 8483
        Story.MapItemQuest(8483, "fiendpast", 9556, 3);

        //Wrathful Wraiths 8484
        Story.KillQuest(8484, "fiendpast", "Doom Wraith");

        //Defeat Scarvitas 8485
        Story.KillQuest(8485, "fiendpast", "Scarvitas");

        //Seal Maintenance 8486
        Story.MapItemQuest(8486, "fiendpast", 9557, 4);
        Story.KillQuest(8486, "fiendpast", "Avarice Guard");

        //Traitors Are Null And Void 8487
        Story.KillQuest(8487, "fiendpast", "Void Fiend");

        //Greedy Imprisonment 8488
        Story.KillQuest(8488, "fiendpast", "Avarice");

        //Defeat Baelgar 8489
        if (!Story.QuestProgression(8489))
        {
            Core.EnsureAccept(8489);
            Core.HuntMonster("fiendpast", "Baelgar", "Baelgar Defeated");
            Core.EnsureComplete(8489);
        }

        //The Legion? 8490
        Story.KillQuest(8490, "fiendpast", "Proto-Legion Knight");

        //Dealing With Defectors 8491
        Story.KillQuest(8491, "fiendpast", "Nation Defector");

        //Ward Destruction 8492
        Story.MapItemQuest(8492, "fiendpast", 9558, 6);

        //The Traitor’s Path 8493
        if (!Story.QuestProgression(8493))
        {
            Core.EnsureAccept(8493);
            Story.MapItemQuest(8493, "fiendpast", 9559);
            Core.HuntMonster("fiendpast", "Nation Defector", "Forces Defeated", 12);
            Core.HuntMonster("fiendpast", "Proto-Legion Knight", "Dage's Key");
            Core.EnsureComplete(8493);
        }

        //Defeat Dage the Lich 8494
        Story.KillQuest(8494, "fiendpast", "Dage the Lich");



    }
}