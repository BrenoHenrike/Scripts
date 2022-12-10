//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class DarkWarLegionMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Legion Defender Medal", "Legion War Banner", "Legion Trophy " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        //Only edit the map and shopID here
        if (!Core.isSeasonalMapActive("darkwarlegion"))
            return;

        Adv.StartBuyAllMerge("darkwarlegion", 2122, findIngredients);

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

                case "Legion Defender Medal":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8584, 8585);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("darkwarlegion", "*", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Legion War Banner":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8587);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("darkwarlegion", "Manslayer Fiend", "ManSlayer Slain", 5);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Legion Trophy":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8586);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("darkwarlegion", "Dreadfiend", "Nation's Dread", 5, isTemp: false, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Soiled Fiend Crystal":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(8588);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("darkwarlegion", "Dirtlicker", "Dirtlicker Defeated");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("68984", "Soulfire Blademaster", "Mode: [select] only\nShould the bot buy \"Soulfire Blademaster\" ?", false),
        new Option<bool>("68991", "Soulfire Mask", "Mode: [select] only\nShould the bot buy \"Soulfire Mask\" ?", false),
        new Option<bool>("68992", "Soulfire Mask + Ponytail", "Mode: [select] only\nShould the bot buy \"Soulfire Mask + Ponytail\" ?", false),
        new Option<bool>("68993", "Soulfire Armet", "Mode: [select] only\nShould the bot buy \"Soulfire Armet\" ?", false),
        new Option<bool>("68995", "Soulfire Armet + Scarf", "Mode: [select] only\nShould the bot buy \"Soulfire Armet + Scarf\" ?", false),
        new Option<bool>("69003", "Soulfire Odachi", "Mode: [select] only\nShould the bot buy \"Soulfire Odachi\" ?", false),
        new Option<bool>("69008", "Soulfire Tonfa", "Mode: [select] only\nShould the bot buy \"Soulfire Tonfa\" ?", false),
        new Option<bool>("69004", "Soulfire Odachis", "Mode: [select] only\nShould the bot buy \"Soulfire Odachis\" ?", false),
        new Option<bool>("69009", "Soulfire Tonfas", "Mode: [select] only\nShould the bot buy \"Soulfire Tonfas\" ?", false),
        new Option<bool>("68985", "Soulfire Warmonger", "Mode: [select] only\nShould the bot buy \"Soulfire Warmonger\" ?", false),
        new Option<bool>("68986", "Flaming Soulfire Warmonger", "Mode: [select] only\nShould the bot buy \"Flaming Soulfire Warmonger\" ?", false),
        new Option<bool>("68994", "Horned Soulfire Armet", "Mode: [select] only\nShould the bot buy \"Horned Soulfire Armet\" ?", false),
        new Option<bool>("68996", "Horned Soulfire Armet + Scarf", "Mode: [select] only\nShould the bot buy \"Horned Soulfire Armet + Scarf\" ?", false),
        new Option<bool>("69000", "Soulfire Cloak", "Mode: [select] only\nShould the bot buy \"Soulfire Cloak\" ?", false),
        new Option<bool>("69001", "Flaming Soulfire Cloak", "Mode: [select] only\nShould the bot buy \"Flaming Soulfire Cloak\" ?", false),
        new Option<bool>("68763", "Dirk of the Underworld", "Mode: [select] only\nShould the bot buy \"Dirk of the Underworld\" ?", false),
        new Option<bool>("68764", "Daggers of the Underworld", "Mode: [select] only\nShould the bot buy \"Daggers of the Underworld\" ?", false),
    };
}
