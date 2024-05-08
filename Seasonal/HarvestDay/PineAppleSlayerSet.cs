/*
name: PineappleSlayerSet
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;
using Skua.Core.Options;

public class PineappleSlayerSet
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();


    public string OptionsStorage = "PineappleSlayerSet";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        new Option<bool>("BankAfter", "Bank After", "Bank all rewards after?", false),
        CoreBots.Instance.SkipOptions,
    };


    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Example();

        Core.SetOptions(false);
    }

    public void Example()
    {
        if (!Core.isSeasonalMapActive("foulfarm") || Core.CheckInventory(Core.QuestRewards(9486), toInv: false))
        {
            Core.Logger(Core.CheckInventory(Core.QuestRewards(9486)) ? "All rewards owned." : "seasonal map not available.");
            return;
        }

        PreReqs();

        List<ItemBase> RewardOptions = Core.EnsureLoad(9486).Rewards;

        foreach (ItemBase item in RewardOptions)
            Core.AddDrop(item.ID);

        Core.EquipClass(ClassType.Solo);
        foreach (ItemBase Reward in RewardOptions)
        {
            if (Core.CheckInventory(Reward.ID, toInv: false))
                continue;

            Core.FarmingLogger(Reward.Name, 1);
            Core.EnsureAccept(9486);
            Core.HuntMonster("freakitiki", "Spineapple", "Fresh Pineapple", 10, log: false);
            Core.EnsureComplete(9486, Reward.ID);

            Bot.Wait.ForPickup(Reward.Name);
            if (Bot.Config!.Get<bool>("BankAfter") && Bot.Inventory.Contains(Reward.ID))
                Core.ToBank(Reward.Name);
        }
    }

    void PreReqs()
    {
        if (Core.isCompletedBefore(9486))
            return;

        Story.PreLoad(this);

        if (!Story.QuestProgression(9484))
        {
            Core.EnsureAccept(9484);
            Core.HuntMonster("foulfarm", "Foul Wishbone", "Wishbone", 10);
            Core.HuntMonster("foulfarm", "Foul Fowl", "Foul Turkey");
            Core.HuntMonster("harvest", "Turdraken", "Turdraken");
            Core.EnsureComplete(9484);
        }

        if (!Story.QuestProgression(9485))
        {
            Core.EnsureAccept(9485);

            if (!Core.CheckInventory("Boar's Feet in Salted-Butter Sauce"))
            {
                Core.AddDrop("Boar's Feet recipe");
                Core.EnsureAccept(1183);
                Core.HuntMonster("bloodtusk", "Rhison", "Quart of Rhison Milk", 7);
                Core.HuntMonster("bloodtusk", "Rhison", "Rhison Tears");
                Core.HuntMonster("bloodtusk", "Horc Boar Scout", "Boar's Foot", 12);
                Core.EnsureComplete(1183);
                Bot.Wait.ForPickup("Boar's Feet recipe");
                Core.BuyItem("bloodtusk", 304, "Boar's Feet in Salted-Butter Sauce");
                Bot.Wait.ForPickup("Boar's Feet in Salted-Butter Sauce");
            }

            Core.HuntMonster("grams", "Wereboar", "Wereboar Captured");
            Core.HuntMonster("trygve", "Rune Boar", "Rune Boar Captured", 10);
            Core.EnsureComplete(9485);
        }

        Story.KillQuest(9486, "freakitiki", "Spineapple");
    }
}
