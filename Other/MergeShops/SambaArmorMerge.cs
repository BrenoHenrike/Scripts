/*
name: Samba Armor Merge
description: This bot will farm the items belonging to the selected mode for the Samba Armor Merge [302] in /bloodtusk
tags: samba, armor, merge, bloodtusk, dancin, green, dance, feathers, purple
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class SambaArmorMerge
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
    //If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Samba Hair", "Samba Outfit!", "Green Dancin' Feathers Merge", "Purple Dancin' Feathers Merge"});
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("bloodtusk", 302, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Green Dancin' Feathers Merge":
                case "Samba Hair":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(1180);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("bloodtusk", "Horc Boar Scout", "Boar Bristles", 5);
                        Core.HuntMonster("bloodtusk", "Horc Boar Scout", "Boar Bone");
                        Core.HuntMonster("bloodtusk", "Jungle Vulture", "Dyed Feathers", 10);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Purple Dancin' Feathers Merge":
                case "Samba Outfit!":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(1181);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("bloodtusk", "Horc Boar Scout", "Bolt of Cloth", 3);
                        Core.HuntMonster("bloodtusk", "Rhison", "Shiny Metal", 3);
                        Core.GetMapItem(46408, 10, "bloodtusk");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("8831", "Dancin' Armor", "Mode: [select] only\nShould the bot buy \"Dancin' Armor\" ?", false),
        new Option<bool>("8744", "Green Dance Feathers", "Mode: [select] only\nShould the bot buy \"Green Dance Feathers\" ?", false),
        new Option<bool>("8745", "Purple Dance Feathers", "Mode: [select] only\nShould the bot buy \"Purple Dance Feathers\" ?", false),
    };
}
