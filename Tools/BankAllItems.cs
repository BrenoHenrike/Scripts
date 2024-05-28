/*
name: BankAllItems
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
using System.Collections.Generic;
using System.Linq;
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
        new Option<bool>("Inventory", "InventoryACBank", "Bank all Ac Inventory Items", true),
        new Option<bool>("House", "HouseACBank", "Bank all Ac House Items", true),
        new Option<bool>("BanknonAc", "BanknonAc", "Bank non-AC items", false),
        new Option<string>("BlackList", "BlackList Items", "Fill in the items teh bot *shouldn't* bank, split with a , (comma).", "")
    };


    public void ScriptMain(IScriptInterface bot)
    {

        Core.SetOptions();

        BankAll(Bot.Config!.Get<bool>("Inventory"), Bot.Config!.Get<bool>("House"), Bot.Config!.Get<bool>("BanknonAc"), Bot.Config!.Get<string>("BlackList"));

        Core.SetOptions(false);
    }

    public void BankAll(bool Inventory, bool House, bool BanknonAc, string BlackList)
    {
        var blackListedItems = new HashSet<string>();

        // Split the BlackList string into an array
        if (!string.IsNullOrEmpty(BlackList))
        {
            // Split the BlackList string into an array
            blackListedItems = new HashSet<string>(BlackList.Split(',').Select(item => item.Trim()));
        }

        // Add additional items to the blacklist
        blackListedItems.Add(Core.SoloClass);
        blackListedItems.UnionWith(Core.SoloGear);
        blackListedItems.Add(Core.FarmClass);
        blackListedItems.UnionWith(Core.FarmGear);

        Core.Logger($"BlackList: {string.Join(", ", blackListedItems.Where(item => !string.IsNullOrEmpty(item)))}");

        // Bank inventory items
        if (Inventory)
        {
            BankItems(Bot.Inventory.Items, blackListedItems);
            Core.Logger($"Inventory Items: {(blackListedItems.Any() ? "✅" : "No items blacklisted")}");
        }

        // Bank house items
        if (House)
        {
            BankItems(Bot.House.Items, blackListedItems, true);
            Core.Logger($"House Items: {(blackListedItems.Any() ? "✅" : "No items blacklisted")}");
        }
    }

    private void BankItems(IEnumerable<InventoryItem> Items, HashSet<string> BlackList, bool IsForHouse = false, bool BankNonAc = false)
    {
        bool logged = false;

        foreach (var item in Items)
        {
            var itemName = item.Name;

            // Skip if item is equipped or blacklisted, or if it shouldn't be banked and isn't AC
            if (item.Equipped || BlackList.Contains(itemName) || (!BankNonAc && !item.Coins))
                continue;

            if (BankNonAc && Bot.Bank.FreeSlots == 0 && !item.Coins)
            {
                if (!logged)
                {
                    Core.Logger($"{Core.Username()}'s Bank is full");
                    logged = true;
                }
                continue;
            }

            // Bank item based on its type
            if (IsForHouse)
                Core.ToHouseBank(itemName);
            else
                Core.ToBank(itemName);

            Core.Sleep();
        }
    }

}