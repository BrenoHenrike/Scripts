/*
name: Tainted Gem (Army)
description: This bot will farm Tainted Gems with your army, using the Cubes method
tags: tainted, gem, nation, nulgath, cubes, ice, receipt, swindle, reagent, boxes, mountfrost, army
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ArmyTaintedGem
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreArmyLite Army = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyTaintedGem";
    public int q = 0;
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        sArmy.player1,
        sArmy.player2,
        sArmy.player3,
        sArmy.player4,
        sArmy.player5,
        sArmy.player6,
        sArmy.packetDelay,
        CoreBots.Instance.SkipOptions,
    };

    public string[] Loot =
    {
        "Cubes",
        "Tainted Gem",
        "Receipt of Swindle"
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Loot);

        Core.SetOptions();

        TaintedGem();

        Core.SetOptions(false);
    }

    public void TaintedGem(int quant = 1000)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        if (Core.CheckInventory("Tainted Gem", quant))
            return;

        Core.AddDrop(Loot);
        Core.FarmingLogger($"Tainted Gem", quant);
        Core.EquipClass(ClassType.Farm);

        Core.RegisterQuests(7817);
        while (!Bot.ShouldExit && !Core.CheckInventory("Tainted Gem", quant))
        {
            Cubes();
            IceCube();
            Bot.Wait.ForPickup("Tained Gem");
        }
        Core.CancelRegisteredQuests();
        //Army.waitForParty("whitemap", "Tainted Gem");

    }

    public void Cubes()
    {
        Core.Join("boxes");
        Army.AggroMonCells("Fort2", "Closet", "Fort1", "Boss");
        Army.AggroMonStart("boxes");
        Army.DivideOnCells("Fort2", "Closet", "Fort1", "Boss", "Boss", "Boss");

        

        while (!Bot.ShouldExit && (!Core.CheckInventory("Cubes", 500)))
            Bot.Combat.Attack("*");
        Army.AggroMonStop(true);

        Core.JumpWait();
        Core.Sleep(2000);
        //Army.waitForParty("boxes", "Cubes");
    }

    public void IceCube()
    {
        Core.Join("mountfrost");
        Army.AggroMonCells("War");
        Army.AggroMonStart("mountfrost");
        Core.Jump("War", "Left");

        

        while (!Bot.ShouldExit && (!Core.CheckInventory("Ice Cubes", 6)))
            Bot.Combat.Attack("*");
        Army.AggroMonStop(true);

        Core.JumpWait();
        Core.Sleep(2000);
        //Army.waitForParty("mountfrost", "Ice Cubes");
    }
}
