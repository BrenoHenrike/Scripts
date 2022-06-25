//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;
using RBot.Options;
using RBot.Shops;
using RBot.Items;
using System.Windows.Forms;

public class MergeTemplateHelper
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();

    public string OptionsStorage = "MergeTemplateHelper";
    public List<IOption> Options = new List<IOption>()
    {
        new Option<string>("", "Dev-Only", "This bot is to help us make merge bots, the average user wont find any use in this bot", ""),
        new Option<string>("", " ", "", ""),
        new Option<string>("mapName", "Map", "Map of the Merge Shop", ""),
        new Option<int>("shopID", "Shop ID", "ID of the Merge Shop", 0)
    };

    public void ScriptMain(ScriptInterface bot)
    {
        Helper();
    }

    public void Helper()
    {
        string? map = Bot.Config.Get<string>("mapName");
        int shopID = Bot.Config.Get<int>("shopID");

        if (shopID == 0 || String.IsNullOrEmpty(map) || String.IsNullOrWhiteSpace(map))
        {
            Core.Logger("Please fill in the starting form");
            return;
        }

        List<ShopItem> shopItems = Core.GetShopItems(map, shopID);
        string output = "";
        List<string> itemsToLearn = new();

        foreach (ShopItem item in shopItems)
        {
            if (Adv.miscCatagories.Contains(item.Category) || item.Requirements == null)
                continue;

            foreach (ItemBase req in item.Requirements)
            {
                if (!shopItems.Any(_item => _item.ID == req.ID) && !output.Contains(req.Name))
                {
                    output += $"\t- {req.Name}\n";

                    Bot.Log($"case \"{req.Name}\":");
                    Bot.Log("    //dostuff");
                    Bot.Log("    break;");
                    Bot.Log("");
                }
            }
        }

        MessageBox.Show(
            "Please add the following cases to the merge bots:\n" + output
        );
    }
}
