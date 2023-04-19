/*
name: BankAllItems
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
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
        new Option<string>("BlackList", "BlackList Items", "Fill in the monsters that the bot should prioritize (in order), split with a , (comma)."),
    };


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
        if (!String.IsNullOrEmpty(Bot.Config.Get<string>("BlackList")))
            blackListedItems.AddRange(Bot.Config.Get<string>("BlackList").Split(',', StringSplitOptions.TrimEntries));


        Bot.Wait.ForMapLoad("battleon");
        Bot.Sleep(Core.ActionDelay);
        Bot.Send.Packet($"%xt%zm%house%1%{Bot.Player.Username}%");


        foreach (InventoryItem item in Bot.Inventory.Items)
        {
            if (item.Equipped || blackListedItems.Contains(item.Name))
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
            Core.ToBank(item.ID);
            Bot.Sleep(Core.ActionDelay);
        }
    }
}

