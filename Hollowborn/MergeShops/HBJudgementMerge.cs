/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class HBJudgementMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("hbchallenge", 2075, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Hollowborn Writ":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8418);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("hbchallenge", "r3", "Right", "Judge's Minion", "Judge's Minion Judged", 12);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("65728", "Hollowborn Judge In Officio", "Mode: [select] only\nShould the bot buy \"Hollowborn Judge In Officio\" ?", false),
        new Option<bool>("65729", "Hollowborn Judge", "Mode: [select] only\nShould the bot buy \"Hollowborn Judge\" ?", false),
        new Option<bool>("65730", "Hel Fö In Officio", "Mode: [select] only\nShould the bot buy \"Hel Fö In Officio\" ?", false),
        new Option<bool>("65731", "Hel Fö's Armor", "Mode: [select] only\nShould the bot buy \"Hel Fö's Armor\" ?", false),
        new Option<bool>("65732", "Hel Fö's Crown + Band", "Mode: [select] only\nShould the bot buy \"Hel Fö's Crown + Band\" ?", false),
        new Option<bool>("65733", "Hel Fö's Morph", "Mode: [select] only\nShould the bot buy \"Hel Fö's Morph\" ?", false),
        new Option<bool>("65734", "Hel Fö's Morph + Band", "Mode: [select] only\nShould the bot buy \"Hel Fö's Morph + Band\" ?", false),
        new Option<bool>("65735", "Hel Fö's Hair", "Mode: [select] only\nShould the bot buy \"Hel Fö's Hair\" ?", false),
        new Option<bool>("65736", "Hollowborn Judge's Hood", "Mode: [select] only\nShould the bot buy \"Hollowborn Judge's Hood\" ?", false),
        new Option<bool>("65737", "Hel Fö's Hat", "Mode: [select] only\nShould the bot buy \"Hel Fö's Hat\" ?", false),
        new Option<bool>("65738", "Hollowborn Altare Devotionis", "Mode: [select] only\nShould the bot buy \"Hollowborn Altare Devotionis\" ?", false),
        new Option<bool>("65739", "Hollowborn Aequitas", "Mode: [select] only\nShould the bot buy \"Hollowborn Aequitas\" ?", false),
        new Option<bool>("65740", "Hollowborn Rune Judicii", "Mode: [select] only\nShould the bot buy \"Hollowborn Rune Judicii\" ?", false),
        new Option<bool>("65741", "Hollowborn Dark Rune Judicii", "Mode: [select] only\nShould the bot buy \"Hollowborn Dark Rune Judicii\" ?", false),
        new Option<bool>("65742", "Hollowborn Judicium Imaginem", "Mode: [select] only\nShould the bot buy \"Hollowborn Judicium Imaginem\" ?", false),
        new Option<bool>("65743", "Hollowborn Jurisdictio", "Mode: [select] only\nShould the bot buy \"Hollowborn Jurisdictio\" ?", false),
        new Option<bool>("65744", "Hollowborn Judgement Ex Vi Legis", "Mode: [select] only\nShould the bot buy \"Hollowborn Judgement Ex Vi Legis\" ?", false),
        new Option<bool>("65745", "Dual Hollowborn Judgement", "Mode: [select] only\nShould the bot buy \"Dual Hollowborn Judgement\" ?", false),
        new Option<bool>("65746", "Hollowborn Punitio", "Mode: [select] only\nShould the bot buy \"Hollowborn Punitio\" ?", false),
        new Option<bool>("65747", "Dual Hollowborn Punitio", "Mode: [select] only\nShould the bot buy \"Dual Hollowborn Punitio\" ?", false),
        new Option<bool>("65748", "Hollowborn Virgam Luminum", "Mode: [select] only\nShould the bot buy \"Hollowborn Virgam Luminum\" ?", false),
        new Option<bool>("65749", "Hollowborn Consummatum Est", "Mode: [select] only\nShould the bot buy \"Hollowborn Consummatum Est\" ?", false),
        new Option<bool>("65750", "Hollowborn Bis In Idem", "Mode: [select] only\nShould the bot buy \"Hollowborn Bis In Idem\" ?", false),
        new Option<bool>("65751", "Hollowborn Remissio", "Mode: [select] only\nShould the bot buy \"Hollowborn Remissio\" ?", false),
        new Option<bool>("65752", "Hollowborn Vade Mecum", "Mode: [select] only\nShould the bot buy \"Hollowborn Vade Mecum\" ?", false),
        new Option<bool>("65753", "Hollowborn Lex et Ordo", "Mode: [select] only\nShould the bot buy \"Hollowborn Lex et Ordo\" ?", false),
    };
}
