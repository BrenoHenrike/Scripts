/*
name: SwindlesReturnPolicy
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;
using Skua.Core.Options;

public class SwindlesReturnPolicy
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreNation Nation = new();

    public string OptionsStorage = "SwindlesReturnPolicy";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
{
    CoreBots.Instance.SkipOptions,
    new Option<RewardsSelection>("RewardSelect", "Choose Your Quest Reward", "Select Your Quest Reward for Swindle's Return Policy.", RewardsSelection.All)
};

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nation.Receipt);
        Core.SetOptions();

        RewardsSelection rewardSelect = Bot.Config?.Get<RewardsSelection>("RewardSelect") ?? RewardsSelection.All;
        DoSwindlesReturnPolicy(rewardSelect);

        Core.SetOptions(false);
    }

    public void DoSwindlesReturnPolicy(RewardsSelection? reward = null, bool getAll = false)
    {
        Core.Logger($"Reward set to {(reward.HasValue ? reward.Value.ToString() : "null")}");

        if (reward == RewardsSelection.All)
            getAll = true;

        Quest rewardQuest = Core.EnsureLoad(7551);
        if (rewardQuest == null)
        {
            Core.Logger("Quest with ID 7551 not found.");
            return;
        }

        string? targetItemName = reward.HasValue ? reward.Value.ToString().Replace("_", " ") : null;

        if (!getAll)
        {
            if (reward == null)
            {
                Core.Logger("Reward is null. Please select a valid reward.");
                return;
            }

            ItemBase? item = rewardQuest.Rewards.Find(x => x.Name == targetItemName);
            if (item == null)
            {
                Core.Logger($"Reward with name {targetItemName} not found in Quest Rewards.");
                return;
            }

            Core.AddDrop(item.ID);

            if (Core.CheckInventory(item.Name, item.MaxStack))
                return;

            SwindleReturn(reward, item.Name, item.MaxStack); // Fix the argument here
        }
        else
        {
            foreach (ItemBase thing in rewardQuest.Rewards)
            {
                Core.AddDrop(thing.ID);

                if (Core.CheckInventory(thing.Name, thing.MaxStack))
                    continue;

                SwindleReturn(reward, thing.Name, thing.MaxStack); // Fix the argument here
            }
        }

        Core.CancelRegisteredQuests();
    }


    public ItemBase? SwindleReturn(RewardsSelection? reward = null, string? itemName = null, int quantity = 1)
    {
        Core.RegisterQuests(641, 907); // Quest IDs for the pets you want to register

        string? targetItemName = reward.HasValue ? reward.Value.ToString().Replace("_", " ") : itemName;

        while (Bot.Inventory.GetQuantity(targetItemName ?? "") < quantity)
        {
            if (reward != null)
                DoSwindlesReturnPolicy(reward, true);
            else
                DoSwindlesReturnPolicy(RewardsSelection.All, true);

            ItemBase? item = Core.EnsureLoad(7551)?.Rewards.Find(x => x.Name == targetItemName);
            if (item != null)
            {
                Core.AddDrop(item.ID);

                if (!Core.CheckInventory(item.Name, item.MaxStack))
                {
                    return item;
                }
            }
            else
            {
                // Handle the case where 'item' is null (item not found)
                Core.Logger($"Item with name {targetItemName} not found in Quest Rewards.");
            }
        }

        return null;
    }






    public enum RewardsSelection
    {
        Dark_Crystal_Shard = 4770,
        Diamond_of_Nulgath = 4771,
        Gem_of_Nulgath = 6136,
        Blood_Gem_of_the_Archfiend = 22332,
        Tainted_Gem = 4769,
        All = 9999
    }

}


