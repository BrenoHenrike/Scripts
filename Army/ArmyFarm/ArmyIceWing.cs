/*
name: IceWing Leveling Army
description: Uses your army to kill Warlord Icewing.
tags: army, warlord icewing, experience, gold, icestorm arena
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;
using Skua.Core.Models.Monsters;

public class IceWingLevelingArmy
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreArmyLite Army = new();

    private static CoreBots sCore = new();
    private static CoreArmyLite sArmy = new();

    public bool DontPreconfigure = true;
    public string OptionsStorage = "ArmyIceWing";
    public List<IOption> Options = new List<IOption>()
    {
        sArmy.player1,
        sArmy.player2,
        sArmy.player3,
        sArmy.player4,
        sArmy.player5,
        sArmy.player6,
        CoreBots.Instance.SkipOptions,
    };

    public int level = 75;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        ArmyIceWing();

        Core.SetOptions(false);
    }

    public void ArmyIceWing()
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Core.OneTimeMessage("Only for army", "This is intended for use with an army, not for solo players.");

        Core.RegisterQuests(Core.IsMember ? 6635 : 6632);
        while (!Bot.ShouldExit)
            ArmyHunt("icewing", new[] { "Warlord Icewing" }, "Warlord Icewing Defeated", ClassType.Solo, true);
        Core.CancelRegisteredQuests();
    }

    void ArmyHunt(string map, string[] monsters, string item, ClassType classType, bool isTemp = false, int quant = 1)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();
        
        PlayerAFK();
        if (!isTemp)
            Core.AddDrop(item);

        Core.EquipClass(classType);
        Army.waitForParty(map, item);
        Core.FarmingLogger(item, quant);        

        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
            Bot.Combat.Attack("Warlord Icewing");

        Core.JumpWait();
    }


    public void PlayerAFK()
    {
        Core.Logger("Anti-AFK engaged");
        Bot.Sleep(1500);
        Bot.Send.Packet("%xt%zm%afk%1%false%");
    }
}
