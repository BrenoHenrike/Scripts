/*
name: ArmyTemplatev8
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Monsters;
using Skua.Core.Models.Quests;
using Skua.Core.Options;

public class ArmyTemplatev8 //Rename This.. ye we skipped one (v7 was private)
{
    private static IScriptInterface Bot => IScriptInterface.Instance;
    private static CoreBots Core => CoreBots.Instance;
    private readonly CoreArmyLite Army = new();
    private static readonly CoreArmyLite sArmy = new();

    public string OptionsStorage = "CustomAggroMon";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        //scroll to after the `WTFisGoingOn()` void, and edit the enum for the quest rewards vv
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


    // Comment out one of these depending:
    readonly int[] QuestIDs = { 1, 2, 3, 4, 5 };
    readonly int PickRewardQuest = 1;

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

        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        // Instructions for using the ArmyBits method 
        // 1. Fill in the map name.
        // 2. Fill in the cell(s) you want to jump to (can be multiple cells).
        // 3. Fill in the MonsterMapID(s) you want to target (can be multiple IDs for multi-targeting).
        // 4. Fill in the item(s) you want to farm (can be multiple items).
        // 5. Fill in the desired quantity of the item(s). --can only use 1 quant atm unless you wanna start getting into ditionary stuff.. and i cba >:()
        // 6. Leave the `QuestIDs` param alone as it takes the ints from what you edited above.
        // 7. Uncomment the appropriate method  based on single/multi-targeting.
        // 8. Repeat the method for each item you want to farm.

        #region Edit This vvv                                           vvv Edit this
        //~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~
        Core.EnsureLoad(QuestIDs);

        // vv more for single item farming like DreadRock LTs
        if (Bot.Config!.Get<Rewards>("QuestRewards") == Rewards.Off)
        {
            // if u want to do a continous farm just do maxstack+1 in the quant
            // vv        Select one of these [single/multi target] to use [leave the `QuestIDs` as it goes off of what is at the top there]    vvvv

            // 1. Single-target example (target MID is the first 1):
            // ArmyBits("map", new[] { "cell" }, 1, new[] { "item" }, 1, ClassType.Solo, QuestIDs);

            // 2. Multi-target example (target MIDs is the first { 1, 2}):
            // ArmyBits("map", new[] { "cell" }, new[] { 1, 2 }, new[] { "item" }, 1, ClassType.Solo, QuestIDs);

            // 3. Multi-target + mMlti (item-quant)s: (highlight lines 95-103 and press control+/ to uncomment it) vv
            ArmyBits("map", new[] { "cell", "cell" }, new[] { 1, 2, 3 }, new[] { ("item", 99999) }, ClassType.Farm, QuestIDs);
            // {

            //     //Edit item and quants here
            //     ("item1", 1),
            //     ("item2", 2),
            //     // Add more items as needed

            // }, ClassType.Solo, QuestIDs);

        }

        //this is for the `Pick Reward`vv
        else
        {
            // Edit these here replacing item# & "item" with the apropriate item name        
            // -- the [1][2] corresponds to  the Questid posion in the  "readonly int[] QuestIDs = new[] { 0000 };" above {1,2,3}  <- [2] is "2".
            // you can add more Questitem# item bases for each item you want
            // add more Quest#Item#

            ItemBase? Quest1Item1 = Core.EnsureLoad(PickRewardQuest)?.Rewards.FirstOrDefault(x => x.Name == "Item");
            ItemBase? Quest1Item2 = Core.EnsureLoad(PickRewardQuest)?.Rewards.FirstOrDefault(x => x.Name == "Item");
            //add more itembases if needed, and if the quest is different change the QuetID in teh `EnsureLoad()`.

            while (!Bot.ShouldExit
                    //Change the quant if u dont want max stack (replace the Item!.MaxStack with your desired quant)
                    && !Core.CheckInventory(Quest1Item1!.Name, Quest1Item1!.MaxStack)
                    && !Core.CheckInventory(Quest1Item2!.Name, Quest1Item2!.MaxStack))
            //add `&& !Core.CheckInventory(item!.Name, item!.MaxStack)` if more items are in the rewards.
            // 'QuestIDs' can be edited above and dont need to be inserted here vv
            {


                // if u want to do a continous farm just do maxstack+1 in the quant
                // vv        Select one of these [single/multi target] to use    vvvv

                // 1. Single-target example:
                // ArmyBits("map", new[] { "cell" }, 1, new[] { "item" }, 1, ClassType.Solo, QuestIDs);

                // 2. ulti-target example:
                // ArmyBits("map", new[] { "cell" }, new[] { 1, 2 }, new[] { "item" }, 1, ClassType.Solo, QuestIDs);

                // 3. Multi-target + mMlti (item-quant)s: (highlight lines 95-103 and press control+/ to uncomment it) vv
                // ArmyBits("map", new[] { "cell", "cell" }, new[] { 1, 2, 3 }, new[]
                // {

                //     //Edit item and quants here
                //     ("item1", 1),
                //     ("item2", 2),
                //     // Add more items as needed

                // }, ClassType.Solo, QuestIDs);

                // --Max stack all--
                if (Bot.Config!.Get<Rewards>("QuestRewards") == Rewards.All)
                    foreach (var rewardValue in Enum.GetValues(typeof(Rewards)).Cast<int>().Where(value => value != (int)Rewards.All || value != (int)Rewards.Off))
                    {
                        ItemBase? rewardItem = Core.EnsureLoad(PickRewardQuest)?.Rewards.FirstOrDefault(x => x.ID == rewardValue);

                        //!!!dont change the quant here ----------------------------------vvvvv!!!
                        if (rewardItem != null && !Core.CheckInventory(rewardItem.Name, rewardItem.MaxStack))
                        {
                            Core.EnsureComplete(PickRewardQuest, rewardValue);
                            continue;
                        }
                    }

                // --Max stack specific--
                else if (Bot.Config!.Get<Rewards>("QuestRewards") != Rewards.All && Bot.Config!.Get<Rewards>("QuestRewards") != Rewards.Off)
                {
                    foreach (var rewardValue in Enum.GetValues(typeof(Rewards)).Cast<int>().Where(value => value != (int)Rewards.All || value != (int)Rewards.Off))
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

                    //~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~~-~-~
                }
                #endregion Edit This ^^^                                          ^^^ Edit this
            }
        }
    }

    //Edit this for when Using the "Pick Reward option, leave "All" and "Off" alone, only edite the items.
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

    // Use for used for: MUltiple mobs, items, and quanities of those items.
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
        Core.EnsureAcceptmultiple(true, QuestIDs);

        bool inventoryConditionMet = ItemandQuants.All(t => Core.CheckInventory(t.Item1, t.Item2, toInv: true));

        if (inventoryConditionMet)
            return;

        Army.AggroMonMIDs(MonsterMapIDs);
        Army.AggroMonStart(map);
        Army.DivideOnCells(cell);


        Bot.Player.SetSpawnPoint();
        string dividedCell = Bot.Player.Cell;

        while (!Bot.ShouldExit && !inventoryConditionMet)
        {
            foreach (Monster monster in Bot.Monsters.MapMonsters.Where(x => MonsterMapIDs.Contains(x.MapID) && x != null))
            {
                while (!Bot.ShouldExit && Bot.Player.Cell != dividedCell)
                {
                    // Ensure the player is on the divided cell
                    Core.Jump(dividedCell);
                    Core.Sleep();
                    if (Bot.Player.Cell == dividedCell)
                        break;
                }

                foreach (var pair in ItemandQuants)
                    Core.Logger($"- \"{pair.Item1}\", {Bot.Inventory.GetQuantity(pair.Item1)} / {pair.Item2}");

                Core.Sleep();

                #region Pickone
                // Farming
                Bot.Combat.Attack(monster.ID);

                // Single Target: 
                Bot.Kill.Monster(monster.MapID);
                #endregion Pickone

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

                // Check and complete quests if conditions are met after attacking
                if (Bot.Quests?.CanComplete(QuestIDs.FirstOrDefault()) ?? false)
                {
                    // Check if quest rewards are disabled
                    if (Bot.Config!.Get<Rewards>("QuestRewards") == Rewards.Off)
                    {
                        // Iterate through quest IDs and complete them
                        foreach (int questID in QuestIDs)
                        {
                            if (Bot.Quests!.CanComplete(questID))
                                Core.EnsureCompleteMulti(questID);
                        }
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

    #endregion IgnoreME


    /* Tato Note
        1. removed the other 2, as this one encompasses the same ideas.
        2. its easier this way to keep track of things.
    */

    /* ToDos: 
        1. Seperate pick & non-pick into seperate templates. ETA: eventualy~
        2. Clean up template, rewrite comments as they are outdated.
    */

    /* Tato Note 2:
    `IsMosnterAlive` is now broke, as monster aways return "Alive = true" (even when killed), so we have to either:
        A. do a single kill first to have the client set thier HP
        B. have the mob have already been killed (do a `PreFarm` kill)
    SO if something has `IsMosnterAlive` before the while or the farm itself do a single kill by making sure you're within the map, and cell and do a Bot.Kill.Monster(MonsterMapID);

    #region Example:   

        // vprefarmkillv
        // --------------
        Bot.Kill.Monster(M.MapID);
        // --------------

        // --proceed to farm now that we've set the mobs HP--
        while(!Bot.ShouldExit && M.HP > 0)
        {
            // -stuff-
            // Single Target(bosses?):  
                Bot.Combat.Attack(M.MapID);

            // Multi-Target(farming shit)      
                Bot.Combat.Attack(M.ID);
            Core.Sleep(0;
            // -stuff-

            if(checkinv("Item", quant))
                break;
        }
        // --proceed to farm now that we've set the mobs HP--
    #endregion Example:    
*/
}
