//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
public class CoreClass
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new CoreAdvanced();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Level();

        Core.SetOptions(false);
    }

    public void Level()
    {
        List<InventoryItem> itemInv = Bot.Inventory.Items.FindAll(i => i.Category == ItemCategory.Class && i.Quantity != 302500);
        foreach (InventoryItem item in itemInv) {
            Core.Logger($"Leveling {item.Name} class");
            Adv.rankUpClass(item.Name);
        }
    }
}