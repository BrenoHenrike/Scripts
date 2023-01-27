/*
name: Creepy Undead Monkey
description: This will do the Creepy Undead Monkey quest to obtain all of the reward items.
tags: creepy-undead-monkey, black-friday, seasonal
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/IsleOfFotia/CoreIsleOfFotia.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
// using Skua.Core.Options;

public class CreepyUndeadMonkeyQuest
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreIsleOfFotia Fotia => new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.Add("Twisted Monkey Paw");
        Core.SetOptions();

        RandomReward();

        Core.SetOptions(false);
    }

    private void RandomReward()
    {
        if (!Core.IsMember && !CalculateFriday13())
        {
            Core.Logger("You must be Member or wait until a week before Friday the 13th to access /twig.");
            return;
        }

        Fotia.UnderRealm();

        string[] QuestRewards = Core.QuestRewards(4106);
        Bot.Drops.Add(QuestRewards);
        int i = 0;

        if (!Core.CheckInventory("Golden Bough"))
        {
            Core.AddDrop("Golden Bough");
            Core.EquipClass(ClassType.Farm);

            Core.EnsureAccept(3010);
            Core.HuntMonster("UnderRealm", "Underworld Soul", "Souls Released", 8);
            Core.EnsureComplete(3010);
        }

        Bot.Quests.UpdateQuest(3010);
        Core.RegisterQuests(4106);
        foreach (string Reward in QuestRewards)
        {
            if (Core.CheckInventory(Reward, toInv: false))
            {
                Core.Logger($"{Reward} found.");
                continue;
            }

            Core.FarmingLogger(Reward, 1);
            while (!Bot.ShouldExit && !Core.CheckInventory(Reward, toInv: false))
            {
                Core.EquipClass(ClassType.Farm);
                Core.HuntMonster("alliance", "Good Soldier", "Good Soldier's Face");
                Core.HuntMonster("alliance", "Evil Soldier", "Evil Soldier's Skull");
                Core.HuntMonster("neverlore", "Whablobble", "Whablobble Tongue");
                Core.HuntMonster("thespan", "Minx Fairy", "Minx Fairy Wings");
                Core.HuntMonster("battlecon", "BrutalCorn", "Evil Con Corny");
                Core.HuntMonster("crossroads", "Lemurphant", "Lemurphant's Peanuts");
                Core.HuntMonster("arcangrove", "Gorillaphant", "Fresh Gorilla Paw");
                Core.HuntMonster("arcangrove", "Gorillaphant", "Bananas in pajamas");
                Core.EquipClass(ClassType.Solo);
                Core.HuntMonster("battlefowl", "Chickencow", "Chickencow Head");
                Core.HuntMonster("mafic", "Scoria Serpent", "Scoria Serpent Charmer");
                Core.HuntMonster("underrealm", "Grief", "Grief's Tears");
                Core.HuntMonster("deepchaos", "Kathool", "Kathool... All of him");
                Core.HuntMonster("twig", "Sweetish Fish", "Candy from a Sweetish Fish");

                i++;

                if (i % 5 == 0)
                    Core.ToBank(QuestRewards);
            }
        }
    }

    bool CalculateFriday13()
        => new DateTime(DateTime.Now.Year, DateTime.Now.Month, 13).DayOfWeek == DayOfWeek.Friday && DateTime.Now.Day >= 5;
}
