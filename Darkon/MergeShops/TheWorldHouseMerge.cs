//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Darkon/CoreDarkon.cs
//cs_include Scripts/Story/ElegyofMadness(Darkon)/CoreAstravia.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class TheWorldHouseMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
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

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("theworld", 2144, findIngredients);

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

                case "Darkon's Receipt":
                    Darkon.FarmReceipt(quant);
                    break;

                case "Teeth":
                    Darkon.Teeth(quant);
                    break;

                case "La's Gratitude":
                    Darkon.LasGratitude(quant);
                    break;

                case "Astravian Medal":
                    Darkon.AstravianMedal(quant);
                    break;

                case "A Melody":
                    Darkon.AMelody(quant);
                    break;

                case "Bandit's Correspondence":
                    Darkon.BanditsCorrespondence(quant);
                    break;

                case "Suki's Prestige":
                    Darkon.SukisPrestiege(quant);
                    break;

                case "Ancient Remnant":
                    Darkon.AncientRemnant(quant);
                    break;

                case "Mourning Flower":
                    Darkon.MourningFlower(quant);
                    break;

                case "Unfinished Musical Score":
                    Darkon.UnfinishedMusicalScore(quant);
                    break;

                case "Darkon's Instant Noodle":
                    Adv.BuyItem("garden", 1831, req.Name, quant);
                    break;

                case "Astravia Castle House":
                    Core.HuntMonster("astraviajudge", "La", req.Name);
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("70995", "Astravia Castle Present I", "Mode: [select] only\nShould the bot buy \"Astravia Castle Present I\" ?", false),
        new Option<bool>("70994", "Astravia Castle Past I", "Mode: [select] only\nShould the bot buy \"Astravia Castle Past I\" ?", false),
        new Option<bool>("70996", "Astravia Castle Past II", "Mode: [select] only\nShould the bot buy \"Astravia Castle Past II\" ?", false),
        new Option<bool>("70997", "Astravia Castle Present II", "Mode: [select] only\nShould the bot buy \"Astravia Castle Present II\" ?", false),
        new Option<bool>("70883", "Ring Of Creation (Activated)", "Mode: [select] only\nShould the bot buy \"Ring Of Creation (Activated)\" ?", false),
    };
}
