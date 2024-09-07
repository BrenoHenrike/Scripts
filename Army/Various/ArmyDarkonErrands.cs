/*
name: Darkon Errands (Army)
description: Uses an army to farm the various Darkon errands
tags: darkon, receipt, first, second, third, errands
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/Army/CoreArmyLite.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;
using Skua.Core.Options;

public class ArmyDarkonErrands
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreArmyLite Army = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyDarkonErrands";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        new Option<Method>("Method", "Which method to get Darkon's Receipt?", "Choose your method", Method.First_Errands_Weak_Team),
        sArmy.player1,
        sArmy.player2,
        sArmy.player3,
        sArmy.player4,
        sArmy.player5,
        sArmy.player6,
        sArmy.packetDelay,
        CoreBots.Instance.SkipOptions
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.Add("Darkon's Receipt");

        Core.SetOptions(disableClassSwap: false);

        Setup(Bot.Config!.Get<Method>("Method"));

        Core.SetOptions(false);
    }

    public void Setup(Method Method, int quant = 1)
    {
        Core.OneTimeMessage("Only for army", "This is intended for use with an army, not for solo players");

        Core.EquipClass(ClassType.Solo);

        switch (Method)
        {
            case Method.First_Errands_Strong_Team:
                ArmyHunt("towerofdoom7", new[] { "Dread Gorillaphant" }, "Darkon's Receipt", ClassType.Farm, false, 222, Method.First_Errands_Strong_Team);
                break;

            case Method.First_Errands_Weak_Team:
                ArmyHunt("maparcangrove", new[] { "Gorillaphant" }, "Darkon's Receipt", ClassType.Farm, false, 222, Method.First_Errands_Weak_Team);
                break;

            case Method.Second_Errands:
                ArmyHunt("doomvault", new[] { "Binky" }, "Darkon's Receipt", ClassType.Solo, false, 222, Method.Second_Errands);
                break;

            case Method.Third_Errands:
                ArmyHunt("tercessuinotlim", new[] { "Nulgath" }, "Darkon's Receipt", ClassType.Solo, false, 222, Method.Third_Errands);
                break;

            default:
                break;
        }
    }


    void ArmyHunt(string map, string[] monsters, string item, ClassType classType, bool isTemp = false, int quant = 1, Method Method = Method.None)
    {
        if (!Bot.Config!.Get<bool>("sellToSync") && Core.CheckInventory(item, quant))
            return;

        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        if (Bot.Config!.Get<bool>("sellToSync"))
            Army.SellToSync(item, quant);

        Core.AddDrop(item);
        Core.EquipClass(classType);

        //Army.waitForParty(map, item);
        Core.FarmingLogger(item, quant);

        switch (Method)
        {
            case Method.First_Errands_Strong_Team:
                Core.RegisterQuests(7324);
                Army.AggroMonCells("r2", "r5");
                Army.AggroMonStart("towerofdoom7");
                Army.DivideOnCells("r2", "r5");
                break;

            case Method.First_Errands_Weak_Team:
                Core.EquipClass(ClassType.Farm);
                Core.RegisterQuests(7324);
                Army.AggroMonCells("Right", "LeftBack");
                Army.AggroMonStart("arcangrove");
                Army.DivideOnCells("Right", "LeftBack");
                break;

            case Method.Second_Errands:
                Core.EquipClass(ClassType.Solo);
                Core.RegisterQuests(7325);
                Army.AggroMonCells("r5");
                Army.AggroMonStart("Binky");
                Army.DivideOnCells("r5");
                break;

            case Method.Third_Errands:
                Core.EquipClass(ClassType.Farm);
                Core.RegisterQuests(7326);
                Army.AggroMonCells("Boss2");
                Army.AggroMonStart("tercessuinotlim");
                Army.DivideOnCells("Boss2");
                break;
        }



        while (!Bot.ShouldExit && isTemp ? !Bot.TempInv.Contains(item, quant) : !Core.CheckInventory(item, quant))
            Bot.Combat.Attack("*");

        Core.CancelRegisteredQuests();
        Army.AggroMonStop(true);
        Core.JumpWait();
    }


    public enum Method
    {
        First_Errands_Weak_Team = 0,
        First_Errands_Strong_Team = 1,
        Second_Errands = 2,
        Third_Errands = 3,
        None = 4
    }
}
