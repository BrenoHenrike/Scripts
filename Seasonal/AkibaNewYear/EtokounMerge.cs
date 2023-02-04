/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/AkibaNewYear/YokaiHunt.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class EtokounMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private YokaiHunt YH = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Ox Medallion" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        YH.AiNoMiko();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("yokaihunt", 1975, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Ox Medallion":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(7942);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("yokaihunt", "r3", "Left", "Golden Ox Guard", "Ox Meat", 8);
                        Core.KillMonster("yokaihunt", "r6", "Left", "Ox Yokai Spirit", "Holographic Ox Meat");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("59230", "Divine Ox Garments", "Mode: [select] only\nShould the bot buy \"Divine Ox Garments\" ?", false),
        new Option<bool>("59231", "Divine Horned Hair", "Mode: [select] only\nShould the bot buy \"Divine Horned Hair\" ?", false),
        new Option<bool>("59232", "Divine Horned Headpiece", "Mode: [select] only\nShould the bot buy \"Divine Horned Headpiece\" ?", false),
        new Option<bool>("59233", "Divine Horned Headdress", "Mode: [select] only\nShould the bot buy \"Divine Horned Headdress\" ?", false),
        new Option<bool>("59234", "Divine Horned Locks", "Mode: [select] only\nShould the bot buy \"Divine Horned Locks\" ?", false),
        new Option<bool>("59235", "Divine Ox Hair", "Mode: [select] only\nShould the bot buy \"Divine Ox Hair\" ?", false),
        new Option<bool>("59236", "Divine Ox Headpiece", "Mode: [select] only\nShould the bot buy \"Divine Ox Headpiece\" ?", false),
        new Option<bool>("59237", "Divine Ox Headdress", "Mode: [select] only\nShould the bot buy \"Divine Ox Headdress\" ?", false),
        new Option<bool>("59238", "Divine Ox Locks", "Mode: [select] only\nShould the bot buy \"Divine Ox Locks\" ?", false),
        new Option<bool>("59240", "Divine Ox Jian", "Mode: [select] only\nShould the bot buy \"Divine Ox Jian\" ?", false),
        new Option<bool>("59241", "Dual Divine Ox Jians", "Mode: [select] only\nShould the bot buy \"Dual Divine Ox Jians\" ?", false),
        new Option<bool>("59431", "Yokai Hanbok", "Mode: [select] only\nShould the bot buy \"Yokai Hanbok\" ?", false),
        new Option<bool>("59432", "Heeoseutail", "Mode: [select] only\nShould the bot buy \"Heeoseutail\" ?", false),
        new Option<bool>("59433", "Jeonrip", "Mode: [select] only\nShould the bot buy \"Jeonrip\" ?", false),
        new Option<bool>("59434", "Sangtugwan", "Mode: [select] only\nShould the bot buy \"Sangtugwan\" ?", false),
        new Option<bool>("59435", "Satgat", "Mode: [select] only\nShould the bot buy \"Satgat\" ?", false),
        new Option<bool>("59436", "Sonjjang", "Mode: [select] only\nShould the bot buy \"Sonjjang\" ?", false),
        new Option<bool>("59437", "Dual Geoms on your Back", "Mode: [select] only\nShould the bot buy \"Dual Geoms on your Back\" ?", false),
        new Option<bool>("59438", "Gong Gyeog on your Back", "Mode: [select] only\nShould the bot buy \"Gong Gyeog on your Back\" ?", false),
        new Option<bool>("59439", "Jonglyu on your Back", "Mode: [select] only\nShould the bot buy \"Jonglyu on your Back\" ?", false),
        new Option<bool>("59440", "Pyojun on your Back", "Mode: [select] only\nShould the bot buy \"Pyojun on your Back\" ?", false),
        new Option<bool>("59441", "Ttogttoghan on your Back", "Mode: [select] only\nShould the bot buy \"Ttogttoghan on your Back\" ?", false),
        new Option<bool>("59442", "Usan", "Mode: [select] only\nShould the bot buy \"Usan\" ?", false),
        new Option<bool>("59443", "Geom", "Mode: [select] only\nShould the bot buy \"Geom\" ?", false),
        new Option<bool>("59444", "Dual Geom", "Mode: [select] only\nShould the bot buy \"Dual Geom\" ?", false),
        new Option<bool>("59445", "Geom2", "Mode: [select] only\nShould the bot buy \"Geom2\" ?", false),
        new Option<bool>("59446", "Dual Geom2", "Mode: [select] only\nShould the bot buy \"Dual Geom2\" ?", false),
        new Option<bool>("59447", "Duo Geoms", "Mode: [select] only\nShould the bot buy \"Duo Geoms\" ?", false),
        new Option<bool>("59448", "Dual Geom Reversed", "Mode: [select] only\nShould the bot buy \"Dual Geom Reversed\" ?", false),
        new Option<bool>("59449", "Dual Geom2 Reversed", "Mode: [select] only\nShould the bot buy \"Dual Geom2 Reversed\" ?", false),
        new Option<bool>("59450", "Gong Gyeog Moglin", "Mode: [select] only\nShould the bot buy \"Gong Gyeog Moglin\" ?", false),
        new Option<bool>("59451", "Jonglyu Moglin", "Mode: [select] only\nShould the bot buy \"Jonglyu Moglin\" ?", false),
        new Option<bool>("59452", "Pyojun Moglin", "Mode: [select] only\nShould the bot buy \"Pyojun Moglin\" ?", false),
        new Option<bool>("59453", "Ttogttoghan Moglin", "Mode: [select] only\nShould the bot buy \"Ttogttoghan Moglin\" ?", false),
    };
}
