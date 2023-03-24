/*
name: Army CraggleRock Rep
description: Farm reputation with your army. Faction: Craggle Rock
tags: army, reputation, craggle rock
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Monsters;
using Skua.Core.Options;

public class ArmyCraggleRockRep
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv => new();
    private CoreArmyLite Army = new();

    private static CoreBots sCore = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyCraggleRockRep";
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
        CoreBots.Instance.SkipOptions
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Setup();

        Core.SetOptions(false);
    }

    public void Setup()
    {
        if (Farm.FactionRank("CraggleRock") >= 10)
            return;

        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();
        Bot.Options.LagKiller = false;
        Core.EquipClass(ClassType.Solo);
        Core.RegisterQuests(7277); //Star of the Sandsea 7277
        Farm.ToggleBoost(BoostType.Reputation);
        //need this join because the map is fucky and teleports you back to enter2-down on hunts
        Core.Join("wanders", "r2", "Down");
        // Bot.Wait.ForCellChange("r2");
        string[] Farmcell = Bot.Monsters.GetMonsterCells("Kalestri Worshiper").ToArray();
        Army.DivideOnCells("r2", "r3", "r5", "r7");
        Army.AggroMonMIDs(1,4,7,10);
        Army.AggroMonStart("wanders");
        Army.SmartAggroMonStart("wanders", "Kalestri Worshiper");
        while (!Bot.ShouldExit && Farm.FactionRank("CraggleRock") < 10)
            Bot.Combat.Attack("Kalestri Worshiper");
        Army.AggroMonStop(true);
        Farm.ToggleBoost(BoostType.Reputation, false);
        Core.CancelRegisteredQuests();
    }
}
