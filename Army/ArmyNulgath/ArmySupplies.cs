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
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreArmyLite Army = new();
    public CoreNation Nation = new();

    private static CoreArmyLite sArmy = new();

    public bool DontPreconfigure = true;
    public string OptionsStorage = "ArmySupplies";

    public List<IOption> Options = new List<IOption>()
    {
        new Option<bool>("sellToSync", "Sell to Sync", "Sell items to make sure the army stays syncronized.\nIf off, there is a higher chance your army might desyncornize", false),
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

        ArmySupplies();

        Core.SetOptions(false);
    }

    public void ArmySupplies()
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();
        Core.OneTimeMessage("Only for army", "This is intended for use with an army, not for solo players.");

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
                    ArmyHunt("hydrachallenge", new[] { "Hydra Head 90" }, "Relic of Chaos", ClassType.Farm, false, 13);
                    if (Bot.Config.Get<bool>("SwindlesReturnDuring") && Core.CheckInventory(Nation.SwindlesReturn))
                        Core.HuntMonster(Core.IsMember ? "nulgath" : "evilmarsh", "Dark Makai", "Dark Makai Rune", 1);
                }
            }
            else
            {
                Core.RegisterQuests(2857);
                Core.FarmingLogger(item.Name, item.MaxStack);
                while (!Bot.ShouldExit && !Core.CheckInventory(item.ID, item.MaxStack))
                    ArmyHunt("hydrachallenge", new[] { "Hydra Head 90" }, "Relic of Chaos", ClassType.Farm, false, 99);
            }
        }
        Core.CancelRegisteredQuests();
    }

    void ArmyHunt(string map, string[] monsters, string item, ClassType classType, bool isTemp = false, int quant = 1)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        if (Bot.Config!.Get<bool>("sellToSync"))
            Army.SellToSync(item, quant);

        Core.AddDrop(item);

        Core.EquipClass(classType);
        Army.waitForParty(map, item);
        Core.FarmingLogger(item, quant);

        Army.SmartAggroMonStart(map, monsters);

        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
            Bot.Combat.Attack("*");

        Army.AggroMonStop(true);
        Core.JumpWait();
    }
}
