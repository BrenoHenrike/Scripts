/*
name: Trench Observe Merge
description: This bot will farm the items belonging to the selected mode for the Trench Observe Merge [2316] in /trenchobserve
tags: trench, observe, merge, trenchobserve, deepwater, drow, top, knot, horns, corona, bioluminescent, trident, sea, streams
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
//cs_include Scripts/Story/AgeOfRuin/CoreAOR.cs
//cs_include Scripts/Other/MergeShops/AbyssalZoneMerge.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class TrenchObserveMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private CoreAOR AOR = new();
    private AbyssalZoneMerge AZM = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Undine Base Scrip", "Dark Elf Pearl", "Ashray Elf Warden", "Ashray Elf Top Knot", "Ashray Elf Locks", "Ashray Top Knot and Horns", "Ashray Locks and Horns", "Coastal Corona", "Golden Ashray Trident", "Dancer's Sea Streams" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        AOR.DeepWater();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("trenchobserve", 2316, findIngredients, buyOnlyThis, buyMode: buyMode);

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
                    bool shouldStop = !Adv.matsOnly || !dontStopMissingIng;
                    Core.Logger($"The bot hasn't been taught how to get {req.Name}." + (shouldStop ? " Please report the issue." : " Skipping"), messageBox: shouldStop, stopBot: shouldStop);
                    break;
                #endregion

                case "Undine Base Scrip":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("sunlightzone", "Infernal Illusion", req.Name, quant, false, false);
                    break;

                case "Dark Elf Pearl":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(9339);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("trenchobserve", "Lady Noelle", "Noelle's Brooch", log: false);
                        Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("trenchobserve", "Sea Spirit", "Green Sea Jelly", 2, log: false);
                        Core.HuntMonster("trenchobserve", "Necro Adipocere", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Ashray Elf Top Knot":
                case "Ashray Elf Warden":
                case "Ashray Elf Locks":
                case "Ashray Top Knot and Horns":
                case "Ashray Locks and Horns":
                case "Coastal Corona":
                case "Golden Ashray Trident":
                case "Dancer's Sea Streams":
                    AZM.BuyAllMerge(req.Name);
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("79050", "DeepWater Drow", "Mode: [select] only\nShould the bot buy \"DeepWater Drow\" ?", false),
        new Option<bool>("79051", "DeepWater Top Knot", "Mode: [select] only\nShould the bot buy \"DeepWater Top Knot\" ?", false),
        new Option<bool>("79052", "DeepWater Locks", "Mode: [select] only\nShould the bot buy \"DeepWater Locks\" ?", false),
        new Option<bool>("79053", "DeepWater Top Knot and Horns", "Mode: [select] only\nShould the bot buy \"DeepWater Top Knot and Horns\" ?", false),
        new Option<bool>("79054", "DeepWater Locks and Horns", "Mode: [select] only\nShould the bot buy \"DeepWater Locks and Horns\" ?", false),
        new Option<bool>("79055", "DeepWater Corona", "Mode: [select] only\nShould the bot buy \"DeepWater Corona\" ?", false),
        new Option<bool>("79056", "Bioluminescent Trident", "Mode: [select] only\nShould the bot buy \"Bioluminescent Trident\" ?", false),
        new Option<bool>("79057", "DeepWater Sea Streams", "Mode: [select] only\nShould the bot buy \"DeepWater Sea Streams\" ?", false),
    };
}
