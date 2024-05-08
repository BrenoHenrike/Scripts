/*
name: script name here
description: Farms [InsertItem] using your army.
tags: army, [item]
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;

public class ArmyPoly
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv => new();
    private CoreArmyLite Army = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyPoly";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
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
        Core.SetOptions();

        GetPolly();

        Core.SetOptions(false);
    }

    public void GetPolly()
    {
        Core.EnsureAccept(7713);

        ArmyBits("frozenlair", new[] { "r3" }, 4, new[] { "Sapphire Orb" }, 5);
        ArmyBits("lostruinswar", new[] { "r7" }, 13, new[] { "Rumors of the Celestial Commander" }, 5);
        ArmyBits("iceplane", new[] { "r7", "r8", "r9" }, new[] { 8, 10, 12 }, new[] { "Starlit Journal Page 1 Scraps" }, 10);
        ArmyBits("ivoliss", new[] { "r11" }, 20, new[] { "Starlit Journal Page 2 Scraps" }, 10);
        ArmyBits("voidnightbane", new[] { "Enter" }, 1, new[] { "Starlit Journal Page 3 Scraps" }, 10);
        ArmyBits("extinction", new[] { "r12" }, 39, new[] { "Starlit Journal Page 4 Scraps" }, 10);
        ArmyBits("starsinc", new[] { "r16" }, 25, new[] { "Map of the Celestial Seas" }, 1);
        ArmyBits("underlair", new[] { "r7" }, 17, new[] { "Coffer of the Stars" }, 1);
        Core.EnsureCompleteChoose(7713, new[] { "Polly Roger" });
    }

    public void ArmyBits(string map, string[] cell, int MonsterMapID, string[] items, int quant)
    {
        // Setting up private rooms and class
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();
        Core.EquipClass(ClassType.Solo);
        Core.AddDrop(items);

        // Single-target scenario
        // Explanation: For each item you specified, target the specified MonsterMapID
        foreach (string item in items)
        {
            // Aggro and divide on cells
            Army.AggroMonStart(map);
            Army.DivideOnCells(cell);

            // Farm the specified item
            while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
                Bot.Combat.Attack(MonsterMapID);

            // Clean up
            Army.AggroMonStop(true);
            Core.JumpWait();
            Core.CancelRegisteredQuests();

            // Wait for the party
            //Army.waitForParty(map, item);
        }
    }

    public void ArmyBits(string map, string[] cell, int[] MonsterMapIDs, string[] items, int quant)
    {
        // Setting up private rooms and class
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();
        Core.EquipClass(ClassType.Solo);
        Core.AddDrop(items);

        // Multi-target scenario
        // Explanation: For each item you specified, target all specified MonsterMapIDs
        foreach (string item in items)
        {
            while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
            {
                // Aggro, divide on cells, and target specified MonsterMapIDs
                Army.AggroMonStart(map);
                Army.DivideOnCells(cell);
                Army.AggroMonMIDs(MonsterMapIDs);
                    
                // Farm the specified item
                while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
                    Bot.Combat.Attack("*");
            }
            // Clean up
            Army.AggroMonStop(true);
            Core.JumpWait();
            Core.CancelRegisteredQuests();

            // Wait for the party
            //Army.waitForParty(map, item);
        }

        // Clean up
        Army.AggroMonStop(true);
        Core.CancelRegisteredQuests();
    }

}
