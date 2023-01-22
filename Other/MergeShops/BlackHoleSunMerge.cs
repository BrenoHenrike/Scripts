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

public class BlackHoleSunMerge
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
        Adv.StartBuyAllMerge("blackholesun", 1268, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "4th Dimension Gem":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(5163);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("blackholesun", "Black Light Elemental", "Black Light", 5);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("35490", "Golden Anubyx Warrior", "Mode: [select] only\nShould the bot buy \"Golden Anubyx Warrior\" ?", false),
        new Option<bool>("35491", "Anubyx Headdress", "Mode: [select] only\nShould the bot buy \"Anubyx Headdress\" ?", false),
        new Option<bool>("35485", "Nega Mummy", "Mode: [select] only\nShould the bot buy \"Nega Mummy\" ?", false),
        new Option<bool>("35486", "Nega Mummy Head", "Mode: [select] only\nShould the bot buy \"Nega Mummy Head\" ?", false),
        new Option<bool>("35420", "Dimension Shard", "Mode: [select] only\nShould the bot buy \"Dimension Shard\" ?", false),
        new Option<bool>("35494", "Inverse Hound", "Mode: [select] only\nShould the bot buy \"Inverse Hound\" ?", false),
        new Option<bool>("35493", "Nega Hound", "Mode: [select] only\nShould the bot buy \"Nega Hound\" ?", false),
        new Option<bool>("35492", "High Nega Commander Spear", "Mode: [select] only\nShould the bot buy \"High Nega Commander Spear\" ?", false),
        new Option<bool>("35488", "Nega Anubyx Headdress", "Mode: [select] only\nShould the bot buy \"Nega Anubyx Headdress\" ?", false),
        new Option<bool>("35487", "Nega Anubyx Warrior", "Mode: [select] only\nShould the bot buy \"Nega Anubyx Warrior\" ?", false),
        new Option<bool>("35489", "Nega Warrior Blade", "Mode: [select] only\nShould the bot buy \"Nega Warrior Blade\" ?", false),
    };
}
