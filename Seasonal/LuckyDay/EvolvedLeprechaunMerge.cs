/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class EvolvedLeprechaun
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetItems();

        Core.SetOptions(false);
    }

    public void GetItems()
    {
        if (!Core.isSeasonalMapActive("luck"))
            return;
        if (Core.CheckInventory("Evolved Leprechaun"))
            return;

        Core.AddDrop("Lucky Clover", "Rainbow Shard", "Golden Ticket");

        //Rainbow Shard
        while (!Bot.ShouldExit && !Core.CheckInventory("Rainbow Shard", 20))
        {
            Core.EnsureAccept(1758);
            Core.HuntMonster("rainbow", "Rainbow Rat", "Prismatic Rainbow Fur", 5);
            Core.EnsureComplete(1758);
        }

        //Golden Ticket
        while (!Bot.ShouldExit && !Core.CheckInventory("Golden Ticket", 2000))
            Core.GetMapItem(101, 1, "luck");

        //Lucky Clover
        if (!Core.CheckInventory("Lucky Clover", 12))
            if (!Bot.Quests.IsDailyComplete(1759))
            {
                Core.EnsureAccept(1759);
                Core.HuntMonster("rainbow", "Lucky Harms", "Clover Leaves", 20);
                Core.EnsureComplete(1759);
            }

        if (!Core.CheckInventory("Lucky Clover", 12))
            Core.Logger($"Not enough \"Lucky Clovers\", please do the daily until you have 12. You can run it once a day (Server resets at 12am EST)");

        //Buy Evolved Leprechaun
        if (Core.CheckInventory("Lucky Clover", 12) && (Core.CheckInventory("Rainbow Shard", 20) && (Core.CheckInventory("Golden Ticket", 2000))))
            Core.BuyItem("luck", 256, "Evolved Leprechaun");

    }
}
