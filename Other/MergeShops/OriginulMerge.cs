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
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class OriginulMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();

    public CoreNation Nation = new();

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
        Adv.StartBuyAllMerge("originul", 1962, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Fiend Emblem":
                    Core.FarmingLogger($"{req.Name}", quant);
                    Core.RegisterQuests(7890);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EquipClass(ClassType.Farm);
                        Core.KillMonster("originul", "r5", "Right", "*", "Essence of The Citadel", 30);

                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("originul", "Fiend Champion", "Champion's Essence");

                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Unidentified 13":
                    Nation.FarmUni13(quant);
                    break;

                case "Diamond of Nulgath":
                    Nation.FarmDiamondofNulgath(quant);
                    break;

                case "Dark Crystal Shard":
                    Nation.FarmDarkCrystalShard(quant);
                    break;

                case "Totem of Nulgath":
                    Nation.FarmTotemofNulgath(quant);
                    break;

                case "Tainted Gem":
                    Nation.SwindleBulk(quant);
                    break;

                case "Essence of Nulgath":
                    Nation.EssenceofNulgath(quant);
                    break;

                case "Blood Gem of the Archfiend":
                    Nation.FarmBloodGem(quant);
                    break;

                case "Voucher of Nulgath (non-mem)":
                    Nation.FarmVoucher(false);
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("58813", "Fiend Champion of Adimonde", "Mode: [select] only\nShould the bot buy \"Fiend Champion of Adimonde\" ?", false),
        new Option<bool>("58814", "Fiend Champion Horns", "Mode: [select] only\nShould the bot buy \"Fiend Champion Horns\" ?", false),
        new Option<bool>("58815", "Fiend Champion's Morph", "Mode: [select] only\nShould the bot buy \"Fiend Champion's Morph\" ?", false),
        new Option<bool>("58816", "Fiend Champion's Cloak", "Mode: [select] only\nShould the bot buy \"Fiend Champion's Cloak\" ?", false),
        new Option<bool>("58817", "Fiend Champion's Spikes", "Mode: [select] only\nShould the bot buy \"Fiend Champion's Spikes\" ?", false),
        new Option<bool>("58818", "Fiend Champion's Cloak + Spikes", "Mode: [select] only\nShould the bot buy \"Fiend Champion's Cloak + Spikes\" ?", false),
        new Option<bool>("58819", "Fiend Champion's Spear", "Mode: [select] only\nShould the bot buy \"Fiend Champion's Spear\" ?", false),
        new Option<bool>("58820", "Fiend Champion's Blade", "Mode: [select] only\nShould the bot buy \"Fiend Champion's Blade\" ?", false),
        new Option<bool>("58732", "Arcane Warfiend of Nulgath", "Mode: [select] only\nShould the bot buy \"Arcane Warfiend of Nulgath\" ?", false),
        new Option<bool>("58733", "Warfiend's Hood", "Mode: [select] only\nShould the bot buy \"Warfiend's Hood\" ?", false),
        new Option<bool>("58734", "Warfiend's Masked Hood", "Mode: [select] only\nShould the bot buy \"Warfiend's Masked Hood\" ?", false),
        new Option<bool>("58735", "Warfiend's Cape", "Mode: [select] only\nShould the bot buy \"Warfiend's Cape\" ?", false),
        new Option<bool>("58736", "Warfiend's Backblade", "Mode: [select] only\nShould the bot buy \"Warfiend's Backblade\" ?", false),
        new Option<bool>("58737", "Warfiend's Dual Backblades", "Mode: [select] only\nShould the bot buy \"Warfiend's Dual Backblades\" ?", false),
        new Option<bool>("58738", "Warfiend's Backblades + Cape", "Mode: [select] only\nShould the bot buy \"Warfiend's Backblades + Cape\" ?", false),
        new Option<bool>("58739", "Gift of The ArchFiend Staff", "Mode: [select] only\nShould the bot buy \"Gift of The ArchFiend Staff\" ?", false),
        new Option<bool>("58770", "Evolved Worshipper of Nulgath", "Mode: [select] only\nShould the bot buy \"Evolved Worshipper of Nulgath\" ?", false),
        new Option<bool>("58771", "Evolved Worshipper's Morph", "Mode: [select] only\nShould the bot buy \"Evolved Worshipper's Morph\" ?", false),
        new Option<bool>("58772", "Evolved Worshipper's Visage", "Mode: [select] only\nShould the bot buy \"Evolved Worshipper's Visage\" ?", false),
        new Option<bool>("58773", "Worshipper Hood of Nulgath", "Mode: [select] only\nShould the bot buy \"Worshipper Hood of Nulgath\" ?", false),
        new Option<bool>("58774", "Worshipper Key of Nulgath", "Mode: [select] only\nShould the bot buy \"Worshipper Key of Nulgath\" ?", false),
        new Option<bool>("58775", "Dual Worshipper Keys of Nulgath", "Mode: [select] only\nShould the bot buy \"Dual Worshipper Keys of Nulgath\" ?", false),
    };
}
