//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Seasonal/HarvestDay/CoreHarvestDay.cs

using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class NightmareHarvestWarMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public CoreHarvestDay HarvestDay = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Nightmare Medal " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        if (!Core.isSeasonalMapActive("nightmarewar"))
            return;
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("nightmarewar", 1941, findIngredients);

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

                case "Nightmare Medal":
                    HarvestDay.NightmareWar();
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(7809, 7810);
                    // Murderbug Medal 7809
                    // Mega Murderbug Medal 7810
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("nightmarewar", "Zombie Cicada", "Murderbug Medal", 5);
                        Core.HuntMonster("nightmarewar", "Zombie Cicada", "Mega Murderbug Medal", 3);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("57178", "HollowNight Pumpkin Lord", "Mode: [select] only\nShould the bot buy \"HollowNight Pumpkin Lord\" ?", false),
        new Option<bool>("57179", "HollowNight Pumpkin Knight", "Mode: [select] only\nShould the bot buy \"HollowNight Pumpkin Knight\" ?", false),
        new Option<bool>("57180", "HollowNight Pumpkin Face", "Mode: [select] only\nShould the bot buy \"HollowNight Pumpkin Face\" ?", false),
        new Option<bool>("57181", "HollowNight Pumpkin Head", "Mode: [select] only\nShould the bot buy \"HollowNight Pumpkin Head\" ?", false),
        new Option<bool>("57182", "HollowNight Pumpkin Hood", "Mode: [select] only\nShould the bot buy \"HollowNight Pumpkin Hood\" ?", false),
        new Option<bool>("57183", "HollowNight Pumpkin Vines", "Mode: [select] only\nShould the bot buy \"HollowNight Pumpkin Vines\" ?", false),
        new Option<bool>("57184", "HollowNight Pumpkin Wings", "Mode: [select] only\nShould the bot buy \"HollowNight Pumpkin Wings\" ?", false),
        new Option<bool>("57185", "HollowNight Pumpkin Greatsword", "Mode: [select] only\nShould the bot buy \"HollowNight Pumpkin Greatsword\" ?", false),
        new Option<bool>("57186", "HollowNight Pumpkin Blade", "Mode: [select] only\nShould the bot buy \"HollowNight Pumpkin Blade\" ?", false),
        new Option<bool>("57188", "HollowNight Pumpkin Pet", "Mode: [select] only\nShould the bot buy \"HollowNight Pumpkin Pet\" ?", false),
        new Option<bool>("57189", "Otherworldly HollowNight Pumpkin Pet", "Mode: [select] only\nShould the bot buy \"Otherworldly HollowNight Pumpkin Pet\" ?", false),
        new Option<bool>("57705", "Enchanted Forest Rogue", "Mode: [select] only\nShould the bot buy \"Enchanted Forest Rogue\" ?", false),
        new Option<bool>("57706", "Enchanted Forest Rogue’s Hat", "Mode: [select] only\nShould the bot buy \"Enchanted Forest Rogue’s Hat\" ?", false),
        new Option<bool>("57707", "Enchanted Rogue’s Hat + Ponytail", "Mode: [select] only\nShould the bot buy \"Enchanted Rogue’s Hat + Ponytail\" ?", false),
        new Option<bool>("57708", "Enchanted Rogue's Grimace", "Mode: [select] only\nShould the bot buy \"Enchanted Rogue's Grimace\" ?", false),
        new Option<bool>("57709", "Enchanted Rogue's Grimace + Ponytail", "Mode: [select] only\nShould the bot buy \"Enchanted Rogue's Grimace + Ponytail\" ?", false),
        new Option<bool>("57710", "Enchanted Rogue’s Back Quiver", "Mode: [select] only\nShould the bot buy \"Enchanted Rogue’s Back Quiver\" ?", false),
        new Option<bool>("57711", "Enchanted Rogue’s Hip Quiver", "Mode: [select] only\nShould the bot buy \"Enchanted Rogue’s Hip Quiver\" ?", false),
        new Option<bool>("57712", "Enchanted Forest Rogue Bow", "Mode: [select] only\nShould the bot buy \"Enchanted Forest Rogue Bow\" ?", false),
        new Option<bool>("57856", "HollowNight Pumpkin Hood Morph", "Mode: [select] only\nShould the bot buy \"HollowNight Pumpkin Hood Morph\" ?", false),
    };
}
