/*
name: Yue's Design Merge
description: Farms all of the items from Yue's Design Merge.
tags: yue-s-design-merge,seasonal,merge-shop,akiba-new-year
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/AkibaNewYear/YokaiHunt.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class YuesDesignMerge
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
        Core.BankingBlackList.AddRange(new[] { "Lunar Fragment", "Etokoun Residue" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        YH.YueHuang();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("yokaihunt", 2233, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Lunar Fragment":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(9094);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("guardiantree", "r3a", "Left", "Blossoming Treeant", "Fresh Blossoms", 8);
                        Core.KillMonster("guardiantree", "r3a", "Left", "Seed Spitter", "Fresh Seeds", 8);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Etokoun Residue":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(9097);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("yokaihunt", "r6a", "Left", "*", "Etokoun Wrangled");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("73045", "Oni Hatamoto", "Mode: [select] only\nShould the bot buy \"Oni Hatamoto\" ?", false),
        new Option<bool>("73046", "Honored Oni Hatamoto", "Mode: [select] only\nShould the bot buy \"Honored Oni Hatamoto\" ?", false),
        new Option<bool>("73047", "Oni Hatamoto Hair", "Mode: [select] only\nShould the bot buy \"Oni Hatamoto Hair\" ?", false),
        new Option<bool>("73048", "Oni Hatamoto Horned Hair", "Mode: [select] only\nShould the bot buy \"Oni Hatamoto Horned Hair\" ?", false),
        new Option<bool>("73049", "Oni Hatamoto Locks", "Mode: [select] only\nShould the bot buy \"Oni Hatamoto Locks\" ?", false),
        new Option<bool>("73050", "Oni Hatamoto Mask Accessory", "Mode: [select] only\nShould the bot buy \"Oni Hatamoto Mask Accessory\" ?", false),
        new Option<bool>("73051", "Oni Hatamoto Tassels", "Mode: [select] only\nShould the bot buy \"Oni Hatamoto Tassels\" ?", false),
        new Option<bool>("73052", "Sheathed Oni Hatamoto Katana", "Mode: [select] only\nShould the bot buy \"Sheathed Oni Hatamoto Katana\" ?", false),
        new Option<bool>("73053", "Oni Hatamoto Katana", "Mode: [select] only\nShould the bot buy \"Oni Hatamoto Katana\" ?", false),
        new Option<bool>("73054", "Oni Hatamoto Katanas", "Mode: [select] only\nShould the bot buy \"Oni Hatamoto Katanas\" ?", false),
        new Option<bool>("73055", "Oni Hatamoto Katana and Sheath", "Mode: [select] only\nShould the bot buy \"Oni Hatamoto Katana and Sheath\" ?", false),
        new Option<bool>("73056", "Oni Hatamoto Naginata", "Mode: [select] only\nShould the bot buy \"Oni Hatamoto Naginata\" ?", false),
        new Option<bool>("76150", "Lunarian Astromancer", "Mode: [select] only\nShould the bot buy \"Lunarian Astromancer\" ?", false),
        new Option<bool>("76153", "Lunarian Astromancer Helmet", "Mode: [select] only\nShould the bot buy \"Lunarian Astromancer Helmet\" ?", false),
        new Option<bool>("76154", "Lunarian Astromancer Helmet and Locks", "Mode: [select] only\nShould the bot buy \"Lunarian Astromancer Helmet and Locks\" ?", false),
        new Option<bool>("76155", "Lunarian Astromancer Visor", "Mode: [select] only\nShould the bot buy \"Lunarian Astromancer Visor\" ?", false),
    };
}
