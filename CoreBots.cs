using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.DependencyInjection;
using Newtonsoft.Json;
using Skua.Core.Interfaces;
using Skua.Core.Models;
using Skua.Core.Models.Items;
using Skua.Core.Models.Monsters;
using Skua.Core.Models.Quests;
using Skua.Core.Models.Servers;
using Skua.Core.Models.Shops;
using Skua.Core.Models.Skills;
using Skua.Core.Options;
using Skua.Core.Utils;

public class CoreBots
{
    #region Declerations
    // [Can Change] Delay between common actions, 700 is the safe number
    public int ActionDelay { get; set; } = 700;
    // [Can Change] Delay used to get out of combat, 1600 is the safe number
    public int ExitCombatDelay { get; set; } = 1600;
    // [Can Change] Delay between jumping rooms after hunting a monster, increase if you think it is jumping too much
    public int HuntDelay { get; set; } = 1000;
    // [Can Change] How many tries to accept/complete the quest will be sent
    public int AcceptandCompleteTries { get; set; } = 20;
    // [Can Change] How many quests the bot should be able to have loaded at once
    public int LoadedQuestLimit { get; set; } = 150;
    // [Can Change] Whether the bots should also log in AQW's chat
    public bool LoggerInChat { get; set; } = true;
    // [Can Change] When enabled, no message boxes will be shown unless absolutely necessary
    public bool ForceOffMessageboxes { get; set; } = false;
    // [Can Change] Whether the bots will use private rooms
    public bool PrivateRooms { get; set; } = true;
    // [Can Change] What private room number the bot should use, if > 99999 it will pick a random room
    public int PrivateRoomNumber { get; set; } = 100000;
    // [Can Change] Use public rooms if the enemy is tough
    public bool PublicDifficult { get; set; } = false;
    // [Can Change] If StopLocations.Custom is selected, where to go
    public string CustomStopLocation { get; set; } = "whitemap";
    // [Can Change] Whether the player should rest after killing a monster
    public bool ShouldRest { get; set; } = false;
    // [Can Change] Whether the bot should attempt to clean your inventory by banking Misc. AC Items before starting the bot
    public bool BankMiscAC { get; set; } = false;
    // [Can Change] Whether you want anti lag features (lag killer, invisible monsters, set to 10 FPS)
    public bool AntiLag { get; set; } = true;
    // [Can Change] Name of your soloing class
    public string SoloClass { get; set; } = "Generic";
    // [Can Change] Mode of soloing class, if it has multiple. 
    public ClassUseMode SoloUseMode { get; set; } = ClassUseMode.Base;
    // [Can Change] Whether you wish to equip solo equipment
    public bool SoloGearOn { get; set; } = true;
    // [Can Change] Names of your soloing equipment
    public string[] SoloGear { get; set; } = { "Weapon", "Headpiece", "Cape" };
    // [Can Change] Name of your farming class
    public string FarmClass { get; set; } = "Generic";
    // [Can Change] Mode of farming class, if it has multiple. 
    public ClassUseMode FarmUseMode { get; set; } = ClassUseMode.Base;
    // [Can Change] Whether you wish to equip farm equipment
    public bool FarmGearOn { get; set; } = true;
    // [Can Change] Names of your farming equipment
    public string[] FarmGear { get; set; } = { "Weapon", "Headpiece", "Cape" };
    // [Can Change] Some Sagas use the hero alignment to give extra reputation, change to your desired rep (Alignment.Evil or Alignment.Good).
    public int HeroAlignment { get; set; } = (int)Alignment.Evil;

    private static CoreBots _instance;
    public static CoreBots Instance => _instance ??= new CoreBots();
    public IScriptInterface Bot => IScriptInterface.Instance;

    #endregion

    #region Start/Stop

    /// <summary>
    /// Set common bot options to desired value
    /// </summary>
    /// <param name="changeTo">Value the options will be changed to</param>
    public void SetOptions(bool changeTo = true, bool disableClassSwap = false)
    {
        if (changeTo)
        {
            if (CBO_Active())
            {
                CBOList = File.ReadAllLines(AppPath + $@"\options\CBO_Storage({Bot.Player.Username}).txt").ToList();
                ReadCBO();
            }

            if (AppPath != null)
                Logger($"Bot Started [{Bot.Manager.LoadedScript.Replace(AppPath, string.Empty).Replace("\\Scripts\\", "").Replace(".cs", "")}]");
            else Logger($"Bot Started");

            SkuaVersionChecker("1.1.1.0");

            if (!Bot.Player.LoggedIn)
            {
                if (Bot.Servers.CachedServers.Count() > 0)
                {
                    Logger("Auto Login triggered");
                    if (!Bot.Servers.EnsureRelogin(Bot.Options.ReloginServer ?? Bot.Servers.CachedServers[0].Name))
                        Logger("Please log-in before starting the bot.", messageBox: true, stopBot: true);
                    Bot.Sleep(5000);
                }
                else Logger("Please log-in before starting the bot.", messageBox: true, stopBot: true);
            }

            IsMember = Bot.Player.IsMember;

            ReadMe();
        }

        // Common Options
        Bot.Options.PrivateRooms = false;
        Bot.Options.SafeTimings = changeTo;
        Bot.Options.RestPackets = changeTo;
        Bot.Options.AutoRelogin = changeTo;
        Bot.Options.InfiniteRange = changeTo;
        Bot.Options.SkipCutscenes = changeTo;
        Bot.Options.QuestAcceptAndCompleteTries = AcceptandCompleteTries;
        Bot.Drops.RejectElse = changeTo;
        Bot.Lite.UntargetDead = changeTo;
        Bot.Lite.UntargetSelf = changeTo;
        Bot.Lite.ReacceptQuest = false;
        Bot.Lite.Set("dOptions[\"disRed\"]", true);

        CollectData(changeTo);

        if (changeTo)
        {
            Bot.Options.HuntDelay = HuntDelay;
            Bot.Events.ScriptStopping += StopBotEvent;

            Bot.Send.Packet("%xt%zm%afk%1%false%");
            Bot.Sleep(ActionDelay);
            bool TimerRunning = false;
            Bot.Handlers.RegisterHandler(5000, b =>
            {
                if (b.Player.AFK && !TimerRunning)
                {
                    TimerRunning = true;
                    Bot.Sleep(300000);
                    if (b.Player.AFK)
                    {
                        b.Options.AutoRelogin = true;
                        b.Servers.Logout();
                    }
                    TimerRunning = false;
                }
            }, "AFK Handler");

            Bot.Handlers.RegisterHandler(3000, b =>
            {
                if (Bot.Quests.Tree.Count() > LoadedQuestLimit)
                {
                    Bot.Flash.SetGameObject("world.questTree", new ExpandoObject());
                }
            }, "Quest-Limit Handler");

            Bot.Events.MapChanged += PrisonDetector;
            void PrisonDetector(string map)
            {
                if (map.ToLower() == "prison" && !joinedPrison && !prisonListernerActive)
                {
                    prisonListernerActive = true;
                    Bot.Options.AutoRelogin = false;
                    Bot.Servers.Logout();
                    string message = "You were teleported to /prison by someone other than the bot. We disconnected you and stopped the bot out of precaution.\n" +
                                     "Be ware that you might have received a ban, as this is a method moderators use to see if you're botting." +
                                     (!PrivateRooms || PrivateRoomNumber < 1000 || PublicDifficult ? "\nGuess you should have stayed out of public rooms!" : String.Empty);
                    Logger(message);
                    Bot.ShowMessageBox(message, "Unauthorized joining of /prison detected!", "Oh fuck!");
                    Bot.Stop(true);
                }
            }

            Bot.Bank.Load();
            Bot.Bank.Loaded = true;
            if (BankMiscAC)
            {
                List<string> Whitelisted = new() { "Note", "Item", "Resource", "QuestItem" };
                List<string> WhitelistedSU = new() { "Note", "Item", "Resource", "QuestItem", "ServerUse" };
                List<string> MiscForBank = new();
                foreach (var item in Bot.Inventory.Items)
                {
                    if (Bot.Boosts.Enabled ? !Whitelisted.Contains(item.Category.ToString()) : !WhitelistedSU.Contains(item.Category.ToString()))
                        continue;
                    if (item.Name != "Treasure Potion" && !BankingBlackList.Contains(item.Name) && item.Coins)
                        MiscForBank.Add(item.Name);
                }
                ToBank(MiscForBank.ToArray());
            }

            foreach (InventoryItem item in Bot.Inventory.Items.Where(i => i.Equipped))
                EquipmentBeforeBot.Add(item.Name);

            usingSoloGeneric = SoloClass.ToLower() == "generic";
            usingFarmGeneric = FarmClass.ToLower() == "generic";
            if (disableClassSwap)
                usingSoloGeneric = true;
            EquipClass(ClassType.Solo);

            // Anti-lag option
            if (AntiLag)
            {
                Bot.Options.LagKiller = true;
                Bot.Flash.SetGameObject("stage.frameRate", 10);
                if (!Bot.Flash.GetGameObject<bool>("ui.monsterIcon.redX.visible"))
                    Bot.Flash.CallGameFunction("world.toggleMonsters");
            }

            Bot.Options.CustomName = "SKUA BOT";
            Bot.Options.CustomGuild = "HTTPS://AUQW.TK/";

            Bot.Drops.Start();

            Logger("Bot Configured");
        }
    }
    public List<string> BankingBlackList = new();
    private List<string> EquipmentBeforeBot = new();
    private bool joinedPrison = false;
    private bool prisonListernerActive = false;

    /// <summary>
    /// Stops the bot and moves you back to /Battleon
    /// </summary>
    private bool StopBot(bool crashed)
    {
        CancelRegisteredQuests();
        SavedState(false);
        Bot.Handlers.Clear();

        if (Bot.Player.LoggedIn)
        {
            JumpWait();
            Bot.Combat.Exit();
            Bot.Sleep(ActionDelay);
            if (EquipmentBeforeBot.Count() > 0)
                Equip(EquipmentBeforeBot.ToArray());
            if (!string.IsNullOrWhiteSpace(CustomStopLocation))
            {
                if (CustomStopLocation.ToLower() == "home")
                    Bot.Send.Packet($"%xt%zm%house%1%{Bot.Player.Username}%");
                else if (new[] { "off", "disabled", "disable", "stop", "same", "currentmap", "bot.map.currentmap" }
                                .Any(m => m.ToLower() == CustomStopLocation.ToLower())) { }
                else
                    Join(CustomStopLocation);
            }
        }
        if (AntiLag)
        {
            Bot.Flash.SetGameObject("stage.frameRate", 60);
            if (Bot.Flash.GetGameObject<bool>("ui.monsterIcon.redX.visible"))
                Bot.Flash.CallGameFunction("world.toggleMonsters");
        }

        Bot.Options.CustomName = Bot.Player.Username.ToUpper();
        Bot.Options.CustomGuild = $"< {(Bot.Flash.GetGameObject<string>("world.myAvatar.objData.guild.Name").Replace("&lt; ", "< ").Replace(" &gt;", " >"))} >"; ;

        if (crashed)
            Logger("Bot Stopped due to crash");
        else if (!Bot.Player.LoggedIn)
            Logger("Auto Relogin appears to have failed");
        else
            Logger("Bot Stopped Successfully");

        GC.KeepAlive(Instance);
        return scriptFinished;
    }
    private bool scriptFinished = true;

