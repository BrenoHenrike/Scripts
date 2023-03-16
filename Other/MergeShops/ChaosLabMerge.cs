/*
name: ChaosLab Mergeshop
description: Gets any item you want in ChaosLab mergeshop
tags: chaosLab, dage, alina, cysero, artix, beleen, mergeshop, daimyo, moglin, hamster, chaos, necropolis, bots,
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/ChaosLab.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ChaosLabMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreStory Story = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private ChaosLabStory ChaosLab = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Chaorrupted Hamster", "Crystallized Chaos", "Daimyo"});
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("chaoslab", 887, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Chaorrupted Hamster":
                    if (!Bot.Player.IsMember)
                    break;
                                     
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("chaoslab", "Chaotic Server Hamster", req.name, isTemp: false, log: false);
                    break;

                case "Crystallized Chaos":
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("chaoslab", "Chaorrupted Moglin", req.Name, quant, isTemp: false, log: false);
                    break;

                case "Daimyo":
                    Core.BuyItem("necropolis", 422, "Daimyo");
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("24085", "Chaorrupted BattleHamster", "Mode: [select] only\nShould the bot buy \"Chaorrupted BattleHamster\" ?", false),
        new Option<bool>("24091", "Eye Am Chaos", "Mode: [select] only\nShould the bot buy \"Eye Am Chaos\" ?", false),
        new Option<bool>("24090", "Chaorrupted Blade of Awe", "Mode: [select] only\nShould the bot buy \"Chaorrupted Blade of Awe\" ?", false),
        new Option<bool>("24094", "Chaorrupted Cysero &amp; Alina", "Mode: [select] only\nShould the bot buy \"Chaorrupted Cysero &amp; Alina\" ?", false),
        new Option<bool>("24095", "Chaorrupted Artix &amp; Beleen", "Mode: [select] only\nShould the bot buy \"Chaorrupted Artix &amp; Beleen\" ?", false),
        new Option<bool>("17849", "Chaos Daimyo", "Mode: [select] only\nShould the bot buy \"Chaos Daimyo\" ?", false),
    };
}
