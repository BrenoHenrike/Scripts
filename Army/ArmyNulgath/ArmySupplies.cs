/*
name:  Supplies Wheel Army
description:  uses an army to farm the "supplies to spin the wheen of chance" quest. 
tags: nulgath, supplies to spin teh wheels, army, reagents
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Army/CoreArmyLite.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
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
        new Option<int>("armysize","Players", "Input the minimum of players to wait for", 1),
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

        Core.AddDrop(Nation.bagDrops);
        Core.AddDrop("Relic of Chaos");
        Core.EquipClass(ClassType.Farm);

        Core.RegisterQuests(2857);
        Core.ConfigureAggro();
        while (!Bot.ShouldExit && !Core.CheckInventory("Relic of Chaos", 13))
            ArmyHydra90("hydrachallenge", "h90", "Left");
        Core.CancelRegisteredQuests();
        Core.ConfigureAggro(false);
    }

    public void ArmyHydra90(string map, string cell, string pad)
    {
        Core.Join(map, cell, pad);
        while ((cell != null && Bot.Map.CellPlayers.Count() > 0 ? Bot.Map.CellPlayers.Count() : Bot.Map.PlayerCount) < Bot.Config.Get<int>("armysize"))
        {
            if (Bot.Player.Cell != "Enter")
                Core.Jump("Enter");
            Core.Logger($"Waiting for the squad. [{Bot.Map.PlayerNames.Count}/{Bot.Config.Get<int>("armysize")}]");
            Bot.Sleep(2000);
        }

        if (Bot.Player.Cell != cell)
            Core.Jump(cell, pad);

        while (!Core.CheckInventory("Relic of Chaos", 13))
            Bot.Combat.Attack("*");
    }
}
