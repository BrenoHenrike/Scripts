/*
name: ArmyAggromonCreatorAutoReward
description: start the script, input the information in the options window, and let it go (read descriptions carefully)
tags: army, aggro, manual, custom, do it yourself
*/
//cs_include Scripts/Army/CoreArmyLite.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
using System.Linq;
using System.Text.RegularExpressions;
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Monsters;
using Skua.Core.Models.Quests;
using Skua.Core.Options;

public class ArmyAggromonCreatorAutoReward
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private readonly CoreArmyLite Army = new();

    private static CoreBots sCore = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyAggromonCreatorAutoReward";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        new Option<string>("Map", "Map to Aggro on", "", ""),
        new Option<string>("MonsterMIDs", "Monster MapIDs", "A list of the MonsterMapIDs of the mobs you wish to aggro (separate them with a comma: 1, 2, 3)", ""),
        new Option<string>("QuestIDs", "Quest IDs", "A list of the quest IDs to use (separate them with a comma: 1,2,3)", ""),
        new Option<string>("Cells", "Army Cells", "Map Cells to split your army \"evenly\" onto [separate them with a comma: r1,r2,r3]", ""),
        new Option<string>("item-int", "Item & ints", "Items and thier desired quantities,  [separate them like so: `(\"item\", quant),(\"item\", quant)`", ""),
        new Option<bool>("ContinuousLogging", "Continuous Logging?", "Spam the Chat with \"item:[]]quant/quant]\"", false),
        new Option<ClassType>("ClassType", "Solo/Farm/None", "What ClassType to equip (goes off of Corebot Options)", ClassType.None),

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

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.OneTimeMessage("Only for army", "This is intended for use with an army, not for solo players.");
        Core.SetOptions();

        WTFisGoingOnv2();

        Core.SetOptions(false);
    }


    //Nothing needs edited, and everything is filled in from the options
    public void WTFisGoingOnv2()
    {
        #region Set Variables

        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        // Retrieving MonsterMIDs from configuration as a string
        string monsterMIDsString = Bot.Config?.Get<string>("MonsterMIDs") ?? "";
        int[] monsterMIDs = monsterMIDsString.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

        // Retrieving QuestIDs from configuration as a string
        string questIDsString = Bot.Config?.Get<string>("QuestIDs") ?? "";
        int[] questIDs = questIDsString.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

        // Retrieving Cells from configuration
        string[] armyCells = (Bot.Config?.Get<string>("Cells") ?? "")
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(cell => cell.Trim())
                .ToArray();

        // Retrieving Map from configuration
        string map = Bot.Config?.Get<string>("Map") ?? "";

        // Retrieving Item & ints from configuration as a string
        string itemIntString = Bot.Config?.Get<string>("item-int") ?? "";

        // Splitting the string into individual ("item", quant) pairs
        var itemsAndQuantities = Regex.Split(itemIntString.Replace("), (", "),("), "\\s*,\\s*\\(")
                .Select(pair =>
                {
                    var parts = pair.Trim('(', ')').Split(',', StringSplitOptions.TrimEntries);
                    if (parts.Length == 2 && int.TryParse(parts[1], out int quantity))
                    {
                        return (parts[0], quantity);
                    }
                    // Handle invalid format, e.g., log or throw an exception
                    return default;
                })
                .Where(itemAndQuantity => itemAndQuantity != default)
                .ToArray();

        #endregion

        #region Check and Log Variables

        // Checking if any of the essential variables are not set
        if (map == null || armyCells == null || monsterMIDs == null || itemsAndQuantities == null)
        {
            // Building the error message based on which variable is not set
            string errorMessage = "Stopping the script. Reason: ";
            if (map == null)
            {
                errorMessage += "Map is not set. ";
            }
            if (armyCells == null)
            {
                errorMessage += "Army Cells are not set. ";
            }
            if (monsterMIDs == null)
            {
                errorMessage += "Monster MapIDs are not set. ";
            }
            if (itemsAndQuantities == null)
            {
                errorMessage += "Item & ints are not set. ";
            }

            Core.Logger(errorMessage, stopBot: true);
        }

        // Log if Monster MapIDs are not null
        if (monsterMIDs != null && monsterMIDs.Length > 0)
            Core.Logger($"Monster MapIDs: {string.Join(", ", monsterMIDs)}");

        // Log if QuestIDs are not null
        if (questIDs != null && questIDs.Length > 0)
            Core.Logger($"QuestIDs: {string.Join(", ", questIDs)}");

        // Log if Army Cells are not null
        if (armyCells != null && armyCells.Length > 0)
            Core.Logger($"Army Cells: {string.Join(", ", armyCells)}");

        // Log if Map is not null
        if (!string.IsNullOrEmpty(map))
            Core.Logger($"Map: {map}");

        if (itemsAndQuantities != null && itemsAndQuantities.Length > 0)
        {
            var itemsLog = itemsAndQuantities.Select(item => $"\n(\"{item.Item1}\", {Bot.Inventory.GetQuantity(item.Item1)} / {item.quantity})");
            Core.Logger($"Item and Quantities:{string.Join("", itemsLog)}");
        }


        #endregion

        #region Execution

        // Example usage in ArmyBits method
        ArmyBits(map!, armyCells!, monsterMIDs!, itemsAndQuantities!, Bot.Config!.Get<ClassType>("ClassType"), questIDs!);

        #endregion
    }


    public void ArmyBits(string map, string[] cell, int[] MonsterMapIDs, (string, int)[] ItemandQuants, ClassType classToUse, int[] QuestIDs)
    {
        // Setting up private rooms and class
        Core.EquipClass(classToUse);

        // Core.AddDrop(Core.QuestRewards(QuestIDs));
        string[] items = ItemandQuants.Select(pair => pair.Item1).ToArray();
        Core.Logger($"{string.Join(", ", items)}");
        Core.AddDrop(items);

        Core.AddDrop(items);
        Core.AddDrop(Core.QuestRewards(QuestIDs));

        Core.RegisterQuests(QuestIDs);

        bool inventoryConditionMet = ItemandQuants.All(t => Core.CheckInventory(t.Item1, t.Item2));

        if (inventoryConditionMet)
            return;

        Army.AggroMonMIDs(MonsterMapIDs);
        Army.AggroMonStart(map);
        Army.DivideOnCells(cell);

        if (!Bot.Config!.Get<bool>("ContinuousLogging"))
            Core.Logger($"Item and Quantities:{string.Join("", ItemandQuants.Select(pair => $"\n(\"{pair.Item1}\", {Bot.Inventory.GetQuantity(pair.Item1)} / {pair.Item2})"))}");

        Bot.Player.SetSpawnPoint();
        string dividedCell = Bot.Player.Cell;

        while (!Bot.ShouldExit && !inventoryConditionMet)
        {
            // Logging the items for farming
            if (Bot.Config!.Get<bool>("ContinuousLogging") && Bot.Player.Alive)
                Core.Logger($"Item and Quantities:{string.Join("", ItemandQuants.Select(pair => $"\n(\"{pair.Item1}\", {Bot.Inventory.GetQuantity(pair.Item1)} / {pair.Item2})"))}");

            foreach (Monster mon in Bot.Monsters.CurrentAvailableMonsters)
            {
                while (!Bot.ShouldExit && mon != null && mon.HP >= 0)
                {
                    while (!Bot.ShouldExit && Bot.Player.Cell != dividedCell)
                    {
                        // Ensure the player is on the divided cell
                        Core.Jump(dividedCell);
                        Core.Sleep();
                        if (Bot.Player.Cell == dividedCell)
                            goto Attack;
                    }

                // Attack the monster
                Attack:
                    Bot.Combat.Attack(mon.MapID);

                    // Check inventory conditions
                    inventoryConditionMet = ItemandQuants.All(t => Core.CheckInventory(t.Item1, t.Item2));

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
        }

    // Clean up section
    CleanUp:
        Army.AggroMonStop(true);
        Core.JumpWait();
        Core.CancelRegisteredQuests();

    }


    // private enum ClassType
    // {
    //     Solo,
    //     Farm,
    //     None
    // }
}


/*
for pick item later:
Core.EnsureAcceptmultiple(true, QuestIDs);
        foreach (int Q in QuestIDs)
        {
            Quest? QuestItems = Bot.Quests.EnsureLoad(Q);
            foreach (ItemBase item in QuestItems!.Rewards)
            {
                var validItemandQuants = ItemandQuants
                    .Where(t => !Core.CheckInventory(t.Item1, t.Item2));

                if (validItemandQuants.Any())
                {
                    Core.EnsureCompleteMulti(Q, -1, item.ID);
                }
            }
        }
*/
