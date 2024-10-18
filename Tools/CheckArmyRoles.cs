/*
name: Check Roles
description: This script will give a popup telling you a bunch of information regarding your account.
tags: tool, evaluate, account, chrono, heromart, beta, founder, badges, enhancements, rare, seasonal
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using System.Text;

public class CheckArmyRoles
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new();
    private CoreStory Story = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Evaluate();
    }

    public void Evaluate()
    {
        while (!Bot.ShouldExit && !Bot.Player.Loaded)
            Core.Sleep();

        if (Bot.House.Items.Any(h => h.Equipped))
        {
            Bot.Send.Packet($"%xt%zm%house%1%{Core.Username()}%");
            Bot.Wait.ForMapLoad("house");
        }

        Bot.Bank.Open();

        // Load Forge Enhancement Quest + some role quests
        Bot.Quests.Load(forgeEnhIDs.Concat(new[] { 793, 2937, 8042 }).ToArray());
        Bot.Wait.ForTrue(() => Bot.Bank.Items.Any(), 20);

        int dmgAll51Items = 0;
        (RacialGearBoost, bool)[] racial75Items = racialGears.Select(x => (x, false)).ToArray();

        Core.Logger("🗂️ Checking Player Inventory Items...");
        processItems("world.myAvatar.items");

        Core.Logger("🏦 Reviewing Player Bank Items...");
        processItems("world.bankinfo.items");

        Core.Logger("🏠 Evaluating Player House Items...");
        processItems("world.myAvatar.houseitems");

        Core.Logger("📝 Compiling and formatting all data for the final output...");

        #region OutPut Generator
        // The actual output
        Bot.ShowMessageBox(
                        $"Evaluation ID: {GenerateEvaluationID()}\n" +
                        $"Extra Secutity: Account Item Count: {Bot.Inventory.Items.Concat(Bot.Bank.Items).Concat(Bot.House.Items).Count()}\n" +
                        $"Ｅｎｈａｎｃｅｍｅｎｔｓ\n" +
                        $"(Victor of War) Valiance:\t\t\t\t{Checkbox(Core.isCompletedBefore(8741))}\n" +
                        $"(Conductor of War) Arcana's Concerto:\t\t{Checkbox(Core.isCompletedBefore(8742))}\n" +
                        $"(Deliverance of War) Elysium:\t\t\t{Checkbox(Core.isCompletedBefore(8821))}\n" +
                        $"(Reflectionist of War) Examen:\t\t\t{Checkbox(Core.isCompletedBefore(8825))}\n" +
                        $"(Penitent of War) Pentience:\t\t\t{Checkbox(Core.isCompletedBefore(8822))}\n" +
                        $"(Miltonious of War) Ravenous:\t\t\t{Checkbox(Core.isCompletedBefore(9560))}\n" +
                        $"(Shadow of War) Dauntless:\t\t\t{Checkbox(Core.isCompletedBefore(9172))}\n" +

                        $"Ｃｌａｓｓｅｓ\n" +
                        "(Avenger of War) " + importantItemCheckbox(3, "Chaos Avenger") +
                        "(ArchMage of War) " + importantItemCheckbox(3, "ArchMage") +
                        "(Revenant of War) " + importantItemCheckbox(3, "Legion Revenant") +
                        "(Highlord of War) " + importantItemCheckbox(3, "Void Highlord", "Void Highlord (IoDA)") +
                        "(Vera of War) " + importantItemCheckbox(3, "Verus DoomKnight") +
                        "(Eternal Dragon of War) " + importantItemCheckbox(2, "Dragon of Time") +
                        "(Diviner of War) " + importantItemCheckbox(3, "Arcana Invoker") +
                        "(Tempest of War) " + importantItemCheckbox(2, "Sovereign of Storms") +
                        "(Autist Of War) " + importantItemCheckbox(3, "Martial Artist") +

                        $"Ｗｅａｐｏｎｓ\n" +
                        "(Prisoner of War) " + importantItemCheckbox(1, "Hollowborn Reaper's Scythe") +
                        "(Primordial of War) " + importantItemCheckbox(2, "Necrotic Sword of Doom") +
                        "(Wraith of War) " + importantItemCheckbox(2, "Hollowborn Sword of Doom") +
                        "(Legatus of War) " + importantItemCheckbox(1, "Necrotic Blade of the Underworld") +
                        "(Chauvinist of War) " + importantItemCheckbox(1, "Necrotic Sword of the Abyss") +
                        "(Prudence of War) " + importantItemCheckbox(3, "Providence") +
                        "(Sinner of War) " + importantItemCheckbox(3, "Sin of the Abyss") +
                        "(Deacon of War) " + importantItemCheckbox(2, "Exalted Apotheosis") +
                        "(Deacon of War) " + importantItemCheckbox(2, "Dual Exalted Apotheosis") +
                        "(Celestial of War) " + importantItemCheckbox(1, "Greatblade of the Entwined Eclipse") +
                        "(Starlight of War) " + importantItemCheckbox(2, "Star Light of the Empyrean", "Star Lights of the Empyrean") +

                        $"Ａｒｍｏｒ\n" +
                        $"(Ascendant of War) Awescended:\t\t\t{Checkbox(Core.isCompletedBefore(8042))}\n" +
                        "(Radiant Goddess of War) " + importantItemCheckbox(1, "Radiant Goddess of War") +

                        $"Ｃｈｒｏｎｏ Ｃｈｅｃｋ\n" +
                        $"(Time Lord of War) Chronomancer\t\t\t{Checkbox(ChronoOwned())}\n" +


                        $"Ｅｎｄ Ｃｈｅｃｋｓ\n" +
                        $"Apprentice of War:\t\t\t\t{Checkbox(ApprenticeOfWar())}\n" +
                        $"Master of War:\t\t\t\t\t{Checkbox(MasterofWar())}\n" +
                        $"Apostle of War:\t\t\t\t\t{Checkbox(Apostleofwar())}\n" +
                        $"Bishop of War:\t\t\t\t\t{Checkbox(BishopofWar())}\n" +
                        $"Cardinal of War:\t\t\t\t\t{Checkbox(CardinalofWar())}\n" +
                        $"51% DMG All Weapons:\t\t\t\t{dmgAll51Items} out of 30\n\n" +

                        "\n" +
                        "\n" +

                        $"Ｉｎｆｏ  Ｆｏｒ  Ｐｌａｙｅｒ\n" +
                        GetStatusReport(),

                        "Evaluation Complete"
                    );

        Core.Logger("✅ All processes complete! Ready for final review.");


        #endregion

        void processItems(string prop)
        {
            var list = Bot.Flash.GetGameObject<List<dynamic>>(prop);
            if (list != null)
            {
                for (int i = 0; i < racial75Items.Length; i++)
                {
                    if (!racial75Items[i].Item2 &&
                        list.Any(item =>
                            item.sMeta != null &&
                            ((string)item.sMeta).Contains($"{racial75Items[i].Item1}:1.75")))
                        racial75Items[i].Item2 = true;
                }
                dmgAll51Items += list.Count(item => item.sMeta != null && ((string)item.sMeta).Contains("dmgAll:1.51"));
            }
        }

    }

    private bool ApprenticeOfWar()
    {
        return Bot.Player.Level >= 100 &&
                Bot.Inventory.Items.Concat(Bot.Bank.Items).Any(item => dpsClasses.Contains(item.Name)) &&
                Bot.Inventory.Items.Concat(Bot.Bank.Items).Any(item => farmerClasses.Contains(item.Name)) &&
                Bot.Inventory.Items.Concat(Bot.Bank.Items).Any(item => supportClasses.Contains(item.Name));
    }

    private bool MasterofWar()
    {
        // Define the types of boosts to check for
        string[] boostTypes = { "dmgAll", "Dragonkin", "Elemental", "Human", "Undead", "Chaos" };

        // Check for a valid item with a damage boost of 30% or more
        bool HasThirtyPerceptArmor = Bot.Inventory.Items.Concat(Bot.Bank.Items)
            .Any(item => item != null &&
                         item.Meta != null &&
                         (double.TryParse(Core.GetBoostFloat(item, "dmgAll").ToString(), out double dmgAllValue) && dmgAllValue >= 1.3 ||
                         boostTypes.Skip(1).All(boostType =>
                             double.TryParse(Core.GetBoostFloat(item, boostType).ToString(), out double value) &&
                             value >= 1.3)) &&
                         !IsNonWeapon(item));

        return ApprenticeOfWar() && HasThirtyPerceptArmor && MasterofWarMeta();
    }





    private bool Apostleofwar()
    {
        return MasterofWar()
       && ApostleWeapons.Concat(Apostleinsignias)
    .Any(item => Core.CheckInventory(new[] { item }, any: true, toInv: false));
    }

    private bool BishopofWar()
    {
        return Apostleofwar()
            // Check for 51% damage boost weapon
            && Bot.Inventory.Items.Concat(Bot.Bank.Items).Any(item => item != null && Core.GetBoostFloat(item, "dmgAll") >= 1.5f)
            // Check for Bishop Data Classes (index 0 to 6)
            && Core.CheckInventory(BishopClasses, any: true, toInv: false)
            // Check for Nulgath insignias or items (index 7 to 9)
            && Core.CheckInventory(NulgathItems, any: true, toInv: false)
            // Check for Dage insignias or items (index 10 to end)
            && Core.CheckInventory(DageItems, any: true, toInv: false)
            // Check for >= 1 of bishopClassesOwned && FiftyOneWeaponsOwned
            && Core.CheckInventory(FiftyOneWeapons, any: true, toInv: false);
    }

    private bool CardinalofWar()
    {
        bool hasEnhancements =
        new[] { 8738, 8739, 8740, 8741, 8742, 8758, 8821, 8820, 9560, 8744 }.Any(Core.isCompletedBefore) &&
        new[] { 8826, 8825, 8758, 8827, 8824 }.Any(Core.isCompletedBefore) &&
        new[] { 8743, 8745, 8758, 8823, 8822, 8744 }.Any(Core.isCompletedBefore);

        int FiftyOneWeaponsOwned = FiftyOneWeapons
            .Count(weapon => Bot.Inventory.Items
                .Concat(Bot.Bank.Items)
                .Any(item => item.Name == weapon));

        int bishopClassesOwned = BishopClasses
            .Count(cls => Bot.Inventory.Items
                .Concat(Bot.Bank.Items)
                .Any(item => item.Name == cls));

        /* Returns in order:
        1. BishopofWar Status
        2. >= 4 Class Roles
        3. >= 4 Weapon 51% dmgAll weapons
        4. Has *ATLEAST* 1 weapon,  1 helm, 1 cape Forge Ehn 
        5. Dark Carnax Quest(& Badge) Completed *or* ArchMage Owned.
        */
        return BishopofWar() && bishopClassesOwned >= 4 && FiftyOneWeaponsOwned >= 4 && hasEnhancements && (Core.isCompletedBefore(8873) || Core.CheckInventory("ArchMage", toInv: false));
    }
    #region Variables
    // DPS Classes
    string[] dpsClasses = new[]
    {
        "Dragon of Time",
        "Glacial Berserker",
        "Guardian",
        "Legion DoomKnight",
        "Legion Revenant",
        "LightCaster",
        "Lycan",
        "Psionic MindBreaker",
        "Void HighLord"
};
    // Farmers
    string[] farmerClasses = new[]
    {
        "Abyssal Angel",
        "ArchMage",
        "Blaze Binder",
        "Daimon",
        "Dragon of Time",
        "Eternal Inversionist",
        "Firelord Summoner",
        "Legion Revenant",
        "Dark Master of Moglins",
        "Master of Moglins",
        "NCM",
        "Scarlet Sorceress",
        "ShadowScythe General",
        "Shaman"
    };
    // Support Classes
    string[] supportClasses = new[]
    {
        "ArchFiend",
        "ArchPaladin",
        "Frostval Barbarian",
        "Infinity Titan",
        "Dark Legendary Hero",
        "Legendary Hero",
        "Legion Revenant",
        "LightCaster",
        "Lord of Order",
        "NorthLands Monk",
        "Quantum Chronomancer",
        "Continuum Chronomancer",
        "StoneCrusher"
    };
    private int[] rareIDs =
    {
        21, // Limited Time Drop
        68, // New Collection Chest
        35, // Rare
        40, // Import Item
        55, // Sesaonal Rare
        60, // Event Item
        65, // Event Rare
        70, // Limited Rare
        75, // Collector's Rare
        80, // Promotional Item
        90, // Ultra Rare
        95, // Super Mega Ultra Rare
    };
    private string[] houseCat =
    {
        "Floor Item",
        "Wall Item",
        "House",
    };
    private int[] forgeEnhIDs =
    {
        8738,
        8739,
        8740,
        8741,
        8742,
        8743,
        8745,
        8758,
        8821,
        8820,
        8822,
        8823,
        8824,
        8825,
        8826,
        8827,
        9172,
        9171,
    };
    private (string Name, int ID)[] ForgeQuests = new (string Name, int ID)[]
      {
    ("Forge Weapon Enhancement", 8738),
    ("Lacerate", 8739),
    ("Smite", 8740),
    ("Hero's Valiance", 8741),
    ("Arcana's Concerto", 8742),
    ("Absolution", 8743),
    ("Avarice", 8745),
    ("Praxis", 9171),
    ("Acheron", 8820),
    ("Elysium", 8821),
    ("Penitence", 8822),
    ("Lament", 8823),
    ("Vim, Ether", 8824),
    ("Anima", 8826),
    ("Pneuma", 8827),
    ("Dauntless", 9172),
    ("Forge Cape Enhancement", 8758),
    ("Examen", 8825)
      };
    private string[] HeroMartClasses =
    {
        "CardClasher",
        "Chrono Chaorruptor",
        "Chrono Commandant",
        "Chrono DataKnight",
        "Chrono DragonKnight",
        "ChronoCommander",
        "ChronoCorruptor",
        "Chronomancer",
        "Chronomancer Prime",
        "Classic Defender",
        "Classic Dragonlord",
        "Classic Guardian",
        "Continuum Chronomancer",
        "Corrupted Chronomancer",
        "Dark Master of Moglins",
        "Defender",
        "Dragonlord",
        "DoomKnight OverLord",
        "Dragon Knight",
        "Empyrean Chronomancer",
        "Eternal Chronomancer",
        "Flame Dragon Warrior",
        "Great Thief",
        "Guardian",
        "Heavy Metal Rockstar",
        "Heavy Metal Necro",
        "Immortal Chronomancer",
        "Infinity Knight",
        "Interstellar Knight",
        "Legion Paladin",
        "Master of Moglins",
        "Nechronomancer",
        "Necrotic Chronomancer",
        "NOT A MOD",
        "Nu Metal Necro",
        "Obsidian Paladin Chronomancer",
        "Overworld Chronomancer",
        "Paladin Chronomancer",
        "Paladin Highlord",
        "PaladinSlayer",
        "Quantum Chronomancer",
        "ShadowStalker of Time",
        "ShadowWalker of Time",
        "ShadowWeaver of Time",
        "Star Captain",
        "StarLord",
        "TimeKeeper",
        "TimeKiller",
        "Timeless Chronomancer",
        "Underworld Chronomancer",
        "Unchained Rocker",
    };
    string[] ApostleWeapons = new[]
    {
        "Exalted Penultima",
        "Exalted Unity",
        "Exalted Apotheosis"
    };
    string[] Apostleinsignias = new[]
    {
        "Ezrajal Insignia",
        "Warden Insignia",
        "Engineer Insignia"
    };
    // Bishop Classes
    string[] BishopClasses = new[]
    {
    "Chaos Avenger",
    "ArchMage",
    "Legion Revenant",
    "Void HighLord",
    "Dragon of Time",
    "Verus DoomKnight",
    "Arcana Invoker",
    "Sovereign of Storms"
};

    // Nulgath Insignias and Items
    string[] NulgathItems = new[]
    {
        "Nulgath Insignia",
        "Sin of the Abyss",
        "Sin of Revontheus",
        "Empowered Overfiend Blade",
        "Empowered Ungodly Reavers",
        "Empowered Shadow Spear",
        "Empowered Bloodletter",
        "Empowered Prismatic Manslayers",
        "Empowered Prismatic Manslayer",
        "Empowered Legacy of Nulgath",
        "Empowered Worshipper of Nulgath",
        "Empowered Evolved Fiend",
        "Empowered Evolved Void",
        "Empowered Evolved Blood",
        "Empowered Evolved Hex",
        "Empowered Evolved Shadow"
    };


    // Dage Insignias and Items
    string[] DageItems = new[]
    {
        "Dage the Evil Insignia",
        "Necrotic Blade of the Underworld",
        "Empowered Caladbolg",
        "Empowered Dual Caladbolgs",
        "Empowered Lich King",
        "Empowered Undead Champion",
        "Empowered Bonfire Altar",
        "Empowered Forge Spawn",
        "Empowered Paragon Plate",
        "Empowered BladeMaster",
        "Empowered BladeMaster's Katana",
        "Empowered Dual Katanas",
        "Empowered Dark Caster",
        "Empowered Prismatic Paragon"
    };


    string[] FiftyOneWeapons = new[]
    {
        "Necrotic Sword of Doom",
        "Hollowborn Sword of Doom",
        "Necrotic Blade of the Underworld",
        "Necrotic Sword of the Abyss",
        "Providence",
        "Sin of the Abyss",
        "Exalted Apotheosis",
        "Dual Exalted Apotheosis",
        "Greatblade of the Entwined Eclipse",
        "Star Light of the Empyrean"
    };
    #endregion

    #region Methods
    /// <summary>
    /// Generates a string representation of an item with a checkbox indicating its presence in the inventory.
    /// </summary>
    /// <param name="tabs">The number of tabs to include before the item name. Default is 0.</param>
    /// <param name="items">The item names to check in the inventory. The first item is used for the checkbox label.</param>
    /// <returns>A string with the item name, tabs, and checkbox status (🗸 for present, X for absent).</returns>
    string importantItemCheckbox(int tabs = 0, params string[] items)
    {
        // Generate the required number of tabs
        string _tabs = new('\t', tabs);

        // Determine if the items exist in the inventory
        bool check = Core.CheckInventory(items, 1, true, false);

        // Return the item name, tabs, and its checkbox status (🗸 for true, X for false)
        return $"{items[0]}:{_tabs}{Checkbox(check)}\n";
    }
    /// <summary>
    /// Returns a checkbox representation based on a boolean value.
    /// </summary>
    /// <param name="check">The boolean value indicating whether the checkbox should be checked.</param>
    /// <returns>A string representation of a checkbox with a checkmark (🗸) or an X (X) depending on the value of <paramref name="check"/>.</returns>
    string Checkbox(bool check) =>
     $"[ {(check ? "✅" : "❌")} ]";
    /// <summary>
    /// Checks the completion status of Forge quests and lists any that are incomplete.
    /// </summary>
    /// <param name="incompleteQuests">An output parameter that will contain the names and IDs of any incomplete quests.</param>
    /// <returns>The number of completed Forge quests.</returns>
    private int CheckForgeQuests(out List<string> incompleteQuests)
    {
        incompleteQuests = new List<string>();
        int unlockedQuests = 0;

        foreach (var (Name, ID) in ForgeQuests)
        {
            var quest = Bot.Quests.EnsureLoad(ID);
            if (quest != null)
            {
                if (Core.isCompletedBefore(quest.ID))
                {
                    unlockedQuests++;
                }
                else
                {
                    incompleteQuests.Add($"'{Name}' (ID: {ID}) is not completed.");
                }
            }
            else
            {
                incompleteQuests.Add($"Failed to load quest '{Name}' (ID: {ID}).");
            }
        }

        return unlockedQuests;
    }
    /// <summary>
    /// Checks if the player owns any class related to "Chrono" or "Time" 
    /// from either their inventory or bank.
    /// </summary>
    /// <returns>
    /// Returns <c>true</c> if a "Chrono" or "Time" class is found, 
    /// otherwise returns <c>false</c>.
    /// </returns>
    private bool ChronoOwned()
    {
        // Filter for Chrono/Time classes
        string[] ChronoClasses = HeroMartClasses.Where(x => x.Contains("Chrono") || x.Contains("Time")).ToArray();

        // Check if any Chrono class is found in Inventory or Bank
        return Bot.Inventory.Items
            .Concat(Bot.Bank.Items)
            .Any(x => ChronoClasses.Contains(x.Name));
    }
    /// <summary>
    /// Generates a unique evaluation ID by combining a random alphanumeric string with a hexadecimal representation of the username, and scrambling the result.
    /// </summary>
    /// <returns>A scrambled evaluation ID string.</returns>
    private string GenerateEvaluationID()
    {
        // Alphanumeric characters
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        // Create Random object
        Random random = new();

        // Generate a random alphanumeric string
        string randomString = new(Enumerable.Range(0, 16)
            .Select(_ => chars[random.Next(chars.Length)])
            .ToArray());

        // Convert username to hexadecimal string
        string usernameHex = BitConverter.ToString(Encoding.UTF8.GetBytes(Core.Username()))
            .Replace("-", "");

        // Combine the random string with the hexadecimal string
        string combinedString = randomString + usernameHex;

        // Scramble the combined string
        return new string(combinedString
            .OrderBy(_ => random.Next())
            .ToArray());
    }
    /// <summary>
    /// Builds and returns a status report summarizing the completion of Forge quests and any additional relevant information.
    /// </summary>
    /// <returns>A formatted status report string.</returns>
    public string GetStatusReport()
    {
        // Call CheckForgeQuests to get the list of incomplete quests
        int unlockedQuests = CheckForgeQuests(out List<string> incompleteQuests);

        // Build the report string
        var reportBuilder = new System.Text.StringBuilder();
        reportBuilder.AppendLine($"Number of unlocked quests: {unlockedQuests}");

        if (incompleteQuests.Count > 0)
        {
            reportBuilder.AppendLine("Incomplete quests:");
            foreach (var quest in incompleteQuests)
            {
                reportBuilder.AppendLine(quest);
            }
        }
        else
        {
            reportBuilder.AppendLine("All Forge Quests are completed!");
        }
        return reportBuilder.ToString();
    }
    /// <summary>
    /// Gets the list of racial gear boosts excluding specific types.
    /// </summary>
    /// <returns>An array of racial gear boosts, excluding the specified types.</returns>
    private RacialGearBoost[] racialGears =>
        Enum.GetValues<RacialGearBoost>().Except(RacialGearBoost.None, RacialGearBoost.Drakath, RacialGearBoost.Orc);
    /// <summary>
    /// Determines if an inventory item is neither a weapon nor armor.
    /// </summary>
    /// <param name="x">The inventory item to evaluate.</param>
    /// <returns>True if the item is neither a weapon nor armor; otherwise, false.</returns>
    bool IsNonWeapon(InventoryItem x)
    {
        return x.Category != ItemCategory.Sword
            && x.Category != ItemCategory.Axe
            && x.Category != ItemCategory.Dagger
            && x.Category != ItemCategory.Gun
            && x.Category != ItemCategory.HandGun
            && x.Category != ItemCategory.Rifle
            && x.Category != ItemCategory.Bow
            && x.Category != ItemCategory.Mace
            && x.Category != ItemCategory.Gauntlet
            && x.Category != ItemCategory.Polearm
            && x.Category != ItemCategory.Staff
            && x.Category != ItemCategory.Wand
            && x.Category != ItemCategory.Whip;
    }
    private bool IsNumeric(string str)
    {
        return int.TryParse(str, out _);
    }

    public bool MasterofWarMeta()
    {
        // Define unwanted meta types as a HashSet for faster lookups
        HashSet<string> unwantedMetaTypes = new() { "AutoAdd", "Drakath", "anim", "chance", "Necromancer", "NoSell" };

        // Check for at least one item with 4 or more valid meta types
        bool hasValidItem = false;

        // Concatenate inventory and bank items, then filter for Armor or Pet categories
        foreach (ItemBase item in Bot.Inventory.Items.Concat(Bot.Bank.Items)
                .Where(item => item != null && item.Meta != null &&
                               (item.Category == ItemCategory.Armor || item.Category == ItemCategory.Pet)))
        {
            // Clean unwanted meta types from the item's meta string
            string cleanedMeta = unwantedMetaTypes.Aggregate(item.Meta, (currentMeta, unwanted) =>
                currentMeta.Replace(unwanted + ",", string.Empty)
                           .Replace("," + unwanted, string.Empty) // Handle cases where the unwanted type is at the end
                           .Replace(unwanted, string.Empty)); // Handle cases without a comma

            // Remove purely numeric meta entries
            cleanedMeta = string.Join(",", cleanedMeta
                .Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .SelectMany(line => line.Split(','))
                .Where(entry => !string.IsNullOrWhiteSpace(entry) &&
                                !IsNumeric(entry.Split(':')[0]) && // Check if the key is numeric
                                entry.Contains(':'))); // Ensure it has a key-value structure

            // Count valid meta pairs
            int validMetaCount = cleanedMeta
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(metaEntry => metaEntry.Split(':')) // Split each entry into key-value pairs
                .Count(metaPair => metaPair.Length == 2 // Ensure the pair has exactly two parts
                    && double.TryParse(metaPair[1], out double value) // Try to parse the value
                    && value >= 1.3); // Count only if the value is >= 1.3

            // Log the item and its metas if it has 4 or more valid meta types and hasn't been logged yet
            if (validMetaCount >= 4)
            {
                hasValidItem = true; // Indicate that a valid item has been found
                break; // Break the loop as soon as a valid item is found
            }
        }

        // Return true if at least one item with 4 or more valid meta types is found
        return hasValidItem;
    }

    #endregion
}
