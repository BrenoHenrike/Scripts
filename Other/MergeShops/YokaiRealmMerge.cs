/*
name: Yokai Realm Merge
description: This bot will farm the items belonging to the selected mode for the Yokai Realm Merge [2431] in /yokairealm
tags: yokai, realm, merge, yokairealm, bujin, doomknight, hairbun, oni, doomed, artillery, array, necrotic, dao, doom, usagi, battle, fan, fans
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/DragonsOfYokai/CoreDOY.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class YokaiRealmMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private CoreDOY DOY = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Doomatter", "Mikoto's Puppet String" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        DOY.YokaiRealm();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("yokairealm", 2431, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Doomatter":
                    Core.FarmingLogger(req.Name, quant);
                    Adv.BuyItem("tercessuinotlim", 1951, req.Name, quant);
                    break;

                case "Mikoto's Puppet String":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(9690);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("yokairealm", "Mikoto Kukol'nyy", "Mikoto's Red String", 3, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("83655", "Bujin DoomKnight", "Mode: [select] only\nShould the bot buy \"Bujin DoomKnight\" ?", false),
        new Option<bool>("83656", "Bujin DoomKnight Hairbun", "Mode: [select] only\nShould the bot buy \"Bujin DoomKnight Hairbun\" ?", false),
        new Option<bool>("83657", "Bujin DoomKnight Locks", "Mode: [select] only\nShould the bot buy \"Bujin DoomKnight Locks\" ?", false),
        new Option<bool>("83658", "Bujin DoomKnight Hairbun Visage", "Mode: [select] only\nShould the bot buy \"Bujin DoomKnight Hairbun Visage\" ?", false),
        new Option<bool>("83659", "Bujin DoomKnight Visage", "Mode: [select] only\nShould the bot buy \"Bujin DoomKnight Visage\" ?", false),
        new Option<bool>("83661", "Bujin DoomKnight Helm", "Mode: [select] only\nShould the bot buy \"Bujin DoomKnight Helm\" ?", false),
        new Option<bool>("83662", "Bujin DoomKnight Oni Mask", "Mode: [select] only\nShould the bot buy \"Bujin DoomKnight Oni Mask\" ?", false),
        new Option<bool>("83663", "Doomed Artillery Array", "Mode: [select] only\nShould the bot buy \"Doomed Artillery Array\" ?", false),
        new Option<bool>("83667", "Necrotic Dao of Doom", "Mode: [select] only\nShould the bot buy \"Necrotic Dao of Doom\" ?", false),
        new Option<bool>("83668", "Dual Necrotic Dao of Doom", "Mode: [select] only\nShould the bot buy \"Dual Necrotic Dao of Doom\" ?", false),
        new Option<bool>("85369", "Usagi Oni Battle Fan", "Mode: [select] only\nShould the bot buy \"Usagi Oni Battle Fan\" ?", false),
        new Option<bool>("85370", "Usagi Oni Battle Fans", "Mode: [select] only\nShould the bot buy \"Usagi Oni Battle Fans\" ?", false),
    };
}
