/*
name: Darkon Errands (Army)
description: Uses an army to farm the various Darkon errands
tags: darkon, receipt, first, second, third, errands
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/Army/CoreArmyLite.cs
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
    public List<IOption> Options = new List<IOption>()
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

        Setup(Bot.Config.Get<Method>("Method"), 222);

        Core.SetOptions(false);
    }


    public void Setup(Method Method, int quant = 222)
    {
        Core.OneTimeMessage("Only for army", "This is intended for use with an army, not for solo players.");

        Core.EquipClass(ClassType.Solo);
        if (Method.ToString() == "Third_Errands")
            ArmyHunt("tercessuinotlim", new[] { "Nulgath" }, "Darkon's Receipt", ClassType.Solo, false, quant, Method.Third_Errands);

        else if (Method.ToString() == "Second_Errands")
            ArmyHunt("doomvault", new[] { "Binky" }, "Darkon's Receipt", ClassType.Solo, false, quant, Method.Second_Errands);

        else if (Method.ToString() == "First_Errands_Strong_Team")
        {
            ArmyHunt("towerofdoom7", new[] { "Dread Gorillaphant" }, "Darkon's Receipt", ClassType.Farm, false, quant, Method.First_Errands_Strong_Team);
        }
        else
        {
            ArmyHunt("maparcangrove", new[] { "Gorillaphant" }, "Darkon's Receipt", ClassType.Farm, false, quant, Method.First_Errands_Weak_Team);
        }
    }

    void ArmyHunt(string map, string[] monsters, string item, ClassType classType, bool isTemp = false, int quant = 1, Method Method = Method.None)
    {
        if (!Bot.Config.Get<bool>("sellToSync") && Core.CheckInventory(item, quant))
            return;

        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        if (Bot.Config.Get<bool>("sellToSync"))
            Army.SellToSync(item, quant);

        Core.AddDrop(item);

        Army.waitForParty(map, item);
        Core.FarmingLogger(item, quant);

        if (Method.ToString() == "First_Errands_Strong_Team")
            Core.RegisterQuests(7324);
        else if (Method.ToString() == "First_Errands_Weak_Team")
            Core.RegisterQuests(7324);
        else if (Method.ToString() == "Second_Errands")
            Core.RegisterQuests(7325);
        else if (Method.ToString() == "Third_Errands")
            Core.RegisterQuests(7326);

        Army.SmartAggroMonStart(map, monsters);

        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
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
