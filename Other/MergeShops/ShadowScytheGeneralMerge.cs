/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreDailies.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ShadowScytheGeneralMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public CoreDailies Daily = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Shadow Shield " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("shadowfall", 1644, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Shadow Shield":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Daily.DailyRoutine(3828, "lightguardwar", "Citadel Crusader", "Broken Blade");
                    if (Core.IsMember)
                        Daily.DailyRoutine(3827, "lightguardwar", "Citadel Crusader", "Broken Blade");
                    Core.EquipClass(ClassType.Solo);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("lightguardwar", "Sigrid Sunshield", req.Name, quant);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("45363", "ShadowScythe General", "Mode: [select] only\nShould the bot buy \"ShadowScythe General\" ?", false),
        new Option<bool>("45255", "ShadowScythe General Armor", "Mode: [select] only\nShould the bot buy \"ShadowScythe General Armor\" ?", false),
        new Option<bool>("45256", "ShadowScythe General Sinister Helm", "Mode: [select] only\nShould the bot buy \"ShadowScythe General Sinister Helm\" ?", false),
        new Option<bool>("45257", "ShadowScythe General Helm", "Mode: [select] only\nShould the bot buy \"ShadowScythe General Helm\" ?", false),
        new Option<bool>("45258", "ShadowScythe General Cape", "Mode: [select] only\nShould the bot buy \"ShadowScythe General Cape\" ?", false),
        new Option<bool>("45259", "ShadowScythe General Axe", "Mode: [select] only\nShould the bot buy \"ShadowScythe General Axe\" ?", false),
    };
}
