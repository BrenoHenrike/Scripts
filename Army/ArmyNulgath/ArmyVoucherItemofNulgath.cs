/*
name: Army Totem And Gem
description: Uses an army to farm Totems Of Nulgath or Gem of Nulgath from "voucher item totem of nulgath"
tags: totems of nulgath, gem of nulgath, army, voucher item totem of nulgath
*/

//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Army/CoreArmyLite.cs
//cs_include Scripts/Nation/CoreNation.cs

using Skua.Core.Interfaces;
using Skua.Core.Options;
using System.Collections.Generic;

public class ArmyVoucherItemofNulgath
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreArmyLite Army = new();
    private CoreNation Nation = new();

    private static CoreBots sCore = new();
    private static CoreArmyLite sArmy = new();

    public int QuestID = 4778;

    public string OptionsStorage = "ArmyTotemAndGem";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        sArmy.player1,
        sArmy.player2,
        sArmy.player3,
        sArmy.player4,
        sArmy.player5,
        sArmy.player6,
        sArmy.packetDelay,
        new Option<bool>("FarmTotems", "Farm Totems of Nulgath", "Enable farming Totems of Nulgath.", true),
        new Option<int>("TotemQuant", "Totems Quantity", "Maximum number of Totems to farm.", 100),
        new Option<bool>("FarmGems", "Farm Gems of Nulgath", "Enable farming Gems of Nulgath.", true),
        new Option<int>("GemQuant", "Gems Quantity", "Maximum number of Gems to farm.", 1000),
        CoreBots.Instance.SkipOptions
    };

    private string[] Loot =
    {
        "Totem of Nulgath",
        "Gem of Nulgath",
        "Essence of Nulgath"
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Loot);

        Core.SetOptions();

        Setup();

        Core.SetOptions(false);
    }
    public void Setup()
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Core.OneTimeMessage("Only for army", "This is intended for use with an army, not for solo players.");

        Nation.FarmVoucher(false);

        var farmTotems = Bot.Config?.Get<bool>("FarmTotems") ?? false;
        var farmGems = Bot.Config?.Get<bool>("FarmGems") ?? false;

        // Allow only either totems or gems to be farmed, not both
        if (farmTotems && farmGems)
        {
            Core.Logger("Error: Both 'FarmTotems' and 'FarmGems' options are enabled. Please enable only one of them.", messageBox: true);
            return;
        }

        if (farmTotems)
            FarmRewards(Rewards.TotemofNulgath, Bot.Config?.Get<int>("TotemQuant") ?? 0);

        if (farmGems)
            FarmRewards(Rewards.GemofNulgath, Bot.Config?.Get<int>("GemQuant") ?? 0);

    }

    void FarmRewards(Rewards reward, int quant)
    {
        if (Core.CheckInventory((int)reward, quant))
            return;

        Core.AddDrop(Loot);
        Core.EquipClass(ClassType.Farm);
        Core.FarmingLogger(reward.ToString(), quant);
        while (!Bot.ShouldExit && !Core.CheckInventory((int)reward, quant))
        {
            Core.EnsureAccept(QuestID);
            FarmTotems(map: "tercessuinotlim", monsters: new[] { "Dark Makai" }, item: "Essence of Nulgath", quant: 60);
            // Add a delay between monster kills to avoid spamming server requests
            Bot.Sleep(Core.ActionDelay);
            Core.EnsureComplete(4778, (int)reward);
        }
    }

    void FarmTotems(string map = "tercessuinotlim", string[]? monsters = null, string item = "Essence of Nulgath", bool isTemp = false, int quant = 1)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        if (Core.CheckInventory(item, quant))
            return;

        Bot.Drops.Add(item);

        Core.EquipClass(ClassType.Farm);
        Core.FarmingLogger(item, quant);

        Army.waitForParty(map, item);
        AggroSetup(map);

        // Attack monsters until the inventory is filled with the specified quantity
        while (!Core.CheckInventory(item, quant) && !Bot.ShouldExit)
        {
            Bot.Combat.Attack("*");
            // Add a delay between attacks to avoid spamming server requests
            Bot.Sleep(Core.ActionDelay);
        }

        Army.AggroMonStop(true);
        Core.JumpWait();
        Bot.Wait.ForPickup(item);
    }

    void AggroSetup(string map)
    {
        if (Bot.Map.Name == null)
            return;

        if (Bot.Map.Name == map)
        {
            Army.AggroMonCells("m1", "m2");
            Army.AggroMonStart(map);
            Army.DivideOnCells("m1", "m2");
        }
    }

    public enum Rewards
    {
        TotemofNulgath = 5357,
        GemofNulgath = 6136
    }
}
