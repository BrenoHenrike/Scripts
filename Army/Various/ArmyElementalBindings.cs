/*
name: Elemental Binding (Army)
description: Farms Prismatic Seams using your army.
tags: army, elemental binding
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;

public class ArmyElementalBinding
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv => new();
    private CoreArmyLite Army = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "CustomAggroMon";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
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
        Core.SetOptions();

        ArmyBits();

        Core.SetOptions(false);
    }


    public void ArmyBits()
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Core.EquipClass(ClassType.Solo);
        Core.AddDrop("Elemental Binding");

        Army.AggroMonStart("archmage");
        Army.DivideOnCells("r2");

        while (!Bot.ShouldExit && !Core.CheckInventory("Elemental Binding", 2500))
        {
            while (!Bot.ShouldExit && Bot.Player.Cell != "r2")
            {
                Core.Jump("r2");
                Core.Sleep(Core.ActionDelay);
            }

            foreach (int MonsterMapID in new[] { 1, 2 })
                while (!Bot.ShouldExit && Core.IsMonsterAlive(MonsterMapID, useMapID: true))
                    Bot.Combat.Attack(MonsterMapID);
        }
        Army.AggroMonStop(true);
        Core.CancelRegisteredQuests();
    }
}
