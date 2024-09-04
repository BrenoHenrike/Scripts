/*
name: The Poisonous Deal
description: This script farms Tainted Gems and Dark Crystal Shards using "The Poisonous Deal" Quest.
tags: poisonous, deal, dark, crystal, shard, tainted, gem, nulgath, nation, crag, bamboozle, quest, claymore
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Nation/Various/TaintedClaymore.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class ThePoisonousDeal
{
    public static IScriptInterface Bot => IScriptInterface.Instance;
    public static CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreNation Nation = new();
    public TaintedClaymore TC = new();

    public string OptionsStorage = "PoisonousDeal";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        CoreBots.Instance.SkipOptions,
        new Option<int>("TaintedQuantity", "How many Tainted Gems?","Max Stack is 1000" ,0),
        new Option<int>("ShardQuantity", "How many Dark Crystal Shards?","Max Stack is 1000", 0),
        new Option<bool>("BankItems", "Bank nation items at the end", "true/false", false),
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nation.bagDrops);
        Core.SetOptions();

        Deal(Bot.Config!.Get<int>("TaintedQuantity"), Bot.Config.Get<int>("ShardQuantity"));

        Core.SetOptions(false);
    }

    public void Deal(int TaintedQuant, int ShardQuant)
    {
        if (!Core.CheckInventory(Nation.CragName))
        {
            Core.Logger($"{Nation.CragName} missing. stopping");
            return;
        }

       Nation.DragonSlayerReward();
        TC.DoAll();

        Core.AddDrop("Tainted Gem", "Dark Crystal Shard");

        Core.FarmingLogger("Tainted Gem", TaintedQuant);

        while (!Bot.ShouldExit && !Core.CheckInventory("Tainted Gem", TaintedQuant))
        {
            Core.EnsureAccept(4776);
            Core.HuntMonster("graveyard", "Big Jack Sprat", "Bone Axe", isTemp: false);
            Nation.FarmBloodGem(2);
            Nation.FarmUni10(30);
            Core.EnsureComplete(4776, 4769);
            Bot.Wait.ForPickup("Tainted Gem");
        }

        Core.FarmingLogger("Dark Crystal Shard", ShardQuant);

        while (!Bot.ShouldExit && !Core.CheckInventory("Dark Crystal Shard", ShardQuant))
        {
            Core.EnsureAccept(4776);
            Core.HuntMonster("graveyard", "Big Jack Sprat", "Bone Axe", isTemp: false);
            Nation.FarmBloodGem(2);
            Nation.FarmUni10(30);
            Core.EnsureComplete(4776, 4770);
            Bot.Wait.ForPickup("Dark Crystal Shard");
        }

        if (Bot.Config!.Get<bool>("BankItems"))
            Core.ToBank(Nation.bagDrops);
    }
}

