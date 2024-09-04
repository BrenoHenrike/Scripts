/*
name: ArmySingleRib
description: Uses an army to kill binky in /doomvault to complet ethe `Single Rib` quest.
tags: single, rib, binky, legion, tokens, 
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
//cs_include Scripts/Legion/CoreLegion.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Monsters;
using Skua.Core.Options;

public class ArmySingleRib
{
    private static IScriptInterface Bot => IScriptInterface.Instance;
    private static CoreBots Core => CoreBots.Instance;
    private readonly CoreArmyLite Army = new();
    private static readonly CoreArmyLite sArmy = new();
    public CoreLegion Legion = new();

    // Comment out one of these depending:
    readonly int[] QuestIDs = { 3393 };

    public string OptionsStorage = "CustomAggroMon";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        //scroll to after the `WTFisGoingOn()` void, and edit the enum for the quest rewards vv
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
        Core.BankingBlackList.AddRange(Core.QuestRewards(QuestIDs));

        Core.SetOptions();

        WTFisGoingOn();

        Core.SetOptions(false);
    }

    public void WTFisGoingOn()
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();
        Core.EnsureLoad(QuestIDs);

        Legion.JoinLegion();
        Core.OneTimeMessage("##READ ME###", "it will take a minute when u divide on the binky\n" +
        "cell as the map takes you to the `Innit` cell first\n" +
        "then itll jump to binky after a moment", true, true);
        ArmyBits("doomvault", new[] { "r5" }, new[] { 9 }, new[] { ("Legion Token", 50001) }, ClassType.Solo, QuestIDs);
    }


    public void ArmyBits(string map, string[] cell, int[] MonsterMapIDs, (string, int)[] ItemandQuants, ClassType classToUse, int[] QuestIDs)
    {
        // Setting up private rooms and class
        Core.EquipClass(classToUse);

        // Adding each item without its quantity to Core.AddDrop
        foreach (var (item, _) in ItemandQuants)
        {
            Core.AddDrop(item);
        }

        Core.AddDrop(Core.QuestRewards(QuestIDs));
        Core.RegisterQuests(QuestIDs);

        bool inventoryConditionMet = ItemandQuants.All(t => Core.CheckInventory(t.Item1, t.Item2, toInv: true));

        if (inventoryConditionMet)
            return;

        Army.AggroMonStart(map);
        Army.DivideOnCells(cell);
        Army.AggroMonMIDs(MonsterMapIDs);


        Bot.Player.SetSpawnPoint();
        string dividedCell = Bot.Player.Cell;

        while (!Bot.ShouldExit && !inventoryConditionMet)
        {
            foreach (Monster monster in Bot.Monsters.CurrentAvailableMonsters)
            {
                while (!Bot.ShouldExit && Bot.Player.Cell != dividedCell)
                {
                    // Ensure the player is on the divided cell
                    Core.Jump(dividedCell, "Left");
                    Core.Sleep();
                    if (Bot.Player.Cell == dividedCell)
                        break;
                }

                // Single Target: 
                Bot.Kill.Monster(monster.MapID);

                // Check inventory conditions
                inventoryConditionMet = ItemandQuants.All(t => Core.CheckInventory(t.Item1, t.Item2, toInv: true));

                // Break loop if inventory condition is met
                if (inventoryConditionMet)
                {
                    // Clean up and exit
                    Army.AggroMonStop(true);
                    Core.JumpWait();
                    goto CleanUp;
                }
            }
        }

    // Clean up section
    CleanUp:
        Army.AggroMonStop(true);
        Core.JumpWait();
        Core.CancelRegisteredQuests();

    }

}
