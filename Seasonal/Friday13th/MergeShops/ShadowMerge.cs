/*
name: Shadow Merge
description: This bot will farm the items belonging to the selected mode for the Shadow Merge [2347] in /shadowbattleon
tags: shadow, merge, shadowbattleon, fabled, doomknight, doom, forbidden, condemned, uprising, doomed, fate, reaper, lance, reborn, spear, enchanted
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ShadowMerge
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
        Core.BankingBlackList.AddRange(new[] { "A Whisper" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("shadowbattleon", 2347, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "A Whisper":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(9421, 9422);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.KillMonster("shadowbattleon", "r7", "Left", "Doomed Troll", "Shadow Hunt Medal", 5, log: false);
                        Core.KillMonster("shadowbattleon", "r7", "Left", "Doomed Troll", "Mega Shadow Hunt Medal", 3, log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("73057", "Fabled Doomknight", "Mode: [select] only\nShould the bot buy \"Fabled Doomknight\" ?", false),
        new Option<bool>("73058", "Fabled Doomknight Helm", "Mode: [select] only\nShould the bot buy \"Fabled Doomknight Helm\" ?", false),
        new Option<bool>("73059", "Fabled Doom Blade", "Mode: [select] only\nShould the bot buy \"Fabled Doom Blade\" ?", false),
        new Option<bool>("80335", "Blade of Forbidden Doom", "Mode: [select] only\nShould the bot buy \"Blade of Forbidden Doom\" ?", false),
        new Option<bool>("80336", "Blades of Forbidden Doom", "Mode: [select] only\nShould the bot buy \"Blades of Forbidden Doom\" ?", false),
        new Option<bool>("80337", "Condemned Shadow Sword", "Mode: [select] only\nShould the bot buy \"Condemned Shadow Sword\" ?", false),
        new Option<bool>("80338", "Condemned Shadow Swords", "Mode: [select] only\nShould the bot buy \"Condemned Shadow Swords\" ?", false),
        new Option<bool>("80339", "Staff of Uprising Doom", "Mode: [select] only\nShould the bot buy \"Staff of Uprising Doom\" ?", false),
        new Option<bool>("80340", "Doomed Fate Reaper", "Mode: [select] only\nShould the bot buy \"Doomed Fate Reaper\" ?", false),
        new Option<bool>("80341", "Lance of Doom Reborn", "Mode: [select] only\nShould the bot buy \"Lance of Doom Reborn\" ?", false),
        new Option<bool>("80342", "Spear of Doom Reborn", "Mode: [select] only\nShould the bot buy \"Spear of Doom Reborn\" ?", false),
        new Option<bool>("80343", "Enchanted Blade of Forbidden Doom", "Mode: [select] only\nShould the bot buy \"Enchanted Blade of Forbidden Doom\" ?", false),
        new Option<bool>("80344", "Enchanted Blades of Forbidden Doom", "Mode: [select] only\nShould the bot buy \"Enchanted Blades of Forbidden Doom\" ?", false),
        new Option<bool>("80345", "Enchanted Condemned Shadow Sword", "Mode: [select] only\nShould the bot buy \"Enchanted Condemned Shadow Sword\" ?", false),
        new Option<bool>("80346", "Enchanted Condemned Shadow Swords", "Mode: [select] only\nShould the bot buy \"Enchanted Condemned Shadow Swords\" ?", false),
        new Option<bool>("80347", "Enchanted Staff of Uprising Doom", "Mode: [select] only\nShould the bot buy \"Enchanted Staff of Uprising Doom\" ?", false),
        new Option<bool>("80348", "Enchanted Doomed Fate Reaper", "Mode: [select] only\nShould the bot buy \"Enchanted Doomed Fate Reaper\" ?", false),
        new Option<bool>("80349", "Enchanted Lance of Doom Reborn", "Mode: [select] only\nShould the bot buy \"Enchanted Lance of Doom Reborn\" ?", false),
        new Option<bool>("80350", "Enchanted Spear of Doom Reborn", "Mode: [select] only\nShould the bot buy \"Enchanted Spear of Doom Reborn\" ?", false),
    };
}
