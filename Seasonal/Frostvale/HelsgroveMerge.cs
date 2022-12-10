//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class HelsgroveMerge
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
        Core.BankingBlackList.AddRange(new[] { "Golden Branch", "Frostval Treat", "Hazel Switch", "Helsgrove Guardian Scarf", "Chibi GroveRider's Locks", "Chibi GroveRider's Locks + Bridle "});
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("helsgrove", 2076, findIngredients);

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

                case "Golden Branch":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8420);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("helsgrove", "Krimpler", "Gilded Branch", 3);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Hazel Switch":
                case "Frostval Treat":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8421);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("helsgrove", "Belsnickling", "Belsnickling Beaten", 6);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Chibi GroveRider's Locks + Bridle":
                case "Chibi GroveRider's Locks":
                case "Helsgrove Guardian Scarf":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("helsgrove", "Helsdottir", req.Name, quant, false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("66052", "Helsgrove Guardian", "Mode: [select] only\nShould the bot buy \"Helsgrove Guardian\" ?", false),
        new Option<bool>("66055", "Helsgrove Guardian Colorful Scarf", "Mode: [select] only\nShould the bot buy \"Helsgrove Guardian Colorful Scarf\" ?", false),
        new Option<bool>("66056", "Helsgrove Guardian Antlers + Scarf", "Mode: [select] only\nShould the bot buy \"Helsgrove Guardian Antlers + Scarf\" ?", false),
        new Option<bool>("66057", "Helsgrove Guardian Colorful Antlers + Scarf", "Mode: [select] only\nShould the bot buy \"Helsgrove Guardian Colorful Antlers + Scarf\" ?", false),
        new Option<bool>("66062", "Chibi GroveRider's Accessories", "Mode: [select] only\nShould the bot buy \"Chibi GroveRider's Accessories\" ?", false),
        new Option<bool>("66063", "Chibi GroveRider's Accessories + Bridle", "Mode: [select] only\nShould the bot buy \"Chibi GroveRider's Accessories + Bridle\" ?", false),
        new Option<bool>("66065", "Helsgrove Guardian Baton", "Mode: [select] only\nShould the bot buy \"Helsgrove Guardian Baton\" ?", false),
        new Option<bool>("66066", "Helsgrove Guardian Dual Batons", "Mode: [select] only\nShould the bot buy \"Helsgrove Guardian Dual Batons\" ?", false),
    };
}
