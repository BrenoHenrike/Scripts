/*
name: BankAllItems
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
using System.Linq.Expressions;
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class BankAllItems
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public bool DontPreconfigure = true;
    public string OptionsStorage = "BankAllBlackList";
    public List<IOption> Options = new()
    {
        new Option<string>("BlackList", "BlackList Items", "Fill in the items teh bot *shouldn't* bank, split with a , (comma)."),
        new Option<bool>("BanknonAc", "BanknonAc", "Bank non-AC items", false),
        new Option<bool>("Inventory", "InventoryACBank", "Bank all Ac Inventory Items", true),
        new Option<bool>("House", "HouseACBank", "Bank all Ac House Items", true)
    };


    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        BankAll();

        Core.SetOptions(false);
    }

    public void BankAll()
    {
        // Initialize the list of blacklisted items
        var blackListedItems = new HashSet<string> { "Treasure Potion" };
        blackListedItems.UnionWith(Core.SoloGear);
        blackListedItems.UnionWith(Core.FarmGear);

        // Check for BlackList from Bot.Config
        var configBlackList = Bot.Config?.Get<string>("BlackList");
        if (!string.IsNullOrEmpty(configBlackList))
        {
            blackListedItems.UnionWith(configBlackList.Split(',').Select(item => item.Trim()));
        }

        // Wait for the player to load before joining "whitemap"
        Bot.Wait.ForMapLoad("battleon");
        Bot.Wait.ForTrue(() => Bot.Player.Loaded, 30);
        Core.Join("whitemap");

        // Bank inventory items
        BankItems(Bot.Inventory.Items, blackListedItems, Bot.Config?.Get<bool>("BanknonAc") == true);

        Core.Logger("Finished inventory items, onto house items.");

        // Bank house items
        BankItems(Bot.House.Items, blackListedItems, Bot.Config?.Get<bool>("HouseACBank") == true);
    }

    private void BankItems(IEnumerable<InventoryItem> items, HashSet<string> blackList, bool bankNonAc)
    {
        bool logged = false;

        foreach (var item in items)
        {
            var itemName = item.Name; // Store the item name in a variable for better performance

            if (item.Equipped || blackList.Contains(itemName))
                continue;

            if (Bot.Bank.FreeSlots == 0 && !item.Coins)
            {
                if (!logged)
                {
                    Core.Logger($"{Bot.Player.Username}'s Bank is full");
                    logged = true;
                }
                continue;
            }

            if (bankNonAc && !item.Coins)
                Core.ToBank(itemName);
            else if (item.Coins)
                Core.ToBank(itemName);

            Core.Sleep();
        }
    }



}

