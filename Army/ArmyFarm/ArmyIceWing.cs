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
using Skua.Core.Models;

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
    public List<IOption> Options = new()
    {
        sArmy.player1,
        sArmy.player2,
        sArmy.player3,
        sArmy.player4,
        sArmy.player5,
        sArmy.player6,
        sArmy.player7,
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
        Bot.Events.PlayerAFK += PlayerAFK;
        Core.OneTimeMessage("Only for army", "This is intended for use with an army, not for solo players.");

    Restart:
        Army.AggroMonIDs(1);
        Army.AggroMonStart("icewing", "Enter", "Spawn");
        Army.DivideOnCells("Enter");
        Bot.Player.SetSpawnPoint();
        bool AggroReenabled = false;
        Core.RegisterQuests(Core.IsMember ? 6635 : 6632);
        while (!Bot.ShouldExit)
        {
            if (Bot.Map.PlayerNames != null && Bot.Map.PlayerNames.Count < Army.Players().Length)
                break;

            while (!Bot.ShouldExit && !Bot.Player.Alive)
            {
                Army.AggroMonStop(true);
                Bot.Sleep(100);
                AggroReenabled = false;
            }

            if (!AggroReenabled)
            {
                Army.AggroMonIDs(1);
                AggroReenabled = true;
            }

            if (Bot.Map.Name != "icewing")
                Core.Join("icewing");
            if (Bot.Player.Cell != "Enter")
                Core.Jump("Enter");

            Bot.Combat.Attack("*");
            Core.Sleep();
        }
        if (!Bot.ShouldExit && Bot.Map.PlayerNames != null && Bot.Map.PlayerNames.Count < Army.Players().Length)
        {
            while (!Bot.ShouldExit && PlayerTakingDmg())
            {
                Army.AggroMonStop(true);
                Core.JumpWait();
            }
            goto Restart;
        }

        Core.CancelRegisteredQuests();
        Army.AggroMonStop(true);
        while (!Bot.ShouldExit && PlayerTakingDmg())
        {
            Army.AggroMonStop(true);
            Core.JumpWait();
        }
        Bot.Events.PlayerAFK -= PlayerAFK;
    }

    public bool PlayerTakingDmg()
    {
        int Playerhp = Bot.Player.Health;
        int PlayerhpUpdated = Playerhp - Bot.Player.Health;

        if (PlayerhpUpdated > 0)
            return true;
        return false;
    }
    public void PlayerAFK()
    {
        Core.Logger("Anti-AFK engaged");
        Core.Sleep(1500);
        Bot.Send.Packet("%xt%zm%afk%1%false%");
    }
}