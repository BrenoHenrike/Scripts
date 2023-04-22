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

        DoSwindlesReturnPolicy(Bot.Config.Get<RewardsSelection>("RewardSelect"));

        Core.SetOptions(false);
    }

    public void DoSwindlesReturnPolicy(RewardsSelection reward, bool getAll = false)
    {
        Core.Logger($"Reward set to {Bot.Config.Get<RewardsSelection>("RewardSelect").ToString()}");

        if ((int)Bot.Config.Get<RewardsSelection>("RewardSelect") == 9999)
            getAll = true;
        ItemBase item = Core.EnsureLoad(7551).Rewards.Find(x => x.ID == (int)Bot.Config.Get<RewardsSelection>("RewardSelect"));

        if (!getAll)
        {
            item = Core.EnsureLoad(7551).Rewards.Find(x => x.ID == (int)Bot.Config.Get<RewardsSelection>("RewardSelect"));
            Core.AddDrop(item.ID);
            if (item == null)
            {
                Core.Logger($"{item.Name} not found in Quest Rewards");
                return;
            }
            if (Core.CheckInventory(item.Name, item.MaxStack))
                return;
            Nation.SwindleReturn(item.Name, item.MaxStack);
        }
        else if (getAll)
        {
            foreach (ItemBase Thing in Core.EnsureLoad(7551).Rewards)
            {
                Core.AddDrop(Thing.ID);
                if (Core.CheckInventory(Thing.Name, Thing.MaxStack))
                    continue;
                Nation.SwindleReturn(Thing.Name, Thing.MaxStack);
            }
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
    };
}
