/*
name: Camlan Merge
description: This bot will farm the items belonging to the selected mode for the Camlan Merge [2349] in /camlan
tags: camlan, merge, camlan, dark, tithe, chevalier, armet, chevaliers, long, cloak, starbane, lance, fallen, star, shield, apocryphal, shadow, executioner, retribution, eons, broadsword, awakened, carnage, maw, cleaver
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Seasonal/BlackFriday/ShadowofDoom/CoreShadowofDoom.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class CamlanMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private CoreShadowofDoom CoreSoD = new();
    private static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Ouroboros Scale", "Advent Darkness Axe", "Advent Darkness Blade", "Dark Eons Broadsword", "Dark Eons Sword", "Shrouded Carnage Maw Cleaver" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        CoreSoD.DoAll(true);
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("camlan", 2349, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Ouroboros Scale":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.RegisterQuests(9443);
                    Core.Logger("Good luck with this \"ultra\"! --the maw");
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("camlan", "Sleih", "Sleih's Changeling Records");
                        Core.HuntMonster("camlan", "Bellona", "Bellona's Edict of War");
                        Core.HuntMonster("camlan", "Metamorphosis Maw", "Alchemic Snake Scale");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Advent Darkness Axe":
                case "Advent Darkness Blade":
                case "Shrouded Carnage Maw Cleaver":
                case "Dark Eons Sword":
                case "Dark Eons Broadsword":
                    Core.EquipClass(ClassType.Solo);
                    Core.FarmingLogger(req.Name, quant);
                    Core.HuntMonster("camlan", "Metamorphosis Maw", req.Name, quant, req.Temp);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("78484", "Dark Tithe Chevalier", "Mode: [select] only\nShould the bot buy \"Dark Tithe Chevalier\" ?", false),
        new Option<bool>("78485", "Dark Tithe Armet", "Mode: [select] only\nShould the bot buy \"Dark Tithe Armet\" ?", false),
        new Option<bool>("78486", "Dark Chevalier's Long Cloak", "Mode: [select] only\nShould the bot buy \"Dark Chevalier's Long Cloak\" ?", false),
        new Option<bool>("78487", "Starbane Lance", "Mode: [select] only\nShould the bot buy \"Starbane Lance\" ?", false),
        new Option<bool>("78488", "Fallen Star Shield", "Mode: [select] only\nShould the bot buy \"Fallen Star Shield\" ?", false),
        new Option<bool>("66487", "Apocryphal Shadow Executioner", "Mode: [select] only\nShould the bot buy \"Apocryphal Shadow Executioner\" ?", false),
        new Option<bool>("66488", "Apocryphal Retribution Blade", "Mode: [select] only\nShould the bot buy \"Apocryphal Retribution Blade\" ?", false),
        new Option<bool>("66489", "Apocryphal Eons Broadsword", "Mode: [select] only\nShould the bot buy \"Apocryphal Eons Broadsword\" ?", false),
        new Option<bool>("66490", "Apocryphal Eons Sword", "Mode: [select] only\nShould the bot buy \"Apocryphal Eons Sword\" ?", false),
        new Option<bool>("66492", "Awakened Carnage Maw Cleaver", "Mode: [select] only\nShould the bot buy \"Awakened Carnage Maw Cleaver\" ?", false),
    };
}
