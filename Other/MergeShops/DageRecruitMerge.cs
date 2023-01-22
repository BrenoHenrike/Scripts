/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Story/Legion/DarkWarLegionandNation.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs

using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class DageRecruitMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public CoreLegion Legion = new CoreLegion();
    public DarkWarLegionandNation DWLaN = new();
    public static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Dark Victory Seal", "Legion Token", "Underworld Asgardian Helm", "Underworld Asgardian Cape", "Underworld Asgardian Sword", "Underworld Asgardian Mace", "Underworld DeathSpine", "Underworld Oni's Blade", "Underworld Oni's Blades", "Underworld Oni's Naginata " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge(string buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        DWLaN.DarkWarLegion();
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("dagerecruit", 2119, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Dark Victory Seal":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(8576);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Story.KillQuest(8576, "dagerecruit", new[] { "Dark Makai", "Dreadfiend", "Bloodfiend", "Infernal Fiend" });
                        Core.HuntMonster("dagerecruit", "Dark Makai", "Dark Makai Defeated", 6);
                        Core.HuntMonster("dagerecruit", "Dreadfiend", "Dreadfiend Defeated", 6);
                        Core.HuntMonster("dagerecruit", "Bloodfiend", "Bloodfiend Defeated", 6);
                        Core.HuntMonster("dagerecruit", "Infernal Fiend", "Infernal Fiend Defeated", 6);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Legion Token":
                    Core.FarmingLogger(req.Name, quant);
                    Legion.FarmLegionToken(quant);
                    break;

                case "Underworld Asgardian Helm":
                case "Underworld Asgardian Cape":
                case "Underworld Asgardian Sword":
                case "Underworld DeathSpine":
                case "Underworld Asgardian Mace":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("dagerecruit", "Hebimaru", req.Name, isTemp: false);
                    Bot.Wait.ForPickup(req.Name);
                    break;


                case "Underworld Oni's Naginata":
                case "Underworld Oni's Blade":
                case "Underworld Oni's Blades":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("dagerecruit", "Nuckelavee", req.Name, isTemp: false);
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("68275", "Legion DeathFiend", "Mode: [select] only\nShould the bot buy \"Legion DeathFiend\" ?", false),
        new Option<bool>("68276", "Legion DeathFiend's Hood", "Mode: [select] only\nShould the bot buy \"Legion DeathFiend's Hood\" ?", false),
        new Option<bool>("68277", "Legion DeathFiend's Gaze", "Mode: [select] only\nShould the bot buy \"Legion DeathFiend's Gaze\" ?", false),
        new Option<bool>("68278", "Legion DeathFiend's Skull", "Mode: [select] only\nShould the bot buy \"Legion DeathFiend's Skull\" ?", false),
        new Option<bool>("68279", "Legion DeathFiend's Mask", "Mode: [select] only\nShould the bot buy \"Legion DeathFiend's Mask\" ?", false),
        new Option<bool>("68280", "Legion DeathFiend's Horns", "Mode: [select] only\nShould the bot buy \"Legion DeathFiend's Horns\" ?", false),
        new Option<bool>("68281", "Legion DeathFiend's Rune", "Mode: [select] only\nShould the bot buy \"Legion DeathFiend's Rune\" ?", false),
        new Option<bool>("68282", "Legion DeathFiend's Cape", "Mode: [select] only\nShould the bot buy \"Legion DeathFiend's Cape\" ?", false),
        new Option<bool>("68283", "Legion DeathFiend's Spear", "Mode: [select] only\nShould the bot buy \"Legion DeathFiend's Spear\" ?", false),
        new Option<bool>("68284", "Legion DeathFiend's Scythe", "Mode: [select] only\nShould the bot buy \"Legion DeathFiend's Scythe\" ?", false),
        new Option<bool>("68285", "Legion DeathFiend's Morningstar", "Mode: [select] only\nShould the bot buy \"Legion DeathFiend's Morningstar\" ?", false),
        new Option<bool>("68286", "Legion DeathFiend's Morningstars", "Mode: [select] only\nShould the bot buy \"Legion DeathFiend's Morningstars\" ?", false),
        new Option<bool>("68322", "Legion Conqueror", "Mode: [select] only\nShould the bot buy \"Legion Conqueror\" ?", false),
        new Option<bool>("68323", "Legion Conqueror Horns", "Mode: [select] only\nShould the bot buy \"Legion Conqueror Horns\" ?", false),
        new Option<bool>("68324", "Legion Conqueror Helm", "Mode: [select] only\nShould the bot buy \"Legion Conqueror Helm\" ?", false),
        new Option<bool>("68325", "Legion Conqueror Cape", "Mode: [select] only\nShould the bot buy \"Legion Conqueror Cape\" ?", false),
        new Option<bool>("68326", "Legion Conqueror Cape + Sword", "Mode: [select] only\nShould the bot buy \"Legion Conqueror Cape + Sword\" ?", false),
        new Option<bool>("68327", "Legion Conqueror Sword", "Mode: [select] only\nShould the bot buy \"Legion Conqueror Sword\" ?", false),
        new Option<bool>("68328", "Legion Conqueror Swords", "Mode: [select] only\nShould the bot buy \"Legion Conqueror Swords\" ?", false),
        new Option<bool>("68329", "Legion Conqueror Mace", "Mode: [select] only\nShould the bot buy \"Legion Conqueror Mace\" ?", false),
        new Option<bool>("68330", "Legion Conqueror Sword + Shield", "Mode: [select] only\nShould the bot buy \"Legion Conqueror Sword + Shield\" ?", false),
        new Option<bool>("68331", "Legion Conqueror Battle Gear", "Mode: [select] only\nShould the bot buy \"Legion Conqueror Battle Gear\" ?", false),
        new Option<bool>("68332", "Legion DeathSpines", "Mode: [select] only\nShould the bot buy \"Legion DeathSpines\" ?", false),
        new Option<bool>("68333", "Legion DeathSpine", "Mode: [select] only\nShould the bot buy \"Legion DeathSpine\" ?", false),
        new Option<bool>("68789", "Legion Dread Knight's Burning Skull", "Mode: [select] only\nShould the bot buy \"Legion Dread Knight's Burning Skull\" ?", false),
        new Option<bool>("68790", "Legion Dread Knight's Burning Crown", "Mode: [select] only\nShould the bot buy \"Legion Dread Knight's Burning Crown\" ?", false),
        new Option<bool>("68309", "Underworld Asgardian Horns", "Mode: [select] only\nShould the bot buy \"Underworld Asgardian Horns\" ?", false),
        new Option<bool>("68312", "Underworld Asgardian Cape + Sword", "Mode: [select] only\nShould the bot buy \"Underworld Asgardian Cape + Sword\" ?", false),
        new Option<bool>("68315", "Underworld Asgardian Swords", "Mode: [select] only\nShould the bot buy \"Underworld Asgardian Swords\" ?", false),
        new Option<bool>("68317", "Underworld Asgardian Battle Gear", "Mode: [select] only\nShould the bot buy \"Underworld Asgardian Battle Gear\" ?", false),
        new Option<bool>("68318", "Underworld Asgardian Sword + Shield", "Mode: [select] only\nShould the bot buy \"Underworld Asgardian Sword + Shield\" ?", false),
        new Option<bool>("68319", "Underworld DeathSpines", "Mode: [select] only\nShould the bot buy \"Underworld DeathSpines\" ?", false),
        new Option<bool>("68321", "Underworld Asgardian Gauntlets", "Mode: [select] only\nShould the bot buy \"Underworld Asgardian Gauntlets\" ?", false),
        new Option<bool>("68538", "Underworld Oni", "Mode: [select] only\nShould the bot buy \"Underworld Oni\" ?", false),
        new Option<bool>("68539", "Underworld Oni's Helm", "Mode: [select] only\nShould the bot buy \"Underworld Oni's Helm\" ?", false),
        new Option<bool>("68542", "Underworld Oni's Burning Blade", "Mode: [select] only\nShould the bot buy \"Underworld Oni's Burning Blade\" ?", false),
        new Option<bool>("68543", "Underworld Oni's Burning Blades", "Mode: [select] only\nShould the bot buy \"Underworld Oni's Burning Blades\" ?", false),
        new Option<bool>("68546", "Underworld Oni's Burning Naginata", "Mode: [select] only\nShould the bot buy \"Underworld Oni's Burning Naginata\" ?", false),
        new Option<bool>("68805", "Legion Dread Knight", "Mode: [select] only\nShould the bot buy \"Legion Dread Knight\" ?", false),
        new Option<bool>("68944", "Dread Knight's Apprentice Blade", "Mode: [select] only\nShould the bot buy \"Dread Knight's Apprentice Blade\" ?", false),
        new Option<bool>("68945", "Dread Knight's Acolyte Blade", "Mode: [select] only\nShould the bot buy \"Dread Knight's Acolyte Blade\" ?", false),
        new Option<bool>("68946", "Dread Knight's Master Blade", "Mode: [select] only\nShould the bot buy \"Dread Knight's Master Blade\" ?", false),
    };
}
