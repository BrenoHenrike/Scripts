/*
name: Supplies Wheel Army
description: uses an army to farm the "supplies to spin the wheen of chance" quest.
tags: nulgath, supplies to spin the wheels, army, reagents
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Army/CoreArmyLite.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;
using Skua.Core.Options;

public class SuppliesWheelArmy
{
    public static IScriptInterface Bot => IScriptInterface.Instance;
    public static CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreArmyLite Army = new();
    public CoreNation Nation = new();

    private static readonly CoreArmyLite sArmy = new();

    public bool DontPreconfigure = true;
    public string OptionsStorage = "ArmySupplies";

    public List<IOption> Options = new()
    {

        new Option<Cell>("mob", "h90 or h85", "h90 for more relic turn ins, but more chance of getting stuck due to deaths - h85 for just Relics from Escherion", Cell.h90),
        new Option<bool>("sellToSync", "Sell to Sync", "Sell \"Relic of Chaos\" to make sure the army stays syncronized. If off, there is a higher chance your army might desyncornize", false),
        new Option<bool>("SwindlesReturnDuring", "Do swindles Return", "Accepts the Swindles Returns items, and goes to kill a makai for the rune, during the quest.", false),
        sArmy.player1,
        sArmy.player2,
        sArmy.player3,
        sArmy.player4,
        sArmy.player5,
        sArmy.player6,
        sArmy.player7,
        sArmy.packetDelay,
        CoreBots.Instance.SkipOptions
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nation.bagDrops);
        Core.BankingBlackList.Add("Relic of Chaos");

        Core.SetOptions();

        Core.OneTimeMessage("Only for army", "This is intended for use with an army, not for solo players.");
        Core.OneTimeMessage("Dark Makai Rune/Sigil Solution", "Randomizing location for \"Dark Makai\"\n" +
        "as the drop can randomly stop showing up", forcedMessageBox: false);
        ArmySupplies();

        Core.SetOptions(false);
    }

    public void ArmySupplies()
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        List<ItemBase> RewardOptions1 = Core.EnsureLoad(2857).Rewards;
        List<ItemBase> RewardOptions2 = Core.EnsureLoad(7551).Rewards;

        if (Bot.Config!.Get<bool>("SwindlesReturnDuring"))
            Core.AddDrop(Nation.SwindlesReturn);
        Core.AddDrop(Nation.bagDrops);
        Core.AddDrop("Relic of Chaos");

        foreach (ItemBase item in RewardOptions1.Concat(RewardOptions2).ToArray())
        {
            Core.RegisterQuests(Bot.Config!.Get<bool>("SwindlesReturnDuring") ? new[] { 2857, 7551 } : new[] { 2857 });
            Core.FarmingLogger(item.Name, item.MaxStack);
            while (!Bot.ShouldExit && !Core.CheckInventory(item.ID, item.MaxStack))
                ArmyHydra(item, item.MaxStack);
        }
        Core.CancelRegisteredQuests();
    }

    void ArmyHydra(ItemBase item, int quant = 99)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        if (Bot.Config!.Get<bool>("sellToSync"))
            Army.SellToSync(item.Name, quant);

        Core.AddDrop("Relic of Chaos", "Hydra Scale Piece");
        Core.AddDrop(item.ID);

        Core.EquipClass(ClassType.Farm);

        while (!Bot.ShouldExit && !Core.CheckInventory(item.ID, quant))
        {
            Bot.Combat.Attack(Bot.Config!.Get<Cell>("mob") == Cell.h85 ? "Hydra Head 85" : "Hydra Head 90");

            Bot.Sleep(Core.ActionDelay);

            if (Core.CheckInventory(Nation.SwindlesReturn) && Bot.Config!.Get<bool>("SwindlesReturnDuring"))
            {
                Army.AggroMonStop(true);
                Core.JumpWait();

                string[] locations = new[] { "tercessuinotlim", Core.IsMember ? "Nulgath" : "evilmarsh" };
                string location = locations[new Random().Next(locations.Length)];
                string cell = location == "tercessuinotlim" ? (new Random().Next(2) == 0 ? "m1" : "m2") : "Field1";
                Core.KillMonster(location, cell, "Left", "Dark Makai", "Dark Makai Rune");

                Bot.Sleep(2500);

                Army.waitForParty("hydrachallenge");

                SetHydraAggro();
            }
        }

        Army.AggroMonStop(true);
        Core.JumpWait();
    }

    void SetHydraAggro()
    {
        Army.AggroMonMIDs(Bot.Config!.Get<Cell>("mob") == Cell.h85 ? new[] { 29, 30, 31 } : new[] { 32, 33, 34 });
        Army.DivideOnCells(Bot.Config!.Get<Cell>("mob") == Cell.h85 ? "h85" : "h90");
        Army.AggroMonStart("hydrachallenge");
    }

    public enum Cell
    {
        h90 = 0,
        h85 = 1
    }
}
