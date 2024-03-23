/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Hollowborn/CoreHollowborn.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Legion/Revenant/CoreLR.cs
//cs_include Scripts/Story/QueenofMonsters/CoreQOM.cs


//cs_include Scripts/Legion\MergeShops\UndeadLegionMerge.cs
//cs_include Scripts/Legion\Various\SoulSand.cs
//cs_include Scripts/Hollowborn\Materials\HollowSoul.cs
//cs_include Scripts/Legion/Various/LetitBurn(SoulEssence).cs

//cs_include Scripts/Legion/InfiniteLegionDarkCaster.cs
//cs_include Scripts/Story/Legion/SeraphicWar.cs
//cs_include Scripts/Legion/LegionExcercise/LegionExercise3.cs
//cs_include Scripts/Legion/LegionExcercise/LegionExercise4.cs
//cs_include Scripts/Nation/Various/DragonBlade[mem].cs
//cs_include Scripts/Legion/Various/LegionBonfire.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;
using System.Runtime.Serialization;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;

public class CoreHollowbornLichKing
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private CoreHollowborn HB = new();
    private CoreQOM QOM = new();
    public CoreLegion Legion = new();
    public UndeadLegionMerge UndeadLegionMerge = new();
    public AnotherOneBitesTheDust AnotherOneBitesTheDust = new();
    public HollowSoul HollowSoul = new();
    public LetItBurn LetItBurn = new();
    public CoreLR CoreLR = new();


    public string OptionsStorage = "HollowbornLichKing";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        new Option<bool>(
            "getAll", "Get all items",
            "Some quests need to be done multiple times in order to get everything, "+
            "if true the bot will continue untill it has everything from that quest before moving on" +
            "\nRecommended setting: True",
            true),
            new Option<bool>("BankAfter", "Bank Rewards", "bank Rewards after", true),
            CoreBots.Instance.SkipOptions,

            new Option<DraftlessRewards>("Draftless", "Draftless Reward", "Reward Selection for Draftless", DraftlessRewards.All),
            new Option<FlowStressRewards>("Flow Stress", "FlowStress Reward", "Reward Selection for Flow Stress", FlowStressRewards.All),
            new Option<HeatTreatmentRewards>("Heat Treatment", "Heat Treatment Reward", "Reward Selection for Heat Treatment", HeatTreatmentRewards.All),
            new Option<CounterblowRewards>("Counterblow", "Counterblow Reward", "Reward Selection for Counterblow", CounterblowRewards.All)
    };

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.RunCore();
    }

    public void GetAll()
    {
        bool getAllDrops = Bot.Config!.Get<bool>("getAll");
        bool BankAfter = Bot.Config!.Get<bool>("BankAfter");
        bool optionsLogged = false;

        var questDictionary = new Dictionary<string, (int Order, Action Action)>
    {
        { "Draftless", (9637, () => Draftless(getAllDrops ? DraftlessRewards.All : Bot.Config!.Get<DraftlessRewards>("Draftless"), !getAllDrops && BankAfter)) },
        { "Flow Stress", (9638, () => FlowStress(getAllDrops ? FlowStressRewards.All : Bot.Config!.Get<FlowStressRewards>("Flow Stress"), !getAllDrops && BankAfter)) },
        { "Heat Treatment", (9639, () => HeatTreatment(getAllDrops ? HeatTreatmentRewards.All : Bot.Config!.Get<HeatTreatmentRewards>("Heat Treatment"), !getAllDrops && BankAfter)) },
        { "Counterblow", (9640, () => Counterblow(getAllDrops ? CounterblowRewards.All : Bot.Config!.Get<CounterblowRewards>("Counterblow"), !getAllDrops && BankAfter)) },
    };
        string[] questOrder = { "Draftless", "Flow Stress", "Heat Treatment", "Counterblow" };


        foreach (var quest in questOrder)
        {
            string questConfig = Bot.Config?.Get<string>(quest) ?? string.Empty;

            //Quest/Item Requirements:
            Legion.JoinLegion();
            Adv.BuyItem("underworld", 216, "Undead Champion");
            UndeadLegionMerge.BuyAllMerge("Ultimate Lich King");
            UndeadLegionMerge.BuyAllMerge("Ultimate Lich King Helm");
            HB.HardcoreContract();
            //Requirements:

            if (!string.IsNullOrEmpty(questConfig))
            {
                if (!optionsLogged)
                {
                    Core.Logger($"Options Selected:\n{string.Join("\n", questOrder.Select(q => $"\t{q.Replace("_", " ")}: [{Bot.Config?.Get<string>(q)?.Replace("_", " ") ?? string.Empty}]"))}");
                    optionsLogged = true;
                }

                var (order, action) = questDictionary[quest];
                action();

                if (BankAfter)
                {
                    Core.ToBank(Core.QuestRewards(order));
                }
            }
            Core.CancelRegisteredQuests();
        }
    }

    public void Draftless(DraftlessRewards rewardSelection = DraftlessRewards.All, bool completeOnce = false, int quant = 1)
    {
        string[] rewards = Core.QuestRewards(9637).Except("Soul Fragment");
        DraftlessRewards DraftlessReward = Bot.Config!.Get<DraftlessRewards>("Draftless");

        // Check if we should return early based on inventory conditions and 'completeOnce' flag
        bool shouldReturnEarly = (DraftlessReward == DraftlessRewards.All && Core.CheckInventory(rewards, toInv: false))
            || DraftlessReward == DraftlessRewards.None
            || rewardSelection == DraftlessRewards.Soul_Fragment && Core.CheckInventory("Soul Fragment", quant)
            || (Core.CheckInventory((int)DraftlessReward, toInv: false) && !completeOnce);

        if (shouldReturnEarly)
        {
            Core.Logger("Quest rewards are already obtained or conditions met. Exiting Stirring Discord.");
            return; // Signal to exit
        }

        Core.AddDrop(rewards);
        Core.AddDrop("Soul Fragment");
        Core.Logger($"Reward Chosen: {Bot.Config!.Get<DraftlessRewards>("Draftless")}");
        while (!Bot.ShouldExit)
        {
            Core.EnsureAccept(9637);

            Legion.FarmLegionToken(100);
            Farm.BattleUnderB("Undead Energy", 50);
            HollowSoul.GetYaSoulsHeeeere(25);

            if (completeOnce)
            {
                Core.EnsureComplete(9637);
                Core.Logger("Draftless quest completed.");
                return;
            }
            else
            {
                if (DraftlessReward == DraftlessRewards.All && Core.CheckInventory(rewards) || rewardSelection == DraftlessRewards.Soul_Fragment && !Core.CheckInventory("Soul Fragment", quant))
                {
                    if (rewardSelection == DraftlessRewards.Soul_Fragment)
                        Core.EnsureComplete(9637, 84835);
                    Core.EnsureCompleteChoose(9637, rewards);
                    Core.Logger("In The Flow Stress quest completed.");
                }
                else
                {
                    Core.EnsureComplete(9637, (int)DraftlessReward);
                    Core.Logger("Draftless quest completed.");
                    break;
                }
            }
        }
    }

    public void FlowStress(FlowStressRewards rewardSelection = FlowStressRewards.All, bool completeOnce = false, int quant = 1)
    {
        if (!Core.isCompletedBefore(9638))
        {
            Core.Logger("Quest not unlocked [9638], doing \"Draftless\"");
            Draftless(completeOnce: true);
        }

        string[] rewards = Core.QuestRewards(9638).Except("Lich King Fragment");
        FlowStressRewards FlowStressreward = Bot.Config!.Get<FlowStressRewards>("Flow Stress");

        // Check if we should return early based on inventory conditions and 'completeOnce' flag
        bool shouldReturnEarly = (FlowStressreward == FlowStressRewards.All && Core.CheckInventory(rewards, toInv: false))
            || FlowStressreward == FlowStressRewards.None
            || rewardSelection == FlowStressRewards.Lich_King_Fragment && Core.CheckInventory("Lich King Fragment", quant)
            || (Core.CheckInventory((int)FlowStressreward, toInv: false) && !completeOnce);

        if (shouldReturnEarly)
        {
            Core.Logger("Conditions met to skip In The Flow Stress quest.");
            return;
        }

        Core.AddDrop(rewards);
        Core.AddDrop("Lich King Fragment");
        Core.Logger($"Reward Chosen: {FlowStressreward}");
        while (!Bot.ShouldExit)
        {
            Core.EnsureAccept(9638);

            //Kill area
            Legion.FarmLegionToken(1000);
            AnotherOneBitesTheDust.SoulSand(1);
            Draftless(DraftlessRewards.Soul_Fragment, false, 6);
            //Kill area

            if (completeOnce)
            {
                Core.EnsureComplete(9638);
                Core.Logger("In The Flow Stress quest completed.");
                return;
            }
            else
            {
                if (rewardSelection == FlowStressRewards.All && !Core.CheckInventory(rewards) || rewardSelection == FlowStressRewards.Lich_King_Fragment && !Core.CheckInventory("Lich King Fragment", quant))
                {
                    if (rewardSelection == FlowStressRewards.Lich_King_Fragment)
                        Core.EnsureComplete(9638, 84836);
                    Core.EnsureCompleteChoose(9638, rewards);
                    Core.Logger("In The Flow Stress quest completed.");
                }
                else
                {
                    Core.EnsureComplete(9638, (int)FlowStressreward);
                    Core.Logger("In The Flow Stress quest completed.");
                    break;
                }
            }
        }
    }

    public void HeatTreatment(HeatTreatmentRewards rewardSelection = HeatTreatmentRewards.All, bool completeOnce = false, int quant = 1)
    {
        if (!Core.isCompletedBefore(9639))
        {
            Core.Logger("Quest not unlocked [9639], doing \"Flow Stress\"");
            Draftless(completeOnce: true);
        }

        string[] rewards = Core.QuestRewards(9639);
        HeatTreatmentRewards HeatTreatmentReward = Bot.Config!.Get<HeatTreatmentRewards>("Heat Treatment");

        // Check if we should return early based on inventory conditions and 'completeOnce' flag
        bool shouldReturnEarly = (HeatTreatmentReward == HeatTreatmentRewards.All && Core.CheckInventory(rewards, toInv: false))
            || HeatTreatmentReward == HeatTreatmentRewards.None
            || (Core.CheckInventory((int)HeatTreatmentReward, toInv: false) && !completeOnce);

        if (shouldReturnEarly)
        {
            Core.Logger("Conditions met to skip In The Flow Stress quest.");
            return;
        }

        Core.AddDrop(rewards);

        Core.Logger($"Reward Chosen: {HeatTreatmentReward}");
        while (!Bot.ShouldExit)
        {
            Core.EnsureAccept(9639);
            //Kill area
            Legion.FarmLegionToken(10000);
            LetItBurn.SoulEssence(1);
            FlowStress(FlowStressRewards.Lich_King_Fragment, false, 6);
            Draftless(DraftlessRewards.Soul_Fragment, false, 24);
            //Kill area

            if (completeOnce)
            {
                Core.EnsureComplete(9639);
                Core.Logger("In The Flow Stress quest completed.");
                return;
            }
            else
            {
                if (rewardSelection == HeatTreatmentRewards.All && !Core.CheckInventory(rewards))
                {
                    Core.EnsureCompleteChoose(9639, Core.QuestRewards(9639));
                    Core.Logger("In The Flow Stress quest completed.");
                }
                else
                {
                    Core.EnsureComplete(9639, (int)HeatTreatmentReward);
                    Core.Logger("In The Flow Stress quest completed.");
                    break;
                }
            }
        }
    }

    public void Counterblow(CounterblowRewards rewardSelection = CounterblowRewards.All, bool completeOnce = false, int quant = 1)
    {
        if (!Core.isCompletedBefore(9640))
        {
            Core.Logger("Quest not unlocked [9640], doing \"In The Flow Stress\"");
            FlowStress(completeOnce: true);
        }

        string[] rewards = Core.QuestRewards(9640);
        CounterblowRewards CounterblowReward = Bot.Config!.Get<CounterblowRewards>("Counterblow");

        // Check if we should return early based on inventory conditions and 'completeOnce' flag
        bool shouldReturnEarly = (CounterblowReward == CounterblowRewards.All && Core.CheckInventory(rewards, quant, toInv: false))
            || CounterblowReward == CounterblowRewards.None
            || (Core.CheckInventory((int)CounterblowReward, quant) && !completeOnce);

        if (shouldReturnEarly)
        {
            Core.Logger("Conditions met to skip Counterblow quest.");
            return;
        }

        Core.AddDrop(rewards);
        Core.Logger($"Reward Chosen: {CounterblowReward}");
        while (!Bot.ShouldExit)
        {
            Core.EnsureAccept(9640);
            //Kill area
            LetItBurn.SoulEssence(3);
            FlowStress(FlowStressRewards.Lich_King_Fragment, false, 12);
            Draftless(DraftlessRewards.Soul_Fragment, false, 36);
            CoreLR.ConquestWreath(4);
            CoreLR.ExaltedCrown(4);
            CoreLR.RevenantSpellscroll(4);
            //Kill area

            if (completeOnce)
            {
                Core.EnsureComplete(9640);
                Core.Logger("Counterblow quest completed.");
                return;
            }
            else
            {
                if (rewardSelection == CounterblowRewards.All && !Core.CheckInventory(rewards))
                {
                    Core.EnsureCompleteChoose(9640, Core.QuestRewards(9640));
                    Core.Logger("Counterblow quest completed.");
                }
                else
                {
                    Core.EnsureComplete(9640, (int)CounterblowReward);
                    Core.Logger("Counterblow quest completed.");
                    break;
                }
            }
        }
    }

    public enum DraftlessRewards
    {
        Soul_Fragment = 84835,
        Hollowborn_Undead_Warrior = 84873,
        Hollowborn_Underworld_Legacy_Horns = 84876,
        Hollowborn_Underworld_Legacy_Helm = 84877,
        All,
        None
    }
    public enum FlowStressRewards
    {
        Lich_King_Fragment = 84836,
        Hollowborn_Legion_Champion = 84872,
        Hollowborn_Undead_Legacy_Horns = 84874,
        Hollowborn_Undead_Legacy_Helm = 84875,
        Hollowborn_Legion_Champion_Crest = 84878,
        Hollowborn_Soul_Eater_Blade = 84879,
        All,
        None
    }
    public enum CounterblowRewards
    {
        Hollowborn_Soul_Eater_Blades = 84880,
        Hollowborn_Lich_Kings_Pride = 84882,
        Hollowborn_Lich_Kings_Morph = 84885,
        Hollowborn_Lich_Kings_Flaming_Skull = 84886,
        Hollowborn_Apocalypse_Flame = 84888,
        Hollowborn_Apocalypse_Flames = 84889,
        Altar_Of_the_Hollowborn_Caladbolg = 84901,
        Hollowborn_Shadow_of_the_Legion = 84919,
        All,
        None
    }
    public enum HeatTreatmentRewards
    {
        Hollowborn_Benevolent_Locks = 74485,
        Hollowborn_Malignant_Locks = 74488,
        Hollowborn_Face_of_Chaos = 74491,
        Hollowborn_Gaze_of_Chaos = 74492,
        All,
        None
    }
}
