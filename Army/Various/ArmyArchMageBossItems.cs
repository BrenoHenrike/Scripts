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


    public bool DontPreconfigure = true;
    public string OptionsStorage = "BossDrops";
    public List<IOption> Options = new List<IOption>()
    {
        CoreBots.Instance.SkipOptions,
        new Option<int>("armysize","Number of Accounts", "Input the number of players that it will be waiting for", 4)
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
        Core.SetOptions(disableClassSwap: true);
        bot.Options.RestPackets = false;
        Bot.Events.PlayerAFK += PlayerAFK;
        Core.BankingBlackList.AddRange(Drops);
        Core.SetOptions();
        Core.Unbank(Drops);
        WaitingRoom();

        Core.SetOptions(false);
    }

    public void WaitingRoom()
    {
        Core.Logger($"We have {Bot.Config.Get<int>("armysize")} passenger/s signed up, lets hope this works LFMAO");
        Bot.Sleep(2500);
        GetmBois();
    }

    // public void WtfAmIDoing()
    // {
    //     while (Bot.Map.PlayerNames.Count < Bot.Config.Get<int>("armysize"))
    //     {
    //         Core.Logger($"Less than {Bot.Config.Get<int>("armysize")} players found");
    //         Bot.Sleep(Core.ActionDelay);
    //     }
    //     RetrieveVoidAuras(7500);
    // }

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
            ArmyKillMonster("voidflibbi", "Enter", "Spawn", "Flibbitiestgibbet", "Void Essentia", log: false);
            ArmyKillMonster("voidnightbane", "Enter", "Spawn", "Nightbane", "voidnightbane", log: false);
            ArmyKillMonster("dage", "Boss", "Right", "Dage the Evil", "Vital Exanima", log: false);
            ArmyKillMonster("fireavatar", "r9", "Left", "Avatar Tyndarius", "Everlight Flame", log: false);
            ArmyKillMonster("tercessuinotlim", "Boss2", "Right", "Nulgath", "The Mortal Coil", log: false);
            ArmyKillMonster("theworld", "r9", "Left", "Encore Darkon", "Undying Resolve", log: false);
            ArmyKillMonster("celestialpast", "r11a", "Left", "Azalith", "drop", log: false);
            ArmyKillMonster("darkcarnax", "Boss", "Right", "Nightmare Carnax", "Calamitous Ruin", log: false);
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
        if (!isTemp && item != null)
            Core.AddDrop(item);
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();
        Core.Join(map, cell, pad);
        Core.Jump(cell, pad);
        while ((cell != null && Bot.Map.CellPlayers.Count() > 0 ? Bot.Map.CellPlayers.Count() : Bot.Map.PlayerCount) < Bot.Config.Get<int>("armysize"))
        {
            Core.Logger($"[{Bot.Map.PlayerNames.Count}/{Bot.Config.Get<int>("armysize")}] Waiting For The Squad!");
            Bot.Sleep(1500);
        }
        if (item == null)
        {
            if (log)
                Core.Logger($"Killing {monster}");
            Bot.Kill.Monster(monster);
            Core.Rest();
        }
        else
        {
            if (Bot.Map.Name == "darkcarnax")
            {
                Bot.Options.AttackWithoutTarget = true;
                Bot.Events.RunToArea += DarkCarnaxMove;
                Core._KillForItem(monster, item, quant, isTemp, log: log);
                Bot.Options.AttackWithoutTarget = false;
                Bot.Events.RunToArea -= DarkCarnaxMove;
            }
            else
            {
                Core._KillForItem(monster, item, quant, isTemp, log: log);
            }
        }
    }

    public void PlayerAFK()
    {
        Core.Logger("Anti-AFK engaged");
        Bot.Sleep(1500);
        Bot.Send.Packet("%xt%zm%afk%1%false%");
    }

    void DarkCarnaxMove(string zone)
    {
        switch (zone.ToLower())
        {
            case "a":
                //Move to the right
                Bot.Player.WalkTo(Bot.Random.Next(600, 930), Bot.Random.Next(380, 475));
                break;
            case "b":
                //Move to the left
                Bot.Player.WalkTo(Bot.Random.Next(25, 325), Bot.Random.Next(380, 475));
                break;
            default:
                //Move to the center
                Bot.Player.WalkTo(Bot.Random.Next(325, 600), Bot.Random.Next(380, 475));
                break;
        }
    }

}

// Learning materials

// public void AFKCheck(IScriptInterface bot)
// {
//     Bot.Sleep(2000);
//     Bot.Events.PlayerAFK += (IScriptInterface bot) => Bot.Send.Packet("%xt%zm%afk%1%false%");
// }

// public void waitForParty(string? cell = null, string? pad = null)
// {
//     int i = 0;
//     if (cell != null)
//         Core.Jump(cell, pad != null ? pad : "Left");
//     while (!Bot.ShouldExit && (cell != null && Bot.Map.CellPlayers.Count() > 0 ? Bot.Map.CellPlayers.Count() : Bot.Map.PlayerCount) != PartySize)
//     {
//         Bot.Sleep(1000);
//         i++;

//         if (i == 15)
//         {
//             if (cell != null && Bot.Map.CellPlayers.Count() > 0)
//                 Core.Logger($"[{Bot.Map.CellPlayers.Count()}/{PartySize}] " +
//                 "Waiting for " + String.Join(" &", PartyMembers.Where(x => !Bot.Map.CellPlayers.Select(x => x.Name).Contains(x)).ToList()));
//             else if (Bot.Map.PlayerNames.Count() > 0)
//                 Core.Logger($"[{Bot.Map.PlayerNames.Count()}/{PartySize}] " +
//                 "Waiting for " + String.Join(" &", PartyMembers.Where(x => !Bot.Map.PlayerNames.Contains(x)).ToList()));
//             else
//                 Core.Logger($"[{Bot.Map.PlayerCount}/{PartySize}] Waiting for the rest of the party");
//             i = 0;
//         }
//     }
