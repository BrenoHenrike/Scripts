/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
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
    public CoreStory Story = new();

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
        {
            Core.Logger(Core.CheckInventory("Sepulchure's DoomKnight Armor") ? "Player already owns the armor" : "Player is non-Member, membership is required for SDKA");
            return;
        }

        Core.AddDrop(SDKAItems);

        UnlockHardCoreMetals();
        NecroticDaggers();
        NecroticBroadsword();
        NecroticBow();
        SummoningSepulchureArmor();
    }

    public void UnlockHardCoreMetals()
    {
        if (!Core.IsMember || Core.isCompletedBefore(2090))
        {
            Core.Logger(message: !Core.IsMember ? "Not a member, skipping." : "Hard Core Metals already unlocked, skipping.");
            return;
        }


        Core.AddDrop("Dark Energy", "Dark Spirit Orb", "DoomKnight Hood",
                     "Experimental Dark Item", "Shadow Terror Axe", "Elders' Blood",
                     "DoomCoin", "Shadow Creeper Enchant", "Shadow Serpent Scythe",
                     "Dark Skull", "Corrupt Spirit Orb");

        #region DoQuests
        if (!Story.QuestProgression(2069))
        {
            Core.EnsureAccept(2069);
            DSO(40);
            Core.BuyItem("shadowfall", 100, "DoomKnight Hood");
            Core.AddDrop("Experimental Dark Item");
            Core.EnsureComplete(2069);
            Bot.Wait.ForPickup("Experimental Dark Item");
            Core.ToBank("Experimental Dark Item");
        }

        if (!Story.QuestProgression(2087))
        {
            Core.EnsureAccept(2087);
            if (!Core.CheckInventory(2083))
            {
                Core.Logger("You don't have the DoomKnight Class, Getting it for you. (+warrior/Healer if those aren't R10)");

                Core.BuyItem("trainers", 176, "Healer");
                Adv.RankUpClass("Healer");

                Core.BuyItem("trainers", 170, "Warrior");
                Adv.RankUpClass("Warrior");

                Adv.BuyItem("shadowfall", 100, "DoomKnight", shopItemID: 6309);
            }
            Adv.RankUpClass("DoomKnight");
            Core.EquipClass(ClassType.Solo);

            Core.EnsureComplete(2087);
            if (Core.SoloClass != "DoomKnight")
                Core.ToBank("DoomKnight");
        }

        if (!Story.QuestProgression(2088))
        {
            Core.EnsureAccept(2088);
            Daily.EldersBlood();

            if (!Core.CheckInventory("Elders' Blood"))
                Core.Logger($"Not enough \"Elders' Blood\", please do the daily upon daily reset.", messageBox: true, stopBot: true);

            Core.HuntMonster("battleundera", "Bone Terror", "Shadow Terror Axe", isTemp: false);
            Core.EnsureComplete(2088);
            Core.ToBank("Elders' Blood");
        }

        // 2089
        Penny(Bot.Inventory.GetQuantity("Dark Spirit Orb") + 1, true);

        if (!Story.QuestProgression(2090))
        {
            Core.EnsureAccept(2090);
            DSO(100);
            Core.HuntMonster("necrocavern", "Shadow Imp", "Dark Skull", isTemp: false);
            Core.EnsureComplete(2090);
        }
        #endregion DoQuests
    }

    public void FarmDSO(int quant = 10500)
    {
        if (Core.CheckInventory("Dark Spirit Orb", quant))
            return;

        if (Bot.Config!.Get<SDKAQuest>("SelectedQuest") == SDKAQuest.DarkSpiritOrbs)
            DSO(quant);
        else Penny(quant);
    }

    public void Penny(int quant = 10500, bool oneTime = false)
    {
        if (Core.CheckInventory("Dark Spirit Orb", quant) && !oneTime)
            return;

        Core.Logger(oneTime
        ? $"oneTime set to: {oneTime}"
        : $"Farming \"Dark Spirit Orb\" {Core.dynamicQuant("Dark Spirit Orb", false)} / {quant}");

        Core.AddDrop("DoomCoin", "Dark Spirit Orb", "Shadow Creeper Enchant");
        Core.EquipClass(ClassType.Farm);
        if (oneTime)
        {
            Core.EnsureAccept(2089);
            Core.KillMonster("maul", "r7", "left", "Shelleton", "DoomCoin", 20, false);
            Core.EnsureComplete(2089);
            Bot.Wait.ForQuestComplete(2089);
            Bot.Wait.ForPickup("Dark Spirit Orb");
            return;
        }
        else
        {
            Core.RegisterQuests(2089);
            Core.KillMonster("maul", "r7", "left", "Shelleton", "Dark Spirit Orb", quant, false);
            Bot.Wait.ForQuestComplete(2089);
            Bot.Wait.ForPickup("Dark Spirit Orb");
            Core.CancelRegisteredQuests();
            Core.AbandonQuest(2089);
            return;
        }
    }

    public void DSO(int quant = 10500)
    {
        if (Core.CheckInventory("Dark Spirit Orb", quant))
            return;

        Core.AddDrop("Dark Spirit Orb", "Shadow Creeper Enchant", "Shadow Serpent Scythe", "Dark Energy");
        Core.EquipClass(ClassType.Farm);
        Core.FarmingLogger("Dark Spirit Orb", quant);
        Core.RegisterQuests(2065);
        while (!Bot.ShouldExit && (!Core.CheckInventory("Dark Spirit Orb", quant)))
        {
            Core.HuntMonster("bludrut2", "Shadow Creeper", "Shadow Creeper Enchant", isTemp: false);
            Core.HuntMonster("bludrut4", "Shadow Serpent", "Shadow Serpent Scythe", isTemp: false);
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
        Core.FarmingLogger("DoomSquire Weapon Kit", quant);
        Core.AddDrop("DoomSquire Weapon Kit");
        Core.RegisterQuests(2144);
        while (!Bot.ShouldExit && (!Core.CheckInventory("DoomSquire Weapon Kit", quant)))
        {
            if (Core.CheckInventory(319))
                Core.BuyItem("swordhaven", 179, "Iron Hammer");
            else Core.HuntMonster("battleundera", "Skeletal Warrior", "Iron Hammer", isTemp: false);

            Core.HuntMonster("sandcastle", "War Mummy", "War Mummy Wrap", isTemp: false, log: false);
            Core.HuntMonster("noobshire", "Horc Noob", "Noob Blade Oil", log: false);
            Core.HuntMonster("farm", "Scarecrow", "Burlap Cloth", 4, log: false);

            Core.HuntMonster("lair", "Bronze Draconian", "Bronze Brush", log: false);
            Core.HuntMonster("bludrut", "Rock Elemental", "Elemental Stone Sharpener", log: false);
            Core.HuntMonster("nulgath", "Dark Makai", "Dark Makai Lacquer Finish", log: false);

            Bot.Wait.ForPickup("DoomSquire Weapon Kit");
        }
        Core.CancelRegisteredQuests();
    }

    public void DoomSoldierWK(int quant = 1)
    {
        if (Core.CheckInventory("DoomSoldier Weapon Kit", quant))
            return;

        Core.FarmingLogger("DoomSoldier Weapon Kit", quant);
        Core.RegisterQuests(2164);
        Core.AddDrop("DoomSoldier Weapon Kit");
        while (!Bot.ShouldExit && (!Core.CheckInventory("DoomSoldier Weapon Kit", quant)))
        {
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("cornelis", "Stone Golem", "Stone Hammer", isTemp: false);
            Core.HuntMonster("hachiko", "Dai Tengu", "Superior Blade Oil");
            Core.HuntMonster("vordredboss", "Shadow Vordred", "Shadow Lacquer Finish");
            Core.HuntMonster("anders", "Copper Sky Pirate", "Copper Awl");
            Core.HuntMonster("necrocavern", "Shadow Imp", "Shadowstone Sharpener");

            Core.EquipClass(ClassType.Farm);
            Core.KillMonster("lycan", "r4", "Left", "Chaos Vampire Knight", "Silver Brush", log: false);
            Core.KillMonster("sandport", "r3", "Right", "Tomb Robber", "Leather Case", log: false);
            Core.KillMonster("pines", "Path1", "Left", "LeatherWing", "LeatherWing Hide", 10, log: false);

            Bot.Wait.ForPickup("DoomSoldier Weapon Kit");
        }
        Core.CancelRegisteredQuests();
    }

    public void DoomKnightWK(string item = "DoomKnight Weapon Kit", int quant = 1)
    {
        if (Core.CheckInventory(item, quant))
            return;

        Core.AddDrop("DoomKnight Weapon Kit", "Dark Spirit Orb", "Corrupt Spirit Orb", "Ominous Aura", "Grumpy Warhammer");
        Core.EquipClass(ClassType.Solo);
        Core.FarmingLogger(item, quant);
        Core.RegisterQuests(2165);
        while (!Bot.ShouldExit && (!Core.CheckInventory(item, quant)))
        {
            Core.KillMonster("boxes", "Boss", "Left", "Sneeviltron", "Grumpy Warhammer", isTemp: false);
            Core.KillKitsune("No. 1337 Blade Oil");
            Core.KillMonster("sandcastle", "r7", "Left", "Chaos Sphinx", "Gold Brush");
            Core.KillMonster("crashsite", "Boss", "Left", "ProtoSartorium", "Non-abrasive Power Powder");
            Core.KillMonster("necrocavern", "r13", "Left", "Shadow Dragon", "ShadowDragon Hide", 3);
            Core.KillMonster("dragonplane", "r9", "Left", "Moganth", "Moganth's Stone Sharpener");
            Core.KillMonster("akiba", "cave4boss", "Left", "Shadow Nukemichi", "Doom Lacquer Finish");
            Core.KillMonster("dreamnexus", "r7", "Left", "Dark Wyvern", "Dark Wyvern Hide Travel Case");

            Bot.Wait.ForPickup(item);
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

            if (!Core.CheckInventory(12476))
            {
                if (!Core.CheckInventory("Accursed Arsenic"))
                {
                    Core.FarmingLogger("Accursed Arsenic");
                    Core.EnsureAccept(2110);
                    Core.HuntMonster("bludrut4", "Shadow Serpent", "Dark Energy", 26, false);
                    Daily.HardCoreMetals(new[] { "Arsenic" });
                    if (!Core.CheckInventory("Arsenic"))
                        Core.Logger("Can't complete Accursed Arsenic Hex (Missing Arsenic).\n" +
                            "This requires a daily, please run the bot again after the daily reset has occurred.", messageBox: true, stopBot: true);
                    DSO(6);
                    Core.HuntMonster("arcangrove", "Seed Spitter", "Deadly Knightshade", 16);
                    Core.EnsureComplete(2110);
                }
                int CSOQuantity = 105 - (Bot.Inventory.GetQuantity("Ominous Aura") * 50);
                if ((CSOQuantity * 100) > Bot.Inventory.GetQuantity("Dark Spirit Orb"))
                    FarmDSO(CSOQuantity * 100);
                DoomMerge("Corrupt Spirit Orb", CSOQuantity);
                DoomMerge("Ominous Aura", 2);
                Core.BuyItem("dwarfhold", 434, 12476, shopItemID: 1198);
            }

            if (!Story.QuestProgression(2137))
            {
                Core.Logger("Unlocking Weapon Kit quests");
                Core.EnsureAccept(2137);
                Core.KillMonster("dwarfhold", "Enter", "Spawn", "Albino Bat", "Forge Key", isTemp: false, log: false);
                Core.EnsureComplete(2137);
            }
            Core.FarmingLogger("Daggers of Destruction");
            DoomSquireWK();
            FarmDSO(50);
            DoomMerge("Daggers of Destruction");
        }

        if (Core.CheckInventory("Daggers of Destruction"))
        {
            Core.FarmingLogger("Shadow Daggers of Destruction");
            DoomSoldierWK();
            DoomKnightWK("Ominous Aura");
            DoomMerge("Shadow Daggers of Destruction");
        }

        if (Core.CheckInventory("Shadow Daggers of Destruction"))
        {
            Core.FarmingLogger("Necrotic Daggers of Destruction");
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
                    Core.FarmingLogger("Calamitous Chromium", 1);
                    Core.AddDrop("Calamitous Chromium");
                    Core.EnsureAccept(2112);
                    Core.HuntMonster("bludrut4", "Shadow Serpent", "Dark Energy", 26, false);
                    Daily.HardCoreMetals(new[] { "Chromium" });
                    if (!Core.CheckInventory("Chromium"))
                        Core.Logger("Can't complete Calamitous Chromium Hex (Missing Chromium).\n" +
                            "This requires a daily, please run the bot again after the daily reset has occurred.", messageBox: true, stopBot: true);
                    DSO(6);
                    Core.HuntMonster("arcangrove", "Seed Spitter", "Deadly Knightshade", 16);
                    Core.EnsureComplete(2112);
                    Bot.Wait.ForPickup("Calamitous Chromium");
                }
                Core.FarmingLogger("Calamitous Chromium of Doom");
                PinpointDaggers();
                DoomKnightWK("Corrupt Spirit Orb", 5);
                Core.BuyItem("dwarfhold", 434, "Calamitous Chromium of Doom");
            }
            if (!Core.CheckInventory("Diabolical Aura"))
            {
                PinpointDaggers(25);
                DoomMerge("Diabolical Aura");
            }
            Core.FarmingLogger("Broadsword of Bane");
            DoomKnightWK("Corrupt Spirit Orb");
            DoomKnightWK("Dark Spirit Orb", 20);
            DoomSquireWK();
            DoomMerge("Broadsword of Bane");
        }

        if (Core.CheckInventory("Broadsword of Bane"))
        {
            Core.FarmingLogger("Shadow Broadsword of Bane");
            DoomKnightWK("Corrupt Spirit Orb");
            PinpointDaggers(1);
            DoomSoldierWK();
            DoomMerge("Shadow Broadsword of Bane");
        }

        if (Core.CheckInventory("Shadow Broadsword of Bane"))
        {
            Core.FarmingLogger("Necrotic Broadsword of Bane");
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
                    Core.FarmingLogger("Reprehensible Rhodium");
                    Core.AddDrop("Reprehensible Rhodium");
                    Core.EnsureAccept(2114);
                    Core.HuntMonster("bludrut4", "Shadow Serpent", "Dark Energy", 26, false);
                    Daily.HardCoreMetals(new[] { "Rhodium" });
                    if (!Core.CheckInventory("Rhodium"))
                        Core.Logger("Can't complete Reprehensible Rhodium Hex (Missing Rhodium).\n" +
                            "This requires a daily, please run the bot again after the daily reset has occurred.", messageBox: true, stopBot: true);
                    DSO(6);
                    Core.HuntMonster("arcangrove", "Seed Spitter", "Deadly Knightshade", 16);
                    Core.EnsureComplete(2114);
                    Bot.Wait.ForPickup("Reprehensible Rhodium");
                }
                Core.FarmingLogger("Reprehensible Rhodium of Doom");
                PinpointDaggers();
                DoomKnightWK("Corrupt Spirit Orb", 5);
                Core.BuyItem("dwarfhold", 434, "Reprehensible Rhodium of Doom");
            }
            Core.FarmingLogger("Bow to the Shadows");
            DoomSquireWK();
            DoomKnightWK("Corrupt Spirit Orb");
            DoomKnightWK("Dark Spirit Orb", 13);
            PinpointBroadsword();
            Farm.BattleUnderB("Undead Energy", 17);
            DoomMerge("Bow to the Shadows");
        }

        if (Core.CheckInventory("Bow to the Shadows"))
        {
            Core.FarmingLogger("ShadowBow of the Shadows");
            DoomSoldierWK();
            DoomKnightWK("Corrupt Spirit Orb");
            Core.HuntMonster("bludrut4", "Shadow Serpent", "Dark Energy", 50, false);
            DoomMerge("ShadowBow of the Shadows");
        }

        if (Core.CheckInventory("ShadowBow of the Shadows"))
        {
            Core.FarmingLogger("Necrotic Bow of the Shadow");
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

        Core.Logger(Core.CheckInventory("Doom Aura") ? "Doom Aura found." : "Farming for Doom Aura");

        PinpointthePieces(2181, new[] { "Doom Aura" }, new[] { 1 });

        if (!Core.CheckInventory("Experimental Dark Item"))
        {
            Core.AddDrop("Experimental Dark Item");
            PinpointBow(50, 0);
            Core.BuyItem("shadowfall", 100, "DoomKnight Hood");
            Core.ChainComplete(2069);
            Bot.Wait.ForPickup("Experimental Dark Item");
        }
        DoomKnightWK();
        Core.AddDrop("Sepulchure's DoomKnight Armor");
        Core.EnsureAccept(2187);
        Core.HuntMonster("ruins", "Dark Elemental", "Heart of Darkness");
        Core.EnsureComplete(2187);
        Bot.Wait.ForPickup("Sepulchure's DoomKnight Armor");
    }

    public void PinpointDaggers(int quant = 5)
    {
        if (Core.CheckInventory("Ominous Aura", quant))
            return;

        if (!Core.CheckInventory("Necrotic Daggers of Destruction", 1)) // Assuming third argument is toInv
            NecroticDaggers();

        PinpointthePieces(2181, new[] { "Ominous Aura" }, new[] { quant });
    }

    public void PinpointBroadsword(int quant = 1)
    {
        if (Core.CheckInventory("Diabolical Aura", quant))
            return;

        if (!Core.CheckInventory("Necrotic Broadsword of Bane", 1, false))
            NecroticBroadsword();

        PinpointthePieces(2183, new[] { "Diabolical Aura" }, new[] { quant });
    }

    public void PinpointBow(int quantDSO, int quantCSO)
    {
        if (Core.CheckInventory("Dark Spirit Orb", quantDSO) && Core.CheckInventory("Corrupt Spirit Orb", quantCSO))
            return;

        if (!Core.CheckInventory("Necrotic Bow of the Shadow", 1, false)) // Assuming third argument is toInv
            NecroticBow();

        Core.EquipClass(ClassType.Farm);
        Core.FarmingLogger("Dark Spirit Orb", quantDSO);
        Core.FarmingLogger("Corrupt Spirit Orb", quantCSO);

        // Process each item individually
        PinpointthePieces(2186, new string[] { "Dark Spirit Orb", "Corrupt Spirit Orb" }, new int[] { quantDSO, quantCSO });
    }

    public void PinpointthePieces(int quest, string[]? items = null, int[]? quants = null)
    {
        if (items == null || quants == null || items.Length != quants.Length)
            return;

        Core.AddDrop("Dark Energy", "Dark Spirit Orb", "Corrupt Spirit Orb", "Ominous Aura", "Diabolical Aura", "Doom Aura");

        Core.EquipClass(ClassType.Farm);

        // Process each item individually
        for (int i = 0; i < items.Length; i++)
        {
            Core.FarmingLogger(items[i], quants[i]);
            while (!Bot.ShouldExit && !Core.CheckInventory(items[i], quants[i]))
            {
                Core.EnsureAccept(quest);
                Core.KillMonster("lycan", "r4", "Left", "*", "DoomKnight Armor Piece", 10, log: false);
                Core.EnsureComplete(quest);
                Bot.Wait.ForPickup(items[i]);
            }
        }

        Core.CancelRegisteredQuests();
        Core.JumpWait();
    }

}

public enum SDKAQuest
{
    APennyforYourFoughts,
    DarkSpiritOrbs,
}
