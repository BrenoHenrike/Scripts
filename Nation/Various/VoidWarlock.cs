/*
name: Void Warlock
description: This script farms the Void Warlock set from the [Tools for the Job] and [Corrupted Touch] quests
tags: void, warlock, tools, job, corrupted, touch, quest, rewards, tools for the job, corrupted touch, voidwarlock
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Nation\Various\EnchantedNulgathNationHouse.cs
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
        // Required to Accept/turnin Quest:
        ENNH.GetENNH();

        List<ItemBase> ToolsRewards;
        List<ItemBase> TouchRewards;
        if (singleToolReward == null)
            ToolsRewards = Core.EnsureLoad(6683).Rewards;
        else
            ToolsRewards = Core.EnsureLoad(6683).Rewards.Where(r => r.Name == singleToolReward).ToList();

        if (singleTouchReward == null)
            TouchRewards = Core.EnsureLoad(6684).Rewards;
        else
            TouchRewards = Core.EnsureLoad(6684).Rewards.Where(r => r.Name == singleTouchReward).ToList();

        foreach (ItemBase item in ToolsRewards)
            Core.AddDrop(item.Name);

        foreach (ItemBase item in TouchRewards)
            Core.AddDrop(item.Name);

        Core.AddDrop(Nation.bagDrops);
        Core.AddDrop("Brittney's Winter Diamond");

        Core.Logger("Starting [Tools for the Job] Quest");
        foreach (ItemBase reward in ToolsRewards)
        {
            if (Core.CheckInventory(reward.Name, toInv: false))
                return;

            Core.FarmingLogger(reward.Name, 1);
            Core.EnsureAccept(6683);
            Core.HuntMonster("northlands", "Aisha's Drake", "Brittney's Winter Diamond", 1, false);
            Nation.FarmUni13(2);
            Nation.FarmVoucher(false);
            Nation.FarmBloodGem(90);
            Nation.SwindleBulk(100);
            Core.EnsureComplete(6683, reward.ID);
            Core.JumpWait();
            Core.ToBank(reward.Name);
        }
        Core.Logger("All drops acquired from [Tools for the Job] Quest");

        Core.Logger("Starting [Corrupted Touch] Quest");
        foreach (ItemBase reward in ToolsRewards)
        {
            if (Core.CheckInventory(reward.Name, toInv: false))
                return;
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
            Core.JumpWait();
            Core.ToBank(reward.Name);
        }
        Core.Logger("All drops acquired from [Corrupted Touch] Quest");
    }
}

