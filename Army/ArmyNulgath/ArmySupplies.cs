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
            if (Bot.Config.Get<bool>("SwindlesReturnDuring"))
            {
                Core.RegisterQuests(2857, 7551);
                Core.FarmingLogger(item.Name, item.MaxStack);
                while (!Bot.ShouldExit && !Core.CheckInventory(item.ID, item.MaxStack))
                {
                    ArmyHydra(item, item.MaxStack);
                    if (Core.CheckInventory(Nation.SwindlesReturn))
                        Core.KillMonster("tercessuinotlim", "m1", "Right", "Dark Makai", "Dark Makai Rune");
                }
            }
            else
            {
                Core.RegisterQuests(2857);
                Core.FarmingLogger(item.Name, item.MaxStack);
                while (!Bot.ShouldExit && !Core.CheckInventory(item.ID, item.MaxStack))
                    ArmyHydra(item, item.MaxStack);
            } 
        }
        Core.CancelRegisteredQuests();
    }

    void ArmyHydra(ItemBase item, int quant = 99)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        if (Bot.Config!.Get<bool>("sellToSync"))
            Army.SellToSync(item.Name, quant);

        Core.AddDrop("Relic of Chaos");

        Core.EquipClass(ClassType.Farm);
        Army.waitForParty("hydrachallenge");

        Army.AggroMonStart("hydrachallenge");
        Army.AggroMonIDs(32, 33, 34);
        Army.AggroMonCells("h90");

        while (!Bot.ShouldExit && !Core.CheckInventory(item.ID, quant))
            Bot.Combat.Attack("*");

        Army.AggroMonStop(true);
        Core.JumpWait();
    }

}
