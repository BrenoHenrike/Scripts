//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class BankAllItems
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface bot)
    {
        BankAll();
    }

    public void BankAll()
     => Bot.Inventory.Items.Where(i => !i.Equipped && i.Name != "Treasure Potion").ForEach(b => Core.ToBank(b.Name));
}
