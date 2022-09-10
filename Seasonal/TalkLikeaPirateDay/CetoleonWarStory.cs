//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/TalkLikeaPirateDay/HeartOfTheSeaStory.cs
using Skua.Core.Interfaces;

public class CetoleonWarStory
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public HeartOfTheSeaStory HeartOfTheSeaStory = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        CetoleonWar();

        Core.SetOptions(false);
    }

    public void CetoleonWar()
    {
        if (Core.isCompletedBefore(6530))
            return;

        HeartOfTheSeaStory.HeartOfTheSea();

        Story.PreLoad();
        //Grislyfang Doubloons 6523
        Story.KillQuest(6523, "CetoleonWar", "Grislyfang Pirate");

        //Stop the Wreckers, Fix the Ship 6524
        if (!Story.QuestProgression(6524))
        {
            Core.EnsureAccept(6524);
            Core.HuntMonster("CetoleonWar", "Grislyfang Wrecker", "Hammer");
            Core.HuntMonster("CetoleonWar", "Grislyfang Wrecker", "Plank", 5);
            Core.HuntMonster("CetoleonWar", "Grislyfang Wrecker", "Nails", 25);
            Core.EnsureComplete(6524);
        }

        //Boil the Engineers 6525
        Story.KillQuest(6525, "CetoleonWar", "Grislyfang Engineer");

        //Grab the Gunpowder 6526
        Story.KillQuest(6526, "CetoleonWar", "Grislyfang Musketeer");

        //Saw THIS! 6527
        Story.KillQuest(6527, "CetoleonWar", "Captain Sawtooth");

        //Remove the Tentacles 6528
        Story.KillQuest(6528, "CetoleonWar", "Nomura's Sting");

        //Ugh, Roaches 6529
        Story.KillQuest(6529, "CetoleonWar", "Sea Roach");

        //Gel the Jellyfish 6530
        Story.KillQuest(6530, "CetoleonWar", "Nomura");

    }
}
