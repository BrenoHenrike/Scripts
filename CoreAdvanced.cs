/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.DependencyInjection;
using Skua.Core.Interfaces;
using Skua.Core.Models;
using Skua.Core.Models.Items;
using Skua.Core.Models.Monsters;
using Skua.Core.Models.Quests;
using Skua.Core.Models.Shops;
using Skua.Core.Options;
using Skua.Core.Utils;

public class CoreAdvanced
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private readonly CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.RunCore();
    }

    #region Shop

    /// <summary>
    /// Buys a item from a shop, but also try to obtain stuff like XP, Rep, Gold, and merge items (where possible)
    /// </summary>
    /// <param name="map">Map of the shop</param>
    /// <param name="shopID">ID of the shop</param>
    /// <param name="itemName">Name of the item</param>
    /// <param name="quant">Desired quantity</param>
    /// <param name="shopItemID">Use this for Merge shops that has 2 or more of the item with the same name and you need the second/third/etc., be aware that it will re-log you after to prevent ghost buy. To get the ShopItemID use the built in loader of Skua</param>
    /// <param name="Log"></param>
    public void BuyItem(string map, int shopID, string itemName, int quant = 1, int shopItemID = 0, bool Log = true)
    {
        if (Core.CheckInventory(itemName, quant))
            return;

        Core.Join(map);

        if (Bot.Player.InCombat || Bot.Player.HasTarget)
        {
            Core.JumpWait();
            Bot.Wait.ForCombatExit();
        }

        ShopItem? item = Core.parseShopItem(Core.GetShopItems(map, shopID).Where(x => shopItemID == 0 ? x.Name.ToLower() == itemName.ToLower() : x.ShopItemID == shopItemID).ToList(), shopID, itemName);
        if (item == null)
            return;

        _BuyItem(map, shopID, item, quant, Log);
    }

    /// <summary>
    /// Buys a item from a shop, but also try to obtain stuff like XP, Rep, Gold, and merge items (where possible)
    /// </summary>
    /// <param name="map">Map of the shop</param>
    /// <param name="shopID">ID of the shop</param>
    /// <param name="itemID">ID of the item</param>
    /// <param name="quant">Desired quantity</param>
    /// <param name="shopQuant">How many items you get for 1 buy</param>
    /// <param name="shopItemID">Use this for Merge shops that has 2 or more of the item with the same name and you need the second/third/etc., be aware that it will relog you after to prevent ghost buy. To get the ShopItemID use the built in loader of Skua</param>
    /// <param name="Log"></param>
    public void BuyItem(string map, int shopID, int itemID, int quant = 1, int shopQuant = 1, int shopItemID = 0, bool Log = true)
    {
        if (Core.CheckInventory(itemID, quant))
            return;

        Core.Join(map);
        Bot.Wait.ForMapLoad(map);
        Core.DebugLogger(this);

        if (Bot.Player.InCombat || Bot.Player.HasTarget)
        {
            Core.DebugLogger(this);
            Core.JumpWait();
            Bot.Wait.ForCombatExit();
        }

        Core.DebugLogger(this);
        ShopItem? item = Core.parseShopItem(Core.GetShopItems(map, shopID).Where(x => shopItemID == 0 ? x.ID == itemID : x.ShopItemID == shopItemID).ToList(), shopID, itemID.ToString());
        Core.DebugLogger(this);
        if (item == null)
            return;

        _BuyItem(map, shopID, item, quant, Log);
    }

    //if Adv.buy fucks up revert to this::
    // private void _BuyItem(string map, int shopID, ShopItem item, int quant = 1, bool Log = true)
    // {
    //     int ShopQuant = item.Quantity;

    //     if (item.Requirements != null)
    //     {
    //         foreach (ItemBase req in item.Requirements)
    //         {
    //             if (Core.CheckInventory(req.ID, req.Quantity * quant))
    //                 continue;

    //             // Core.Jump(Bot.Player.Cell, Bot.Player.Pad);

    //             switch (req.ID)
    //             {
    //                 case 7132:
    //                     Farm.DragonRunestone(req.Quantity * quant);
    //                     // Farm.DragonRunestone(req.Quantity);
    //                     break;

    //                 default:
    //                     if (Core.GetShopItems(map, shopID).Any(x => req.ID == x.ID))
    //                         BuyItem(map, shopID, req.ID, req.Quantity * quant, Log: Log);
    //                     break;
    //             }
    //         }

    //         GetItemReq(item, quant);
    //         Core._BuyItem(map, shopID, item, quant, Log);
    //     }
    // }


    private void _BuyItem(string map, int shopID, ShopItem item, int quant = 1, bool Log = true)
    {
        int shopQuant = item.Quantity; // Quantity per purchase from the shop

        if (item.Requirements != null)
        {
            foreach (ItemBase req in item.Requirements)
            {
                if (Core.CheckInventory(item.ID, quant))
                    continue;

                // Total required quantity of the required item based on the desired quantity
                int totalReqNeeded = quant; // Total number of main items desired

                // Calculate the total number of the required item that needs to be bought
                int totalBundlesNeeded = (int)Math.Ceiling((double)totalReqNeeded / shopQuant);

                // Check the current quantity of the required item in the inventory
                int currentReqQuantity = Bot.Inventory.GetQuantity(req.ID);

                // Calculate how many more bundles we need to purchase
                int bundlesToBuy = totalBundlesNeeded - (currentReqQuantity / req.Quantity);

                if (bundlesToBuy <= 0)
                    continue; // Skip if we already have enough

                // Specific handling for certain requirements
                switch (req.ID)
                {
                    case 7132: // Example specific handling
                        Farm.DragonRunestone(req.Quantity * bundlesToBuy);
                        break;

                    // Check if the required item is available in the shop and buy it if necessary
                    default:
                        if (Core.GetShopItems(map, shopID).Any(x => req.ID == x.ID))
                            Core.BuyItem(map, shopID, req.Name, req.Quantity * bundlesToBuy, Log: Log);
                        break;
                }
            }

            // Attempt to purchase the main item after ensuring required items are available
            GetItemReq(item, quant);
            Core._BuyItem(map, shopID, item, quant, Log);
        }
    }



    /// <summary>
    /// Will make sure you have every requierment (XP, Rep and Gold) to buy the item.
    /// </summary>
    /// <param name="item">The ShopItem object containing all the information</param>
    /// <param name="quant"></param>
    public void GetItemReq(ShopItem item, int quant = 1)
    {
        if (!string.IsNullOrEmpty(item.Faction) && item.Faction != "None" && item.RequiredReputation > 0)
            runRep(item.Faction, Core.PointsToLevel(item.RequiredReputation));
        Farm.Experience(item.Level);
        if (!item.Coins)
            Farm.Gold(item.Cost * quant);
    }

    private void runRep(string faction, int rank)
    {
        faction = faction.Replace(" ", "");
        Type farmClass = Farm.GetType();
        MethodInfo? theMethod = farmClass.GetMethod(faction + "REP");
        if (theMethod == null)
        {
            Core.Logger("Failed to find " + faction + "REP. Make sure you have the correct name and capitalization.");
            return;
        }

        try
        {
            switch (faction.ToLower())
            {
                case "alchemy":
                case "blacksmith":
                    theMethod.Invoke(Farm, new object[] { rank, true });
                    break;
                case "bladeofawe":
                    theMethod.Invoke(Farm, new object[] { rank, false });
                    break;
                default:
                    theMethod.Invoke(Farm, new object[] { rank });
                    break;
            }
        }
        catch
        {
            Core.Logger($"Faction {faction} has invalid paramaters, please report", messageBox: true, stopBot: true);
        }
    }

    /// <summary>
    /// Buys merge items from a shop based on specified options. Filters ShopItems to ensure uniqueness by ID and ShopItemID,
    /// selecting items based on Upgrade requirements and excluding those ending with "insignia".
    /// </summary>
    /// <param name="map">The map from which the shop is loaded.</param>
    /// <param name="shopID">The shop ID to load shop data.</param>
    /// <param name="findIngredients">Action determining where to retrieve items.</param>
    /// <param name="buyOnlyThis">Optional. Limits purchases to a specific item.</param>
    /// <param name="itemBlackList">Optional. List of excluded items.</param>
    /// <param name="buyMode">Optional. Specifies buying mode.</param>
    /// <param name="Group">Optional. Specifies group selection method.</param>
    /// <param name="ShopItemID">Optional. Specifies ShopItem ID.</param>
    /// <param name="Log">Optional. Enables logging.</param>
    public void StartBuyAllMerge(string map, int shopID, Action findIngredients, string? buyOnlyThis = null, string[]? itemBlackList = null, mergeOptionsEnum? buyMode = null, string Group = "First", int ShopItemID = 0, bool Log = true)
    {
        if (buyOnlyThis == null && buyMode == null && Bot.Config != null && !Bot.Config.Get<bool>(CoreBots.Instance.SkipOptions))
            Bot.Config!.Configure();

        int mode = 0;
        if (buyOnlyThis != null)
            mode = (int)mergeOptionsEnum.all;
        else if (buyMode != null)
            mode = (int)buyMode;
        else if (Bot.Config != null && Bot.Config.MultipleOptions.Any(o => o.Value.Any(x => x.Category == "Generic" && x.Name == "mode")))
            mode = (int)Bot.Config.Get<mergeOptionsEnum>("Generic", "mode");
        else Core.Logger("Invalid setup detected for StartBuyAllMerge. Please report", messageBox: true, stopBot: true);

        matsOnly = mode == 2;

        //i stg i keep having to add filters to this..
        List<ShopItem> shopItems = Core.GetShopItems(map, shopID)
                                .GroupBy(item => new { item.Name, item.ID })
                                .Select(group =>
                                {
                                    var orderedGroup = group.OrderBy(item => item.ShopItemID != group.First().ShopItemID);
                                    return Group == "First" ? orderedGroup.First() : orderedGroup.Last();
                                })
                                .Where(x => !x.Name.ToLower().EndsWith("insignia"))
                                .ToList();

        List<ShopItem> items = new();
        bool memSkipped = false;

        foreach (ShopItem item in shopItems)
        {
            if (Core.CheckInventory(item.ID, toInv: false) ||
                    miscCatagories.Contains(item.Category) ||
                    (!string.IsNullOrEmpty(buyOnlyThis) && buyOnlyThis != item.Name) ||
                    (itemBlackList != null && itemBlackList.Any(x => x.ToLower() == item.Name.ToLower())))
                continue;

            if (Core.IsMember || !item.Upgrade)
            {
                if (mode == 3)
                {
                    if (Bot.Config!.Get<bool>("Select", $"{item.ID}"))
                        items.Add(item);
                }
                else if (mode != 1)
                    items.Add(item);

                else if (item.Coins)
                    items.Add(item);
            }
            else if (mode == 3 && Bot.Config!.Get<bool>("Select", $"{item.ID}"))
            {
                Core.Logger($"\"{item.Name}\" will be skipped, as you aren't member.");
                memSkipped = true;
            }
        }

        if (items.Count == 0)
        {
            if (buyOnlyThis != null)
                return;

            switch (mode)
            {
                case 0: // all
                case 2: // mergeMats
                    Core.Logger("The bot fetched 0 items to farm. Something must have gone wrong.");
                    return;
                case 1: // acOnly
                    if (shopItems.All(x => !x.Coins))
                        Core.Logger("The bot fetched 0 items to farm. This is because none of the items in this shop are AC tagged.");
                    else Core.Logger("The bot fetched 0 items to farm. Something must have gone wrong.");
                    return;
                case 3: // select
                    if (memSkipped)
                        Core.Logger("The bot fetched 0 items to farm. This is because you aren't member.");
                    else Core.Logger("The bot fetched 0 items to farm. Something must have gone wrong.");
                    return;
            }
        }

        int t = 1;
        for (int i = 0; i < 2; i++)
        {
            foreach (ShopItem item in items)
            {
                if (!matsOnly)
                    Core.Logger($"Farming to buy {item.Name} (#{t}/{items.Count})");

                getIngredients(item, 1);

                if (!matsOnly && !Core.CheckInventory(item.ID, toInv: false))
                {
                    Core.Logger($"Buying {item.Name} (#{t++}/{items.Count})");
                    BuyItem(map, shopID, item.ID, shopItemID: item.ShopItemID, Log: Log);

                    if (item.Coins)
                        Core.ToBank(item.ID);
                    else Core.Logger($"{item} could not be banked");
                }
            }
            if (!matsOnly)
                i++;
        }

        void getIngredients(ShopItem item, int craftingQ)
        {
            if (Core.CheckInventory(item.ID, craftingQ) || item.Requirements == null)
                return;

            Core.FarmingLogger(item.Name, craftingQ);

            foreach (ItemBase req in item.Requirements)
            {
                if (matsOnly)
                {
                    if (Bot.Inventory.IsMaxStack(req.ID))
                        externalQuant = req.MaxStack;
                    else
                    {
                        if (req.Temp)
                            externalQuant = Bot.TempInv.GetQuantity(req.ID) + req.Quantity;
                        else externalQuant = Bot.Inventory.GetQuantity(req.ID) + req.Quantity;
                    }
                }
                else if (MaxStackOneItems.Contains(req.Name))
                    externalQuant = 1;
                else
                    externalQuant = req.Quantity * (craftingQ - Bot.Inventory.GetQuantity(item.ID));

                if (Core.CheckInventory(req.Name, externalQuant) && (!matsOnly || req.MaxStack == 1))
                    continue;

                if (shopItems.Select(x => x.ID).Contains(req.ID) && !AltFarmItems.Contains(req.Name))
                {
                    // ShopItem selectedItem = shopItems.First(x => x.ID == req.ID && !req.Name.EndsWith("Insignia"));
                    ShopItem selectedItem = shopItems
                    .Where(x => x.ID == req.ID && !x.Name.EndsWith("Insignia"))
                    // Add more filtering conditions as needed
                    .First();


                    if (selectedItem.Requirements.Any(r => MaxStackOneItems.Contains(r.Name)))
                    {
                        while (!Core.CheckInventory(selectedItem.ID, req.Quantity))
                        {
                            getIngredients(selectedItem, req.Quantity);
                            Core.Sleep();

                            if (!matsOnly)
                                BuyItem(map, shopID, selectedItem.ID, Bot.Inventory.GetQuantity(selectedItem.ID) + selectedItem.Quantity, shopItemID: selectedItem.ShopItemID, Log: Log);
                            else break;
                        }
                    }
                    else
                    {
                        getIngredients(selectedItem, req.Quantity);
                        Core.Sleep();

                        if (!matsOnly)
                            BuyItem(map, shopID, selectedItem.ID, req.Quantity, shopItemID: selectedItem.ShopItemID, Log: Log);
                    }
                }
                else
                {
                    Core.AddDrop(req.Name);
                    externalItem = req;

                    findIngredients();
                }
            }
        }
    }
    public List<ItemCategory> miscCatagories = new() { ItemCategory.Note, ItemCategory.Item, ItemCategory.QuestItem, ItemCategory.ServerUse };
    public ItemBase externalItem = new();
    public int externalQuant = 0;
    public bool matsOnly = false;
    public List<string> MaxStackOneItems = new();
    public List<string> AltFarmItems = new();

    /// <summary>
    /// The list of ScriptOptions for any merge script.
    /// </summary>
    public List<IOption> MergeOptions = new()
    {
        CoreBots.Instance.SkipOptions,
        new Option<mergeOptionsEnum>("mode", "Select the mode to use", "Regardless of the mode you pick, the bot wont (attempt to) buy Legend-only items if you're not a Legend.\n" +
                                                                     "Select the Mode Explanation item to get more information", mergeOptionsEnum.all),
        new Option<string>(" ", "Mode Explanation [all]", "Mode [all]: \t\tYou get all the items from shop, even if non-AC ones if any.", "click here"),
        new Option<string>(" ", "Mode Explanation [acOnly]", "Mode [acOnly]: \tYou get all the AC tagged items from the shop.", "click here"),
        new Option<string>(" ", "Mode Explanation [mergeMats]", "Mode [mergeMats]: \tYou dont buy any items but instead get the materials to buy them yourself, this way you can choose.", "click here"),
        new Option<string>(" ", "Mode Explanation [select]", "Mode [select]: \tYou are able to select what items you get and which ones you dont in the Select Category below.", "click here"),
    };

    /// <summary>
    /// The name of ScriptOptions for any merge script.
    /// </summary>
    public string OptionsStorage = "MergeOptionStorage";
    #endregion

    #region Kill
