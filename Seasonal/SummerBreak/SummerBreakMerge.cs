/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Legion/CoreLegion.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class SummerBreakMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public CoreNation Nation = new();
    public CoreLegion Legion = new CoreLegion();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nation.bagDrops);
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        if (!Core.isSeasonalMapActive("summerbreak"))
            return;

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("summerbreak", 2155, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Diamond of Nulgath":
                    Nation.FarmDiamondofNulgath(quant);
                    break;

                case "Legion Token":
                    if (Core.isCompletedBefore(793))
                        Legion.FarmLegionToken(quant);
                    else
                        Core.Logger("Skipped the item because user is not part of the Legion", messageBox: true);
                    break;

                case "Summer Sizzle Lotion":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8794);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("summerbreak", "MMMirage", "Gum Ball", 6);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Volleyball Captain":
                case "Volleyball Hero":
                case "Volleyball Hero's Hat + Glasses":
                case "Volleyball Heroine's Hat + Glasses":
                case "Volleyball Team A Mascot":
                case "Volleyball Team B Mascot":
                case "Volleyball Team C Mascot":
                case "Volleyball Hero's Board Cape":
                case "Volleyball Hero's Rod":
                case "Volleyball Hero's Surfboard":
                case "Volleyball Hero's Foam Spear":
                case "Volleyball Hero's Foam Gauntlets":
                case "Volleyball Hero's WaterGun":
                case "Volleyball Hero's WaterGuns":
                    Core.EquipClass(ClassType.Farm);
                    Core.EnsureAccept(8794);
                    Core.HuntMonster("summerbreak", "MMMirage", "Gum Ball", 6);
                    Core.EnsureCompleteChoose(8794, new[] { req.Name });
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("44211", "Surfboard of Nulgath Nation", "Mode: [select] only\nShould the bot buy \"Surfboard of Nulgath Nation\" ?", false),
        new Option<bool>("44210", "Surfboard of the Undead Legion", "Mode: [select] only\nShould the bot buy \"Surfboard of the Undead Legion\" ?", false),
        new Option<bool>("71099", "Enchanted Volleyball Captain", "Mode: [select] only\nShould the bot buy \"Enchanted Volleyball Captain\" ?", false),
        new Option<bool>("71100", "Enchanted Volleyball Hero", "Mode: [select] only\nShould the bot buy \"Enchanted Volleyball Hero\" ?", false),
        new Option<bool>("71103", "Enchanted Volleyballer's Hat + Glasses", "Mode: [select] only\nShould the bot buy \"Enchanted Volleyballer's Hat + Glasses\" ?", false),
        new Option<bool>("71104", "Enchanted Volleyballer's Female Hat + Glasses", "Mode: [select] only\nShould the bot buy \"Enchanted Volleyballer's Female Hat + Glasses\" ?", false),
        new Option<bool>("71105", "Enchanted Team A Mascot", "Mode: [select] only\nShould the bot buy \"Enchanted Team A Mascot\" ?", false),
        new Option<bool>("71106", "Enchanted Team B Mascot", "Mode: [select] only\nShould the bot buy \"Enchanted Team B Mascot\" ?", false),
        new Option<bool>("71107", "Enchanted Team C Mascot", "Mode: [select] only\nShould the bot buy \"Enchanted Team C Mascot\" ?", false),
        new Option<bool>("71108", "Enchanted Volleyballer's Board Cape", "Mode: [select] only\nShould the bot buy \"Enchanted Volleyballer's Board Cape\" ?", false),
        new Option<bool>("71109", "Enchanted Volleyballer's Rod", "Mode: [select] only\nShould the bot buy \"Enchanted Volleyballer's Rod\" ?", false),
        new Option<bool>("71110", "Enchanted Volleyballer's Surfboard", "Mode: [select] only\nShould the bot buy \"Enchanted Volleyballer's Surfboard\" ?", false),
        new Option<bool>("71111", "Enchanted Volleyballer's Foam Spear", "Mode: [select] only\nShould the bot buy \"Enchanted Volleyballer's Foam Spear\" ?", false),
        new Option<bool>("71112", "Enchanted Volleyballer's Foam Gauntlets", "Mode: [select] only\nShould the bot buy \"Enchanted Volleyballer's Foam Gauntlets\" ?", false),
        new Option<bool>("71113", "Enchanted Volleyballer's WaterGun", "Mode: [select] only\nShould the bot buy \"Enchanted Volleyballer's WaterGun\" ?", false),
        new Option<bool>("71114", "Enchanted Volleyballer's WaterGuns", "Mode: [select] only\nShould the bot buy \"Enchanted Volleyballer's WaterGuns\" ?", false),
    };
}
