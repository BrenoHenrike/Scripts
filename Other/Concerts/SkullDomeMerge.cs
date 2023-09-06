/*
name: Skull Dome Merge
description: This bot will farm the items belonging to the selected mode for the Skull Dome Merge [2312] in /skulldome
tags: skull, dome, merge, skulldome, diabolical, techwear, punk, shave, aviators, , street, summer, bag, guitar, delinquent, bat, bats, necrobard, apprentice, bony, cap, necrotic, tune, resting, ribcage, sickle, undead, masses, sickles, bone, basher, reverb, al, fine, neo, metal, necro
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Other/Concerts/NeoMetalNecro.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Evil/ADK.cs
//cs_include Scripts/Evil/VordredsArmor.cs
//cs_include Scripts/Story/Doomwood/CoreDoomwood.cs
//cs_include Scripts/Evil/SDKA/CoreSDKA.cs
//cs_include Scripts/Other/Concerts/BattleConcert2023.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class SkullDomeMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private BattleConcertClassQuests BCCQ = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Bone Pick", "Cursed Fabric", "Rotten Meat", "Metanoia Shag", "Metanoia Shaggy Locks" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        BCCQ.BattleConcertQuests();

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("skulldome", 2312, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Bone Pick":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EnsureAccept(9327);
                        Core.HuntMonster("brainmeat", "Brain Matter", "Brain Matter", log: false);
                        Core.EnsureComplete(9327);
                    }
                    Bot.Wait.ForPickup(req.Name);
                    Core.CancelRegisteredQuests();
                    break;

                case "Cursed Fabric":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("skullhall", "Necroupie", req.Name, quant, false, false);
                    break;

                case "Rotten Meat":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("brainmeat", "Brain Matter", req.Name, quant, false, false);
                    break;

                case "Metanoia Shaggy Locks":
                case "Metanoia Shag":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("skullarena", "Bellum", req.Name, quant, false, false);
                    Core.CancelRegisteredQuests();
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("78040", "Diabolical Techwear", "Mode: [select] only\nShould the bot buy \"Diabolical Techwear\" ?", false),
        new Option<bool>("78041", "Diabolical Punk Shave", "Mode: [select] only\nShould the bot buy \"Diabolical Punk Shave\" ?", false),
        new Option<bool>("78042", "Diabolical Punk Locks", "Mode: [select] only\nShould the bot buy \"Diabolical Punk Locks\" ?", false),
        new Option<bool>("78043", "Diabolical Aviators", "Mode: [select] only\nShould the bot buy \"Diabolical Aviators\" ?", false),
        new Option<bool>("78044", "Diabolical Aviators + Locks", "Mode: [select] only\nShould the bot buy \"Diabolical Aviators + Locks\" ?", false),
        new Option<bool>("78045", "Diabolical Street Mask", "Mode: [select] only\nShould the bot buy \"Diabolical Street Mask\" ?", false),
        new Option<bool>("78046", "Diabolical Street Mask + Locks", "Mode: [select] only\nShould the bot buy \"Diabolical Street Mask + Locks\" ?", false),
        new Option<bool>("78047", "Diabolical Summer Bag", "Mode: [select] only\nShould the bot buy \"Diabolical Summer Bag\" ?", false),
        new Option<bool>("78048", "Diabolical Guitar", "Mode: [select] only\nShould the bot buy \"Diabolical Guitar\" ?", false),
        new Option<bool>("78049", "Diabolical Delinquent Bat", "Mode: [select] only\nShould the bot buy \"Diabolical Delinquent Bat\" ?", false),
        new Option<bool>("78050", "Diabolical Delinquent Bats", "Mode: [select] only\nShould the bot buy \"Diabolical Delinquent Bats\" ?", false),
        new Option<bool>("78924", "Necrobard", "Mode: [select] only\nShould the bot buy \"Necrobard\" ?", false),
        new Option<bool>("78925", "Apprentice Necrobard", "Mode: [select] only\nShould the bot buy \"Apprentice Necrobard\" ?", false),
        new Option<bool>("78926", "Bony Necrobard Cap", "Mode: [select] only\nShould the bot buy \"Bony Necrobard Cap\" ?", false),
        new Option<bool>("78927", "Bony Necrobard Cap and Locks", "Mode: [select] only\nShould the bot buy \"Bony Necrobard Cap and Locks\" ?", false),
        new Option<bool>("78928", "Necrotic Tune", "Mode: [select] only\nShould the bot buy \"Necrotic Tune\" ?", false),
        new Option<bool>("78929", "Resting Ribcage Guitar", "Mode: [select] only\nShould the bot buy \"Resting Ribcage Guitar\" ?", false),
        new Option<bool>("78930", "Ribcage Guitar", "Mode: [select] only\nShould the bot buy \"Ribcage Guitar\" ?", false),
        new Option<bool>("78931", "Sickle of the Undead Masses", "Mode: [select] only\nShould the bot buy \"Sickle of the Undead Masses\" ?", false),
        new Option<bool>("78932", "Sickles of the Undead Masses", "Mode: [select] only\nShould the bot buy \"Sickles of the Undead Masses\" ?", false),
        new Option<bool>("78959", "Bone Basher Reverb", "Mode: [select] only\nShould the bot buy \"Bone Basher Reverb\" ?", false),
        new Option<bool>("78855", "Al Fine Hair", "Mode: [select] only\nShould the bot buy \"Al Fine Hair\" ?", false),
        new Option<bool>("78856", "Al Fine Locks", "Mode: [select] only\nShould the bot buy \"Al Fine Locks\" ?", false),
        new Option<bool>("78967", "Neo Metal Necro", "Mode: [select] only\nShould the bot buy \"Neo Metal Necro\" ?", false),
    };
}
