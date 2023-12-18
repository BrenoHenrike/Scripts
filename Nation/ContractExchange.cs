/*
name: Contract Exchange
description: null
tags: bamblooze vs drudgen, contract exchange, pet, 
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;
using System.Collections.Generic;
using System.Linq;

public class ContractExchanging
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreNation Nation = new();

    public string OptionsStorage = "ContractExchange";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        CoreBots.Instance.SkipOptions,
        new Option<bool>("MaxorChoose", "Max bags or Choose", "Either choose to max the quest rewards or choose your reward manualy [true: max, false: choose]", true),
        new Option<ContractExchangeRewards>("RewardSelect", "Choose Your Quest Reward", "Select Your Quest Reward for Contract Exchange.", ContractExchangeRewards.All),
        new Option<bool>("Tainted Gem", "Farm Tainted Gem", "Farm \"Tainted Gem\" to max stack", true),
        new Option<bool>("Dark Crystal Shard", "Farm Dark Crystal Shard", "Farm \"Dark Crystal Shard\" to max stack", true),
        new Option<bool>("Gem of Nulgath", "Farm Gem of Nulgath", "Farm \"Gem of Nulgath\" to max stack", true),
        new Option<bool>("Blood Gem of the Archfiend", "Farm Blood Gem of the Archfiend", "Farm \"Blood Gem of the Archfiend\" to max stack", true)
    };

    string[] ContractExchangeItems = { "Drudgen the Assistant", "Tainted Gem", "Dark Crystal Shard", "Gem of Nulgath", "Blood Gem of the Archfiend" };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(ContractExchangeItems);
        Core.SetOptions();

        ContractExchange();

        Core.SetOptions(false);
    }

    public void ContractExchange()
    {
        if (!Core.CheckInventory("Drudgen the Assistant"))
        {
            Core.Logger("Missing: Drudgen the Assistant pet.", stopBot: true);
            return;
        }

        Core.AddDrop(ContractExchangeItems.Concat(Nation.bagDrops).ToArray());

        bool maxOrChoose = Bot.Config!.Get<bool>("MaxorChoose");
        bool rewardSelectAll = Bot.Config!.Get<ContractExchangeRewards>("RewardSelect") == ContractExchangeRewards.All;
        bool taintedGem = Bot.Config!.Get<bool>("Tainted Gem");
        bool darkCrystalShard = Bot.Config!.Get<bool>("Dark Crystal Shard");
        bool gemOfNulgath = Bot.Config!.Get<bool>("Gem of Nulgath");
        bool bloodGemOfArchfiend = Bot.Config!.Get<bool>("Blood Gem of the Archfiend");

        if (maxOrChoose)
            // Condition 1: ChooseReward must be set to All, and the last 4 options must be true
            if (!rewardSelectAll || !taintedGem || !darkCrystalShard || !gemOfNulgath || !bloodGemOfArchfiend)
                Core.Logger("Options conflict: Max bags is true, but ChooseReward must be set to All, and the last 4 options must be true. Setting MaxorChoose to false.", stopBot: true);

        foreach (ContractExchangeRewards reward in Enum.GetValues(typeof(ContractExchangeRewards)).Cast<ContractExchangeRewards>().Where(r => r != ContractExchangeRewards.All))
        {
            string? rewardName = Enum.GetName(typeof(ContractExchangeRewards), reward)?.Replace("_", " ");
            ItemBase? item = Core.EnsureLoad(870).Rewards.FirstOrDefault(x => x.Name == rewardName);

            if (item == null)
            {
                Core.Logger($"Item with name '{rewardName}' not found.", stopBot: true);
                continue;
            }

            if (!Bot.Config.Get<bool>(rewardName!))
                continue;

            // Condition 2: Max bags is true, but one of the last 4 options is false
            if (maxOrChoose && (!taintedGem || !darkCrystalShard || !gemOfNulgath || !bloodGemOfArchfiend))
                Core.Logger("Options conflict: Max bags is true, but one of the last 4 options is false.", stopBot: true);

            while (!Bot.ShouldExit && !Core.CheckInventory(item.Name, item.MaxStack))
                Nation.FarmContractExchage(rewardName, item.MaxStack);
        }
    }



}
