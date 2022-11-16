//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Legion/CoreLegion.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class DarkPalaceMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public CoreLegion Legion = new CoreLegion();
    public static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Dark Palace Token", "Shadow of Cerberus", "Legion Token", "Axe Of Cerberus", "Flail Of Cerberus " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("dagefortress", 1144, findIngredients);

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

                case "Legion Token":
                    Legion.FarmLegionToken(quant);
                    break;

                case "Axe Of Cerberus":
                case "Shadow of Cerberus":
                case "Flail Of Cerberus":
                case "Dark Palace Token":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        Core.HuntMonster("dagefortress", "Grrrberus", req.Name, isTemp: false, log: false);
                    Bot.Wait.ForPickup(req.Name);
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("29583", "Screaming Might", "Mode: [select] only\nShould the bot buy \"Screaming Might\" ?", false),
        new Option<bool>("29780", "Victor's Trophy Spear", "Mode: [select] only\nShould the bot buy \"Victor's Trophy Spear\" ?", false),
        new Option<bool>("29889", "Commander of Cerberus", "Mode: [select] only\nShould the bot buy \"Commander of Cerberus\" ?", false),
        new Option<bool>("29890", "General of Cerberus", "Mode: [select] only\nShould the bot buy \"General of Cerberus\" ?", false),
        new Option<bool>("29898", "Axe and Shield of Cerberus", "Mode: [select] only\nShould the bot buy \"Axe and Shield of Cerberus\" ?", false),
        new Option<bool>("29897", "Flail and Shield of Cerberus", "Mode: [select] only\nShould the bot buy \"Flail and Shield of Cerberus\" ?", false),
        new Option<bool>("29903", "Hood Of Cerberus", "Mode: [select] only\nShould the bot buy \"Hood Of Cerberus\" ?", false),
        new Option<bool>("29902", "Visage of Cerberus", "Mode: [select] only\nShould the bot buy \"Visage of Cerberus\" ?", false),
        new Option<bool>("29901", "Demon Helm of Cerberus", "Mode: [select] only\nShould the bot buy \"Demon Helm of Cerberus\" ?", false),
        new Option<bool>("29900", "Skull Helm of Cerberus", "Mode: [select] only\nShould the bot buy \"Skull Helm of Cerberus\" ?", false),
        new Option<bool>("29891", "Cloak of the Underworld", "Mode: [select] only\nShould the bot buy \"Cloak of the Underworld\" ?", false),
        new Option<bool>("29893", "Underworld Watchdog", "Mode: [select] only\nShould the bot buy \"Underworld Watchdog\" ?", false),
        new Option<bool>("29892", "Pre-furred Cape of Cerberus", "Mode: [select] only\nShould the bot buy \"Pre-furred Cape of Cerberus\" ?", false),
        new Option<bool>("29894", "Cerberus' Bane", "Mode: [select] only\nShould the bot buy \"Cerberus' Bane\" ?", false),
        new Option<bool>("29899", "Skull Of Cerberus", "Mode: [select] only\nShould the bot buy \"Skull Of Cerberus\" ?", false),
    };
}
