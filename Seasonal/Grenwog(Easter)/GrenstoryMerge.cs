/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class GrenstoryMerge
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
        Core.BankingBlackList.AddRange(new[] { "Grenstory Token"});
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("grenstory", 1236, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Grenstory Token":
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("grenstory", "Chinchilizard", req.Name, quant, isTemp: false);
                    break;

                    

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("34374", "Shamanic Summoner", "Mode: [select] only\nShould the bot buy \"Shamanic Summoner\" ?", false),
        new Option<bool>("34376", "Raptor", "Mode: [select] only\nShould the bot buy \"Raptor\" ?", false),
        new Option<bool>("34373", "Azure Egg Staff", "Mode: [select] only\nShould the bot buy \"Azure Egg Staff\" ?", false),
        new Option<bool>("34370", "Shamanic Summoner Helm", "Mode: [select] only\nShould the bot buy \"Shamanic Summoner Helm\" ?", false),
        new Option<bool>("34371", "Tribal Feathers", "Mode: [select] only\nShould the bot buy \"Tribal Feathers\" ?", false),
        new Option<bool>("34369", "Jurassic Grenwog", "Mode: [select] only\nShould the bot buy \"Jurassic Grenwog\" ?", false),
        new Option<bool>("34372", "Jurassic Grenwog Tail", "Mode: [select] only\nShould the bot buy \"Jurassic Grenwog Tail\" ?", false),
        new Option<bool>("34378", "Raptor Morph", "Mode: [select] only\nShould the bot buy \"Raptor Morph\" ?", false),
        new Option<bool>("34377", "Raptor Tail", "Mode: [select] only\nShould the bot buy \"Raptor Tail\" ?", false),
    };
}
