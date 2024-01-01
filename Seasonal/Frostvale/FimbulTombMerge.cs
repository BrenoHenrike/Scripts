/*
name: Fimbul Tomb Merge
description: This bot will farm the items belonging to the selected mode for the Fimbul Tomb Merge [2378] in /fimbultomb
tags: frostval, fimbul, tomb, merge, fimbultomb, cursed, fimbulventr, witch, vengeful, azure, blossom, resonating, melodic, ice, crystal, crystals
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal\Frostvale\Frostvale.cs
//cs_include Scripts/Story/Glacera.cs

using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class FimbulTombMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private Frostvale FV = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Fimbul's Frost", "Fimbul's Crystal" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        FV.Fimbultomb();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("fimbultomb", 2378, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Fimbul's Frost":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(9507);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("glacetomb", "Kriomein", "Valedictorian Speech", log: false);
                        Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("glacetomb", "Draugr", "Frozen Marrow", 8, log: false);
                        Core.HuntMonster("glacetomb", "Snow Fairy", "Crystalline Wings", 8, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Fimbul's Crystal":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(9519);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("fimbultomb", "Fimbulventr Witch", "Ice Crown", log: false);
                        Core.HuntMonster("fimbultomb", "Daselm", "Daselm's Thesis", log: false);
                        Core.HuntMonster("fimbultomb", "Peter", "Peter's Recc Letter", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("82387", "Cursed Fimbulventr Witch", "Mode: [select] only\nShould the bot buy \"Cursed Fimbulventr Witch\" ?", false),
        new Option<bool>("82388", "Fimbulventr Witch", "Mode: [select] only\nShould the bot buy \"Fimbulventr Witch\" ?", false),
        new Option<bool>("82389", "Fimbulventr Witch Visage", "Mode: [select] only\nShould the bot buy \"Fimbulventr Witch Visage\" ?", false),
        new Option<bool>("82390", "Fimbulventr Witch Locks", "Mode: [select] only\nShould the bot buy \"Fimbulventr Witch Locks\" ?", false),
        new Option<bool>("82391", "Fimbulventr Witch Hair", "Mode: [select] only\nShould the bot buy \"Fimbulventr Witch Hair\" ?", false),
        new Option<bool>("82392", "Vengeful Azure Blossom Staff", "Mode: [select] only\nShould the bot buy \"Vengeful Azure Blossom Staff\" ?", false),
        new Option<bool>("82393", "Resonating Azure Blossom Staff", "Mode: [select] only\nShould the bot buy \"Resonating Azure Blossom Staff\" ?", false),
        new Option<bool>("82394", "Melodic Ice Crystal", "Mode: [select] only\nShould the bot buy \"Melodic Ice Crystal\" ?", false),
        new Option<bool>("82395", "Melodic Ice Crystals", "Mode: [select] only\nShould the bot buy \"Melodic Ice Crystals\" ?", false),
    };
}
