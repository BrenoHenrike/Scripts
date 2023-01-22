/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Hollowborn/CoreHollowborn.cs
//cs_include Scripts/Nation/AFDL/WillpowerExtraction.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ArchFiendWarlordMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public CoreNation Nation = new();
    public CoreHollowborn HB = new();
    public WillpowerExtraction WPE = new();
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
        Adv.StartBuyAllMerge("tercessuinotlim", 1820, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Unidentified 36":
                    Core.EquipClass(ClassType.Farm);
                    HB.FreshSouls(1, 0);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Diamond of Nulgath":
                    Core.EquipClass(ClassType.Farm);
                    Nation.FarmDiamondofNulgath(quant);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Bone Dust":
                    Core.EquipClass(ClassType.Farm);
                    Farm.BattleUnderB("Bone Dust", quant);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Gem of Nulgath":
                    Core.EquipClass(ClassType.Farm);
                    Nation.FarmGemofNulgath(quant);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Blood Gem of the Archfiend":
                    Core.EquipClass(ClassType.Farm);
                    Nation.FarmBloodGem(quant);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Fresh Soul":
                    Core.EquipClass(ClassType.Farm);
                    HB.FreshSouls(0, quant);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Unidentified 13":
                    Core.EquipClass(ClassType.Farm);
                    Nation.FarmUni13(quant);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Unidentified 34":
                    Core.EquipClass(ClassType.Farm);
                    WPE.Unidentified34(quant);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Voucher of Nulgath (non-mem)":
                    Core.EquipClass(ClassType.Farm);
                    Nation.FarmVoucher(false);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Voucher of Nulgath":
                    Core.EquipClass(ClassType.Farm);
                    Nation.FarmVoucher(true);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Dark Crystal Shard":
                    Core.EquipClass(ClassType.Farm);
                    Nation.FarmDarkCrystalShard(quant);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Totem of Nulgath":
                    Core.EquipClass(ClassType.Farm);
                    Nation.FarmTotemofNulgath(quant);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Strand of Vath's Hair":
                    Core.EquipClass(ClassType.Farm);
                    Core.KillVath(req.Name, quant);
                    Bot.Wait.ForPickup(req.Name);
                    break;

                case "Unidentified 25":
                    Core.EquipClass(ClassType.Farm);
                    Adv.BuyItem("tercessuinotlim", 1951, "Unidentified 25");
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("52599", "Fiend Minion", "Mode: [select] only\nShould the bot buy \"Fiend Minion\" ?", false),
        new Option<bool>("52600", "Fiend Warrior", "Mode: [select] only\nShould the bot buy \"Fiend Warrior\" ?", false),
        new Option<bool>("52601", "Archfiend Warlord", "Mode: [select] only\nShould the bot buy \"Archfiend Warlord\" ?", false),
        new Option<bool>("52602", "Fiend Minion Horns", "Mode: [select] only\nShould the bot buy \"Fiend Minion Horns\" ?", false),
        new Option<bool>("52603", "Fiend Warrior Horns", "Mode: [select] only\nShould the bot buy \"Fiend Warrior Horns\" ?", false),
        new Option<bool>("52604", "Archfiend Horns", "Mode: [select] only\nShould the bot buy \"Archfiend Horns\" ?", false),
        new Option<bool>("52605", "Fiend Warrior Mask", "Mode: [select] only\nShould the bot buy \"Fiend Warrior Mask\" ?", false),
        new Option<bool>("52606", "Fiend Warrior Mask + Locks", "Mode: [select] only\nShould the bot buy \"Fiend Warrior Mask + Locks\" ?", false),
        new Option<bool>("52607", "Fiend Warrior Mask + Hair", "Mode: [select] only\nShould the bot buy \"Fiend Warrior Mask + Hair\" ?", false),
        new Option<bool>("52608", "Fiend Minion Fur", "Mode: [select] only\nShould the bot buy \"Fiend Minion Fur\" ?", false),
        new Option<bool>("52620", "ArchFiend Enchanted Orbs", "Mode: [select] only\nShould the bot buy \"ArchFiend Enchanted Orbs\" ?", false),
        new Option<bool>("52615", "Mortal Fiend Armblades", "Mode: [select] only\nShould the bot buy \"Mortal Fiend Armblades\" ?", false),
        new Option<bool>("52614", "Mortal Fiend Armblade", "Mode: [select] only\nShould the bot buy \"Mortal Fiend Armblade\" ?", false),
        new Option<bool>("52625", "Mortal Fiend Fangs Spear", "Mode: [select] only\nShould the bot buy \"Mortal Fiend Fangs Spear\" ?", false),
        new Option<bool>("52626", "Mortal Fiend Staff", "Mode: [select] only\nShould the bot buy \"Mortal Fiend Staff\" ?", false),
        new Option<bool>("52623", "Fiend Minion Staff", "Mode: [select] only\nShould the bot buy \"Fiend Minion Staff\" ?", false),
        new Option<bool>("52618", "Mortal Fiend Katana", "Mode: [select] only\nShould the bot buy \"Mortal Fiend Katana\" ?", false),
        new Option<bool>("52619", "Mortal Fiend Katanas", "Mode: [select] only\nShould the bot buy \"Mortal Fiend Katanas\" ?", false),
        new Option<bool>("52621", "Mortal Fiend Reaver", "Mode: [select] only\nShould the bot buy \"Mortal Fiend Reaver\" ?", false),
        new Option<bool>("52622", "Mortal Fiend Reavers", "Mode: [select] only\nShould the bot buy \"Mortal Fiend Reavers\" ?", false),
        new Option<bool>("52616", "Mortal Fiend Claw", "Mode: [select] only\nShould the bot buy \"Mortal Fiend Claw\" ?", false),
        new Option<bool>("52617", "Mortal Fiend Claws", "Mode: [select] only\nShould the bot buy \"Mortal Fiend Claws\" ?", false),
        new Option<bool>("52624", "ArchFiend Spear", "Mode: [select] only\nShould the bot buy \"ArchFiend Spear\" ?", false),
        new Option<bool>("52610", "Fiend Warrior Tail", "Mode: [select] only\nShould the bot buy \"Fiend Warrior Tail\" ?", false),
        new Option<bool>("52611", "Fiend Warrior Tail + Fur", "Mode: [select] only\nShould the bot buy \"Fiend Warrior Tail + Fur\" ?", false),
        new Option<bool>("52612", "Archfiend Warlord Wings", "Mode: [select] only\nShould the bot buy \"Archfiend Warlord Wings\" ?", false),
        new Option<bool>("52613", "Archfiend Warlord Wings + Tail", "Mode: [select] only\nShould the bot buy \"Archfiend Warlord Wings + Tail\" ?", false),
        new Option<bool>("52609", "Fiend Warrior Orbs", "Mode: [select] only\nShould the bot buy \"Fiend Warrior Orbs\" ?", false),
    };
}
