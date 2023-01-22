/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class SkullbreakerKnightMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
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
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("stonewood", 2071, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                // Add how to get items here
                case "Especially Unbroken Skull":
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(!Core.IsMember ? 8411 : 8412);
                    Core.Logger($"Farming {req.Name} ({currentQuant}/{quant})");
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("warundead", "r3", "Left", "*", "Unbroken Skulls", 100);
                        Core.HuntMonster("warundead", "Summon Lich", "Summon Lich's Orb");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("65444", "Skullbreaker Knight", "Mode: [select] only\nShould the bot buy \"Skullbreaker Knight\" ?", false),
        new Option<bool>("65445", "Skullbreaker Knight's Hood", "Mode: [select] only\nShould the bot buy \"Skullbreaker Knight's Hood\" ?", false),
        new Option<bool>("65446", "Skullbreaker's Shadow Hood", "Mode: [select] only\nShould the bot buy \"Skullbreaker's Shadow Hood\" ?", false),
        new Option<bool>("65447", "Skullbreaker's Hooded Skull", "Mode: [select] only\nShould the bot buy \"Skullbreaker's Hooded Skull\" ?", false),
        new Option<bool>("65448", "Skullbreaker's Spikes", "Mode: [select] only\nShould the bot buy \"Skullbreaker's Spikes\" ?", false),
        new Option<bool>("65449", "Skullbreaker's Cloak", "Mode: [select] only\nShould the bot buy \"Skullbreaker's Cloak\" ?", false),
        new Option<bool>("65450", "Skullbreaker's Spiked Cloak", "Mode: [select] only\nShould the bot buy \"Skullbreaker's Spiked Cloak\" ?", false),
        new Option<bool>("65451", "Skullbreaker's Blade", "Mode: [select] only\nShould the bot buy \"Skullbreaker's Blade\" ?", false),
        new Option<bool>("65452", "Skullbreaker's Spear", "Mode: [select] only\nShould the bot buy \"Skullbreaker's Spear\" ?", false),
        new Option<bool>("65453", "Skullbreaker's Reaver", "Mode: [select] only\nShould the bot buy \"Skullbreaker's Reaver\" ?", false),
        new Option<bool>("65454", "Dual Skullbreaker's Reaver", "Mode: [select] only\nShould the bot buy \"Dual Skullbreaker's Reaver\" ?", false),
        new Option<bool>("65455", "Skullbreaker's Dagger", "Mode: [select] only\nShould the bot buy \"Skullbreaker's Dagger\" ?", false),
        new Option<bool>("65456", "Skullbreaker's Blade + Axe", "Mode: [select] only\nShould the bot buy \"Skullbreaker's Blade + Axe\" ?", false),
        new Option<bool>("65457", "Skullbreaker's Axe", "Mode: [select] only\nShould the bot buy \"Skullbreaker's Axe\" ?", false),
    };
}
