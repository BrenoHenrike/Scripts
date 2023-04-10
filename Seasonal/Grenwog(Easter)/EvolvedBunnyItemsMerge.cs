/*
name: EvolvedBunnyItemsMerge
description: Farms materials and buys the items from the EvolvedBunnyItems mergeshop
tags: grenwog, evolved, bunny, merge
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class EvolvedBunnyItemsMerge
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
        Core.BankingBlackList.AddRange(new[] { "Magical Marshmallow Cheep", "Gold-foil Chocolate Bunny", "Basketful of Dyed Eggs", "Berserker Bunny" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        if (!Core.isSeasonalMapActive("grenwog"))
            return;
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("grenwog", 269, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Magical Marshmallow Cheep":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("deathgazer", "Deathgazer", req.Name, quant, isTemp: false);
                    break;

                case "Gold-foil Chocolate Bunny":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("greendragon", "Greenguard Dragon", req.Name, quant, isTemp: false);

                    break;

                case "Basketful of Dyed Eggs":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("trunk", "Greenguard Basilisk", req.Name, quant, isTemp: false);

                    break;

                case "Berserker Bunny":
                    Farm.BerserkerBunny(0, sell: false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("34414", "Evolved Bunny Berserker", "Mode: [select] only\nShould the bot buy \"Evolved Bunny Berserker\" ?", false),
        new Option<bool>("34411", "Evolved Bunny Berserker Long Sword", "Mode: [select] only\nShould the bot buy \"Evolved Bunny Berserker Long Sword\" ?", false),
        new Option<bool>("34412", "Evolved Bunny Berserker Helm", "Mode: [select] only\nShould the bot buy \"Evolved Bunny Berserker Helm\" ?", false),
        new Option<bool>("34413", "Evolved Bunny Berserker Spear", "Mode: [select] only\nShould the bot buy \"Evolved Bunny Berserker Spear\" ?", false),
    };
}
