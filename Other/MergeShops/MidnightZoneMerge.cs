/*
name: Midnight Zone Merge
description: This bot will farm the items belonging to the selected mode for the Midnight Zone Merge [2302] in /midnightzone
tags: midnight, zone, merge, midnightzone, elven, heritage, guardian, morph, dark, forest, shawl, silver, edged, lamenter, lamenters, ranger, rustic, quiver, immortal, yew, bow
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story\ShadowsOfWar\CoreSoW.cs
//cs_include Scripts/Story\AgeOfRuin\CoreAOR.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class MidnightZoneMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private CoreAOR AOR = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Undine Base Scrip", "Water Elf Pearl" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        AOR.MidnightZone();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("midnightzone", 2302, findIngredients, buyOnlyThis, buyMode: buyMode);

        #region Dont edit this part
        void findIngredients()
        {
            ItemBase req = Adv.externalItem;
            int quant = Adv.externalQuant;
            int currentQuant = req.Temp ? Bot.TempInv.GetQuantity(req.Name) : Bot.Inventory.GetQuantity(req.Name);
            if (req == null)
            {
                Core.Logger("req is NULL");
                return;
            }

            switch (req.Name)
            {
                default:
                    bool shouldStop = Adv.matsOnly ? !dontStopMissingIng : true;
                    Core.Logger($"The bot hasn't been taught how to get {req.Name}." + (shouldStop ? " Please report the issue." : " Skipping"), messageBox: shouldStop, stopBot: shouldStop);
                    break;
                #endregion

                case "Undine Base Scrip":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("sunlightzone", "Infernal Illusion", req.Name, quant, false, false);
                    break;

                case "Water Elf Pearl":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(9302);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("midnightzone", "Sparagmos", "Memory Card", log: false);
                        Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("midnightzone", "Shadow Viscera", "Fleshy Shadows", 8);
                        Core.HuntMonster("midnightzone", "Venerated Wraith");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("77487", "Elven Heritage Guardian", "Mode: [select] only\nShould the bot buy \"Elven Heritage Guardian\" ?", false),
        new Option<bool>("77488", "Elven Heritage Helm", "Mode: [select] only\nShould the bot buy \"Elven Heritage Helm\" ?", false),
        new Option<bool>("77489", "Elven Guardian Morph", "Mode: [select] only\nShould the bot buy \"Elven Guardian Morph\" ?", false),
        new Option<bool>("77490", "Dark Forest Shawl", "Mode: [select] only\nShould the bot buy \"Dark Forest Shawl\" ?", false),
        new Option<bool>("77491", "Silver Edged Lamenter", "Mode: [select] only\nShould the bot buy \"Silver Edged Lamenter\" ?", false),
        new Option<bool>("77492", "Silver Edged Lamenters", "Mode: [select] only\nShould the bot buy \"Silver Edged Lamenters\" ?", false),
        new Option<bool>("77493", "Elven Heritage Ranger", "Mode: [select] only\nShould the bot buy \"Elven Heritage Ranger\" ?", false),
        new Option<bool>("77494", "Elven Heritage Hood", "Mode: [select] only\nShould the bot buy \"Elven Heritage Hood\" ?", false),
        new Option<bool>("77495", "Elven Ranger Morph", "Mode: [select] only\nShould the bot buy \"Elven Ranger Morph\" ?", false),
        new Option<bool>("77496", "Rustic Elven Quiver", "Mode: [select] only\nShould the bot buy \"Rustic Elven Quiver\" ?", false),
        new Option<bool>("77497", "Immortal Yew Bow", "Mode: [select] only\nShould the bot buy \"Immortal Yew Bow\" ?", false),
    };
}
