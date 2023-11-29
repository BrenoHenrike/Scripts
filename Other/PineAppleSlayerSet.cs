/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class APineappleSlayer
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();


    public string OptionsStorage = "APineappleSlayer";
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
        if (!Core.isSeasonalMapActive("freakitiki") || Core.CheckInventory(Core.QuestRewards(9486), toInv: false))
        {
            Core.Logger(Core.CheckInventory(Core.QuestRewards(9486)) ? "All rewards owned." : "seasonal map not available.");
            return;
        }
        PreReqs();

        Core.EquipClass(ClassType.Solo);
        Core.AddDrop(Core.QuestRewards(9486));
        foreach (string reward in Core.QuestRewards(9486))
        {
            Core.FarmingLogger(reward, 1);
            while (!Bot.ShouldExit && !Core.CheckInventory(reward))
                Core.HuntMonster("freakitiki", "Spineapple", "Fresh Pineapple", 10);
            Bot.Wait.ForPickup(reward);

            if (Bot.Config!.Get<bool>("BankAfter"))
                Core.ToBank(reward);
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
            if (!Core.CheckInventory("Boar's Feet Recipe"))
            {
                Core.AddDrop("Boar's Feet Recipe");
                Core.EnsureAccept(1183);
                Core.HuntMonster("bloodtusk", "Rhison", "Quart of Rhison Milk", 7);
                Core.HuntMonster("bloodtusk", "Rhison", "Rhison Tears");
                Core.HuntMonster("bloodtusk", "Horc Boar Scout", "Boar's Foot", 12);
                Core.EnsureComplete(1183);
                Bot.Wait.ForPickup("Boar's Feet Recipe");
            }

            Story.BuyQuest(9485, "bloodtusk", 304, "Boar's Feet in Salted-Butter Sauce");
            Story.KillQuest(9485, "grams", "Wereboar");
            Story.KillQuest(9485, "trygve", "Rune Boar");
        }

        Story.KillQuest(9486, "freakitiki", "Spineapple");
    }
}