#nullable enable

    /// <summary>
    /// Joins a map, jumps to a specified cell and pad, sets the spawn point, and kills the specified monster using the best available race gear.
    /// </summary>
    /// <param name="map">The map to join.</param>
    /// <param name="cell">The cell to jump to.</param>
    /// <param name="pad">The pad to jump to.</param>
    /// <param name="monster">The name of the monster to kill.</param>
    /// <param name="item">The item to kill the monster for. If null or empty, will just kill the monster once.</param>
    /// <param name="quant">The desired quantity of the item to collect.</param>
    /// <param name="isTemp">Whether the item is temporary.</param>
    /// <param name="log">Whether to log the killing of the monster.</param>
    /// <param name="publicRoom">Whether the action should take place in a public room.</param>
    public void BoostKillMonster(string map, string cell, string pad, string monster, string item = "", int quant = 1, bool isTemp = true, bool log = true, bool publicRoom = false)
    {
        if (item != "" && Core.CheckInventory(item, quant))
            return;

        Core.Join(map, cell, pad, publicRoom: publicRoom);

        // _RaceGear(monster);
        Core.KillMonster(map, cell, pad, monster, item, quant, isTemp, log, publicRoom);

        GearStore(true);
    }

    /// <summary>
    /// Kills a monster using it's ID, with the specified monsters the best available race gear
    /// </summary>
    /// <param name="map">Map to join</param>
    /// <param name="cell">Cell to jump to</param>
    /// <param name="pad">Pad to jump to</param>
    /// <param name="monsterID">ID of the monster</param>
    /// <param name="item">Item to kill the monster for, if null will just kill the monster 1 time</param>
    /// <param name="quant">Desired quantity of the item</param>
    /// <param name="isTemp">Whether the item is temporary</param>
    /// <param name="log">Whether it will log that it is killing the monster</param>
    /// <param name="publicRoom"></param>
    public void BoostKillMonster(string map, string cell, string pad, int monsterID, string item = "", int quant = 1, bool isTemp = true, bool log = true, bool publicRoom = false)
    {
        if (item != "" && Core.CheckInventory(item, quant))
            return;

        Core.Join(map, cell, pad, publicRoom: publicRoom);

        // _RaceGear(monsterID);

        Core.KillMonster(map, cell, pad, monsterID, item, quant, isTemp, log, publicRoom);

        GearStore(true);
    }

    /// <summary>
    /// Joins a map, hunts for the monster, and kills the specified monster using the best available race gear.
    /// </summary>
    /// <param name="map">The map to join.</param>
    /// <param name="monster">The name of the monster to hunt and kill.</param>
    /// <param name="item">The item to hunt the monster for. If null, it will just hunt and kill the monster once.</param>
    /// <param name="quant">The desired quantity of the item to collect.</param>
    /// <param name="isTemp">Whether the item is temporary.</param>
    /// <param name="log">Whether to log the hunting and killing of the monster.</param>
    /// <param name="publicRoom">Whether the action should take place in a public room.</param>
    public void BoostHuntMonster(string map, string monster, string? item = null, int quant = 1, bool isTemp = true, bool log = true, bool publicRoom = false)
    {
        if (item != null && Core.CheckInventory(item, quant))
            return;

        Core.Join(map, publicRoom: publicRoom);

        // _RaceGear(monster);

        Core.HuntMonster(map, monster, item, quant, isTemp, log, publicRoom);

        GearStore(true);
    }

    /// <summary>
    /// Joins a map, jumps to a specified cell and pad, sets the spawn point, and kills the specified monster using the best available race gear. Additionally, it listens for counter-attacks.
    /// </summary>
    /// <param name="map">The map to join.</param>
    /// <param name="cell">The cell to jump to.</param>
    /// <param name="pad">The pad to jump to.</param>
    /// <param name="monster">The name of the monster to kill.</param>
    /// <param name="item">The item to kill the monster for. If null, it will just kill the monster once.</param>
    /// <param name="quant">The desired quantity of the item to collect.</param>
    /// <param name="isTemp">Whether the item is temporary.</param>
    /// <param name="log">Whether to log the killing of the monster.</param>
    /// <param name="publicRoom">Whether the action should take place in a public room.</param>
    /// <param name="forAuto">Whether the method is used for an automated process.</param>
    public void KillUltra(string map, string cell, string pad, string monster, string? item = null, int quant = 1, bool isTemp = true, bool log = true, bool publicRoom = true, bool forAuto = false)
    {
        if (item != null && Core.CheckInventory(item, quant))
            return;
        if (item != null && !isTemp)
            Core.AddDrop(item);

        Core.Join(map, cell, pad, publicRoom: publicRoom);
        // if (!forAuto)
        //     _RaceGear(monster);
        Core.Jump(cell, pad);

        if (item == null)
        {
            if (log)
                Core.Logger($"Killing Ultra-Boss {monster}");
            bool ded = false;
            Bot.Events.MonsterKilled += b => ded = true;
            while (!Bot.ShouldExit && !ded)
            {
                Core.Jump(cell, pad);
                if (!Bot.Combat.StopAttacking)
                    Bot.Combat.Attack(monster);
                Core.Sleep();
            }
            Core.Rest();
            return;
        }

        if (log)
            Core.Logger($"Killing Ultra-Boss {monster} for {item} ({quant}) [Temp = {isTemp}]");

        Bot.Hunt.ForItem(monster, item, quant, isTemp);

        if (!forAuto)
            GearStore(true);
    }

    #endregion

    #region Gear

    /// <summary>
    /// Ranks up your class
    /// </summary>
    /// <param name="className"></param>
    /// <param name="gearRestore"></param>
    public void RankUpClass(string className, bool gearRestore = true)
    {
        Bot.Wait.ForTrue(() => Bot.Inventory.Contains(className), 20);

        InventoryItem? itemInv = Bot.Inventory.Items.Find(i => i.Name.Equals(className, StringComparison.Ordinal) && i.Category == ItemCategory.Class);

        if (itemInv == null)
        {
            Core.Logger($"Can't level up \"{className}\" because you don't own it.");
            return;
        }

        if (itemInv.Quantity == 302500)
        {
            Core.Logger($"\"{className}\" is already Rank 10");
        }
        else if (itemInv.Name.Equals("Hobo Highlord") || itemInv.Name.Equals("No Class") || itemInv.Name.Equals("Obsidian No Class"))
        {
            Core.Logger($"\"{itemInv.Name}\" cannot be leveled past Rank 1");
        }
        else
        {
            if (gearRestore)
                GearStore();

            Core.JumpWait();

            SmartEnhance(itemInv.Name);
            var classItem = Bot.Inventory.Items.Find(i => i.Name.Equals(itemInv.Name, StringComparison.Ordinal) && i.Category == ItemCategory.Class);
            if (classItem?.EnhancementLevel == 0)
            {
                Core.Logger($"Can't level up \"{itemInv.Name}\" because it's not enhanced, and AutoEnhance is turned off");
            }
            else
            {
                if (classItem != null && !Bot.Inventory.IsEquipped(classItem.Name))
                {
                    Core.Equip(classItem.Name);
                    Bot.Wait.ForTrue(() => Bot.Inventory.IsEquipped(classItem.Name), 20);
                }
                // string cpBoost = BestGear(GenericGearBoost.cp, false);
                // EnhanceItem(cpBoost, CurrentClassEnh(), CurrentCapeSpecial(), CurrentHelmSpecial(), CurrentWeaponSpecial());
                // Core.Equip(cpBoost);
                Farm.ToggleBoost(BoostType.Class);
                Farm.IcestormArena(Bot.Player.Level, true);
                Core.Logger($"\"{itemInv.Name}\" is now Rank 10");

                Farm.ToggleBoost(BoostType.Class, false);
                if (gearRestore)
                    GearStore(true);
            }
        }
    }

    // Temp here cuz name change is fucky on auto update for some reason
    // no longer used.
    // public void rankUpClass(string ClassName, bool GearRestore = true) => RankUpClass(ClassName, GearRestore);

    #region BestGear
    // /// <summary>
    // /// Do not use this variant
    // /// </summary>
    // private void BestGear()
    // {
    //     // Just here so I can read the other things I had planned

    //     //foreach (string Item in ArrayOutput)
    //     //{
    //     //    InventoryItem invItem = BankInvData.First(x => x.Name == Item);
    //     //    if (!invItem.Equipped)
    //     //        continue;

    //     //    if (invItem.ItemGroup == "Weapon")
    //     //    {
    //     //        List<InventoryItem> theList = new();
    //     //        theList.AddRange(Bot.Inventory.Items.Where(x => x.Name != Item && x.ItemGroup == "Weapon" && x.EnhancementLevel > 0 && Core.IsMember ? true : !x.Upgrade));
    //     //        if (theList.Count == 0)
    //     //            theList.AddRange(Bot.Bank.Items.Where(x => x.Name != Item && x.ItemGroup == "Weapon" && x.EnhancementLevel > 0 && Core.IsMember ? true : !x.Upgrade));

    //     //        if (theList.Count != 0)
    //     //            Core.Equip(theList.First().Name);
    //     //        else
    //     //        {
    //     //            Core.BuyItem(Bot.Map.Name, 299, "Battle Oracle Battlestaff");
    //     //            Core.Equip("Battle Oracle Battlestaff");
    //     //        }
    //     //    }
    //     //    else
    //     //    {
    //     //        Core.JumpWait();
    //     //        Bot.Send.Packet($"%xt%zm%unequipItem%{Bot.Map.RoomID}%{invItem.ID}%");
    //     //    }
    //     //}

    //     //List<BestGearData> BestBestGearData = BestGearData.Where(x => x.BoostValue == TotalBoostValue).ToList();
    //     //if (BestBestGearData.Count() > 1)
    //     //{
    //     //    if (BestBestGearData.Any(x => BankInvData.Where(i => i.Equipped).Select(x => x.Name).Contains(x.iRace)
    //     //     || BestBestGearData.Any(x => BankInvData.Where(i => i.Equipped).Select(x => x.Name).Contains(x.iDMGall))))
    //     //        ArrayOutput = new[] { BestBestGearData.First(x => BankInvData.Where(i => i.Equipped).Select(x => x.Name).Contains(x.Key)).Key };
    //     //    foreach (BestGearData Gear in BestGearData.Where(x => x.BoostValue == TotalBoostValue))
    //     //    {
    //     //        InventoryItem Item = BankInvData.First(x => x.Name == Gear.iRace || x.Name == Gear.iDMGall);
    //     //        InventoryItem equippedWeapon = BankInvData.First(x => x.Equipped == true && x.ItemGroup == "Weapon");
    //     //        if (Item != null && equippedWeapon != null
    //     //            && Bot.Flash.GetGameObject<int>($"world.invTree.{Item.ID}.EnhID") == Bot.Flash.GetGameObject<int>($"world.invTree.{equippedWeapon.ID}.EnhID"))
    //     //        {
    //     //            ArrayOutput = new[] { Item.Name };
    //     //            break;
    //     //        }
    //     //    }
    //     //}

    //     //foreach (string Item in ArrayOutput)
    //     //{
    //     //    InventoryItem invItem = BankInvData.First(x => x.Name == Item);
    //     //    if (!invItem.Equipped)
    //     //        continue;

    //     //    if (invItem.ItemGroup == "Weapon")
    //     //    {
    //     //        List<InventoryItem> theList = new();
    //     //        theList.AddRange(Bot.Inventory.Items.Where(x => x.Name != Item && x.ItemGroup == "Weapon" && x.EnhancementLevel > 0 && Core.IsMember ? true : !x.Upgrade));
    //     //        if (theList.Count == 0)
    //     //            theList.AddRange(Bot.Bank.Items.Where(x => x.Name != Item && x.ItemGroup == "Weapon" && x.EnhancementLevel > 0 && Core.IsMember ? true : !x.Upgrade));

    //     //        if (theList.Count != 0)
    //     //            Core.Equip(theList.First().Name);
    //     //        else
    //     //        {
    //     //            Core.BuyItem(Bot.Map.Name, 299, "Battle Oracle Battlestaff");
    //     //            Core.Equip("Battle Oracle Battlestaff");
    //     //        }
    //     //    }
    //     //    else
    //     //    {
    //     //        Core.JumpWait();
    //     //        Bot.Send.Packet($"%xt%zm%unequipItem%{Bot.Map.RoomID}%{invItem.ID}%");
    //     //    }
    //     //}
    // }

    // /// <summary>
    // /// Equips the best gear available in a player's inventory/bank by checking what item has the highest boost value of the given type. Also works with damage stacking for monsters with a Race
    // /// </summary>
    // /// <param name="BoostType">Type "GenericGearBoost." and then the boost of your choice in order to determine and equip the best available boosting gear</param>
    // /// <param name="EquipItem">To Equip the found item(s) or not</param>
    // public string BestGear(GenericGearBoost boostType, bool equipItem = true)
    // {
    //         try
    //         {
    //             // If CBO settings disable bestgear, dont do anything
    //             if (Core.CBOBool("DisableBestGear", out bool _DisableBestGear) && _DisableBestGear)
    //                 return String.Empty;

    //             // If this bestgear prompt is the same as the last, return the previous value
    //             if (LastGenericBoostType == boostType)
    //                 return LastGenericBestGear ?? String.Empty;

    //             string boostString = boostType.ToString();
    //             Core.Logger($"Searching for the best available gear for {boostString}");

    //             IEnumerable<InventoryItem> GearWithChosenBoost =
    //                 Bot.Inventory.Items
    //                     .Concat(Bot.Bank.Items)
    //                     .Where(item => !String.IsNullOrEmpty(item.Meta) && item.Meta.Contains(boostString));
    //             InventoryItem? toReturn = null;

    //             // Cant find anything if the list is empty
    //             if (!GearWithChosenBoost.Any())
    //             {
    //                 Core.Logger($"Best gear for {boostString} wasn't found!");
    //                 return String.Empty;
    //             }

    //             IEnumerable<(InventoryItem, float)> bestGearData = GearWithChosenBoost.Select(item => (item, _getBoostFloat(item, boostString)));
    //             //bestGearData.ForEach(b => Core.DebugLogger(this, $"{b.Item1.Name} = {b.Item2}"));
    //             float BestBoostValue = bestGearData.Select(x => x.Item2).Max();
    //             IEnumerable<InventoryItem> bestItems = bestGearData.Where(x => x.Item2 == BestBoostValue).Select(x => x.Item1);

    //             // Prioritize an item that is already equipped
    //             IEnumerable<InventoryItem> filter = bestItems.Where(x => x.Equipped);
    //             if (filter != null && filter.Any(x => x != null))
    //                 setToReturn(filter);
    //             else
    //             {
    //                 // Prioritize an item that has the same EnhID
    //                 // The int (Item2) is EnhID
    //                 List<(InventoryItem, int)> equippedItems =
    //                     Bot.Inventory.Items
    //                         .Where(x => x.Equipped)
    //                         .Select(x => (x, getEnhID(x)))
    //                         .ToList();
    //                 IEnumerable<(InventoryItem, int)> bestItemsEnh =
    //                     bestItems
    //                         .Select(x => (x, getEnhID(x)));

    //                 filter =
    //                     bestItemsEnh
    //                         .Where(x =>
    //                             x.Item2 == equippedItems.Find(e => e.Item1.ItemGroup == x.Item1.ItemGroup).Item2)
    //                         .Select(b => b.Item1);
    //                 // Should always return true if its two pets or armors or ground runes

    //                 if (filter != null && filter.Any(x => x != null && x.ID > 0))
    //                     setToReturn(filter);
    //                 else
    //                 {
    //                     // If none of the enhancement IDs match, prioritize items that are enhanced in general
    //                     filter = bestItemsEnh.Where(x => x.Item2 != 0).Select(b => b.Item1);
    //                     if (filter != null && filter.Any(x => x != null))
    //                         setToReturn(filter);
    //                     // If no items are enhanced, just pick the item (based on category ofc)
    //                     else setToReturn(bestItems);
    //                 }
    //             }

    //             if (toReturn == null)
    //             {
    //                 // This should be impossible to reach, but is a good savety precaughtion
    //                 Core.Logger($"Best gear for {boostString} wasn't found!");
    //                 return String.Empty;
    //             }
    //             else Core.Logger($"Best gear for {boostString} found: {toReturn.Name} ({BestBoostValue - 1:+0.##%})");

    //             LastGenericBestGear = toReturn.Name;
    //             if (equipItem)
    //             {
    //                 // If the item is not enhanced and it can be and should be done so before it's equipped
    //                 if (toReturn.EnhancementLevel == 0 && EnhanceableCatagories.Contains(toReturn.Category))
    //                 {
    //                     if (!Core.CBOBool("DisableAutoEnhance", out bool _disableAutoEnhance) || !_disableAutoEnhance)
    //                     {
    //                         InventoryItem? equippedItem = Bot.Inventory.Items.Find(x => x.Equipped && x.ItemGroup == toReturn.ItemGroup);
    //                         EnhancementType type = EnhancementType.Lucky;
    //                         CapeSpecial cape = CapeSpecial.None;
    //                         HelmSpecial helm = HelmSpecial.None;
    //                         WeaponSpecial weapon = WeaponSpecial.None;

    //                         if (equippedItem != null)
    //                         {
    //                             switch (toReturn.Category)
    //                             {
    //                                 case ItemCategory.Cape:
    //                                     if (equippedItem.EnhancementPatternID <= 10 && !IsEnhancedWithBaseForge(equippedItem)) // If its not a forge enhancement
    //                                         type = (EnhancementType)equippedItem.EnhancementPatternID;
    //                                     else cape = (CapeSpecial)equippedItem.EnhancementPatternID;
    //                                     break;
    //                                 case ItemCategory.Helm:
    //                                     if (equippedItem.EnhancementPatternID <= 25 && !IsEnhancedWithBaseForge(equippedItem)) // If its not a forge enhancement
    //                                         type = (EnhancementType)equippedItem.EnhancementPatternID;
    //                                     else helm = (HelmSpecial)equippedItem.EnhancementPatternID;
    //                                     break;
    //                                 case ItemCategory.Class:
    //                                     type = (EnhancementType)equippedItem.EnhancementPatternID;
    //                                     break;
    //                                 default: // Weapon
    //                                     if (equippedItem.EnhancementPatternID <= 6 && !IsEnhancedWithBaseForge(equippedItem)) // If its not a forge enhancement
    //                                         type = (EnhancementType)equippedItem.EnhancementPatternID;
    //                                     weapon = (WeaponSpecial)getProcID(equippedItem);
    //                                     break;
    //                             }
    //                         }
    //                         EnhanceItem(toReturn.Name, type, cape, helm, weapon);
    //                     }
    //                     else
    //                     {
    //                         Core.Logger("Equipping Failed: BestGear tried to equip an unenhanced item and AutoEnhance is disabled.");
    //                     }
    //                 }
    //                 else Core.Equip(LastGenericBestGear);
    //             }

    //             LastGenericBoostType = boostType;
    //             return LastGenericBestGear;

    //             void setToReturn(IEnumerable<InventoryItem> combos)
    //             {
    //                 toReturn =
    //                     combos.OrderBy(c => getPriority(bestGearData.First(r => r.Item1.ID == c.ID).Item1))
    //                     .First();
    //             }
    //         }
    //         catch (Exception e)
    //         {
    //             AdvCrash(e);
    //             return String.Empty;
    //         }
    // }

    // /// <summary>
    // /// Equips the best gear available in a player's inventory/bank by checking what item has the highest boost value of the given type. Also works with damage stacking for monsters with a Race
    // /// </summary>
    // /// <param name="BoostType">Type "RacialGearBoost." and then the boost of your choice in order to determine and equip the best available boosting gear</param>
    // /// <param name="EquipItem">To Equip the found item(s) or not</param>
    // public string[] BestGear(RacialGearBoost boostType, bool equipItem = true)
    // {
    //         try
    //         {
    //             // If CBO settings disable bestgear, dont do anything
    //             if (Core.CBOBool("DisableBestGear", out bool _DisableBestGear) && _DisableBestGear)
    //                 return Array.Empty<string>();

    //             // If this bestgear prompt is the same as the last, return the previous value
    //             if (LastRacialBoostType == boostType)
    //                 return LastRacialBestGear ?? Array.Empty<string>();
    //             // If the enemy has no race, focus dmgAll

    //             string boostString = boostType == RacialGearBoost.None ? "Untagged" : boostType.ToString();
    //             Core.Logger($"Searching for the best available gear against {boostString}");

    //             IEnumerable<InventoryItem> GearWithMeta = Bot.Inventory.Items.Concat(Bot.Bank.Items).Where(item => !String.IsNullOrEmpty(item.Meta));
    //             IEnumerable<InventoryItem> GearWithChosenBoost = GearWithMeta.Where(item => item.Meta.Contains(boostString));
    //             List<InventoryItem> toReturn = new();

    //             // Fetch all dmg all items
    //             IEnumerable<(InventoryItem, float)> damageAllItems =
    //                 GearWithMeta
    //                     .Where(item => item.Meta.Contains("dmgAll"))
    //                     .Select(item => (item, _getBoostFloat(item, "dmgAll")));
    //             IEnumerable<InventoryItem> relevantItems = GearWithChosenBoost.Concat(damageAllItems.Select(x => x.Item1));
    //             List<RacialBestGearData> bestGearData = new();

    //             // If the player has damage all items (should also work if empty)
    //             bestGearData.AddRange(
    //                 damageAllItems.Select(dmgTulpe =>
    //                     new RacialBestGearData(dmgTulpe.Item1, dmgTulpe.Item2)));

    //             // If the player has racial boosting items
    //             if (GearWithChosenBoost.Any())
    //             {
    //                 foreach (InventoryItem racialItem in GearWithChosenBoost)
    //                 {
    //                     float racialBoost = _getBoostFloat(racialItem, boostString);
    //                     // Add the racial item standalone
    //                     bestGearData.Add(new(racialItem, racialBoost));

    //                     // Add the racial items in combination with the 
    //                     bestGearData.AddRange(
    //                         damageAllItems
    //                             .Where(dmgTulpe => dmgTulpe.Item1.ItemGroup != racialItem.ItemGroup)
    //                             .Select(dmgTulpe =>
    //                                 new RacialBestGearData(racialItem, dmgTulpe.Item1, dmgTulpe.Item2 * racialBoost)));
    //                 }
    //             }

    //             // This triggers if there are no racial boost items, but also no damage all items
    //             if (!bestGearData.Any())
    //             {
    //                 Core.Logger($"Best gear against {boostString} wasn't found!");
    //                 return Array.Empty<string>();
    //             }

    //             //bestGearData.ForEach(b => Core.DebugLogger(this, $"{b.Item1.Name} + {b.Item2?.Name ?? "NULL"} = {b.BoostValue}"));

    //             float BestBoostValue = bestGearData.Select(x => x.BoostValue).Max();
    //             IEnumerable<RacialBestGearData> bestCombos = bestGearData.Where(x => x.BoostValue == BestBoostValue);

    //             // Prioritize a combination where both items of any of the optimal set are already equipped
    //             IEnumerable<RacialBestGearData> filter = bestCombos.Where(x => x.Item1.Equipped && (x.Item2 == null || x.Item2.Equipped));
    //             if (filter != null && filter.Any(x => x != null))
    //                 setToReturn(filter);
    //             else
    //             {
    //                 // Prioritize a combination where one items in the optimal sets is equipped
    //                 filter = bestCombos.Where(x => x.Item1.Equipped || (x.Item2 != null && x.Item2.Equipped));
    //                 if (filter != null && filter.Any(x => x != null))
    //                     setToReturn(filter);
    //                 // If it gets here, that means none of the optimal items are equipped
    //                 // Supporting enhancement consideration for racial items is overly complex and unneccessary
    //                 else setToReturn(bestCombos);
    //             }

    //             switch (toReturn.Count)
    //             {
    //                 // This might already be handled and could be unneccessary
    //                 //case 0:
    //                 //    Core.Logger($"Best gear against {boostString} wasn't found!");
    //                 //    return Array.Empty<string>();
    //                 case 1:
    //                     Core.Logger($"Best gear against {boostString} found: {toReturn[0].Name} ({BestBoostValue - 1:+0.##%})");
    //                     break;
    //                 case 2:
    //                     Core.Logger($"Best gear against {boostString} found: {toReturn[0].Name} + {toReturn[1].Name} ({BestBoostValue - 1:+0.##%})");
    //                     break;
    //                 default:
    //                     Core.Logger($"How the fuck did toReturn.Count get {toReturn.Count}. Please report");
    //                     break;
    //             }

    //             LastRacialBestGear = toReturn.Select(i => i.Name).ToArray();
    //             if (equipItem)
    //                 Core.Equip(LastRacialBestGear);

    //             LastRacialBoostType = boostType;
    //             return LastRacialBestGear;

    //             void setToReturn(IEnumerable<RacialBestGearData> combos)
    //             {
    //                 RacialBestGearData combo =
    //                     combos.OrderBy(c =>
    //                         c.Item2 == null ?
    //                             getPriority(
    //                                 relevantItems.First(r => r.ID == c.Item1.ID)) :
    //                             getPriority(
    //                                 relevantItems.First(r => r.ID == c.Item1.ID),
    //                                 relevantItems.First(r => r.ID == c.Item2.ID)))
    //                     .First();

    //                 if (combo.Item2 == null)
    //                     toReturn = new() { combo.Item1 };
    //                 else toReturn = new() { combo.Item1, combo.Item2 };
    //             }
    //         }
    //         catch (Exception e)
    //         {
    //             AdvCrash(e);
    //             return Array.Empty<string>();
    //         }
    // }

    #endregion BestGear

    // int getPriority(params InventoryItem[] items)
    // {
    //     int toReturn = 0;
    //     foreach (ItemCategory c in items.Select(i => i.Category))
    //     {
    //         switch (c)
    //         {
    //             case ItemCategory.Misc: // Ground runes
    //                 break; // Value is 0
    //             case ItemCategory.Helm:
    //                 toReturn += 1;
    //                 break;
    //             case ItemCategory.Pet:
    //                 toReturn += 2;
    //                 break;
    //             case ItemCategory.Cape:
    //                 toReturn += 4;
    //                 break;
    //             case ItemCategory.Armor:
    //                 toReturn += 8;
    //                 break;
    //             default: // Weapons
    //                 toReturn += 16;
    //                 break;
    //         }
    //     }
    //     return toReturn;
    // }
    // private readonly GenericGearBoost? LastGenericBoostType = null;
    // private readonly RacialGearBoost? LastRacialBoostType = null;
    // private readonly string? LastGenericBestGear = null;
    // private readonly string[]? LastRacialBestGear = null;
    // private class RacialBestGearData
    // {
    //     public InventoryItem Item1 { get; set; }
    //     public InventoryItem? Item2 { get; set; }
    //     public float BoostValue { get; set; }
    //     public RacialBestGearData(InventoryItem item1, InventoryItem item2, float boostValue)
    //     {
    //         Item1 = item1;
    //         Item2 = item2;
    //         BoostValue = boostValue;
    //     }
    //     public RacialBestGearData(InventoryItem item1, float boostValue)
    //     {
    //         Item1 = item1;
    //         BoostValue = boostValue;
    //     }
    // }

    /// <summary>
    /// Stores the gear a player has so that it can later restore these
    /// </summary>
    /// <param name="Restore">Set true to restore previously stored gear</param>
    /// <param name="EnhAfter"></param>
    public void GearStore(bool Restore = false, bool EnhAfter = false)
    {
        if (!Restore)
        {
            foreach (InventoryItem Item in Bot.Inventory.Items.FindAll(i => i.Equipped == true))
                ReEquippedItems.Add(Item.Name);

            ReEnhanceAfter = CurrentClassEnh();
            if (Bot.Inventory.Items.Any(x => x.Category == ItemCategory.Cape && x.Equipped))
                ReCEnhanceAfter = CurrentCapeSpecial();
            if (Bot.Inventory.Items.Any(x => x.Category == ItemCategory.Helm && x.Equipped))
                ReHEnhanceAfter = CurrentHelmSpecial();
            ReWEnhanceAfter = CurrentWeaponSpecial();
        }
        else if (ReEquippedItems.Count > 0)
        {
            Core.JumpWait();
            Core.Equip(ReEquippedItems.ToArray());
            if (EnhAfter)
                EnhanceEquipped(ReEnhanceAfter, ReCEnhanceAfter, ReHEnhanceAfter, ReWEnhanceAfter);
        }
    }
    private readonly List<string> ReEquippedItems = new();
    private EnhancementType ReEnhanceAfter = EnhancementType.Lucky;
    private CapeSpecial ReCEnhanceAfter = CapeSpecial.None;
    private HelmSpecial ReHEnhanceAfter = HelmSpecial.None;
    private WeaponSpecial ReWEnhanceAfter = WeaponSpecial.None;

    /// <summary>
    /// Find out if an item is a weapon or not
    /// </summary>
    /// <param name="Item">The ItemBase object of the item</param>
    /// <returns>Returns if its a weapon or not</returns>
    public bool isWeapon(ItemBase Item) => Item.ItemGroup == "Weapon";

    /// <summary>
    /// Will do GearStore() and then figure out the race of the monster paramater and equip bestGear on it
    /// </summary>
    /// <param name="Monster">The Monster object of the monster</param>
    public void _RaceGear(string Monster)
    {
        if (!Bot.Monsters.MapMonsters.Any(x => x.Name.ToLower() == Monster.ToLower()))
        {
            Core.Logger("Could not find any monster with the name " + Monster);
            return;
        }
        GearStore();
        string Map = Bot.Map.LastMap;
        string MonsterRace = "";
        if (Monster != "*")
            MonsterRace = Bot.Monsters.MapMonsters.First(x => x.Name.ToLower() == Monster.ToLower())?.Race ?? "";
        else
        {
            if (Bot.Monsters.CurrentMonsters.Count == 0)
            {
                Core.Logger($"No monsters are present in cell \"{Bot.Player.Cell}\" in /{Bot.Map.Name}");
                return;
            }
            MonsterRace = Bot.Monsters.CurrentMonsters.First().Race ?? "";
        }

        if (MonsterRace == null || MonsterRace == "")
            return;

        // string[] _BestGear = BestGear((RacialGearBoost)Enum.Parse(typeof(RacialGearBoost), MonsterRace), false);
        // if (_BestGear.Length == 0)
        //     return;
        // EnhanceItem(_BestGear, CurrentClassEnh(), CurrentCapeSpecial(), CurrentHelmSpecial(), CurrentWeaponSpecial());
        // Core.Equip(_BestGear);
        Core.Logger("BestGear Disabled");

        //EnhanceEquipped(CurrentClassEnh(), CurrentCapeSpecial(), CurrentHelmSpecial(), CurrentWeaponSpecial());
        Core.Join(Map);
    }

    /// <summary>
    /// Will do GearStore() and then figure out the race of the monster paramater and equip bestGear on it
    /// </summary>
    /// <param name="MonsterID">The MonsterID of the monster</param>
    public void _RaceGear(int MonsterID)
    {
        GearStore();
        string Map = Bot.Map.LastMap;
        string MonsterRace = Bot.Monsters.MapMonsters.First(x => x.ID == MonsterID).Race;

        if (MonsterRace == null || MonsterRace == "")
            return;

        // string[] _BestGear = BestGear((RacialGearBoost)Enum.Parse(typeof(RacialGearBoost), MonsterRace), false);
        // if (_BestGear.Length == 0)
        //     return;
        // EnhanceItem(_BestGear, CurrentClassEnh(), CurrentCapeSpecial(), CurrentHelmSpecial(), CurrentWeaponSpecial());
        // Core.Equip(_BestGear);

        Core.Logger("BestGear Disabled");
        //EnhanceEquipped(CurrentClassEnh(), CurrentCapeSpecial(), CurrentHelmSpecial(), CurrentWeaponSpecial());
        Core.Join(Map);
    }

    public bool HasMinimalBoost(GenericGearBoost boostType, int percentage)
        => Bot.Inventory.Items.Concat(Bot.Bank.Items).Any(x => Core.GetBoostFloat(x, boostType.ToString()) >= ((percentage / (float)100) + 1));
    public bool HasMinimalBoost(RacialGearBoost boostType, int percentage)
        => Bot.Inventory.Items.Concat(Bot.Bank.Items).Any(x => Core.GetBoostFloat(x, boostType.ToString()) >= ((percentage / (float)100) + 1));

    #endregion

    #region Enhancement

    /// <summary>
    /// Enhances your currently equipped gear
    /// </summary>
    /// <param name="type"></param>
    /// <param name="cSpecial"></param>
    /// <param name="hSpecial"></param>
    /// <param name="wSpecial"></param>
    public void EnhanceEquipped(EnhancementType type, CapeSpecial cSpecial = CapeSpecial.None, HelmSpecial hSpecial = HelmSpecial.None, WeaponSpecial wSpecial = WeaponSpecial.None)
    {
        if (Core.CBOBool("DisableAutoEnhance", out bool _disableAutoEnhance) && _disableAutoEnhance)
            return;

        List<InventoryItem> EquippedItems = Bot.Inventory.Items.FindAll(i => i.Equipped == true && EnhanceableCatagories.Contains(i.Category));
        try
        {
            AutoEnhance(EquippedItems, type, cSpecial, hSpecial, wSpecial);
        }
        catch (Exception e)
        {
            AdvCrash(e);
        }
    }

    /// <summary>
    /// Enhances a selected item
    /// </summary>
    /// <param name="item"></param>
    /// <param name="type"></param>
    /// <param name="cSpecial"></param>
    /// <param name="hSpecial"></param>
    /// <param name="wSpecial"></param>
    public void EnhanceItem(string item, EnhancementType type, CapeSpecial cSpecial = CapeSpecial.None, HelmSpecial hSpecial = HelmSpecial.None, WeaponSpecial wSpecial = WeaponSpecial.None)
    {
        if (string.IsNullOrEmpty(item) || (Core.CBOBool("DisableAutoEnhance", out bool _disableAutoEnhance) && _disableAutoEnhance))
            return;

        if (!Core.CheckInventory(item))
        {
            Core.Logger($"Enhancement Failed: Could not find \"{item}\"");
            return;
        }

        while (!Bot.ShouldExit && Bot.Player.InCombat)
        {
            if (Bot.Player.HasTarget)
                Bot.Combat.CancelTarget();
            Core.JumpWait();
            Core.Sleep();
        }

        InventoryItem? SelectedItem = Bot.Inventory.Items.Find(i => i.Name.ToLower().Trim() == item.ToLower().Trim() && EnhanceableCatagories.Contains(i.Category)); ;
        if (SelectedItem == null)
        {
            // Bot.Log(Bot.Inventory.Items.Find(i => i.Name.ToLower().Trim() == item.ToLower().Trim()));
            // Bot.Log(Bot.Inventory.Items.Find(i => EnhanceableCatagories.Contains(i.Category)));
            if (Bot.Inventory.Items.Any(i => i.Name.ToLower().Trim() == item.ToLower().Trim()))
                Core.Logger($"Enhancement Failed: {item} cannot be enhanced");
            return;
        }

        try
        {
            AutoEnhance(new() { SelectedItem }, type, cSpecial, hSpecial, wSpecial);
        }
        catch (Exception e)
        {
            AdvCrash(e);
        }
    }

    /// <summary>
    /// Enhances multiple selected items
    /// </summary>
    /// <param name="items"></param>
    /// <param name="type"></param>
    /// <param name="cSpecial"></param>
    /// <param name="hSpecial"></param>
    /// <param name="wSpecial"></param>
    public void EnhanceItem(string[] items, EnhancementType type, CapeSpecial cSpecial = CapeSpecial.None, HelmSpecial hSpecial = HelmSpecial.None, WeaponSpecial wSpecial = WeaponSpecial.None)
    {
        if (items.Length == 0 || (Core.CBOBool("DisableAutoEnhance", out bool _disableAutoEnhance) && _disableAutoEnhance))
            return;

        // If any of the items in the items array cant be found, return
        List<string>? notFound = new();
        foreach (string item in items)
            if (!Core.CheckInventory(item))
                notFound.Add(item);

        if (notFound.Count > 0)
        {
            if (notFound.Count == 1)
                Core.Logger($"Enhancement Failed: Could not find {notFound.First()}");
            else Core.Logger($"Enhancement Failed: Could not find the following items: {string.Join(", ", notFound)}");
            return;
        }
        notFound = null;

        // Find all the items in the items array
        List<InventoryItem> SelectedItems = Bot.Inventory.Items.FindAll(i => items.Contains(i.Name) && EnhanceableCatagories.Contains(i.Category));

        // If any of the items in the items array cant be enhanced, return
        if (SelectedItems.Count != items.Length)
        {
            List<string> unEnhanceable = new();

            foreach (string item in items)
                if (!Bot.Inventory.Items.Any(i => i.Name == item && EnhanceableCatagories.Contains(i.Category)))
                    unEnhanceable.Add(item);

            if (unEnhanceable.Count == 1)
                Core.Logger($"Enhancement Failed: Unenhanceable item found, {unEnhanceable.First()}");
            else Core.Logger($"Enhancement Failed: The following items are unenhanceable, {string.Join(", ", unEnhanceable)}");

            return;
        }

        try
        {
            AutoEnhance(SelectedItems, type, cSpecial, hSpecial, wSpecial);
        }
        catch (Exception e)
        {
            AdvCrash(e);
        }
    }

    private static bool IsEnhancedWithBaseForge(InventoryItem item) => item.EnhancementPatternID == 0 && item.EnhancementLevel > 0;

    private void AdvCrash(Exception e, [CallerMemberName] string? caller = null)
    {
        if (e == null || (Bot.ShouldExit && e is OperationCanceledException))
            return;
        List<string> logs = Ioc.Default.GetRequiredService<ILogService>().GetLogs(LogType.Script);
        logs = logs.Skip(logs.Count > 5 ? (logs.Count - 5) : logs.Count).ToList();
        Bot.Handlers.RegisterOnce(1, Bot => Bot.ShowMessageBox($"{caller} has crashed. Please fill in the Skua Bug Report/Request for under the topic: Crashed\n" +
                $"Due to special handling for this type of crash, your script will continue without using {caller} in this instance.\n\n" +
                "---------------------------------------------------" +
                "Last 5 logs:\n\t" +
                logs.Join("\n\t") +
                "\n\n" +
                "---------------------------------------------------" +
                "Crash Log:\n\t" +
                e.Message + "\n" + e.InnerException,
            caller + " crashed"));
    }

    /// <summary>
    /// Determines what Enhancement Type the player has on their currently equipped class
    /// </summary>
    /// <returns>Returns the equipped Enhancement Type</returns>
    public EnhancementType CurrentClassEnh()
    {
        int? EnhPatternID = Bot.Player.CurrentClass?.EnhancementPatternID;
        if (EnhPatternID == 1 || EnhPatternID == 23 || EnhPatternID == null)
            EnhPatternID = 9;
        return (EnhancementType)EnhPatternID;
    }

    /// <summary>
    /// Determines what Cape Special the player has on their currently equipped cape
    /// </summary>
    /// <returns>Returns the equipped Cape Special</returns>
    public CapeSpecial CurrentCapeSpecial()
    {
        InventoryItem? EquippedCape = Bot.Inventory.Items.Find(i => i.Equipped && i.Category == ItemCategory.Cape);
        if (EquippedCape == null)
            return CapeSpecial.None;
        int pattern_id = EquippedCape.EnhancementPatternID;
        if (Enum.IsDefined(typeof(EnhancementType), pattern_id))
            return CapeSpecial.None;
        return (CapeSpecial)pattern_id;
    }

    /// <summary>
    /// Determines what Helm Special the player has on their currently equipped helm
    /// </summary>
    /// <returns>Returns the equipped Helm Special</returns>
    public HelmSpecial CurrentHelmSpecial()
    {
        InventoryItem? EquippedHelm = Bot.Inventory.Items.Find(i => i.Equipped && i.Category == ItemCategory.Helm);
        if (EquippedHelm == null)
            return HelmSpecial.None;
        int pattern_id = EquippedHelm.EnhancementPatternID;
        if (Enum.IsDefined(typeof(EnhancementType), pattern_id))
            return HelmSpecial.None;
        return (HelmSpecial)pattern_id;
    }

    /// <summary>
    /// Determines what Weapon Special the player has on their currently equipped weapon
    /// </summary>
    /// <returns>Returns the equipped Weapon Special</returns>
    public WeaponSpecial CurrentWeaponSpecial()
    {
        InventoryItem? EquippedWeapon = Bot.Inventory.Items.Find(i => i.Equipped && WeaponCatagories.Contains(i.Category));
        if (EquippedWeapon == null)
            return WeaponSpecial.None;
        int pattern_id = getProcID(EquippedWeapon);
        if (Enum.IsDefined(typeof(EnhancementType), pattern_id))
            return WeaponSpecial.None;
        return (WeaponSpecial)pattern_id;
    }

    private static readonly ItemCategory[] EnhanceableCatagories =
    {
        ItemCategory.Sword,
        ItemCategory.Axe,
        ItemCategory.Dagger,
        ItemCategory.Gun,
        ItemCategory.HandGun,
        ItemCategory.Rifle,
        ItemCategory.Bow,
        ItemCategory.Mace,
        ItemCategory.Gauntlet,
        ItemCategory.Polearm,
        ItemCategory.Staff,
        ItemCategory.Wand,
        ItemCategory.Whip,
        ItemCategory.Class,
        ItemCategory.Helm,
        ItemCategory.Cape,

    };

    public readonly ItemCategory[] WeaponCatagories = EnhanceableCatagories[..12];

    private void AutoEnhance(List<InventoryItem> ItemList, EnhancementType type, CapeSpecial cSpecial, HelmSpecial hSpecial, WeaponSpecial wSpecial)
    {
        // In case the 'CurrentEnhancement()' failed and returned 0
        if ((int)type == 0)
            return;

        // Empty check
        if (ItemList.Count == 0)
        {
            Core.Logger("Enhancement Failed:\t\"ItemList\" is empty");
            return;
        }

        // Defining cape
        InventoryItem? cape = null;
        if (cSpecial != CapeSpecial.None && ItemList.Any(i => i.Category == ItemCategory.Cape))
        {
            cape = ItemList.Find(i => i.Category == ItemCategory.Cape);

            // Removing cape from the list because it needs to be enhanced seperately
            if (cape != null)
                ItemList.Remove(cape);
        }

        // Defining helm
        InventoryItem? helm = null;
        if (hSpecial != HelmSpecial.None && ItemList.Any(i => i.Category == ItemCategory.Helm))
        {
            helm = ItemList.Find(i => i.Category == ItemCategory.Helm);

            // Removing helm from the list because it needs to be enhanced seperately
            if (helm != null)
                ItemList.Remove(helm);
        }

        // Defining weapon
        InventoryItem? weapon = null;
        // If Awe-Enhancements aren't unlocked, enhance them with normal enhancements
        if (wSpecial != WeaponSpecial.None && ItemList.Any(i => i.ItemGroup == "Weapon") && (uAwe() || (int)wSpecial > 6))
        {
            weapon = ItemList.Find(i => i.ItemGroup == "Weapon");

            // Removing weapon from the list because it needs to be enhanced seperately
            if (weapon != null)
                ItemList.Remove(weapon);
        }

        int skipCounter = 0;

        // Setting the shop ID for the enhancement type
        if (ItemList.Count > 0)
        {
            int shopID = 0;

            switch (type)
            {
                case EnhancementType.Fighter:
                    shopID = Bot.Player.Level >= 50 ? 768 : 141;
                    break;
                case EnhancementType.Thief:
                    shopID = Bot.Player.Level >= 50 ? 767 : 142;
                    break;
                case EnhancementType.Hybrid:
                    shopID = Bot.Player.Level >= 50 ? 766 : 143;
                    break;
                case EnhancementType.Wizard:
                    shopID = Bot.Player.Level >= 50 ? 765 : 144;
                    break;
                case EnhancementType.Healer:
                    shopID = Bot.Player.Level >= 50 ? 762 : 145;
                    break;
                case EnhancementType.SpellBreaker:
                    shopID = Bot.Player.Level >= 50 ? 764 : 146;
                    break;
                case EnhancementType.Lucky:
                    shopID = Bot.Player.Level >= 50 ? 763 : 147;
                    break;
                default:
                    Core.Logger($"Enhancement Failed:\tInvalid EnhancementType given, received {(int)type} | {type}");
                    return;
            }

            // Enhancing the remaining items
            foreach (InventoryItem item in ItemList)
            {
                _AutoEnhance(item, shopID);
                Core.Sleep();
            }
        }

        // Enhancing the cape with the cape special
        if (cape != null)
        {
            bool canEnhance = true;

            switch (cSpecial)
            {
                case CapeSpecial.Forge:
                    if (!uForgeCape())
                    {
                        Core.Logger("Enhancement Failed:\tYou did not unlock the Forge (Cape) Enhancement yet");
                        canEnhance = false;
                    }
                    break;
                case CapeSpecial.Absolution:
                    if (!uAbsolution())
                        Fail();
                    break;
                case CapeSpecial.Avarice:
                    if (!uAvarice())
                        Fail();
                    break;
                case CapeSpecial.Vainglory:
                    if (!uVainglory())
                        Fail();
                    break;
                case CapeSpecial.Penitence:
                    if (!uPenitence())
                        Fail();
                    break;
                case CapeSpecial.Lament:
                    if (!uLament())
                        Fail();
                    break;
                default:
                    Core.Logger($"Enhancement Failed:\tInvalid \"CapeSpecial\" given, received {(int)cSpecial} | {cSpecial}");
                    return;

                    void Fail()
                    {
                        Core.Logger($"Enhancement Failed:\tYou did not unlock the {cSpecial} Enhancement yet");
                        canEnhance = false;
                    }
            }

            if (canEnhance)
                _AutoEnhance(cape, 2143, ((int)cSpecial > 0) ? "forge" : null);
            else skipCounter++;
        }

        // Enhancing the helm with the helm special
        if (helm != null)
        {
            bool canEnhance = true;

            switch (hSpecial)
            {
                case HelmSpecial.Vim:
                    if (!uVim())
                        Fail();
                    break;
                case HelmSpecial.Examen:
                    if (!uExamen())
                        Fail();
                    break;
                case HelmSpecial.Forge:
                    if (!uForgeHelm())
                        Fail();
                    break;
                case HelmSpecial.Anima:
                    if (!uAnima())
                        Fail();
                    break;
                case HelmSpecial.Pneuma:
                    if (!uPneuma())
                        Fail();
                    break;
                case HelmSpecial.Hearty:
                    if (!uHearty())
                        Fail();
                    break;
                default:
                    Core.Logger($"Enhancement Failed:\tInvalid \"HelmSpecial\" given, received {(int)hSpecial} | {hSpecial}");
                    return;

                    void Fail()
                    {
                        Core.Logger($"Enhancement Failed:\tYou did not unlock the {hSpecial} Enhancement yet");
                        canEnhance = false;
                    }
            }

            if (canEnhance)
                _AutoEnhance(helm, 2164, ((int)hSpecial > 0) ? "forge" : null);
            else skipCounter++;
        }

        // Enhancing the weapon with the weapon special
        if (weapon != null)
        {
            int shopID = 0;
            bool canEnhance = true;

            if ((int)wSpecial <= 6)
            {
                switch (type)
                {
                    case EnhancementType.Fighter:
                        shopID = 635;
                        break;
                    case EnhancementType.Thief:
                        shopID = 637;
                        break;
                    case EnhancementType.Hybrid:
                        shopID = 633;
                        break;
                    case EnhancementType.Wizard:
                    case EnhancementType.SpellBreaker:
                        shopID = 636;
                        break;
                    case EnhancementType.Healer:
                        shopID = 638;
                        break;
                    case EnhancementType.Lucky:
                        shopID = 639;
                        break;
                    default:
                        Core.Logger($"Enhancement Failed:\tInvalid \"EnhancementType\" given, received {(int)wSpecial} | {wSpecial}");
                        return;
                }
            }
            else
            {
                switch (wSpecial)
                {
                    case WeaponSpecial.Forge:
                        if (!uForgeWeapon())
                        {
                            Core.Logger("Enhancement Failed:\tYou did not unlock the Forge (Weapon) Enhancement yet");
                            canEnhance = false;
                        }
                        break;
                    case WeaponSpecial.Lacerate:
                        if (!uLacerate())
                            Fail();
                        break;
                    case WeaponSpecial.Smite:
                        if (!uSmite())
                            Fail();
                        break;
                    case WeaponSpecial.Valiance:
                        if (!uValiance())
                            Fail();
                        break;
                    case WeaponSpecial.Arcanas_Concerto:
                        if (!uArcanasConcerto())
                        {
                            Core.Logger("Enhancement Failed:\tYou did not unlock the Arcana's Concerto Enhancement yet");
                            canEnhance = false;
                        }
                        break;
                    case WeaponSpecial.Elysium:
                        if (!uElysium())
                            Fail();
                        break;
                    case WeaponSpecial.Acheron:
                        if (!uAcheron())
                            Fail();
                        break;
                    case WeaponSpecial.Praxis:
                        if (!uPraxis())
                            Fail();
                        break;
                    case WeaponSpecial.Dauntless:
                        if (!uDauntless())
                            Fail();
                        break;
                    case WeaponSpecial.Ravenous:
                        if (!uRavenous())
                            Fail();
                        break;

                    default:
                        Core.Logger($"Enhancement Failed:\tInvalid \"WeaponSpecial\" given, received {(int)wSpecial} | {wSpecial}");
                        return;

                        void Fail()
                        {
                            Core.Logger($"Enhancement Failed:\tYou did not unlock the {wSpecial} Enhancement yet");
                            canEnhance = false;
                        }
                }

                shopID = 2142;
            }

            if (canEnhance)
                _AutoEnhance(weapon, shopID, ((int)wSpecial > 6) ? "forge" : null);
            else skipCounter++;
        }

        if (skipCounter > 0)
            Core.Logger($"Enhancement Skipped:\t{skipCounter} item{(skipCounter > 1 ? 's' : null)}");

        void _AutoEnhance(InventoryItem item, int shopID, string? map = null)
        {
            bool specialOnCape = item.Category == ItemCategory.Cape && cSpecial != CapeSpecial.None;
            bool specialOnHelm = item.Category == ItemCategory.Helm && hSpecial != HelmSpecial.None;
            bool specialOnWeapon = item.ItemGroup == "Weapon" && wSpecial.ToString() != "None";
            List<ShopItem> shopItems = Core.GetShopItems(map ?? Bot.Map.Name, shopID);

            // Shopdata complete check
            if (!shopItems.Any(x => x.Category == ItemCategory.Enhancement) || shopItems.Count == 0)
            {
                Core.Logger($"Enhancement Failed:\tCouldn't find enhancements in shop {shopID}");
                return;
            }

            // Checking if the item is already optimally enhanced
            // Core.DebugLogger(this, item.Name);
            if (Bot.Player.Level == item.EnhancementLevel)
            {
                if (specialOnCape)
                {
                    if ((int)cSpecial == item.EnhancementPatternID)
                    {
                        skipCounter++;
                        return;
                    }
                }
                else if (specialOnWeapon)
                {
                    if (((int)wSpecial <= 6 ? (int)type : 10) == item.EnhancementPatternID && ((int)wSpecial == getProcID(item) || ((int)wSpecial == 99 && getProcID(item) == 0)))
                    {
                        skipCounter++;
                        return;
                    }
                }
                else if ((int)type == item.EnhancementPatternID)
                {
                    skipCounter++;
                    return;
                }
            }

            // Logging
            if (specialOnCape)
                Core.Logger($"Searching Enhancement:\tForge/{cSpecial.ToString().Replace("_", " ")} - \"{item.Name}\"");
            else if (specialOnWeapon)
                Core.Logger($"Searching Enhancement:\t{((int)wSpecial <= 6 ? type : "Forge")}/{wSpecial.ToString().Replace("_", " ")} - \"{item.Name}\"");
            else
                Core.Logger($"Searching Enhancement:\t{type} - \"{item.Name}\"");

            List<ShopItem> availableEnh = new();

            // Filters
            foreach (ShopItem enh in shopItems)
            {
                // Remove enhancments that you dont have access to
                if ((!Core.IsMember && enh.Upgrade) || (enh.Level > Bot.Player.Level))
                    continue;

                string enhName = enh.Name.Replace(" ", "").Replace("\'", "").ToLower();

                // Cape if cSpecial
                if (specialOnCape && enhName.Contains(cSpecial.ToString().Replace("_", "").ToLower()))
                    availableEnh.Add(enh);
                // Weapon if wSpecial
                else if (specialOnWeapon && enhName.Contains(wSpecial.ToString().Replace("_", "").ToLower()))
                    availableEnh.Add(enh);
                //Helm if hSpecial
                else if (specialOnHelm && enhName.Contains(hSpecial.ToString().Replace("_", "").ToLower()))
                    availableEnh.Add(enh);
                // Class
                else if (item.Category == ItemCategory.Class && enhName.Contains("armor"))
                    availableEnh.Add(enh);
                // Helm
                else if (item.Category == ItemCategory.Helm && enhName.Contains("helm"))
                    availableEnh.Add(enh);
                // Cape if not cSpecial
                else if (item.Category == ItemCategory.Cape && enhName.Contains("cape"))
                    availableEnh.Add(enh);
                // Weapon2 if not wSpecial
                else if (item.ItemGroup == "Weapon" && enhName.Contains("weapon"))
                    availableEnh.Add(enh);
            }

            // Empty check
            ShopItem? bestEnhancement = null;
            if (availableEnh.Count == 0)
            {
                Core.Logger($"Enhancement Failed:\t\"availableEnh\" is empty");
                return;
            }
            else if (availableEnh.Count == 1)
                bestEnhancement = availableEnh.First();
            else
            {
                // Sorting by level (descending)
                List<ShopItem> sortedList = availableEnh.OrderByDescending(x => x.Level)
                    .ThenByDescending(x => x.Upgrade ? 1 : 0).ToList();
                bestEnhancement = sortedList[0];
            }

            // Null check
            if (bestEnhancement == null)
            {
                Core.Logger($"Enhancement Failed:\tCould not find the best enhancement for \"{item.Name}\"");
                return;
            }

            // Compare with current enhancement
            if (bestEnhancement.ID == getEnhID(item))
            {
                Core.Logger($"Enhancement Canceled:\tBest enhancement is already applied for \"{item.Name}\"");
                return;
            }

            // Enhancing the item
            Bot.Send.Packet($"%xt%zm%enhanceItemShop%{Bot.Map.RoomID}%{item.ID}%{bestEnhancement.ID}%{shopID}%");

            // Final logging
            if (specialOnCape)
                Core.Logger($"Enhancement Applied:\tForge/{cSpecial.ToString().Replace("_", " ")} - \"{item.Name}\" (Lvl {bestEnhancement.Level})");
            else if (specialOnWeapon)
                Core.Logger($"Enhancement Applied:\t{((int)wSpecial <= 6 ? type : "Forge")}/{wSpecial.ToString().Replace("_", " ")} - \"{item.Name}\" (Lvl {bestEnhancement.Level})");
            else
                Core.Logger($"Enhancement Applied:\t{type} - \"{item.Name}\" (Lvl {bestEnhancement.Level})");

            Core.Sleep();
        }
    }

    private int getProcID(InventoryItem? item)
        => item == null ? 0 : Core.GetItemProperty<int>(item, "ProcID");
    private int getEnhID(InventoryItem? item)
        => item == null ? 0 : Core.GetItemProperty<int>(item, "iEnh");

    private bool uAwe()
        => Core.isCompletedBefore(2937);
    private bool uForgeWeapon()
        => Core.isCompletedBefore(8738);
    private bool uLacerate()
        => Core.isCompletedBefore(8739);
    private bool uSmite()
        => Core.isCompletedBefore(8740);
    private bool uValiance()
        => Core.isCompletedBefore(8741);
    private bool uArcanasConcerto()
        => Core.isCompletedBefore(8742);
    private bool uAbsolution()
        => Core.isCompletedBefore(8743);
    private bool uVainglory()
        => Core.isCompletedBefore(8744);
    private bool uAvarice()
        => Core.isCompletedBefore(8745);
    private bool uForgeCape()
        => Core.isCompletedBefore(8758);
    private bool uElysium()
        => Core.isCompletedBefore(8821);
    private bool uAcheron()
        => Core.isCompletedBefore(8820);
    private bool uPenitence()
        => Core.isCompletedBefore(8822);
    private bool uLament()
        => Core.isCompletedBefore(8823);
    private bool uVim()
        => Core.isCompletedBefore(8824);
    private bool uExamen()
        => Core.isCompletedBefore(8825);
    private bool uForgeHelm()
        => Core.isCompletedBefore(8828);
    private bool uPneuma()
        => Core.isCompletedBefore(8827);
    private bool uAnima()
        => Core.isCompletedBefore(8826);
    private bool uDauntless()
        => Core.isCompletedBefore(9172);
    private bool uPraxis()
        => Core.isCompletedBefore(9171);
    private bool uRavenous()
        => Core.isCompletedBefore(9560);
    private bool uHearty()
    {
        return Core.isCompletedBefore(9466) && Farm.FactionRank("Grimskull") >= 7;
    }

    #endregion

    #region SmartEnhance

    /// <summary>
    /// Automatically finds the best Enhancement for the given class and enhances all equipped gear with it too
    /// </summary>
    /// <param name="className">Name of the class you wish to enhance</param>
    public void SmartEnhance(string? className)
    {
        if (string.IsNullOrEmpty(className))
        {
            Core.Logger($"{className} is null");
            return;
        }

        if (!Core.CheckInventory(className))
        {
            Core.Logger($"SmartEnhance Failed: Class {className} was not found in inventory");
            return;
        }

        if (Bot.Player.InCombat)
            Core.JumpWait();

        // Error correction
        className = className.ToLower().Trim();
        InventoryItem? SelectedClass = Bot.Inventory.Items.Find(i => i.Name.ToLower() == className && i.Category == ItemCategory.Class);
        if (SelectedClass == null)
        {
            Core.Logger($"SmartEnhance Failed: Class {className} was not found in inventory");
            return;
        }

        // Creating variables that can be assigned to
        className = SelectedClass.Name.ToLower();
        EnhancementType? type = null;
        CapeSpecial cSpecial = CapeSpecial.None;
        HelmSpecial hSpecial = HelmSpecial.None;
        WeaponSpecial wSpecial = WeaponSpecial.None;

        // If the item doesnt exist in the forge enh lib, or the player doesn't have the Forge enh unlocked, use Awe enh instead
        if (!ForgeEnhancementLibrary())
            AweEnhancementLibrary();

        // Can't be too careful
        if (type == null)
        {
            Core.Logger($"SmartEnhance Failed: 'type' for {className} is NULL");
            return;
        }

        // If the class isn't enhanced yet, enhance it with the enhancement type
        if (SelectedClass.EnhancementLevel <= 0)
            EnhanceItem(className, (EnhancementType)type);
        Core.Equip(className);
        Bot.Wait.ForTrue(() => Bot.Player.CurrentClass?.Name == className, 40);
        EnhanceEquipped((EnhancementType)type, cSpecial, hSpecial, wSpecial);

        bool ForgeEnhancementLibrary()
        {
            switch (className)
            {

                #region Lucky Region

                #region Luck - Awe_Blast | Ravenous | Valiance - ForgeHelm - Penitence
                case "lord of order":
                    if (!uAwe() || !uForgeHelm() || !uPenitence())
                        goto default;

                    type = EnhancementType.Lucky;
                    wSpecial = uRavenous() ? WeaponSpecial.Ravenous : WeaponSpecial.Awe_Blast;
                    hSpecial = HelmSpecial.Forge;
                    cSpecial = CapeSpecial.Penitence;
                    break;
                #endregion

                #region Ravenous
                case "PlaceHodler":
                    if (!uRavenous())
                        goto default;

                    type = EnhancementType.Lucky;
                    cSpecial = CapeSpecial.Forge;
                    wSpecial = WeaponSpecial.Ravenous;
                    break;
                #endregion Ravenous


                #region Lucky - Forge - Spiral Carve
                case "corrupted chronomancer":
                case "underworld chronomancer":
                case "timekeeper":
                case "timekiller":
                case "eternal chronomancer":
                case "immortal chronomancer":
                case "dark metal necro":
                case "great thief":
                    if (!uForgeCape())
                        goto default;

                    type = EnhancementType.Lucky;
                    cSpecial = CapeSpecial.Forge;
                    wSpecial = WeaponSpecial.Spiral_Carve;
                    break;
                #endregion

                #region Lucky - Forge - Awe Blast
                case "glacial berserker":
                    if (!Core.isCompletedBefore(8758))
                        goto default;

                    type = EnhancementType.Lucky;
                    cSpecial = CapeSpecial.Forge;
                    wSpecial = WeaponSpecial.Awe_Blast;
                    break;
                #endregion

                #region Lucky - Forge - Mana Vamp
                case "legendary elemental warrior":
                case "ultra elemental warrior":
                    if (!uForgeCape())
                        goto default;

                    type = EnhancementType.Lucky;
                    cSpecial = CapeSpecial.Forge;
                    wSpecial = WeaponSpecial.Mana_Vamp;
                    break;
                #endregion

                #region Lucky - Forge - Smite
                case "Draconic Chronomancer":
                    if (!uSmite() || !uForgeCape())
                        goto default;

                    type = EnhancementType.Lucky;
                    cSpecial = CapeSpecial.Forge;
                    wSpecial = WeaponSpecial.Smite;
                    break;
                #endregion

                #region Lucky - Forge - Elysium
                case "ultra omniknight":
                case "dark ultra omninight":
                    if (!uElysium() || !uForgeCape())
                        goto default;

                    type = EnhancementType.Lucky;
                    cSpecial = CapeSpecial.Forge;
                    wSpecial = WeaponSpecial.Elysium;
                    break;
                #endregion

                #region Lucky - Vainglory - Valiance - Anima
                case "archfiend":
                case "eternal inversionist":
                case "dragonlord":
                    if (!uVainglory() || !uValiance() || !uAnima())
                        goto default;

                    type = EnhancementType.Lucky;
                    cSpecial = CapeSpecial.Vainglory;
                    wSpecial = WeaponSpecial.Valiance;
                    hSpecial = HelmSpecial.Anima;

                    break;
                #endregion

                #region Lucky - Vainglory - Valiance - Vim
                case "continuum chronomancer":
                case "quantum chronomancer":
                case "chaos avenger":
                    if (!uPenitence()
                    || (!uDauntless() || !uValiance()) || !uRavenous() || !uValiance()
                    || !uAnima())
                        goto default;

                    type = EnhancementType.Lucky;
                    cSpecial = CapeSpecial.Vainglory;
                    wSpecial = uRavenous() ? WeaponSpecial.Ravenous :
                                uDauntless() ? WeaponSpecial.Dauntless : WeaponSpecial.Valiance;

                    hSpecial = HelmSpecial.Anima;
                    break;
                #endregion

                #region Lucky - Lacerate - Forge - Lament

                case "doom metal necro":
                case "neo metal necro":
                    if (!uLacerate() || !uForgeHelm() || !uLament())
                        goto default;

                    type = EnhancementType.Lucky;
                    cSpecial = uLament() ? CapeSpecial.Lament : CapeSpecial.Vainglory;
                    wSpecial = WeaponSpecial.Lacerate;
                    hSpecial = HelmSpecial.Forge;
                    break;
                #endregion Lucky - lacerate - forge

                #region Lucky - Vainglory - Dauntless|Valiance|Smite - Vim
                case "yami no ronin":
                    if ((!uDauntless() && !uValiance() && !uSmite()) || !uVainglory() || !uVim())
                        goto default;


                    type = EnhancementType.Lucky;
                    cSpecial = CapeSpecial.Vainglory;
                    wSpecial = uDauntless() ? WeaponSpecial.Dauntless :
                                uValiance() ? WeaponSpecial.Valiance :
                                WeaponSpecial.Smite; // else do smite, if no smite > do Awe
                    hSpecial = HelmSpecial.Vim;
                    break;
                #endregion

                #region Lucky - Vainglory - Valiance - Anima
                case "nechronomancer":
                case "necrotic chronomancer":
                    if (!uVainglory() || !uArcanasConcerto() || !uAnima())
                        goto default;

                    type = EnhancementType.Lucky;
                    cSpecial = CapeSpecial.Vainglory;
                    wSpecial = WeaponSpecial.Valiance;
                    hSpecial = HelmSpecial.Anima;
                    break;
                #endregion

                #region Lucky - Vainglory - Elysium - Vim
                case "shadowwalker of time":
                case "shadowstalker of time":
                case "shadowweaver of time":
                    if (!uVainglory() || !uElysium() || !uVim())
                        goto default;

                    type = EnhancementType.Lucky;
                    cSpecial = CapeSpecial.Vainglory;
                    wSpecial = WeaponSpecial.Elysium;
                    hSpecial = HelmSpecial.Vim;
                    break;
                #endregion

                #region Lucky - Vainglory - Valiance - None
                case "legion doomknight":
                    if (!uVainglory() || !uValiance())
                        goto default;

                    type = EnhancementType.Lucky;
                    cSpecial = CapeSpecial.Vainglory;
                    wSpecial = WeaponSpecial.Valiance;
                    hSpecial = CurrentHelmSpecial();
                    break;
                #endregion

                #region Lucky - Vainglory - Elysium - Pneuma
                case "antique hunter":
                case "artifact hunter":
                    if (!uVainglory() || !uElysium() || !uPneuma())
                        goto default;

                    type = EnhancementType.Lucky;
                    cSpecial = CapeSpecial.Vainglory;
                    wSpecial = WeaponSpecial.Elysium;
                    hSpecial = HelmSpecial.Pneuma;
                    break;
                #endregion

                #region Lucky - Lament - Elysium - Pneuma
                case "abyssal angel":
                case "abyssal angel's shadow":
                    if (!uLament() || !uElysium() || !uPneuma())
                        goto default;

                    type = EnhancementType.Lucky;
                    cSpecial = CapeSpecial.Vainglory;
                    wSpecial = WeaponSpecial.Elysium;
                    hSpecial = HelmSpecial.Pneuma;
                    break;
                #endregion

                #region Lucky - Dauntless | Ravenous - Anima | ForgeHelm - Vainglory
                case "verus doomknight":
                    if (!uRavenous() || !uForgeHelm() || !uVainglory())
                        goto default;

                    type = EnhancementType.Lucky;
                    cSpecial = CapeSpecial.Vainglory;
                    wSpecial = uDauntless() ? WeaponSpecial.Dauntless : WeaponSpecial.Ravenous;
                    hSpecial = uAnima() ? HelmSpecial.Anima : HelmSpecial.Forge;
                    break;
                #endregion

                #region Lucky - Vainglory - Dauntless/Valiance - Anima
                case "void highlord":
                case "void highlord (ioda)":
                    if (!uAnima() || !uValiance() || !uVainglory())
                        goto default;

                    type = EnhancementType.Lucky;
                    cSpecial = CapeSpecial.Vainglory;
                    wSpecial = !uDauntless() ?
                    (uRavenous() ? WeaponSpecial.Ravenous
                    : (uValiance() ? WeaponSpecial.Valiance : WeaponSpecial.Forge))
                    : WeaponSpecial.Dauntless;
                    hSpecial = HelmSpecial.Anima;
                    break;
                #endregion



                #region Lucky - Avarice - Dauntless - Anima
                case "flame dragon warrior":
                    if (!uAvarice() || !uDauntless() || !uAnima())
                        goto default;

                    type = EnhancementType.Lucky;
                    cSpecial = CapeSpecial.Avarice;
                    wSpecial = WeaponSpecial.Dauntless;
                    hSpecial = HelmSpecial.Anima;
                    break;
                #endregion

                #region Lucky - Avarice - Elysium - Anima
                case "chaos slayer":
                case "chaos slayer berserker":
                case "chaos slayer cleric":
                case "chaos slayer mystic":
                case "chaos slayer thief":
                    if (!uAvarice() || !uElysium() || !uAnima())
                        goto default;

                    type = EnhancementType.Lucky;
                    cSpecial = CapeSpecial.Avarice;
                    wSpecial = WeaponSpecial.Elysium;
                    hSpecial = HelmSpecial.Anima;
                    break;
                #endregion

                #region Lucky - Penitence - Ravenous | Praxis | Lacerate - Forge | None 
                case "archpaladin":
                    if (!uLacerate() || !uForgeHelm() || !uPenitence())
                        goto default;

                    type = EnhancementType.Lucky;
                    wSpecial = uRavenous() ? WeaponSpecial.Ravenous : (uPraxis() ? WeaponSpecial.Praxis : WeaponSpecial.Lacerate);
                    hSpecial = uForgeHelm() ? HelmSpecial.Forge : HelmSpecial.None;
                    cSpecial = CapeSpecial.Penitence;
                    break;
                #endregion

                #region Fighter - Ravenous | Valiance - Anima - Absolution
                case "stonecrusher":
                    if (!uValiance() || !uAnima() || !uAbsolution())
                        goto default;

                    type = EnhancementType.Fighter;
                    wSpecial = uRavenous() ? WeaponSpecial.Ravenous : WeaponSpecial.Valiance;
                    hSpecial = HelmSpecial.Anima;
                    cSpecial = CapeSpecial.Absolution;
                    break;
                #endregion

                #endregion

                #region Wizard Region

                #region Wizard -  Valiance|Praxis - Pneuna - Vainglory|Lament
                case "lightcaster":
                    if (!uValiance() || !uPneuma() || !uVainglory())
                    {
                        if (!uLament() || !uPraxis())
                            goto default;
                    }
                    type = EnhancementType.Wizard;
                    cSpecial = !uVainglory() ? CapeSpecial.Lament : CapeSpecial.Vainglory;
                    wSpecial = !uValiance() ? WeaponSpecial.Praxis : WeaponSpecial.Valiance;
                    hSpecial = !uPneuma() ? CurrentHelmSpecial() : HelmSpecial.Pneuma;
                    break;
                #endregion

                #region Wizard - Forge - Awe Blast
                case "infinity knight":
                    if (!uForgeCape())
                        goto default;

                    type = EnhancementType.Wizard;
                    cSpecial = CapeSpecial.Forge;
                    wSpecial = WeaponSpecial.Awe_Blast;
                    hSpecial = CurrentHelmSpecial();
                    break;
                #endregion

                #region Wizard - Vainglory - Valiance - Pneuma
                case "archmage":
                case "darklord":
                case "arcana invoker":
                    if (!uVainglory() || !uValiance() || !uPneuma())
                        goto default;

                    type = EnhancementType.Wizard;
                    cSpecial = CapeSpecial.Vainglory;
                    wSpecial = uRavenous() ? WeaponSpecial.Ravenous : WeaponSpecial.Valiance;
                    hSpecial = HelmSpecial.Pneuma;
                    break;
                #endregion

                #region Wizard - Penitence - Acheron - Pneuma
                case "master of moglins":
                case "dark master of moglins":
                    if (!uPenitence() || !uAcheron() || !uPneuma())
                        goto default;

                    type = EnhancementType.Wizard;
                    cSpecial = CapeSpecial.Penitence;
                    wSpecial = WeaponSpecial.Acheron;
                    hSpecial = HelmSpecial.Pneuma;
                    break;
                #endregion

                #region Wizard - Vainglory - Ravenous | Valiance - Pneuma | Wizard
                case "legion revenant":
                case "legion revenant (ioda)":
                    if (!uVainglory() || !uValiance() || !uPneuma())
                        goto default;

                    type = EnhancementType.Wizard;
                    cSpecial = CapeSpecial.Vainglory;
                    wSpecial = uArcanasConcerto() ? WeaponSpecial.Ravenous : WeaponSpecial.Valiance;
                    hSpecial = HelmSpecial.Pneuma;
                    break;
                #endregion

                #region Wizard - Avarice - Elysium - Pneuma
                case "shaman":
                case "vampire lord":
                case "enchanted vampire lord":
                case "royal vampire lord":
                case "darkside":
                case "dark lord":
                    if (!uAvarice() || !uElysium() || !uPneuma())
                        goto default;

                    type = EnhancementType.Wizard;
                    cSpecial = CapeSpecial.Avarice;
                    wSpecial = WeaponSpecial.Elysium;
                    hSpecial = HelmSpecial.Pneuma;
                    break;
                #endregion

                #region Wizard - Avarice - Acheron - Pneuma
                case "blaze binder":
                    if (!uAvarice() || !uAcheron() || !uPneuma())
                        goto default;

                    type = EnhancementType.Wizard;
                    cSpecial = CapeSpecial.Avarice;
                    wSpecial = WeaponSpecial.Acheron;
                    hSpecial = HelmSpecial.Pneuma;
                    break;
                #endregion

                #region Wizard - Lament - Elysium - Pneuma
                case "royal battlemage":
                    if (!uLament() || !uElysium() || !uPneuma())
                        goto default;

                    type = EnhancementType.Wizard;
                    cSpecial = CapeSpecial.Lament;
                    wSpecial = WeaponSpecial.Elysium;
                    hSpecial = HelmSpecial.Pneuma;
                    break;
                #endregion

                #region Wizard - Lament - Valiance - Pneuma
                case "scarlet sorceress":
                    if (!uLament() || !uValiance() || !uPneuma())
                        goto default;

                    type = EnhancementType.Wizard;
                    cSpecial = CapeSpecial.Lament;
                    wSpecial = WeaponSpecial.Valiance;
                    hSpecial = HelmSpecial.Pneuma;
                    break;
                #endregion


                #region Wizard - Vainglory / Forge - Daunt / Ravenous / Forge - Pneuma / Forge       
                case "Sovereign of Storms":
                    if (!uVainglory() || !uDauntless() || !uRavenous() || !uPneuma())
                        goto default;

                    type = EnhancementType.Wizard;
                    cSpecial = uVainglory() ? CapeSpecial.Vainglory : CapeSpecial.Forge;
                    wSpecial = uDauntless() ? WeaponSpecial.Dauntless : (uRavenous() ? WeaponSpecial.Ravenous : WeaponSpecial.Forge);
                    hSpecial = uPneuma() ? HelmSpecial.Pneuma : HelmSpecial.Forge;
                    break;
                #endregion
                #endregion

                #region Healer Region

                #region Healer - Avarice - Elysium - Pneuma
                case "dragon of time":
                    if (!uAvarice() || !uElysium() || !uPneuma())
                        goto default;

                    type = EnhancementType.Healer;
                    cSpecial = CapeSpecial.Avarice;
                    wSpecial = WeaponSpecial.Elysium;
                    hSpecial = HelmSpecial.Pneuma;
                    break;

                #endregion

                #region  Healer - None - Valiance - Nine
                case "obsidian paladin chronomancer":
                case "paladin chronomancer":
                    if (!uValiance())
                        goto default;

                    type = EnhancementType.Healer;
                    cSpecial = CapeSpecial.None;
                    wSpecial = WeaponSpecial.Valiance;
                    hSpecial = HelmSpecial.None;
                    break;

                #endregion

                #region Fighter - Ravenous | Valiance - Anima - Absolution
                case "frostval barbarian":
                    if (!uAbsolution() || !uValiance() || !uAnima())
                        goto default;
                    type = EnhancementType.Fighter;
                    cSpecial = CapeSpecial.Absolution;
                    wSpecial = uRavenous() ? WeaponSpecial.Ravenous : WeaponSpecial.Valiance;
                    hSpecial = HelmSpecial.Anima;
                    break;
                #endregion

                #region Lucky - Penitence | Absolution - Elysium | Valiance - Vim
                case "arachnomancer":
                    if (!uAbsolution() || !uAbsolution() || !uVim())
                        goto default;

                    type = EnhancementType.Lucky;
                    cSpecial = uPenitence() ? CapeSpecial.Penitence : CapeSpecial.Absolution;
                    wSpecial = uElysium() ? WeaponSpecial.Elysium : WeaponSpecial.Valiance;
                    hSpecial = HelmSpecial.Vim;
                    break;
                #endregion

                #region Healer - Valiance - Current - Current

                #endregion

                #region Wizard - Elysium - Pneuma | Wizard - Vainglory

                #endregion

                #region Healer - Current - Valiance/Awe - Current
                case "healer":
                case "healer (rare)":
                    type = EnhancementType.Healer;
                    cSpecial = CurrentCapeSpecial();
                    wSpecial = uValiance() ? WeaponSpecial.Valiance : WeaponSpecial.Awe_Blast;
                    hSpecial = CurrentHelmSpecial();
                    break;
                #endregion

                #region Luck - Vim - Lam - Rav
                case "Chrono ShadowSlayer":
                case "Chrono ShadowHunter":
                    type = EnhancementType.Lucky;
                    cSpecial = uLament() ? CapeSpecial.Lament : (uForgeCape() ? CapeSpecial.Forge : CurrentCapeSpecial());
                    wSpecial = uRavenous() ? WeaponSpecial.Ravenous : (uArcanasConcerto() ? WeaponSpecial.Arcanas_Concerto : (uForgeWeapon() ? WeaponSpecial.Forge : WeaponSpecial.Awe_Blast));
                    hSpecial = uVim() ? HelmSpecial.Vim : (uForgeHelm() ? HelmSpecial.Forge : CurrentHelmSpecial());
                    break;
                #endregion

                #region Lucky - Vainglory - Valiance / Dauntless - Anima
                case "glacial warlord":
                    if (!uVainglory() || !uValiance() || !uAnima())
                        goto default;

                    type = EnhancementType.Lucky;
                    cSpecial = CapeSpecial.Vainglory;
                    wSpecial = uDauntless() ? WeaponSpecial.Dauntless : WeaponSpecial.Valiance;
                    hSpecial = HelmSpecial.Anima;
                    break;
                #endregion

                #region Luck - Val/Smite/Mana - Anima - Vg
                case "dragonslayer general":
                    type = EnhancementType.Lucky;

                    cSpecial = uVainglory()
                        ? CapeSpecial.Vainglory
                        : uForgeCape()
                            ? CapeSpecial.Forge
                            : CurrentCapeSpecial();

                    wSpecial = uValiance()
                        ? WeaponSpecial.Valiance
                        : uSmite()
                            ? WeaponSpecial.Smite
                            : WeaponSpecial.Mana_Vamp;

                    hSpecial = uAnima()
                        ? HelmSpecial.Anima
                        : uForgeHelm()
                            ? HelmSpecial.Forge
                            : CurrentHelmSpecial();

                    break;
                #endregion

                #region Luck - Dauntless | Ravenous - Anima - Vainglory
                case "chrono chaorruptor":
                    if (!uRavenous() || !uAnima() || !uVainglory())
                        goto default;

                    type = EnhancementType.Lucky;
                    cSpecial = CapeSpecial.Vainglory;
                    wSpecial = uDauntless() ? WeaponSpecial.Dauntless : WeaponSpecial.Ravenous;
                    hSpecial = HelmSpecial.Anima;
                    break;
                #endregion

                #region Wizard - Ravenous - Pneuma - Vainglory
                case "chrono dataknight":
                case "chrono dragonknight":
                    if (!uRavenous() || !uPneuma() || !uVainglory())
                        goto default;

                    type = EnhancementType.Wizard;
                    cSpecial = CapeSpecial.Vainglory;
                    wSpecial = WeaponSpecial.Ravenous;
                    hSpecial = HelmSpecial.Pneuma;
                    break;
                #endregion

                #region Luck - Ravenous | Valiance - ForgeHelm | Luck - Absolution
                case "legendary hero":
                    if (!uValiance() || !uForgeHelm() || !uAbsolution())
                        goto default;

                    type = EnhancementType.Wizard;
                    cSpecial = CapeSpecial.Absolution;
                    wSpecial = uRavenous() ? WeaponSpecial.Ravenous : WeaponSpecial.Valiance;
                    hSpecial = HelmSpecial.Forge;
                    break;
                #endregion

                #endregion

                #region Unassigned Region

                // This list serves as an overview of what classes dont have a Forge Enhancement yet, when adding a setup for it, remove it from here
                case "acolyte":
                case "alpha doommega":
                case "alpha omega":
                case "alpha pirate":
                case "arcane dark caster":
                case "assassin":
                case "barber":
                case "bard":
                case "battlemage of love":
                case "battlemage":
                case "beast warrior":
                case "beastmaster":
                case "berserker":
                case "beta berserker":
                case "blademaster assassin":
                case "blademaster":
                case "blood ancient":
                case "blood sorceress":
                case "blood titan":
                case "cardclasher":
                case "chaos avenger member preview":
                case "chaos champion prime":
                case "chaos shaper":
                case "chrono assassin":
                case "chrono commandant":
                case "chronocommander":
                case "chronocorrupter":
                case "chronomancer prime":
                case "chronomancer":
                case "chunin":
                case "classic alpha pirate":
                case "classic barber":
                case "classic defender":
                case "classic doomknight":
                case "classic dragonlord":
                case "classic exalted soul cleaver":
                case "classic guardian":
                case "classic paladin":
                case "classic pirate":
                case "classic soul cleaver":
                case "clawsuit":
                case "cryomancer mini pet coming soon":
                case "cryomancer":
                case "daimon":
                case "dark battlemage":
                case "dark caster":
                case "dark chaos berserker":
                case "dark cryomancer":
                case "dark harbinger":
                case "dark legendary hero":
                case "darkblood stormking":
                case "deathknight lord":
                case "deathknight":
                case "defender":
                case "doomknight overlord":
                case "doomknight":
                case "dragon knight":
                case "dragon shinobi":
                case "dragonslayer":
                case "dragonsoul shinobi":
                case "drakel warlord":
                case "elemental dracomancer":
                case "empyrean chronomancer":
                case "enforcer":
                case "evolved clawsuit":
                case "evolved dark caster":
                case "evolved leprechaun":
                case "evolved pumpkin lord":
                case "evolved shaman":
                case "exalted harbinger":
                case "exalted soul cleaver":
                case "firelord summoner":
                case "frost spiritreaver":
                case "glacial berserker test":
                case "grim necromancer":
                case "grunge rocker":
                case "guardian":
                case "heavy metal necro":
                case "heavy metal rockstar":
                case "heroic naval commander":
                case "highseas commander":
                case "hobo highlord":
                case "horc evader":
                case "immortal dark caster":
                case "imperial chunin":
                case "infinite dark caster":
                case "infinite legion dark caster":
                case "infinity titan":
                case "legendary naval commander":
                case "legion blademaster assassin":
                case "legion doomknight tester":
                case "legion evolved dark caster":
                case "legion paladin":
                case "legion revenant member test":
                case "legion swordmaster assassin":
                case "leprechaun":
                case "lightcaster test":
                case "lightmage":
                case "love caster":
                case "lycan":
                case "master ranger":
                case "mechajouster":
                case "mindbreaker":
                case "mystical dark caster":
                case "naval commander":
                case "necromancer":
                case "ninja warrior":
                case "no class":
                case "northlands monk":
                case "not a mod":
                case "nu metal necro":
                case "obsidian no class":
                case "oracle":
                case "overworld chronomancer":
                case "paladin high lord":
                case "paladin":
                case "paladinslayer":
                case "pink romancer":
                case "pinkomancer":
                case "pirate":
                case "prismatic clawsuit":
                case "protosartorium":
                case "psionic mindbreaker":
                case "pumpkin lord":
                case "pyromancer":
                case "ranger":
                case "renegade":
                case "rustbucket":
                case "sakura cryomancer":
                case "sentinel":
                case "shadow dragon shinobi":
                case "shadow ripper":
                case "shadow rocker":
                case "shadowflame dragonlord":
                case "shadowscythe general":
                case "silver paladin":
                case "skycharged grenadier":
                case "skyguard grenadier":
                case "sorcerer":
                case "soul cleaver":
                case "star captain":
                case "starlord":
                case "swordmaster assassin":
                case "swordmaster":
                case "the collector":
                case "thief of hours":
                case "timeless chronomancer":
                case "timeless dark caster":
                case "troubador of love":
                case "unchained rocker":
                case "unchained rockstar":
                case "undead goat":
                case "undead leperchaun":
                case "undeadslayer":
                case "unlucky leperchaun":
                case "vampire":
                case "vindicator of they":
                case "void highlord tester":
                case "warlord":
                case "warrior (rare)":
                case "warrior":
                case "warriorscythe general":
                case "witch":
                default: // If the correct enhancement arent unlocked, or the class in question isnt in the Forge Enhancement Lib, use Awe Enhancements Lib
                    return false;

                    #endregion
            }
            return true;

            // // Always place this check as the last one in a 'if' + '||' stack.
            // // See EXAMPLE_CLASS as an example. 
            // bool uDauntlessExtra()
            // {
            //     // Check if Dauntless is unlocked, and set it as wSpecial if true.
            //     if (uDauntless())
            //     {
            //         wSpecial = WeaponSpecial.Dauntless;
            //         return true;
            //     }
            //     // If Dauntless is not unlocked, try Valiance and it's extras
            //     // If neither Valiance nor its bonusses are unlocked, this will return false so that it can be used with the 'goto default' lines
            //     else return uValianceExtra();
            // }

            // // Always place this check as the last one in a 'if' + '||' stack.
            // // See ArchPaladin as an example. 
            // bool uValianceExtra()
            // {
            //     // Check if Valiance is unlocked, and set it as wSpecial if true.
            //     if (uValiance())
            //         wSpecial = WeaponSpecial.Valiance;
            //     // Otherwise, check if Praxis is unlocked, and set it as wSpecial if true.
            //     else if (uPraxis())
            //         wSpecial = WeaponSpecial.Praxis;
            //     // If neither Valiance and Praxis are not unlocked, return false so that it can be used in conjunction with the 'goto default' lines.
            //     else return false;

            //     // This will only occur if Valiance or Praxis is unlocked.
            //     return true;
            // }
        }

        void AweEnhancementLibrary()
        {
            switch (className)
            {
                #region Lucky Region

                #region Lucky - Spiral Carve
                case "abyssal angel":
                case "abyssal angel's shadow":
                case "archpaladin":
                case "artifact hunter":
                case "assassin":
                case "archmage":
                case "beastmaster":
                case "berserker":
                case "beta berserker":
                case "blademaster assassin":
                case "blademaster":
                case "blood titan":
                case "cardclasher":
                case "chaos avenger member preview":
                case "chaos champion prime":
                case "chaos slayer":
                case "chaos slayer berserker":
                case "chaos slayer cleric":
                case "chaos slayer mystic":
                case "chaos slayer thief":
                case "chrono chaorruptor":
                case "chrono commandant":
                case "chronocommander":
                case "chronocorrupter":
                case "chunin":
                case "classic alpha pirate":
                case "classic barber":
                case "classic doomknight":
                case "classic exalted soul cleaver":
                case "classic guardian":
                case "classic paladin":
                case "classic pirate":
                case "classic soul cleaver":
                case "continuum chronomancer":
                case "corrupted chronomancer":
                case "dark chaos berserker":
                case "dark harbinger":
                case "doomknight":
                case "empyrean chronomancer":
                case "eternal chronomancer":
                case "evolved clawsuit":
                case "evolved dark caster":
                case "evolved leprechaun":
                case "exalted harbinger":
                case "exalted soul cleaver":
                case "glacial warlord":
                case "great thief":
                case "immortal chronomancer":
                case "imperial chunin":
                case "infinite dark caster":
                case "infinite legion dark caster":
                case "infinity titan":
                case "legion blademaster assassin":
                case "legion evolved dark caster":
                case "legion swordmaster assassin":
                case "leprechaun":
                case "lycan":
                case "master ranger":
                case "mechajouster":
                case "necromancer":
                case "ninja warrior":
                case "not a mod":
                case "overworld chronomancer":
                case "pinkomancer":
                case "prismatic clawsuit":
                case "quantum chronomancer":
                case "ranger":
                case "renegade":
                case "rogue":
                case "rogue (rare)":
                case "scarlet sorceress":
                case "shadowscythe general":
                case "skycharged grenadier":
                case "skyguard grenadier":
                case "soul cleaver":
                case "starlord":
                case "swordmaster assassin":
                case "swordmaster":
                case "timekeeper":
                case "timekiller":
                case "timeless chronomancer":
                case "undead goat":
                case "undead leperchaun":
                case "undeadslayer":
                case "underworld chronomancer":
                case "unlucky leperchaun":
                case "void highlord":
                case "void highlord (ioda)":
                case "verus doomknight":
                    type = EnhancementType.Lucky;
                    wSpecial = WeaponSpecial.Spiral_Carve;
                    break;
                #endregion

                #region Lucky - Mana Vamp
                case "alpha doommega":
                case "alpha omega":
                case "alpha pirate":
                case "beast warrior":
                case "blood ancient":
                case "chaos avenger":
                case "chaos shaper":
                case "classic defender":
                case "clawsuit":
                case "cryomancer mini pet coming soon":
                case "dark legendary hero":
                case "ultra omniknight":
                case "dark ultra omninight":
                case "doomknight overlord":
                case "dragonslayer general":
                case "drakel warlord":
                case "glacial berserker test":
                case "heroic naval commander":
                case "legendary elemental warrior":
                case "horc evader":
                case "legendary naval commander":
                case "legion revenant member test":
                case "naval commander":
                case "paladin high lord":
                case "paladin":
                case "paladinslayer":
                case "pirate":
                case "pumpkin lord":
                case "shadowflame dragonlord":
                case "shadowstalker of time":
                case "shadowwalker of time":
                case "shadowweaver of time":
                case "silver paladin":
                case "thief of hours":
                case "ultra elemental warrior":
                case "void highlord tester":
                case "warlord":
                case "warrior":
                case "warrior (rare)":
                case "warriorscythe general":
                case "yami no ronin":
                case "arachnomancer":
                    type = EnhancementType.Lucky;
                    wSpecial = WeaponSpecial.Mana_Vamp;
                    break;
                #endregion

                #region Lucky - Awe Blast
                case "bard":
                case "chrono assassin":
                case "chronomancer":
                case "chronomancer prime":
                case "dark metal necro":
                case "deathknight lord":
                case "dragon shinobi":
                case "dragonlord":
                case "evolved pumpkin lord":
                case "dragonsoul shinobi":
                case "glacial berserker":
                case "grunge rocker":
                case "guardian":
                case "heavy metal necro":
                case "heavy metal rockstar":
                case "hobo highlord":
                case "lord of order":
                case "legendary hero":
                case "nechronomancer":
                case "necrotic chronomancer":
                case "Draconic Chronomancer":
                case "no class":
                case "nu metal necro":
                case "obsidian no class":
                case "protosartorium":
                case "shadow dragon shinobi":
                case "shadow ripper":
                case "shadow rocker":
                case "star captain":
                case "troubador of love":
                case "unchained rocker":
                case "unchained rockstar":
                case "doom metal necro":
                case "neo metal necro":
                case "antique hunter":
                    type = EnhancementType.Lucky;
                    wSpecial = WeaponSpecial.Awe_Blast;
                    break;
                #endregion

                #region Lucky - Health Vamp
                case "eternal inversionist":
                case "archfiend":
                case "barber":
                case "classic dragonlord":
                case "dragonslayer":
                case "enforcer":
                case "flame dragon warrior":
                case "rustbucket":
                case "sentinel":
                case "vampire":
                case "vampire lord":
                case "enchanted vampire lord":
                case "royal vampire lord":
                case "Chrono ShadowHunter":
                    type = EnhancementType.Lucky;
                    wSpecial = WeaponSpecial.Health_Vamp;
                    break;
                #endregion

                #endregion

                #region  Theif Region

                #region  Theif - Mana Vamp
                case "ninja":
                    type = EnhancementType.Thief;
                    wSpecial = WeaponSpecial.Mana_Vamp;
                    break;
                #endregion

                #endregion

                #region Wizard Region

                #region Wizard - Awe Blast
                case "acolyte":
                case "arcane dark caster":
                case "battlemage":
                case "battlemage of love":
                case "blaze binder":
                case "blood sorceress":
                case "dark battlemage":
                case "dragon knight":
                case "firelord summoner":
                case "grim necromancer":
                case "highseas commander":
                case "infinity knight":
                case "interstellar knight":
                case "master of moglins":
                case "dark master of moglins":
                case "mystical dark caster":
                case "northlands monk":
                case "royal battlemage":
                case "timeless dark caster":
                case "witch":
                case "stonecrusher":
                    type = EnhancementType.Wizard;
                    wSpecial = WeaponSpecial.Awe_Blast;
                    break;
                #endregion

                #region Wizard - Spiral Carve
                case "chrono dataknight":
                case "chrono dragonknight":
                case "cryomancer":
                case "dark caster":
                case "dark cryomancer":
                case "darkblood stormking":
                case "darkside":
                case "defender":
                case "frost spiritreaver":
                case "immortal dark caster":
                case "legion paladin":
                case "legion revenant":
                case "legion revenant (ioda)":
                case "lightcaster":
                case "pink romancer":
                case "psionic mindbreaker":
                case "pyromancer":
                case "sakura cryomancer":
                case "troll spellsmith":
                case "classic legion doomknight":
                case "legion doomknight":
                case "legion doomknight tester":
                case "arcana invoker":
                    type = EnhancementType.Wizard;
                    wSpecial = WeaponSpecial.Spiral_Carve;
                    break;
                #endregion

                #region Wizard - Health Vamp
                case "daimon":
                case "dark lord":
                case "evolved shaman":
                case "lightmage":
                case "mindbreaker":
                case "vindicator of they":
                case "elemental dracomancer":
                case "lightcaster test":
                case "love caster":
                case "mage":
                case "mage (rare)":
                case "sorcerer":
                case "the collector":
                    type = EnhancementType.Wizard;
                    wSpecial = WeaponSpecial.Health_Vamp;
                    break;
                #endregion

                #region Wizard - Mana Vamp
                case "oracle":
                case "shaman":
                    type = EnhancementType.Wizard;
                    wSpecial = WeaponSpecial.Mana_Vamp;
                    break;
                #endregion

                #endregion

                #region Fighter Region

                #region Fighter - Awe Blast
                case "deathknight":
                case "frostval barbarian":
                    type = EnhancementType.Fighter;
                    wSpecial = WeaponSpecial.Awe_Blast;
                    break;
                #endregion

                #endregion

                #region Healer Region

                #region Healer - Health Vamp
                case "dragon of time":
                    type = EnhancementType.Healer;
                    wSpecial = WeaponSpecial.Health_Vamp;
                    break;
                #endregion

                #region Healer - Mana Vamp
                case "obsidian paladin chronomancer":
                case "paladin chronomancer":
                    type = EnhancementType.Healer;
                    wSpecial = WeaponSpecial.Mana_Vamp;
                    break;
                #endregion

                #endregion

                default:
                    Core.Logger($"SmartEnhance Failed: \"{className}\" is not found in the Smart Enhance Library, please report to @tato2", messageBox: true);
                    return;
            }
        }
    }

    #endregion
}

