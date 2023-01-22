/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Darkon/CoreDarkon.cs
//cs_include Scripts/Story/ElegyofMadness(Darkon)/CoreAstravia.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class EridaniPastMerge
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

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("eridanipast", 2112, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Bandit's Correspondence":
                    Darkon.BanditsCorrespondence(quant);
                    break;

                case "Suki's Sword":
                    Core.HuntMonsterMapID("eridanipast", 19, req.Name, isTemp: false);
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("67959", "Bandit Armor", "Mode: [select] only\nShould the bot buy \"Bandit Armor\" ?", false),
        new Option<bool>("67953", "Aurola's Casual Wear", "Mode: [select] only\nShould the bot buy \"Aurola's Casual Wear\" ?", false),
        new Option<bool>("67954", "Aurola's Hair", "Mode: [select] only\nShould the bot buy \"Aurola's Hair\" ?", false),
        new Option<bool>("67955", "Aurola's Morph", "Mode: [select] only\nShould the bot buy \"Aurola's Morph\" ?", false),
        new Option<bool>("67956", "Darkon's Casual Wear", "Mode: [select] only\nShould the bot buy \"Darkon's Casual Wear\" ?", false),
        new Option<bool>("67957", "Prince Darkon's Hair", "Mode: [select] only\nShould the bot buy \"Prince Darkon's Hair\" ?", false),
        new Option<bool>("67958", "Prince Darkon's Morph", "Mode: [select] only\nShould the bot buy \"Prince Darkon's Morph\" ?", false),
        new Option<bool>("67966", "Astravian Secretary", "Mode: [select] only\nShould the bot buy \"Astravian Secretary\" ?", false),
        new Option<bool>("67967", "Astravian Royal Secretary", "Mode: [select] only\nShould the bot buy \"Astravian Royal Secretary\" ?", false),
        new Option<bool>("67968", "Astravian Secretary Cut", "Mode: [select] only\nShould the bot buy \"Astravian Secretary Cut\" ?", false),
        new Option<bool>("67969", "Astravian Secretary Hair", "Mode: [select] only\nShould the bot buy \"Astravian Secretary Hair\" ?", false),
        new Option<bool>("67970", "Astravian Secretary Cut + Glasses", "Mode: [select] only\nShould the bot buy \"Astravian Secretary Cut + Glasses\" ?", false),
        new Option<bool>("67971", "Astravian Secretary Hair + Glasses", "Mode: [select] only\nShould the bot buy \"Astravian Secretary Hair + Glasses\" ?", false),
        new Option<bool>("67965", "Suki's Seraphic Sword", "Mode: [select] only\nShould the bot buy \"Suki's Seraphic Sword\" ?", false),
    };
}
