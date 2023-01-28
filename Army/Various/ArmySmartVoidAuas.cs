/*
name:  Army Smart Void Auras
description:  
tags: army, void aura, methods, smart
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class ArmySmartVoidAuras
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public bool DontPreconfigure = true;
    public string OptionsStorage = "RVAArmy";
    public List<IOption> Options = new List<IOption>()
    {
        new Option<int>("armysize","Number of Accounts", "Input the number of players that it will be waiting for", 1),
        CoreBots.Instance.SkipOptions
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

    public string[] VASDKA =
    {
        "Void Aura",
        "Empowered Essence",
        "Malignant Essence"
    };

    public string[] SellMe =
    {
        "Astral Ephemerite Essence",
        "Belrot the Fiend Essence",
        "Black Knight Essence",
        "Tiger Leech Essence",
        "Carnax Essence",
        "Chaos Vordred Essence",
        "Dai Tengu Essence",
        "Unending Avatar Essence",
        "Void Dragon Essence",
        "Creature Creation Essence",
        "Empowered Essence",
        "Malignant Essence"
    };



    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(VA);
        Core.SetOptions();
        
        WaitingRoom();

        Core.SetOptions(false);
    }

    public void WaitingRoom()
    {
        Core.Logger($"We have {Bot.Config.Get<int>("armysize")} passenger/s signed up\n" +
                                                            "lets hope this works LFMAO");
        Bot.Events.PlayerAFK += PlayerAFK;
        Bot.Sleep(2500);

        if (Core.CheckInventory(14474))
            Core.Unbank(VASDKA);
        else Core.Unbank(VA);
        
        Core.Logger("Selling essences for syncing purposes");
        foreach (string item in SellMe)
            Core.SellItem(item, all: true);

        if (Core.CheckInventory(14474))
            CommandingShadowEssences(7500);
        else RetrieveVoidAurasArmy(7500);
    }

    public void CommandingShadowEssences(int Quantity)
    {
        if (Core.CheckInventory("Void Aura", Quantity))
            return;

        Core.AddDrop("Void Aura");
        Core.Logger($"Farming Void Aura's with SDKA Method");

        EssenceQuantity = 50;
        Core.AddDrop(VA);
        Core.FarmingLogger($"Void Aura", Quantity);
        Core.ConfigureAggro();
        Core.Logger("Army may get slightly out of sync\n" +
                    "(4-5 items on farm mobs, 0 - 1 on boss mobs, still better then before");

        while (!Bot.ShouldExit && !Core.CheckInventory("Void Aura", Quantity))
        {
            Core.EnsureAccept(4439);
            Core.EquipClass(ClassType.Farm);
            ArmyKillMonster("shadowrealmpast", "Enter", "Spawn", "*", "Empowered Essence", 50, false);
            Core.EquipClass(ClassType.Solo);
            ArmyKillMonster("shadowrealmpast", "r4", "Left", "Shadow Lord", "Malignant Essence", 3, false);
            Core.EnsureComplete(4439);
        }
        Core.ConfigureAggro(false);
        Core.Logger("THANKS FOR RIDING THE PAIN TRAIN!");
    }

    public void RetrieveVoidAurasArmy(int Quantity)
    {
        if (Core.CheckInventory("Void Aura", Quantity))
            return;

        Core.Logger($"Farming Void Aura's with Non-SDKA Method");

        EssenceQuantity = 100;
        Farm.EvilREP();
        Core.EquipClass(ClassType.Solo);
        Core.AddDrop(VA);
        if (!Core.CheckInventory("Necromancer", toInv: false) && !Core.CheckInventory("Creature Shard", toInv: false))
            Core.AddDrop("Creature Shard");
        Core.FarmingLogger($"Void Aura", Quantity);
        Core.ConfigureAggro();
        Core.Logger("Army may get slightly out of sync\n" +
                    "(4-5 items on farm mobs, 0 - 1 on boss mobs, still better then before");

        while (!Bot.ShouldExit && !Core.CheckInventory("Void Aura", Quantity))
        {
            Core.EnsureAccept(4432);
            ArmyKillMonster("timespace", "Frame1", "Left", "Astral Ephemerite", "Astral Ephemerite Essence", EssenceQuantity, false);
            ArmyKillMonster("citadel", "m13", "Left", "Belrot the Fiend", "Belrot the Fiend Essence", EssenceQuantity, false);
            ArmyKillMonster("greenguardwest", "BKWest15", "Down", "Black Knight", "Black Knight Essence", EssenceQuantity, false);
            ArmyKillMonster("mudluk", "Boss", "Down", "Tiger Leech", "Tiger Leech Essence", EssenceQuantity, false);
            ArmyKillMonster("aqlesson", "Frame9", "Right", "Carnax", "Carnax Essence", EssenceQuantity, false);
            ArmyKillMonster("necrocavern", "r16", "Down", "Chaos Vordred", "Chaos Vordred Essence", EssenceQuantity, false);
            ArmyKillMonster("hachiko", "Roof", "Left", "Dai Tengu", "Dai Tengu Essence", EssenceQuantity, false);
            ArmyKillMonster("timevoid", "Frame8", "Left", "Unending Avatar", "Unending Avatar Essence", EssenceQuantity, false);
            ArmyKillMonster("dragonchallenge", "r4", "Left", "Void Dragon", "Void Dragon Essence", EssenceQuantity, false);
            ArmyKillMonster("maul", "r3", "Down", "Creature Creation", "Creature Creation Essence", EssenceQuantity, false);
            Core.EnsureCompleteMulti(4432);
        }
        Core.ConfigureAggro(false);
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
            Bot.Sleep(2500);
        }
        Core.EquipClass(monster == "Astral Ephemerite" ? ClassType.Farm : ClassType.Solo);
        Bot.Sleep(2500);
        if (item == null)
        {
            if (log)
                Core.Logger($"Killing {monster}");
            Bot.Kill.Monster(monster);
            Core.Rest();
        }
        else
        {
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
