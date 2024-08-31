/*
name: Royal Wentira Merge
description: This bot will farm the items belonging to the selected mode for the Royal Wentira Merge [2318] in /wentira
tags: royal, wentira, merge, wentira, pesugihan, boar, crown, maned, morph, ritual, instruments, kabasaran, master, headdress, adornment, shielded, beloved, blessing, garb, udeng, golden, bungan, mitir, blessed, beloveds, kris, knives, wiracana, fans
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/IndonesianDay/Wentira.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class RoyalWentiraMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private Wentira Wen = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Wentiran Seal", "Broken Tusk", "Gold Nugget", "Ancient Bone" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        Wen.Storyline();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("wentira", 2318, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Wentiran Seal":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(9342);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("wentira", "Pesugihan Boar", "Boar Leather", 6, log: false);
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("wentira", "Kabasaran Waranei", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Gold Nugget":
                case "Broken Tusk":
                case "Beloved Blessing Hair":
                case "Uncut Ruby":
                case "Beloved Blessing Locks":
                case "Blessed Beloved's Kris Knife":
                case "Wiracana Fan":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("wentira", "Pesugihan Boar", req.Name, quant, false, false);
                    break;

                case "Ancient Bone":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("wentira", "Kabasaran Waranei", req.Name, quant, false, false);
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("79077", "Pesugihan Boar", "Mode: [select] only\nShould the bot buy \"Pesugihan Boar\" ?", false),
        new Option<bool>("79079", "Pesugihan Boar Crown", "Mode: [select] only\nShould the bot buy \"Pesugihan Boar Crown\" ?", false),
        new Option<bool>("79081", "Royal Pesugihan Boar", "Mode: [select] only\nShould the bot buy \"Royal Pesugihan Boar\" ?", false),
        new Option<bool>("79082", "Maned Pesugihan Boar Morph", "Mode: [select] only\nShould the bot buy \"Maned Pesugihan Boar Morph\" ?", false),
        new Option<bool>("79083", "Maned Pesugihan Boar Visage", "Mode: [select] only\nShould the bot buy \"Maned Pesugihan Boar Visage\" ?", false),
        new Option<bool>("79084", "Royal Pesugihan Boar Morph", "Mode: [select] only\nShould the bot buy \"Royal Pesugihan Boar Morph\" ?", false),
        new Option<bool>("79085", "Royal Pesugihan Boar Visage", "Mode: [select] only\nShould the bot buy \"Royal Pesugihan Boar Visage\" ?", false),
        new Option<bool>("79094", "Pesugihan Ritual Instruments", "Mode: [select] only\nShould the bot buy \"Pesugihan Ritual Instruments\" ?", false),
        new Option<bool>("79117", "Kabasaran", "Mode: [select] only\nShould the bot buy \"Kabasaran\" ?", false),
        new Option<bool>("79120", "Master Kabasaran Headdress", "Mode: [select] only\nShould the bot buy \"Master Kabasaran Headdress\" ?", false),
        new Option<bool>("79121", "Master Kabasaran Adornment", "Mode: [select] only\nShould the bot buy \"Master Kabasaran Adornment\" ?", false),
        new Option<bool>("79133", "Kabasaran Polearm", "Mode: [select] only\nShould the bot buy \"Kabasaran Polearm\" ?", false),
        new Option<bool>("79136", "Kabasaran Shielded Sword", "Mode: [select] only\nShould the bot buy \"Kabasaran Shielded Sword\" ?", false),
        new Option<bool>("79137", "Kabasaran Shielded Polearm", "Mode: [select] only\nShould the bot buy \"Kabasaran Shielded Polearm\" ?", false),
        new Option<bool>("87520", "Beloved Blessing Garb", "Mode: [select] only\nShould the bot buy \"Beloved Blessing Garb\" ?", false),
        new Option<bool>("87523", "Beloved Blessing Udeng", "Mode: [select] only\nShould the bot buy \"Beloved Blessing Udeng\" ?", false),
        new Option<bool>("87524", "Beloved Blessing Adornment", "Mode: [select] only\nShould the bot buy \"Beloved Blessing Adornment\" ?", false),
        new Option<bool>("87526", "Golden Bungan Mitir", "Mode: [select] only\nShould the bot buy \"Golden Bungan Mitir\" ?", false),
        new Option<bool>("87528", "Blessed Beloved's Kris Knives", "Mode: [select] only\nShould the bot buy \"Blessed Beloved's Kris Knives\" ?", false),
        new Option<bool>("87530", "Wiracana Fans", "Mode: [select] only\nShould the bot buy \"Wiracana Fans\" ?", false),
    };
}
