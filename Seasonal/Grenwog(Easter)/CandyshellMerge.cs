/*
name: CandyShellMerge
description: Farms materials and items from the candyshell mergeshop
tags: merge, grenwog
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class CandyshellMerge
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
        Core.BankingBlackList.AddRange(new[] { "Chocolate Eggshells", "Creme Eggshells", "Caramel Eggshells", "Rainbow Eggshells", "Shadow Eggshells", "Chaotic Eggshells", "Golden Eggshells", "Anti-Neggshells" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        if (!Core.isSeasonalMapActive("grenwog"))
            return;

        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("grenwog", 1873, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Chocolate Eggshells":
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("GreenguardEast", "Gurushroom", req.Name, quant, isTemp: false);
                    break;

                case "Creme Eggshells":
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("GreenShell", "Tsukumogami", req.Name, quant, isTemp: false);
                    break;

                case "Caramel Eggshells":
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("GreenguardWest", "Kittarian", req.Name, quant, isTemp: false);
                    break;

                case "Rainbow Eggshells":
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("Greendragon", "Greenguard Dragon", req.Name, quant, isTemp: false);
                    break;

                case "Shadow Eggshells":
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("Grenwog", "Grenwog", req.Name, quant, isTemp: false);
                    break;

                case "Chaotic Eggshells":
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("Grenstory", "Imposter Egg", req.Name, quant, isTemp: false);
                    break;

                case "Golden Eggshells":
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("Greed", "Treasure Pile", req.Name, quant, isTemp: false);
                    break;

                case "Anti-Neggshells":
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("Greymoor", "Spooky Treeant", req.Name, quant, isTemp: false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("54254", "Chocolate Egg", "Mode: [select] only\nShould the bot buy \"Chocolate Egg\" ?", false),
        new Option<bool>("54252", "Creme Egg", "Mode: [select] only\nShould the bot buy \"Creme Egg\" ?", false),
        new Option<bool>("54253", "Caramel Egg", "Mode: [select] only\nShould the bot buy \"Caramel Egg\" ?", false),
        new Option<bool>("54251", "Rainbow Egg", "Mode: [select] only\nShould the bot buy \"Rainbow Egg\" ?", false),
        new Option<bool>("54255", "Shadow Egg", "Mode: [select] only\nShould the bot buy \"Shadow Egg\" ?", false),
        new Option<bool>("54257", "Chaos Egg", "Mode: [select] only\nShould the bot buy \"Chaos Egg\" ?", false),
        new Option<bool>("54256", "Golden Egg", "Mode: [select] only\nShould the bot buy \"Golden Egg\" ?", false),
        new Option<bool>("54258", "Anti-Negg", "Mode: [select] only\nShould the bot buy \"Anti-Negg\" ?", false),
        new Option<bool>("54227", "Evil Bunny Suit", "Mode: [select] only\nShould the bot buy \"Evil Bunny Suit\" ?", false),
        new Option<bool>("54228", "Evil Bunny Ears", "Mode: [select] only\nShould the bot buy \"Evil Bunny Ears\" ?", false),
        new Option<bool>("54229", "Evil Bunny Pet", "Mode: [select] only\nShould the bot buy \"Evil Bunny Pet\" ?", false),
        new Option<bool>("54224", "White Rabbit Hair", "Mode: [select] only\nShould the bot buy \"White Rabbit Hair\" ?", false),
        new Option<bool>("54225", "White Rabbit Hat", "Mode: [select] only\nShould the bot buy \"White Rabbit Hat\" ?", false),
        new Option<bool>("54226", "White Rabbit Smile", "Mode: [select] only\nShould the bot buy \"White Rabbit Smile\" ?", false),
        new Option<bool>("54085", "Grenwog Worshipper", "Mode: [select] only\nShould the bot buy \"Grenwog Worshipper\" ?", false),
        new Option<bool>("54088", "Grenwog Worshipper Hoodie + Mask", "Mode: [select] only\nShould the bot buy \"Grenwog Worshipper Hoodie + Mask\" ?", false),
        new Option<bool>("54086", "Grenwog Worshipper Helmet", "Mode: [select] only\nShould the bot buy \"Grenwog Worshipper Helmet\" ?", false),
        new Option<bool>("54087", "Grenwog Worshipper Hoodie", "Mode: [select] only\nShould the bot buy \"Grenwog Worshipper Hoodie\" ?", false),
        new Option<bool>("54094", "Grenwog Worshipper Greatsword", "Mode: [select] only\nShould the bot buy \"Grenwog Worshipper Greatsword\" ?", false),
        new Option<bool>("54397", "White Rabbit Suit", "Mode: [select] only\nShould the bot buy \"White Rabbit Suit\" ?", false),
        new Option<bool>("60145", "EggHunter Berserker", "Mode: [select] only\nShould the bot buy \"EggHunter Berserker\" ?", false),
        new Option<bool>("60146", "EggHunter Berserker's Helm", "Mode: [select] only\nShould the bot buy \"EggHunter Berserker's Helm\" ?", false),
        new Option<bool>("60147", "EggHunter Berserker's Rune Cape", "Mode: [select] only\nShould the bot buy \"EggHunter Berserker's Rune Cape\" ?", false),
        new Option<bool>("60148", "EggHunter Berserker's Cape", "Mode: [select] only\nShould the bot buy \"EggHunter Berserker's Cape\" ?", false),
        new Option<bool>("60149", "EggHunter Berserker's Polearm", "Mode: [select] only\nShould the bot buy \"EggHunter Berserker's Polearm\" ?", false),
        new Option<bool>("60150", "EggHunter Berserker's Drone Pet", "Mode: [select] only\nShould the bot buy \"EggHunter Berserker's Drone Pet\" ?", false),
    };
}
