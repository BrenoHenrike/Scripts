/*
name: DeathKnightLootQuest
description: This script will farm everything in DeathKnightLootChestQuest id: 6509 in /ivoliss
tags: death, knight, loot, chest, fallendeathknight, accoutrements
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Story/AranxQuests.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
// using Skua.Core.Options;

public class DeathKnightLootChestQuest
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    
    private AranxQuests AR = new();

    private CoreAdvanced Adv = new();

    private CoreFarms Cf = new();



    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        DoQuest();
        RandomReward(6509);

        Core.SetOptions(true);
    }

    public void DoQuest()
    {

        QuestsIfNeeded();

    }

    private void RandomReward(int questID = 6509)
    {
        List<ItemBase> RewardOptions = Core.EnsureLoad(questID).Rewards;

        foreach (ItemBase item in RewardOptions)
            Bot.Drops.Add(item.Name);

        string[] QuestRewards = RewardOptions.Select(x => x.Name).ToArray();

        Core.EquipClass(ClassType.Solo);
        //Adv.BestGear(RacialGearBoost.Elemental);
        Core.RegisterQuests(questID);
        foreach (ItemBase Reward in RewardOptions)
        {
            if (Core.CheckInventory(Reward.Name, toInv: false))
                Core.Logger($"{Reward.Name} Found.");
            else
            {
                Core.FarmingLogger(Reward.Name, 1);
                while (!Bot.ShouldExit && !Core.CheckInventory(Reward.Name, toInv: false))
                {
                    Core.HuntMonster("ivoliss", "ivoliss", "Loot Key", 6, true, false);
                }
            }
        }
    }



    public void QuestsIfNeeded()
    {
        AR.StoryLine();
    }

    void RequiredItems(params string[] items)
    {
        if (Core.CheckInventory(items) || items == null)
            return;
        else Core.Logger("Required Items not found, Stopping", stopBot: true);
    }
}
