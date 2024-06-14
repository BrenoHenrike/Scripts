/*
name: Infernal Arena Merge
description: This bot will farm the items belonging to the selected mode for the Infernal Arena Merge [2334] in /infernalarena
tags: infernal, arena, merge, infernalarena, tainted, naal, cervus, malus, skull, krampus, claws, defiler, scythe, azalith, fallen, crown, , great, wings, divine, wanderer, wanderers, circlet, champions, mantle, guidance, spear, grace, serene, sharpshooter, bow, celestial, dire, engineers, horns
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story\QueenofMonsters\Extra\InfernalArena.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/CelestialArena.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class InfernalArenaMerge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private static CoreAdvanced sAdv = new();
    private InfernalArena IA = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Tainted Blade of Na'al", "Champion's Seal", "Tainted Dagger of Na'al",
         "Cervus Dente", "Infernal Krampus' Claw", "Infernal Emblem", "Axe of the Infernal Defiler", "Infernal Incantation",
          "Scythe Shard", "Duo's Dinner", "Infernal Badge", "Scythe of Azalith" });
        Core.SetOptions();

        BuyAllMerge();
        Core.SetOptions(false);
    }

    public void BuyAllMerge(string? buyOnlyThis = null, mergeOptionsEnum? buyMode = null)
    {
        IA.DoStory(true);
        Core.Logger("The mobs are strong so this may take a while.");
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("infernalarena", 2334, findIngredients, buyOnlyThis, buyMode: buyMode);

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

                case "Tainted Blade of Na'al":
                case "Champion's Seal":
                case "Tainted Dagger of Na'al":
                    Core.FarmingLogger(req.Name, quant);
                    Core.BossClass(Core.CheckInventory("Void HighLord (IoDA)") ? "Void HighLord (IoDA)" : "Void Highlord");
                    Core.HuntMonster("infernalarena", "Na'al", req.Name, quant, false, false);
                    break;

                case "Cervus Dente":
                    Core.FarmingLogger(req.Name, quant);
                    if (Core.CheckInventory(new[] { "Legion DoomKnight", "Classic Legion DoomKnight" }, any: true))
                        Core.BossClass(Core.CheckInventory("Legion DoomKnight") ? "Legion DoomKnight" : "Classic Legion DoomKnight");
                    Core.HuntMonster("infernalarena", "Cervus Malus", req.Name, quant, false, false);
                    break;

                case "Infernal Krampus' Claw":
                case "Infernal Emblem":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("infernalarena", "Infernal Krampus", req.Name, quant, false, false);
                    break;

                case "Axe of the Infernal Defiler":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("infernalarena", "Destructive Defiler", req.Name, quant, false, false);
                    break;

                case "Infernal Incantation":
                    Core.FarmingLogger(req.Name, quant);
                    Core.BossClass("Dragon of Time");
                    Core.HuntMonster("infernalarena", "Key of Sholemoh", req.Name, quant, false, false);
                    break;

                case "Scythe Shard":
                    Core.DodgeClass("Lord Of Order");
                    Core.HuntMonster("infernalarena", "Azalith's Scythe", req.Name, quant, false, false);
                    break;

                case "Duo's Dinner":
                    Core.FarmingLogger(req.Name, quant);
                    Core.BossClass();
                    Core.HuntMonster("infernalarena", "Deadly Duo", req.Name, quant, false, false);
                    break;

                case "Infernal Badge":
                    Core.FarmingLogger(req.Name, quant);
                    Core.EquipClass(ClassType.Farm);
                    Core.HuntMonster("infernalarena", "Infernal Mage", req.Name, quant, false, false);
                    break;

            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("79721", "Tainted Blades of Na'al", "Mode: [select] only\nShould the bot buy \"Tainted Blades of Na'al\" ?", false),
        new Option<bool>("79723", "Tainted Daggers of Na'al", "Mode: [select] only\nShould the bot buy \"Tainted Daggers of Na'al\" ?", false),
        new Option<bool>("79742", "Cervus Malus' Skull", "Mode: [select] only\nShould the bot buy \"Cervus Malus' Skull\" ?", false),
        new Option<bool>("79748", "Infernal Krampus' Claws", "Mode: [select] only\nShould the bot buy \"Infernal Krampus' Claws\" ?", false),
        new Option<bool>("79750", "Axes of the Infernal Defiler", "Mode: [select] only\nShould the bot buy \"Axes of the Infernal Defiler\" ?", false),
        new Option<bool>("79659", "Scythe of Azalith", "Mode: [select] only\nShould the bot buy \"Scythe of Azalith\" ?", false),
        new Option<bool>("79660", "Scythe of the Fallen", "Mode: [select] only\nShould the bot buy \"Scythe of the Fallen\" ?", false),
        new Option<bool>("79713", "Armor of Azalith", "Mode: [select] only\nShould the bot buy \"Armor of Azalith\" ?", false),
        new Option<bool>("79714", "Crown of Azalith", "Mode: [select] only\nShould the bot buy \"Crown of Azalith\" ?", false),
        new Option<bool>("79715", "Crown + Locks of Azalith", "Mode: [select] only\nShould the bot buy \"Crown + Locks of Azalith\" ?", false),
        new Option<bool>("79717", "Great Wings of Azalith", "Mode: [select] only\nShould the bot buy \"Great Wings of Azalith\" ?", false),
        new Option<bool>("79724", "Divine Wanderer", "Mode: [select] only\nShould the bot buy \"Divine Wanderer\" ?", false),
        new Option<bool>("79725", "Divine Wanderer's Circlet", "Mode: [select] only\nShould the bot buy \"Divine Wanderer's Circlet\" ?", false),
        new Option<bool>("79726", "Divine Wanderer's Locks", "Mode: [select] only\nShould the bot buy \"Divine Wanderer's Locks\" ?", false),
        new Option<bool>("79727", "Divine Champion's Mantle", "Mode: [select] only\nShould the bot buy \"Divine Champion's Mantle\" ?", false),
        new Option<bool>("79728", "Divine Wanderer's Guidance", "Mode: [select] only\nShould the bot buy \"Divine Wanderer's Guidance\" ?", false),
        new Option<bool>("79729", "Spear of Divine Grace", "Mode: [select] only\nShould the bot buy \"Spear of Divine Grace\" ?", false),
        new Option<bool>("79730", "Serene Sharpshooter Bow", "Mode: [select] only\nShould the bot buy \"Serene Sharpshooter Bow\" ?", false),
        new Option<bool>("79731", "Celestial Wanderer", "Mode: [select] only\nShould the bot buy \"Celestial Wanderer\" ?", false),
        new Option<bool>("79732", "Celestial Champion's Mantle", "Mode: [select] only\nShould the bot buy \"Celestial Champion's Mantle\" ?", false),
        new Option<bool>("79733", "Celestial Wanderer's Guidance", "Mode: [select] only\nShould the bot buy \"Celestial Wanderer's Guidance\" ?", false),
        new Option<bool>("79734", "Celestial Sharpshooter Bow", "Mode: [select] only\nShould the bot buy \"Celestial Sharpshooter Bow\" ?", false),
        new Option<bool>("79737", "Dire Armor of Na'al", "Mode: [select] only\nShould the bot buy \"Dire Armor of Na'al\" ?", false),
        new Option<bool>("79738", "Dire Helm of Na'al", "Mode: [select] only\nShould the bot buy \"Dire Helm of Na'al\" ?", false),
        new Option<bool>("79740", "Infernal Engineer's Skull + Horns", "Mode: [select] only\nShould the bot buy \"Infernal Engineer's Skull + Horns\" ?", false),
    };
}
