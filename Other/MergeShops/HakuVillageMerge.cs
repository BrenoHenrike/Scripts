/*
name: Haku Village Merge
description: This bot will farm the items belonging to the selected mode for the Haku Village Merge [2414] in /hakuvillage
tags: haku, village, merge, hakuvillage, stylish, qipao, modern, trend, bun, dragons, favor, fan, fans, elegant, vogue, elegance
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story\DragonsOfYokai\CoreDOY.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class HakuVillageMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private CoreDOY DOY = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Tengu Feather" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        DOY.HakuVillage();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("hakuvillage", 2414, findIngredients, buyOnlyThis, buyMode: buyMode);

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
                    bool shouldStop = !Adv.matsOnly || !dontStopMissingIng;
                    Core.Logger($"The bot hasn't been taught how to get {req.Name}." + (shouldStop ? " Please report the issue." : " Skipping"), messageBox: shouldStop, stopBot: shouldStop);
                    break;
                #endregion

                case "Tengu Feather":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(9600);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EquipClass(ClassType.Farm);
                        Core.KillMonster("hakuvillage", "r3", "Left", "*", "Enchanted Chime", 8, log: false);
                        Core.KillMonster("hakuvillage", "r4", "Left", "*", "Pale Scale", 8, log: false);

                        Core.EquipClass(ClassType.Solo);
                        Core.KillMonster("hakuvillage", "r5", "Left", "*", "Wind Blade", log: false);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("83955", "Stylish Qipao", "Mode: [select] only\nShould the bot buy \"Stylish Qipao\" ?", false),
        new Option<bool>("83956", "Modern Trend Hair", "Mode: [select] only\nShould the bot buy \"Modern Trend Hair\" ?", false),
        new Option<bool>("83957", "Modern Trend Bun", "Mode: [select] only\nShould the bot buy \"Modern Trend Bun\" ?", false),
        new Option<bool>("83958", "Dragon's Favor Fan", "Mode: [select] only\nShould the bot buy \"Dragon's Favor Fan\" ?", false),
        new Option<bool>("83959", "Dragon's Favor Fans", "Mode: [select] only\nShould the bot buy \"Dragon's Favor Fans\" ?", false),
        new Option<bool>("83960", "Elegant Qipao", "Mode: [select] only\nShould the bot buy \"Elegant Qipao\" ?", false),
        new Option<bool>("83961", "Vogue Trend Hair", "Mode: [select] only\nShould the bot buy \"Vogue Trend Hair\" ?", false),
        new Option<bool>("83962", "Vogue Trend Bun", "Mode: [select] only\nShould the bot buy \"Vogue Trend Bun\" ?", false),
        new Option<bool>("83963", "Dragon's Elegance Fan", "Mode: [select] only\nShould the bot buy \"Dragon's Elegance Fan\" ?", false),
        new Option<bool>("83964", "Dragon's Elegance Fans", "Mode: [select] only\nShould the bot buy \"Dragon's Elegance Fans\" ?", false),
    };
}
