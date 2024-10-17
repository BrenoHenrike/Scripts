/*
name: RankUpAllClasses
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;
using Skua.Core.Models.Items;

public class RankUpAll
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public string OptionsStorage = "RankUpAll";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        new Option<bool>("inclBank", "Include Bank", "If True, will also rank up all unranked classes in the bank", false),
        CoreBots.Instance.SkipOptions
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        RankUpAllClasses(Bot.Config!.Get<bool>("inclBank"));

        Core.SetOptions(false);
    }

    public void RankUpAllClasses(bool includeBank = false)
    {
        // Define the classes to exclude
        List<string> excludedClasses = new()
    {
        "Hobo Highlord",
        "No Class",
        "Obsidian No Class"
    };

        // Populate SelectedClasses from inventory, excluding specific classes
        List<string> SelectedClasses = Bot.Inventory.Items
            .Where(c => c.Category == ItemCategory.Class
                        && c.Quantity < 302500
                        && !excludedClasses.Contains(c.Name)) // Exclude specific classes
            .Select(x => x.Name)
            .ToList();

        // If includeBank is true, add classes from bank to SelectedClasses
        List<string> bankClasses = new();
        if (includeBank)
        {
            bankClasses = Bot.Bank.Items
                .Where(c => c.Category == ItemCategory.Class
                            && c.Quantity < 302500
                            && !excludedClasses.Contains(c.Name)) // Exclude specific classes
                .Select(x => x.Name)
                .ToList();

            SelectedClasses.AddRange(bankClasses);
        }

        // Optional: Log the updated SelectedClasses
        Core.Logger("Classes to Rank: " + string.Join(", ", SelectedClasses));

        // Check if Smart Enhance is enabled
        if (Core.CBOBool("DisableAutoEnhance", out bool _disableAutoEnhance) && _disableAutoEnhance)
        {
            Core.Logger("This bot requires Smart Enhance to work properly, please modify your CBO settings", messageBox: true, stopBot: true);
            return; // Stop the execution if Smart Enhance is disabled
        }

        // Rank up classes and bank them if they were sourced from the bank
        foreach (string Class in SelectedClasses)
        {
            if (Core.CheckInventory(Class))
            {
                Adv.RankUpClass(Class, false);

                // Bank the class if it was originally from the bank
                if (bankClasses.Contains(Class))
                {
                    Core.ToBank(Class); // Bank the class that came from the bank
                }
            }
        }
    }
}
