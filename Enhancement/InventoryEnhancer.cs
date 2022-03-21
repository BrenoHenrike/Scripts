//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;
using RBot.Items;
using System.Collections;

public class project
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new CoreAdvanced();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Enhance();

        Core.SetOptions(false);
    }
    /// <summary>
    /// Enhances all non-leveled/non-user-level level enhanced items
    /// </summary>
    /// <param name="bank">Wether the bot should bank the enhanced items True/False, false by default</param>
    public void Enhance(bool bank =false)
    {
        List<string> InventoryItems = Bot.Inventory.Items.FindAll(x => x.EnhancementLevel < Bot.Player.Level && (x.ItemGroup != "None" && x.ItemGroup != "am" && x.ItemGroup != "co" && x.ItemGroup != "pe")).Select(x => x.Name).ToList();
        // List<string> BankItems = Bot.Bank.BankItems.FindAll(x => x.EnhancementLevel < Bot.Player.Level && (x.ItemGroup != "None" && x.ItemGroup != "am" && x.ItemGroup != "co" && x.ItemGroup != "pe")).Select(x => x.Name).ToList();

        // Core.CheckInventory(BankItems.ToArray());

        if (bank)
        {
            foreach (string item in InventoryItems)
            {
                Adv.EnhanceItem(InventoryItems.ToArray(), EnhancementType.Lucky);
                // Adv.EnhanceItem(BankItems.ToArray(), EnhancementType.Lucky);
                string[] toBank = Bot.Inventory.Items.FindAll(x => InventoryItems.Contains(x.Name) && x.Coins).Select(x => x.Name).ToArray();
                // string[] BankItems = Bot.Bank.BankItems.FindAll(x => BankItems.Contains(x.Name) && x.Coins).Select(x => x.Name).ToArray();
                Core.Logger(string.Join(',', InventoryItems.ToArray()));
                // Core.Logger(string.Join(',', Items.Concat(BankItems)ToArray()));
                Core.ToBank(InventoryItems.ToArray());
                // Core.ToBank(Items.Concat(BankItems).ToArray());
            }
        }
        if (!bank)
            Adv.EnhanceItem(InventoryItems.ToArray(), EnhancementType.Lucky);
        // Adv.EnhanceItem(Items.Concat(BankItems).ToArray());, EnhancementType.Lucky);
    }
}