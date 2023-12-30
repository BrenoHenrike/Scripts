/*
name: Prismatic Seams (Army)
description: Farms Prismatic Seams using your army.
tags: army, prismatic seams
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
using Skua.Core.Interfaces;

public class ArmyPrimaticSeams
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv => new();
    private CoreArmyLite Army = new();
    private static CoreArmyLite sArmy = new();
    private CoreSoW SoW = new();

    public string OptionsStorage = "CustomAggroMon";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
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

        Dothething();

        Core.SetOptions(false);
    }

    private void Dothething()
    {
        Core.OneTimeMessage("Only for army", "This is intended for use with an army, not for solo players.");

        SoW.CompleteCoreSoW();
        ArmyBits();
    }


    public void ArmyBits()
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();
        Core.EquipClass(ClassType.Farm);

        Army.AggroMonMIDs(1, 2, 15, 3, 4, 14, 8, 9, 10, 11, 12, 13);
        Army.AggroMonStart("streamwar");
        Army.DivideOnCells("r2", "r3", "r3a");
        Core.RegisterQuests(8814, 8815);

        if (Bot.Player.CurrentClass?.Name == "ArchMage")
            Bot.Options.AttackWithoutTarget = true;

        while (!Bot.ShouldExit && !Core.CheckInventory("Prismatic Seams", 2000))
            Bot.Combat.Attack("*");

        Army.waitForParty("streamwar", "Prismatic Seams");
        Army.AggroMonStop(true);
        Core.CancelRegisteredQuests();
    }
}
