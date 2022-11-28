//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreDailies.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class CoreSDKA
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public static CoreBots sCore => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();
    public CoreDailies Daily = new();

    public string OptionsStorage = "SupulchuresDoomKnightArmorOptions";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        CoreBots.Instance.SkipOptions,
        new Option<SDKAQuest>("SelectedQuest", "Dark Spirit Orbs Quest",
            "Which quest should the bot use to farm Dark Spirit Orbs with?\nRecommended setting: A Penny for Your Foughts", SDKAQuest.APennyforYourFoughts),
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.RunCore();
    }

    public string[] SDKAItems =
    {
        "Sepulchure's DoomKnight Armor",
        // Arsenics
        "Arsenic",
        "Accursed Arsenic",
        "Accursed Arsenic of Doom",
        // Daggers
        "Daggers of Destruction",
        "Shadow Daggers of Destruction",
        "Necrotic Daggers of Destruction",
        // Chromiums
        "Chromium",
        "Calamitous Chromium",
        "Calamitous Chromium of Doom",
        // Broadswords
        "Broadsword of Bane",
        "Shadow Broadsword of Bane",
        "Necrotic Broadsword of Bane",
        // Rhodiums
        "Rhodium",
        "Reprehensible Rhodium",
        "Reprehensible Rhodium of Doom",
        // Bows
        "Bow to the Shadows",
        "ShadowBow of the Shadows",
        "Necrotic Bow of the Shadow",
        // Merge misc.
        "Experimental Dark Item",
        "Dark Energy",
        "Dark Spirit Orb",
        "Corrupt Spirit Orb",
        "Ominous Aura",
        "Diabolical Aura",
        "Doom Aura",
        // Weapon kits
        "DoomSquire Weapon Kit",
        "DoomSoldier Weapon Kit",
        "DoomKnight Weapon Kit",
        // Misc.
        "DoomKnight Hood",
        "Elders' Blood",
        "Undead Energy",
        "Iron Hammer",
        "War Mummy Wrap",
        "Stone Hammer",
        "Grumpy Warhammer",
        "Shadow Terror Axe",
        "DoomCoin",
        "Dark Skull",
        "Shadow Creeper Enchant",
        "Shadow Serpent Scythe"
    };

    public void DoAll()
    {
        if (Core.CheckInventory("Sepulchure's DoomKnight Armor") || !Core.IsMember)
            return;

        Core.AddDrop(SDKAItems);

        UnlockHardCoreMetals();
        NecroticDaggers();
        NecroticBroadsword();
        NecroticBow();
        SummoningSepulchureArmor();
    }

    public void UnlockHardCoreMetals()
    {
        if (Bot.Quests.IsUnlocked(2098))
        {
            Core.Logger("Hard Core Metals already unlocked, skipping.");
            return;
        }

        Core.AddDrop("Dark Energy", "Dark Spirit Orb", "DoomKnight Hood",
                     "Experimental Dark Item", "Shadow Terror Axe", "Elders' Blood",
                     "DoomCoin", "Shadow Creeper Enchant", "Shadow Serpent Scythe",
                     "Dark Skull", "Corrupt Spirit Orb");

        if (!Bot.Quests.IsUnlocked(2087))
        {
            Core.Logger("Sepulchure's Armor [2069]");
            DSO(40);
            Core.BuyItem("shadowfall", 100, "DoomKnight Hood");
            Core.ChainComplete(2069);
            Bot.Drops.Pickup("Experimental Dark Item");
            Core.ToBank("Experimental Dark Item");
        }

        if (!Bot.Quests.IsUnlocked(2088))
        {
            Adv.GearStore();
            Core.Logger("The Doom that Looms [2087]");
            if (!Core.CheckInventory(2083))
            {
                Core.Logger("You don't have the DoomKnight Class, Getting it for you. (+warrior/Healer if those aren't R10)");

                Core.BuyItem("trainers", 176, "Healer");
                Adv.rankUpClass("Healer", false);

                Core.BuyItem("trainers", 170, "Warrior");
                Adv.rankUpClass("Warrior", false);

                Adv.BuyItem("shadowfall", 100, "DoomKnight", shopItemID: 6309);
            }
            Adv.rankUpClass("DoomKnight", false);

            Adv.GearStore(true);
            Core.EquipClass(ClassType.Solo);

            Core.ChainComplete(2087);
            Core.ToBank("DoomKnight");
        }

        if (!Bot.Quests.IsUnlocked(2089))
        {
            Core.Logger("Toiling with Terror [2088]");
            Daily.EldersBlood();

            if (!Core.CheckInventory("Elders' Blood"))
                Core.Logger($"Not enough \"Elders' Blood\", please do the daily at a later date", messageBox: true, stopBot: true);

            Core.HuntMonster("battleundera", "Bone Terror", "Shadow Terror Axe", 1, false);
            Core.ChainComplete(2088);
            Core.ToBank("Elders' Blood");
        }

        if (!Bot.Quests.IsUnlocked(2090))
        {
            Core.Logger("Quest: A Penny for your Foughts [2089]");
            Penny(oneTime: true);
        }

        if (!Bot.Quests.IsAvailable(2098))
        {
            Core.Logger("Quest: Dark Spirit Donation [2090]");
            Core.EnsureAccept(2090);
            DSO(100);
            Core.HuntMonster("necrocavern", "Shadow Imp", "Dark Skull", 1, false);
            Core.EnsureComplete(2090);
        }
    }

    public void FarmDSO(int quant = 10500)
    {
        if (Core.CheckInventory("Dark Spirit Orb", quant))
            return;

        if (Bot.Config.Get<SDKAQuest>("SelectedQuest") == SDKAQuest.DarkSpiritOrbs)
            DSO(quant);
        else Penny(quant);
    }

    public void Penny(int quant = 10500, bool oneTime = false)
    {
        if (Core.CheckInventory("Dark Spirit Orb", quant) && !oneTime)
            return;

        Core.AddDrop("DoomCoin", "Dark Spirit Orb", "Shadow Creeper Enchant");
        if (!oneTime)
        {
            Core.Logger($"Farming {quant} DSOs");
            Core.EquipClass(ClassType.Farm);
        }
        else Core.Logger($"Farming {quant} DSOs");

        Core.RegisterQuests(2089);
        while (!Bot.ShouldExit && (!Core.CheckInventory("Dark Spirit Orb", quant)))
        {
            Core.KillMonster("maul", "r7", "Left", "*", "DoomCoin", oneTime ? 20 : 80, false);
            Bot.Drops.Pickup("Dark Spirit Orb");
            if (oneTime)
                break;
        }
        Core.CancelRegisteredQuests();
    }

    public void DSO(int quant = 10500)
    {
        if (Core.CheckInventory("Dark Spirit Orb", quant))
            return;

        Core.AddDrop("Dark Spirit Orb", "Shadow Creeper Enchant", "Shadow Serpent Scythe");
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming {quant} DSOs");
        Core.RegisterQuests(2065);
        while (!Bot.ShouldExit && (!Core.CheckInventory("Dark Spirit Orb", quant)))
        {
            Core.HuntMonster("bludrut2", "Shadow Creeper", "Shadow Creeper Enchant", 1, false);
            Core.HuntMonster("bludrut4", "Shadow Serpent", "Shadow Serpent Scythe", 1, false);
            Core.HuntMonster("ruins", "Dark Witch", "Shadow Whiskers", 6);

            if (Core.CheckInventory("Dark Energy", 5000))
                DoomMerge("Dark Spirit Orb", 100);
        }
        Core.CancelRegisteredQuests();
    }

    public void DoomMerge(string item, int quant = 1)
        => Core.BuyItem("necropolis", 423, item, quant);

    public void DoomSquireWK(int quant = 1)
    {
        if (Core.CheckInventory("DoomSquire Weapon Kit", quant))
            return;

        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming {quant} DoomSquire Weapon Kit");
        Core.RegisterQuests(2144);
        while (!Bot.ShouldExit && (!Core.CheckInventory("DoomSquire Weapon Kit", quant)))
        {
            Core.BuyItem("swordhaven", 179, "Iron Hammer");

            Core.KillMonster("sandcastle", "r5", "Left", "War Mummy", "War Mummy Wrap", 1, false);
            Core.KillMonster("noobshire", "North", "Left", "Horc Noob", "Noob Blade Oil");
            Core.KillMonster("farm", "Crop1", "Right", "Scarecrow", "Burlap Cloth", 4);

            Core.HuntMonster("lair", "Bronze Draconian", "Bronze Brush");
            Core.HuntMonster("bludrut", "Rock Elemental", "Elemental Stone Sharpener");
            Core.HuntMonster("nulgath", "Dark Makai", "Dark Makai Lacquer Finish");

            Bot.Drops.Pickup("DoomSquire Weapon Kit");
        }
        Core.CancelRegisteredQuests();
    }

    public void DoomSoldierWK(int quant = 1)
    {
        if (Core.CheckInventory("DoomSoldier Weapon Kit", quant))
            return;

        Core.Logger($"Farming {quant} DoomSoldier Weapon Kit");
        Core.RegisterQuests(2164);
        while (!Bot.ShouldExit && (!Core.CheckInventory("DoomSoldier Weapon Kit", quant)))
        {
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("cornelis", "Stone Golem", "Stone Hammer", 1, false);
            Core.HuntMonster("hachiko", "Dai Tengu", "Superior Blade Oil", publicRoom: true);
            Core.HuntMonster("vordredboss", "Shadow Vordred", "Shadow Lacquer Finish", publicRoom: true);
            Core.HuntMonster("anders", "Copper Sky Pirate", "Copper Awl");
            Core.HuntMonster("necrocavern", "Shadow Imp", "Shadowstone Sharpener");

            Core.EquipClass(ClassType.Farm);
            Core.KillMonster("lycan", "r4", "Left", "Chaos Vampire Knight", "Silver Brush");
            Core.KillMonster("sandport", "r3", "Right", "Tomb Robber", "Leather Case");
            Core.KillMonster("pines", "Path1", "Left", "Leatherwing", "Leatherwing Hide", 10);

            Bot.Drops.Pickup("DoomSoldier Weapon Kit");
        }
        Core.CancelRegisteredQuests();
    }

    public void DoomKnightWK(string item = "DoomKnight Weapon Kit", int quant = 1)
    {
        if (Core.CheckInventory(item, quant))
            return;

        Core.AddDrop("DoomKnight Weapon Kit", "Dark Spirit Orb", "Corrupt Spirit Orb", "Ominous Aura", "Grumpy Warhammer");
        Core.EquipClass(ClassType.Solo);
        Core.Logger($"Farming {quant} {item}");
        Core.RegisterQuests(2165);
        while (!Bot.ShouldExit && (!Core.CheckInventory(item, quant)))
        {
            Core.KillMonster("boxes", "Boss", "Left", "Sneeviltron", "Grumpy Warhammer", 1, false);
            Core.KillMonster("kitsune", "Boss", "Left", "Kitsune", "No. 1337 Blade Oil", publicRoom: true);
            Core.KillMonster("sandcastle", "r7", "Left", "Chaos Sphinx", "Gold Brush", publicRoom: true);
            Core.KillMonster("crashsite", "Boss", "Left", "ProtoSartorium", "Non-abrasive Power Powder");
            Core.KillMonster("necrocavern", "r13", "Left", "Shadow Dragon", "ShadowDragon Hide", 3);
            Core.KillMonster("dragonplane", "r9", "Left", "Moganth", "Moganth's Stone Sharpener");
            Core.KillMonster("akiba", "cave4boss", "Left", "Shadow Nukemichi", "Doom Lacquer Finish");
            Core.KillMonster("dreamnexus", "r6", "Left", "Dark Wyvern", "Dark Wyvern Hide Travel Case");

            Bot.Drops.Pickup(item);
        }
        Core.CancelRegisteredQuests();
    }

    public void NecroticDaggers()
    {
        if (Core.CheckInventory("Necrotic Daggers of Destruction"))
        {
            Core.Logger("Daggers found, skipping");
            return;
        }

        if (!Core.CheckInventory(new[] { "Necrotic Daggers of Destruction", "Shadow Daggers of Destruction", "Daggers of Destruction" }, any: true))
        {
            if (!Core.CheckInventory(new[] { "Accursed Arsenic of Doom", "Accursed Arsenic" }, any: true)
                && !Core.CheckInventory("Ominous Aura", 2))
            {
                int DSOQuantity = 10500 - (Bot.Inventory.GetQuantity("Corrupt Spirit Orb") * 100) - (Bot.Inventory.GetQuantity("Ominous Aura") * 5000);
                FarmDSO(DSOQuantity);
            }

            if (!Core.CheckInventory(new[] { "Accursed Arsenic of Doom", "Accursed Arsenic" }, any: true)
                && !Core.CheckInventory("Ominous Aura", 2))
            {
                int CSOQuantity = 105 - (Bot.Inventory.GetQuantity("Ominous Aura") * 50);
                if ((CSOQuantity * 100) > Bot.Inventory.GetQuantity("Dark Spirit Orb"))
                    FarmDSO(CSOQuantity * 100);
                DoomMerge("Corrupt Spirit Orb", CSOQuantity);
            }

            if (!Core.CheckInventory(new[] { "Accursed Arsenic of Doom", "Accursed Arsenic" }, any: true)
                && !Core.CheckInventory("Ominous Aura", 2))
                DoomMerge("Ominous Aura", 2);

            if (!Core.CheckInventory("Accursed Arsenic of Doom"))
            {
                if (!Core.CheckInventory("Accursed Arsenic"))
                {
                    Core.Logger("Farming for Accursed Arsenic");
                    Core.EnsureAccept(2110);
                    Core.HuntMonster("bludrut4", "Shadow Serpent", "Dark Energy", 26, false);
                    Daily.HardCoreMetals(new[] { "Arsenic" });
                    if (!Core.CheckInventory("Arsenic"))
                        Core.Logger("Can't complete Accursed Arsenic Hex (Missing Arsenic).", messageBox: true, stopBot: true);
                    DSO(6);
                    Core.HuntMonster("arcangrove", "Seed Spitter", "Deadly Knightshade", 16);
                    Core.EnsureComplete(2110);
                }
                int CSOQuantity = 105 - (Bot.Inventory.GetQuantity("Ominous Aura") * 50);
                if ((CSOQuantity * 100) > Bot.Inventory.GetQuantity("Dark Spirit Orb"))
                    FarmDSO(CSOQuantity * 100);
                DoomMerge("Corrupt Spirit Orb", CSOQuantity);
                DoomMerge("Ominous Aura", 2);
                Core.BuyItem("dwarfhold", 434, "Accursed Arsenic of Doom");
            }

            if (!Bot.Quests.IsUnlocked(2144))
            {
                Core.Logger("Unlocking Weapon Kit quests");
                Core.EnsureAccept(2137);
                Core.KillMonster("dwarfhold", "Enter", "Spawn", "Albino Bat", "Forge Key", 1, false);
                Core.EnsureComplete(2137);
            }
            Core.Logger("Farming for Daggers of Destruction");
            DoomSquireWK();
            FarmDSO(50);
            DoomMerge("Daggers of Destruction");
        }

        if (Core.CheckInventory("Daggers of Destruction"))
        {
            Core.Logger("Farming for Shadow Daggers of Destruction");
            DoomSoldierWK();
            DoomKnightWK("Ominous Aura");
            DoomMerge("Shadow Daggers of Destruction");
        }

        if (Core.CheckInventory("Shadow Daggers of Destruction"))
        {
            Core.Logger("Farming for Necrotic Daggers of Destruction");
            DoomKnightWK();
            DoomMerge("Necrotic Daggers of Destruction");
        }
    }

    public void NecroticBroadsword()
    {
        if (Core.CheckInventory("Necrotic Broadsword of Bane"))
        {
            Core.Logger("Broadsword found, skipping.");
            return;
        }
        if (!Core.CheckInventory("Necrotic Daggers of Destruction"))
            NecroticDaggers();

        if (!Core.CheckInventory(new[] { "Necrotic Broadsword of Bane", "Shadow Broadsword of Bane", "Broadsword of Bane" }, any: true))
        {
            if (!Core.CheckInventory("Calamitous Chromium of Doom"))
            {
                if (!Core.CheckInventory("Calamitous Chromium"))
                {
                    Core.Logger("Farming for Calamitous Chromium");
                    Core.EnsureAccept(2112);
                    Core.HuntMonster("bludrut4", "Shadow Serpent", "Dark Energy", 26, false);
                    Daily.HardCoreMetals(new[] { "Chromium" });
                    if (!Core.CheckInventory("Chromium"))
                        Core.Logger("Can't complete Calamitous Chromium Hex (Missing Chromium).", messageBox: true, stopBot: true);
                    DSO(6);
                    Core.HuntMonster("arcangrove", "Seed Spitter", "Deadly Knightshade", 16);
                    Core.EnsureComplete(2112);
                    Bot.Drops.Pickup("Calamitous Chromium");
                }
                Core.Logger("Farming for Calamitous Chromium of Doom");
                PinpointDaggers();
                DoomKnightWK("Corrupt Spirit Orb", 5);
                Core.BuyItem("dwarfhold", 434, "Calamitous Chromium of Doom");
            }
            if (!Core.CheckInventory("Diabolical Aura"))
            {
                PinpointDaggers(25);
                DoomMerge("Diabolical Aura");
            }
            Core.Logger("Farming for Broadsword of Bane");
            DoomKnightWK("Corrupt Spirit Orb");
            DoomKnightWK("Dark Spirit Orb", 20);
            DoomSquireWK();
            DoomMerge("Broadsword of Bane");
        }

        if (Core.CheckInventory("Broadsword of Bane"))
        {
            Core.Logger("Farming for Shadow Broadsword of Bane");
            DoomKnightWK("Corrupt Spirit Orb");
            PinpointDaggers(1);
            DoomSoldierWK();
            DoomMerge("Shadow Broadsword of Bane");
        }

        if (Core.CheckInventory("Shadow Broadsword of Bane"))
        {
            Core.Logger("Farming for Necrotic Broadsword of Bane");
            DoomKnightWK();
            DoomMerge("Necrotic Broadsword of Bane");
        }
    }

    public void NecroticBow()
    {
        if (Core.CheckInventory("Necrotic Bow of the Shadow"))
        {
            Core.Logger("Bow found, skipping.");
            return;
        }
        if (!Core.CheckInventory("Necrotic Broadsword of Bane"))
            NecroticBroadsword();

        if (!Core.CheckInventory(new[] { "Necrotic Bow of the Shadow", "ShadowBow of the Shadows", "Bow to the Shadows" }, any: true))
        {
            if (!Core.CheckInventory("Reprehensible Rhodium of Doom"))
            {
                if (!Core.CheckInventory("Reprehensible Rhodium"))
                {
                    Core.Logger("Farming for Reprehensible Rhodium");
                    Core.EnsureAccept(2114);
                    Core.HuntMonster("bludrut4", "Shadow Serpent", "Dark Energy", 26, false);
                    Daily.HardCoreMetals(new[] { "Rhodium" });
                    if (!Core.CheckInventory("Rhodium"))
                        Core.Logger("Can't complete Reprehensible Rhodium Hex (Missing Rhodium).", messageBox: true, stopBot: true);
                    DSO(6);
                    Core.HuntMonster("arcangrove", "Seed Spitter", "Deadly Knightshade", 16);
                    Core.EnsureComplete(2114);
                    Bot.Drops.Pickup("Reprehensible Rhodium");
                }
                Core.Logger("Farming for Reprehensible Rhodium of Doom");
                PinpointDaggers();
                DoomKnightWK("Corrupt Spirit Orb", 5);
                Core.BuyItem("dwarfhold", 434, "Reprehensible Rhodium of Doom");
            }
            Core.Logger("Farming for Bow to the Shadows");
            DoomSquireWK();
            DoomKnightWK("Corrupt Spirit Orb");
            DoomKnightWK("Dark Spirit Orb", 13);
            PinpointBroadsword();
            Farm.BattleUnderB("Undead Energy", 17);
            DoomMerge("Bow to the Shadows");
        }

        if (Core.CheckInventory("Bow to the Shadows"))
        {
            Core.Logger("Farming for ShadowBow of the Shadows");
            DoomSoldierWK();
            DoomKnightWK("Corrupt Spirit Orb");
            Core.HuntMonster("bludrut4", "Shadow Serpent", "Dark Energy", 50, false);
            DoomMerge("ShadowBow of the Shadows");
        }

        if (Core.CheckInventory("ShadowBow of the Shadows"))
        {
            Core.Logger("Farming for Necrotic Bow of the Shadow");
            DoomKnightWK();
            DoomMerge("Necrotic Bow of the Shadow");
        }
    }

    public void SummoningSepulchureArmor()
    {
        if (Core.CheckInventory("Sepulchure's DoomKnight Armor"))
            return;

        Core.Logger("Final part");
        PinpointBow(500, 250);
        PinpointDaggers(125);
        PinpointBroadsword(75);
        int i = 1;
        Core.Logger(Core.CheckInventory("Doom Aura") ? "Doom Aura found." : "Farming for Doom Aura");
        while (!Bot.ShouldExit && (!Core.CheckInventory("Doom Aura")))
        {
            PinpointthePieces(2181);
            Bot.Drops.Pickup("Doom Aura");
            Core.Logger($"Completed x{i}");
        }
        if (!Core.CheckInventory("Experimental Dark Item"))
        {
            PinpointBow(50, 0);
            Core.BuyItem("shadowfall", 100, "DoomKnight Hood");
            Core.ChainComplete(2069);
            Bot.Drops.Pickup("Experimental Dark Item");
        }
        DoomKnightWK();
        Core.EnsureAccept(2187);
        Core.HuntMonster("ruins", "Dark Elemental", "Heart of Darkness");
        Core.EnsureComplete(2187);
        Bot.Drops.Pickup("Sepulchure's DoomKnight Armor");
    }

    public void PinpointDaggers(int quant = 5)
    {
        if (Core.CheckInventory("Ominous Aura", quant))
            return;

        if (!Core.CheckInventory("Necrotic Daggers of Destruction"))
            NecroticDaggers();

        int i = 1;
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming {quant} Ominous Aura");
        while (!Bot.ShouldExit && (!Core.CheckInventory("Ominous Aura", quant)))
        {
            PinpointthePieces(2181);
            Bot.Drops.Pickup("Ominous Aura");
            Core.Logger($"Completed x{i++}");
        }
    }

    public void PinpointBroadsword(int quant = 1)
    {
        if (Core.CheckInventory("Diabolical Aura", quant))
            return;

        if (!Core.CheckInventory("Necrotic Broadsword of Bane"))
            NecroticBroadsword();

        int i = 1;
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming {quant} Diabolical Aura");
        while (!Bot.ShouldExit && (!Core.CheckInventory("Diabolical Aura", quant)))
        {
            PinpointthePieces(2183);
            Bot.Drops.Pickup("Diabolical Aura");
            Core.Logger($"Completed x{i}");
        }
    }

    public void PinpointBow(int quantDSO, int quantCSO)
    {
        if (Core.CheckInventory("Dark Spirit Orb", quantDSO) && Core.CheckInventory("Corrupt Spirit Orb", quantDSO))
            return;

        if (!Core.CheckInventory("Necrotic Bow of the Shadow"))
            NecroticBow();

        int i = 1;
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming {quantDSO} DSOs and {quantCSO} CSOs");
        while (!Bot.ShouldExit && (!Core.CheckInventory("Dark Spirit Orb", quantDSO)) || (!Core.CheckInventory("Corrupt Spirit Orb", quantDSO)))
        {
            PinpointthePieces(2186);
            Bot.Drops.Pickup("Dark Spirit Orb", "Corrupt Spirit Orb");
            Core.Logger($"Completed x{i}");
        }
    }

    public void PinpointthePieces(int quest)
    {
        Core.AddDrop("Dark Energy", "Dark Spirit Orb", "Corrupt Spirit Orb", "Ominous Aura", "Diabolical Aura", "Doom Aura");

        Core.EnsureAccept(quest);
        Core.KillMonster("lycan", "r4", "Left", "*", "DoomKnight Armor Piece", 10);
        Core.EnsureComplete(quest);
    }
}

public enum SDKAQuest
{
    APennyforYourFoughts,
    DarkSpiritOrbs,
}
