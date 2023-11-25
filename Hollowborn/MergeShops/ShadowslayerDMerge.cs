/*
name: Shadowslayer D Merge
description: This bot will farm the items belonging to the selected mode for the Shadowslayer D Merge [2369] in /hbchallenge
tags: shadowslayer, d, merge, hbchallenge, hollowborn, vampire, knight, fanged, wings, plague, calibur, vampiric, saber, sabers, lycan, ferocious, morph, claws, lord, regal, lords, territory, cap, amulet, top, veil, beard, bearded, scarf, halo, thorns, gloria, queen, pain, penance, absolution, longinus, decimation, admonition, slayers, thorn
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/Hollowborn/CoreHollowbornStory.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ShadowslayerDMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private CoreHollowbornStory CHBS = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Hollow Essence", "Hollowborn Vampire Fang", "Hollowborn Lycan Claw", "Hollowborn Lycan Morph", "Hollowborn Vampire Lord Mask", "Noble Hollowborn Vampire Wings" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        CHBS.ShadowslayerD();
     
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("hbchallenge", 2369, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Hollow Essence":
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(9487);
                    Core.HuntMonster("hbchallenge", "Sentient Hollow", req.Name, quant, req.Temp);
                    Core.CancelRegisteredQuests();
                    break;

                case "Hollowborn Vampire Fang":
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(9488);
                    Core.HuntMonster("hbchallenge", "Hollowborn Vampire", req.Name, quant, req.Temp);
                    Core.CancelRegisteredQuests();
                    break;

                case "Hollowborn Lycan Claw":
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(9489);
                    Core.HuntMonster("hbchallenge", "Hollowborn Lycan", req.Name, quant, req.Temp);
                    Core.CancelRegisteredQuests();
                    break;

                case "Hollowborn Lycan Morph":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("hbchallenge", "Hollowborn Lycan", req.Name, quant, req.Temp);
                    break;

                case "Hollowborn Vampire Lord Mask":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("hbchallenge", "Sentient Hollow", req.Name, quant, req.Temp);
                    break;

                case "Noble Hollowborn Vampire Wings":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("hbchallenge", "Hollowborn Vampire", req.Name, quant, req.Temp);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("81426", "Hollowborn Vampire Knight", "Mode: [select] only\nShould the bot buy \"Hollowborn Vampire Knight\" ?", false),
        new Option<bool>("81427", "Hollowborn Vampire Knight Helm", "Mode: [select] only\nShould the bot buy \"Hollowborn Vampire Knight Helm\" ?", false),
        new Option<bool>("81428", "Fanged Hollowborn Helm", "Mode: [select] only\nShould the bot buy \"Fanged Hollowborn Helm\" ?", false),
        new Option<bool>("81429", "Hollowborn Vampire Knight Wings", "Mode: [select] only\nShould the bot buy \"Hollowborn Vampire Knight Wings\" ?", false),
        new Option<bool>("81430", "Hollowborn Plague Calibur", "Mode: [select] only\nShould the bot buy \"Hollowborn Plague Calibur\" ?", false),
        new Option<bool>("81431", "Dual Hollowborn Plague Calibur", "Mode: [select] only\nShould the bot buy \"Dual Hollowborn Plague Calibur\" ?", false),
        new Option<bool>("81432", "Vampiric Hollowborn Saber", "Mode: [select] only\nShould the bot buy \"Vampiric Hollowborn Saber\" ?", false),
        new Option<bool>("81433", "Vampiric Hollowborn Sabers", "Mode: [select] only\nShould the bot buy \"Vampiric Hollowborn Sabers\" ?", false),
        new Option<bool>("81777", "Hollowborn Lycan", "Mode: [select] only\nShould the bot buy \"Hollowborn Lycan\" ?", false),
        new Option<bool>("81778", "Ferocious Hollowborn Lycan Morph", "Mode: [select] only\nShould the bot buy \"Ferocious Hollowborn Lycan Morph\" ?", false),
        new Option<bool>("81781", "Hollowborn Lycan Claws", "Mode: [select] only\nShould the bot buy \"Hollowborn Lycan Claws\" ?", false),
        new Option<bool>("81782", "Hollowborn Vampire Lord", "Mode: [select] only\nShould the bot buy \"Hollowborn Vampire Lord\" ?", false),
        new Option<bool>("81783", "Hollowborn Vampire Lord Morph", "Mode: [select] only\nShould the bot buy \"Hollowborn Vampire Lord Morph\" ?", false),
        new Option<bool>("81786", "Regal Hollowborn Vampire Wings", "Mode: [select] only\nShould the bot buy \"Regal Hollowborn Vampire Wings\" ?", false),
        new Option<bool>("81788", "Hollowborn Vampire Lord's Territory", "Mode: [select] only\nShould the bot buy \"Hollowborn Vampire Lord's Territory\" ?", false),
        new Option<bool>("81925", "Hollowborn Shadowslayer", "Mode: [select] only\nShould the bot buy \"Hollowborn Shadowslayer\" ?", false),
        new Option<bool>("81926", "Hollowborn Shadowslayer Hair", "Mode: [select] only\nShould the bot buy \"Hollowborn Shadowslayer Hair\" ?", false),
        new Option<bool>("81927", "Hollowborn Shadowslayer Locks", "Mode: [select] only\nShould the bot buy \"Hollowborn Shadowslayer Locks\" ?", false),
        new Option<bool>("81928", "Hollowborn Shadowslayer Cap", "Mode: [select] only\nShould the bot buy \"Hollowborn Shadowslayer Cap\" ?", false),
        new Option<bool>("81929", "Hollowborn Shadowslayer Cap and Locks", "Mode: [select] only\nShould the bot buy \"Hollowborn Shadowslayer Cap and Locks\" ?", false),
        new Option<bool>("81930", "Hollowborn Shadowslayer Amulet", "Mode: [select] only\nShould the bot buy \"Hollowborn Shadowslayer Amulet\" ?", false),
        new Option<bool>("81931", "Hollowborn Shadowslayer Locks and Amulet", "Mode: [select] only\nShould the bot buy \"Hollowborn Shadowslayer Locks and Amulet\" ?", false),
        new Option<bool>("81932", "Hollowborn Shadowslayer Top Hat", "Mode: [select] only\nShould the bot buy \"Hollowborn Shadowslayer Top Hat\" ?", false),
        new Option<bool>("81933", "Hollowborn Shadowslayer Veil", "Mode: [select] only\nShould the bot buy \"Hollowborn Shadowslayer Veil\" ?", false),
        new Option<bool>("81934", "Hollowborn Shadowslayer Beard", "Mode: [select] only\nShould the bot buy \"Hollowborn Shadowslayer Beard\" ?", false),
        new Option<bool>("81935", "Hollowborn Shadowslayer Bearded Cap", "Mode: [select] only\nShould the bot buy \"Hollowborn Shadowslayer Bearded Cap\" ?", false),
        new Option<bool>("81936", "Hollowborn Shadowslayer Scarf and Cap", "Mode: [select] only\nShould the bot buy \"Hollowborn Shadowslayer Scarf and Cap\" ?", false),
        new Option<bool>("81937", "Hollowborn Bearded Shadowslayer Amulet", "Mode: [select] only\nShould the bot buy \"Hollowborn Bearded Shadowslayer Amulet\" ?", false),
        new Option<bool>("81938", "Hollowborn Halo of Thorns", "Mode: [select] only\nShould the bot buy \"Hollowborn Halo of Thorns\" ?", false),
        new Option<bool>("81939", "Hollowborn Gloria", "Mode: [select] only\nShould the bot buy \"Hollowborn Gloria\" ?", false),
        new Option<bool>("81940", "Hollowborn Queen of Pain", "Mode: [select] only\nShould the bot buy \"Hollowborn Queen of Pain\" ?", false),
        new Option<bool>("81941", "Hollowborn Penance", "Mode: [select] only\nShould the bot buy \"Hollowborn Penance\" ?", false),
        new Option<bool>("81942", "Dual Hollowborn Penance", "Mode: [select] only\nShould the bot buy \"Dual Hollowborn Penance\" ?", false),
        new Option<bool>("81943", "Hollowborn Absolution", "Mode: [select] only\nShould the bot buy \"Hollowborn Absolution\" ?", false),
        new Option<bool>("81944", "Dual Hollowborn Absolution", "Mode: [select] only\nShould the bot buy \"Dual Hollowborn Absolution\" ?", false),
        new Option<bool>("81945", "Hollowborn Longinus", "Mode: [select] only\nShould the bot buy \"Hollowborn Longinus\" ?", false),
        new Option<bool>("81946", "Hollowborn Decimation", "Mode: [select] only\nShould the bot buy \"Hollowborn Decimation\" ?", false),
        new Option<bool>("81947", "Dual Hollowborn Decimation", "Mode: [select] only\nShould the bot buy \"Dual Hollowborn Decimation\" ?", false),
        new Option<bool>("81948", "Hollowborn Admonition", "Mode: [select] only\nShould the bot buy \"Hollowborn Admonition\" ?", false),
        new Option<bool>("81949", "Dual Hollowborn Admonition", "Mode: [select] only\nShould the bot buy \"Dual Hollowborn Admonition\" ?", false),
        new Option<bool>("81950", "Hollowborn Slayer's Thorn", "Mode: [select] only\nShould the bot buy \"Hollowborn Slayer's Thorn\" ?", false),
        new Option<bool>("81951", "Hollowborn Slayer's Thorns", "Mode: [select] only\nShould the bot buy \"Hollowborn Slayer's Thorns\" ?", false),
    };
}
