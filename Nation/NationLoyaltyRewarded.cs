/*
name: Nation Loyalty Rewarded
description: Does the Nation Loyalty Rewarded Quest to max the quest rewards.
tags: nation loyalty rewarded, nulgath, nation, dark crystal shard, diamond of nulgath, diamond badge of nulgath
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/CoreAdvanced.cs

using System.Linq;
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class NationLoyaltyRewarded
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreNation Nation = new();
    public CoreAdvanced Adv = new();




    public void ScriptMain(IScriptInterface bot)
    {
        var questRewards = Core.QuestRewards(4749);
        var acceptRequirements = Core.EnsureLoad(4749)?.AcceptRequirements;

        Core.BankingBlackList.AddRange(
            (questRewards != null ? questRewards.Select(item => item.ToString()) : Enumerable.Empty<string>())
            .Concat(acceptRequirements != null ? acceptRequirements.Select(item => item.ToString()) : Enumerable.Empty<string>()));

        Core.SetOptions();

        FarmQuest(Core.QuestRewards(4749));

        Core.SetOptions(false);
    }

    public void FarmQuest(string[]? farmItems = null, int quantity = 0)
    {
        // Process all items if farmItems is null
        if (farmItems == null)
        {
            var allRewards = Core.EnsureLoad(4749).Rewards;
            foreach (var item in allRewards)
            {
                NLR(new[] { item.Name }, quantity == 0 ? (Core.EnsureLoad(4749)?.Rewards.Find(x => x.Name == item.Name)?.MaxStack ?? 0) : quantity);
            }
        }
        else
        {
            // Process specified items
            foreach (var farmItem in farmItems)
            {
                Core.AddDrop((farmItem ?? Core.EnsureLoad(4749).Rewards.FirstOrDefault()?.ID.ToString())!);
            }

            // Required items to start quest;
            Nation.NationRound4Medal();
            Nation.FarmUni13(1);

            // Process each specified item
            foreach (var farmItem in farmItems)
            {
                var reward = Core.EnsureLoad(4749).Rewards.Find(x => x.Name == farmItem);
                if (reward != null && !Bot.Inventory.IsMaxStack(reward.ID))
                    NLR(new[] { farmItem }, quantity == 0 ? reward.MaxStack : quantity);
            }

        }
    }


    public void NLR(string[]? items = null, int quantity = 1)
    {
        // Rule 2: Fix nullable
        if (items == null || items.Any(item => item == null || Core.CheckInventory(item, quantity)))
        {
            Core.Logger(items == null ? "Items Null" : $"{string.Join(", ", items)} x{quantity} Found! Returning...");
            return;
        }

        foreach (var item in items)
        {
            if (item != null)
            {
                Core.FarmingLogger(item, quantity);
            }
        }

        Core.EquipClass(ClassType.Solo);

        // Nation Loyalty Rewarded 4749
        while (!Bot.ShouldExit && items.Any(item => item != null && !Core.CheckInventory(item, quantity)))
        {
            Core.EnsureAccept(4749);
            Core.EquipClass(ClassType.Solo);
            while (!Bot.ShouldExit && !Core.CheckInventory(33257))
                Core.KillMonster("dflesson", "r12", "Right", "Fluffy the Dracolich", log: false);
                
            Core.HuntMonster("aqlesson", "Carnax", "Carnax Eye", log: false);
            Core.HuntMonster("deepchaos", "Kathool", "Kathool Tentacle", log: false);
            Core.HuntMonster("lair", "Red Dragon", "Red Dragon's Fang", log: false);
            Core.HuntMonster("bloodtitan", "Blood Titan", "Blood Titan's Blade", log: false);

            Core.EquipClass(ClassType.Farm);
            Core.KillMonster("tercessuinotlim", "m2", "Left", "*", "Defeated Makai", 25, false, false);

            foreach (var item in items)
            {
                if (item != null)
                {
                    Bot.Wait.ForPickup(item);
                }
            }
            Core.EnsureComplete(4749);
        }
    }
}