public enum GenericGearBoost
{
    cp,
    gold,
    rep,
    exp,
    dmgAll,
}
public enum RacialGearBoost
{
    None,
    Chaos,
    Dragonkin,
    Drakath,
    Elemental,
    Human,
    Orc,
    Undead,
}

public enum EnhancementType // Enhancement Pattern ID
{
    Fighter = 2,
    Thief = 3,
    Hybrid = 5,
    Wizard = 6,
    Healer = 7,
    SpellBreaker = 8,
    Lucky = 9,
}
public enum CapeSpecial // Enhancement Pattern ID
{
    None = 0,
    Forge = 10,
    Absolution = 11,
    Avarice = 12,
    Vainglory = 24,
    Penitence = 29,
    Lament = 30,

}
public enum WeaponSpecial // Proc ID
{
    None = 0,
    Spiral_Carve = 2,
    Awe_Blast = 3,
    Health_Vamp = 4,
    Mana_Vamp = 5,
    Powerword_Die = 6,
    Ravenous = 7,

    Forge = 99, // Not really 99, but cant have 0 3 times
    Lacerate = 8,
    Smite = 9,
    Valiance = 10,
    Arcanas_Concerto = 11,
    Elysium = 12,
    Acheron = 13,
    Praxis = 14,
    Dauntless = 15
}

public enum HelmSpecial //Enhancement Pattern ID
{
    None = 0,
    Forge = 99, // Not really 99, but cant have 0 3 times
    Vim = 25,
    Examen = 26,
    Anima = 28,
    Pneuma = 27,
    Hearty = 32
}

public enum mergeOptionsEnum
{
    all = 0,
    acOnly = 1,
    mergeMats = 2,
    select = 3
};