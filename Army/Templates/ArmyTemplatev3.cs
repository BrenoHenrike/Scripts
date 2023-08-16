/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;

public class ArmyTemplatev3 //Rename This
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

        /*     ~_~_~_~_~_~_~IMPORTANT INFO:~_~_~_~_~_~_~

        map, cell, item, item quant
        if multiple items, seperate them with a comma. ex:{ "item", "item" }
        if multiple cells, seperate them with a comma. ex:{ "cell", "cell" }
        replace map and quanity appropriately below.

        I mostly use the [monsterMapIDS] one for single targets(bosses)
        and [monsterIDs] for multi-target(farming)

        How to get to and use grabber:
        1. Ingame goto the mob
        2.  then on teh top Skua bar > Tools > Grabber > scroll left to "Map Monsters" > bottom "Grab" 
        3. in the grabber it'll show as:  MonsterName [monsterID][monsterMapID, Cell]
        Photo Example: https://i.imgur.com/1oDkh39.png

                            **SO** 
        (1. Multi-Target / 2. Singl-Target) [remove the 2 //'s]:

              ~_~_~_~_~_~_~IMPORTANT INFO:~_~_~_~_~_~_~ */



        // ArmyBits("map", new[] { "cell" }, new[] { MonsterMapID, MonsterMapID, MonsterMapID }, new[] { "item" }, 1);
        // ArmyBits("map", new[] { "cell" }, MonsterMapID, new[] { "item" }, 1);

        Core.SetOptions(false);
    }


    public void ArmyBits(string map, string[] cell, int MonsterMapID, string[] items, int quant)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Core.EquipClass(ClassType.Solo);
        Core.AddDrop(items);


        Army.AggroMonMIDs(MonsterMapID);

        foreach (string item in items)
        {
            Army.AggroMonStart(map);
            Army.DivideOnCells(cell);

            while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
                Bot.Combat.Attack(MonsterMapID);
            Army.waitForParty(map, item);
        }

        Army.AggroMonStop(true);
        Core.CancelRegisteredQuests();
    }

    public void ArmyBits(string map, string[] cell, int[] MonsterMapIDs, string[] items, int quant)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Core.EquipClass(ClassType.Solo);
        Core.AddDrop(items);

        Army.AggroMonStart(map);
        Army.DivideOnCells(cell);

        foreach (string item in items)
        {
            while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
            {
                Army.AggroMonStart(map);
                Army.DivideOnCells(cell);
                Army.AggroMonMIDs(MonsterMapIDs);

                while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
                    Bot.Combat.Attack("*");
                Army.waitForParty(map, item);
            }
            Army.waitForParty(map, item);
        }

        Army.AggroMonStop(true);
        Core.CancelRegisteredQuests();
    }
}
