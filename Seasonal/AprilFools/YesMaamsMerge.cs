/*
name: Yes, Ma'am's Merge
description: This bot will farm the items belonging to the selected mode for the Yes, Maams Merge [2425] in /ebilart
tags: yes, ma'ams, merge, ebilart, ebilnet, specialist, operator, ebil, company, scavenger, broken, binary, monitor, flashlights, flashlight, signs, sign, neganet, protective, gear, code, tank, tanks, netspace, portal, red, shadescadoo, banner, gray, tear, in, reality, chairmans, signature
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal\AprilFools\EbilArt.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class YesMaamsMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private EbilArt EA = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Epic Item Name", "Ebil Company Sign" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        if (!Core.isSeasonalMapActive("ebilart"))
            return;

        EA.StoryLine();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("ebilart", 2425, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Epic Item Name":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(9660); // Gathering Evidence (9660)
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("ebilart", "Ebil AI Blender", "AI Learning Algorithm", 1643631, log: false);
                        Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("ebilart", "UNUNSkellingdens", "Blurry Teeth", 94543, log: false);
                        Core.HuntMonster("ebilart", "Fish", "Wet Sashimi", 64731, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Ebil Company Sign":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("ebilart", "Ebil AI Blender", req.Name, quant, false, false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("85061", "EbilNet Specialist Operator", "Mode: [select] only\nShould the bot buy \"EbilNet Specialist Operator\" ?", false),
        new Option<bool>("85060", "EbilNet Operator", "Mode: [select] only\nShould the bot buy \"EbilNet Operator\" ?", false),
        new Option<bool>("85050", "Ebil Company Scavenger", "Mode: [select] only\nShould the bot buy \"Ebil Company Scavenger\" ?", false),
        new Option<bool>("85049", "Ebil Company", "Mode: [select] only\nShould the bot buy \"Ebil Company\" ?", false),
        new Option<bool>("85062", "EbilNet Helm", "Mode: [select] only\nShould the bot buy \"EbilNet Helm\" ?", false),
        new Option<bool>("85052", "Ebil Company Helmet", "Mode: [select] only\nShould the bot buy \"Ebil Company Helmet\" ?", false),
        new Option<bool>("84987", "Broken Binary Monitor", "Mode: [select] only\nShould the bot buy \"Broken Binary Monitor\" ?", false),
        new Option<bool>("85067", "EbilNet Flashlights", "Mode: [select] only\nShould the bot buy \"EbilNet Flashlights\" ?", false),
        new Option<bool>("85066", "EbilNet Flashlight", "Mode: [select] only\nShould the bot buy \"EbilNet Flashlight\" ?", false),
        new Option<bool>("85065", "EbilNet Signs", "Mode: [select] only\nShould the bot buy \"EbilNet Signs\" ?", false),
        new Option<bool>("85064", "EbilNet Sign", "Mode: [select] only\nShould the bot buy \"EbilNet Sign\" ?", false),
        new Option<bool>("84994", "NegaNet Protective Gear", "Mode: [select] only\nShould the bot buy \"NegaNet Protective Gear\" ?", false),
        new Option<bool>("85063", "EbilNet Code Tank", "Mode: [select] only\nShould the bot buy \"EbilNet Code Tank\" ?", false),
        new Option<bool>("85053", "Ebil Company Tanks", "Mode: [select] only\nShould the bot buy \"Ebil Company Tanks\" ?", false),
        new Option<bool>("84990", "Netspace Portal", "Mode: [select] only\nShould the bot buy \"Netspace Portal\" ?", false),
        new Option<bool>("85021", "Red ShadeScadoo Banner", "Mode: [select] only\nShould the bot buy \"Red ShadeScadoo Banner\" ?", false),
        new Option<bool>("85020", "Gray ShadeScadoo Banner", "Mode: [select] only\nShould the bot buy \"Gray ShadeScadoo Banner\" ?", false),
        new Option<bool>("85013", "Tear in Reality", "Mode: [select] only\nShould the bot buy \"Tear in Reality\" ?", false),
        new Option<bool>("85012", "Chairman's Signature", "Mode: [select] only\nShould the bot buy \"Chairman's Signature\" ?", false),
    };
}
