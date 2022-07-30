//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Darkon/CoreDarkon.cs
//cs_include Scripts/Story/ElegyofMadness(Darkon)/CoreAstravia.cs
using RBot;
using RBot.Items;
using RBot.Options;

public class TheWorldMerge
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public CoreDarkon Darkon = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("theworld", 2141, findIngredients);

        #region Dont edit this part
        void findIngredients()
        {
            ItemBase req = Adv.externalItem;
            int quant = Adv.externalQuant;
            int currentQuant = req.Temp ? Bot.Inventory.GetTempQuantity(req.Name) : Bot.Inventory.GetQuantity(req.Name);
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

                case "Unfinished Musical Score":
                    Darkon.UnfinishedMusicalScore(quant);
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("70876", "Darkon's Debris 1", "Mode: [select] only\nShould the bot buy \"Darkon's Debris 1\" ?", false),
        new Option<bool>("70879", "Darkon's Debris 2 (Recovered)", "Mode: [select] only\nShould the bot buy \"Darkon's Debris 2 (Recovered)\" ?", false),
        new Option<bool>("70877", "Darkon's Hair", "Mode: [select] only\nShould the bot buy \"Darkon's Hair\" ?", false),
        new Option<bool>("70878", "Darkon's Morph", "Mode: [select] only\nShould the bot buy \"Darkon's Morph\" ?", false),
    };
}