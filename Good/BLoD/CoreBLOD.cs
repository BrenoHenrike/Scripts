/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/BattleUnder.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Shops;
using Skua.Core.Models.Items;
using Skua.Core.Utils;

public class CoreBLOD
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreDailies Daily = new();
    private CoreStory Story = new();
    private BattleUnder BattleUnder = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.RunCore();
    }

    public string[] BLoDItems =
    {
        "Blinding Light of Destiny",
        "Get Your Blinding Light of Destiny",
        // Coppers
        "Copper",
        "Celestial Copper",
        "Celestial Copper of Destiny",
        // Maces
        "Mace of Destiny",
        "Bright Mace of Destiny",
        "Blinding Mace of Destiny",
        // Silvers
        "Silver",
        "Sanctified Silver",
        "Sanctified Silver of Destiny",
        // Bows
        "Bow of Destiny",
        "Bright Bow of Destiny",
        "Blinding Bow of Destiny",
        // Bariums
        "Barium",
        "Blessed Barium",
        "Blessed Barium of Destiny",
        // Blades
        "Blade of Destiny",
        "Bright Blade of Destiny",
        "Blinding Blade of Destiny",
        // Weapon Kits
        "Basic Weapon Kit",
        "Advanced Weapon Kit",
        "Ultimate Weapon Kit",
        // Merge misc.
        "Bone Dust",
        "Undead Essence",
        "Undead Energy",
        "Blinding Light Fragments",
        "Spirit Orb",
        "Loyal Spirit Orb",
        "Bright Aura",
        "Brilliant Aura",
        "Blinding Aura",
    };

    public void BlindingLightOfDestiny(BLODMethod method = BLODMethod.Optimized)
    {
        if (Core.CheckInventory("Blinding Light of Destiny"))
            return;

        if (!Core.CheckInventory(40187)) // Get your Blinding Light of Destiny
        {
            Bot.Drops.Add(BLoDItems);

            UnlockMineCrafting();
            switch ((int)method)
            {
                case 0: // Fewest Dailies
                    GetBlindingWeapon(WeaponOfDestiny.Blade);
                    break;
                case 1: // Optimized
                    GetBlindingWeapon(WeaponOfDestiny.Daggers);
                    GetBlindingWeapon(WeaponOfDestiny.Mace);
                    break;
                case 2: // Fewest Hours
                    GetBlindingWeapon(WeaponOfDestiny.Mace);
                    GetBlindingWeapon(WeaponOfDestiny.Bow);
                    GetBlindingWeapon(WeaponOfDestiny.Blade);
                    break;
            }

            BrilliantAura(75);
            BrightAura(125);
            LoyalSpiritOrb(250);
            SpiritOrb(500);
            BlindingAura(1);

            UltimateWK();

            Core.ChainComplete(2180);
            Bot.Drops.Pickup(40187);
        }
        Core.BuyItem(Bot.Map.Name, 1415, "Blinding Light of Destiny");
        Core.ToBank(40187);
    }

    public void UnlockMineCrafting()
    {
        if (Core.isCompletedBefore(2084))
            return;

        Story.PreLoad(this);

        // 2066 - Reforging the Blinding Light
        Story.BuyQuest(2066, "doomwood", 276, "Blinding Light of Destiny Handle");

        // 2067 - Secret Order of Undead Slayers
        Story.BuyQuest(2067, "doomwood", 276, "Bonegrinder Medal");

        // 2082 - Essential Essences
        if (!Story.QuestProgression(2082))
        {
            Core.Logger("Doing Quest: [2082] \"Essential Essences\"");
            Core.EnsureAccept(2082);
            Farm.BattleUnderB("Undead Essence", 25);
            Story.QuestComplete(2082);
        }
        Story.KillQuest(2082, "battleunderb", "Undead Warrior");

        // 2083 - Bust some Dust
        Story.KillQuest(2083, "battleunderb", "Undead Warrior");

        // 2084 - A Loyal Follower
        if (!Story.QuestProgression(2084))
        {
            Core.Logger("Doing Quest: [2084] \"A Loyal Follower\"");
            Core.EnsureAccept(2084);
            SpiritOrb(100);
            Core.HuntMonster("timevoid", "Ephemerite", "Celestial Compass");
            Story.QuestComplete(2084);
        }
    }

    #region Materials

    public void SpiritOrb(int quant)
    {
        if (Core.CheckInventory("Spirit Orb", quant))
            return;

        farmFindingFrag(WeaponOfDestiny.Blade, "Spirit Orb", quant);
        farmFindingFrag(WeaponOfDestiny.Broadsword, "Spirit Orb", quant);

        // Default
        SoulSearching("Spirit Orb", quant);
    }

    public void LoyalSpiritOrb(int quant)
    {
        if (Core.CheckInventory("Loyal Spirit Orb", quant))
            return;

        farmFindingFrag(WeaponOfDestiny.Blade, "Loyal Spirit Orb", quant);
        farmFindingFrag(WeaponOfDestiny.Daggers, "Loyal Spirit Orb", quant);
        farmFindingFrag(WeaponOfDestiny.Scythe, "Loyal Spirit Orb", quant);
        farmUltimateWK("Loyal Spirit Orb", quant);

        // Default
        if (!Core.CheckInventory("Loyal Spirit Orb", quant))
        {
            Core.FarmingLogger("Loyal Spirit Orb", quant);
            SpiritOrb(100 * quant);
            LightMerge("Loyal Spirit Orb", quant);
        }
    }

    public void BrightAura(int quant)
    {
        if (Core.CheckInventory("Bright Aura", quant))
            return;

        farmFindingFrag(WeaponOfDestiny.Bow, "Bright Aura", quant);
        farmFindingFrag(WeaponOfDestiny.Broadsword, "Bright Aura", quant);
        farmFindingFrag(WeaponOfDestiny.Scythe, "Bright Aura", quant);
        farmUltimateWK("Bright Aura", quant);

        // Default
        if (!Core.CheckInventory("Bright Aura", quant))
        {
            Core.FarmingLogger("Bright Aura", quant);
            LoyalSpiritOrb(50 * quant);
            LightMerge("Bright Aura", quant);
        }
    }

    public void BrilliantAura(int quant)
    {
        if (Core.CheckInventory("Brilliant Aura", quant))
            return;

        farmFindingFrag(WeaponOfDestiny.Mace, "Brilliant Aura", quant);

        // Default
        if (!Core.CheckInventory("Brilliant Aura", quant))
        {
            Core.FarmingLogger("Brilliant Aura", quant);
            BrightAura(25 * quant);
            LightMerge("Brilliant Aura", quant);
        }
    }

    public void BlindingAura(int quant)
    {
        if (Core.CheckInventory("Blinding Aura", quant))
            return;

        farmFindingFrag(WeaponOfDestiny.Scythe, "Blinding Aura", quant);

        // Default (any of these)
        farmFindingFrag(WeaponOfDestiny.Blade, "Blinding Aura", quant);
        farmFindingFrag(WeaponOfDestiny.Broadsword, "Blinding Aura", quant);
        farmFindingFrag(WeaponOfDestiny.Bow, "Blinding Aura", quant);
        farmFindingFrag(WeaponOfDestiny.Mace, "Blinding Aura", quant);
        farmFindingFrag(WeaponOfDestiny.Daggers, "Blinding Aura", quant);
    }

    public void SoulSearching(string item, int quant, bool farmSpiritOrbs = true)
    {
        if (Core.CheckInventory(item, quant))
            return;

        if (!Bot.Quests.IsUnlocked(939))
            BattleUnder.BattleUnderC();

        Core.EquipClass(ClassType.Solo);
        Core.FarmingLogger(item, quant);

        Core.AddDrop("Cavern Celestite", "Undead Essence");
        if (farmSpiritOrbs)
        {
            Core.RegisterQuests(939, 2082, 2083); // + Bone Some Dust & Essential Essences
            Core.AddDrop("Bone Dust", "Undead Energy", "Spirit Orb");
        }
        else Core.RegisterQuests(939);

        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
        {
            Core.HuntMonster("battleundera", "Bone Terror", "Bone Terror Soul", log: false);
            Core.HuntMonster("battleunderb", "Undead Champion", "Undead Champion Soul", log: false);
            Core.HuntMonster("battleunderc", "Crystalized Jellyfish", "Jellyfish Soul", log: false);

            Bot.Wait.ForPickup(item);
        }
        Core.CancelRegisteredQuests();
    }

    //Unused, here for archiving purposes I guess... ~Exe
    public void BoneSomeDust(int quant = 10500)
    {
        if (Core.CheckInventory("Spirit Orb", quant))
            return;

        Core.AddDrop("Bone Dust", "Undead Essence", "Undead Energy", "Spirit Orb");
        Core.EquipClass(ClassType.Farm);
        Core.FarmingLogger("Spirit Orb", quant);

        Core.RegisterQuests(2082, 2083);
        while (!Bot.ShouldExit && !Core.CheckInventory("Spirit Orb", quant))
            Core.KillMonster("battleunderb", "Enter", "Spawn", "Skeleton Warrior", log: false);
        Core.CancelRegisteredQuests();
    }

    public void FindingFragments(WeaponOfDestiny weapon, string item, int quant = 1)
    {
        if (Core.CheckInventory(item, quant))
            return;

        GetBlindingWeapon(weapon);

        int quest = (weapon) switch
        {
            WeaponOfDestiny.Bow => 2174,
            WeaponOfDestiny.Daggers => 2175,
            WeaponOfDestiny.Mace => 2176,
            WeaponOfDestiny.Scythe => 2177,
            WeaponOfDestiny.Broadsword => 2178,
            WeaponOfDestiny.Blade => 2179,
            _ => 0,
        };

        Core.AddDrop(Core.QuestRewards(quest).Append("Blinding Light Fragments").ToArray());
        Core.EquipClass(ClassType.Farm);
        Core.FarmingLogger(item, quant);

        Core.RegisterQuests(quest);
        Core.KillMonster("battleunderb", "Enter", "Spawn", "*", item, quant, log: false, isTemp: false);
        Core.CancelRegisteredQuests();
    }

    private void farmFindingFrag(WeaponOfDestiny weapon, string item, int quant)
    {
        if (!Core.CheckInventory(item, quant) && Core.isCompletedBefore(2163) &&
            (Core.CheckInventory($"Blinding {weapon} of Destiny") || Core.CheckInventory(40187))) // Basically means you can buy it from the final shop
            FindingFragments(weapon, item, quant);
    }

    private void farmUltimateWK(string item, int quant)
    {
        if (!Core.CheckInventory(item, quant) && Bot.Quests.IsUnlocked(2163))
            UltimateWK(item, quant);
    }

    #endregion

    #region Weapon Kits

    public void BasicWK(int quant = 1)
    {
        if (Core.CheckInventory("Basic Weapon Kit", quant))
            return;

        Core.EquipClass(ClassType.Farm);
        Core.FarmingLogger("Basic Wepon Kit", quant);
        Core.AddDrop("Basic Weapon Kit");

        Core.RegisterQuests(2136);
        while (!Bot.ShouldExit && !Core.CheckInventory("Basic Weapon Kit", quant))
        {
            Core.KillMonster("forest", "Forest3", "Left", "*", "Zardman's StoneHammer", 1, false);
            Core.KillMonster("noobshire", "North", "Left", "Horc Noob", "Noob Blade Oil");
            Core.KillMonster("farm", "Crop1", "Right", "Scarecrow", "Burlap Cloth", 4);

            Bot.Quests.UpdateQuest(4614);
            Core.HuntMonster("pyramid", "Mummy", "Triple Ply Mummy Wrap", 7);
            Core.HuntMonster("pyramid", "Golden Scarab", "Golden Lacquer Finish");
            Core.HuntMonster("lair", "Bronze Draconian", "Bronze Brush");
            Core.HuntMonster("bloodtusk", "Rock", "Rocky Stone Sharpener");

            Bot.Wait.ForPickup("Basic Weapon Kit");
        }
        Core.CancelRegisteredQuests();
    }

    public void AdvancedWK(int quant = 1)
    {
        if (Core.CheckInventory("Advanced Weapon Kit", quant))
            return;

        Core.FarmingLogger("Advanced Weapon Kit", quant);
        Core.AddDrop("Advanced Weapon Kit");

        Core.RegisterQuests(2162);
        while (!Bot.ShouldExit && !Core.CheckInventory("Advanced Weapon Kit", quant))
        {
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("hachiko", "Dai Tengu", "Superior Blade Oil");
            Core.HuntMonster("airstorm", "Lightning Ball", "Shining Lacquer Finish");
            Core.HuntMonster("faerie", "Cyclops Warlord", "Brass Awl");
            Core.HuntMonster("darkoviaforest", "Lich of the Stone", "Slate Stone Sharpener");

            Core.EquipClass(ClassType.Farm);
            Core.KillMonster("safiria", "c3", "Left", "Chaos Lycan", "WolfClaw Hammer", 1, false);
            Core.KillMonster("lycan", "r4", "Left", "Chaos Vampire Knight", "Silver Brush");
            Core.KillMonster("sandport", "r3", "Right", "Tomb Robber", "Leather Case");
            Core.KillMonster("pines", "Path1", "Left", "Leatherwing", "Leatherwing Hide", 10);

            Bot.Wait.ForPickup("Advanced Weapon Kit");
        }
        Core.CancelRegisteredQuests();
    }

    public void UltimateWK(string item = "Ultimate Weapon Kit", int quant = 1)
    {
        if (Core.CheckInventory(item, quant))
            return;

        Core.AddDrop("Ultimate Weapon Kit", "Blinding Light Fragments", "Bright Aura", "Spirit Orb", "Loyal Spirit Orb");
        Core.FarmingLogger(item, quant);

        Core.RegisterQuests(2163);
        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
        {
            Core.EquipClass(ClassType.Farm);
            Core.KillMonster("dragonplane", "r2", "Right", "Earth Elemental", "Great Ornate Warhammer", 1, false);

            Core.EquipClass(ClassType.Solo);
            Core.KillMonster("greendragon", "Boss", "Left", "Greenguard Dragon", "Greenguard Dragon Hide", 3);
            Core.KillMonster("sandcastle", "r7", "Left", "Chaos Sphinx", "Gold Brush", publicRoom: true);
            Core.KillMonster("crashsite", "Boss", "Left", "ProtoSartorium", "Non-abrasive Power Powder");
            Core.KillMonster("kitsune", "Boss", "Left", "Kitsune", "No. 1337 Blade Oil", publicRoom: true);
            Core.KillMonster("citadel", "m14", "Left", "Grand Inquisitor", "Blinding Lacquer Finish");
            Core.HuntMonster("djinn", "Harpy", "Suede Travel Case");
            Core.KillMonster("roc", "Enter", "Spawn", "Rock Roc", "Sharp Stone Sharpener");

            Bot.Wait.ForPickup(item);
        }
        Core.CancelRegisteredQuests();
    }

    #endregion

    #region Weapons

    public void GetBlindingWeapon(WeaponOfDestiny weapon)
    {
        string weaponName = $"Blinding {weapon} of Destiny";
        if (Core.CheckInventory(weaponName))
            return;

        // Bypass any farms if you already farmed BLOD in the past
        if (Core.CheckInventory(40187)) // Get Your Blinding Light of Destiny
        {
            Core.BuyItem(Bot.Map.Name, 1415, weaponName);
            Core.ToBank(40187);
            return;
        }

        GetBrightWeapon(weapon);
        UltimateWK();
        LightMerge(weaponName);
    }

    public void GetBrightWeapon(WeaponOfDestiny weapon)
    {
        string weaponName = $"Bright {weapon} of Destiny";
        if (Core.CheckInventory(weaponName))
            return;

        GetBaseWeapon(weapon);

        List<ItemBase> weaponReqs = (LightMergeShopItems ??= Core.GetShopItems("necropolis", 422)).First(item => item.Name == weaponName).Requirements;

        getMergeRequirements(weaponReqs, weaponReqs.First(item => item.Name == weapon + " of Destiny").ID);
        LightMerge(weaponName);
    }

    public void GetBaseWeapon(WeaponOfDestiny weapon)
    {
        string weaponName = weapon + " of Destiny";
        if (Core.CheckInventory(weaponName))
            return;

        List<ItemBase> weaponReqs = (LightMergeShopItems ??= Core.GetShopItems("necropolis", 422)).First(item => item.Name == weaponName).Requirements;
        ItemBase metal = weaponReqs.First(req => req.Name.EndsWith("of Destiny"));
        UpgradeMetal((MineCraftingMetalsEnum)Enum.Parse(typeof(MineCraftingMetalsEnum), metal.Name.Split(' ')[1]));

        getMergeRequirements(weaponReqs, metal.ID);
        LightMerge(weaponName);
    }

    // Dynamic way of getting all the requierments via the shop info, which will work for different quants or combinations
    private void getMergeRequirements(List<ItemBase> requirements, params int[] exceptions)
    {
        foreach (ItemBase req in requirements.Where(r => !exceptions.Contains(r.ID)))
        {
            switch (req.ID)
            {
                case 12503: // Basic Weapon Kit
                    BasicWK(req.Quantity);
                    break;
                case 12544: // Advanced Weapon Kit
                    AdvancedWK(req.Quantity);
                    break;
                case 12184: // Spirit Orb
                    SpiritOrb(req.Quantity);
                    break;
                case 12311: // Loyal Spirit Orb
                    LoyalSpiritOrb(req.Quantity);
                    break;
                case 6537: // Brilliant Aura (why is the ID so far away from the others)
                    BrilliantAura(req.Quantity);
                    break;
                case 6535: // Bright Aura
                    BrightAura(req.Quantity);
                    break;
                case 11285: // Undead Energy
                    Farm.BattleUnderB("Undead Energy", req.Quantity);
                    break;
            }
        }
    }

    public void UpgradeMetal(MineCraftingMetalsEnum metal)
    {
        string fullMetalName = String.Empty;
        int upgradeMetalQuest = 0;
        int forgeKeyQuest = 0;
        switch (metal)
        {
            case MineCraftingMetalsEnum.Aluminum:
                fullMetalName = "Almighty Aluminum of Destiny";
                upgradeMetalQuest = 2103;
                forgeKeyQuest = 2129;
                break;
            case MineCraftingMetalsEnum.Barium:
                fullMetalName = "Blessed Barium of Destiny";
                upgradeMetalQuest = 2104;
                forgeKeyQuest = 2130;
                break;
            case MineCraftingMetalsEnum.Gold:
                fullMetalName = "Glorious Gold of Destiny";
                upgradeMetalQuest = 2105;
                forgeKeyQuest = 2131;
                break;
            case MineCraftingMetalsEnum.Iron:
                fullMetalName = "Immortal Iron of Destiny";
                upgradeMetalQuest = 2106;
                forgeKeyQuest = 2132;
                break;
            case MineCraftingMetalsEnum.Copper:
                fullMetalName = "Celestial Copper of Destiny";
                upgradeMetalQuest = 2107;
                forgeKeyQuest = 2133;
                break;
            case MineCraftingMetalsEnum.Silver:
                fullMetalName = "Sanctified Silver of Destiny";
                upgradeMetalQuest = 2108;
                forgeKeyQuest = 2134;
                break;
            case MineCraftingMetalsEnum.Platinum:
                fullMetalName = "Pious Platinum of Destiny";
                upgradeMetalQuest = 2109;
                forgeKeyQuest = 2135;
                break;
        };
        if (Core.CheckInventory(fullMetalName))
            return;

        string upgradeMetalName = fullMetalName.Split(' ')[..2].Join(' ');
        Core.FarmingLogger(fullMetalName, 1);

        // Getting the partially upgraded metal
        if (!Core.CheckInventory(upgradeMetalName))
        {
            Core.AddDrop(upgradeMetalName);
            Core.FarmingLogger(upgradeMetalName, 1);
            Core.EnsureAccept(upgradeMetalQuest);

            if (!Core.CheckInventory((int)metal))
                Daily.MineCrafting(new[] { metal.ToString() });
            if (!Core.CheckInventory((int)metal))
                Core.Logger($"Can't complete {fullMetalName.Split(' ')[..2].Join(' ')} Enchantment (missing {metal}).\n" +
                            "This requiers a daily, please run the bot again after the daily reset has occurred.", messageBox: true, stopBot: true);

            Farm.BattleUnderB("Undead Energy", 25);
            SpiritOrb(5);
            Core.HuntMonster("arcangrove", "Seed Spitter", "Paladaffodil", 25);

            Core.EnsureComplete(upgradeMetalQuest);
            Bot.Wait.ForPickup(upgradeMetalName);
        }


        // Unlocking "Forge Metal"-shop [434] and "Basic Weapon Kit Construction"-quest [2136]
        if (!Core.isCompletedBefore(forgeKeyQuest))
        {
            Core.AddDrop(fullMetalName);
            Core.EnsureAccept(forgeKeyQuest);

            // Getting the fully upgraded metal to complete forgeKeyQuest
            BrightAura(2);
            LoyalSpiritOrb(5);
            Core.BuyItem("dwarfhold", 434, fullMetalName);

            //Getting the Forge key for the Quest
            Core.HuntMonster("dwarfhold", "Albino Bat", "Forge Key", isTemp: false);

            Core.EnsureComplete(forgeKeyQuest);
            Bot.Wait.ForPickup(fullMetalName);
        }
    }

    #endregion

    private List<ShopItem>? LightMergeShopItems = null;

    private void LightMerge(string item, int quant = 1)
        => Core.BuyItem("necropolis", 422, item, quant);
}

public enum WeaponOfDestiny
{
    Daggers,
    Bow,
    Mace,
    Scythe,
    Broadsword,
    Blade,
}

public enum BLODMethod
{
    Fewest_Dailies,
    Optimized,
    Fewest_Hours,
}