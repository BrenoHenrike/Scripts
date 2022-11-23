//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class RVAArmy
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public bool DontPreconfigure = true;
    public string OptionsStorage = "RVAArmy";
    public List<IOption> Options = new List<IOption>()
    {
        new Option<int>("armysize","Number of Accounts", "Input the number of players that it will be waiting for", 1),
        CoreBots.Instance.SkipOptions,
    };
    private int EssenceQuantity;

    public string[] VA =
    {
        "Void Aura",
        "Astral Ephemerite Essence",
        "Belrot the Fiend Essence",
        "Black Knight Essence",
        "Tiger Leech Essence",
        "Carnax Essence",
        "Chaos Vordred Essence",
        "Dai Tengu Essence",
        "Unending Avatar Essence",
        "Void Dragon Essence",
        "Creature Creation Essence"
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Bot.Events.PlayerAFK += PlayerAFK;
        Core.BankingBlackList.AddRange(VA);
        Core.SetOptions();
        Core.Unbank(VA);
        WaitingRoom();

        Core.SetOptions(false);
    }

    public void WaitingRoom()
    {
        Core.Logger($"We have {Bot.Config.Get<int>("armysize")} passenger/s signed up, lets hope this works LFMAO");
        Bot.Sleep(2500);
        RetrieveVoidAurasArmy(7500);
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

    public void RetrieveVoidAurasArmy(int Quantity)
    {
        if (Core.CheckInventory("Void Aura", Quantity))
            return;

        EssenceQuantity = 100;
        Farm.EvilREP(); //Anti-AFK should work now so yeah - though who doesn't have Evil Rank 10 if you're going for NSOD
        Core.EquipClass(ClassType.Solo);
        Core.AddDrop(VA);
        if (!Core.CheckInventory("Necromancer", toInv: false) && !Core.CheckInventory("Creature Shard", toInv: false))
            Core.AddDrop("Creature Shard");
        Core.RegisterQuests(4432);
        Core.FarmingLogger($"Void Aura", Quantity);
        while (!Bot.ShouldExit && !Core.CheckInventory("Void Aura", Quantity))
        {
            Core.DebugLogger(this, "Accept Quest");
            Core.EnsureAccept(4432);
            //if (!Core.CheckInventory("Astral Ephemerite Essence", EssenceQuantity))
            Core.DebugLogger(this, "Astral Ephemerite Essence");
            ArmyKillMonster("timespace", "Frame1", "Left", "Astral Ephemerite", "Astral Ephemerite Essence", EssenceQuantity, false);
            //if (!Core.CheckInventory("Belrot the Fiend Essence", EssenceQuantity))
            Core.DebugLogger(this, "Belrot the Fiend Essence");
            ArmyKillMonster("citadel", "m13", "Left", "Belrot the Fiend", "Belrot the Fiend Essence", EssenceQuantity, false);
            //if (!Core.CheckInventory("Black Knight Essence", EssenceQuantity))
            Core.DebugLogger(this, "Black Knight Essence");
            ArmyKillMonster("greenguardwest", "BKWest15", "Down", "Black Knight", "Black Knight Essence", EssenceQuantity, false);
            //if (!Core.CheckInventory("Tiger Leech Essence", EssenceQuantity))
            Core.DebugLogger(this, "Tiger Leech Essence");
            ArmyKillMonster("mudluk", "Boss", "Down", "Tiger Leech", "Tiger Leech Essence", EssenceQuantity, false);
            //if (!Core.CheckInventory("Carnax Essence", EssenceQuantity))
            Core.DebugLogger(this, "Carnax Essence");
            ArmyKillMonster("aqlesson", "Frame9", "Right", "Carnax", "Carnax Essence", EssenceQuantity, false);
            //if (!Core.CheckInventory("Chaos Vordred Essence", EssenceQuantity))
            Core.DebugLogger(this, "Chaos Vordred Essence");
            ArmyKillMonster("necrocavern", "r16", "Down", "Chaos Vordred", "Chaos Vordred Essence", EssenceQuantity, false);
            //if (!Core.CheckInventory("Dai Tengu Essence", EssenceQuantity))
            Core.DebugLogger(this, "Dai Tengu Essence");
            ArmyKillMonster("hachiko", "Roof", "Left", "Dai Tengu", "Dai Tengu Essence", EssenceQuantity, false);
            //if (!Core.CheckInventory("Unending Avatar Essence", EssenceQuantity))
            Core.DebugLogger(this, "Unending Avatar Essence");
            ArmyKillMonster("timevoid", "Frame8", "Left", "Unending Avatar", "Unending Avatar Essence", EssenceQuantity, false);
            //if (!Core.CheckInventory("Void Dragon Essence", EssenceQuantity))
            Core.DebugLogger(this, "Void Dragon Essence");
            ArmyKillMonster("dragonchallenge", "r4", "Left", "Void Dragon", "Void Dragon Essence", EssenceQuantity, false);
            //if (!Core.CheckInventory("Creature Creation Essence", EssenceQuantity))
            Core.DebugLogger(this, "Creature Creation Essence");
            ArmyKillMonster("maul", "r3", "Down", "Creature Creation", "Creature Creation Essence", EssenceQuantity, false);
        }
        Core.CancelRegisteredQuests();
        Core.Logger("THANKS FOR RIDING THE PAIN TRAIN!");
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
        Core.Join(map, cell, pad, publicRoom: publicRoom);
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
            Bot.Options.AggroMonsters = true;
            Bot.Kill.Monster(monster);
            Core.Rest();
        }
        else
        {
            Bot.Options.AggroMonsters = true;
            Core._KillForItem(monster, item, quant, isTemp, log: log);
        }
    }

    public void PlayerAFK()
    {
        Core.Logger("Anti-AFK engaged");
        Bot.Sleep(1500);
        Bot.Send.Packet("%xt%zm%afk%1%false%");
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
//                 "Waiting for " + String.Join(" & ", PartyMembers.Where(x => !Bot.Map.CellPlayers.Select(x => x.Name).Contains(x)).ToList()));
//             else if (Bot.Map.PlayerNames.Count() > 0)
//                 Core.Logger($"[{Bot.Map.PlayerNames.Count()}/{PartySize}] " +
//                 "Waiting for " + String.Join(" & ", PartyMembers.Where(x => !Bot.Map.PlayerNames.Contains(x)).ToList()));
//             else
//                 Core.Logger($"[{Bot.Map.PlayerCount}/{PartySize}] Waiting for the rest of the party");
//             i = 0;
//         }
//     }