    private bool StopBotEvent(Exception e)
    {
        SetOptions(false);

        if (e != null)
        {
            string eSlice = e.Message + "\n" + e.InnerException;
            List<string> logs = Ioc.Default.GetRequiredService<ILogService>().GetLogs(LogType.Script);
            logs = logs.Skip(logs.Count() > 5 ? (logs.Count() - 5) : logs.Count()).ToList();
            if (Bot.ShowMessageBox("A crash has been detected, please fill in the report form (prefilled):\n\n" + eSlice,
                                   "Script Crashed", "Open Form", "Close Window").Text == "Open Form")
            {
                string url = "\"https://docs.google.com/forms/d/e/1FAIpQLSeI_S99Q7BSKoUCY2O6o04KXF1Yh2uZtLp0ykVKsFD1bwAXUg/viewform?usp=pp_url&" +
                    "entry.2118425091=Bug+Report&" +
                   $"entry.290078150={Bot.Manager.LoadedScript.Split("Scripts").Last().Replace('/', '\\').Substring(1).Replace(".cs", "")}&" +
                    "entry.1803231651=It+stopped+at+the+wrong+time+(crash)&" +
                   $"entry.1954840906={logs.Join("%0A")}&" +
                   $"entry.285894207={eSlice}&\"";
                url = url.Replace("\r\n", "%0A").Replace("\n", "").Replace(" ", "%20");

                Process p = new();
                p.StartInfo.FileName = "rundll32";
                p.StartInfo.Arguments = "url,OpenURL " + url;
                p.StartInfo.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.System).Split('\\').First() + "\\";
                p.Start();

                Logger("Thank you for reporting the crash. Below you will find the information you will need to report, in case it isn't being auto filled");

            }
            else Logger("A crash has occurred. Please report it in the form with the details below");

            Bot.Log("--------------------------------------");
            Logger("Last 5 Logs:");
            Bot.Log(logs.Join('\n'));
            Bot.Log("--------------------------------------");
            Logger("Crash (Debug)");
            Bot.Log(eSlice);
            Bot.Log("--------------------------------------");
        }
        return StopBot(e != null);
    }

    public void ScriptMain(IScriptInterface bot)
    {
        RunCore();
    }

    #endregion

    #region Inventory, Bank and Shop
