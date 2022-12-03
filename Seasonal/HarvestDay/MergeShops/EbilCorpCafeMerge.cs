//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/HarvestDay/CoreHarvestDay.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class EbilCorpCafeMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private CoreHarvestDay HD = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Tin Can of ???", "PACVEC Mach 1.0", "PACVEC Helm", "PACVEC Visor", "PACVEC Guard", "PACVEC Battle Wings", "PACVEC Battle Hammer", "PACVEC Railgun", "PACVEC Alien" });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        if (!Core.isSeasonalMapActive("ebiltakeover"))
            return;
        HD.EpilTakeOver();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("ebiltakeover", 2190, findIngredients);

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

                case "Tin Can of ???":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(8971);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("ebiltakeover", "Smorgasbord", req.Name, quant, false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "PACVEC Mach 1.0":
                case "PACVEC Helm":
                case "PACVEC Visor":
                case "PACVEC Guard":
                case "PACVEC Battle Wings":
                case "PACVEC Battle Hammer":
                case "PACVEC Railgun":
                case "PACVEC Alien":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("ebilmech", "Ebil Mech Dragon", req.Name, quant, false);
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("70594", "PACV Mach 2.0", "Mode: [select] only\nShould the bot buy \"PACV Mach 2.0\" ?", false),
        new Option<bool>("70595", "PACV Helm", "Mode: [select] only\nShould the bot buy \"PACV Helm\" ?", false),
        new Option<bool>("70596", "PACV Visor", "Mode: [select] only\nShould the bot buy \"PACV Visor\" ?", false),
        new Option<bool>("70597", "PACV Guard", "Mode: [select] only\nShould the bot buy \"PACV Guard\" ?", false),
        new Option<bool>("70598", "PACV Battle Wings", "Mode: [select] only\nShould the bot buy \"PACV Battle Wings\" ?", false),
        new Option<bool>("70603", "PACV Battle Hammer", "Mode: [select] only\nShould the bot buy \"PACV Battle Hammer\" ?", false),
        new Option<bool>("70605", "PACV Railgun", "Mode: [select] only\nShould the bot buy \"PACV Railgun\" ?", false),
        new Option<bool>("70600", "PACV Alien", "Mode: [select] only\nShould the bot buy \"PACV Alien\" ?", false),
    };
}
