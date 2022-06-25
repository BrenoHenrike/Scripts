//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;
using RBot.Options;
using RBot.Shops;
using RBot.Items;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

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
        new Option<string>("mapName", "Map", "Map of the Merge Shop, please capitalize it properly", ""),
        new Option<int>("shopID", "Shop ID", "ID of the Merge Shop", 0),
        new Option<bool>("genFile", "Generate File", "Generate a MergeTemplate based bot, output will be in \\Scripts\\WIP\\", true)
    };

    public void ScriptMain(ScriptInterface bot)
    {
        Helper();
    }

    public void Helper()
    {
        string? map = Bot.Config.Get<string>("mapName");
        int shopID = Bot.Config.Get<int>("shopID");
        bool genFile = Bot.Config.Get<bool>("genFile");

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
                    if (!genFile)
                    {
                        output += $"\t- {req.Name}\n";

                        Bot.Log($"case \"{req.Name}\":");
                        Bot.Log("    //dostuff");
                        Bot.Log("    break;");
                        Bot.Log("");
                    }
                    else
                    {
                        output += $"\n                case \"{req.Name}\":\n";
                        output += "                    Core.Logger(\"This item is not setup yet\");\n";
                        output += "                    break;";
                    }
                }
            }
        }

        if (!genFile)
        {
            MessageBox.Show("Please add the following cases to the merge bots:\n" + output);
            return;
        }

        Bot.Log("-");
        Bot.Log(output);
        Bot.Log("-");

        string AppPath = Core.AppPath ?? "";
        string[] MergeTemplate = File.ReadAllLines(AppPath + @"\Scripts\MergeTemplate.cs");

        int index = Array.IndexOf(MergeTemplate, "                // Add how to get items here") - 1;
        if (index < 0)
        {
            Core.Logger("Failed to find index");
            return;
        }
        int classIndex = Array.IndexOf(MergeTemplate, "public class MergeTemplate");
        if (classIndex < 0)
        {
            Core.Logger("Failed to find classIndex");
            return;
        }
        MergeTemplate[classIndex] = $"public class {map}Merge";
        int startIndex = Array.IndexOf(MergeTemplate, "        Adv.StartBuyAllMerge(\"map\", 1234, findIngredients);");
        if (startIndex < 0)
        {
            Core.Logger("Failed to find startIndex");
            return;
        }
        MergeTemplate[startIndex] = $"        Adv.StartBuyAllMerge(\"{map.ToLower()}\", {shopID}, findIngredients);";

        int endIndex = MergeTemplate.Count() - 4;
        string[] content = MergeTemplate[..index].Concat(new[] { output }).Concat(MergeTemplate[endIndex..]).ToArray();

        string path = AppPath + $@"\Scripts\WIP\{map}Merge.cs";
        Directory.CreateDirectory(AppPath + @"\Scripts\WIP");
        File.WriteAllLines(path, content);
        DialogResult result = MessageBox.Show($"File has been generated. Path is {path}\n\nPress OK to open the file",
                                                "File Generated", MessageBoxButtons.OKCancel);
        if (result == DialogResult.OK)
            Process.Start("explorer", path);
    }
}
