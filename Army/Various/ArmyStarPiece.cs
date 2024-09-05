/*
name: ArmyStarPiece
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/Army/CoreArmyLite.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class Generated_ArmyStarPiece
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreArmyLite Army = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "CustomAggroMon";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        CoreBots.Instance.SkipOptions,
        sArmy.player1,
        sArmy.player2,
        sArmy.player3,
        sArmy.player4,
        sArmy.player5,
    };

    public string[] Loot =
{
        "Star Piece"
    };

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(Loot);

        Core.SetOptions();

        ArmyStarPiece();
        Core.SetOptions();
    }

    public void ArmyStarPiece(int quant = 650)
    {

        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        if (Core.CheckInventory("Star Piece", quant))
            return;

        Core.AddDrop(Loot);
        Core.FarmingLogger($"Star Piece", quant);
        Core.EquipClass(ClassType.Farm);

        Army.AggroMonMIDs(7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22);
        Army.AggroMonStart("starfield");
        Army.DivideOnCells("r3");

        while (!Bot.ShouldExit && (!Core.CheckInventory("Star Piece", int.MaxValue)))
            Bot.Combat.Attack("*");

        Army.AggroMonStop(true);
        Core.JumpWait();
        Core.Sleep(2000);
    }



}
