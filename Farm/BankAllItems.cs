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
            Core.ToBank(item.Name);
        }
    }
}
