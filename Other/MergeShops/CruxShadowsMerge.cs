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

public class CruxShadowsMerge
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
        Core.BankingBlackList.AddRange(new[] { "Glowing Pumpkinseed " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("cruxship", 1172, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Glowing Pumpkinseed":
                    if (Core.IsMember)
                    {
                        Core.EquipClass(ClassType.Solo);
                        Core.RegisterQuests(4617);

                        while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        {
                            //ULTRA Pumpkinseed Farming Quest 4617 [Member]
                            Core.HuntMonster("CruxShip", "Apephryx", "Otherworld Sigil", isTemp: false);
                            Bot.Wait.ForPickup(req.Name);
                        }
                        Core.CancelRegisteredQuests();
                    }
                    else
                    {
                        Core.EquipClass(ClassType.Farm);
                        Core.RegisterQuests(4615);
                        while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                        {
                            //Gather the Gold Debns 4615
                            Core.HuntMonster("CruxShip", "Treasure Hunter", "Debns Gathered", 6);
                            Bot.Wait.ForPickup(req.Name);
                        }
                        Core.CancelRegisteredQuests();
                    }
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("31848", "Evolved Pumpkin Lord", "Mode: [select] only\nShould the bot buy \"Evolved Pumpkin Lord\" ?", false),
        new Option<bool>("31905", "SteamPunk TreasureHunter", "Mode: [select] only\nShould the bot buy \"SteamPunk TreasureHunter\" ?", false),
        new Option<bool>("31906", "TreasureHunter Musket", "Mode: [select] only\nShould the bot buy \"TreasureHunter Musket\" ?", false),
        new Option<bool>("31907", "TreasureHunter Wrap", "Mode: [select] only\nShould the bot buy \"TreasureHunter Wrap\" ?", false),
        new Option<bool>("31908", "TreasureHunter Hair", "Mode: [select] only\nShould the bot buy \"TreasureHunter Hair\" ?", false),
        new Option<bool>("31909", "TreasureHunter Musket Cape", "Mode: [select] only\nShould the bot buy \"TreasureHunter Musket Cape\" ?", false),
        new Option<bool>("31981", "Monster Mummy", "Mode: [select] only\nShould the bot buy \"Monster Mummy\" ?", false),
        new Option<bool>("31982", "Monster Mummy Wrap", "Mode: [select] only\nShould the bot buy \"Monster Mummy Wrap\" ?", false),
        new Option<bool>("31929", "Darkwave Dancer", "Mode: [select] only\nShould the bot buy \"Darkwave Dancer\" ?", false),
        new Option<bool>("31924", "Live/Love Banner (Left)", "Mode: [select] only\nShould the bot buy \"Live/Love Banner (Left)\" ?", false),
        new Option<bool>("31925", "Be/Believe Banner (Left)", "Mode: [select] only\nShould the bot buy \"Be/Believe Banner (Left)\" ?", false),
        new Option<bool>("31961", "Darkwave Guitar", "Mode: [select] only\nShould the bot buy \"Darkwave Guitar\" ?", false),
        new Option<bool>("31959", "Bloodwave Violin and Bow", "Mode: [select] only\nShould the bot buy \"Bloodwave Violin and Bow\" ?", false),
        new Option<bool>("31960", "Darkwave Violin and Bow", "Mode: [select] only\nShould the bot buy \"Darkwave Violin and Bow\" ?", false),
        new Option<bool>("31958", "Darkwave Drumset", "Mode: [select] only\nShould the bot buy \"Darkwave Drumset\" ?", false),
        new Option<bool>("31957", "Darkwave Keyboards", "Mode: [select] only\nShould the bot buy \"Darkwave Keyboards\" ?", false),
    };
}
