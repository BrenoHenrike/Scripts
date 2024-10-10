/*
name: Void Warlock
description: This script farms the Void Warlock set from the [Tools for the Job] and [Corrupted Touch] quests
tags: void, warlock, tools, job, corrupted, touch, quest, rewards, tools for the job, corrupted touch, voidwarlock
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Nation/Various/EnchantedNulgathNationHouse.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Good/BLOD/CoreBLOD.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class VoidWarlock
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreNation Nation = new();
    public EnhancedNulgathNationHouse ENNH = new();


    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetWarlock();

        Core.SetOptions(false);
    }

    public void GetWarlock(string? singleToolReward = null, string? singleTouchReward = null)
    {
        // Required to Accept/turn in Quest:
        ENNH.GetENNH();

        // Load rewards for both quests (6683 - Tools, 6684 - Touch)
        List<ItemBase> LoadRewards(int questID, string? singleReward)
        {
            var rewards = Core.EnsureLoad(questID).Rewards;
            return singleReward == null ? rewards : rewards.Where(r => r.Name == singleReward).ToList();
        }

        List<ItemBase> ToolsRewards = LoadRewards(6683, singleToolReward);
        List<ItemBase> TouchRewards = LoadRewards(6684, singleTouchReward);

        // Add items to drop
        ToolsRewards.ForEach(item => Bot.Drops.Add(item.Name));
        TouchRewards.ForEach(item => Bot.Drops.Add(item.Name));

        Bot.Drops.Add(Nation.bagDrops);
        Bot.Drops.Add("Brittney's Winter Diamond");

        // Handle [Tools for the Job] Quest
        Core.Logger("Starting [Tools for the Job] Quest");
        foreach (ItemBase reward in ToolsRewards)
        {
            if (Core.CheckInventory(reward.ID, toInv: false))
                continue;

            Core.FarmingLogger(reward.Name, 1);
            Core.EnsureAccept(6683);
            Core.HuntMonster("northlands", "Aisha's Drake", "Brittney's Winter Diamond", 1, false);
            Nation.FarmUni13(2);
            Nation.FarmVoucher(false);
            Nation.FarmBloodGem(90);
            Nation.SwindleBulk(100);
            Core.EnsureComplete(6683, reward.ID);
            Bot.Wait.ForQuestComplete(6683);
            Core.ToBank(reward.ID);
        }
        Core.Logger("All drops acquired from [Tools for the Job] Quest");

        // Handle [Corrupted Touch] Quest
        Core.Logger("Starting [Corrupted Touch] Quest");
        foreach (ItemBase reward in TouchRewards)
        {
            if (Core.CheckInventory(reward.ID, toInv: false))
                continue;

            Core.FarmingLogger(reward.Name, 1);
            Core.EnsureAccept(6684);
            Nation.FarmUni13(1);
            Nation.FarmVoucher(true);
            Nation.FarmDiamondofNulgath(75);
            Nation.FarmGemofNulgath(100);
            Nation.SwindleBulk(75);
            Nation.ApprovalAndFavor(1000, 0);
            Core.HuntMonster("northlands", "Aisha's Drake", "Brittney's Winter Diamond", 1, false);
            Core.EnsureComplete(6684, reward.ID);
            Bot.Wait.ForQuestComplete(6684);
            Core.ToBank(reward.ID);
        }
        Core.Logger("All drops acquired from [Corrupted Touch] Quest");
    }

}