#nullable enable

    /// <summary>
    /// Check the Bank, Inventory and Temp Inventory for the item
    /// </summary>
    /// <param name="item">Name of the item</param>
    /// <param name="quant">Desired quantity</param>
    /// <param name="toInv">Whether or not send the item to Inventory</param>
    /// <returns>Returns whether the item exists in the desired quantity in the bank and inventory</returns>
    public bool CheckInventory(string item, int quant = 1, bool toInv = true)
    {
        if (Bot.TempInv.Contains(item, quant))
            return true;

        if (Bot.Inventory.Contains(item, quant))
            return true;

        if (Bot.Bank.Contains(item))
        {
            if (toInv)
                Unbank(item);

            if ((toInv && Bot.Inventory.GetQuantity(item) >= quant) ||
               (!toInv && Bot.Bank.TryGetItem(item, out InventoryItem? _item) && _item != null && _item.Quantity >= quant))
                return true;
        }

        if (Bot.House.Contains(item))
            return true;

        return false;
    }

    /// <summary>
    /// Checks the Bank and Inventory for the item with it's ID
    /// </summary>
    /// <param name="itemID">ID of the item to verify</param>
    /// <param name="quant">Desired quantity</param>
    /// <param name="toInv">Whether or not send the item to Inventory</param>
    /// <returns>Returns whether the item exists in the desired quantity in the Bank and Inventory</returns>
    public bool CheckInventory(int itemID, int quant = 1, bool toInv = true)
    {
        ItemBase? itemTempInv = Bot.TempInv.Items.Find(i => i.ID == itemID);
        InventoryItem? itemInv = Bot.Inventory.Items.Find(i => i.ID == itemID);
        InventoryItem? itemBank = Bot.Bank.Items.Find(i => i.ID == itemID);

        if (itemTempInv == null && itemInv == null && itemBank == null)
            return false;

        if (itemTempInv != null && Bot.TempInv.Contains(itemTempInv.Name, quant))
            return true;

        if (itemInv != null && Bot.Inventory.Contains(itemInv.Name, quant))
            return true;

        if (itemBank != null && Bot.Bank.Contains(itemBank.Name))
        {
            if (toInv)
                Unbank(itemBank.Name);

            if (itemBank.Quantity >= quant)
                return true;
        }

        return false;
    }

    /// <summary>
    /// Check if the Bank/Inventory has at least 1 of all listed items
    /// </summary>
    /// <param name="itemNames">Array of names of the items to be check</param>
    /// <param name="toInv">Whether or not send the item to Inventory</param>
    /// <param name="any">If any of the items exist, returns true</param>
    /// <returns>Returns whether all the items exist in the Bank or Inventory</returns>
    public bool CheckInventory(string[] itemNames, int quant = 1, bool any = false, bool toInv = true)
    {
        if (itemNames == null)
            return true;

        foreach (string name in itemNames)
        {
            if (Bot.Inventory.Contains(name, quant))
            {
                if (any)
                    return true;
                else
                    continue;
            }

            else if (Bot.Bank.Contains(name))
            {
                if (toInv)
                    Unbank(name);

                if ((toInv && Bot.Inventory.GetQuantity(name) >= quant) ||
                   (!toInv && Bot.Bank.TryGetItem(name, out InventoryItem? _item) && _item != null && _item.Quantity >= quant))
                {
                    if (any)
                        return true;
                    else continue;
                }
            }

            if (!any)
                return false;
        }

        return !any;
    }

    public void CheckSpaces(ref int counter, params string[] items)
    {
        int count = 0;
        foreach (string s in items)
        {
            if (CheckInventory(s, toInv: false))
                count++;
        }
        if (Bot.Inventory.FreeSlots < (items.Count() - count))
            Logger($"Not enough free slots, please clear {(items.Count() - count)} slot" + ((items.Count() - count) > 1 ? "s" : ""), messageBox: true, stopBot: true);
    }

    /// <summary>
    /// Move items from bank to inventory
    /// </summary>
    /// <param name="items">Items to move</param>
    public void Unbank(params string[] items)
    {
        if (items == null)
            return;
        JumpWait();
        if (Bot.Flash.GetGameObject("ui.mcPopup.currentLabel") != "Bank")
            Bot.Bank.Open();
        foreach (string item in items)
        {
            if (Bot.Bank.Contains(item))
            {
                Bot.Sleep(ActionDelay);
                if (Bot.Inventory.FreeSlots == 0)
                    Logger("Your inventory is full, please clean it and restart the bot", messageBox: true, stopBot: true);

                if (!Bot.Bank.EnsureToInventory(item))
                {
                    Logger($"Failed to unbank {item}, skipping it", messageBox: true);
                    continue;
                }
                Logger($"{item} moved from bank");
            }
        }
    }

    /// <summary>
    /// Move items from inventory to bank
    /// </summary>
    /// <param name="items">Items to move</param>
    public void ToBank(params string[] items)
    {
        if (items == null)
            return;

        JumpWait();
        if (Bot.Flash.GetGameObject("ui.mcPopup.currentLabel") != "Bank")
            Bot.Bank.Open();
        foreach (string item in items)
        {
            if (Bot.Inventory.IsEquipped(item))
            {
                Logger("Can't bank an equipped item");
                continue;
            }
            if (Bot.Inventory.Contains(item))
            {
                if (!Bot.Inventory.EnsureToBank(item))
                {
                    Logger($"Failed to b {item}, skipping it", messageBox: true);
                    continue;
                }
                Logger($"{item} moved to bank");
            }
        }
    }

    /// <summary>
    /// Buys a item till you have the desired quantity
    /// </summary>
    /// <param name="map">Map of the shop</param>
    /// <param name="shopID">ID of the shop</param>
    /// <param name="itemName">Name of the item</param>
    /// <param name="quant">Desired quantity</param>
    /// <param name="shopQuant">How many items you get for 1 buy</param>
    /// <param name="shopItemID">Use this for Merge shops that has 2 or more of the item with the same name and you need the second/third/etc., be aware that it will re-log you after to prevent ghost buy. To get the ShopItemID use the built in loader of Skua</param>
    public void BuyItem(string map, int shopID, string itemName, int quant = 1, int shopQuant = 1, int shopItemID = 0)
    {
        if (CheckInventory(itemName, quant))
            return;

        ShopItem item = parseShopItem(GetShopItems(map, shopID).Where(x => shopItemID == 0 ? x.Name == itemName : x.ShopItemID == shopItemID).ToList(), shopID, itemName, shopItemID);
        if (item.ID == 0)
            return;

        quant = _CalcBuyQuantity(item, quant, shopQuant);
        if (quant <= 0)
            return;
        if (item.Coins && item.Cost > 0)
            if (Bot.ShowMessageBox(
                                $"The bot is about to buy \"{item.Name}\", which costs {item.Cost} AC, do you accept this?",
                                "Warning: Costs AC!", true)
                            != true)
                Logger($"The bot cannot continue without buying \"{item.Name}\", stopping the bot.", messageBox: true, stopBot: true);
            else if (Bot.Flash.GetGameObject<int>("world.myAvatar.objData.intCoins") < item.Cost)
                Logger($"You don't have enough AC to buy \"{item.Name}\", the bot cannot continue.", messageBox: true, stopBot: true);
        if (!item.Coins && item.Cost > Bot.Player.Gold)
            Logger($"You don't have the {item.Cost} Gold to buy \"{item.Name}\", the bot cannot continue.", messageBox: true, stopBot: true);

        Join(map);
        _BuyItem(shopID, item, quant, shopQuant);

        if (CheckInventory(item.Name, quant))
            Logger($"Bought {quant}x{shopQuant} {item.Name}");
        else Logger($"Failed at buying {quant}x{shopQuant} {item.Name}");
    }

    /// <summary>
    /// Buys a item till it have the desired quantity
    /// </summary>
    /// <param name="map">Map of the shop</param>
    /// <param name="shopID">ID of the shop</param>
    /// <param name="itemID">ID of the item</param>
    /// <param name="quant">Desired quantity</param>
    /// <param name="shopQuant">How many items you get for 1 buy</param>
    /// <param name="shopItemID">Use this for Merge shops that has 2 or more of the item with the same name and you need the second/third/etc., be aware that it will relog you after to prevent ghost buy. To get the ShopItemID use the built in loader of Skua</param>
    public void BuyItem(string map, int shopID, int itemID, int quant = 1, int shopQuant = 1, int shopItemID = 0)
    {
        if (CheckInventory(itemID, quant))
            return;

        ShopItem item = parseShopItem(GetShopItems(map, shopID).Where(x => shopItemID == 0 ? x.ID == itemID : x.ShopItemID == shopItemID).ToList(), shopID, itemID.ToString(), shopItemID);
        if (item == new ShopItem())
            return;

        quant = _CalcBuyQuantity(item, quant, shopQuant);
        if (quant <= 0)
            return;

        if (item.Coins && item.Cost > 0)
            if (Bot.ShowMessageBox(
                                $"The bot is about to buy \"{item.Name}\", which costs {item.Cost} AC, do you accept this?",
                                "Warning: Costs AC!", true)
                            != true)
                Logger($"The bot cannot continue without buying \"{item.Name}\", stopping the bot.", messageBox: true, stopBot: true);
            else if (Bot.Flash.GetGameObject<int>("world.myAvatar.objData.intCoins") < item.Cost)
                Logger($"You don't have the {item.Cost} AC needed to buy \"{item.Name}\", the bot cannot continue.", messageBox: true, stopBot: true);
        if (!item.Coins && item.Cost > Bot.Player.Gold)
            Logger($"You don't have the {item.Cost} Gold to buy \"{item.Name}\", the bot cannot continue.", messageBox: true, stopBot: true);

        Join(map);
        _BuyItem(shopID, item, quant, shopQuant);

        if (CheckInventory(item.Name, quant))
            Logger($"Bought {quant}x{shopQuant} {item.Name}");
        else Logger($"Failed at buying {quant}x{shopQuant} {item.Name}");
    }

    private void _BuyItem(int shopID, ShopItem item, int quant, int shopQuant)
    {
        Bot.Events.ExtensionPacketReceived += RelogRequieredListener;
        for (int i = 0; i < quant; i++)
        {
            Bot.Shops.BuyItem(item.ID, item.ShopItemID);
            Bot.Sleep(ActionDelay);
        }
        Bot.Events.ExtensionPacketReceived -= RelogRequieredListener;

        void RelogRequieredListener(dynamic packet)
        {
            string type = packet["params"].type;
            dynamic data = packet["params"].dataObj;
            if (type == "json")
            {
                string str = data.strMessage;
                switch (str)
                {
                    case "Item is not buyable. Item Inventory full. Re-login to syncronize your real bag slot amount.":
                        Logger("Inventory de-sync (AE Issue) detected, reloggin so the bot can continue");
                        Relogin();
                        break;
                }
            }
        }
    }

    private int _CalcBuyQuantity(ShopItem item, int quant, int shopQuant)
    {
        if (Bot.Inventory.GetQuantity(item.Name) + shopQuant > item.MaxStack)
        {
            Logger("Can't buy merge item past its max stack, skipping");
            return 0;
        }
        int quantB = quant - Bot.Inventory.GetQuantity(item.Name);
        if (quantB < 0)
            return 0;
        decimal quantF = (decimal)quantB / (decimal)shopQuant;
        return (int)Math.Ceiling(quantF);
    }

    /// <summary>
    /// Sells a item till you have the desired quantity
    /// </summary>
    /// <param name="itemName">Name of the item</param>
    /// <param name="quant">Desired quantity</param>
    /// <param name="all">Set to true if you wish to sell all the items</param>
    public void SellItem(string itemName, int quant = 0, bool all = false)
    {
        if (!CheckInventory(itemName))
            return;
        JumpWait();
        if (!all)
        {
            for (int i = 0; i < quant; i++)
            {
                Bot.Shops.SellItem(itemName);
                Bot.Sleep(ActionDelay);
            }
            return;
        }

        while (!Bot.ShouldExit && Bot.Inventory.GetQuantity(itemName) != 0)
        {
            Bot.Shops.SellItem(itemName);
            Bot.Sleep(ActionDelay);
        }

        Logger($"{(all ? string.Empty : quant.ToString())} {itemName} sold");
    }

    public List<ShopItem> GetShopItems(string map, int shopID)
    {
        Bot.Wait.ForTrue(() => Bot.Shops.ID == shopID, () =>
        {
            Join(map);
            Bot.Shops.Load(shopID);
            Bot.Sleep(ActionDelay);
        }, 20, 1000);

        if (Bot.Shops.ID != shopID || Bot.Shops.Items == null)
        {
            Bot.ShowMessageBox("Failed to load shop the shop and get it's data, please restart the client.", "Shop Data Loading Failed");
            return new();
        }
        return Bot.Shops.Items;
    }

    public ShopItem parseShopItem(List<ShopItem> shopItem, int shopID, string itemNameID, int shopItemID = 0)
    {
        if (shopItem.Count == 0)
        {
            Logger($"Item {itemNameID} not found in shop {shopID}.");
            return new();
        }
        else if (shopItem.Count > 1)
        {
            if (shopItemID > 0)
            {
                if (!shopItem.Any(x => x.ShopItemID == shopItemID))
                {
                    Logger($"Item {itemNameID} with ShopItemID {shopItemID} was not in {shopID}. The developer needs to correc the Shop Item ID");
                    return new();
                }
                return shopItem.First(x => x.ShopItemID == shopItemID);
            }
            Logger($"Multiple items found with the name {shopItem.First().Name} in shop {shopID}. The developer needs to specify the Shop Item ID.");
            return new();
        }

        return shopItem.First();
    }

    /// <summary>
    /// <param name="item">Item to Trash/Bank</param>
    /// Removes the Specific {items} from Players Inv (Banks Coin{ac} items)
    /// </summary>
    public void TrashCan(string item)
    {
        InventoryItem? TrashItem = Bot.Inventory.GetItem(item);

        if (item == null || TrashItem == null)
            return;

        if (!TrashItem.Coins && CheckInventory(item))
        {
            Logger($"Trashing {TrashItem}");
            Bot.Send.Packet($"%xt%zm%removeItem%{Bot.Map.RoomID}%{TrashItem.ID}%{Bot.Player.ID}%{TrashItem.Quantity}%");
        }
        else ToBank(item);
    }


    /// <summary>
    /// <param name="items">Items to Trash/Bank</param>
    /// Removes the Specific {items} from Players Inv (Banks Coin{ac} items)
    /// </summary>
    public void TrashCan(params string[] items)
    {
        foreach (string item in items)
        {
            InventoryItem? TrashItem = Bot.Inventory.GetItem(item);

            if (item == null || TrashItem == null)
                return;

            if (!TrashItem.Coins && CheckInventory(item))
            {
                Logger($"Trashing: {TrashItem}");
                Bot.Send.Packet($"%xt%zm%removeItem%{Bot.Map.RoomID}%{TrashItem.ID}%{Bot.Player.ID}%{TrashItem.Quantity}%");
            }
            else ToBank(item);
        }
    }

    #endregion

    #region Drops

    /// <summary>
    /// Adds drops to the pickup list, un-bank the items.
    /// </summary>
    /// <param name="items">Items to add</param>
    public void AddDrop(params string[] items)
    {
        Unbank(items);
        Bot.Drops.Add(items);
    }

    /// <summary>
    /// Removes drops from the pickup list.
    /// </summary>
    /// <param name="items">Items to remove</param>
    public void RemoveDrop(params string[] items)
    {
        Bot.Drops.Remove(items);
    }

    #endregion

    #region Quest
    private CancellationTokenSource? questCTS = null;
    /// <summary>
    /// This will register quests to be completed while doing something else, i.e. while in combat.
    /// If it has quests already registered, it will cancel them first and then register the new quests.
    /// </summary>
    /// <param name="questIDs">ID of the quests to be completed.</param>
    public void RegisterQuests(params int[] questIDs)
    {
        if (questCTS is not null)
            CancelRegisteredQuests();

        // Defining all the lists to be used=
        List<Quest> questData = EnsureLoad(questIDs);
        Dictionary<Quest, int> chooseQuests = new();
        Dictionary<Quest, int> nonChooseQuests = new();

        foreach (Quest q in questData)
        {
            bool shouldBreak = false;
            // Removing quests that you can't accept
            foreach (ItemBase req in q.AcceptRequirements)
            {
                if (!CheckInventory(req.Name))
                {
                    Logger($"Missing requirement {req.Name} for \"{q.Name}\" [{q.ID}]");
                    shouldBreak = true;
                    break;
                }
            }
            if (shouldBreak)
                break;

            // Separating the quests into choose and non-choose
            if (q.SimpleRewards.Any(r => r.Type == 2))
                chooseQuests.Add(q, 1);
            else
                nonChooseQuests.Add(q, 1);
        }

        EnsureAccept(questIDs);
        questCTS = new();
        Task.Run(async () =>
        {
            while (!Bot.ShouldExit && !questCTS.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(ActionDelay);

                    // Quests that dont need a choice
                    foreach (KeyValuePair<Quest, int> kvp in nonChooseQuests)
                    {
                        if (Bot.Quests.CanComplete(kvp.Key.ID))
                        {
                            EnsureComplete(kvp.Key.ID);
                            await Task.Delay(ActionDelay);
                            EnsureAccept(kvp.Key.ID);
                            Logger($"Quest completed x{nonChooseQuests[kvp.Key]++} times: [{kvp.Key.ID}] \"{kvp.Key.Name}\"");
                        }
                    }

                    // Quests that need a choice
                    foreach (KeyValuePair<Quest, int> kvp in chooseQuests)
                    {
                        if (Bot.Quests.CanComplete(kvp.Key.ID))
                        {
                            // Finding the next item that you dont have max stack of yet
                            List<SimpleReward> simpleRewards =
                                kvp.Key.SimpleRewards.Where(r => r.Type == 2 &&
                                                            (!Bot.Inventory.IsMaxStack(r.Name) ||
                                                                            !(Bot.Bank.TryGetItem(r.Name, out InventoryItem? item) && item != null && item.Quantity >= r.MaxStack))).ToList(); if (simpleRewards.Count == 0)
                            {
                                EnsureComplete(kvp.Key.ID);
                                await Task.Delay(ActionDelay);
                                EnsureAccept(kvp.Key.ID);
                                continue;
                            }

                            Bot.Drops.Add(kvp.Key.Rewards.Where(x => simpleRewards.Any(t => t.ID == x.ID)).Select(i => i.Name).ToArray());
                            EnsureComplete(kvp.Key.ID, simpleRewards.First().ID);
                            await Task.Delay(ActionDelay);
                            EnsureAccept(kvp.Key.ID);
                            Logger($"Quest completed x{chooseQuests[kvp.Key]++} times: [{kvp.Key.ID} \"{kvp.Key.Name}\" (got {kvp.Key.Rewards.First(x => x.ID == simpleRewards.First().ID).Name}])");

                        }
                    }
                }
                catch { }
            }
            questCTS = null;
        });
    }

    /// <summary>
    /// Cancels the current registered quests.
    /// </summary>
    public void CancelRegisteredQuests()
    {
        questCTS?.Cancel();
        Bot.Wait.ForTrue(() => questCTS == null, 30);
    }

    /// <summary>
    /// Ensures you are out of combat before accepting the quest
    /// </summary>
    /// <param name="questID">ID of the quest to accept</param>
    public bool EnsureAccept(int questID)
    {
        Quest QuestData = EnsureLoad(questID);

        if (QuestData.Upgrade && !IsMember)
            Logger($"\"{QuestData.Name}\" [{questID}] is member-only, stopping the bot.", stopBot: true);

        if (Bot.Quests.IsInProgress(questID))
            return true;
        if (questID <= 0)
            return false;

        Bot.Drops.Add(QuestData.Requirements.Where(x => !x.Temp).Select(y => y.Name).ToArray());
        Bot.Sleep(ActionDelay);
        return Bot.Quests.EnsureAccept(questID);
    }

    /// <summary>
    /// Accepts all the quests given
    /// </summary>
    /// <param name="questIDs">IDs of the quests</param>
    public void EnsureAccept(params int[] questIDs)
    {
        List<Quest> QuestData = EnsureLoad(questIDs);
        foreach (Quest quest in QuestData)
        {
            if (quest.Upgrade && !IsMember)
                Logger($"\"{quest.Name}\" [{quest.ID}] is member-only, stopping the bot.", stopBot: true);

            if (Bot.Quests.IsInProgress(quest.ID) || quest.ID <= 0)
                continue;

            Bot.Drops.Add(quest.Requirements.Where(x => !x.Temp).Select(y => y.Name).ToArray());
            Bot.Sleep(ActionDelay);
            Bot.Quests.EnsureAccept(quest.ID);
        }
    }

    /// <summary>
    /// Completes the quest with a choose-able reward item
    /// </summary>
    /// <param name="questID">ID of the quest to complete</param>
    /// <param name="itemID">ID of the choose-able reward item</param>
    public bool EnsureComplete(int questID, int itemID = -1)
    {
        if (questID <= 0)
            return false;
        Bot.Sleep(ActionDelay);
        return Bot.Quests.EnsureComplete(questID, itemID);
    }

    /// <summary>
    /// Completes all the quests given but doesn't support quests with choose-able rewards
    /// </summary>
    /// <param name="questIDs">IDs of the quests</param>
    public void EnsureComplete(params int[] questIDs)
    {
        Bot.Quests.EnsureComplete(questIDs);
    }

    /// <summary>
    /// Completes a quest and choose any item from it that you don't have (automatically accepts the drop)
    /// </summary>
    /// <param name="questID">ID of the quest</param>
    /// <param name="itemList">List of the items to get, if you want all just let it be null</param>
    public bool EnsureCompleteChoose(int questID, string[]? itemList = null)
    {
        if (questID <= 0)
            return false;
        Bot.Sleep(ActionDelay);
        Quest quest = EnsureLoad(questID);
        if (quest is not null)
        {
            foreach (ItemBase item in quest.Rewards)
            {
                if (!CheckInventory(item.Name, toInv: false)
                    && (itemList == null || (itemList != null && itemList.Contains(item.Name))))
                {
                    bool completed = Bot.Quests.EnsureComplete(questID, item.ID);
                    Bot.Drops.Pickup(item.Name);
                    Bot.Wait.ForPickup(item.Name);
                    return completed;
                }
            }
        }
        else
        {
            Logger($"Failed to load Quest {questID}, EnsureCompleteChoose failed");
            return false;
        }
        Logger($"Could not complete the quest {questID}. Maybe all items are already in your inventory");
        return false;
    }

    public Quest EnsureLoad(int questID)
    {
        var toReturn = Bot.Quests.Tree.Find(x => x.ID == questID) ?? _EnsureLoad(questID);
        if (toReturn == null)
        {
            Logger("Failed to get the Quest Object, please restart the client.", messageBox: true, stopBot: true);
            return new();
        }

        return toReturn;

        Quest _EnsureLoad(int questID)
        {
            Bot.Sleep(ActionDelay);
            Bot.Wait.ForTrue(() => Bot.Quests.Tree.Contains(x => x.ID == questID), () => Bot.Quests.Load(questID), 20);
            return Bot.Quests.Tree.Find(q => q.ID == questID)!;
            // return Bot.Quests.EnsureLoad(questID);
        }
    }

    public List<Quest> EnsureLoad(params int[] questIDs)
    {
        List<Quest> quests = Bot.Quests.Tree.Where(x => questIDs.Contains(x.ID)).ToList();
        if (quests.Count == questIDs.Length)
            return quests;
        List<int> missing = questIDs.Where(x => !quests.Any(y => y.ID == x)).ToList();

        Bot.Sleep(ActionDelay);
        for (int i = 0; i < missing.Count; i = i + 30)
        {
            Bot.Quests.Load(missing.ToArray()[i..(missing.Count > i ? missing.Count : i + 30)]);
            Bot.Sleep(1500);
        }
        var toReturn = Bot.Quests.Tree.Where(x => questIDs.Contains(x.ID)).ToList();
        if (toReturn.Count() <= 0 || toReturn == null)
        {
            Logger("Failed to get the Quest Object, please restart the client.", messageBox: true, stopBot: true);
            return new();
        }
        return toReturn;
    }

    /// <summary>
    /// Accepts and then completes the quest, used inside a loop
    /// </summary>
    /// <param name="questID">ID of the quest</param>
    /// <param name="itemID">ID of the choose-able reward item</param>
    public void ChainComplete(int questID, int itemID = -1)
    {
        Bot.Quests.EnsureAccept(questID);
        Bot.Sleep(ActionDelay);
        Bot.Quests.EnsureComplete(questID, itemID);
    }

    /// <param name="QuestID">ID of the quest</param>
    public bool isCompletedBefore(int QuestID)
    {
        Quest? QuestData = EnsureLoad(QuestID);
        try
        {
            return QuestData.Slot < 0 || Bot.Flash.CallGameFunction<int>("world.getQuestValue", QuestData.Slot) >= QuestData.Value;
        }
        catch
        {
            QuestData = Bot.Quests.EnsureLoad(QuestID);
            return QuestData?.Slot < 0 || Bot.Flash.CallGameFunction<int>("world.getQuestValue", QuestData!.Slot) >= QuestData.Value;
        }
    }

    #endregion

    #region Kill

    /// <summary>
    /// Joins a map, jump & set the spawn point and kills the specified monster
    /// </summary>
    /// <param name="map">Map to join</param>
    /// <param name="cell">Cell to jump to</param>
    /// <param name="pad">Pad to jump to</param>
    /// <param name="monster">Name of the monster to kill</param>
    /// <param name="item">Item to kill the monster for, if null will just kill the monster 1 time</param>
    /// <param name="quant">Desired quantity of the item</param>
    /// <param name="isTemp">Whether the item is temporary</param>
    /// <param name="log">Whether it will log that it is killing the monster</param>
    public void KillMonster(string map, string cell, string pad, string monster, string? item = null, int quant = 1, bool isTemp = true, bool log = true, bool publicRoom = false)
    {
        if (item != null && (isTemp ? Bot.TempInv.Contains(item, quant) : CheckInventory(item, quant)))
            return;
        if (!isTemp && item != null)
            AddDrop(item);

        Join(map, cell, pad, publicRoom: publicRoom);
        Jump(cell, pad);

        if (item == null)
        {
            if (log)
                Logger($"Killing {monster}");
            Bot.Kill.Monster(monster);
            Rest();
            return;
        }

        _KillForItem(monster, item, quant, isTemp, log: log);
    }

    /// <summary>
    /// Kills a monster using it's ID
    /// </summary>
    /// <param name="map">Map to join</param>
    /// <param name="cell">Cell to jump to</param>
    /// <param name="pad">Pad to jump to</param>
    /// <param name="monsterID">ID of the monster</param>
    /// <param name="item">Item to kill the monster for, if null will just kill the monster 1 time</param>
    /// <param name="quant">Desired quantity of the item</param>
    /// <param name="isTemp">Whether the item is temporary</param>
    /// <param name="log">Whether it will log that it is killing the monster</param>
    public void KillMonster(string map, string cell, string pad, int monsterID, string? item = null, int quant = 1, bool isTemp = true, bool log = true, bool publicRoom = false)
    {
        if (item != null && (isTemp ? Bot.TempInv.Contains(item, quant) : CheckInventory(item, quant)))
            return;
        if (!isTemp && item != null)
            AddDrop(item);

        Join(map, cell, pad, publicRoom: publicRoom);
        Jump(cell, pad);

        Monster? monster = Bot.Monsters.CurrentMonsters?.Find(m => m.ID == monsterID);
        if (monster == null)
        {
            Logger($"Monster [{monsterID}] not found. Something is wrong. Stopping bot", messageBox: true, stopBot: true);
            return;
        }

        if (item == null)
        {
            if (log)
                Logger($"Killing {monster}");
            Bot.Kill.Monster(monster);
            Rest();
            return;
        }

        _KillForItem(monster.Name, item, quant, isTemp, log: log);
    }

    /// <summary>
    /// Joins a map and hunt for the monster
    /// </summary>
    /// <param name="map">Map to join</param>
    /// <param name="monster">Name of the monster to kill</param>
    /// <param name="item">Item to hunt the monster for, if null will just hunt & kill the monster 1 time</param>
    /// <param name="quant">Desired quantity of the item</param>
    /// <param name="isTemp">Whether the item is temporary</param>
    public void HuntMonster(string map, string monster, string? item = null, int quant = 1, bool isTemp = true, bool log = true, bool publicRoom = false)
    {
        if (item != null && (isTemp ? Bot.TempInv.Contains(item, quant) : CheckInventory(item, quant)))
            return;
        if (!isTemp && item != null)
            AddDrop(item);

        Join(map, publicRoom: publicRoom);
        if (item == null)
        {
            if (log)
                Logger($"Hunting {monster}");
            Bot.Hunt.Monster(monster);
            Rest();
            return;
        }

        if (log)
        {
            int dynamicQuantity = isTemp ? Bot.TempInv.GetQuantity(item) : Bot.Inventory.GetQuantity(item);
            Logger($"Hunting {monster} for {item}, ({dynamicQuantity}/{quant}) [Temp = {isTemp}]");
        }

        while (!Bot.ShouldExit && (isTemp ? !Bot.TempInv.Contains(item, quant) : !CheckInventory(item, quant)))
        {
            if (!Bot.Combat.StopAttacking)
                Bot.Hunt.Monster(monster);
            Bot.Sleep(ActionDelay);
            Rest();
        }
    }

    /// <summary>
    /// Kills a monster using it's MapID
    /// </summary>
    /// <param name="map">Map to join</param>
    /// <param name="cell">Cell to jump to</param>
    /// <param name="pad">Pad to jump to</param>
    /// <param name="monsterID">ID of the monster</param>
    /// <param name="item">Item to kill the monster for, if null will just kill the monster 1 time</param>
    /// <param name="quant">Desired quantity of the item</param>
    /// <param name="isTemp">Whether the item is temporary</param>
    /// <param name="log">Whether it will log that it is killing the monster</param>
    public void HuntMonsterMapID(string map, int monsterMapID, string? item = null, int quant = 1, bool isTemp = true, bool log = true, bool publicRoom = false)
    {
        if (item != null && (isTemp ? Bot.TempInv.Contains(item, quant) : CheckInventory(item, quant)))
            return;
        if (!isTemp && item != null)
            AddDrop(item);
        Join(map, publicRoom: publicRoom);

        Monster? monster = Bot.Monsters.MapMonsters?.Find(m => m.MapID == monsterMapID);
        if (monster == null)
        {
            Logger($"Failed to find monsterMapID {monsterMapID} in {map}");
            return;
        }
        Jump(monster.Cell, "Left");

        if (item == null)
        {
            if (log)
                Logger($"Killing {monster.Name}");
            Bot.Kill.Monster(monster);
            Rest();
            return;
        }

        if (log)
        {
            int dynamicQuantity = isTemp ? Bot.TempInv.GetQuantity(item) : Bot.Inventory.GetQuantity(item);
            Logger($"Killing {monster.Name} for {item}, ({dynamicQuantity}/{quant}) [Temp = {isTemp}]");
        }
        while (!Bot.ShouldExit && (isTemp ? !Bot.TempInv.Contains(item, quant) : !CheckInventory(item, quant)))
        {
            if (!Bot.Combat.StopAttacking)
                Bot.Combat.Attack(monster);
            Bot.Sleep(ActionDelay);
            Rest();
        }
    }

    /// <summary>
    /// Kill Escherion for the desired item
    /// </summary>
    /// <param name="item">Item name</param>
    /// <param name="quant">Desired quantity</param>
    /// <param name="isTemp">Whether the item is temporary</param>
    public void KillEscherion(string? item = null, int quant = 1, bool isTemp = false, bool log = true, bool publicRoom = false)
    {
        if (item != null && (isTemp ? Bot.TempInv.Contains(item, quant) : CheckInventory(item, quant)))
            return;
        if (!isTemp && item != null)
            AddDrop(item);

        Join("escherion", publicRoom: publicRoom);
        Jump("Boss", "Left");

        if (item == null)
        {
            if (log)
                Logger("Killing Escherion");
            while (!Bot.ShouldExit && Bot.Monsters.MapMonsters.First(m => m.Name == "Escherion").Alive)
            {
                if (Bot.Monsters.MapMonsters.First(m => m.Name == "Staff of Inversion").Alive)
                    Bot.Hunt.Monster("Staff of Inversion");
                Bot.Combat.Attack("Escherion");
                Bot.Sleep(1000);
            }
            return;
        }

        if (log)
            Logger($"Killing Escherion for {item} ({quant}) [Temp = {isTemp}]");
        while (!Bot.ShouldExit && !CheckInventory(item, quant))
        {
            if (Bot.Monsters.MapMonsters?.FirstOrDefault(m => m.Name == "Staff of Inversion")?.Alive ?? false)
                Bot.Hunt.Monster("Staff of Inversion");
            Bot.Combat.Attack("Escherion");
            Bot.Sleep(1000);
        }
    }

    /// <summary>
    /// Kill Vath for the desired item
    /// </summary>
    /// <param name="item">Item name</param>
    /// <param name="quant">Desired quantity</param>
    /// <param name="isTemp">Whether the item is temporary</param>
    public void KillVath(string? item = null, int quant = 1, bool isTemp = false, bool log = true, bool publicRoom = false)
    {
        if (item != null && (isTemp ? Bot.TempInv.Contains(item, quant) : CheckInventory(item, quant)))
            return;
        if (item != null)
            AddDrop(item);

        Join("stalagbite", "r2", "Left");

        if (item == null)
        {
            if (log)
                Logger("Killing Escherion");
            while (!Bot.ShouldExit && Bot.Monsters.MapMonsters.First(m => m.Name == "Stalagbite").Alive)
            {
                if (Bot.Monsters.MapMonsters.First(m => m.Name == "Stalagbite").Alive)
                    Bot.Hunt.Monster("Vath");
                Bot.Combat.Attack("Stalagbite");
                Bot.Sleep(1000);
            }
            return;
        }

        if (log)
            Logger($"Killing Vath for {item} ({quant}) [Temp = {isTemp}]");
        while (!Bot.ShouldExit && !CheckInventory(item, quant))
        {
            if (Bot.Monsters.MapMonsters?.FirstOrDefault(m => m.Name == "Stalagbite")?.Alive ?? false)
                Bot.Kill.Monster("Stalagbite");
            Bot.Combat.Attack("Vath");
            Bot.Sleep(1000);
        }
    }

    /// <summary>
    /// Kill DoomKitten for the desired item
    /// </summary>
    /// <param name="item">Item name</param>
    /// <param name="quant">Desired quantity</param>
    /// <param name="isTemp">Whether the item is temporary</param>
    public void KillDoomKitten(string item, int quant = 1, bool isTemp = false, bool log = true, bool publicRoom = false)
    {
        string[] DOTClasses = {
                    "ShadowStalker of Time",
                    "ShadowWeaver of Time",
                    "ShadowWalker of Time",
                    "Infinity Knight",
                    "Interstellar Knight",
                    "Void Highlord",
                    "Dragon of Time",
                    "Timeless Dark Caster",
                    "Frostval Barbarian",
                    "Blaze Binder",
                    "DeathKnight",
                    "DragonSoul Shinobi",
                };

        if (isTemp ? Bot.TempInv.Contains(item, quant) : CheckInventory(item, quant))
            return;
        AddDrop(item);

        if (!DOTClasses.Any(c => CheckInventory(c, toInv: false)))
            Logger($" \'Damage over Time\' Class / VHL not found, stopping.", messageBox: true, stopBot: true);

        foreach (string Class in DOTClasses)
        {
            if (CheckInventory(Class))
            {
                Bot.Skills.StartAdvanced(Class, true, ClassUseMode.Base);
                break;
            }
        }

        HuntMonster("doomkitten", "Doomkitten", item, quant, isTemp, log, publicRoom);
    }

    /// <summary>
    /// Kill Xiang for the desired item
    /// </summary>
    /// <param name="item">Item name</param>
    /// <param name="quant">Desired quantity</param>
    /// <param name="ultra">Fight the ultra variant</param>
    /// <param name="isTemp">Whether the item is temporary</param>
    public void KillXiang(string item, int quant = 1, bool ultra = false, bool isTemp = false, bool log = true, bool publicRoom = false)
    {
        if (isTemp ? Bot.TempInv.Contains(item, quant) : CheckInventory(item, quant))
            return;

        if (CheckInventory("Dragon of Time"))
            Bot.Skills.StartAdvanced("Dragon of Time", true, ClassUseMode.Base);
        else if (CheckInventory("Healer (Rare)"))
            Bot.Skills.StartAdvanced("Healer (Rare)", true, ClassUseMode.Base);
        else if (CheckInventory("Healer"))
            Bot.Skills.StartAdvanced("Healer", true, ClassUseMode.Base);

        if (ultra)
        {
            KillMonster("mirrorportal", "r6", "Right", "Ultra Xiang", item, quant, isTemp, log, publicRoom);
            return;
        }

        KillMonster("mirrorportal", "r4", "Right", "Chaos Lord Xiang", item, quant, isTemp, log, publicRoom);
    }

    public void _KillForItem(string name, string item, int quantity, bool tempItem = false, bool rejectElse = false, bool log = true)
    {
        if (log)
        {
            int dynamicQuantity = tempItem ? Bot.TempInv.GetQuantity(item) : Bot.Inventory.GetQuantity(item);
            Logger($"Killing {name} for {item}, ({dynamicQuantity}/{quantity}) [Temp = {tempItem}]");
        }

        while (!Bot.ShouldExit && !CheckInventory(item, quantity))
        {
            if (!Bot.Combat.StopAttacking)
                Bot.Combat.Attack(name);
            Bot.Sleep(ActionDelay);
            if (rejectElse)
                Bot.Drops.RejectExcept(item);
        }
        Rest();
    }

    #endregion

    #region Utility

    // Whether the player is Member (set to true if neccessary during setOptions)
    public bool IsMember = false;

    /// <summary>
    /// Logs a line of text to the script log with time, method from where it's called and a message
    /// </summary>
    public void Logger(string message = "", [CallerMemberName] string caller = "", bool messageBox = false, bool stopBot = false)
    {
        Bot.Log($"[{DateTime.Now:HH:mm:ss}] ({caller})  {message}");
        if (LoggerInChat && Bot.Player.LoggedIn)
            Bot.Send.ClientModerator(message.Replace('[', '(').Replace(']', ')'), caller);
        if (messageBox & !ForceOffMessageboxes)
            Message(message, caller);
        if (stopBot)
        {
            scriptFinished = false;
            Bot.Stop(true);
        }
    }

    public void FarmingLogger(string item, int quant, [CallerMemberName] string caller = "")
        => Logger($"Farming {item} ({Bot.Inventory.GetQuantity(item)}/{quant})", caller);

    public void DebugLogger(object _this, string marker = "Checkpoint", [CallerMemberName] string? caller = null, [CallerLineNumber] int lineNumber = 0)
    {
        if (!DL_Enabled || ((DL_MarkerFilter == null ? false : DL_MarkerFilter != marker) || (DL_CallerFilter == null ? false : DL_CallerFilter != caller)))
            return;

        string _class = _this.GetType().ToString();
        string[] compiledScript = CompiledScript();

        int compiledClassLine = Array.IndexOf(compiledScript, compiledScript.First(line => line.Trim() == $"public class {_class}")) + 1;

        string[] currentScript = File.ReadAllLines(Bot.Manager.LoadedScript);
        int seperateClassLine = -1;

        if (currentScript.Any(line => line.Trim() == $"public class {_class}"))
            seperateClassLine = Array.IndexOf(currentScript, currentScript.First(line => line.Trim() == $"public class {_class}")) + 1;
        else
        {
            string[] cs_includes = currentScript.Where(x => x.StartsWith("//cs_include")).ToArray();
            foreach (string cs in cs_includes)
            {
                string[] includedScript = File.ReadAllLines(cs.Replace("//cs_include ", ""));
                if (includedScript.Any(line => line.Trim() == $"public class {_class}"))
                {
                    seperateClassLine = Array.IndexOf(includedScript, includedScript.First(line => line.Trim() == $"public class {_class}")) + 1;
                    break;
                }
            }
        }

        if (seperateClassLine == -1)
        {
            Logger("Failed trying to find seperateClassLine", "DEBUG LOGGER");
            return;
        }

        Logger($"{marker}, {_class} => {caller}, line {lineNumber - (compiledClassLine - seperateClassLine)}", "DEBUG LOGGER");
    }
    private bool DL_Enabled { get; set; } = false;
    public string? DL_CallerFilter { get; set; } = null;
    public string? DL_MarkerFilter { get; set; } = null;
    public void DL_Enable()
    {
        DL_Enabled = true;
        LoggerInChat = false;
    }
    public string[] CompiledScript() => Bot.Manager.CompiledScript.Split(
                                                                new string[] { "\r\n", "\r", "\n" },
                                                                StringSplitOptions.None);

    /// <summary>
    /// Creates a Message Box with the desired text and caption
    /// </summary>
    /// <param name="message">Message to display</param>
    /// <param name="caption">Title of the box</param>
    public void Message(string message, string caption)
    {
        if (Bot.ShowMessageBox(message, caption) == true)
            return;
    }

    /// <summary>
    /// Send a packet to the server the desired amount of times
    /// </summary>
    /// <param name="packet">Packet to send</param>
    /// <param name="times">How many times to send</param>
    public void SendPackets(string packet, int times = 1, bool toClient = false)
    {
        for (int i = 0; i < times; i++)
        {
            if (toClient)
                Bot.Send.ClientPacket(packet);
            else
                Bot.Send.Packet(packet);
            Bot.Sleep(ActionDelay * 2);
        }
    }

    /// <summary>
    /// Rest the player until full if ShouldRest = true
    /// </summary>
    public void Rest()
    {
        if (ShouldRest)
            Bot.Player.Rest(true);
    }

    /// <summary>
    /// Logs the player out and then in again to the same server. Disables Options.AutoRelogin temporarily 
    /// </summary>
    public void Relogin()
    {
        Bot.Servers.EnsureRelogin(Bot.Options.ReloginServer ?? Bot.Servers.CachedServers[0].Name);
    }

    /// <summary>
    /// Checks, and prompts for the latest Skua Version
    /// <param name="targetVersion">Current Skua Version to Check against</param>
    /// </summary>
    private void SkuaVersionChecker(string targetVersion)
    {
        if (Version.Parse(targetVersion).CompareTo(Bot.Version) <= 0)
            return;

        if (Bot.ShowMessageBox($"This script requires Skua {targetVersion} or above, " +
        "click OK to open the download page of the latest release", "Outdated Skua detected") == true)
            Process.Start("explorer", "https://github.com/BrenoHenrike/Skua/releases/latest");
        Logger($"This script requires Skua {targetVersion} or above. Stopping the script", messageBox: true, stopBot: true);
    }

    ClassType currentClass = ClassType.None;
    bool usingSoloGeneric = false;
    bool usingFarmGeneric = false;
    /// <summary>
    /// Equips either the FarmClass or SoloClass from the CanChange section at the top of CoreBots 
    /// </summary>
    /// <param name="classToUse">Type "ClassType." and then either Farm or Solo in order to select which type it should swap too</param>
    public void EquipClass(ClassType classToUse)
    {
        if (currentClass == classToUse && Bot.Skills.TimerRunning)
            return;
        switch (classToUse)
        {
            case ClassType.Farm:
                JumpWait();
                if (!usingFarmGeneric)
                {
                    if (FarmGearOn & Bot.Player.CurrentClass?.Name != FarmClass)
                    {
                        logEquip = false;
                        Bot.Sleep(ActionDelay);
                        Equip(FarmGear);
                        logEquip = true;
                    }
                    Bot.Wait.ForItemEquip(FarmClass);
                    Bot.Skills.StartAdvanced(FarmClass, true, FarmUseMode);
                    break;
                }
                Bot.Skills.StartAdvanced(Bot.Player.CurrentClass?.Name ?? "generic", false);
                break;
            default:
                JumpWait();
                if (!usingSoloGeneric)
                {
                    if (SoloGearOn & Bot.Player.CurrentClass?.Name != SoloClass)
                    {
                        logEquip = false;
                        Bot.Sleep(ActionDelay);
                        Equip(SoloGear);
                        logEquip = true;
                    }
                    Bot.Wait.ForItemEquip(SoloClass);
                    Bot.Skills.StartAdvanced(SoloClass, true, SoloUseMode);
                    break;
                }
                Bot.Skills.StartAdvanced(Bot.Player.CurrentClass?.Name ?? "generic", false);
                break;
        }
        currentClass = classToUse;
    }
    private bool logEquip = true;

    public void Equip(params string[] gear)
    {
        if (gear == null)
            return;
        JumpWait();
        foreach (string Item in gear)
        {
            if ((Item != "Weapon" && Item != "Headpiece" && Item != "Cape" && Item != "False") && CheckInventory(Item) && !Bot.Inventory.IsEquipped(Item))
            {
                Bot.Inventory.EquipItem(Item);
                Bot.Sleep(ActionDelay);
                if (logEquip)
                    Logger($"Equipped {Item}");
            }
        }
    }

    /// <summary>
    /// Switches the player's Alignment to the input Alignment type
    /// </summary>
    /// <param name="Side">Type "Alignment." and then Good, Evil or Chaos in order to select which Alignment it should swap too</param>
    public void ChangeAlignment(Alignment Side)
    {
        Bot.Send.Packet($"%xt%zm%updateQuest%{Bot.Map.RoomID}%41%{(int)Side}%");
        Bot.Sleep(ActionDelay * 2);
    }

    public bool HasAchievement(int ID, string ia = "ia0")
    {
        return Bot.Flash.CallGameFunction<bool>("world.getAchievement", ia, ID);
    }

    public void SetAchievement(int ID, string ia = "ia0")
    {
        if (!HasAchievement(ID, ia))
            Bot.Send.Packet($"%xt%zm%setAchievement%{Bot.Map.RoomID}%{ia}%{ID}%1%");
    }

    #region Save State
    public void SavedState(bool on = true)
    {
        string[] Files = Directory.GetFiles(@"Scripts\SavedState");
        string file = Files[Bot.Random.Next(0, Files.Count() - 1)];
        string[] SavedStateRNG = File.ReadAllLines(file);

        if (on)
        {
            int MinumumDelay = 180;
            int MaximumDelay = 300;
            int timerInterval = Bot.Random.Next(MinumumDelay, MaximumDelay + 1);
            int SSH = 0;
            Logger("Saved State Handler enabled");
            Bot.Send.ClientModerator("These Moderator messages about botting are client side and wont be seen by AE", "Mod-Messages");
            Bot.Handlers.RegisterHandler(5000, s =>
            {
                SSH++;
                if (SSH >= (timerInterval / 5))
                {
                    int messageSelect = Bot.Random.Next(1, SavedStateRNG.Length);
                    Bot.Send.ClientModerator($"Ignore the whisper below, this is to save your player data ({file.Split('\\').Last().Split('/').Last()})", "Saved-State");
                    Bot.Send.Whisper(Bot.Player.Username, SavedStateRNG[messageSelect][2..]);
                    timerInterval = Bot.Random.Next(MinumumDelay, MaximumDelay);
                    SSH = 0;
                }
            }, "Saved-State Handler");
        }
        else if (Bot.Handlers.CurrentHandlers.Any(handler => handler.Name == "Saved-State Handler"))
        {
            Bot.Handlers.Remove("Saved-State Handler");
            int messageSelect = Bot.Random.Next(1, SavedStateRNG.Length);
            Bot.Send.ClientModerator("Final Saved-State before the Saved State Handler is turned off", "Saved-State");
            Bot.Send.Whisper(Bot.Player.Username, SavedStateRNG[messageSelect][2..]);
            Logger("Saved State Handler disabled");
        }
    }

    public Option<bool> SkipOptions = new Option<bool>("SkipOption", "Skip this window next time", "You will be able to return to this screen via [Options] -> [Script Options] if you wish to change anything.", false);
    public bool DontPreconfigure = true;

    #endregion

    public void RunCore()
    {
        Bot.ShowMessageBox("Files that start with the word \"Core\" are not meant to be run, these are for storage. Please select the correct script.", "Core File Info");
        Bot.Stop(true);
    }

    #endregion

    #region Map

    /// <summary>
    /// Jumps to the desired cell and set spawn point
    /// </summary>
    /// <param name="cell">Cell to jump to</param>
    /// <param name="pad">Pad to jump to</param>
    public void Jump(string cell = "Enter", string pad = "Spawn", bool ignoreCheck = false)
    {
        Bot.Player.SetSpawnPoint(cell, pad);
        if (!ignoreCheck && Bot.Player.Cell == cell)
            return;
        Bot.Map.Jump(cell, pad);
        Bot.Sleep(200);
        if (Bot.Player.X != 480 || Bot.Player.Y != 275 || !inPublicRoom())
            return;

        if (cell.Contains("Enter"))
        {
            Bot.Map.Jump(cell, "Spawn");
            return;
        }

        string[] pads =
        {
            "Left",
            "Right",
            "Top",
            "Bottom",
            "Spawn",
            "Up",
            "Down",
            "Center"
        };

        foreach (string _pad in pads)
        {
            if (_pad == pad)
                continue;
            Bot.Map.Jump(cell, _pad);
            Bot.Sleep(200);
            if (Bot.Player.X != 480 || Bot.Player.Y != 275)
                break;
        }
    }

    /// <summary>
    /// Searches for a cell without monsters and jumps to it. If non is found it jumps twice in its current cell. <see cref="ExitCombatDelay"/>
    /// </summary>
    public void JumpWait()
    {
        if (!Bot.Player.InCombat)
            return;

        List<string> MonsterCells = Bot.Monsters.MapMonsters.Select(monster => monster.Cell).ToList();

        if (!MonsterCells.Contains(Bot.Player.Cell))
            return;
        string[] blankCells = new[] { "wait", "blank" };
        string cell = string.Empty;
        string pad = string.Empty;
        bool jumpTwice = false;

        if (!MonsterCells.Contains("Enter"))
        {
            cell = "Enter";
            pad = "Spawn";
        }
        else
        {
            foreach (string _cell in Bot.Map.Cells)
            {
                if (_cell == Bot.Player.Cell || blankCells.Contains(_cell.ToLower()) || _cell.ToLower().Contains("cut"))
                    continue;
                if (!MonsterCells.Contains(cell))
                {
                    cell = _cell;
                    pad = "Left";
                    break;
                }
            }
        }

        if (string.IsNullOrEmpty(cell) || string.IsNullOrEmpty(pad))
        {
            cell = Bot.Player.Cell;
            pad = Bot.Player.Pad;
            jumpTwice = true;
        }

        if (lastJumpWait != $"{Bot.Map.Name} | {cell} | {pad}" || Bot.Player.InCombat)
        {
            Jump(cell, pad, true);
            if (jumpTwice)
                Jump(cell, pad, true);

            lastJumpWait = $"{Bot.Map.Name} | {cell} | {pad}";

            Bot.Sleep(ExitCombatDelay - 200);
            Bot.Wait.ForCombatExit();
        }
        Bot.Combat.Exit();
    }
    private string lastJumpWait = "";

    /// <summary>
    /// Joins a map and does bonus steps for said map if needed
    /// </summary>
    /// <param name="map">The name of the map</param>
    /// <param name="cell">The cell to jump to</param>
    /// <param name="pad">The pad to jump to</param>
    /// <param name="publicRoom">Whether or not it should be a public room, if PrivateRoom is on in the CanChange section on the top of CoreBots</param>
    /// <param name="ignoreCheck">If set to true, the bot will not check if the player is already in the given room</param>
    public void Join(string map, string cell = "Enter", string pad = "Spawn", bool publicRoom = false, bool ignoreCheck = false)
    {
        map = map.ToLower() == "tercess" ? "tercessuinotlim" : map.ToLower(); map = map.Replace(" ", "");
        if (Bot.Map.Name != null && Bot.Map.Name.ToLower() == map && !ignoreCheck)
            return;

        bool AggroMonsters = false;
        if (Bot.Options.AggroMonsters)
        {
            AggroMonsters = true;
            Bot.Options.AggroMonsters = false;
        }

        switch (map)
        {
            default:
                JumpWait();
                tryJoin();
                break;

            case "tercessuinotlim":
                Bot.Map.Jump("m22", "Left");
                tryJoin();
                break;

            case "doomvault":
                JumpWait();
                Bot.Quests.UpdateQuest(3008);
                tryJoin();
                break;

            case "doomvaultb":
                JumpWait();
                Bot.Quests.UpdateQuest(3008);
                SetAchievement(18);
                Bot.Quests.UpdateQuest(3004);
                tryJoin();
                break;

            case "prison":
                joinedPrison = true;
                JumpWait();
                tryJoin();
                joinedPrison = false;
                break;

            case "hyperium":
                JumpWait();
                tryJoin();
                Bot.Send.Packet($"%xt%zm%serverUseItem%{Bot.Map.RoomID}%+%5041%525,275%hyperium%");
                break;
        }

        Jump(cell, pad);
        Bot.Sleep(200);

        if (AggroMonsters)
            Bot.Options.AggroMonsters = true;

        void tryJoin()
        {
            for (int i = 0; i < 20; i++)
            {
                Bot.Map.Join((publicRoom && PublicDifficult) || !PrivateRooms ? map : $"{map}-{PrivateRoomNumber}", cell, pad, ignoreCheck);
                Bot.Wait.ForMapLoad(map);

                string? currentMap = Bot.Map.Name;
                if (!String.IsNullOrEmpty(currentMap) && currentMap.ToLower() == map)
                    break;

                if (i == 19)
                    Logger($"Failed to join {map}");
            }
        }
    }

    /// <summary>
    /// Sends a getMapItem packet for the specified item
    /// </summary>
    /// <param name="itemID">ID of the item</param>
    /// <param name="quant">Desired quantity of the item</param>
    /// <param name="map">Map where the item is</param>
    public void GetMapItem(int itemID, int quant = 1, string? map = null)
    {
        if (map != null)
            Join(map);
        Bot.Sleep(ActionDelay);
        List<ItemBase> tempItems = Bot.TempInv.Items;
        ItemBase? newItem = null;
        bool found = false;
        for (int i = 0; i < quant; i++)
        {
            Bot.Map.GetMapItem(itemID);
            Bot.Sleep(1000);
            if (!found && Bot.TempInv.Items.Except(tempItems).Count() > 0)
            {
                newItem = Bot.TempInv.Items.Except(tempItems).First();
                found = true;
            }
        }
        if (quant > 1 && newItem != null)
        {
            int t = 0;
            while (Bot.TempInv.GetQuantity(newItem.Name) < quant ||
                (Bot.TempInv.TryGetItem(newItem.Name, out ItemBase? _item) && _item != null &&
                (Bot.TempInv.GetQuantity(newItem.Name) < _item.MaxStack)))
            {
                Bot.Map.GetMapItem(itemID);
                Bot.Sleep(1000);
                t++;

                if (t > (quant + 10))
                    break;
            }
        }

        Logger($"Map item {itemID}({quant}) acquired");
    }

    /// <summary>
    /// This method is used to move between PvP rooms
    /// </summary>
    /// <param name="mtcid">Last number of the mtcid packet</param>
    /// <param name="cell">Cell you want to be</param>
    /// <param name="moveX">X position of the door</param>
    /// <param name="moveY">Y position of the door</param>
    public void PvPMove(int mtcid, string cell, int moveX = 828, int moveY = 276)
    {
        while (!Bot.ShouldExit && Bot.Player.Cell != cell)
        {
            Bot.Send.Packet($"%xt%zm%mv%{Bot.Map.RoomID}%{moveX}%{moveY}%8%");
            Bot.Sleep(2500);
            Bot.Send.Packet($"%xt%zm%mtcid%{Bot.Map.RoomID}%{mtcid}%");
        }
    }

    /// <summary>
    /// Checks if the room you're in is a public room or not
    /// </summary>
    /// <returns>If room number is less than 1000</returns>
    public bool inPublicRoom()
    {
        Bot.Wait.ForMapLoad(Bot.Map.Name);
        if (!Int32.TryParse(Bot.Map.FullName.Split('-').Last(), out int nr))
            nr = 1;
        return nr < 1000;
    }

    //public bool isSeasonalMapActive(string map, bool log = true)
    //{
    //    map = map.ToLower().Replace(" ", "");
    //    if (Bot.Map.Name != null && Bot.Map.Name.ToLower() == map)
    //        return true;

    //    bool AggroMonsters = false;
    //    if (Bot.Options.AggroMonsters)
    //    {
    //        AggroMonsters = true;
    //        Bot.Options.AggroMonsters = false;
    //    }

    //    JumpWait();
    //    Bot.Events.ExtensionPacketReceived += PacketListener;
    //    bool seasonalMessageProc = false;

    //    for (int i = 0; i < 20; i++)
    //    {
    //        Bot.Map.Join(!PrivateRooms ? map : $"{map}-{PrivateRoomNumber}");
    //        Bot.Wait.ForMapLoad(map);

    //        string? currentMap = Bot.Map.Name;
    //        if (!String.IsNullOrEmpty(currentMap) && currentMap.ToLower() == map)
    //            break;

    //        if (seasonalMessageProc)
    //        {
    //            Bot.Events.ExtensionPacketReceived -= PacketListener;
    //            return false;
    //        }

    //        if (i == 19)
    //            Logger($"Failed to join {map}");
    //    }

    //    if (AggroMonsters)
    //        Bot.Options.AggroMonsters = true;

    //    Bot.Events.ExtensionPacketReceived -= PacketListener;
    //    return true;

    //    void PacketListener(dynamic packet)
    //    {
    //        string type = packet;
    //        switch (type)
    //        {
    //            case "%xt%warning%-1%\"mogloween\" is not available.%":
    //                if (log)
    //                    Logger($"Map \"map\" is currently disabled.");
    //                seasonalMessageProc = true;
    //                break;
    //        }

    //    }
    //}

    #endregion

    #region Using Local Files

    /// <summary>
    /// Returns the file path the user has Skua stored in.
    /// </summary>
    public string AppPath = Path.GetDirectoryName(AppContext.BaseDirectory) ?? string.Empty;

    private void ReadMe()
    {
        string readMePath = AppPath + @"\ReadMeV1.txt";
        if (File.Exists(readMePath))
            return;

        // Popup
        bool? result = Bot.ShowMessageBox(
            "Welcome to Skua's Master Bots!\n" +
            "These bots are a tad different from what you might be used to with Grimoire or other botting clients.\n\n" +
            "Its highly recommended to read the ReadMe.txt file if this is your first time running one of our bots, or if you just started.\n" +
            "There are plenty of things that are useful to know there, which arent immediately obvious.\n\n" +
            "This messagebox will not appear again after you close it.\n" +
            $"You will still be able to read the file later by going to [{readMePath}]\n" +
            "If you do see it again at a later moment, there might have just been a update to the ReadMe, in which case you can ignore this message.\n\n" +
            "Click OK to open the ReadMe.txt",

            "READ ME");

        // Creating ReadMe.txt
        string[] ReadMe =
        {
            "Welcome and thank you for using Skua's Master Bots!",
            "",
            "=== Basic Information ===",
                "These bots are a tad different from what you might be used to with Grimoire or other botting clients.",
                "All our bots are \"Master Bots\" and thus will do everything you might need it to do in order to farm the item of your choice.",
                "This includes but is not limited to:",
                "· Finishing questlines to unlock farms, maps or get a specific items.",
                "· Using bypasses so you dont have to do questlines in order to continue farming.",
                "· Do other farms that you might need to do in order to farm the item of your choice (I.E. Get NSoD as well when farming for HBSoD).",
                "",
                "== Skills ==",
                    "We also have a big file that contains 95% of all classes with one or multiple skill combinations for different scenarios.",
                    "So you'll know that your class will use a optimized combo without you having to set the skills yourself.",
                    "These combos are ofcourse always up for debate and we are happy to change them based off of community input.",
                    "If you wish to play with these for yourself, the easiest way to do so is to use the \"Advanced Skills\" window, which can be found in the top row of Skua and then Skills.",
                    "",
                "== File Naming ==",
                    "Whilst using our bots, you might notice that there are files that start with the word \"Core\", these files are storage for methods that we use in our bots.",
                    "These bots are not meant to be run and wont do anything usefull for you. If you do, expect a pop-up that tells you the exact same thing.",
                    "Another file naming convention is files that start with a \"0\" (zero), these files are usually inside a folder.",
                    "These files can be run and will usually do everything in the folder for you, as a sort of combo bot. Like farming everything for VHL and buying + leveling it too.",
                    "",
                "== Bugs and Bot Requests ==",
                    "As much as we try, bugs pop up from time to time.",
                    "If you find one, please report it to us via the form which can be found near the bottom of the Scripts menu.",
                    "This same form will also be used to request new features or bots.",
                    "",
                "== GitHub Prompt ==",
                    "You might have noticed how Skua asks you to authorize with a GitHub account when you first run Skua.",
                    "This is so that Skua can update the bots from our GitHub repository.",
                    "Without this you are bound to a 50 requests p/h limiter that is shared with everyone else who didn't authortize.",
                    "Considering that you already send 3 requests on startup, you can see how this can be reached quickly.",
                    "Therefore it's highly recommended to do the authorization, as you will then have your own limiter instead of a shared one.",
                    "",
                    "",
            "=== Plugins ===",
                "== CoreBots Options ==",
                    "Now, this plugin is where you customize a lot of the things that happen for all the bots. It's highly recommended to open this one up and set some options.",
                    "I highly recommend setting all your preffered options in the Generic tab, as this houses the important ones.",
                    "You can ofcourse also check our the other options and set them to what you want too.",
                    "It's recommended to stay in private rooms, as public rooms have a higher chance of getting you banned.",
                    "It should also be noted that Skua version 4.1.3, comes with a outdated version of the \"CoreBots Options\" plugin.",
                    "You can find the latest here https://github.com/LordExelot/Skua-CBO/releases/tag/v1",
                        "Within the discord this plugin is often reffered to as CBO.",
                    "",
                "== Wait Timeout Override ==",
                    "This is a plugin that allows you to override some default data for Skua, it's used to modify how long Skua waits before it considers a task to be failed.",
                    "You don't have to touch these values in most cases, it's mostly used for debugging.",
                    "",
                    "",
                "=== The End ===",
                    "Thanks for reading, I hope it wasn't too much of a bore!",
                    "",
                "== Contact ==",
                    "If you wish to contact us, you can find us on our discord server: https://discord.io/AQWBots/",
                    "",
                "== Credits ==",
                    "Active:",
                        "· BrenoHenrike\t- He took over Skua development when Rodit disappeared. Breno also build the framework that these Master Bots now use.",
                        "· Lord Exelot\t- Lead Developer/Head of the Skua Master Bot team. Expanded the framework and spearheaded the development of the Master Bots.",
                        "· Tato\t\t\t- Major contributor to the Master Bots and a lot of bug fixes.",
                        "· Bogajl\t\t- Major contributor to the Master Bots.",
                        "· Hao\t\t\t- Considerable contributor to the Master Bots.",
                        "· [S] Elune\t\t- Contributor to the Master Bots, bug fixes and primary bug hunter.",
                        "· SunnyD\t\t- Contributor to the Master Bots.",
                        "· Novar\t\t\t- Minor Contributor to the Master Bots.",
                        "· FishingKing\t- Minor Contributor the the Master Bots",
                    "Inactive:",
                        "· Rodit\t\t\t- Creator of RBot.",
                        "· Purple\t\t- Contributor to RBot.",
                        "· Vladimir\t\t- Major contributor to the Master Bots and bug fixes.",
                        "· usuckshit\t\t- Considerable contributor to the Master Bots.",
                        "· Hadmos\t\t- Considerable contributor to the Master Bots.",
                        "· Eso\t\t\t- Minor bug fixes to the Master Bots.",
                        "· .Richie\t\t- Minor contributor to the Master Bots.",
                        "· ToxlcChain\t- Minor contributor to the Master Bots.",
                        "· Ferdy\t\t\t- Minor contributor to the Master Bots.",
                    "Thanks to you, for reading this far down. ReadMe's are usually a drag so I tried to keep it to the point.",
                    "And thanks to everyone who has put time and effort RBot/Skua and the Master Bots! ~ Exelot",
        };
        File.WriteAllLines(readMePath, ReadMe);

        // Opening ReadMe.txt
        if (result == true)
            Process.Start("explorer", readMePath);
    }

    private void CollectData(bool onStartup)
    {
        string UserID = "null";
        bool genericData = false;
        bool scriptNameData = false;
        bool stopTimeData = false;
        FileSetup();

        if (!genericData || UserID == "null")
            return;

        // If on stop and it's not allowed, return
        if (!onStartup && !stopTimeData)
            return;

        // Init HttpClient to send the request
        HttpClient client = new HttpClient();

        // Build the Field Ids and Answers dictionary object
        var bodyValues = new Dictionary<string, string>
        {
            {"entry.1700030786", UserID},
            {"entry.942504290", onStartup ? "Start" : "Stop"},
        };

        // If allowed, send scriptNameData
        if (scriptNameData)
        {
            string botPath = Bot.Manager.LoadedScript.Split("Scripts").Last().Replace('/', '\\').Substring(1);

            if (botPath.StartsWith("Nulgath\\"))
                botPath.Replace("Nulgath\\", "Nation\\");

            string[] allowedPathStarters =
            {
                "ArmyOnly",
                "Chaos",
                "Dailies",
                "Darkon",
                "Enhancement",
                "Evil",
                "Farm",
                "Good",
                "Hollowborn",
                "Legion",
                "Nation",
                "Nulgath",
                "Other",
                "Prototypes",
                "Seasonal",
                "Story",
                "WIP"
            };

            if (!allowedPathStarters.Any(x => botPath.StartsWith(x)))
                botPath = "CustomPath\\" + botPath.Split("\\").Last();

            bodyValues.Add("entry.1597948191", botPath);
        }

        // If allowed, send scriptInstanceData
        if (stopTimeData)
        {
            if (ScriptInstanceID == 0)
                ScriptInstanceID = Bot.Random.Next(1, Int32.MaxValue);

            bodyValues.Add("entry.1361306892", ScriptInstanceID.ToString());
        }

        // Encode object to application/x-www-form-urlencoded MIME type
        var content = new FormUrlEncodedContent(bodyValues);

        // Post the request
        // https://docs.google.com/forms/u/0/d/e/1FAIpQLSe7nkDQSKL55-g1MQQ-31jqbpVh8g65jMEJCMw7wbdjQugbVg/formResponse
        client.PostAsync(
            "https://docs.google.com/forms/d/e/" +
            "1FAIpQLSe7nkDQSKL55-g1MQQ-31jqbpVh8g65jMEJCMw7wbdjQugbVg" +
            "/formResponse",
            content);

        void FileSetup()
        {
            string path = AppPath + @"/DataCollectionSettings.txt";
            if (!File.Exists(path))
            {
                DialogResult consent = Bot.ShowMessageBox(
                    "We wish to gather data, in an effort to keep us motivated, knowing people use what we make.\n\n" +
                    "We would gather the following things:\n" +
                    "· An anon userID we generate which will allows us to know our active user count.\n" +
                    "· Start time of scripts.\n" +
                    "· What script is being run.\n" +
                    "· Stop time of scripts, this would be paired with the point below\n" +
                    "· Script Instance ID, a random number that allows us to match start- and stoptime.\n\n" +
                    "Consent for this is requiered, and puts my mind at ease. " +
                    "So you will be able to select what data is being send and what is not.\n\n" +
                    "Select \"Full\" to give full consent.\n" +
                    "Select \"Partial\" to give partial consent, you will then get a couple more pop-up boxes where you can select your preferences.\n" +
                    "Select \"None\" to not consent, we will then gather no data whatsoever.",

                    "Data Collection",
                    "Full", "Partial", "None"
                );

                if (consent.Text == "Full")
                {
                    genericData = true;
                    scriptNameData = true;
                    stopTimeData = true;
                }
                else if (consent.Text is "Cancel" or "None")
                {
                    genericData = false;
                    scriptNameData = false;
                    stopTimeData = false;
                }
                else if (consent.Text == "Partial")
                {
                    DialogResult nonOptional = Bot.ShowMessageBox(
                        "The following two points are not optional:\n" +
                        "· An anon userID we generate which will allows us to know our active user count.\n" +
                        "· Start time of scripts.\n\n" +
                        "If you accept this, select \"Yes\".\n" +
                        "If you dont accept this, select \"No\", and we will not gather data whatsoever.",

                        "Non-Optional Data",
                        "Yes", "No"
                    );

                    if (nonOptional.Text == "No")
                    {
                        genericData = false;
                        scriptNameData = false;
                        stopTimeData = false;
                    }
                    else if (nonOptional.Text == "Yes")
                    {
                        DialogResult scriptName = Bot.ShowMessageBox(
                            "Do you give consent to send us the following data-point:\n" +
                            "· What script is being run.\n\n" +
                            "This allows us to know what scripts are populair",

                            "Script Name",
                            "Yes", "No"
                        );

                        DialogResult stopTime = Bot.ShowMessageBox(
                            "Do you give consent to send us the following data-points:\n" +
                            "· Stop time of scripts, this would be paired with the point below" +
                            "· Script Instance ID, a random number that allows us to match start- and stoptime.\n\n" +
                            "Allowing us to have this data means we'll know how long a script has been running.",

                            "Stop Time & Script Instance ID",
                            "Yes", "No"
                        );

                        genericData = true;
                        scriptNameData = scriptName.Text == "Yes";
                        stopTimeData = stopTime.Text == "Yes";
                    }
                }

                if (genericData)
                {
                    UserID = Bot.Random.Next(100000001, Int32.MaxValue).ToString();
                }

                string[] fileContent =
                {
                    $"UserID: {UserID}",
                    $"genericDataConsent: {genericData}",
                    $"scriptNameConsent: {scriptNameData}",
                    $"stopTimeConsent: {stopTimeData}"
                };

                File.WriteAllLines(path, fileContent);

                Bot.ShowMessageBox(
                    "If you wish to change these settings, you can easily modify them in the following file:\n" +
                    $"[{path}]",

                    "File Location"
                );
            }
            else
            {
                string[] savedSettings = File.ReadAllLines(path);

                UserID = ConsentString("UserID");
                genericData = ConsentBool("genericDataConsent");
                scriptNameData = ConsentBool("scriptNameConsent");
                stopTimeData = ConsentBool("stopTimeConsent");

                string ConsentString(string input)
                    => (savedSettings.FirstOrDefault(x => x.StartsWith(input)) ?? $"{input}: ").Split(": ").Last();
                bool ConsentBool(string input)
                    => ConsentString(input) == "True";
            }
        }
    }
    private int ScriptInstanceID = 0;

    private void ReadCBO()
    {
        //Generic
        if (CBOBool("PrivateRooms", out bool _PrivateRooms))
            PrivateRooms = _PrivateRooms;
        if (CBOInt("PrivateRoomNr", out int _PrivateRoomNumber))
            PrivateRoomNumber = _PrivateRoomNumber;
        if (CBOBool("PublicDifficult", out bool _PublicDifficult))
            PublicDifficult = _PublicDifficult;
        if (CBOBool("BankMiscAC", out bool _BankMiscAC))
            BankMiscAC = _BankMiscAC;
        if (CBOBool("LoggerInChat", out bool _LoggerInChat))
            LoggerInChat = _LoggerInChat;

        if (CBOString("StopLocationSelect", out string _StopLocationSelect))
            CustomStopLocation = _StopLocationSelect;

        if (CBOString("SoloClassSelect", out string _SoloClassSelect))
            SoloClass = String.IsNullOrEmpty(_SoloClassSelect) ? "Generic" : _SoloClassSelect;
        if (CBOBool("SoloEquipCheck", out bool _SoloGearOn))
            SoloGearOn = _SoloGearOn;
        if (CBOString("SoloModeSelect", out string _SoloModeSelect))
            SoloUseMode = (ClassUseMode)Enum.Parse(typeof(ClassUseMode), String.IsNullOrEmpty(_SoloModeSelect) ? "Base" : _SoloModeSelect);

        if (CBOString("FarmClassSelect", out string _FarmClassSelect))
            FarmClass = String.IsNullOrEmpty(_FarmClassSelect) ? "Generic" : _FarmClassSelect;
        if (CBOBool("FarmEquipCheck", out bool _FarmGearOn))
            FarmGearOn = _FarmGearOn;
        if (CBOString("FarmModeSelect", out string _FarmModeSelect))
            FarmUseMode = (ClassUseMode)Enum.Parse(typeof(ClassUseMode), String.IsNullOrEmpty(_FarmModeSelect) ? "Base" : _FarmModeSelect);

        //Advanced
        if (CBOBool("MessageBoxCheck", out bool _ForceOffMessageboxes))
            ForceOffMessageboxes = _ForceOffMessageboxes;
        if (CBOBool("RestCheck", out bool _ShouldRest))
            ShouldRest = _ShouldRest;
        if (CBOBool("AntiLag", out bool _AntiLag))
            AntiLag = _AntiLag;

        if (CBOInt("ActionDelay", out int _ActionDelay))
            ActionDelay = _ActionDelay;
        if (CBOInt("ExitCombatNr", out int _ExitCombatDelay))
            ExitCombatDelay = _ExitCombatDelay;
        if (CBOInt("HuntDelayNr", out int _HuntDelay))
            HuntDelay = _HuntDelay;
        if (CBOInt("QuestTriesNr", out int _AcceptandCompleteTries))
            AcceptandCompleteTries = _AcceptandCompleteTries;
        if (CBOInt("QuestMaxNr", out int _LoadedQuestLimit))
            LoadedQuestLimit = _LoadedQuestLimit;

        //Class Equipment
        List<string> _SoloGear = new List<string>();
        if (CBOString("Helm1Select", out string _Helm1))
            _SoloGear.Add(_Helm1);
        if (CBOString("Armor1Select", out string _Armor1))
            _SoloGear.Add(_Armor1);
        if (CBOString("Cape1Select", out string _Cape1))
            _SoloGear.Add(_Cape1);
        if (CBOString("Weapon1Select", out string _Weapon1))
            _SoloGear.Add(_Weapon1);
        if (CBOString("Pet1Select", out string _Pet1))
            _SoloGear.Add(_Pet1);
        if (CBOString("GroundItem1Select", out string _GroundItem1))
            _SoloGear.Add(_GroundItem1);
        SoloGear = _SoloGear.ToArray();

        List<string> _FarmGear = new List<string>();
        if (CBOString("Helm2Select", out string _Helm2))
            _FarmGear.Add(_Helm2);
        if (CBOString("Armor2Select", out string _Armor2))
            _FarmGear.Add(_Armor2);
        if (CBOString("Cape2Select", out string _Cape2))
            _FarmGear.Add(_Cape2);
        if (CBOString("Weapon2Select", out string _Weapon2))
            _FarmGear.Add(_Weapon2);
        if (CBOString("Pet2Select", out string _Pet2))
            _FarmGear.Add(_Pet2);
        if (CBOString("GroundItem2Select", out string _GroundItem2))
            _FarmGear.Add(_GroundItem2);
        FarmGear = _FarmGear.ToArray();

        //Best set order modification
        string[] bestSet = {
            "Necrotic Sword of Doom",
            "Polly Roger",
            "Head of the legion Beast",
            "Fire Champion's Armor",
            "Awescended Omni Wings"
        };
        if (SoloGear.All(x => bestSet.Contains(x)))
            SoloGear = bestSet.Concat(new[] { _GroundItem1 }).ToArray();
        if (FarmGear.All(x => bestSet.Contains(x)))
            FarmGear = bestSet.Concat(new[] { _GroundItem2 }).ToArray();
    }

    public bool CBO_Active() => File.Exists(AppPath + $@"\options\CBO_Storage({Bot.Player.Username}).txt");

    public bool CBOString(string Name, out string output)
    {
        if (!CBO_Active())
        {
            output = "";
            return false;
        }
        output = (CBOList.FirstOrDefault(x => x.StartsWith(Name)) ?? $".: fail").Split(": ")[1];
        return output != "fail";
    }
    public bool CBOBool(string Name, out bool output)
    {
        if (!CBOString(Name, out string str))
        {
            output = false;
            return false;
        }
        output = str == "True";
        return true;
    }
    public bool CBOInt(string Name, out int output)
    {
        if (!CBOString(Name, out string str) || !int.TryParse(str, out int toReturn))
        {
            output = 0;
            return false;
        }
        output = toReturn;
        return true;
    }

    private List<string> CBOList = new();

    #endregion
}

public enum Alignment
{
    Good = 1,
    Evil = 2,
    Chaos = 3
}

public enum ClassType
{
    Solo,
    Farm,
    None
}
