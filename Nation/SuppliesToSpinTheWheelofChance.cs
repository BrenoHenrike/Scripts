/*
name: SuppliesToSpinTheWheelofChance
description: Do "Supplies to Spin the Wheel" [*or* swindles bilk quests if u have it avaible.]
tags: swindles return policy, supplies to spin the wheel, swindles bilk, the assistant, nulgath, nation, supplies, ultra alteon, escherion
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;
using Skua.Core.Options;

public class SuppliesToSpinTheWheelofChance
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private static CoreBots Core => CoreBots.Instance;
    public CoreNation Nation = new();

    public string OptionsStorage = "SuppliesOptions";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        CoreBots.Instance.SkipOptions,
        new Option<SwindlesReturnItem>("SwindlesReturnItem", "SwindlesReturnItem", "pick the reward for the \"Swindles Return\" Quest", SwindlesReturnItem.All),
        new Option<SuppliesReward>("SuppliesReward", "SuppliesReward", "pick the reward for the \"Supplies to spin the wheel\" Quest", SuppliesReward.All),
        new Option<bool>("AssistantDuring", "Do: \"The Assistant\" during?", "Do the quest: [The Assistant], (requires alota gold, that you will get from the vouchers of nulgath (mem)) during this.", false),
        new Option<bool>("UltraAlteon", "Kill \"UltraAlteon\"", "Instead of \"Escherion\" or bamboozle, do \"Ultra Alteon\"?", false),
        new Option<bool>("KeepVoucher", "Keep Voucher?", "Keep Voucher? (false = gold)", false),
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nation.SuppliesRewards.Concat(Nation.SwindlesReturnRewards));
        Core.SetOptions();

        DoSupplies();

        Core.SetOptions(false);
    }

    public void DoSupplies()
    {
        // Set Config Options & Convert Enum into string for display
        string? SwindlesReturnItem = Bot.Config!.Get<SwindlesReturnItem>("SwindlesReturnItem").ToString().Replace('_', ' ');
        string? SuppliesItem = Bot.Config.Get<SuppliesReward>("SuppliesReward").ToString().Replace('_', ' ');

        // Check to see if we're maxing 1 or all items for reward sets.
        SuppliesItem = SuppliesItem == "All" ? null : SuppliesItem;
        SwindlesReturnItem = SwindlesReturnItem == "All" ? null : SwindlesReturnItem;

        // Log Settings
        Bot.Log($"Item(s):\n" +
            $"{(SuppliesItem == "All" ? "SuppliesItem is \"All,\" the bot will maximize all rewards from Supplies." : string.Empty)}\n" +
            $"{(SwindlesReturnItem == "All" ? "SwindlesReturnItem is \"All,\" the bot will maximize all rewards from Swindles Return." : string.Empty)}\n" +
            $"Members method?: [{Bot.Player.IsMember}]");

        // Load quests
        Quest Supplies = Bot.Quests.EnsureLoad(2857);
        Quest SwindlesReturn = Bot.Quests.EnsureLoad(7551);

        // Create a list to hold combined rewards from Supplies and SwindlesReturn
        List<ItemBase> combinedRewards = new();

        // Add unique items from Supplies that are valid rewards
        combinedRewards.AddRange(Supplies.Rewards
            .Where(r => r != null && Nation.SuppliesRewards.Contains(r.Name))
            .DistinctBy(r => r.ID)); // Remove duplicates based on item ID

        // Add unique items from SwindlesReturn that are valid rewards
        combinedRewards.AddRange(SwindlesReturn.Rewards
            .Where(r => r != null && Nation.SwindlesReturnRewards.Contains(r.Name))
            .DistinctBy(r => r.ID)); // Remove duplicates based on item ID

        // Remove duplicates from combinedRewards based on item ID
        combinedRewards = combinedRewards.DistinctBy(r => r.ID).ToList();

        foreach (ItemBase item in combinedRewards)
        {
            // Skip if the item is already in the inventory and we have the max stack
            if (Core.CheckInventory(item.ID, item.MaxStack))
                continue;

            // Determine the SwindlesReturnItem if it's null
            SwindlesReturnItem ??= SwindlesReturn.Rewards
                .Where(r => r != null && !Bot.Inventory.Items.Concat(Bot.Bank.Items).Any(i => i.ID == r.ID) && r.Quantity < r.MaxStack)
                .Select(r => r.Name)
                .FirstOrDefault();

            // Determine the max stack values directly without null checks
            int suppliesMaxStack = SuppliesItem == "All"
                ? Supplies.Rewards.FirstOrDefault(x => x != null && x.Name == item.Name).MaxStack
                : Supplies.Rewards.FirstOrDefault(x => x != null && x.Name == SuppliesItem).MaxStack;

            int swindlesMaxStack = SwindlesReturnItem == "All"
                ? SwindlesReturn.Rewards.FirstOrDefault(x => x != null && x.Name == item.Name).MaxStack
                : SwindlesReturn.Rewards.FirstOrDefault(x => x != null && x.Name == SwindlesReturnItem).MaxStack;

            // Call the Nation.Supplies method with the correct parameters
            Nation.Supplies(
                SuppliesItem ?? item.Name, // Coalesce SuppliesItem to item.Name if it's null
                suppliesMaxStack, // Use determined max stack for Supplies
                Bot.Config!.Get<bool>("UltraAlteon"),
                Bot.Config!.Get<bool>("KeepVoucher"),
                Bot.Config!.Get<bool>("AssistantDuring"),
                SwindlesReturnItem ?? item.Name, // Use SwindlesReturnItem if set, otherwise use item.Name
                swindlesMaxStack // Use determined max stack for SwindlesReturn
            );
        }
    }

    public enum SwindlesReturnItem
    {
        All,
        Tainted_Gem,
        Dark_Crystal_Shard,
        Diamond_of_Nulgath,
        Gem_of_Nulgath,
        Blood_Gem_of_the_Archfiend,
        Receipt_of_Swindle
    }

    public enum SuppliesReward
    {
        All,
        Tainted_Gem,
        Dark_Crystal_Shard,
        Diamond_of_Nulgath,
        Voucher_of_Nulgath,
        Voucher_of_Nulgath_NonMem,
        Gem_of_Nulgath,
        Unidentified_10,
        Essence_of_Nulgath,
    }


}
