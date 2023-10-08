/*
name: script name here
description: Farms [InsertItem]  using your army.
tags: army, [item]
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

        // Instructions for using the ArmyBits method 
        // 1. Fill in the map name.
        // 2. Fill in the cell(s) you want to jump to (can be multiple cells).
        // 3. Fill in the MonsterMapID(s) you want to target (can be multiple IDs for multi-targeting).
        // 4. Fill in the item(s) you want to farm (can be multiple items).
        // 5. Fill in the desired quantity of the item(s).
        // 6. Uncomment the appropriate method call based on single/multi-targeting.
        // 7. Repeat the method call for each item you want to farm.

        #region Edit This vvv                                           vvv Edit this
        //~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~
        // Single-target example:
        // ArmyBits("map", new[] { "cell" }, MonsterMapID, new[] { "item" }, 1, ClassType.Solo);

        // Multi-target example:
        // ArmyBits("map", new[] { "cell" }, new[] { MonsterMapID1, MonsterMapID2 }, new[] { "item" }, 1, ClassType.Solo);
        //~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~
        #endregion Edit This ^^^                                           ^^^ Edit this

        Core.SetOptions(false);
    }

    public void ArmyBits(string map, string[] cell, int MonsterMapID, string[] items, int quant, ClassType classToUse)
    {
        // Setting up private rooms and class
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();
        Core.EquipClass(classToUse);
        Core.AddDrop(items);

        // Single-target scenario
        // Explanation: For each item you specified, target the specified MonsterMapID
        foreach (string item in items)
        {
            // Aggro and divide on cells
            Army.AggroMonMIDs(MonsterMapID);
            Army.AggroMonStart(map);
            Army.DivideOnCells(cell);

            // Farm the specified item
            while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
            {
                Bot.Combat.Attack(MonsterMapID);
                Bot.Sleep(Core.ActionDelay);
                if (Core.CheckInventory(item))
                    break;
            }

            // Wait for the party
            Army.waitForParty(map, item);
        }

        // Clean up
        Core.JumpWait();
        Army.AggroMonStop(true);
        Core.CancelRegisteredQuests();
    }

    public void ArmyBits(string map, string[] cell, int[] MonsterMapIDs, string[] items, int quant, ClassType classToUse)
    {
        // Setting up private rooms and class
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();
        Core.EquipClass(classToUse);
        Core.AddDrop(items);

        // Multi-target scenario
        // Explanation: For each item you specified, target all specified MonsterMapIDs
        foreach (string item in items)
        {
            // Aggro, divide on cells, and target specified MonsterMapIDs
            Army.AggroMonMIDs(MonsterMapIDs);
            Army.AggroMonStart(map);
            Army.DivideOnCells(cell);

            while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
            {
                foreach (int monsterMapID in MonsterMapIDs)
                {
                    if (Core.IsMonsterAlive(monsterMapID, useMapID: true))
                    {
                        Bot.Combat.Attack(monsterMapID);
                        Bot.Sleep(Core.ActionDelay);
                        if (Core.CheckInventory(item))
                            break;
                    }
                }

                // Wait for the party
                Army.waitForParty(map, item);
            }

            // Clean up
            Core.JumpWait();
            Army.AggroMonStop(true);
            Core.CancelRegisteredQuests();
        }
    }
}
