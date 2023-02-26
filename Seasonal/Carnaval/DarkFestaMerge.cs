/*
name: Dark Festa Merge
description: This script farms the materials needed for Dark Festa Merge.
tags: seasonal, dark festa, carnaval, merge
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/Carnaval/DarkFesta.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class DarkFestaMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private DarkFesta DF = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Pena mágica", "Rainha da Bateria", "Rainha da Bateria Headdress", "Rainha da Bateria Headdress + Locks", "Rainha da Bateria Staff", "Rainha da Bateria Feathered Tail", "Rainha da Bateria Feathers" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        DF.Storyline();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("darkfesta", 1699, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Pena mágica":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("darkfesta", "Ultra Belo", req.Name, quant, false, false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

                case "Rainha da Bateria":
                case "Rainha da Bateria Headdress":
                case "Rainha da Bateria Headdress + Locks":
                case "Rainha da Bateria Staff":
                case "Rainha da Bateria Feathered Tail":
                case "Rainha da Bateria Feathers":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("darkfesta", "Dark Boitata", req.Name, quant, false, false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("47671", "Rainha do Carnaval", "Mode: [select] only\nShould the bot buy \"Rainha do Carnaval\" ?", false),
        new Option<bool>("47674", "Rainha do Carnaval Headdress", "Mode: [select] only\nShould the bot buy \"Rainha do Carnaval Headdress\" ?", false),
        new Option<bool>("47673", "Rainha do Carnaval Headdress + Locks", "Mode: [select] only\nShould the bot buy \"Rainha do Carnaval Headdress + Locks\" ?", false),
        new Option<bool>("47677", "Rainha do Carnaval Staff", "Mode: [select] only\nShould the bot buy \"Rainha do Carnaval Staff\" ?", false),
        new Option<bool>("47675", "Rainha do Carnaval Feathered Tail", "Mode: [select] only\nShould the bot buy \"Rainha do Carnaval Feathered Tail\" ?", false),
        new Option<bool>("47676", "Rainha do Carnaval Feathers", "Mode: [select] only\nShould the bot buy \"Rainha do Carnaval Feathers\" ?", false),
    };
}
