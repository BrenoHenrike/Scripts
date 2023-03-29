/*
name: WorldsCoreMerge
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/ShadowsOfWar/CoreSoWMats.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class WorldsCoreMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    private CoreSoWMats SOWM = new();
    public CoreSoW SoW = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Acquiescence", "Unbound Thread", "Prismatic Seams " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        SoW.ShadowFlame();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("worldscore", 2182, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Acquiescence":
                    SOWM.Acquiescence(quant);
                    break;

                case "Unbound Thread":
                    SOWM.UnboundThread(quant);
                    break;

                case "Prismatic Seams":
                    SOWM.PrismaticSeams(quant);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("73570", "Mystical Devotee of Mana", "Mode: [select] only\nShould the bot buy \"Mystical Devotee of Mana\" ?", false),
        new Option<bool>("73571", "Mythical Devotee's Helm", "Mode: [select] only\nShould the bot buy \"Mythical Devotee's Helm\" ?", false),
        new Option<bool>("73572", "Mystical Devotee's Hair", "Mode: [select] only\nShould the bot buy \"Mystical Devotee's Hair\" ?", false),
        new Option<bool>("73573", "Mystical Devotee's Locks", "Mode: [select] only\nShould the bot buy \"Mystical Devotee's Locks\" ?", false),
        new Option<bool>("73574", "Mythical Devotee's Veil + Locks", "Mode: [select] only\nShould the bot buy \"Mythical Devotee's Veil + Locks\" ?", false),
        new Option<bool>("73575", "Miraculous Orb Of Divination", "Mode: [select] only\nShould the bot buy \"Miraculous Orb Of Divination\" ?", false),
        new Option<bool>("73576", "Radiant Orb Of Clarity", "Mode: [select] only\nShould the bot buy \"Radiant Orb Of Clarity\" ?", false),
        new Option<bool>("73577", "Mystical Orb Of Illusions", "Mode: [select] only\nShould the bot buy \"Mystical Orb Of Illusions\" ?", false),
        new Option<bool>("73578", "Manifestation of Mana", "Mode: [select] only\nShould the bot buy \"Manifestation of Mana\" ?", false),
        new Option<bool>("73579", "Mystical Devotee's Scimitar", "Mode: [select] only\nShould the bot buy \"Mystical Devotee's Scimitar\" ?", false),
        new Option<bool>("73580", "Mystical Devotee's Scimitars", "Mode: [select] only\nShould the bot buy \"Mystical Devotee's Scimitars\" ?", false),
        new Option<bool>("73581", "Staff Of Miracles", "Mode: [select] only\nShould the bot buy \"Staff Of Miracles\" ?", false),
        new Option<bool>("73582", "Wand Of Miracles", "Mode: [select] only\nShould the bot buy \"Wand Of Miracles\" ?", false),
        new Option<bool>("73583", "Mystical Chakram Of Illusions", "Mode: [select] only\nShould the bot buy \"Mystical Chakram Of Illusions\" ?", false),
        new Option<bool>("73584", "Mystical Chakrams Of Illusion", "Mode: [select] only\nShould the bot buy \"Mystical Chakrams Of Illusion\" ?", false),
    };
}
