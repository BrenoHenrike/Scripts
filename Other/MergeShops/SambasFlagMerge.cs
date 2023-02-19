/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/SambaFlag.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class SambasFlagMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    public SambasFlag SF = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Costume Piece", "Ceremonial Standard", "Cavaquinho", "Pandeiro", "Tantan" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //story for materials quest.
        SF.StoryLine();

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("sambaflag", 2237, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Ceremonial Standard":
                case "Costume Piece":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(9115);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("sambaflag", "Flag Bearer", "Flag Standard");
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("sambaflag", "Master Of Ceremonies", "Ceremony Feathe");
                        Core.Logger("This item is not setup yet");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Pandeiro":
                case "Tantan":
                case "Cavaquinho":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("sambaflag", "Master Of Ceremonies", req.Name, quant);
                    Bot.Wait.ForPickup(req.Name);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("76366", "Sambista Armor", "Mode: [select] only\nShould the bot buy \"Sambista Armor\" ?", false),
        new Option<bool>("76367", "Sambista Helm", "Mode: [select] only\nShould the bot buy \"Sambista Helm\" ?", false),
        new Option<bool>("76371", "Dorival The Moglin", "Mode: [select] only\nShould the bot buy \"Dorival The Moglin\" ?", false),
        new Option<bool>("76372", "Jorge The Moglin", "Mode: [select] only\nShould the bot buy \"Jorge The Moglin\" ?", false),
        new Option<bool>("76373", "Zeca The Moglin", "Mode: [select] only\nShould the bot buy \"Zeca The Moglin\" ?", false),
        new Option<bool>("76375", "Encanto Sambista Armor", "Mode: [select] only\nShould the bot buy \"Encanto Sambista Armor\" ?", false),
        new Option<bool>("76376", "Encanto Sambista Hat", "Mode: [select] only\nShould the bot buy \"Encanto Sambista Hat\" ?", false),
        new Option<bool>("76377", "Encanto Cavaquinho", "Mode: [select] only\nShould the bot buy \"Encanto Cavaquinho\" ?", false),
        new Option<bool>("76378", "Encanto Pandeiro", "Mode: [select] only\nShould the bot buy \"Encanto Pandeiro\" ?", false),
        new Option<bool>("76379", "Encanto Tantan", "Mode: [select] only\nShould the bot buy \"Encanto Tantan\" ?", false),
    };
}
