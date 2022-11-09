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
        foreach (InventoryItem item in Bot.Inventory.Items.Where(i => !i.Equipped && i.Name != "Treasure Potion"))
        {
            Bot.Sleep(Core.ActionDelay);
            if (item.Coins)
                Core.ToBank(item.Name);
            if (!item.Coins || Bot.Bank.FreeSlots < 1)
                Core.Logger($"Failed to bank {item.Name}");
            else
                Core.ToBank(item.Name);
        }

        // foreach (InventoryItem item in Bot.House.Items.Where(i => !i.Equipped))
        // {
        //     Bot.Sleep(Core.ActionDelay);
        //     if (item.Coins)
        //         Core.ToBank(item.Name);
        //     if (!item.Coins || Bot.Bank.FreeSlots < 1)
        //         Core.Logger($"Failed to bank {item.Name}");
        //     else
        //         Core.ToBank(item.Name);
        // }

    }
}
