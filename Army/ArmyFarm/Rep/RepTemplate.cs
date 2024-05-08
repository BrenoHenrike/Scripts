/*
name: Army Aegis Rep
description: Farm reputation with your army. Faction: Aegis
tags: army, reputation, aegis
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ArmyRepTemplate
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv => new();
    private CoreArmyLite Army = new();

    private static CoreBots sCore = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyRep";
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
        CoreBots.Instance.SkipOptions
    };

    string repname = "";
    string AggroMonStart = "";
    int[] AggroMonMIDs = { };
    string[] DivideOnCells = { };
    int[] RegisterQuests = { };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Setup();

        Core.SetOptions(false);
    }

    public void Setup()
    {
        if (Farm.FactionRank(repname) >= 10)
            return;

        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Army.AggroMonMIDs(AggroMonMIDs);
        Army.AggroMonStart(AggroMonStart);
        Army.DivideOnCells(DivideOnCells);

        Farm.ToggleBoost(BoostType.Reputation);
        Core.EquipClass(ClassType.Farm);
        Core.RegisterQuests(RegisterQuests);
        
        
            
        while (!Bot.ShouldExit && Farm.FactionRank(repname) < 10)
            Bot.Combat.Attack("*");
        Army.AggroMonStop(true);
        Farm.ToggleBoost(BoostType.Reputation, false);
        Core.CancelRegisteredQuests();
        //Army.waitForParty("whitemap");
    }
}







