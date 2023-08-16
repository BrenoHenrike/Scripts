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

        Army.AggroMonIDs(5241, 5242, 5243);
        Army.AggroMonStart("streamwar");
        Army.DivideOnCells("r2", "r3", "r3a");

        // this wont stop when capped, if u want 
        // it to stop just uncomment and delete the ) after the exit
        Core.RegisterQuests(8814, 8815);
        while (!Bot.ShouldExit)// && !Core.CheckInventory("Prismatic Seams", 2000))
            Bot.Combat.Attack("*");
        Army.AggroMonStop(true);
        Core.CancelRegisteredQuests();
    }
}
