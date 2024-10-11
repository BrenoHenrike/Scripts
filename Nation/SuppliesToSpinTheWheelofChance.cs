/*
name: SuppliesToSpinTheWheelofChance
description: Do "Supplies to Spin the Wheel" [*or* swindles bilk quests if u have it avaible.]
tags: swindles return policy, supplies to spin the wheel, swindles bilk, the assistant, nulgath, nation, supplies, ultra alteon, escherion
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;
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

        Dictionary<string, int> itemQuantities = new()
            {
                { "Tainted Gem", 1000 },
                { "Dark Crystal Shard", 1000 },
                { "Diamond of Nulgath", 1000 },
                { "Gem of Nulgath", 1000 },
                { "Blood Gem of the Archfiend", 100 },
                { "Receipt of Swindle", 100 }
            };


        string? SwindlesReturnItem = Bot.Config!.Get<SwindlesReturnItem>("SwindlesReturnItem").ToString().Replace('_', ' ');
        string? SuppliesItem = Bot.Config.Get<SuppliesReward>("SuppliesReward").ToString().Replace('_', ' ');
        int quantity;

        if (SwindlesReturnItem == "All")
        {
            SwindlesReturnItem = null;
            SuppliesItem = null;
        }

        Bot.Log($"Item Selected:\nSwindlesReturnItem: {SwindlesReturnItem ?? "All"}\nSuppliesItem: {SuppliesItem ?? "All"}\nIf SwindlesReturnItem is \"All,\" the bot will maximize all rewards from Swindles Return.");
        Nation.Supplies(SuppliesItem, Bot.Quests.EnsureLoad(2857).Rewards.FirstOrDefault(x => x != null && x.Name == SuppliesItem).MaxStack, Bot.Config!.Get<bool>("UltraAlteon"), Bot.Config!.Get<bool>("KeepVoucher"), Bot.Config!.Get<bool>("AssistantDuring"), SwindlesReturnItem);

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
