/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class NulgathWarMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private CoreNation Nation = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Archfiend's Favor", "Nulgath's Approval", "Diamond of Nulgath", "Unidentified 13", "Totem of Nulgath", "Tainted Gem", "Primal Dread Fang", "Random Weapon of Nulgath", "Blood Gem of the Archfiend", "Voucher of Nulgath (non-mem)", "Gem of Nulgath", "Dark Crystal Shard", "Unidentified 27", "Bone Dust" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("evilwarnul", 452, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Archfiend's Favor":
                    Nation.ApprovalAndFavor(0, quant);
                    break;

                case "Nulgath's Approval":
                    Nation.ApprovalAndFavor(quant, 0);
                    break;

                case "Diamond of Nulgath":
                    Nation.FarmDiamondofNulgath(quant);
                    break;

                case "Unidentified 13":
                    Nation.FarmUni13(quant);
                    break;

                case "Totem of Nulgath":
                    Nation.FarmTotemofNulgath(quant);
                    break;

                case "Tainted Gem":
                    Nation.SwindleBulk(quant);
                    break;

                case "Random Weapon of Nulgath":
                case "Primal Dread Fang":
                    Nation.Supplies(req.Name, quant);
                    break;

                case "Blood Gem of the Archfiend":
                    Nation.FarmBloodGem(quant);
                    break;

                case "Voucher of Nulgath (non-mem)":
                    Nation.FarmVoucher(false);
                    break;

                case "Gem of Nulgath":
                    Nation.FarmGemofNulgath(quant);
                    break;

                case "Dark Crystal Shard":
                    Nation.FarmDarkCrystalShard(quant);
                    break;

                case "Unidentified 27":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(584);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Nation.Supplies("Unidentified 26");
                        Nation.SwindleBulk(5);
                        Core.HuntMonster("evilmarsh", "Dark Makai", "Dark Makai Sigil");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Bone Dust":
                    Farm.BattleUnderB(quant: quant);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("13008", "Redemption Helm", "Mode: [select] only\nShould the bot buy \"Redemption Helm\" ?", false),
        new Option<bool>("13006", "Barbarian Helm of Dilligaf", "Mode: [select] only\nShould the bot buy \"Barbarian Helm of Dilligaf\" ?", false),
        new Option<bool>("13013", "DeathFiend Cloak", "Mode: [select] only\nShould the bot buy \"DeathFiend Cloak\" ?", false),
        new Option<bool>("13028", "Giant Treetunk of Klunk", "Mode: [select] only\nShould the bot buy \"Giant Treetunk of Klunk\" ?", false),
        new Option<bool>("13026", "Redemption Blade of Nulgath", "Mode: [select] only\nShould the bot buy \"Redemption Blade of Nulgath\" ?", false),
        new Option<bool>("13167", "Mortality Cape of Revontheus", "Mode: [select] only\nShould the bot buy \"Mortality Cape of Revontheus\" ?", false),
        new Option<bool>("13206", "Sage Hood of Revontheus", "Mode: [select] only\nShould the bot buy \"Sage Hood of Revontheus\" ?", false),
        new Option<bool>("13153", "NeoFiend Helm of Revontheus", "Mode: [select] only\nShould the bot buy \"NeoFiend Helm of Revontheus\" ?", false),
        new Option<bool>("13370", "Oblivion of Revontheus", "Mode: [select] only\nShould the bot buy \"Oblivion of Revontheus\" ?", false),
        new Option<bool>("13372", "Oblivion Helm of Revontheus", "Mode: [select] only\nShould the bot buy \"Oblivion Helm of Revontheus\" ?", false),
        new Option<bool>("13373", "Oblivion Wings of Revontheus", "Mode: [select] only\nShould the bot buy \"Oblivion Wings of Revontheus\" ?", false),
        new Option<bool>("13377", "Genesis Wings of Revontheus", "Mode: [select] only\nShould the bot buy \"Genesis Wings of Revontheus\" ?", false),
        new Option<bool>("13371", "Oblivion Spear of Revontheus", "Mode: [select] only\nShould the bot buy \"Oblivion Spear of Revontheus\" ?", false),
        new Option<bool>("13369", "Mini Dilligaf", "Mode: [select] only\nShould the bot buy \"Mini Dilligaf\" ?", false),
        new Option<bool>("27849", "Golden Hanzo Void Katana", "Mode: [select] only\nShould the bot buy \"Golden Hanzo Void Katana\" ?", false),
        new Option<bool>("27850", "Dual Golden Hanzo Void Katanas", "Mode: [select] only\nShould the bot buy \"Dual Golden Hanzo Void Katanas\" ?", false),
        new Option<bool>("27851", "Golden Hanzo Void Mask", "Mode: [select] only\nShould the bot buy \"Golden Hanzo Void Mask\" ?", false),
        new Option<bool>("27852", "Golden Hanzo Void Horns", "Mode: [select] only\nShould the bot buy \"Golden Hanzo Void Horns\" ?", false),
        new Option<bool>("28031", "Golden Hanzo Katana Cape", "Mode: [select] only\nShould the bot buy \"Golden Hanzo Katana Cape\" ?", false),
        new Option<bool>("27848", "Golden Hanzo Void Cape", "Mode: [select] only\nShould the bot buy \"Golden Hanzo Void Cape\" ?", false),
        new Option<bool>("42716", "Nulgath's Bloodsucker Larvae", "Mode: [select] only\nShould the bot buy \"Nulgath's Bloodsucker Larvae\" ?", false),
        new Option<bool>("42717", "Bloodsucker of Nulgath", "Mode: [select] only\nShould the bot buy \"Bloodsucker of Nulgath\" ?", false),
    };
}
