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
    public CoreFarms Farm = new CoreFarms();
    public CoreNation Nation = new();

    public string OptionsStorage = "SwindlesReturnPolicy";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
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

    public void DoSwindlesReturnPolicy(RewardsSelection? reward, bool getAll = false)
    {
        Core.Logger($"Reward set to {reward?.ToString() ?? "null"}");

        if (reward == RewardsSelection.All)
            getAll = true;

        Quest rewardQuest = Core.EnsureLoad(7551);
        if (rewardQuest == null)
        {
            Core.Logger($"Quest with ID 7551 not found.");
            return;
        }

        if (!getAll)
        {
            if (reward == null)
            {
                Core.Logger($"Reward is null. Please select a valid reward.");
                return;
            }

            ItemBase? item = rewardQuest.Rewards.Find(x => x.ID == (int)reward);
            if (item == null)
            {
                Core.Logger($"Reward with ID {(int)reward} not found in Quest Rewards.");
                return;
            }

            Core.AddDrop(item.ID);

            if (Core.CheckInventory(item.Name, item.MaxStack))
                return;

            Nation.SwindleReturn(item.Name, item.MaxStack);
        }
        else if (getAll)
        {
            foreach (ItemBase thing in rewardQuest.Rewards)
            {
                Core.AddDrop(thing.ID);

                if (Core.CheckInventory(thing.Name, thing.MaxStack))
                    continue;

                Nation.SwindleReturn(thing.Name, thing.MaxStack);
            }
        }
    }


    public void SwindleReturn(string itemName, int quantity)
    {
        while (Bot.Inventory.GetQuantity(itemName) < quantity)
        {
            DoSwindlesReturnPolicy(RewardsSelection.All, true);
            Core.RegisterQuests(641, 907); // Quest IDs for the pets you want to register
        }
    }

    public void SwindleReturn(RewardsSelection reward, int quantity)
    {
        while (Bot.Inventory.GetQuantity(reward.ToString().Replace("_", " ")) < quantity)
        {
            DoSwindlesReturnPolicy(reward, true);
            Core.RegisterQuests(641, 907); // Quest IDs for the pets you want to register
        }
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


