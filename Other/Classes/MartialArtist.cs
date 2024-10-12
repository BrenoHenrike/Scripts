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
            Story.ChainQuest(9922);

            // 9923 | 500 Punches and 500 Kicks
            // 9933 | Deathly Slow Start
            // 9924 | Discount Diploma
            // 9925 | One Million Miles Searching
            // 9926 | Ughhhhhh
            // 9927 | Work Smarter, Not Harder
            foreach (Quest Q in Core.EnsureLoad(Core.FromTo(9923, 9927)))
            {
                // 9924 | Discount Diploma
                if (Q.ID == 9924)
                {
                    // 9933 Deathly Slow Start
                    if (!Story.QuestProgression(9924))
                    {
                        if (!Core.isCompletedBefore(9933))
                            Core.HuntMonsterQuest(9933, new (string? mapName, string? monsterName, ClassType classType)[] {
                        ("dreadfight", "Dreadhaven General", ClassType.Solo), // Dreadhaven General's Soul Fragment (10): Join dreadfight, kill Dreadhaven General
                        ("hakuwar", "Zakhvatchik", ClassType.Solo),           // Zakhvatchik's Soul Fragment (10): Join hakuwar, kill Zakhvatchik (last room)
                        ("towerofdoom5", "Creel", ClassType.Solo),            // Creel's Soul Fragment (10): Join towerofdoom5, kill Creel (last room)
                    }, log: true);

                        GoldFarm(1500000);
                        Core.BuyItem("alchemyacademy", 2115, "Gold Voucher 500k");
                        Core.ChainComplete(9936);
                    }
                }
                else if (!Story.QuestProgression(Q.ID))
                {
                    Core.HuntMonsterQuest(Q.ID, new (string? mapName, string? monsterName, ClassType classType)[] {
                    ("nexus", "Frogzard", ClassType.Farm),         // Frogzards Defeated (500): Join nexus, kill Frogzards
                    ("arcangrove", "Gorillaphant", ClassType.Farm), // Gorillaphants Defeated (500): Join arcangrove, kill Gorillaphants
                    ("etherwardes", "Water Dragon", ClassType.Farm),      // Dragons Defeated (500): Join etherwardes, kill dragons
                }, log: true);
                }
            }

            // Ensure quest is complete before buying
            if (!Story.QuestProgression(9928))
            {
                Story.KillQuest(9928, "hakuvillage", "The Master");
                Bot.Wait.ForQuestComplete(9928);
            }

            Core.BuyItem("hakuvillage", 2490, "Master Artist");
            Bot.Wait.ForPickup("Martial Artist");
        }

        if (rankUpClass)
            Adv.RankUpClass("Martial Artist");
    }

    void GoldFarm(int Gold = 100000000)
    {
        if (Bot.Player.Gold >= Gold)
            return;

        while (!Bot.ShouldExit && Bot.Player.Gold <= Gold)
        {
            Core.HuntMonsterQuest(9933, new (string? mapName, string? monsterName, ClassType classType)[] {
                        ("dreadfight", "Dreadhaven General", ClassType.Solo), // Dreadhaven General's Soul Fragment (10): Join dreadfight, kill Dreadhaven General
                        ("hakuwar", "Zakhvatchik", ClassType.Solo),           // Zakhvatchik's Soul Fragment (10): Join hakuwar, kill Zakhvatchik (last room)
                        ("towerofdoom5", "Creel", ClassType.Solo),            // Creel's Soul Fragment (10): Join towerofdoom5, kill Creel (last room)
                    }, log: true);
            Bot.Wait.ForQuestComplete(9933);
        }
    }
}


