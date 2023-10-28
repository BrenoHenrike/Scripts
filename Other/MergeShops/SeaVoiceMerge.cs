/*
name: SeaVoice Merge
description: This bot will farm the items belonging to the selected mode for the SeaVoice Merge [2320] in /seavoice
tags: seavoice, merge, seavoice, midnight, glaucus, sage, mystic, morph, companion, abyssal, atlanticus, trident
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class SeaVoiceMerge
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
        Core.BankingBlackList.AddRange(new[] { "Algal Bloom", "Bioluminessence", "Dark Elf Pearl", "Glaucus Mystic", "Water Elf Pearl", "Water Elf Antler", "Glaucus Companion", "Sundered Tentacle", "Calamity Atlanticus Trident" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("seavoice", 2320, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Bioluminessence":
                case "Calamity Atlanticus Trident":
                case "Glaucus Mystic":
                case "Glaucus Companion":
                    Core.FarmingLogger(req.Name, quant);
                    AttackVoiceInTheSea(req.Name, quant);
                    break;

                case "Dark Elf Pearl":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(9339);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("trenchobserve", "Lady Noelle", "Noelle's Brooch", log: false);
                        Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("trenchobserve", "Sea Spirit", "Green Sea Jelly", 2, log: false);
                        Core.HuntMonster("trenchobserve", "Necro Adipocere", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Water Elf Pearl":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(9302);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("midnightzone", "Shadow Viscera", "Fleshy Shadows", 8, log: false);
                        Core.HuntMonster("midnightzone", "Venerated Wraith", "Wraith Memento", 8, log: false);
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("midnightzone", "Sparagmos", "Memory Card", log: false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Water Elf Antler":
                    Core.FarmingLogger(req.Name, quant);
                    Core.RegisterQuests(9316);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("abyssalzone", "The Ashray", "Ashray Artifacts", log: false);
                        Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("abyssalzone", "Necro Adipocere", "Adipocere Antler", 3, log: false);
                        Core.HuntMonster("abyssalzone", "Foam Scavenger");
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;

                case "Sundered Tentacle":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.RegisterQuests(9269);
                    while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, quant))
                    {
                        Core.HuntMonster("twilightzone", "Leviathan", "Leviathan Tentacle", 1, true, false);
                        Core.HuntMonster("twilightzone", "Decay Spirit", "Decay Essence", 8, true, false);
                        Core.HuntMonster("twilightzone", "Ice Guardian", "Tarnished Icicle", 8, true, false);
                        Bot.Wait.ForPickup(req.Name);
                    }
                    Core.CancelRegisteredQuests();
                    break;
            }
        }
    }

    public void AttackVoiceInTheSea(string itemName, int quant)
    {

        // Register the quest
        Core.RegisterQuests(9349);
        Core.EquipClass(ClassType.Solo);
        Core.AddDrop("Algal Bloom");
        Core.Unbank("Algal Bloom");
        Bot.Options.AttackWithoutTarget = true;  // Enable AttackWithoutTarget
        while (!Bot.ShouldExit && !Core.CheckInventory(itemName, quant))
        {
            // Join the map "seavoice"
            if (Bot.Map.Name != "seavoice")
            {
                Core.Join("seavoice", "r2", "Left");
                Bot.Wait.ForMapLoad("seavoice");
            }

            // Ensure we're in the correct cell "r2", "Left"
            if (Bot.Player.Cell != "r2" || Bot.Player.Pad != "Left")
            {
                Core.Jump("r2", "Left");
                Bot.Sleep(2500);
            }

            // Attack the monster
            while (!Bot.ShouldExit && !Bot.Player.InCombat)
                Bot.Combat.Attack("Voice in the Sea");
        }

        Bot.Options.AttackWithoutTarget = false;  // Disable AttackWithoutTarget
        Core.CancelRegisteredQuests();  // Unregister the quest
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("79161", "Midnight Glaucus Sage", "Mode: [select] only\nShould the bot buy \"Midnight Glaucus Sage\" ?", false),
        new Option<bool>("79160", "Midnight Glaucus Mystic", "Mode: [select] only\nShould the bot buy \"Midnight Glaucus Mystic\" ?", false),
        new Option<bool>("79154", "Glaucus Sage", "Mode: [select] only\nShould the bot buy \"Glaucus Sage\" ?", false),
        new Option<bool>("79165", "Midnight Glaucus Locks", "Mode: [select] only\nShould the bot buy \"Midnight Glaucus Locks\" ?", false),
        new Option<bool>("79164", "Midnight Glaucus Hair", "Mode: [select] only\nShould the bot buy \"Midnight Glaucus Hair\" ?", false),
        new Option<bool>("79163", "Midnight Glaucus Visage", "Mode: [select] only\nShould the bot buy \"Midnight Glaucus Visage\" ?", false),
        new Option<bool>("79162", "Midnight Glaucus Morph", "Mode: [select] only\nShould the bot buy \"Midnight Glaucus Morph\" ?", false),
        new Option<bool>("79156", "Glaucus Visage", "Mode: [select] only\nShould the bot buy \"Glaucus Visage\" ?", false),
        new Option<bool>("79155", "Glaucus Morph", "Mode: [select] only\nShould the bot buy \"Glaucus Morph\" ?", false),
        new Option<bool>("79166", "Midnight Glaucus Companion", "Mode: [select] only\nShould the bot buy \"Midnight Glaucus Companion\" ?", false),
        new Option<bool>("79167", "Abyssal Atlanticus Trident", "Mode: [select] only\nShould the bot buy \"Abyssal Atlanticus Trident\" ?", false),
    };
}
