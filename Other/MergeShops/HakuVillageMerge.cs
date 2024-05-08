/*
name: Haku Village Merge
description: This bot will farm the items belonging to the selected mode for the Haku Village Merge [2414] in /hakuvillage
tags: haku, village, merge, hakuvillage, stylish, qipao, modern, trend, bun, dragons, favor, fan, fans, elegant, vogue, elegance, sacred, forest, dragon, dragoness, morph, tail, wings, cleyera, sakaki, katana, katanas, cypress, fang, fangs
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
        Core.BankingBlackList.AddRange(new[] { "Tengu Feather", "Mikoto's Puppet String", "Yokai Realm Moss" });
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

                case "Mikoto's Puppet String":
                    DOY.YokaiRealm();
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(9690);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("yokairealm", "Mikoto Kukol'nyy", "Mikoto's Red String", 3, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Yokai Realm Moss":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("yokairealm", "Snake Shikigami", req.Name, quant, false, false);
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
        new Option<bool>("83832", "Sacred Forest Dragon", "Mode: [select] only\nShould the bot buy \"Sacred Forest Dragon\" ?", false),
        new Option<bool>("83835", "Sacred Forest Dragon Hair", "Mode: [select] only\nShould the bot buy \"Sacred Forest Dragon Hair\" ?", false),
        new Option<bool>("83836", "Sacred Forest Dragoness Locks", "Mode: [select] only\nShould the bot buy \"Sacred Forest Dragoness Locks\" ?", false),
        new Option<bool>("83837", "Forest Dragon Morph", "Mode: [select] only\nShould the bot buy \"Forest Dragon Morph\" ?", false),
        new Option<bool>("83838", "Forest Dragoness Visage", "Mode: [select] only\nShould the bot buy \"Forest Dragoness Visage\" ?", false),
        new Option<bool>("83839", "Sacred Forest Dragon Morph", "Mode: [select] only\nShould the bot buy \"Sacred Forest Dragon Morph\" ?", false),
        new Option<bool>("83840", "Sacred Forest Dragoness Visage", "Mode: [select] only\nShould the bot buy \"Sacred Forest Dragoness Visage\" ?", false),
        new Option<bool>("83842", "Sacred Forest Dragon Tail", "Mode: [select] only\nShould the bot buy \"Sacred Forest Dragon Tail\" ?", false),
        new Option<bool>("83843", "Sacred Forest Dragon Wings", "Mode: [select] only\nShould the bot buy \"Sacred Forest Dragon Wings\" ?", false),
        new Option<bool>("83844", "Sacred Forest Dragon Wings and Tail", "Mode: [select] only\nShould the bot buy \"Sacred Forest Dragon Wings and Tail\" ?", false),
        new Option<bool>("83847", "Cleyera Sakaki Katana", "Mode: [select] only\nShould the bot buy \"Cleyera Sakaki Katana\" ?", false),
        new Option<bool>("83848", "Cleyera Sakaki Katanas", "Mode: [select] only\nShould the bot buy \"Cleyera Sakaki Katanas\" ?", false),
        new Option<bool>("83850", "Cypress Dragon Fang", "Mode: [select] only\nShould the bot buy \"Cypress Dragon Fang\" ?", false),
        new Option<bool>("83851", "Cypress Dragon Fangs", "Mode: [select] only\nShould the bot buy \"Cypress Dragon Fangs\" ?", false),
    };
}
