/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/Nation/Fiendshard.cs
//cs_include Scripts/Story/Nation/Originul.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class FiendshardMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public Fiendshard_Story Fiendshard = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Shard of the Shard", "Void General Surveillance " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("fiendshard", 1965, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Shard of the Shard":
                    Fiendshard.Fiendshard_Questline();
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(7901);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        //De-shard the Shard 7901
                        Core.HuntMonster("Fiendshard", "Nulgath's Fiend Shard", "Piece of the Shard");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Void General Surveillance":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("Fiendshard", "Dirtlicker", req.Name, isTemp: false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("58887", "Dirt Void of Nulgath", "Mode: [select] only\nShould the bot buy \"Dirt Void of Nulgath\" ?", false),
        new Option<bool>("58888", "Dirt Guard of Nulgath", "Mode: [select] only\nShould the bot buy \"Dirt Guard of Nulgath\" ?", false),
        new Option<bool>("58889", "Grand Dirt Guard of Nulgath", "Mode: [select] only\nShould the bot buy \"Grand Dirt Guard of Nulgath\" ?", false),
        new Option<bool>("58890", "Terran Shard of Nulgath", "Mode: [select] only\nShould the bot buy \"Terran Shard of Nulgath\" ?", false),
        new Option<bool>("58891", "Terran Annihilator of Nulgath", "Mode: [select] only\nShould the bot buy \"Terran Annihilator of Nulgath\" ?", false),
        new Option<bool>("58892", "Earthsplitter of Nulgath", "Mode: [select] only\nShould the bot buy \"Earthsplitter of Nulgath\" ?", false),
        new Option<bool>("58893", "Crystal Void of Nulgath", "Mode: [select] only\nShould the bot buy \"Crystal Void of Nulgath\" ?", false),
        new Option<bool>("58894", "Crystal Guard of Nulgath", "Mode: [select] only\nShould the bot buy \"Crystal Guard of Nulgath\" ?", false),
        new Option<bool>("58895", "Grand Crystal Guard of Nulgath", "Mode: [select] only\nShould the bot buy \"Grand Crystal Guard of Nulgath\" ?", false),
        new Option<bool>("58896", "Crystal Shard of Nulgath", "Mode: [select] only\nShould the bot buy \"Crystal Shard of Nulgath\" ?", false),
        new Option<bool>("58897", "Crystal Annihilator of Nulgath", "Mode: [select] only\nShould the bot buy \"Crystal Annihilator of Nulgath\" ?", false),
        new Option<bool>("58898", "Crystalsplitter of Nulgath", "Mode: [select] only\nShould the bot buy \"Crystalsplitter of Nulgath\" ?", false),
        new Option<bool>("59144", "Fiendish Gem Pet", "Mode: [select] only\nShould the bot buy \"Fiendish Gem Pet\" ?", false),
        new Option<bool>("59140", "Void General Oversight", "Mode: [select] only\nShould the bot buy \"Void General Oversight\" ?", false),
    };
}
