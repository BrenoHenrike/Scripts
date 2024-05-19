/*
name: SeaVoice Merge
description: This bot will farm the items belonging to the selected mode for the SeaVoice Merge [2320] in /seavoice
tags: seavoice, merge, seavoice, midnight, glaucus, sage, mystic, morph, companion, abyssal, atlanticus, trident
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using System.Dynamic;
using Newtonsoft.Json;
using Skua.Core.Interfaces;
using Skua.Core.Models.Auras;
using Skua.Core.Models.Items;
using Skua.Core.Models.Monsters;
using Skua.Core.Models.Skills;
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
        // Define the possible solo classes
        string[] PossibleSoloClasses = new[] { "Chaos Avenger", "Verus Doomknight", "Void Highlord", "ArchPaladin" };

        if (!Core.CheckInventory(PossibleSoloClasses, any: true))
            Core.Logger("no Soloing classes found stopping (go get AP atleast and rerun)", stopBot: true);

        // Register the quest
        Core.RegisterQuests(9349);
        Core.EquipClass(ClassType.Solo);
        Core.AddDrop("Algal Bloom");
        Core.Unbank("Algal Bloom");
        Adv.GearStore();
        while (!Bot.ShouldExit && !Core.CheckInventory(itemName, quant))
        {
            // Find the first available class in inventory or bank
            string? selectedClass = PossibleSoloClasses.FirstOrDefault(className =>
                Bot.Inventory.Items.Any(item => item.Name == className) ||
                Bot.Bank.Items.Any(item => item.Name == className)
            );

            Core.Logger($"Soloing \"Voice of the Sea\" with {selectedClass}");

            Adv.SmartEnhance(selectedClass);

            // Call the KillThing method with the specified parameters
            KillThing(
                map: "seavoice",
                mobMapID: 1,
                targetAuraName: "Oxidize",
                ItemUsed: 78994,
                Class: selectedClass,
                item: itemName,
                quant: quant,
                isTemp: false
            );
        }
        Adv.GearStore(true);
        Core.CancelRegisteredQuests();  // Unregister the quest

    }
    
    public void KillThing(string? map = null, int mobMapID = 1, string? targetAuraName = null, int ItemUsed = 1, string? Class = null, string? item = null, int quant = 1, bool isTemp = false)
    {
        Adv.BuyItem("seavoice", 2320, "Vigil", 1000, 12023);
        // ItemCheckingAndBuying();

        Core.Join(map);
        Bot.Wait.ForMapLoad(map!);
        Bot.Wait.ForTrue(() => Bot.Player.Loaded, 20);

        Core.Logger($"map: {map}");
        Core.Logger($"mobMapID: {mobMapID}");
        Core.Logger($"targetAuraName: {targetAuraName}");
        Core.Logger($"ItemUsed: {ItemUsed} [Vigil]");
        Core.Logger($"Class: {Class}");
        Core.Logger($"item: {item}");
        Core.Logger($"quant: {quant}");
        Core.Logger($"isTemp: {isTemp}");

        Core.Equip(Class!);
        if (Class == "Void Highlord")
            Bot.Skills.StartAdvanced("Void HighLord", true, ClassUseMode.Def);
        Core.Equip(ItemUsed);
        Core.Logger($"{ItemUsed} [Vigil] Equiped? {Bot.Inventory.IsEquipped("Vigil")}");

        Monster? mob = Bot.Monsters.MapMonsters.FirstOrDefault(m => m.MapID == mobMapID);
        if (targetAuraName != null)
        {
            Aura? targetAura = Bot.Target.Auras.Concat(Bot.Self.Auras).FirstOrDefault(a => a.Name == targetAuraName);
        }

        if (Bot.Player.Cell != mob!.Cell)
            Core.Jump(mob.Cell);

        #region  UltraSpeaker
        // if (map == "ultraspeaker")
        // {
        //     Random random = new();
        //     int xpos = random.Next(1, 30);
        //     int ypos = random.Next(1, 30);
        //     Bot.Player.WalkTo(x: xpos, y: ypos);
        // }
        #endregion
        Bot.Player.SetSpawnPoint();

        while (!Bot.ShouldExit && item != null && isTemp ? !Bot.TempInv.Contains(item!, quant) : !Core.CheckInventory(item!, quant))
        {
            //Check if/move to /in mob cell && Bot.Player.Alive)
            if (Bot.Player.Cell != mob.Cell)
                Core.Jump(mob.Cell);

            #region  UltraSpeaker
            if (map == "ultraspeaker")
            {
                while (!Bot.ShouldExit && !Bot.Player.Alive)
                    Core.Sleep();
            }
            #endregion

            if (targetAuraName != null)
                AuraHandling(targetAuraName);

            if (Bot.Player.Alive && !Bot.Self.HasActiveAura(targetAuraName!) && !Bot.Target.HasActiveAura(targetAuraName!))
                Bot.Combat.Attack(mob);
            Core.Sleep();

            if (isTemp ? Bot.TempInv.Contains(item!, quant) : Core.CheckInventory(item, quant))
            {
                break;
            }
        }

        void AuraHandling(string? targetAuraName)
        {
            foreach (Aura A in Bot.Target.Auras.Concat(Bot.Self.Auras))
            {
                if (targetAuraName == null)
                    continue;

                switch (A.Name)
                {
                    case "Oxidize":
                        while (!Bot.ShouldExit && !Bot.Self.HasActiveAura("Vigil"))
                        {
                            UsePotion();
                            Core.Sleep();

                            // Check if targetAura is not null before accessing its SecondsRemaining() method
                            // Assuming `targetAura` is the aura you're referring to
                            if (Bot.Self.HasActiveAura("Vigil"))
                            {
                                Core.Logger($"Vigil Active!");
                                break;
                            }
                        }
                        break;

                    case null:
                        break;
                }
            }
        }

        void UsePotion()
        {
            var skill = Bot.Flash.GetArrayObject<dynamic>("world.actions.active", 5);
            if (skill == null) return;
            Bot.Flash.CallGameFunction("world.testAction", JsonConvert.DeserializeObject<ExpandoObject>(JsonConvert.SerializeObject(skill))!);
        }
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
