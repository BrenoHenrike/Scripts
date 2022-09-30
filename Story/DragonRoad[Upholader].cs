//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;
using System.Collections.Generic;

public class DragonRoad
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
        IDictionary<int, string> Upholder = new Dictionary<int, string>();
        Upholder.Add(22, "ip9");  //8th Upholder
        Upholder.Add(15, "ip11"); //9th Upholder
        Upholder.Add(8, "ip14");  //10th Upholder
        Upholder.Add(10, "ip16"); //11th Upholder
        Upholder.Add(12, "ip17"); //12th Upholder
        Upholder.Add(18, "ip18"); //13th Upholder
        Upholder.Add(2, "ip20");  //14th Upholder

        foreach (KeyValuePair<int, string> Id in Upholder)
        {
            if (!Core.HasAchievement(Id.Key, Id.Value))
                return;
            else
                break;
        }

        if (Core.isCompletedBefore(4547))
            return;

        Story.PreLoad(this);

        //Obtain the DragonStar Radar 4534
        if (!Story.QuestProgression(4534) || !Core.CheckInventory("Dragon Star Radar") || !Core.CheckInventory("DragonStar Map"))
        {
            Core.AddDrop("Dragon Star Radar", "DragonStar Map");
            Core.EnsureAccept(4534);
            Core.GetMapItem(3750, 1, "BattleonTown");
            Core.GetMapItem(3751, 1, "Arcangrove");
            Core.GetMapItem(3752, 1, "Dragonrune");
            Core.EnsureComplete(4534);
        }

        //Find the First DragonStar Crystal 4535
        if (!Story.QuestProgression(4535))
        {
            Core.EnsureAccept(4535);
            Core.GetMapItem(3761, 1, "DragonRoad");
            Core.HuntMonster("Natatorium", "Anglerfish", "First DragonStar Found");
            Core.EnsureComplete(4535);
        }

        //Find the Second DragonStar Crystal  4536
        if (!Story.QuestProgression(4536))
        {
            Core.EnsureAccept(4536);
            Core.GetMapItem(3754, 1, "OnslaughtTower");
            Core.GetMapItem(3762, 1, "DragonRoad");
            Core.EnsureComplete(4536);
        }

        //Find the Third DragonStar Crystal  4537
        if (!Story.QuestProgression(4537))
        {
            Core.EnsureAccept(4537);
            Core.GetMapItem(3756, 1, "XanTown");
            Core.GetMapItem(3763, 1, "DragonRoad");
            Core.EnsureComplete(4537);
        }

        //Find the Fourth DragonStar Crystal 4538
        Story.KillQuest(4538, "Lair", new[] { "Onyx Lava Dragon", "Onyx Lava Dragon" });

        //Find the Fifth DragonStar Crystal 4539
        if (!Story.QuestProgression(4539))
        {
            Core.EnsureAccept(4539);
            Core.GetMapItem(3765, 1, "DragonRoad");
            Core.HuntMonster("EarthStorm", "Diamond Golem", "Fifth DragonStar Found");
            Core.EnsureComplete(4539);
        }


        //Find the Sixth DragonStar Crystal  4540
        if (!Story.QuestProgression(4540))
        {
            Core.EnsureAccept(4540);
            Core.GetMapItem(3758, 1, "Bamboo");
            Core.GetMapItem(3766, 1, "DragonRoad");
            Core.EnsureComplete(4540);
        }

        //Raid the Bandits 4541
        Story.KillQuest(4541, "DragonRoad", "Desert Wolf Bandit");

        //HAMmer the Fabled Oinklong 4542
        Story.KillQuest(4542, "DragonRoad", "Oinklong");

        //Defeat the Great Horccolo 4543
        Story.KillQuest(4543, "DragonRoad", "Horccolo");

        //Eliminate the Legion Cyborgs 4544
        Story.KillQuest(4544, "DragonRoad", new[] { "Cyborg 71", "Cyborg 81" });

        //Pulverize Majic Guu  4545
        Story.KillQuest(4545, "DragonRoad", "Majic Guu");

        //Cleaning up the Guu  4546
        Story.MapItemQuest(4546, "DragonRoad", 3759, 10);

        //Defeat Super Dragon Twig! 4547
        Story.KillQuest(4547, "DragonRoad", "Super Dragon Twig");

    }
}
