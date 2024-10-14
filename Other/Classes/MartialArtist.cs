/*
name: Martial Artist (Class)
description: This script will get Martial Artist class.
tags: Martial Artist, early game, class
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Quests;

public class MartialArtist
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm => new();
    public CoreAdvanced Adv = new();
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetMartialArtist();

        Core.SetOptions(false);
    }

    public void GetMartialArtist(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Martial Artist"))
        {
            if (rankUpClass)
                Adv.RankUpClass("Martial Artist");
            return;
        }

        Core.AddDrop("Martial Artist");
        Core.AddDrop(88662, 88661, 88660);

        if (!Core.CheckInventory("Martial Artist"))
        {
            // Lvl 65 is required
            Farm.Experience(65);

            // 9922 | Join My Dojo
            Core.ChainComplete(9922);

            // 9923 | 500 Punches and 500 Kicks
            if (!Story.QuestProgression(9923))
            {
                Core.Logger("Quest is required, we'll stack mats via \"Deathly Slow Start [9933]\" After");
                Core.HuntMonsterQuest(9923,
("nexus", "Frogzard", ClassType.Farm),         // Frogzards Defeated (500): Join nexus, kill Frogzards
                    ("arcangrove", "Gorillaphant", ClassType.Farm), // Gorillaphants Defeated (500): Join arcangrove, kill Gorillaphants
                    ("etherwardes", "Water Dragon Warrior", ClassType.Farm)     // Dragons Defeated (500): Join etherwardes, kill dragons
);
            }

            //stack and then turn in to  get all required mats for the rest...
            Core.EquipClass(ClassType.Solo);
            Core.EnsureAccept(Core.IsMember ? 9911 : 9902);
            Core.HuntMonster("dreadfight", "Dreadhaven General", "Dreadhaven General's Soul Fragment", Core.IsMember ? 200 : 400, isTemp: false);
            Bot.Quests.UpdateQuest(9607);
            Core.HuntMonster("hakuwar", "Zakhvatchik", "Zakhvatchik's Soul Fragment", Core.IsMember ? 200 : 400, isTemp: false);
            Core.HuntMonster("towerofdoom5", "Creel", "Creel's Soul Fragment", Core.IsMember ? 200 : 400, isTemp: false);

            Core.EnsureCompleteMulti(Core.IsMember ? 9911 : 9902);
            foreach (int i in new[] { 88662, 88661, 88660 })
                Bot.Wait.ForPickup(i);

            Core.ChainComplete(9933);

            foreach (int Q in Core.FromTo(9924, 9927))
            {
                // 9933 | Deathly Slow Start
                // 9924 | Discount Diploma
                // 9925 | One Million Miles Searching
                // 9926 | Ughhhhhh
                // 9927 | Work Smarter, Not Harder
                Core.ChainComplete(Q);
            }
        }

        // Ensure quest is complete before buying
        if (!Story.QuestProgression(9928))
        {
            Story.KillQuest(9928, "hakuvillage", "The Master");
            Bot.Wait.ForQuestComplete(9928);
        }

        Core.BuyItem("hakuvillage", 2490, "Martial Artist");
        Bot.Wait.ForPickup("Martial Artist");

        if (rankUpClass)
            Adv.RankUpClass("Martial Artist");
    }

    void GoldFarm(int Gold = 100000000)
    {
        if (Bot.Player.Gold >= Gold)
            return;

        while (!Bot.ShouldExit && Bot.Player.Gold <= Gold)
        {
            Core.HuntMonsterQuest(9933,
("dreadfight", "Dreadhaven General", ClassType.Solo), // Dreadhaven General's Soul Fragment (10): Join dreadfight, kill Dreadhaven General
                        ("hakuwar", "Zakhvatchik", ClassType.Solo),           // Zakhvatchik's Soul Fragment (10): Join hakuwar, kill Zakhvatchik (last room)
                        ("towerofdoom5", "Creel", ClassType.Solo)          // Creel's Soul Fragment (10): Join towerofdoom5, kill Creel (last room)
);
            Bot.Wait.ForQuestComplete(9933);
        }
    }
}


