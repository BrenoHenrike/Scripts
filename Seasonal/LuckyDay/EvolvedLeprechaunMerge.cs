//cs_include Scripts/CoreBots.cs
using RBot;

public class EvolvedLeprechaun
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetItems();

        Core.SetOptions(false);
    }

    public void GetItems()
    {

        if (Core.CheckInventory("Evolved Leprechaun"))
            return;

        Core.AddDrop("Lucky Clover", "Rainbow Shard", "Golden Ticket");

        //Rainbow Shard
        while (!Core.CheckInventory("Rainbow Shard", 20))
        {
            Core.EnsureAccept(1758);
            Core.HuntMonster("rainbow", "Rainbow Rat", "Prismatic Rainbow Fur", 5);
            Core.EnsureComplete(1758);
        }

        //Golden Ticket
        while (!Core.CheckInventory("Golden Ticket", 2000))
        {
            Core.GetMapItem(101, 1, "luck");
        }

        //Lucky Clover
        if (!Core.CheckInventory("Lucky Clover", 12))
            if (!Bot.Quests.IsDailyComplete(1759))
            {
                Core.SmartKillMonster(1759, "rainbow", "Lucky Harms ", 20, true);
            }
        if (!Core.CheckInventory("Lucky Clover", 12))
        {
            Core.Logger($"Not enough \"Lucky Clovers\", please do the daily until you have 12. You can run it once a day (Server resets at 12am EST)");
        }

        //Buy Evolved Leprechaun
        if (Core.CheckInventory("Lucky Clover", 12) && (Core.CheckInventory("Rainbow Shard", 20) && (Core.CheckInventory("Golden Ticket", 2000))))
        {
            Core.BuyItem("luck", 256, "Evolved Leprechaun");
        }
    }
}