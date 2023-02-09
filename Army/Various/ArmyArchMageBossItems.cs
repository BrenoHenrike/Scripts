/*
name: Army ArchMage Boss Items
description: uses an army to help farm the required boss items for archmage.
tags: archmage, army, boss items
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class ArchMageMatsArmy
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    private CoreArmyLite Army = new();

    private static CoreArmyLite sArmy = new();

    public bool DontPreconfigure = true;
    public string OptionsStorage = "BossDrops";
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

    public string[] Drops =
    {
        "Calamitous Ruin",
        "Vital Exanima",
        "Everlight Flame",
        "The Mortal Coil",
        "Undying Resolve",
        "The Divine Will",
        "Insatiable Hunger",
        "Void Essentia",
        "Elemental Binding",
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Drops);
        Core.SetOptions(disableClassSwap: true);

        WaitingRoom();

        Core.SetOptions(false);
    }

    public void WaitingRoom()
    {
        Core.OneTimeMessage("Only for army", "This is intended for use with an army, not for solo players.");

        Bot.Events.PlayerAFK += PlayerAFK;

        GetmBois();

        Bot.Events.PlayerAFK += PlayerAFK;
    }

    public void GetmBois()
    {
        if (Core.CheckInventory(Drops))
            return;

        Core.EquipClass(ClassType.Solo);

        foreach (string item in Drops)
        {
            if (!Core.CheckInventory(item, toInv: false))
            {
                Bot.Drops.Add(item);
                Core.FarmingLogger(item, 1);
            }
            else Core.Logger($"{item} Found.");
        }

        Bot.Quests.UpdateQuest(8732);
        Core.ConfigureAggro();

        while (!Bot.ShouldExit && !Core.CheckInventory(Drops))
        {
            ArmyKillMonster("voidflibbi", "Enter", "Spawn", "Flibbitiestgibbet", "Void Essentia", isTemp: false, log: false);
            ArmyKillMonster("voidnightbane", "Enter", "Spawn", "Nightbane", "Insatiable Hunger", isTemp: false, log: false);
            ArmyKillMonster("dage", "Boss", "Right", "Dage the Evil", "Vital Exanima", isTemp: false, log: false);
            ArmyKillMonster("fireavatar", "r9", "Left", "Avatar Tyndarius", "Everlight Flame", isTemp: false, log: false);
            ArmyKillMonster("tercessuinotlim", "Boss2", "Right", "Nulgath", "The Mortal Coil", isTemp: false, log: false);
            ArmyKillMonster("theworld", "r9", "Left", "Encore Darkon", "Undying Resolve", isTemp: false, log: false);
            ArmyKillMonster("celestialpast", "r11a", "Left", "Azalith", "The Divine Will", isTemp: false, log: false);
            ArmyKillMonster("darkcarnax", "Boss", "Right", "Nightmare Carnax", "Calamitous Ruin", isTemp: false, log: false);
            ArmyKillMonster("archmage", "r2", "Right", "Prismata", "Elemental Binding", 250, isTemp: false, log: false);
        }
        Core.ConfigureAggro(false);
        Core.Logger($"🖕");
    }

    /// <summary>
    /// Joins a map, jump & set the spawn point and kills the specified monster - with an army check that waits for the input number of players
    /// </summary>
    /// <param name="map">Map to join</param>
    /// <param name="cell">Cell to jump to</param>
    /// <param name="pad">Pad to jump to</param>
    /// <param name="monster">Name of the monster to kill</param>
    /// <param name="item">Item to kill the monster for, if null will just kill the monster 1 time</param>
    /// <param name="quant">Desired quantity of the item</param>
    /// <param name="isTemp">Whether the item is temporary</param>
    /// <param name="log">Whether it will log that it is killing the monster</param>
    public void ArmyKillMonster(string map, string cell, string pad, string monster, string item = null, int quant = 1, bool isTemp = true, bool log = true, bool publicRoom = false)
    {
        if (item != null && isTemp ? Bot.TempInv.Contains(item, quant) : Core.CheckInventory(item, quant))
            return;

        Bot.Events.PlayerAFK += PlayerAFK;

        if (!isTemp && item != null)
            Core.AddDrop(item);

        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        if (Bot.Config.Get<bool>("sellToSync"))
            Army.SellToSync(item, quant);

        Army.waitForParty(map, item);
        Core.FarmingLogger(item, quant);
        Army.SmartAggroMonStart(map, monster);

        if (Bot.Map.Name == "darkcarnax")
        {
            Bot.Options.AttackWithoutTarget = true;
            Bot.Events.RunToArea += DarkCarnaxMove;

            while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
                Bot.Combat.Attack("*");

            Bot.Options.AttackWithoutTarget = false;
            Bot.Events.RunToArea -= DarkCarnaxMove;
        }
        else
        {
            while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
                Bot.Combat.Attack("*");
        }
        Army.AggroMonStop(true);
        Core.JumpWait();
    }


    void ArmyHunt(string map, string[] monsters, string item, ClassType classType, bool isTemp = false, int quant = 1)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        if (Bot.Config.Get<bool>("sellToSync"))
            Army.SellToSync(item, quant);

        Core.AddDrop(item);

        Army.waitForParty(map, item);
        Core.FarmingLogger(item, quant);

        Army.SmartAggroMonStart(map, monsters);

        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
            Bot.Combat.Attack("*");

        Army.AggroMonStop(true);
        Core.JumpWait();
    }

    public void PlayerAFK()
    {
        Core.Logger("Anti-AFK engaged");
        Bot.Sleep(1500);
        Bot.Send.Packet("%xt%zm%afk%1%false%");
    }

    void DarkCarnaxMove(string zone)
    {
        Bot.Sleep(Core.ActionDelay);
        switch (zone.ToLower())
        {
            case "a":
                //Move to the right
                Bot.Sleep(1500);
                Bot.Player.WalkTo(Bot.Random.Next(600, 930), Bot.Random.Next(380, 475));
                break;
            case "b":
                //Move to the left
                Bot.Sleep(1500);
                Bot.Player.WalkTo(Bot.Random.Next(25, 325), Bot.Random.Next(380, 475));
                break;
            default:
                //Move to the center
                Bot.Sleep(1500);
                Bot.Player.WalkTo(Bot.Random.Next(325, 600), Bot.Random.Next(380, 475));
                break;
        }
    }

}