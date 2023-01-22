/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class BankAllItems
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface bot)
    {
        BankAll();
    }

    public void BankAll()
    {
        bool logged = false;
        List<string> blackListedItems = new() { Core.SoloClass, Core.FarmClass, "Treasure Potion" };
        blackListedItems.AddRange(Core.SoloGear);
        blackListedItems.AddRange(Core.FarmGear);

        foreach (InventoryItem item in Bot.Inventory.Items)
        {
            if (item.Equipped || blackListedItems.Contains(item.Name))
                continue;

            if (Bot.Bank.FreeSlots == 0 && !item.Coins)
            {
                if (!logged)
                {
                    Core.Logger($"Bank is full");
                    logged = true;
                }
                continue;
            }

            Core.ToBank(item.ID);
        }
    }
}
