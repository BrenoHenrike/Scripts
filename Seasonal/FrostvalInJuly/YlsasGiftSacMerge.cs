/*
name: Ylsas Gift Sac Merge
description: This bot will farm the items belonging to the selected mode for the Ylsas Gift Sac Merge [1605] in /icestorm
tags: ylsas, gift, sac, merge, icestorm, winter, assassin, , bangs, bandana, shuriken, ninjato, kunai, katana
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal\FrostvalInJuly\IceStorm.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class YlsasGiftSacMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private IceStorm IS = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Burnt Bow", "Dragon Scale" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        IS.Storyline();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("icestorm", 1605, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Burnt Bow":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("icestorm", "Dragon Hunter", req.Name, quant, false, false);
                    break;

                case "Dragon Scale":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("icestorm", "Fire Dragonling", req.Name, quant, false, false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("44387", "Winter Assassin", "Mode: [select] only\nShould the bot buy \"Winter Assassin\" ?", false),
        new Option<bool>("44388", "Winter Assassin Hood", "Mode: [select] only\nShould the bot buy \"Winter Assassin Hood\" ?", false),
        new Option<bool>("44389", "Winter Assassin Hooded Mask", "Mode: [select] only\nShould the bot buy \"Winter Assassin Hooded Mask\" ?", false),
        new Option<bool>("44391", "Winter Assassin Mask", "Mode: [select] only\nShould the bot buy \"Winter Assassin Mask\" ?", false),
        new Option<bool>("44390", "Winter Assassin Mask + Bangs", "Mode: [select] only\nShould the bot buy \"Winter Assassin Mask + Bangs\" ?", false),
        new Option<bool>("44393", "Winter Assassin Hair", "Mode: [select] only\nShould the bot buy \"Winter Assassin Hair\" ?", false),
        new Option<bool>("44392", "Winter Assassin Hair + Bangs", "Mode: [select] only\nShould the bot buy \"Winter Assassin Hair + Bangs\" ?", false),
        new Option<bool>("44395", "Winter Assassin Bandana", "Mode: [select] only\nShould the bot buy \"Winter Assassin Bandana\" ?", false),
        new Option<bool>("44394", "Winter Assassin Bandana + Bangs", "Mode: [select] only\nShould the bot buy \"Winter Assassin Bandana + Bangs\" ?", false),
        new Option<bool>("44399", "Winter Assassin Shuriken", "Mode: [select] only\nShould the bot buy \"Winter Assassin Shuriken\" ?", false),
        new Option<bool>("44397", "Winter Assassin Ninjato", "Mode: [select] only\nShould the bot buy \"Winter Assassin Ninjato\" ?", false),
        new Option<bool>("44400", "Winter Assassin Kunai", "Mode: [select] only\nShould the bot buy \"Winter Assassin Kunai\" ?", false),
        new Option<bool>("44396", "Winter Assassin Katana", "Mode: [select] only\nShould the bot buy \"Winter Assassin Katana\" ?", false),
        new Option<bool>("44398", "Winter Assassin Katana + Ninjato", "Mode: [select] only\nShould the bot buy \"Winter Assassin Katana + Ninjato\" ?", false),
    };
}
