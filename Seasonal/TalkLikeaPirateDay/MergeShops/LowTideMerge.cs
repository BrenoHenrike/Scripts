/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Seasonal/TalkLikeaPirateDay/LowTideStory.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class LowTideMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();
    public LowTideStory LT = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Evidence Tag", "Dark Sea Corsair", "Dark Sea Corsair's Mask", "Dark Sea Corsair's Mask + Locks", "Dark Sea Corsair's Hat", "Dark Sea Corsair's Hat + Locks", "Dark Sea Corsair's Battle Mask", "Dark Sea Corsair's Battle Mask + Locks", "Enchanted Corsair's Rapier", "Enchanted Corsair's Pistol " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        LT.Storyline();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("lowtide", 2166, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Evidence Tag":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(8846);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("lowtide", "Exiled General Miel", "Gem Encrusted Medal", 3);
                        Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("lowtide", "Spectral Jellyfish", "Spindley Tentacles", 30);
                        Core.HuntMonster("lowtide", "Ghostly Eel", "Eel Fangs", 30);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Dark Sea Corsair":
                case "Dark Sea Corsair's Mask":
                case "Dark Sea Corsair's Mask + Locks":
                case "Dark Sea Corsair's Hat":
                case "Dark Sea Corsair's Hat + Locks":
                case "Dark Sea Corsair's Battle Mask":
                case "Dark Sea Corsair's Battle Mask + Locks":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("lowtide", "Exiled General Miel", req.Name, 1, false);
                    break;

                case "Enchanted Corsair's Rapier":
                case "Enchanted Corsair's Pistol":
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("lowtide", "Spectral Jellyfish", req.Name, 1, false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("71132", "Enchanted Corsair", "Mode: [select] only\nShould the bot buy \"Enchanted Corsair\" ?", false),
        new Option<bool>("71133", "Enchanted Corsair's Mask", "Mode: [select] only\nShould the bot buy \"Enchanted Corsair's Mask\" ?", false),
        new Option<bool>("71134", "Enchanted Corsair's Mask + Locks", "Mode: [select] only\nShould the bot buy \"Enchanted Corsair's Mask + Locks\" ?", false),
        new Option<bool>("71135", "Enchanted Corsair's Hat", "Mode: [select] only\nShould the bot buy \"Enchanted Corsair's Hat\" ?", false),
        new Option<bool>("71136", "Enchanted Corsair's Hat + Locks", "Mode: [select] only\nShould the bot buy \"Enchanted Corsair's Hat + Locks\" ?", false),
        new Option<bool>("71137", "Enchanted Corsair's Battle Mask", "Mode: [select] only\nShould the bot buy \"Enchanted Corsair's Battle Mask\" ?", false),
        new Option<bool>("71138", "Enchanted Corsair's Battle Mask + Locks", "Mode: [select] only\nShould the bot buy \"Enchanted Corsair's Battle Mask + Locks\" ?", false),
        new Option<bool>("71140", "Enchanted Corsair's Rapiers", "Mode: [select] only\nShould the bot buy \"Enchanted Corsair's Rapiers\" ?", false),
        new Option<bool>("71142", "Enchanted Corsair's Pistols", "Mode: [select] only\nShould the bot buy \"Enchanted Corsair's Pistols\" ?", false),
        new Option<bool>("71143", "Enchanted Corsair's Battle Gear", "Mode: [select] only\nShould the bot buy \"Enchanted Corsair's Battle Gear\" ?", false),
        new Option<bool>("72047", "Dark Sea Fleet Captain", "Mode: [select] only\nShould the bot buy \"Dark Sea Fleet Captain\" ?", false),
        new Option<bool>("72048", "Dark Sea Captain's Hat", "Mode: [select] only\nShould the bot buy \"Dark Sea Captain's Hat\" ?", false),
        new Option<bool>("72049", "Dark Sea Captain's Hat + Locks", "Mode: [select] only\nShould the bot buy \"Dark Sea Captain's Hat + Locks\" ?", false),
        new Option<bool>("72050", "Dark Sea Captain's Hat + Beard", "Mode: [select] only\nShould the bot buy \"Dark Sea Captain's Hat + Beard\" ?", false),
        new Option<bool>("72051", "Dark Sea Captain's Ship", "Mode: [select] only\nShould the bot buy \"Dark Sea Captain's Ship\" ?", false),
        new Option<bool>("71588", "Explorer Moglin", "Mode: [select] only\nShould the bot buy \"Explorer Moglin\" ?", false),
        new Option<bool>("71592", "Looter Crew Moglin", "Mode: [select] only\nShould the bot buy \"Looter Crew Moglin\" ?", false),
        new Option<bool>("71593", "Cap'n MogBeard", "Mode: [select] only\nShould the bot buy \"Cap'n MogBeard\" ?", false),
    };
}
