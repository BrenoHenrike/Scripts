//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Legion/CoreLegion.cs

using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class UbearMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public CoreLegion Legion = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Ubear X Pass", "Legion Token "});
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("ubear", 1849, findIngredients);

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

                case "Ubear X Pass":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("limft", "Ubear", req.Name, quant, isTemp: false);
                    break;

                case "Legion Token":
                    Legion.FarmLegionToken(quant);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("54015", "Old Skewl Helm", "Mode: [select] only\nShould the bot buy \"Old Skewl Helm\" ?", false),
        new Option<bool>("54016", "Old Skewl Locks", "Mode: [select] only\nShould the bot buy \"Old Skewl Locks\" ?", false),
        new Option<bool>("54017", "Old Skewl Pet", "Mode: [select] only\nShould the bot buy \"Old Skewl Pet\" ?", false),
        new Option<bool>("54030", "Mystical Plank Of Awe", "Mode: [select] only\nShould the bot buy \"Mystical Plank Of Awe\" ?", false),
        new Option<bool>("54018", "DoomKeKnight Helm", "Mode: [select] only\nShould the bot buy \"DoomKeKnight Helm\" ?", false),
        new Option<bool>("54019", "Legion DoomKeKnight Helm", "Mode: [select] only\nShould the bot buy \"Legion DoomKeKnight Helm\" ?", false),
        new Option<bool>("54022", "Peace Ducky Pet", "Mode: [select] only\nShould the bot buy \"Peace Ducky Pet\" ?", false),
        new Option<bool>("54020", "Double Trobble Pet", "Mode: [select] only\nShould the bot buy \"Double Trobble Pet\" ?", false),
        new Option<bool>("54021", "Team Double Trobble Pet", "Mode: [select] only\nShould the bot buy \"Team Double Trobble Pet\" ?", false),
        new Option<bool>("54034", "Not So Invisible Ninja", "Mode: [select] only\nShould the bot buy \"Not So Invisible Ninja\" ?", false),
    };
}
