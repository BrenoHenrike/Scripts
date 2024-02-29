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
using Skua.Core.Models.Items;
using Skua.Core.Models.Monsters;
using Skua.Core.Options;

public class ArmyTemplatev5 //Rename This
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv => new();
    private CoreArmyLite Army = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "CustomAggroMon";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        //scroll to after the `WTFisGoingOn()` void, and edit teh enum for the quest rewards vv
        new Option<Rewards>("QuestRewards", "Pick your reward", "Pick your reward", Rewards.Off),
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


    //commet out one of these depending:
    readonly int[] QuestIDs = new[] { 0000 };
    readonly int PickRewardQuest = 0000;

    public void ScriptMain(IScriptInterface bot)
    {

        //automaticly add the quest rewards to the banking blacklist (it wotn bank then even if bankmisc in corebots is on)

        //Non-Pick Reward Quest:
        Core.BankingBlackList.AddRange(Core.QuestRewards(QuestIDs));
        //Pick Reward Quest:
        // Core.BankingBlackList.AddRange(Core.QuestRewards(PickRewardQuest));

        Core.SetOptions();

        WTFisGoingOn();

        Core.SetOptions(false);
    }

    //move everything outside the scriptmain (exe will yell at you otherwise)
    //take the void name vvv and put it after the set options above.
    public void WTFisGoingOn()
    {
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

        // Edit these here replacing item# & "item" with the apropriate item name        
        // -- the [1][2] corresponds to  the Questid posion in the  "readonly int[] QuestIDs = new[] { 0000 };" above {1,2,3}  <- [2] is "2".
        ItemBase? Item1 = Core.EnsureLoad(QuestIDs[1])?.Rewards.FirstOrDefault(x => x.Name == "Item");
        ItemBase? Item2 = Core.EnsureLoad(QuestIDs[2])?.Rewards.FirstOrDefault(x => x.Name == "Item");
        //add more itembases if needed, and if the quest is different change the QuetID in teh `EnsureLoad()`.


        while (!Bot.ShouldExit &&
        //Change the quant if u dont want max stack (replace the Item!.MaxStack with your desired quant)
        !Core.CheckInventory(Item1!.Name, Item1!.MaxStack)
        && !Core.CheckInventory(Item2!.Name, Item2!.MaxStack))
        //add `&& !Core.CheckInventory(item!.Name, item!.MaxStack)` if more items are in the rewards.
        // 'QuestIDs' can be edited above and dont need to be inserted here vv
        {

            // vv        Select one of these [single/multi target] to use    vvvv
            // Single-target example:
            // ArmyBits("map", new[] { "cell" }, 1, new[] { "item" }, 1, ClassType.Solo, QuestIDs);

            // Multi-target example:
            // ArmyBits("map", new[] { "cell" }, new[] { 1, 2 }, new[] { "item" }, 1, ClassType.Solo, QuestIDs);

            if (Bot.Config!.Get<Rewards>("Pick your reward") == Rewards.All)
                foreach (var rewardValue in Enum.GetValues(typeof(Rewards)).Cast<int>().Where(value => value != (int)Rewards.All))
                {
                    var rewardItem = Bot.Inventory.Items.FirstOrDefault(x => x.ID == rewardValue);

                    //!!!dont change the quant here ----------------------------------vvvvv!!!
                    if (rewardItem != null && !Core.CheckInventory(rewardItem.Name, rewardItem.MaxStack))
                    {
                        Core.EnsureComplete(PickRewardQuest, rewardValue);
                        continue;
                    }
                }

            else if (Bot.Config!.Get<Rewards>("Pick your reward") != Rewards.All && Bot.Config!.Get<Rewards>("Pick your reward") != Rewards.Off)
            {
                foreach (var rewardValue in Enum.GetValues(typeof(Rewards)).Cast<int>().Where(value => value != (int)Rewards.All))
                {
                    ItemBase? RewardID = Core.EnsureLoad(PickRewardQuest)?.Rewards.FirstOrDefault(x => x.ID == rewardValue);
                    switch (rewardValue)
                    {
                        // add more cases if the quest has more rewards, and add them and their itemids to the enum below
                        // you may edit here vvv
                        case (int)Rewards.item_one:
                        case (int)Rewards.item_two:
                            {
                                //leave this quant be its just to set an int
                                int Quant = 0;

                                // you may change the quant here --------vvv
                                //these 2 quants will set the Checkinvs quants below
                                if (rewardValue == (int)Rewards.item_one)
                                    Quant = 0;
                                else if (rewardValue == (int)Rewards.item_two)
                                    Quant = 0;
                                // you may change the quant here --------^^

                                //leave this vv as it goes of he quants above here
                                if (!Core.CheckInventory(rewardValue, Quant > 0 ? Quant : RewardID!.ID))
                                    Core.EnsureComplete(PickRewardQuest, rewardValue);
                                continue;
                            }
                    }

                }
            }

            //~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~
        }
        #endregion Edit This ^^^                                           ^^^ Edit this
    }

    private enum Rewards
    {
        //you may edit here vvv
        //[item_name = itemID,] - _'s are required
        All = 0,
        item_one = 1,
        item_two = 2,
        Off = 3

        //you may edit here ^^^
    };

    //ignore everything below here vvvvvv
    #region IgnoreME

    // Use this one for when its a single map, and your just aggroing around (Example: Dreadrock LTs)
    public void ArmyBits(string map, string[] cell, int MonsterMapID, string[] items, int quant, ClassType classToUse, int[] QuestIDs)
    {
        // Setting up private rooms and class
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();
        Core.EquipClass(classToUse);
        Core.AddDrop(items);

        Core.EnsureAcceptmultiple(Bot.Config!.Get<Rewards>("Pick your reward") != Rewards.Off, QuestIDs);

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
                foreach (Monster Mob in Bot.Monsters.CurrentAvailableMonsters.Where(m => m.MapID == MonsterMapID))
                {
                    while (!Bot.ShouldExit && Core.IsMonsterAlive(Mob.MapID, true))
                    {
                        Bot.Combat.Attack(Mob.MapID);
                        if (Bot.Config!.Get<Rewards>("Pick your reward") == Rewards.Off)
                        {
                            foreach (int Q in QuestIDs)
                                if (Bot.Quests.CanComplete(Q))
                                    Bot.Quests.Complete(Q);
                        }
                        if (Core.CheckInventory(item, quant))
                            break;
                    }
                }
            }

            // Clean up
            Army.AggroMonStop(true);
            Core.JumpWait();
            Core.CancelRegisteredQuests();
        }
    }

    // Use this one for multi-mob farms (like VA's where there are multiple bosses)
    public void ArmyBits(string map, string[] cell, int[] MonsterMapIDs, string[] items, int quant, ClassType classToUse, int[] QuestIDs)
    {
        // Setting up private rooms and class
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();
        Core.EquipClass(classToUse);
        Core.AddDrop(items);

        Core.EnsureAcceptmultiple(Bot.Config!.Get<Rewards>("Pick your reward") != Rewards.Off, QuestIDs);

        foreach (string item in items)
        {
            bool inventoryConditionMet = Core.CheckInventory(item, quant);

            if (inventoryConditionMet)
                return;

            Army.AggroMonMIDs(MonsterMapIDs);
            Army.AggroMonStart(map);
            Army.DivideOnCells(cell);
            Core.FarmingLogger(item, quant);
            Bot.Player.SetSpawnPoint();
            string DividedCell = Bot.Player.Cell;

            Core.EnsureAcceptmultiple(true, QuestIDs);

            while (!Bot.ShouldExit && !inventoryConditionMet)
            {
                foreach (int monsterMapID in MonsterMapIDs)
                {
                    while (!Bot.ShouldExit && Bot.Player.Cell != DividedCell)
                    {
                        Core.Jump(DividedCell);
                        Core.Sleep();
                        if (Bot.Player.Cell == DividedCell)
                            break;
                    }

                    while (!Bot.ShouldExit && Core.IsMonsterAlive(monsterMapID, useMapID: true))
                        Bot.Combat.Attack(monsterMapID);

                    if (Bot.Config!.Get<Rewards>("Pick your reward") == Rewards.Off)
                    {
                        foreach (int Q in QuestIDs)
                            if (Bot.Quests.CanComplete(Q))
                                Bot.Quests.Complete(Q);
                    }
                    
                    inventoryConditionMet = Core.CheckInventory(item, quant);

                    // Break out of the foreach loop
                    if (inventoryConditionMet)
                    {
                        Army.AggroMonStop(true);
                        Core.JumpWait();
                        break;
                    }
                }
            }
        }

        #endregion IgnoreME
    }
}