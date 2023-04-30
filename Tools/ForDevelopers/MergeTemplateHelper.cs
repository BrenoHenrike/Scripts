/*
name: Merge Shop Bot Generator/Helper
description: Fill in the map and shop ID and this tool will generate most of the merge bot for you, then you fill in the rest
tags: merge, shop, generator, helper, developer
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;
using Skua.Core.Models;
using Skua.Core.Models.Shops;
using Skua.Core.Models.Items;
using Skua.Core.Utils;
using System.IO;
using System.Diagnostics;

public class MergeTemplateHelper
{
    public IScriptInterface Bot => IScriptInterface.Instance;
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

    public void ScriptMain(IScriptInterface bot)
    {
        Helper();
    }

    public void Helper()
    {
        string? map = Bot.Config!.Get<string>("mapName")?.ToLower();
        int shopID = Bot.Config.Get<int>("shopID");
        bool genFile = Bot.Config.Get<bool>("genFile");

        if (shopID == 0 || String.IsNullOrEmpty(map) || String.IsNullOrWhiteSpace(map))
        {
            Core.Logger("Please fill in the starting form");
            return;
        }

        List<ShopItem> shopItems = Core.GetShopItems(map, shopID);
        string output = String.Empty;
        List<string> itemsToLearn = new();
        string scriptName = Bot.Shops.Name.Replace("Merge", "").Replace("merge", "").Replace("shop", "").Replace("Shop", "").Replace("'", "");
        string className = scriptName.Replace(" ", "");

        string scriptInfo =
            "/*\n" +
            $"name: {scriptName}\n" +
            $"description: This bot will farm the items belonging to the selected mode for the {scriptName} [{shopID}] in /{map}\n" +
            $"tags: ";
        List<string> tags = scriptName.ToLower().Split(' ').ToList();
        tags.Add(map);

        List<string> shopItemNames = new();
        if (genFile)
        {
            shopItemNames.Add("");
            shopItemNames.Add("    public List<IOption> Select = new()");
            shopItemNames.Add("    {");
        }

        foreach (ShopItem item in shopItems)
        {
            if (Adv.miscCatagories.Contains(item.Category) || item.Requirements == null)
                continue;

            shopItemNames.Add($"        new Option<bool>(\"{item.ID}\", \"{item.Name}\", \"Mode: [select] only\\nShould the bot buy \\\"{item.Name}\\\" ?\", false),");

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
                        output += "                    Core.FarmingLogger(req.Name, quant);\n";
                        output += "                    Core.EquipClass(ClassType.Farm);\n";
                        output += "                    Core.RegisterQuests(0000);\n";
                        output += "                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))\n";
                        output += "                    {\n";
                        output += "                        Core.Logger(\"This item is not setup yet\");\n";
                        output += "                        Bot.Wait.ForPickup(req.Name);\n";
                        output += "                    }\n";
                        output += "                    Core.CancelRegisteredQuests();\n";
                        output += "                    break;\n";
                        itemsToLearn.Add(req.Name);
                        tags.AddRange(req.Name.ToLower().Split(' ').Except(tags));
                    }
                }
            }
        }

        if (!genFile)
        {
            Bot.ShowMessageBox("Please add the following cases to the merge bots:\n" + output, "Merge Template Helper");
            return;
        }

        string[] MergeTemplate = File.ReadAllLines(Path.Combine(ClientFileSources.SkuaScriptsDIR, "Templates", "MergeTemplate.cs"));

        int itemsIndex = Array.IndexOf(MergeTemplate, "                // Add how to get items here") - 1;
        if (itemsIndex < 0)
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
        MergeTemplate[classIndex] = $"public class {className}Merge";

        int blackListIndex = Array.IndexOf(MergeTemplate, "        Core.BankingBlackList.AddRange(new[] { \"\" });");
        if (blackListIndex < 0)
        {
            Core.Logger("Failed to find blackListIndex");
            return;
        }
        MergeTemplate[blackListIndex] = "        Core.BankingBlackList.AddRange(new[] { \"" + String.Join("\", \"", itemsToLearn) + "\"});";

        int startIndex = Array.IndexOf(MergeTemplate, "        Adv.StartBuyAllMerge(\"map\", 1234, findIngredients, buyOnlyThis, buyMode: buyMode);");
        if (startIndex < 0)
        {
            Core.Logger("Failed to find startIndex");
            return;
        }
        MergeTemplate[startIndex] = $"        Adv.StartBuyAllMerge(\"{map.ToLower()}\", {shopID}, findIngredients, buyOnlyThis, buyMode: buyMode);";

        shopItemNames.Add("    };");

        scriptInfo += tags.Join(", ") + "\n*/";

        string[] content = new[] { scriptInfo }
                            .Concat(MergeTemplate[5..itemsIndex])
                            .Concat(new[] { output })
                            .Concat(MergeTemplate[(MergeTemplate.Count() - 4)..(MergeTemplate.Count() - 1)])
                            .Concat(shopItemNames.ToArray())
                            .Concat(new[] { "}" })
                            .ToArray();

        string path = Path.Combine(ClientFileSources.SkuaScriptsDIR, "WIP", className + "Merge.cs");
        Directory.CreateDirectory(Path.Combine(ClientFileSources.SkuaScriptsDIR, "WIP"));
        Core.WriteFile(path, content);
        if (Bot.ShowMessageBox($"File has been generated. Path is {path}\n\nPress OK to open the file",
                                                "File Generated", "OK").Text == "OK")
            Process.Start("explorer", path);
    }
}
