/*
name: NaoiseGrave Loot Merge
description: This bot will farm the items belonging to the selected mode for the NaoiseGrave Loot Merge [2453] in /naoisegrave
tags: naoisegrave, loot, merge, naoisegrave, gold, voucher, k, antistorm, warbeast, skull, war, beast, warbeasts, energy, fist, fists, tank, tanks
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story\AgeOfRuin\CoreAOR.cs
//cs_include Scripts/Story\ShadowsOfWar\CoreSoW.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class NaoiseGraveLootMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private CoreAOR AOR = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Dragonling Bone", "Volgritian's Dragon Bone" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        AOR.NaoiseGrave();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("naoisegrave", 2453, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Dragonling Bone":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("naoisegrave", "Dragonling", req.Name, quant, req.Temp, false);
                    break;

                case "Volgritian's Dragon Bone":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(9778);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("naoisegrave", "Bone Dragonling", "Dragonling Soul", log: false);
                        Core.HuntMonster("naoisegrave", "Ice Guardian", "Cryostone", log: false);
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("naoisegrave", "Volgritian", "Gold Chain", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("57304", "Gold Voucher 25k", "Mode: [select] only\nShould the bot buy \"Gold Voucher 25k\" ?", false),
        new Option<bool>("86499", "Anti-Storm Warbeast", "Mode: [select] only\nShould the bot buy \"Anti-Storm Warbeast\" ?", false),
        new Option<bool>("86501", "Anti-Storm Warbeast Skull", "Mode: [select] only\nShould the bot buy \"Anti-Storm Warbeast Skull\" ?", false),
        new Option<bool>("86500", "Anti-Storm War Beast Helm", "Mode: [select] only\nShould the bot buy \"Anti-Storm War Beast Helm\" ?", false),
        new Option<bool>("86502", "Anti-Storm Warbeast's Energy Aura", "Mode: [select] only\nShould the bot buy \"Anti-Storm Warbeast's Energy Aura\" ?", false),
        new Option<bool>("86503", "Anti-Storm Warbeast's Energy Fist", "Mode: [select] only\nShould the bot buy \"Anti-Storm Warbeast's Energy Fist\" ?", false),
        new Option<bool>("86504", "Anti-Storm Warbeast's Energy Fists", "Mode: [select] only\nShould the bot buy \"Anti-Storm Warbeast's Energy Fists\" ?", false),
        new Option<bool>("82167", "Anti-Storm War Tank", "Mode: [select] only\nShould the bot buy \"Anti-Storm War Tank\" ?", false),
        new Option<bool>("82169", "Anti-Storm War Tank Skull", "Mode: [select] only\nShould the bot buy \"Anti-Storm War Tank Skull\" ?", false),
        new Option<bool>("82168", "Anti-Storm War Tank Helm", "Mode: [select] only\nShould the bot buy \"Anti-Storm War Tank Helm\" ?", false),
        new Option<bool>("82171", "Anti-Storm War Tank's Energy Aura", "Mode: [select] only\nShould the bot buy \"Anti-Storm War Tank's Energy Aura\" ?", false),
        new Option<bool>("82172", "Anti-Storm War Tank's Energy Fist", "Mode: [select] only\nShould the bot buy \"Anti-Storm War Tank's Energy Fist\" ?", false),
        new Option<bool>("82173", "Anti-Storm War Tank's Energy Fists", "Mode: [select] only\nShould the bot buy \"Anti-Storm War Tank's Energy Fists\" ?", false),
    };
}
