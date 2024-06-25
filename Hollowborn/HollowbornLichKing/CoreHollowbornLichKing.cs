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


//cs_include Scripts/Legion/MergeShops/UndeadLegionMerge.cs
//cs_include Scripts/Legion/Various/SoulSand.cs
//cs_include Scripts/Hollowborn/Materials/HollowSoul.cs
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
    private static IScriptInterface Bot => IScriptInterface.Instance;
    private static CoreBots Core => CoreBots.Instance;
    private readonly CoreFarms Farm = new();
    private readonly CoreAdvanced Adv = new();
    private readonly CoreHollowborn HB = new();
    private readonly CoreLegion Legion = new();
    private readonly UndeadLegionMerge UndeadLegionMerge = new();
    private readonly AnotherOneBitesTheDust AnotherOneBitesTheDust = new();
    private readonly HollowSoul HollowSoul = new();
    private readonly LetItBurn LetItBurn = new();
    private readonly CoreLR CoreLR = new();


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

    int DraftlessTurnin = 1;
    int FlowStressTurnin = 1;
    int HeatTreatmentTurnin = 1;
    int CounterblowTurnin = 1;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.RunCore();
    }

    public void GetAll()
    {
        Core.Logger("Startup may take a moment. Please be patient!");
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
            CheckReqs();
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
                    Core.ToBank(Core.QuestRewards(order).Except("Soul Fragment", "Lich King Fragment"));
                }
            }
            Core.CancelRegisteredQuests();
        }
    }

    public void Draftless(DraftlessRewards rewardSelection = DraftlessRewards.All, bool completeOnce = false, int quant = 1)
    {
        Core.Logger(quant > 1 ? $"~~Draftless~~[Farm Mode], Soul Fragment: {Bot.Inventory.Items?.FirstOrDefault(x => x.Name == "Soul Fragment")?.Quantity ?? 0} / {quant}" : "~~Draftless [Set/Story Mode]~~");
        string[] rewards = Core.QuestRewards(9637).Except("Soul Fragment");

        DraftlessRewards DraftlessReward;
        DraftlessReward = rewardSelection;

        bool shouldReturnEarly =
                                DraftlessReward == DraftlessRewards.None
                                || (quant > 1 && DraftlessReward == DraftlessRewards.Soul_Fragment && Core.CheckInventory("Soul Fragment", quant))
                                || (DraftlessReward != DraftlessRewards.All && DraftlessReward != DraftlessRewards.Soul_Fragment && Core.CheckInventory((int)DraftlessReward, toInv: false) && !completeOnce)
                                || (DraftlessReward == DraftlessRewards.All && Core.CheckInventory(rewards, toInv: false));

        if (shouldReturnEarly)
        {
            Core.Logger(DraftlessReward == DraftlessRewards.Soul_Fragment
            ? $"Soul Fragment: {Bot.Inventory.Items?.FirstOrDefault(x => x.Name == "Soul Fragment")?.Quantity ?? 0} / {quant} Continuing"
            : "Conditions met to skip `Draftless` quest.");
            return;
        }

        Core.AddDrop(rewards);
        Core.AddDrop("Soul Fragment");
        Core.Logger($"Reward Chosen: {Bot.Config!.Get<DraftlessRewards>("Draftless")}");

        CheckReqs();
        while (!Bot.ShouldExit)
        {
            if (DraftlessReward == DraftlessRewards.All)
                foreach (string item in rewards)
                    Core.Logger(Core.CheckInventory(item, toInv: false) ? $"{item}: ✅" : $"{item} ❌");
            if (DraftlessReward == DraftlessRewards.Soul_Fragment)
                Core.FarmingLogger("Soul Fragment", quant);

            if ((DraftlessReward == DraftlessRewards.All && Core.CheckInventory(rewards, toInv: false))
            || (DraftlessReward == DraftlessRewards.Soul_Fragment && Core.CheckInventory("Soul Fragment", quant))
            || (DraftlessReward != DraftlessRewards.All && DraftlessReward != DraftlessRewards.Soul_Fragment && Core.CheckInventory((int)DraftlessReward, toInv: false)))
                break;

            Core.EnsureAccept(9637);

            Legion.FarmLegionToken(100);
            Farm.BattleUnderB("Undead Energy", 50);
            HollowSoul.GetYaSoulsHeeeere(25);

            if (completeOnce)
            {
                Core.EnsureCompleteChoose(9637, rewards);
                Core.Logger($"Draftless quest completed x{DraftlessTurnin++}.");
                break;
            }
            else
            {
                if (DraftlessReward == DraftlessRewards.All && !Core.CheckInventory(rewards) || DraftlessReward == DraftlessRewards.Soul_Fragment && !Core.CheckInventory("Soul Fragment", quant))
                {
                    if (DraftlessReward == DraftlessRewards.Soul_Fragment)
                        Core.EnsureComplete(9637, 84835);
                    else Core.EnsureCompleteChoose(9637, rewards);
                    Core.Logger($"Draftless quest completed x{DraftlessTurnin++}.");
                }
                else
                {
                    if (DraftlessReward != DraftlessRewards.All || DraftlessReward != DraftlessRewards.Soul_Fragment)
                    {
                        Core.EnsureComplete(9637, (int)DraftlessReward);
                        Core.Logger($"Draftless quest completed x{DraftlessTurnin++}.");
                    }
                }
            }
            Bot.Wait.ForPickup("Soul Fragment");
        }
    }

    public void FlowStress(FlowStressRewards rewardSelection = FlowStressRewards.All, bool completeOnce = false, int quant = 1)
    {
        Core.Logger(quant > 1 ? $"~~FlowStress~~[Farm Mode], Lich King Fragment: {Bot.Inventory.Items?.FirstOrDefault(x => x.Name == "Soul Fragment")?.Quantity ?? 0} / {quant}" : "~~FlowStress [Set/Story Mode]~~");
        if (!Core.isCompletedBefore(9637))
        {
            Core.Logger("Quest not unlocked [9638], doing \"Draftless\"");
            Draftless(completeOnce: true);
        }

        string[] rewards = Core.QuestRewards(9638).Except("Lich King Fragment");

        FlowStressRewards FlowStressReward;
        FlowStressReward = rewardSelection;

        bool shouldReturnEarly =
                                (FlowStressReward == FlowStressRewards.All && Core.CheckInventory(rewards, toInv: false))
                                || (FlowStressReward == FlowStressRewards.None)
                                || (quant > 1 && FlowStressReward == FlowStressRewards.Lich_King_Fragment && Core.CheckInventory("Lich King Fragment", quant))
                                || (FlowStressReward != FlowStressRewards.All && FlowStressReward != FlowStressRewards.Lich_King_Fragment && Core.CheckInventory((int)FlowStressReward, quant) && !completeOnce);

        if (shouldReturnEarly)
        {
            Core.Logger(FlowStressReward == FlowStressRewards.Lich_King_Fragment
            ? $"Lich King Fragment: {Bot.Inventory.Items?.FirstOrDefault(x => x.Name == "Soul Fragment")?.Quantity ?? 0} / {quant} Continuing"
            : "Conditions met to skip `Flow Stress` quest.");
            return;
        }

        Core.AddDrop(rewards);
        Core.AddDrop("Lich King Fragment");
        Core.Logger($"Reward Chosen: {FlowStressReward}");
        CheckReqs();
        while (!Bot.ShouldExit)
        {
            if (FlowStressReward == FlowStressRewards.All)
                foreach (string item in rewards)
                    Core.Logger(Core.CheckInventory(item, toInv: false) ? $"{item}: ✅" : $"{item} ❌");
            if (FlowStressReward == FlowStressRewards.Lich_King_Fragment)
                Core.FarmingLogger("Lich King Fragment", quant);

            if ((FlowStressReward == FlowStressRewards.All && Core.CheckInventory(rewards))
            || (FlowStressReward == FlowStressRewards.Lich_King_Fragment && Core.CheckInventory("Lich King Fragment", quant))
            || (FlowStressReward != FlowStressRewards.All && FlowStressReward != FlowStressRewards.Lich_King_Fragment && Core.CheckInventory((int)FlowStressReward, toInv: false)))
                break;

            Core.EnsureAccept(9638);

            Draftless(DraftlessRewards.Soul_Fragment, false, 6);
            AnotherOneBitesTheDust.SoulSand(1);
            Legion.FarmLegionToken(1000);

            if (completeOnce)
            {
                Core.EnsureCompleteChoose(9638, rewards);
                Core.Logger($"Flow Stress quest completed x{FlowStressTurnin++}.");
                break;
            }
            else
            {
                if (FlowStressReward == FlowStressRewards.All && !Core.CheckInventory(rewards) || FlowStressReward == FlowStressRewards.Lich_King_Fragment && !Core.CheckInventory("Lich King Fragment", quant))
                {
                    if (FlowStressReward == FlowStressRewards.Lich_King_Fragment)
                        Core.EnsureComplete(9638, 84836);
                    else Core.EnsureCompleteChoose(9638, rewards);
                    Core.Logger($"Flow Stress quest completed x{FlowStressTurnin++}.");
                }
                else
                {
                    if (FlowStressReward != FlowStressRewards.All || FlowStressReward != FlowStressRewards.Lich_King_Fragment)
                    {
                        Core.EnsureComplete(9638, (int)FlowStressReward);
                        Core.Logger($"Flow Stress quest completed x{FlowStressTurnin++}.");
                    }
                }
            }
            Bot.Wait.ForPickup("Lich King Fragment");
        }
    }

    public void HeatTreatment(HeatTreatmentRewards rewardSelection = HeatTreatmentRewards.All, bool completeOnce = false, int quant = 1)
    {
        Core.Logger(quant > 1 ? "~~HeatTreatment~~[Farm Mode]" : "~~HeatTreatment [Set/Story Mode]~~");
        if (!Core.isCompletedBefore(9638))
        {
            Core.Logger("Quest not unlocked [9639], doing \"Flow Stress\"");
            FlowStress(completeOnce: true);
        }

        string[] rewards = Core.QuestRewards(9639);

        HeatTreatmentRewards HeatTreatmentReward;
        HeatTreatmentReward = rewardSelection;

        bool shouldReturnEarly =
                                (HeatTreatmentReward == HeatTreatmentRewards.All && Core.CheckInventory(rewards, toInv: false))
                                || (HeatTreatmentReward == HeatTreatmentRewards.None)
                                || (HeatTreatmentReward != HeatTreatmentRewards.All && Core.CheckInventory((int)HeatTreatmentReward, quant) && !completeOnce);

        if (shouldReturnEarly)
        {
            Core.Logger("Conditions met to skip Heat Treatment quest.");
            return;
        }

        Core.AddDrop(rewards);

        Core.Logger($"Reward Chosen: {HeatTreatmentReward}");

        CheckReqs();
        while (!Bot.ShouldExit)
        {
            if (HeatTreatmentReward == HeatTreatmentRewards.All)
                foreach (string item in rewards)
                    Core.Logger(Core.CheckInventory(item, toInv: false) ? $"{item}: ✅" : $"{item} ❌");

            if ((HeatTreatmentReward == HeatTreatmentRewards.All && Core.CheckInventory(rewards))
            || (HeatTreatmentReward != HeatTreatmentRewards.All && Core.CheckInventory((int)HeatTreatmentReward, toInv: false)))
                break;

            Core.EnsureAccept(9639);
            FlowStress(FlowStressRewards.Lich_King_Fragment, false, 6);
            Draftless(DraftlessRewards.Soul_Fragment, false, 24);
            LetItBurn.SoulEssence(1);
            Legion.FarmLegionToken(10000);


            if (completeOnce)
            {
                Core.EnsureCompleteChoose(9639, rewards);
                Core.Logger($"Heat Treatment quest completed x{HeatTreatmentTurnin++}.");
                break;
            }
            else
            {
                if (HeatTreatmentReward == HeatTreatmentRewards.All && !Core.CheckInventory(rewards))
                {
                    Core.EnsureCompleteChoose(9639, rewards);
                    Core.Logger($"Heat Treatment quest completed x{HeatTreatmentTurnin++}.");
                }
                else
                {
                    if (HeatTreatmentReward != HeatTreatmentRewards.All)
                    {
                        Core.EnsureComplete(9639, (int)HeatTreatmentReward);
                        Core.Logger($"Heat Treatment quest completed x{HeatTreatmentTurnin++}.");
                    }
                }
            }
        }
    }

    public void Counterblow(CounterblowRewards rewardSelection, bool completeOnce = false, int quant = 1)
    {
        if (!Core.isCompletedBefore(9639))
        {
            Core.Logger("Quest not unlocked [9640], doing \"Heat Treatment\"");
            HeatTreatment(completeOnce: true);
        }

        string[] rewards = Core.QuestRewards(9640);
        CounterblowRewards CounterblowReward;
        CounterblowReward = rewardSelection;

        bool shouldReturnEarly =
                                (CounterblowReward == CounterblowRewards.All && Core.CheckInventory(rewards, quant, toInv: false))
                                || (CounterblowReward == CounterblowRewards.None)
                                || (CounterblowReward != CounterblowRewards.All && Core.CheckInventory((int)CounterblowReward, quant) && !completeOnce);

        if (shouldReturnEarly)
        {
            Core.Logger("Conditions met to skip Counterblow quest.");
            return;
        }

        Core.Logger((quant > 1 || CounterblowReward != CounterblowRewards.All) ? "~~Counterblow~~[Farm Mode]" : "~~Counterblow [Set/Story Mode]~~");

        Core.AddDrop(rewards);
        Core.Logger($"Reward Chosen: {CounterblowReward}");

        CheckReqs();
        while (!Bot.ShouldExit)
        {
            if (CounterblowReward == CounterblowRewards.All)
                foreach (string item in rewards)
                    Core.Logger(Core.CheckInventory(item, toInv: false) ? $"{item}: ✅" : $"{item} ❌");

            if ((CounterblowReward == CounterblowRewards.All && Core.CheckInventory(rewards))
            || (CounterblowReward != CounterblowRewards.All && Core.CheckInventory((int)CounterblowReward, toInv: false)))
                break;

            Core.EnsureAccept(9640);
            FlowStress(FlowStressRewards.Lich_King_Fragment, false, 12);
            Draftless(DraftlessRewards.Soul_Fragment, false, 36);
            LetItBurn.SoulEssence(3);
            CoreLR.ConquestWreath(4);
            CoreLR.ExaltedCrown(4);
            CoreLR.RevenantSpellscroll(4);

            if (completeOnce)
            {
                Core.EnsureCompleteChoose(9640, rewards);
                Core.Logger($"Counterblow quest completed x{CounterblowTurnin++}.");
                break;
            }
            else
            {
                if (CounterblowReward == CounterblowRewards.All && !Core.CheckInventory(rewards))
                {
                    Core.EnsureCompleteChoose(9640, rewards);
                    Core.Logger($"Counterblow quest completed x{CounterblowTurnin++}.");
                }
                else
                {
                    if (CounterblowReward != CounterblowRewards.All)
                    {
                        Core.EnsureComplete(9640, (int)CounterblowReward);
                        Core.Logger($"Counterblow quest completed x{CounterblowTurnin++}.");
                    }
                }
            }
        }
    }


    private void CheckReqs()
    {
        Farm.Experience(95);

        //Quest/Item Requirements:
        Legion.JoinLegion();

        if (!Core.CheckInventory("Undead Champion"))
            Adv.BuyItem("underworld", 216, "Undead Champion");

        if (!Core.CheckInventory("Ultimate Lich King"))
            UndeadLegionMerge.BuyAllMerge("Ultimate Lich King");

        if (!Core.CheckInventory("Ultimate Lich King Helm"))
            UndeadLegionMerge.BuyAllMerge("Ultimate Lich King Helm");

        if (!Core.CheckInventory(55157))
            HB.HardcoreContract();
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
        Hollowborn_Lich_King = 84881,
        Hollowborn_Lich_Kings_Horns = 84883,
        Hollowborn_Lich_Kings_Hood = 84884,
        Hollowborn_Lich_Kings_Cloak = 84887,
        All,
        None
    }
}
