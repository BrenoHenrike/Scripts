/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/QueenofMonsters/CoreQOM.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class DeepTunnelMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    public CoreQOM QOM = new();
    public CoreNation Nation = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Pure Monstrite", "Unidentified 23", "Blood Gem of the Archfiend" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        QOM.TheQueensSecrets(true);
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("transformation", 2002, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Pure Monstrite":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.AddDrop("Queen's Follower Slain");
                    Core.RegisterQuests(8095);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("transformation", "Enter", "Spawn", "*", "Queen's Follower Slain", 100, false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Unidentified 23":
                    Core.FarmingLogger(req.Name, quant);
                    Nation.TheAssistant(req.Name, quant);
                    break;

                case "Blood Gem of the Archfiend":
                    Core.FarmingLogger(req.Name, quant);
                    Nation.FarmBloodGem(quant);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("61090", "Chaorrupted Sorcerer", "Mode: [select] only\nShould the bot buy \"Chaorrupted Sorcerer\" ?", false),
        new Option<bool>("61089", "Chaorrupted Sorcerer's Staff", "Mode: [select] only\nShould the bot buy \"Chaorrupted Sorcerer's Staff\" ?", false),
        new Option<bool>("61115", "Chaotic Sorcerer's Staff", "Mode: [select] only\nShould the bot buy \"Chaotic Sorcerer's Staff\" ?", false),
        new Option<bool>("61091", "Chaorrupted Sorcerer's Hood", "Mode: [select] only\nShould the bot buy \"Chaorrupted Sorcerer's Hood\" ?", false),
        new Option<bool>("61092", "Chaorrupted Sorcerer's Spikes", "Mode: [select] only\nShould the bot buy \"Chaorrupted Sorcerer's Spikes\" ?", false),
        new Option<bool>("61093", "Chaorrupted Sorcerer's Headdress", "Mode: [select] only\nShould the bot buy \"Chaorrupted Sorcerer's Headdress\" ?", false),
        new Option<bool>("61094", "Chaorrupted Sorcerer's Rise", "Mode: [select] only\nShould the bot buy \"Chaorrupted Sorcerer's Rise\" ?", false),
        new Option<bool>("61095", "Chaorrupted Sorcerer's Ascension", "Mode: [select] only\nShould the bot buy \"Chaorrupted Sorcerer's Ascension\" ?", false),
        new Option<bool>("61096", "Chaorrupted Sorcerer's Rune", "Mode: [select] only\nShould the bot buy \"Chaorrupted Sorcerer's Rune\" ?", false),
        new Option<bool>("59223", "Void Sorcerer", "Mode: [select] only\nShould the bot buy \"Void Sorcerer\" ?", false),
        new Option<bool>("59222", "Void Sorcerer's Staff", "Mode: [select] only\nShould the bot buy \"Void Sorcerer's Staff\" ?", false),
        new Option<bool>("59224", "Void Sorcerer's Hood", "Mode: [select] only\nShould the bot buy \"Void Sorcerer's Hood\" ?", false),
        new Option<bool>("59225", "Void Sorcerer's Spikes", "Mode: [select] only\nShould the bot buy \"Void Sorcerer's Spikes\" ?", false),
        new Option<bool>("59226", "Void Sorcerer's Headdress", "Mode: [select] only\nShould the bot buy \"Void Sorcerer's Headdress\" ?", false),
        new Option<bool>("59227", "Void Sorcerer's Rise", "Mode: [select] only\nShould the bot buy \"Void Sorcerer's Rise\" ?", false),
        new Option<bool>("59228", "Void Sorcerer's Ascension", "Mode: [select] only\nShould the bot buy \"Void Sorcerer's Ascension\" ?", false),
        new Option<bool>("59229", "Void Sorcerer's Rune", "Mode: [select] only\nShould the bot buy \"Void Sorcerer's Rune\" ?", false),
        new Option<bool>("59217", "Chaorrupted Usurper", "Mode: [select] only\nShould the bot buy \"Chaorrupted Usurper\" ?", false),
        new Option<bool>("59218", "Chaorrupted Usurper's Crown", "Mode: [select] only\nShould the bot buy \"Chaorrupted Usurper's Crown\" ?", false),
        new Option<bool>("59219", "Chaorrupted Usurper's Headdress", "Mode: [select] only\nShould the bot buy \"Chaorrupted Usurper's Headdress\" ?", false),
        new Option<bool>("59220", "Chaorrupted Usurper's Spiked Helm", "Mode: [select] only\nShould the bot buy \"Chaorrupted Usurper's Spiked Helm\" ?", false),
        new Option<bool>("59221", "Chaorrupted Usurper's Phantasm", "Mode: [select] only\nShould the bot buy \"Chaorrupted Usurper's Phantasm\" ?", false),
    };
}
