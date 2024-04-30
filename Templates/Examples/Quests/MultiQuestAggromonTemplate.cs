/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class MultiQuestAggromonTemplate  //<-- replace
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public static CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public static CoreAdvanced Adv => new();
    private readonly CoreArmyLite Army = new();

    private static readonly CoreBots sCore = new();
    private static readonly CoreArmyLite sArmy = new();

    public string OptionsStorage = "RenameME"; //<-- replace
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        new Option<int>("armysize", "Players", "Input the minimum of players to wait for 1-6", 1),
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
        Core.BankingBlackList.AddRange(new[] { "all the item names here, to prevent them from beign banked." }); //<-- fill in stuffs

        Core.SetOptions();

        RenameME();
        Core.SetOptions(false);
    }

    public void RenameME()
    {
        Core.EquipClass(ClassType.Farm);
        Core.AddDrop("item", "item");

        while (!Bot.ShouldExit && !Core.CheckInventory(new[] { "item", "item", "etc" })) //<-- fill in stuffs
        {
            //use these for quest items
            if (!Core.CheckInventory("item"))
                ArmyThing(0000, "map", new[] { 1, 2, 3, 4 }, "drop", 1, isTemp: false); //<-- fill in stuffs
            if (!Core.CheckInventory("item"))                                       //<--                   |
                ArmyThing(0000, "map", new[] { 1, 2, 3, 4 }, "drop", 1, isTemp: false); //<--               |
            if (!Core.CheckInventory("item"))                                       //<--                   |
                ArmyThing(0000, "map", new[] { 1, 2, 3, 4 }, "drop", 1, isTemp: false); //<-- fill in stuffs
        }
    }

    void ArmyThing(int questID, string map, int[] MonsterMapID, string? item, int quant, bool isTemp)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        if (Core.CheckInventory(item, quant))
            return;

        if (item == null)
            return;

        Bot.Drops.Add(item);

        Core.EquipClass(ClassType.Farm);
        Core.FarmingLogger(item, quant);

        Army.waitForParty(map, item);
        Core.EnsureAccept(questID);
        Armyshit(map, MonsterMapID);

        

        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
            Bot.Combat.Attack("*");

        Army.waitForParty("whitemap", item);
        Army.AggroMonStop(true);
        Core.JumpWait();
        Core.EnsureComplete(questID);
        Bot.Wait.ForPickup(item);

        void Armyshit(string map, int[] MonsterMapID)
        {
            if (Bot.Map.Name == null)
                return;

            if (Bot.Map.Name == "MapName") //<-- fill in stuffs
            {
                Army.AggroMonCells("insert", "cells", "here"); //<-- replace the cells with the cells mobs are in.
                Army.AggroMonMIDs(MonsterMapID);
                Army.AggroMonStart(map);
                Army.DivideOnCells("insert", "cells", "here"); //<-- replace the cells with the cells mobs are in.
            }
        }
    }
}
