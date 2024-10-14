/*
name: LotusTomb Merge
description: This bot will farm the items belonging to the selected mode for the LotusTomb Merge [2492] in /lotustomb
tags: lotustomb, merge, lotustomb, chaotic, pathshaper, scar, ripper, morph, rippers, edges, unending, light, bow, arrow, arrows, scepter
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/BlackFriday/ShadowofDoom/CoreShadowofDoom.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class LotusTombMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private CoreShadowofDoom SoD = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Corrupted Hieroglyph" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        SoD.LotusTomb();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("lotustomb", 2492, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Corrupted Hieroglyph":
                    Core.FarmingLogger(req.Name, quant);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonsterQuest(9921, new (string? mapName, string? monsterName, ClassType classType)[] {
        ("lotustomb", SoD.UMLotusTomb[4], ClassType.Solo),
        ("lotustomb", SoD.UMLotusTomb[2], ClassType.Farm),
        ("lotustomb", SoD.UMLotusTomb[3], ClassType.Farm)});
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("87844", "Chaotic Pathshaper", "Mode: [select] only\nShould the bot buy \"Chaotic Pathshaper\" ?", false),
        new Option<bool>("87848", "Chaotic Pathshaper Visage", "Mode: [select] only\nShould the bot buy \"Chaotic Pathshaper Visage\" ?", false),
        new Option<bool>("87851", "Chaotic Scar Ripper", "Mode: [select] only\nShould the bot buy \"Chaotic Scar Ripper\" ?", false),
        new Option<bool>("87847", "Chaotic Pathshaper Morph", "Mode: [select] only\nShould the bot buy \"Chaotic Pathshaper Morph\" ?", false),
        new Option<bool>("87852", "Chaotic Scar Rippers", "Mode: [select] only\nShould the bot buy \"Chaotic Scar Rippers\" ?", false),
        new Option<bool>("87854", "Chaotic Scar Edges", "Mode: [select] only\nShould the bot buy \"Chaotic Scar Edges\" ?", false),
        new Option<bool>("88822", "Unending Light Rune", "Mode: [select] only\nShould the bot buy \"Unending Light Rune\" ?", false),
        new Option<bool>("88825", "Unending Light Bow", "Mode: [select] only\nShould the bot buy \"Unending Light Bow\" ?", false),
        new Option<bool>("88826", "Unending Light Arrow", "Mode: [select] only\nShould the bot buy \"Unending Light Arrow\" ?", false),
        new Option<bool>("88827", "Unending Light Arrows", "Mode: [select] only\nShould the bot buy \"Unending Light Arrows\" ?", false),
        new Option<bool>("88828", "Unending Light Bow and Arrow", "Mode: [select] only\nShould the bot buy \"Unending Light Bow and Arrow\" ?", false),
        new Option<bool>("88823", "Unending Light Aura", "Mode: [select] only\nShould the bot buy \"Unending Light Aura\" ?", false),
        new Option<bool>("88824", "Unending Light Scepter", "Mode: [select] only\nShould the bot buy \"Unending Light Scepter\" ?", false),
    };
}
