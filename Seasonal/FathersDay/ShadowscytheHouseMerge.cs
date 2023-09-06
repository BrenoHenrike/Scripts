/*
name: Shadowscythe House Merge
description: This bot will farm the items belonging to the selected mode for the Shadowscythe House Merge [1731] in /nursery
tags: shadowscythe, house, merge, nursery, sepulchures, wing, shadow, family, quarters, moglinster, rug, desk, doom, crib, world, domination, globe, totally, dead, plant, experiments, toxic, cauldron, scattered, legows, bone, blocks, phone, chattering, teeth, pyramid, jackinthebox, green, floor, sludge, side, table, bottles, l, r, kitchen
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/FathersDay/HoratioQuests.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ShadowscytheHouseMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private HoratioQuests HQ = new();



    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Bones", "Glue", "Darkness" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        HQ.Horatio();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("nursery", 1731, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Bones":
                    Core.FarmingLogger(req.Name, quant);

                    Core.HuntMonster("nursery", "Skeletal Minion", req.Name, quant, false, true);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Glue":
                    Core.FarmingLogger(req.Name, quant);
                    Core.HuntMonster("nursery", "Flesh Golem", req.Name, quant, false, true);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Darkness":
                    Core.FarmingLogger(req.Name, quant);
                    Core.HuntMonster("nursery", "Spilled Ink", req.Name, quant, false, true);
                    Bot.Wait.ForPickup(req.Name);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("48874", "Sepulchure's Wing House", "Mode: [select] only\nShould the bot buy \"Sepulchure's Wing House\" ?", false),
        new Option<bool>("48875", "Shadow Family Quarters", "Mode: [select] only\nShould the bot buy \"Shadow Family Quarters\" ?", false),
        new Option<bool>("48845", "Moglinster Rug", "Mode: [select] only\nShould the bot buy \"Moglinster Rug\" ?", false),
        new Option<bool>("48844", "Desk of Doom", "Mode: [select] only\nShould the bot buy \"Desk of Doom\" ?", false),
        new Option<bool>("48846", "Crib of Doom", "Mode: [select] only\nShould the bot buy \"Crib of Doom\" ?", false),
        new Option<bool>("48847", "World Domination Globe", "Mode: [select] only\nShould the bot buy \"World Domination Globe\" ?", false),
        new Option<bool>("48851", "Totally Dead Plant", "Mode: [select] only\nShould the bot buy \"Totally Dead Plant\" ?", false),
        new Option<bool>("48848", "Experiments", "Mode: [select] only\nShould the bot buy \"Experiments\" ?", false),
        new Option<bool>("48850", "Toxic Cauldron", "Mode: [select] only\nShould the bot buy \"Toxic Cauldron\" ?", false),
        new Option<bool>("48852", "Scattered LegOWs", "Mode: [select] only\nShould the bot buy \"Scattered LegOWs\" ?", false),
        new Option<bool>("48856", "Bone Blocks", "Mode: [select] only\nShould the bot buy \"Bone Blocks\" ?", false),
        new Option<bool>("48857", "Bone Phone", "Mode: [select] only\nShould the bot buy \"Bone Phone\" ?", false),
        new Option<bool>("48853", "Chattering Teeth", "Mode: [select] only\nShould the bot buy \"Chattering Teeth\" ?", false),
        new Option<bool>("48855", "Bone Pyramid", "Mode: [select] only\nShould the bot buy \"Bone Pyramid\" ?", false),
        new Option<bool>("48854", "Jack-in-the-Box", "Mode: [select] only\nShould the bot buy \"Jack-in-the-Box\" ?", false),
        new Option<bool>("48860", "Green Floor Sludge", "Mode: [select] only\nShould the bot buy \"Green Floor Sludge\" ?", false),
        new Option<bool>("48863", "Shadow Side Table", "Mode: [select] only\nShould the bot buy \"Shadow Side Table\" ?", false),
        new Option<bool>("48865", "Bottles Table (L)", "Mode: [select] only\nShould the bot buy \"Bottles Table (L)\" ?", false),
        new Option<bool>("48864", "Bottles Table (R)", "Mode: [select] only\nShould the bot buy \"Bottles Table (R)\" ?", false),
        new Option<bool>("48866", "Shadow Kitchen", "Mode: [select] only\nShould the bot buy \"Shadow Kitchen\" ?", false),
    };
}
