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

        BankAll(
            Bot.Config!.Get<bool>("Inventory"),
            Bot.Config!.Get<bool>("House"),
            Bot.Config!.Get<bool>("BanknonAc"),
            Bot.Config!.Get<string>("BlackList") ?? string.Empty
        );
        Core.SetOptions(false);
    }

    public void BankAll(bool inventory, bool house, bool bankNonAc, string blackList)
    {
        bool logged = false;

        var itemsToBank = new List<(IEnumerable<InventoryItem> Items, bool IsForHouse)>();

        if (inventory)
            itemsToBank.Add((Bot.Inventory.Items, false));

        if (house)
            itemsToBank.Add((Bot.House.Items, true));

        var blackListedItems = new HashSet<string>();

        if (!string.IsNullOrEmpty(blackList))
            blackListedItems = new HashSet<string>(blackList.Split(',')
                                                    .Select(item => item.Trim()));

        blackListedItems.Add(Core.SoloClass);
        blackListedItems.UnionWith(Core.SoloGear);
        blackListedItems.Add(Core.FarmClass);
        blackListedItems.Add("Treasure Potion");
        blackListedItems.UnionWith(Core.FarmGear);
        blackListedItems.UnionWith(Core.BankingBlackList);

        int BlackListCount = blackListedItems.Count;
        Core.Logger($"[{blackListedItems.Where(x => Bot.Inventory.Contains(x)).Count()}x Bag Spaces used] BlackList: {string.Join(", ", blackListedItems.Where(item => !string.IsNullOrEmpty(item)))}");
        foreach (var (items, isForHouse) in itemsToBank)
        {
            var filter = items.Where(item => !blackListedItems.Contains(item.Name)
                                             && (bankNonAc || item.Coins) && !item.Equipped);
            foreach (var item in filter)
            {
                if (Bot.Inventory.Contains(item.ID))
                    BlackListCount--;

                if (bankNonAc && Bot.Bank.FreeSlots == 0 && !item.Coins)
                {
                    if (!logged)
                    {
                        Core.Logger($"{Core.Username()}'s Bank is full");
                        logged = true;
                    }
                    continue;
                }

                // Bank item based on its type (house or inventory)
                if (isForHouse)
                {
                    if (item.Coins && bankNonAc)
                    {
                        Bot.House.EnsureToBank(item.ID);
                        Bot.Wait.ForTrue(() => Bot.House.EnsureToBank(item.ID), 20);
                    }
                    else
                    {
                        Core.ToHouseBank(item.ID);
                    }
                }
                else
                {
                    if (item.Coins && bankNonAc)
                    {
                        Bot.Inventory.EnsureToBank(item.ID);
                        Bot.Wait.ForTrue(() => Bot.Inventory.EnsureToBank(item.ID), 20);
                    }
                    else
                    {
                        Core.ToBank(item.ID);
                    }
                }
            }

            Core.Logger($"{(isForHouse ? "House" : "Inventory")} Items: {(filter.Any() ? "✅" : "No items blacklisted")}");
        }
    }

}
