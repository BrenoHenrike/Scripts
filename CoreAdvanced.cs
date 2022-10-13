//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using System.Globalization;
using System.Reflection;
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Monsters;
using Skua.Core.Models.Quests;
using Skua.Core.Models.Shops;
using Skua.Core.Options;

public class CoreAdvanced
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface bot)
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
    /// <param name="shopQuant">How many items you get for 1 buy</param>
    /// <param name="shopItemID">Use this for Merge shops that has 2 or more of the item with the same name and you need the second/third/etc., be aware that it will re-log you after to prevent ghost buy. To get the ShopItemID use the built in loader of Skua</param>
    public void BuyItem(string map, int shopID, string itemName, int quant = 1, int shopQuant = 1, int shopItemID = 0)
    {
        if (Core.CheckInventory(itemName, quant))
            return;

        ShopItem item = Core.parseShopItem(Core.GetShopItems(map, shopID).Where(x => shopItemID == 0 ? x.Name == itemName : x.ShopItemID == shopItemID).ToList(), shopID, itemName);
        if (item == null)
            return;

        _BuyItem(map, shopID, item, quant, shopQuant, shopItemID);
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
    public void BuyItem(string map, int shopID, int itemID, int quant = 1, int shopQuant = 1, int shopItemID = 0)
    {
        if (Core.CheckInventory(itemID, quant))
            return;

        ShopItem item = Core.parseShopItem(Core.GetShopItems(map, shopID).Where(x => shopItemID == 0 ? x.ID == itemID : x.ShopItemID == shopItemID).ToList(), shopID, itemID.ToString());
        if (item == null)
            return;

        _BuyItem(map, shopID, item, quant, shopQuant, shopItemID);
    }

    private void _BuyItem(string map, int shopID, ShopItem item, int quant = 1, int shopQuant = 1, int shopItemID = 0)
    {
        GetItemReq(item);

        if (item.Requirements != null)
        {
            foreach (ItemBase req in item.Requirements)
            {
                if (Core.CheckInventory(req.ID, req.Quantity))
                    continue;

                if (Core.GetShopItems(map, shopID).Any(x => req.ID == x.ID))
                    BuyItem(map, shopID, req.ID, req.Quantity * quant);
            }
        }

        if (canBuy(new List<ShopItem>() { item }, shopID, item.Name))
            Core.BuyItem(map, shopID, item.Name, quant, shopQuant, shopItemID);
    }

    /// <summary>
    /// Buys all merge from a shop based on the script options selected setting. Will read where to get the ingredients from from the findIngredients param
    /// </summary>
    /// <param name="map">The map where the shop can be loaded from</param>
    /// <param name="shopID">The shop ID to load the shopdata</param>
    /// <param name="findIngredients">A switch nested in a void that will explain this function where to get items</param>
    public void StartBuyAllMerge(string map, int shopID, Action findIngredients, string buyOnlyThis = null, string[] itemBlackList = null)
    {
        if (buyOnlyThis == null)
            Bot.Config.Configure();

        int mode = (int)Bot.Config.Get<mergeOptionsEnum>("Generic", "mode");
        matsOnly = mode == 2;
        List<ShopItem> shopItems = Core.GetShopItems(map, shopID);
        List<ShopItem> items = new();
        bool memSkipped = false;

        foreach (ShopItem item in shopItems)
        {
            if (Core.CheckInventory(item.ID, toInv: false) ||
                miscCatagories.Contains(item.Category) ||
                (String.IsNullOrEmpty(buyOnlyThis) ? false : buyOnlyThis == item.Name) ||
                (itemBlackList != null && itemBlackList.Any(b => b.ToLower() == item.Name.ToLower())))
                continue;

            if (Core.IsMember ? true : !item.Upgrade)
            {
                if (mode == 3)
                {
                    if (Bot.Config.Get<bool>("Select", $"{item.ID}"))
                        items.Add(item);
                }
                else if (mode != 1)
                    items.Add(item);
                else if (item.Coins)
                    items.Add(item);
            }
            else if (mode == 3 && Bot.Config.Get<bool>("Select", $"{item.ID}"))
            {
                Core.Logger($"\"{item.Name}\" will be skipped, as you aren't member.");
                memSkipped = true;
            }
        }

        if (items.Count == 0)
        {
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
                    BuyItem(map, shopID, item.ID);

                    if (item.Coins)
                        Core.ToBank(item.Name);
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
                    if (Bot.Inventory.IsMaxStack(req.Name))
                        externalQuant = req.MaxStack;
                    else
                    {
                        if (req.Temp)
                            externalQuant = Bot.TempInv.GetQuantity(req.Name) + req.Quantity;
                        else externalQuant = Bot.Inventory.GetQuantity(req.Name) + req.Quantity;
                    }
                }
                else if (MaxStackOneItems.Contains(req.Name))
                    externalQuant = 1;
                else
                    externalQuant = req.Quantity * (craftingQ - Bot.Inventory.GetQuantity(item.ID));

                if (Core.CheckInventory(req.Name, externalQuant) && (matsOnly ? req.MaxStack == 1 : true))
                    continue;

                if (shopItems.Select(x => x.ID).Contains(req.ID) && !AltFarmItems.Contains(req.Name))
                {
                    ShopItem selectedItem = shopItems.First(x => x.ID == req.ID);

                    if (selectedItem.Requirements.Any(r => MaxStackOneItems.Contains(r.Name)))
                    {
                        while (!Core.CheckInventory(selectedItem.ID, req.Quantity))
                        {
                            getIngredients(selectedItem, req.Quantity);
                            Bot.Sleep(Core.ActionDelay);

                            if (!matsOnly)
                                BuyItem(map, shopID, selectedItem.ID, (Bot.Inventory.GetQuantity(selectedItem.ID) + selectedItem.Quantity), selectedItem.Quantity);
                            else break;
                        }
                    }
                    else
                    {
                        getIngredients(selectedItem, req.Quantity);
                        Bot.Sleep(Core.ActionDelay);

                        if (!matsOnly)
                            BuyItem(map, shopID, selectedItem.ID, req.Quantity);
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
    public List<ItemCategory> miscCatagories = new() { ItemCategory.Note, ItemCategory.Item, ItemCategory.Resource, ItemCategory.QuestItem, ItemCategory.ServerUse };
    public ItemBase externalItem = new();
    public int externalQuant = 0;
    public bool matsOnly = false;
    public List<string> MaxStackOneItems = new();
    public List<string> AltFarmItems = new();

    /// <summary>
    /// Checks if everything needed to buy the item is present, if not, it will log and return false
    /// </summary>
    /// <param name="map">The map where the shop can be loaded from</param>
    /// <param name="shopID">The shop ID to load the shopdata</param>
    /// <param name="itemName">The name of the item you're gonna check</param>
    public bool canBuy(string map, int shopID, string itemName)
    {
        List<ShopItem> shopItem = Core.GetShopItems(map, shopID).Where(x => x.Name == itemName).ToList();
        return canBuy(shopItem, shopID, itemName);
    }

    /// <summary>
    /// Checks if everything needed to buy the item is present, if not, it will log and return false
    /// </summary>
    /// <param name="map">The map where the shop can be loaded from</param>
    /// <param name="shopID">The shop ID to load the shopdata</param>
    /// <param name="itemID">The ID of the item you're gonna check</param>
    public bool canBuy(string map, int shopID, int itemID)
    {
        List<ShopItem> shopItem = Core.GetShopItems(map, shopID).Where(x => x.ID == itemID).ToList();
        return canBuy(shopItem, shopID, itemID.ToString());
    }

    private bool canBuy(List<ShopItem> shopItem, int shopID, string itemNameID = "")
    {
        ShopItem item = Core.parseShopItem(shopItem, shopID, itemNameID);
        if (item == null)
            return false;

        //Achievement Check
        int achievementID = Bot.Flash.GetGameObject<int>("world.shopinfo.iIndex");
        string io = Bot.Flash.GetGameObject<string>("world.shopinfo.sField");
        if (achievementID > 0 && io != null && !Core.HasAchievement(achievementID, io))
        {
            Core.Logger($"Cannot buy {item.Name} from {shopID} because you dont have achievement {achievementID} of category {io}.");
            return false;
        }

        //Member Check
        if (item.Upgrade && !Core.IsMember)
        {
            Core.Logger($"Cannot buy {item.Name} from {shopID} because you aren't a member.");
            return false;
        }

        //Requiered-Item Check
        int reqItemID = Bot.Flash.GetGameObject<int>("world.shopinfo.reqItems");
        if (reqItemID > 0 && !Core.CheckInventory(reqItemID))
        {
            Core.Logger($"Cannot buy {item.Name} from {shopID} because you dont have the requiered item needed to buy stuff from the shop, itemID: {reqItemID}");
            return false;
        }

        //Quest Check
        //string questName = Bot.Flash.GetGameObject<string>($"world.shopinfo.items[{item.ID}].sQuest");
        //List<QuestData> cache = Bot.Quests.Cached;
        //Bot.Quests.Cached.Count;

        //Rep check
        if (!String.IsNullOrEmpty(item.Faction) && item.Faction != "None")
        {
            int reqRank = RepCPLevel.First(x => x.Key == item.RequiredReputation).Value;
            if (reqRank > Farm.FactionRank(item.Faction))
            {
                Core.Logger($"Cannot buy {item.Name} from {shopID} because you dont have rank {reqRank} {item.Faction}.");
                return false;
            }
        }

        //Merge item check
        if (item.Requirements != null)
        {
            foreach (ItemBase req in item.Requirements)
            {
                Bot.Drops.Pickup(req.ID);
                Bot.Wait.ForPickup(req.ID);

                if (!Core.CheckInventory(req.ID, req.Quantity))
                {
                    if (Core.CheckInventory(req.ID))
                    {
                        Core.Logger($"Cannot buy {item.Name} from {shopID}. You own {Bot.Inventory.GetQuantity(req.ID)}x {req.Name} but need {req.Quantity} .");
                        return false;
                    }
                    Core.Logger($"Cannot buy {item.Name} from {shopID} because {req.Name} is missing.");
                    return false;
                }
            }
        }

        //Gold check
        if (item.Cost > Bot.Player.Gold)
        {
            Core.Logger($"Cannot buy {item.Name} from {shopID} because you are missing {item.Cost - Bot.Player.Gold} gold.");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Will make sure you have every requierment (XP, Rep and Gold) to buy the item.
    /// </summary>
    /// <param name="item">The ShopItem object containing all the information</param>
    public void GetItemReq(ShopItem item)
    {
        if (item.Faction != null && item.Faction != "None" && item.RequiredReputation > 0)
            runRep(item.Faction, RepCPLevel.First(x => x.Key == item.RequiredReputation).Value);
        Farm.Experience(item.Level);
        Farm.Gold(item.Cost);
    }

    private void runRep(string faction, int rank)
    {
        faction = faction.Replace(" ", "");
        Type farmClass = Farm.GetType();
        MethodInfo theMethod = farmClass.GetMethod(faction + "REP");
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

    private Dictionary<int, int> RepCPLevel = new()
    {
        { 0, 1 },
        { 900, 2 },
        { 3600, 3 },
        { 10000, 4 },
        { 22500, 5 },
        { 44100, 6 },
        { 78400, 7 },
        { 129600, 8 },
        { 202500, 9 },
        { 302500, 10 }
    };

    /// <summary>
    /// The list of ScriptOptions for any merge script.
    /// </summary>
    public List<IOption> MergeOptions = new()
    {
        new Option<mergeOptionsEnum>("mode", "Select the mode to use", "Regardless of the mode you pick, the bot wont (attempt to) buy Legend-only items if you're not a Legend.\n" +
                                                                     "Select the Mode Explanation item to get more information", mergeOptionsEnum.all),
        new Option<string>("blank", " ", "", ""),
        new Option<string>(" ", "Mode Explanation [all]", "Mode [all]: \t\tYou get all the items from shop, even if non-AC ones if any.", "click here"),
        new Option<string>(" ", "Mode Explanation [acOnly]", "Mode [acOnly]: \tYou get all the AC tagged items from the shop.", "click here"),
        new Option<string>(" ", "Mode Explanation [mergeMats]", "Mode [mergeMats]: \tYou dont buy any items but instead get the materials to buy them yourself, this way you can choose.", "click here"),
        new Option<string>(" ", "Mode Explanation [select]", "Mode [select]: \tYou are able to select what items you get and which ones you dont in the Select Category below.", "click here"),
        new Option<string>("blank", " ", "", ""),
    };

    /// <summary>
    /// The name of ScriptOptions for any merge script.
    /// </summary>
    public string OptionsStorage = "MergeOptionStorage";

    private enum mergeOptionsEnum
    {
        all = 0,
        acOnly = 1,
        mergeMats = 2,
        select = 3
    };

    #endregion

    #region Kill
#nullable enable

    /// <summary>
    /// Joins a map, jump & set the spawn point and kills the specified monster with the best available race gear
    /// </summary>
    /// <param name="map">Map to join</param>
    /// <param name="cell">Cell to jump to</param>
    /// <param name="pad">Pad to jump to</param>
    /// <param name="monster">Name of the monster to kill</param>
    /// <param name="item">Item to kill the monster for, if null will just kill the monster 1 time</param>
    /// <param name="quant">Desired quantity of the item</param>
    /// <param name="isTemp">Whether the item is temporary</param>
    /// <param name="log">Whether it will log that it is killing the monster</param>
    public void BoostKillMonster(string map, string cell, string pad, string monster, string item = "", int quant = 1, bool isTemp = true, bool log = true, bool publicRoom = false)
    {
        if (item != "" && Core.CheckInventory(item, quant))
            return;

        Core.Join(map, cell, pad, publicRoom: publicRoom);

        _RaceGear(monster);
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
    public void BoostKillMonster(string map, string cell, string pad, int monsterID, string item = "", int quant = 1, bool isTemp = true, bool log = true, bool publicRoom = false)
    {
        if (item != "" && Core.CheckInventory(item, quant))
            return;

        Core.Join(map, cell, pad, publicRoom: publicRoom);

        _RaceGear(monsterID);

        Core.KillMonster(map, cell, pad, monsterID, item, quant, isTemp, log, publicRoom);

        GearStore(true);
    }

    /// <summary>
    /// Joins a map and hunt for the monster and kills the specified monster with the best available race gear
    /// </summary>
    /// </summary>
    /// <param name="map">Map to join</param>
    /// <param name="monster">Name of the monster to kill</param>
    /// <param name="item">Item to hunt the monster for, if null will just hunt & kill the monster 1 time</param>
    /// <param name="quant">Desired quantity of the item</param>
    /// <param name="isTemp">Whether the item is temporary</param>
    public void BoostHuntMonster(string map, string monster, string item = "", int quant = 1, bool isTemp = true, bool log = true, bool publicRoom = false)
    {
        if (item != "" && Core.CheckInventory(item, quant))
            return;

        Core.Join(map, publicRoom: publicRoom);

        _RaceGear(monster);

        Core.HuntMonster(map, monster, item, quant, isTemp, log, publicRoom);

        GearStore(true);
    }

    /// <summary>
    /// Joins a map, jump & set the spawn point and kills the specified monster with the best available race gear. But also listens for Counter Attacks
    /// </summary>
    /// <param name="map">Map to join</param>
    /// <param name="cell">Cell to jump to</param>
    /// <param name="pad">Pad to jump to</param>
    /// <param name="monster">Name of the monster to kill</param>
    /// <param name="item">Item to kill the monster for, if null will just kill the monster 1 time</param>
    /// <param name="quant">Desired quantity of the item</param>
    /// <param name="isTemp">Whether the item is temporary</param>
    /// <param name="log">Whether it will log that it is killing the monster</param>
    public void KillUltra(string map, string cell, string pad, string monster, string? item = null, int quant = 1, bool isTemp = true, bool log = true, bool publicRoom = true, bool forAuto = false)
    {
        if (item != null && Core.CheckInventory(item, quant))
            return;
        if (item != null && !isTemp)
            Core.AddDrop(item);

        Core.Join(map, cell, pad, publicRoom: publicRoom);
        if (!forAuto)
            _RaceGear(monster);
        Core.Jump(cell, pad);

        if (item == null)
        {
            if (log)
                Core.Logger($"Killing Ultra-Boss {monster}");
            bool ded = false;
            Bot.Events.MonsterKilled += b => ded = true;
            while (!Bot.ShouldExit && !ded)
                if (!Bot.Combat.StopAttacking)
                    Bot.Combat.Attack(monster);
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
    /// <param name="ClassName">Name of the class you want it to rank up</param>
    public void rankUpClass(string ClassName, bool GearRestore = true)
    {
        Bot.Wait.ForPickup(ClassName);

        if (!Core.CheckInventory(ClassName))
            return;
        InventoryItem? itemInv = Bot.Inventory.Items.Find(i => i.Name.ToLower().Trim() == ClassName.ToLower().Trim() && i.Category == ItemCategory.Class);
        if (itemInv == null && !Bot.Inventory.TryGetItem("ClassName", out itemInv))
        {
            Core.Logger($"Cant level up \"{ClassName}\" because you do not own it.", messageBox: true);
            return;
        }
        if (itemInv == null)
            return;
        if (itemInv.Quantity == 302500)
        {
            Core.Logger($"\"{itemInv.Name}\" is already Rank 10");
            return;
        }

        if (GearRestore)
            GearStore();

        SmartEnhance(ClassName);
        string[]? CPBoost = BestGear(GearBoost.cp);
        EnhanceItem(CPBoost, CurrentClassEnh(), CurrentCapeSpecial(), CurrentHelmSpecial(), CurrentWeaponSpecial());
        Core.Equip(CPBoost);

        Farm.IcestormArena(Bot.Player.Level, true);
        Core.Logger($"\"{itemInv.Name}\" is now Rank 10");

        if (GearRestore)
            GearStore(true);
    }

    /// <summary>
    /// Equips the best gear available in a player's inventory/bank by checking what item has the highest boost value of the given type. Also works with damage stacking for monsters with a Race
    /// </summary>
    /// <param name="BoostType">Type "GearBoost." and then the boost of your choice in order to determine and equip the best available boosting gear</param>
    /// <param name="EquipItem">To Equip the found item(s) or not</param>
    public string[] BestGear(GearBoost BoostType, bool EquipItem = true)
    {
        if (Core.CBOBool("DisableBestGear", out bool _DisableBestGear))
            if (_DisableBestGear)
                return new string[0];

        if (BoostType == GearBoost.None)
            BoostType = GearBoost.dmgAll;
        if (LastBoostType == BoostType)
            return LastBestGear ?? new[] { "" };
        LastBoostType = BoostType;

        Core.Logger("Searching for the best available gear " + (isRacial() ? "against " : "for ") + BoostType.ToString());

        List<InventoryItem> BankInvData = Bot.Inventory.Items.Concat(Bot.Bank.Items).ToList();
        float TotalBoostValue = 0F;
        string[] ArrayOutput = new string[0];

        if (!isRacial())
        {
            Dictionary<string, float> BoostedGear = new();

            foreach (InventoryItem Item in BankInvData)
                if (Item.Meta != null && Item.Meta.Contains(BoostType.ToString()))
                    BoostedGear.Add(Item.Name, getBoostFloat(Item, BoostType.ToString()));

            if (BoostedGear.Count != 0)
            {
                TotalBoostValue = BoostedGear.MaxBy(x => x.Value).Value;
                ArrayOutput = new[] { BoostedGear.MaxBy(x => x.Value).Key };

                Dictionary<string, float> BoostedGearMultiple = BoostedGear.Where(x => x.Value == TotalBoostValue).ToDictionary(x => x.Key, y => y.Value);
                if (BoostedGearMultiple.Count() > 1)
                {
                    if (BoostedGearMultiple.Keys.Any(x => BankInvData.Where(i => i.Equipped).Select(x => x.Name).Contains(x)))
                        ArrayOutput = new[] { BoostedGearMultiple.First(x => BankInvData.Where(i => i.Equipped).Select(x => x.Name).Contains(x.Key)).Key };
                    else foreach (KeyValuePair<string, float> Gear in BoostedGearMultiple)
                        {
                            InventoryItem Item = BankInvData.First(x => x.Name == Gear.Key && x.Meta.Contains(BoostType.ToString()));
                            InventoryItem equippedItem = BankInvData.First(x => x.Equipped && x.ItemGroup == Item.ItemGroup);
                            if (Item != null && equippedItem != null
                                && Bot.Flash.GetGameObject<int>($"world.invTree.{Item.ID}.EnhID") == Bot.Flash.GetGameObject<int>($"world.invTree.{equippedItem.ID}.EnhID"))
                            {
                                ArrayOutput = new[] { Item.Name };
                                break;
                            }
                        }
                }

                if (EquipItem)
                {
                    //foreach (string Item in ArrayOutput)
                    //{
                    //    InventoryItem invItem = BankInvData.First(x => x.Name == Item);
                    //    if (!invItem.Equipped)
                    //        continue;

                    //    if (invItem.ItemGroup == "Weapon")
                    //    {
                    //        List<InventoryItem> theList = new();
                    //        theList.AddRange(Bot.Inventory.Items.Where(x => x.Name != Item && x.ItemGroup == "Weapon" && x.EnhancementLevel > 0 && Core.IsMember ? true : !x.Upgrade));
                    //        if (theList.Count == 0)
                    //            theList.AddRange(Bot.Bank.Items.Where(x => x.Name != Item && x.ItemGroup == "Weapon" && x.EnhancementLevel > 0 && Core.IsMember ? true : !x.Upgrade));

                    //        if (theList.Count != 0)
                    //            Core.Equip(theList.First().Name);
                    //        else
                    //        {
                    //            Core.BuyItem(Bot.Map.Name, 299, "Battle Oracle Battlestaff");
                    //            Core.Equip("Battle Oracle Battlestaff");
                    //        }
                    //    }
                    //    else
                    //    {
                    //        Core.JumpWait();
                    //        Bot.Send.Packet($"%xt%zm%unequipItem%{Bot.Map.RoomID}%{invItem.ID}%");
                    //    }
                    //}
                    Core.Equip(ArrayOutput);
                }

                LastBestGear = ArrayOutput;
            }
        }
        else
        {
            List<BestGearData> BestGearData = new();
            List<InventoryItem> AllRaceItems = new();
            List<InventoryItem> AllDMGallItems = new();

            foreach (InventoryItem Item in BankInvData)
            {
                if (!String.IsNullOrEmpty(Item.Meta))
                {
                    if (Item.Meta.Contains(BoostType.ToString()))
                        AllRaceItems.Add(Item);
                    if (Item.Meta.Contains("dmgAll"))
                        AllDMGallItems.Add(Item);
                }
            }

            if (AllRaceItems.Count != 0)
                foreach (InventoryItem iRace in AllRaceItems)
                {
                    float iRaceBoostFloat = getBoostFloat(iRace, BoostType.ToString());
                    BestGearData.Add(new(iRace.Name, "", iRaceBoostFloat));

                    foreach (InventoryItem iAll in AllDMGallItems)
                    {
                        if (iRace.ID == iAll.ID || iRace.Category == iAll.Category || iRace.ItemGroup == iAll.ItemGroup)
                            continue;

                        float iAllBoostFloat = getBoostFloat(iAll, "dmgAll");
                        BestGearData.Add(new(iRace.Name, iAll.Name, (iRaceBoostFloat * iAllBoostFloat)));
                        if (!BestGearData.Any(x => x.iDMGall == iAll.Name && x.iRace == ""))
                            BestGearData.Add(new("", iAll.Name, iAllBoostFloat));
                    }
                }
            else foreach (InventoryItem iAll in AllDMGallItems)
                    BestGearData.Add(new("", iAll.Name, getBoostFloat(iAll, "dmgAll")));

            BestGearData FinalCombo = BestGearData.MaxBy(x => x.BoostValue) ?? new("", "", 0);
            TotalBoostValue = FinalCombo.BoostValue;
            string BestRace = FinalCombo.iRace ?? "";
            string BestDMGAll = FinalCombo.iDMGall ?? "";
            List<string> ListOutput = new();

            //List<BestGearData> BestBestGearData = BestGearData.Where(x => x.BoostValue == TotalBoostValue).ToList();
            //if (BestBestGearData.Count() > 1)
            //{
            //    if (BestBestGearData.Any(x => BankInvData.Where(i => i.Equipped).Select(x => x.Name).Contains(x.iRace)
            //     || BestBestGearData.Any(x => BankInvData.Where(i => i.Equipped).Select(x => x.Name).Contains(x.iDMGall))))
            //        ArrayOutput = new[] { BestBestGearData.First(x => BankInvData.Where(i => i.Equipped).Select(x => x.Name).Contains(x.Key)).Key };
            //    foreach (BestGearData Gear in BestGearData.Where(x => x.BoostValue == TotalBoostValue))
            //    {
            //        InventoryItem Item = BankInvData.First(x => x.Name == Gear.iRace || x.Name == Gear.iDMGall);
            //        InventoryItem equippedWeapon = BankInvData.First(x => x.Equipped == true && x.ItemGroup == "Weapon");
            //        if (Item != null && equippedWeapon != null
            //            && Bot.Flash.GetGameObject<int>($"world.invTree.{Item.ID}.EnhID") == Bot.Flash.GetGameObject<int>($"world.invTree.{equippedWeapon.ID}.EnhID"))
            //        {
            //            ArrayOutput = new[] { Item.Name };
            //            break;
            //        }
            //    }
            //}

            if (!String.IsNullOrEmpty(BestRace))
                ListOutput.Add(BestRace);
            if (!String.IsNullOrEmpty(BestDMGAll))
                ListOutput.Add(BestDMGAll);
            ArrayOutput = ListOutput.ToArray();

            if (EquipItem)
            {
                //foreach (string Item in ArrayOutput)
                //{
                //    InventoryItem invItem = BankInvData.First(x => x.Name == Item);
                //    if (!invItem.Equipped)
                //        continue;

                //    if (invItem.ItemGroup == "Weapon")
                //    {
                //        List<InventoryItem> theList = new();
                //        theList.AddRange(Bot.Inventory.Items.Where(x => x.Name != Item && x.ItemGroup == "Weapon" && x.EnhancementLevel > 0 && Core.IsMember ? true : !x.Upgrade));
                //        if (theList.Count == 0)
                //            theList.AddRange(Bot.Bank.Items.Where(x => x.Name != Item && x.ItemGroup == "Weapon" && x.EnhancementLevel > 0 && Core.IsMember ? true : !x.Upgrade));

                //        if (theList.Count != 0)
                //            Core.Equip(theList.First().Name);
                //        else
                //        {
                //            Core.BuyItem(Bot.Map.Name, 299, "Battle Oracle Battlestaff");
                //            Core.Equip("Battle Oracle Battlestaff");
                //        }
                //    }
                //    else
                //    {
                //        Core.JumpWait();
                //        Bot.Send.Packet($"%xt%zm%unequipItem%{Bot.Map.RoomID}%{invItem.ID}%");
                //    }
                //}
                Core.Equip(ArrayOutput);
            }
            LastBestGear = ArrayOutput;
        }

        if (ArrayOutput.Length == 0)
        {
            Core.Logger("Best gear " + (isRacial() ? "against " : "for ") + $"{BoostType.ToString()} wasnt found!");
            return new[] { "" };
        }
        else if (ArrayOutput.Length == 1)
            Core.Logger("Best gear " + (isRacial() ? "against " : "for ") + $"{BoostType.ToString()} found: {ArrayOutput[0]} ({(TotalBoostValue - 1).ToString("+0.##%")})");
        else if (ArrayOutput.Length == 2)
            Core.Logger($"Best gear against {BoostType.ToString()} found: {ArrayOutput[0]} + {ArrayOutput[1]} ({(TotalBoostValue - 1).ToString("+0.##%")})");
        return ArrayOutput;

        bool isRacial()
        {
            return RaceBoosts.Contains(BoostType);
        }

        float getBoostFloat(InventoryItem Item, string BoostType)
        {
            string CorrectData = Item.Meta.Split(',').ToList().First(i => i.Contains(BoostType));
            return float.Parse(CorrectData.Replace($"{BoostType}:", ""), CultureInfo.InvariantCulture.NumberFormat);
        }
    }
    private GearBoost[] RaceBoosts =
    {
        GearBoost.Chaos,
        GearBoost.Dragonkin,
        GearBoost.Drakath,
        GearBoost.Elemental,
        GearBoost.Human,
        GearBoost.Orc,
        GearBoost.Undead
    };
    private GearBoost LastBoostType = GearBoost.None;
    private string[] LastBestGear = { };
    private class BestGearData
    {
        public string iRace { get; set; }
        public string iDMGall { get; set; }
        public float BoostValue { get; set; }
        public BestGearData(string iRace, string iDMGall, float BoostValue)
        {
            this.iRace = iRace;
            this.iDMGall = iDMGall;
            this.BoostValue = BoostValue;
        }
    }

    /// <summary>
    /// Stores the gear a player has so that it can later restore these
    /// </summary>
    /// <param name="Restore">Set true to restore previously stored gear</param>
    public void GearStore(bool Restore = false)
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
        else
        {
            Core.Equip(ReEquippedItems.ToArray());
            EnhanceEquipped(ReEnhanceAfter, ReCEnhanceAfter, ReHEnhanceAfter, ReWEnhanceAfter);
        }
    }
    private List<string> ReEquippedItems = new List<string>();
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
        else MonsterRace = Bot.Monsters.CurrentMonsters.First().Race ?? "";

        if (MonsterRace == null || MonsterRace == "")
            return;

        string[] _BestGear = BestGear((GearBoost)Enum.Parse(typeof(GearBoost), MonsterRace));
        if (_BestGear.Length == 0)
            return;
        EnhanceItem(_BestGear, CurrentClassEnh(), CurrentCapeSpecial(), CurrentHelmSpecial(), CurrentWeaponSpecial());
        Core.Equip(_BestGear);
        EnhanceEquipped(CurrentClassEnh(), CurrentCapeSpecial(), CurrentHelmSpecial(), CurrentWeaponSpecial());
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

        string[] _BestGear = BestGear((GearBoost)Enum.Parse(typeof(GearBoost), MonsterRace));
        if (_BestGear.Length == 0)
            return;
        EnhanceItem(_BestGear, CurrentClassEnh(), CurrentCapeSpecial(), CurrentHelmSpecial(), CurrentWeaponSpecial());
        Core.Equip(_BestGear);
        EnhanceEquipped(CurrentClassEnh(), CurrentCapeSpecial(), CurrentHelmSpecial(), CurrentWeaponSpecial());
        Core.Join(Map);
    }

    #endregion

    #region Enhancement

    /// <summary>
    /// Enhances your currently equipped gear
    /// </summary>
    /// <param name="Type">Example: EnhancementType.Lucky , replace Lucky with whatever enhancement you want to have it use</param>
    /// <param name="Special">Example: WeaponSpecial.Spiral_Carve , replace Spiral_Carve with whatever weapon special you want to have it use</param>
    public void EnhanceEquipped(EnhancementType type, CapeSpecial cSpecial = CapeSpecial.None, HelmSpecial hSpecial = HelmSpecial.None, WeaponSpecial wSpecial = WeaponSpecial.None)
    {
        if (Core.CBOBool("DisableAutoEnhance", out bool _disableAutoEnhance) && _disableAutoEnhance)
            return;

        List<InventoryItem> EquippedItems = Bot.Inventory.Items.FindAll(i => i.Equipped == true && EnhanceableCatagories.Contains(i.Category));
        AutoEnhance(EquippedItems, type, cSpecial, hSpecial, wSpecial);
    }

    /// <summary>
    /// Enhances a selected item
    /// </summary>
    /// <param name="ItemName">Name of the item you want to enhance</param>
    /// <param name="Type">Example: EnhancementType.Lucky , replace Lucky with whatever enhancement you want to have it use</param>
    /// <param name="Special">Example: WeaponSpecial.Spiral_Carve , replace Spiral_Carve with whatever weapon special you want to have it use</param>
    public void EnhanceItem(string item, EnhancementType type, CapeSpecial cSpecial = CapeSpecial.None, HelmSpecial hSpecial = HelmSpecial.None, WeaponSpecial wSpecial = WeaponSpecial.None)
    {
        if (string.IsNullOrEmpty(item) || (Core.CBOBool("DisableAutoEnhance", out bool _disableAutoEnhance) && _disableAutoEnhance))
            return;

        if (!Core.CheckInventory(item))
        {
            Core.Logger($"Enhancement Failed: Could not find \"{item}\"");
            return;
        }

        InventoryItem? SelectedItem = Bot.Inventory.Items.Find(i => i.Name == item && EnhanceableCatagories.Contains(i.Category)); ;
        if (SelectedItem == null)
        {
            if (Bot.Inventory.Items.Any(i => i.Name == item))
                Core.Logger($"Enhancement Failed: {item} cannot be enhanced");
            return;
        }

        AutoEnhance(new() { SelectedItem }, type, cSpecial, hSpecial, wSpecial);
    }

    /// <summary>
    /// Enhances multiple selected items
    /// </summary>
    /// <param name="ItemName">Names of the items you want to enhance (Case-Sensitive)</param>
    /// <param name="Type">Example: EnhancementType.Lucky , replace Lucky with whatever enhancement you want to have it use</param>
    /// <param name="Special">Example: WeaponSpecial.Spiral_Carve , replace Spiral_Carve with whatever weapon special you want to have it use</param>
    public void EnhanceItem(string[] items, EnhancementType type, CapeSpecial cSpecial = CapeSpecial.None, HelmSpecial hSpecial = HelmSpecial.None, WeaponSpecial wSpecial = WeaponSpecial.None)
    {
        if (items.Count() == 0 || (Core.CBOBool("DisableAutoEnhance", out bool _disableAutoEnhance) && _disableAutoEnhance))
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
        if (SelectedItems.Count != items.Count())
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

        AutoEnhance(SelectedItems, type, cSpecial, hSpecial, wSpecial);
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
        return (CapeSpecial)EquippedCape.EnhancementPatternID;
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
        return (HelmSpecial)EquippedHelm.EnhancementPatternID;
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
        return (WeaponSpecial)Bot.Flash.GetGameObject<int>($"world.invTree.{EquippedWeapon.ID}.ProcID");
    }

    private static ItemCategory[] EnhanceableCatagories =
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
    private ItemCategory[] WeaponCatagories = EnhanceableCatagories[..12];

    private void AutoEnhance(List<InventoryItem> ItemList, EnhancementType type, CapeSpecial cSpecial, HelmSpecial hSpecial, WeaponSpecial wSpecial)
    {
        // Empty check
        if (ItemList.Count == 0)
        {
            Core.Logger("Enhancement Failed: ItemList is empty");
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
        if (hSpecial.ToString() != "None" && ItemList.Any(i => i.Category == ItemCategory.Helm))
        {
            Core.DebugLogger(this);
            helm = ItemList.Find(i => i.Category == ItemCategory.Helm);

            // Removing helm from the list because it needs to be enhanced seperately
            if (helm != null)
                ItemList.Remove(helm);
        }

        // Defining weapon
        InventoryItem? weapon = null;
        if (wSpecial.ToString() != "None" && ItemList.Any(i => i.ItemGroup == "Weapon"))
        {
            Core.DebugLogger(this);
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
            }

            // Enhancing the remaining items
            foreach (InventoryItem item in ItemList)
            {
                _AutoEnhance(item, shopID);
                Core.DebugLogger(this);
            }
        }

        Core.DebugLogger(this);
        // Enhancing the cape with the cape special
        if (cape != null)
        {
            bool canEnhance = true;

            switch (cSpecial)
            {
                case CapeSpecial.Forge:
                    if (!uForgeCape())
                    {
                        Core.Logger("Enhancement Failed: You did not unlock the Forge (Cape) Enhancement yet");
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

                    void Fail()
                    {
                        Core.Logger($"Enhancement Failed: You did not unlock the {cSpecial} Enhancement yet");
                        canEnhance = false;
                    }
            }

            if (canEnhance)
                _AutoEnhance(cape, 2143, ((int)cSpecial > 0) ? "forge" : null);
            else skipCounter++;
        }
        Core.DebugLogger(this);

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
                case HelmSpecial.Anima:
                    if (!uAnima())
                        Fail();
                    break;
                case HelmSpecial.Pneuma:
                    if (!uPneuma())
                        Fail();
                    break;

                    void Fail()
                    {
                        Core.Logger($"Enhancement Failed: You did not unlock the {hSpecial} Enhancement yet");
                        canEnhance = false;
                    }
            }

            if (canEnhance)
                _AutoEnhance(helm, 2164, ((int)hSpecial > 0) ? "forge" : null);
            else skipCounter++;
        }
        Core.DebugLogger(this);

        // Enhancing the weapon with the weapon special
        if (weapon != null)
        {
            Core.DebugLogger(this);
            int shopID = 0;
            bool canEnhance = true;

            if ((int)wSpecial <= 6)
            {
                Core.DebugLogger(this);
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
                }
            }
            else
            {
                Core.DebugLogger(this);
                switch (wSpecial)
                {
                    case WeaponSpecial.Forge:
                        if (!uForgeWeapon())
                        {
                            Core.Logger("Enhancement Failed: You did not unlock the Forge (Weapon) Enhancement yet");
                            canEnhance = false;
                        }
                        break;
                    case WeaponSpecial.Lacerate:
                        if (!uLaceratey())
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
                            Core.Logger("Enhancement Failed: You did not unlock the Arcana's Concerto Enhancement yet");
                            canEnhance = false;
                        }
                        break;
                    case WeaponSpecial.Elysium:
                        if (!uElysium())
                            Fail();
                        break;
                    case WeaponSpecial.Acheron:
                        if (!uArchon())
                            Fail();
                        break;

                        void Fail()
                        {
                            Core.Logger($"Enhancement Failed: You did not unlock the {wSpecial} Enhancement yet");
                            canEnhance = false;
                        }
                }

                Core.DebugLogger(this);
                shopID = 2142;
            }

            if (canEnhance)
                _AutoEnhance(weapon, shopID, ((int)wSpecial > 6) ? "forge" : null);
            else skipCounter++;
        }

        if (skipCounter > 0)
            Core.Logger($"Skipped enhancement for {skipCounter} item{(skipCounter > 1 ? 's' : null)}");

        void _AutoEnhance(InventoryItem item, int shopID, string? map = null)
        {
            bool specialOnCape = item.Category == ItemCategory.Cape && cSpecial != CapeSpecial.None;
            bool specialOnWeapon = item.ItemGroup == "Weapon" && wSpecial.ToString() != "None";
            List<ShopItem> shopItems = Core.GetShopItems(map != null ? map : Bot.Map.Name, shopID);

            // Shopdata complete check
            if (!shopItems.Any(x => x.Category == ItemCategory.Enhancement) || shopItems.Count == 0)
            {
                Core.Logger($"Enhancement Failed: Couldn't find enhancements in shop {shopID}");
                return;
            }

            // Checking if the item is already optimally enhanced
            Core.DebugLogger(this, item.Name);
            if (Bot.Player.Level == item.EnhancementLevel)
            {
                Core.DebugLogger(this);
                if (specialOnCape)
                {
                    if ((int)cSpecial == getEnhPatternID(item))
                    {
                        skipCounter++;
                        return;
                    }
                }
                else if (specialOnWeapon)
                {
                    Core.DebugLogger(this);
                    if (((int)wSpecial <= 6 ? (int)type : 10) == getEnhPatternID(item) && ((int)wSpecial == getProcID(item) || ((int)wSpecial == 99 && getProcID(item) == 0)))
                    {
                        Core.DebugLogger(this);
                        skipCounter++;
                        return;
                    }
                }
                else if ((int)type == getEnhPatternID(item))
                {
                    skipCounter++;
                    return;
                }
            }

            // Logging
            if (specialOnCape)
                Core.Logger($"Searching Enhancement: \tForge/{cSpecial.ToString().Replace("_", " ")} - \"{item.Name}\"");
            else if (specialOnWeapon)
                Core.Logger($"Searching Enhancement: \t{((int)wSpecial <= 6 ? type : "Forge")}/{wSpecial.ToString().Replace("_", " ")} - \"{item.Name}\"");
            else
                Core.Logger($"Searching Enhancement: \t{type} - \"{item.Name}\"");

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
                Core.Logger($"Enhancement Failed: availableEnh is empty");
                return;
            }
            else if (availableEnh.Count == 1)
                bestEnhancement = availableEnh.First();
            else
            {
                // Sorting by level (ascending)
                List<ShopItem> sortedList = availableEnh.OrderBy(x => x.Level).ToList();

                // Grabbing the two best enhancements
                List<ShopItem> bestTwoEnhancements = new();
                if (sortedList.Count >= 4)
                    bestTwoEnhancements = sortedList.Skip(sortedList.Count - 2).OrderBy(x => x.Level).ToList();
                else if (sortedList.Count == 3)
                    bestTwoEnhancements = sortedList.Skip(sortedList.Count - 1).OrderBy(x => x.Level).ToList();
                else if (sortedList.Count == 2)
                    bestTwoEnhancements = sortedList.Skip(sortedList.Count - 0).OrderBy(x => x.Level).ToList();
                else
                {
                    Core.Logger($"Enhancement Failed: sortedList {(sortedList.Count > 0 ? $"has a count of {sortedList.Count}" : "is empty")}");
                    return;
                }

                if (bestTwoEnhancements.Count != 2)
                {
                    Core.Logger($"Enhancement Failed: bestTwoEnhancements {(bestTwoEnhancements.Count > 0 ? $"has a count of {sortedList.Count}" : "is empty")}");
                    return;
                }

                // Getting the best enhancement out of the two
                bestEnhancement =
                   bestTwoEnhancements.First().Level == bestTwoEnhancements.Last().Level ?
                       bestTwoEnhancements.First(x => Core.IsMember ? x.Upgrade : !x.Upgrade) : bestTwoEnhancements.Last();
            }

            // Null check
            if (bestEnhancement == null)
            {
                Core.Logger($"Enhancement Failed: Could not find the best enhancement for \"{item.Name}\"");
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

            Bot.Sleep(Core.ActionDelay);
        }
    }

    private int getProcID(InventoryItem item) => item == null ? 0 : Bot.Flash.GetGameObject<int>($"world.invTree.{item.ID}.ProcID");
    private int getEnhPatternID(InventoryItem item) => item == null ? 0 : Bot.Flash.GetGameObject<int>($"world.invTree.{item.ID}.EnhPatternID");

    private bool uForgeWeapon() => Core.isCompletedBefore(8738);
    private bool uLaceratey() => Core.isCompletedBefore(8739);
    private bool uSmite() => Core.isCompletedBefore(8740);
    private bool uValiance() => Core.isCompletedBefore(8741);
    private bool uArcanasConcerto() => Core.isCompletedBefore(8742);
    private bool uAbsolution() => Core.isCompletedBefore(8743);
    private bool uVainglory() => Core.isCompletedBefore(8744);
    private bool uAvarice() => Core.isCompletedBefore(8745);
    private bool uForgeCape() => Core.isCompletedBefore(8758);
    private bool uElysium() => Core.isCompletedBefore(8821);
    private bool uArchon() => Core.isCompletedBefore(8820);
    private bool uPenitence() => Core.isCompletedBefore(8822);
    private bool uLament() => Core.isCompletedBefore(8823);
    private bool uVim() => Core.isCompletedBefore(8824);
    private bool uExamen() => Core.isCompletedBefore(8825);
    private bool uAnima() => Core.isCompletedBefore(8826);
    private bool uPneuma() => Core.isCompletedBefore(8827);

    #endregion

    #region SmartEnhance

    /// <summary>
    /// Automatically finds the best Enhancement for the given class and enhances all equipped gear with it too
    /// </summary>
    /// <param name="Class">Name of the class you wish to enhance</param>
    public void SmartEnhance(string Class)
    {
        if (!Core.CheckInventory(Class))
        {
            Core.Logger($"SmartEnhance Failed: Class {Class} was not found in inventory");
            return;
        }

        InventoryItem? SelectedClass = Bot.Inventory.Items.Where(i => i.Name.ToLower() == Class.ToLower() && i.Category == ItemCategory.Class).FirstOrDefault();

        if (SelectedClass == null)
        {
            Core.Logger($"SmartEnhance Failed: Class {Class} was not found in inventory");
            return;
        }

        EnhancementType? type = null;
        CapeSpecial cSpecial = CapeSpecial.None;
        HelmSpecial hSpecial = HelmSpecial.None;
        WeaponSpecial wSpecial = WeaponSpecial.None;

        #region Forge Enhancement Library
        switch (SelectedClass.Name.ToLower())
        {
            #region Lucky - Forge - Spiral Carve
            case "corrupted chronomancer":
            case "underworld chronomancer":
            case "timekeeper":
            case "timekiller":
            case "eternal chronomancer":
            case "immortal chronomancer":
            case "dark metal necro":
            case "great thief":
            case "legion doomknight":
            case "void highlord":
            case "chaos slayer":
            case "chaos slayer berserker":
            case "chaos slayer cleric":
            case "chaos slayer mystic":
            case "chaos slayer thief":
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
            case "shadowwalker of time":
            case "shadowstalker of time":
            case "shadowweaver of time":
            case "yami no ronin":
            case "legendary elemental warrior":
            case "ultra elemental warrior":
            case "chaos avenger":
                if (!uForgeCape())
                    goto default;

                type = EnhancementType.Lucky;
                cSpecial = CapeSpecial.Forge;
                wSpecial = WeaponSpecial.Mana_Vamp;
                break;
            #endregion

            #region Wizard - Forge - Spiral Carve
            case "legion revenant":
            case "legion revenant (ioda)":
            case "lightcaster":
                if (!uForgeCape())
                    goto default;

                type = EnhancementType.Wizard;
                cSpecial = CapeSpecial.Forge;
                wSpecial = WeaponSpecial.Spiral_Carve;
                break;
            #endregion

            #region Wizard - Forge - Awe Blast
            case "infinity knight":
                if (!uForgeCape())
                    goto default;

                type = EnhancementType.Wizard;
                cSpecial = CapeSpecial.Forge;
                wSpecial = WeaponSpecial.Awe_Blast;
                break;
            #endregion

            #region Wizard - Forge - Health Vamp
            case "shaman":
                if (!uForgeCape())
                    goto default;

                type = EnhancementType.Wizard;
                cSpecial = CapeSpecial.Forge;
                wSpecial = WeaponSpecial.Health_Vamp;
                break;
            #endregion

            #region Healer - Forge - Health Vamp
            case "dragon of time":
                if (!uForgeCape())
                    goto default;

                type = EnhancementType.Healer;
                cSpecial = CapeSpecial.Forge;
                wSpecial = WeaponSpecial.Health_Vamp;
                break;
            #endregion

            #region Lucky - Vainglory - Valiance
            case "archfiend":
                if (!uVainglory() || !uValiance())
                    goto default;

                type = EnhancementType.Lucky;
                cSpecial = CapeSpecial.Vainglory;
                wSpecial = WeaponSpecial.Valiance;
                break;
            #endregion

            #endregion
            #region Awe Enhancement Library
            default:
                switch (SelectedClass.Name.ToLower())
                {
                    #region Lucky - None - Spiral Carve
                    case "abyssal angel":
                    case "abyssal angel's shadow":
                    case "archpaladin":
                    case "artifact hunter":
                    case "assassin":
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
                    case "classic legion doomknight":
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
                    case "legion doomknight":
                    case "legion evolved dark caster":
                    case "legion swordmaster assassin":
                    case "leprechaun":
                    case "lycan":
                    case "master ranger":
                    case "mechajouster":
                    case "necromancer":
                    case "ninja":
                    case "ninja warrior":
                    case "not a mod":
                    case "overworld chronomancer":
                    case "pinkomancer":
                    case "prismatic clawsuit":
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
                    case "stonecrusher":
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
                        type = EnhancementType.Lucky;
                        wSpecial = WeaponSpecial.Spiral_Carve;
                        break;
                    #endregion

                    #region Lucky - None - Mana Vamp
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
                    case "dark ultra omninight":
                    case "doomknight overlord":
                    case "dragonslayer general":
                    case "drakel warlord":
                    case "glacial berserker test":
                    case "heroic naval commander":
                    case "legendary elemental warrior":
                    case "horc evader":
                    case "legendary hero":
                    case "legendary naval commander":
                    case "legion doomknight tester":
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
                    case "ultra omniknight":
                    case "void highlord tester":
                    case "warlord":
                    case "warrior":
                    case "warrior (rare)":
                    case "warriorscythe general":
                    case "yami no ronin":
                        type = EnhancementType.Lucky;
                        wSpecial = WeaponSpecial.Mana_Vamp;
                        break;
                    #endregion

                    #region Lucky - None - Awe Blast
                    case "arachnomancer":
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
                    case "nechronomancer":
                    case "necrotic chronomancer":
                    case "no class":
                    case "nu metal necro":
                    case "obsidian no class":
                    case "oracle":
                    case "protosartorium":
                    case "shadow dragon shinobi":
                    case "shadow ripper":
                    case "shadow rocker":
                    case "star captain":
                    case "troubador of love":
                    case "unchained rocker":
                    case "unchained rockstar":
                        type = EnhancementType.Lucky;
                        wSpecial = WeaponSpecial.Awe_Blast;
                        break;
                    #endregion

                    #region Lucky - None - Health Vamp
                    case "eternal inversionist":
                    case "archfiend":
                    case "barber":
                    case "classic dragonlord":
                    case "dragonslayer":
                    case "enchanted vampire lord":
                    case "enforcer":
                    case "flame dragon warrior":
                    case "royal vampire lord":
                    case "rustbucket":
                    case "sentinel":
                    case "vampire":
                    case "vampire lord":
                        type = EnhancementType.Lucky;
                        wSpecial = WeaponSpecial.Health_Vamp;
                        break;
                    #endregion

                    #region Wizard - None - Awe Blast
                    case "acolyte":
                    case "arcane dark caster":
                    case "battlemage":
                    case "battlemage of love":
                    case "blaze binder":
                    case "blood sorceress":
                    case "dark battlemage":
                    case "dark master of moglins":
                    case "dragon knight":
                    case "firelord summoner":
                    case "grim necromancer":
                    case "healer":
                    case "healer (rare)":
                    case "highseas commander":
                    case "infinity knight":
                    case "interstellar knight":
                    case "master of moglins":
                    case "mystical dark caster":
                    case "northlands monk":
                    case "royal battlemage":
                    case "timeless dark caster":
                    case "witch":
                        type = EnhancementType.Wizard;
                        wSpecial = WeaponSpecial.Awe_Blast;
                        break;
                    #endregion

                    #region Wizard - None - Spiral Carve
                    case "chrono dataknight":
                    case "chrono dragonknight":
                    case "cryomancer":
                    case "dark caster":
                    case "dark cryomancer":
                    case "dark lord":
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
                        type = EnhancementType.Wizard;
                        wSpecial = WeaponSpecial.Spiral_Carve;
                        break;
                    #endregion

                    #region Wizard - None - Health Vamp
                    case "daimon":
                    case "evolved shaman":
                    case "lightmage":
                    case "mindbreaker":
                    case "shaman":
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

                    #region Fighter - None - Awe Blast
                    case "deathknight":
                    case "frostval barbarian":
                        type = EnhancementType.Fighter;
                        wSpecial = WeaponSpecial.Awe_Blast;
                        break;
                    #endregion

                    #region Healer - None - Health Vamp
                    case "dragon of time":
                        type = EnhancementType.Healer;
                        wSpecial = WeaponSpecial.Health_Vamp;
                        break;
                    #endregion
                    default:
                        Core.Logger($"Class: \"{Class}\" is not found in the Smart Enhance Library, please report to Lord Exelot#9674", messageBox: true);
                        return;
                }
                break;
                #endregion
        }

        if (SelectedClass.EnhancementLevel == 0 && type != null)
            EnhanceItem(Class, (EnhancementType)type);
        Core.Equip(SelectedClass.Name);

        if (type == null)
            return;

        EnhanceEquipped((EnhancementType)type, (CapeSpecial)cSpecial, (HelmSpecial)hSpecial, (WeaponSpecial)wSpecial);
    }

    #endregion
}

public enum GearBoost
{
    None,

    cp,
    gold,
    rep,
    exp,

    dmgAll,
    Chaos,
    Dragonkin,
    Drakath,
    Elemental,
    Human,
    Orc,
    Undead
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

    Forge = 99, // Not really 99, but cant have 0 3 times
    Lacerate = 7,
    Smite = 8,
    Valiance = 9,
    Arcanas_Concerto = 10,
    Elysium = 12,
    Acheron = 11,
}

public enum HelmSpecial //Enhancement Pattern ID
{
    None = 0,
    Forge = 99, // Not really 99, but cant have 0 3 times
    Vim = 25,
    Examen = 26,
    Anima = 28,
    Pneuma = 27,
}