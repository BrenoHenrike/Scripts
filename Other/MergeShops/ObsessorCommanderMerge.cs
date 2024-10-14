/*
name: Obsessor Commander Merge
description: This bot will farm the items belonging to the selected mode for the Obsessor Commander Merge [2486] in /shadowrealm
tags: taka, obsessor, commander, merge, shadowrealm, captain, cap, skull, obsessors, shadow, compass, rose, pet, cutlass, cutlasses, skulls
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreDailies.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ObsessorCommanderMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private CoreDailies Daily = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Elden Ruby", "Compass Rose Skull" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("shadowrealm", 2486, findIngredients, buyOnlyThis, buyMode: buyMode);

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
                    bool shouldStop = !Adv.matsOnly || !dontStopMissingIng;
                    Core.Logger($"The bot hasn't been taught how to get {req.Name}." + (shouldStop ? " Please report the issue." : " Skipping"), messageBox: shouldStop, stopBot: shouldStop);
                    break;
                #endregion

                case "Elden Ruby":
                    Core.FarmingLogger(req.Name, quant);
                    Daily.EldenRuby(quant);
                    break;

                case "Compass Rose Skull":
                    Core.FarmingLogger(req.Name, quant);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonsterQuest(9894,
("dracocon", "Treasure Pile", ClassType.Farm),
                    ("battleundere", "Treasure Pile", ClassType.Farm),
                    ("greed", "Treasure Pile", ClassType.Farm)
);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("88373", "Obsessor Commander", "Mode: [select] only\nShould the bot buy \"Obsessor Commander\" ?", false),
        new Option<bool>("88375", "Obsessor Captain Hat", "Mode: [select] only\nShould the bot buy \"Obsessor Captain Hat\" ?", false),
        new Option<bool>("88376", "Obsessor Captain Cap", "Mode: [select] only\nShould the bot buy \"Obsessor Captain Cap\" ?", false),
        new Option<bool>("88377", "Obsessor Captain Skull Hat", "Mode: [select] only\nShould the bot buy \"Obsessor Captain Skull Hat\" ?", false),
        new Option<bool>("88379", "Obsessor's Shadow", "Mode: [select] only\nShould the bot buy \"Obsessor's Shadow\" ?", false),
        new Option<bool>("88380", "Compass Rose Skull Pet", "Mode: [select] only\nShould the bot buy \"Compass Rose Skull Pet\" ?", false),
        new Option<bool>("88381", "Obsessor Cutlass", "Mode: [select] only\nShould the bot buy \"Obsessor Cutlass\" ?", false),
        new Option<bool>("88382", "Obsessor Cutlasses", "Mode: [select] only\nShould the bot buy \"Obsessor Cutlasses\" ?", false),
        new Option<bool>("88384", "Compass Rose Skulls", "Mode: [select] only\nShould the bot buy \"Compass Rose Skulls\" ?", false),
    };
}
