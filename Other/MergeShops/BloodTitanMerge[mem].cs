/*
name: Blood Titan Merge [Mem]
description: This bot will farm the items belonging to the selected mode for the Blood Titan Merge [617] in /classhalla
tags: blood, titan, merge, classhalla, horns, titans, gilded, crusher
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class BloodTitanMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Blood Titan Token" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("classhallc", 617, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Blood Titan Token":
                    Core.FarmingLogger(req.Name, quant);
                    Core.AddDrop(Core.EnsureLoad(9253).Requirements.Select(item => item.Name).Concat(Core.EnsureLoad(2908).Requirements.Select(item => item.Name)).ToArray());
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(9253);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        if (!Core.CheckInventory("Blood Titan's Tribute"))
                        {
                            Core.EnsureAccept(2908);
                            Core.HuntMonster("bloodtitan", "Blood Titan", "Blood Titan's Phial", 1, false, false);
                            Core.HuntMonster("titandrakath", "Titan Drakath", "Titanic Drakath's Blood", 1, false, false);
                            Core.HuntMonster("desoloth", "Desoloth", "Desoloth's Blood", 1, false, false);
                            Core.HuntMonster("ultracarnax", "Ultra-Carnax", "Ultra Carnax's Blood", 1, false, false);
                            Core.EnsureComplete(2908);
                            Bot.Wait.ForPickup("Blood Titan's Tribute");
                        }
                        Core.HuntMonster("bloodtitan", "Ultra Blood Titan", "Ultra Blood Titan Defeated", 1, false, false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("16641", "Blood Titan", "Mode: [select] only\nShould the bot buy \"Blood Titan\" ?", false),
        new Option<bool>("16650", "Blood Titan Horns", "Mode: [select] only\nShould the bot buy \"Blood Titan Horns\" ?", false),
        new Option<bool>("17253", "Titan's Gilded Crusher", "Mode: [select] only\nShould the bot buy \"Titan's Gilded Crusher\" ?", false),
        new Option<bool>("48200", "Blood Titan Armor", "Mode: [select] only\nShould the bot buy \"Blood Titan Armor\" ?", false),
    };
}
