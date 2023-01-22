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

public class ChronoMerge
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
        Adv.StartBuyAllMerge("chronohub", 2011, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Time Piece":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8171);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("shadowrealmpast", "Shadow Guardian", "Shadow Guardians Defeated");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("53185", "Silver ChronoGuardian", "Mode: [select] only\nShould the bot buy \"Silver ChronoGuardian\" ?", false),
        new Option<bool>("53198", "Bronze ChronoGuardian", "Mode: [select] only\nShould the bot buy \"Bronze ChronoGuardian\" ?", false),
        new Option<bool>("53186", "Silver ChronoGuardian Helmet", "Mode: [select] only\nShould the bot buy \"Silver ChronoGuardian Helmet\" ?", false),
        new Option<bool>("53187", "Silver ChronoGuardian Mask", "Mode: [select] only\nShould the bot buy \"Silver ChronoGuardian Mask\" ?", false),
        new Option<bool>("53188", "Silver ChronoGuardian Mask + Hair", "Mode: [select] only\nShould the bot buy \"Silver ChronoGuardian Mask + Hair\" ?", false),
        new Option<bool>("53189", "Silver ChronoGuardian Mask + Locks", "Mode: [select] only\nShould the bot buy \"Silver ChronoGuardian Mask + Locks\" ?", false),
        new Option<bool>("53190", "Silver ChronoGuardian Open Helm", "Mode: [select] only\nShould the bot buy \"Silver ChronoGuardian Open Helm\" ?", false),
        new Option<bool>("53199", "Bronze ChronoGuardian Helmet", "Mode: [select] only\nShould the bot buy \"Bronze ChronoGuardian Helmet\" ?", false),
        new Option<bool>("53200", "Bronze ChronoGuardian Mask", "Mode: [select] only\nShould the bot buy \"Bronze ChronoGuardian Mask\" ?", false),
        new Option<bool>("53201", "Bronze ChronoGuardian Mask + Hair", "Mode: [select] only\nShould the bot buy \"Bronze ChronoGuardian Mask + Hair\" ?", false),
        new Option<bool>("53202", "ChronoGuardian Hair", "Mode: [select] only\nShould the bot buy \"ChronoGuardian Hair\" ?", false),
        new Option<bool>("53203", "Bronze ChronoGuardian Mask + Locks", "Mode: [select] only\nShould the bot buy \"Bronze ChronoGuardian Mask + Locks\" ?", false),
        new Option<bool>("53204", "ChronoGuardian Locks", "Mode: [select] only\nShould the bot buy \"ChronoGuardian Locks\" ?", false),
        new Option<bool>("53205", "Bronze ChronoGuardian Open Helm", "Mode: [select] only\nShould the bot buy \"Bronze ChronoGuardian Open Helm\" ?", false),
        new Option<bool>("53191", "Silver ChronoGuardian Rocket Pack", "Mode: [select] only\nShould the bot buy \"Silver ChronoGuardian Rocket Pack\" ?", false),
        new Option<bool>("53192", "Silver ChronoGuardian Bazooka Cape", "Mode: [select] only\nShould the bot buy \"Silver ChronoGuardian Bazooka Cape\" ?", false),
        new Option<bool>("53206", "Bronze ChronoGuardian Rocket Pack", "Mode: [select] only\nShould the bot buy \"Bronze ChronoGuardian Rocket Pack\" ?", false),
        new Option<bool>("53207", "Bronze ChronoGuardian Bazooka Cape", "Mode: [select] only\nShould the bot buy \"Bronze ChronoGuardian Bazooka Cape\" ?", false),
        new Option<bool>("53193", "Silver ChronoGuardian Bazooka", "Mode: [select] only\nShould the bot buy \"Silver ChronoGuardian Bazooka\" ?", false),
        new Option<bool>("53194", "Silver ChronoGuardian Blaster", "Mode: [select] only\nShould the bot buy \"Silver ChronoGuardian Blaster\" ?", false),
        new Option<bool>("53195", "Silver Overclocked Laser Blaster", "Mode: [select] only\nShould the bot buy \"Silver Overclocked Laser Blaster\" ?", false),
        new Option<bool>("53196", "Silver Overclocked Hammer", "Mode: [select] only\nShould the bot buy \"Silver Overclocked Hammer\" ?", false),
        new Option<bool>("53197", "Silver Overclocked Polearm", "Mode: [select] only\nShould the bot buy \"Silver Overclocked Polearm\" ?", false),
        new Option<bool>("53208", "Bronze ChronoGuardian Bazooka", "Mode: [select] only\nShould the bot buy \"Bronze ChronoGuardian Bazooka\" ?", false),
        new Option<bool>("53209", "Bronze ChronoGuardian Blaster", "Mode: [select] only\nShould the bot buy \"Bronze ChronoGuardian Blaster\" ?", false),
        new Option<bool>("53210", "Bronze Overclocked Laser Blaster", "Mode: [select] only\nShould the bot buy \"Bronze Overclocked Laser Blaster\" ?", false),
        new Option<bool>("53211", "Bronze Overclocked Hammer", "Mode: [select] only\nShould the bot buy \"Bronze Overclocked Hammer\" ?", false),
        new Option<bool>("53212", "Bronze Overclocked Polearm", "Mode: [select] only\nShould the bot buy \"Bronze Overclocked Polearm\" ?", false),
    };
}
