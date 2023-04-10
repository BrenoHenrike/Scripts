/*
name: FutureWarMerge
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Seasonal//StaffBirthdays/DageTheEvil/FutureLegion.cs

using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class FutureWarMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    public CoreLegion Legion = new CoreLegion();
    public FutureLegion FutureLegion = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Legion Token", "ProtoSoul Gem", "UnDeath Core" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        if (!Core.isSeasonalMapActive("futurewar"))
            return;

        FutureLegion.DoStory();

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("futurewar", 1378, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Legion Token":
                    Legion.FarmLegionToken(quant);
                    break;

                case "UnDeath Core":
                case "ProtoSoul Gem":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("futurewar", "SF3017 Paragonator", req.Name, quant, log: false);
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("38790", "Eternal Dark Caster", "Mode: [select] only\nShould the bot buy \"Eternal Dark Caster\" ?", false),
        new Option<bool>("38782", "Eternal Dark Caster X Hood", "Mode: [select] only\nShould the bot buy \"Eternal Dark Caster X Hood\" ?", false),
        new Option<bool>("38785", "Eternal Dark Caster Runes", "Mode: [select] only\nShould the bot buy \"Eternal Dark Caster Runes\" ?", false),
        new Option<bool>("38786", "Eternal Infernal Dark Wings", "Mode: [select] only\nShould the bot buy \"Eternal Infernal Dark Wings\" ?", false),
        new Option<bool>("38787", "Eternal Dark Caster Wings", "Mode: [select] only\nShould the bot buy \"Eternal Dark Caster Wings\" ?", false),
        new Option<bool>("38788", "Eternal Dark Caster Winged Rune", "Mode: [select] only\nShould the bot buy \"Eternal Dark Caster Winged Rune\" ?", false),
        new Option<bool>("38783", "Eternal Dark Caster X Spikes", "Mode: [select] only\nShould the bot buy \"Eternal Dark Caster X Spikes\" ?", false),
        new Option<bool>("38784", "Eternal Dark Caster X Locks", "Mode: [select] only\nShould the bot buy \"Eternal Dark Caster X Locks\" ?", false),
        new Option<bool>("38789", "Eternal Dark Caster X", "Mode: [select] only\nShould the bot buy \"Eternal Dark Caster X\" ?", false),
        new Option<bool>("38761", "Dreadnaught House", "Mode: [select] only\nShould the bot buy \"Dreadnaught House\" ?", false),
        new Option<bool>("38615", "SF 3017 Armor", "Mode: [select] only\nShould the bot buy \"SF 3017 Armor\" ?", false),
        new Option<bool>("38638", "ProtoParagon Samurai Helm", "Mode: [select] only\nShould the bot buy \"ProtoParagon Samurai Helm\" ?", false),
        new Option<bool>("38644", "ProtoParagon Samurai Skull", "Mode: [select] only\nShould the bot buy \"ProtoParagon Samurai Skull\" ?", false),
        new Option<bool>("38616", "Uw3017 Blaster", "Mode: [select] only\nShould the bot buy \"Uw3017 Blaster\" ?", false),
        new Option<bool>("38719", "Proto Blasters", "Mode: [select] only\nShould the bot buy \"Proto Blasters\" ?", false),
        new Option<bool>("38718", "Proto Blaster", "Mode: [select] only\nShould the bot buy \"Proto Blaster\" ?", false),
        new Option<bool>("38716", "Uw3017 Blaster Mask", "Mode: [select] only\nShould the bot buy \"Uw3017 Blaster Mask\" ?", false),
        new Option<bool>("38664", "Uw3017 Helm", "Mode: [select] only\nShould the bot buy \"Uw3017 Helm\" ?", false),
        new Option<bool>("38631", "Uw3017 Cape", "Mode: [select] only\nShould the bot buy \"Uw3017 Cape\" ?", false),
        new Option<bool>("38648", "Uw3017 Hooded Mask", "Mode: [select] only\nShould the bot buy \"Uw3017 Hooded Mask\" ?", false),
        new Option<bool>("38643", "ProtoParagon Samurai Mask + Scarf", "Mode: [select] only\nShould the bot buy \"ProtoParagon Samurai Mask + Scarf\" ?", false),
        new Option<bool>("38642", "ProtoParagon Samurai Mask", "Mode: [select] only\nShould the bot buy \"ProtoParagon Samurai Mask\" ?", false),
        new Option<bool>("38641", "ProtoParagon Samurai Hood", "Mode: [select] only\nShould the bot buy \"ProtoParagon Samurai Hood\" ?", false),
        new Option<bool>("38621", "Shogun Paragon Pet", "Mode: [select] only\nShould the bot buy \"Shogun Paragon Pet\" ?", false),
    };
}
