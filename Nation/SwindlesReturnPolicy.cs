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

        DoSwindlesReturnPolicy(Bot.Config!.Get<RewardsSelection>("RewardSelect"));

        Core.SetOptions(false);
    }

    public void DoSwindlesReturnPolicy(RewardsSelection? reward = null, bool getAll = false)
    {
        Core.Logger($"Reward set to {(reward.HasValue ? reward.Value.ToString().Replace("_", " ") : null)}");

        if (reward == RewardsSelection.All || reward == null)
            getAll = true;

        if (!getAll)
        {
            ItemBase? item = Core.EnsureLoad(7551).Rewards.Find(x => x.Name == reward!.Value.ToString().Replace("_", " "));
            if (item == null)
            {
                Core.Logger($"Reward with name {reward!.Value.ToString().Replace("_", " ")} not found in Quest Rewards.");
                return;
            }
            Nation.SwindleReturn(item.Name, item.MaxStack); // Fix the argument here
        }
        else
        {
            foreach (ItemBase thing in Core.EnsureLoad(7551).Rewards)
            {
                if (Core.CheckInventory(thing.Name, thing.MaxStack))
                    continue;

                Nation.SwindleReturn(thing.Name, thing.MaxStack); // Fix the argument here
            }
        }

        Core.CancelRegisteredQuests();
    }

    public enum RewardsSelection
    {
        Dark_Crystal_Shard = 4770,
        Diamond_of_Nulgath = 4771,
        Gem_of_Nulgath = 6136,
        Blood_Gem_of_the_Archfiend = 22332,
        Tainted_Gem = 4769,
        All = 0
    }

}


