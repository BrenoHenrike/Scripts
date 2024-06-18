/*
name: Daily Gifts
description: This will get all of the available gifts upto this date.
tags: daily-gifts, rare-items
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Seasonal/MayThe4th/TwiggusGearMerge.cs
//cs_include Scripts/Seasonal/SummerBreak/RoseRapiers.cs
//cs_include Scripts/Other/Pets/CursedWazikashi.cs
using System.Globalization;
using Skua.Core.Interfaces;
using Skua.Core.Options;
using Skua.Core.Models.Monsters;

public class CurrentDailyGifts
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private static CoreBots sCore = new();
    private CoreAdvanced Adv = new();
    private TwiggusGearMerge TGM = new();
    private RoseRapiers RR = new();
    private CursedWazikashi CursedWazikashi = new();

    public string OptionsStorage = "CurrentDailyGifts";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        CoreBots.Instance.SkipOptions,
        new Option<DailyGiftsMode>("mode", "Daily Gift Mode", "Please select what behavior you would like the bot to have.", DailyGiftsMode.All_Chronological)
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetAllGifts(Bot.Config!.Get<DailyGiftsMode>("mode"));

        Core.SetOptions(false);
    }

    public void GetAllGifts(DailyGiftsMode mode)
    {
        int Day = 29;
        int Month = 03;
        int Year = 2023;
        DateTime lastUpdate = new(Year, Month, Day);
        Core.Logger("Last update to this bot was on: " + lastUpdate.ToString(formatInfo)[..10]);

        this.mode = mode;
        Core.Logger("Selected mode: " + mode.ToString().Replace('_', ' '));

        for (int i = 0; i < 2; i++)
        {
            #region Old Permanent Gifts

            //Any gifts before this are either permanent or are gone. You guys can add more permanent ones if you feel like doing grunt work.
            //GetGift(Permanent, "map", "monster", "");
            GetGift(Permanent, "legionarena", "Blade Master", "Soulfire Scarf", "Soulfire Sheath", "Soulfire Sheath + Scarf");
            GetGift(Permanent, "maul", "Vending Machine", "Carmel Sandwich Cookie", "Carmel Sandwich Cookies", "Chocolate Sandwich Cookie", "Dual Sandwich Cookies", "Grilled Provolone Sandwich", "Milanesa And Mashed Potatoes", "Sweet Tart", "Sweet Tart with Coconut");
            GetGift(Permanent, "garden", "Fa", "Re and Fa House Guest Duo");
            GetGift(Permanent, "garden", "Creature 83", "Creature 83 Bat", "Bag of Creature 83 Fluffles", "Creature 83 Bats", "Creature 83 Hoodie", "Creature 83 Hood + Locks", "Creature 83 Hood");
            GetGift(Permanent, "eden", "Klawaii Machine", "Apa's Tour Guide Morph", "Crystallis Megaphone", "Eden City Tour Flag", "Eden Tour Guide's Vest", "Eta's Tour Guide Morph", "Furled Eden City Umbrella", "Key to Eden City", "Keys to Eden City", "Rainy Day Tour Guide");
            GetGift(Permanent, "garden", "Creature 83", "Chibi Darkon's Shag", "Chibi Darkon's Twintails", "Chibi Drago's Shag", "Chibi Drago's Twintails", "Chibi Fa's Shag", "Chibi Fa's Twintails", "Chibi La's Shag", "Chibi La's Twintails", "Chibi Mi's Shag", "Chibi Mi's Twintails", "Chibi Re's Shag", "Chibi Re's Twintails", "Chibi So's Shag", "Chibi So's Twintails", "Chibi Suki's Shag", "Chibi Suki's Twintails", "Chibi Ti's Shag", "Chibi Ti's Twintails");
            GetGift(Permanent, "garden", "Creature 72", "Chibi Darkon's Pillow", "Chibi Drago's Pillow", "Chibi Fa's Pillow", "Chibi La's Pillow", "Chibi Mi's Pillow", "Chibi Re's Pillow", "Chibi So's Pillow", "Chibi Suki's Pillow", "Chibi Ti's Pillow");
            // GetGift(Permanent, "eridani", 25, "Malevolent Hourglass");
            GetGift(Permanent, "falcontower", "Alteon", "Alteon's Dragon Sword", "Alteon's Polished Dragon Sword");
            GetGift(Permanent, "aqlesson", "Carnax", "Guardian Blade Evolution", "Sanctified Guardian Blade");
            GetGift(Permanent, "superdeath", "Super Death", "HeroSmash Electina Guard", "HeroSmash General Smash Guard", "HeroSmash Hottica Guard", "HeroSmash Rider Guard", "HeroSmash Ultimate Good Guard", "HeroSmash Ultimate Evil Guard");
            // GetGift(Permanent, "sepulchure", "Dark Sepulchure", "Gravelyn's Champion Wings", "Gravelyn's Purified DoomBlade", "Vampragon of DOOM");
            GetGift(Permanent, "dflesson", "Fluffy the Dracolich", "Zurvana's Blessing", "Zurvana's Wrath", "Zurvana's Wrath", "Zurvana's Pity", "Zurvana's Pity");
            GetGift(Permanent, "mqlesson", "Dragonoid", "Dragonoid Edge", "Dragonoid Edges");
            GetGift(Permanent, "aqw3d", "Trolluk", "Underworld Anguish Scythe");
            GetGift(Permanent, "lavarockbay", "Kalayo", "Lavarock Orb Pet");
            // GetGift(Permanent, "burningbeach", "Lava Guardian", "Burnt Ember Scrap Pet");
            GetGift(Permanent, "twilightzone", "Leviathan", "Fallen Leviathan's Spear", "Staff of the Twilight Sea God", "Sea Dragon's Scimitars", "Sea Dragon's Scimitar");

            #endregion
            //GetGift(AvailableUntil(30, 11, 2022), "moonlab", "Nightmare Zorbak", "Zorbak's VordredSlayer", "Zorbak's VordredSlayers");
            //GetGift(AvailableUntil(1, 1, 2022), "tricktown", "Madam Ester", "Twilleven's Power", "Zorbaknar");
            //GetGift(AvailableUntil(1, 1, 2022), "birthday", "Twilly Pinata", "Enchanted League Competitor's Hair", "Enchanted League Competitor's Locks", "Enchanted League Competitor's Locks and Shades", "Enchanted League Competitor's Shades", "League Competitor's Hair Morph", "League Competitor's Locks Morph", "League Competitor's Shaded Locks Morph", "League Competitor's Shades Morph", "Competitor Watch", "Custom Competitor's Watch", "Custom Competitor's Phone", "Competitor's Cane", "Custom Competitor's Cane");
            //GetGift(AvailableUntil(30, 11, 2022), "deathofgames", "Titan Fluffy", "Enchanted Transcendence Blade", "Enchanted Transcendence Blades", "Transcendence Blade", "Transcendence Blades");
            //GetGift(AvailableUntil(30, 11, 2022), "citadelruins", "Grand Inquisitor Murry", "Crimson Grand Inquisitor Armor", "Grand Inquisitor's Cape", "Crimson Grand Inquisitor's Bearded Headdress", "Crimson Grand Inquisitor's Collared Headdress", "Crimson Grand Inquisitor's Headdress", "Crimson Grand Inquisitor's Shaded Headdress");
            //GetGift(AvailableUntil(30, 11, 2022), "harvestqueen", "Harvest Queen", "Cursed Pumpkin Pet", "Cute Pumpkin Pet");
            //GetGift(AvailableUntil(30, 11, 2022), "birdswithharms", "Turking", "Harvest Assassin", "Harvest Assassin's Cape", "Harvest Assassin's Hood", "Harvest Assassin's Blades", "Harvest Assassin's Sword + Axe", "Harvest Assassin's Blade");
            //GetGift(AvailableUntil(9, 12, 2022), "ebiltakeover", "Ebil Jack Sprat", "Pink POSE! Pitchfork", "Pitchfork of Shadows");
            //GetGift(AvailableUntil(9, 12, 2022), "hbchallenge", "Module 005", "Module 005 Chibi Pet");
            //GetGift(AvailableUntil(9, 12, 2022), "ebiltakeover", "Ebil Red Dragon", "Darkened Adherent Faceplate", "Darken", "Darken Helm", "Prisma Guard", "Prisma Cracked Orb", "Prisma Plunger", "Prisma Shoe-Chucks");
            //GetGift(AvailableUntil(9, 12, 2022), "blackfridaywar", "Deal Bot 2.0", "Rose Phoenix Sword", "Obsidian Phoenix Sword");
            //GetGift(AvailableUntil(9, 12, 2022), "dreammaster", "Calico Cobby", "Cute Calico Cobby Pet");
            //GetGift(AvailableUntil(31, 12, 2022), "ebilcorphq", "Master Chairman", "Suave Suit of Ebil", "Chic Frostval Locks", "Chic Frostval Locks + Glasses", "Chic Locks", "Chic Locks + Glasses", "Suave Frostval Hair + Glasses", "Suave Frostval Hat", "Suave Hair", "Suave Hair + Glasses", "Classy Cane of Ebil", "Elegant Cane of Ebil");
            //GetGift(AvailableUntil(31, 12, 2022), "Helsgrove", "Helsdottir", "Frostval Barbarian Axe", "Frostval Barbarian Axes");
            //GetGift(AvailableUntil(31, 12, 2022), "deerhunt", "Deer?", "Little Reindeer Morph", "Reind'AWWW Moglin");
            //GetGift(AvailableUntil(31, 12, 2022), "yulecat", "Kitty Yu Yule", "Beleen's Festive Frostval Outfit", "Beleen's Festive Hero Hat + Locks", "Beleen's Festive Hero Hat + Hair");
            //GetGift(AvailableUntil(31, 12, 2022), "ultrafrostfang", "Ultra Frost Fang", "Baby Frost Dragon");
            //GetGift(AvailableUntil(31, 12, 2022), "snowview", "Arctic Fox", "Arctic Fox Guard", "Arctic Fox Guard At Rest", "Arctic Fox Pet");
            //GetGift(AvailableUntil(31, 12, 2022), "deerhunt", "Zweinichthirsch", "Spirit Of Frostval", "Frostval Holly + Candle");

            #region January 2023

            //GetGift(AvailableUntil(14, 1), "snowview", "Vaderix", "Dire Wolf Pup");
            //GetGift(AvailableUntil(31, 1), "snowviewrace", "Bandit", "Frostlorn Hair", "Frostlorn Locks");
            //GetGift(AvailableUntil(31, 1), "carolinn", "Frostval Deer", "CandyCane Capybara Pet");
            //GetGift(AvailableUntil(31, 1), "everfrost", "Chillbite", "Ancient Axe of the Archipelago");
            //GetGift(AvailableUntil(31, 1), "newyear", "2023 Ball", "Chaotic Chrono Eye", "New Year Dawning", "Elegant Bejeweled Cane", "AntiRetrograde Clock", "Nascent ChronoWeaver's Runes", "Frigid Wolf Spear");
            //GetGift(AvailableUntil(31, 1), "timeretaliate", "Retrograde Maw", "Nascent ChronoWeaver's Clock", "Polychronic Magister's Hourglasses", "Polychronic Magister's Hourglass", "Chronobound Gear", "Nascent Temporal Portal");
            //GetGift(AvailableUntil(31, 1), "timeretaliate", $"Min’et’s Corpse", "Astromancer's Shag", "Astromancer's Locks", "Astromancer's Cowl + locks", "Astromancer's Cowl", "BlackHole Shrine", "Astral Balance", "Astral Lantern");
            //GetGift(AvailableUntil(31, 1), "timeritual", "Chronocide", "Space-timeless Claws", "Space-timeless Sword", "Space-timeless Staff", "Space-timeless Daggers", "Space-timeless Axe", "Hollowborn Alchemist's Fist");
            GetGift(Permanent, "brokenwoods", "Eldritch Amalgamation", "Diabolical Warden's Katana", "Diabolical Warden's Katanas", "Possessed Diabolical Blade", "Inflamed Diabolical Tail", "Diabolical Banner", "Diabolical Bed", "Diablocal BookShelf", "Diabolical Torch");
            //Bot.Quests.UpdateQuest(7874);
            //GetGift(AvailableUntil(31, 1), "dreampalace", "Zahad", "Mana Rift", "Mana Spiral", "Golden Hawk of Dreams Statue", "Golden Lion of Dreams Statue");
            //GetGift(AvailableUntil(31, 1), "tercessuinotlim", "Dark Makai", "Birthday Helm + Locks of Evil", "Birthday Helm + Locks of Good", "Birthday Helm of Evil", "Birthday Helm of Good");

            #endregion

            #region Febuari 2023
            //GetGift(AvailableUntil(10, 2), "dreampalace", "Zahad", "Mana Spiral", "Mana Rift");
            //GetGift(AvailableUntil(10, 2), "tercessuinotlim", "Void Knight", "Carnage Void's Morph");
            //GetGift(AvailableUntil(10, 2), "tercessuinotlim", "Taro Blademaster", "Infernal SoulRipper", "Infernal SoulRippers");
            //GetGift(AvailableUntil(10, 2), "tercessuinotlim", "Shadow of Nulgath", "Shadow of Nulgath Guard", "Void of Nulgath Guard", "Fiend of Nulgath Guard", "GrimLord of Nulgath Guard");
            //GetGift(AvailableUntil(28, 2), "manor", "Bird of Paradise", "Bird of Coeurs Snowboard");
            //GetGift(AvailableUntil(28, 2), "worldscore", "Crystalized Mana", "Twisted Shadow Daggers");
            //GetGift(AvailableUntil(28, 2), "brokenwoods", "Eldritch Amalgamation", "Diabolical Banner", "Diabolical Bed", "Diabolical BookShelf", "Diabolical Carpet", "Diabolical Couch", "Diabolical Eye Lamp", "Diabolical Fountain", "Diabolical Throne", "Diabolical Torch", "Diabolical Work Desk");
            //GetGift(AvailableUntil(28, 2), "yokaihunt", "Elixir Etokoun", "Lunarian Blasters", "Lunarian Blaster", "Lunarian Gohei", "Lunarian Cresent");
            #endregion

            #region March 2023
            //Fix this its not perm.. i just dont have a date atm...
            //GetGift(AvailableUntil(31, 3), "undervoid", "Conquest", "Dark Birthday Party Guests I", "Dark Birthday Party Guests II", "Evil Birthday Party Guest", "Hungry Dark Birthday Party Guest");
            //GetGift(AvailableUntil(20, 3), "shadowrealmpast", "*", "Shadow Warrior Sword");
            //GetGift(AvailableUntil(27, 3), "eden", "Klawaii Machine", "Minty Fresh Gacha Orb", "Sweet Treat Gacha Orb", "Dragon Lover's Gacha Orb", "TreasureHunter's Gacha Orb", "TechFiend's Gacha Orb", "TreasureHunter's Gacha Orb Decor", "Sweet Treat Gacha Orb Decor", "Minty Fresh Gacha Orb Decor", "TechFiend's Gacha Orb Decor", "Dragon Lover's Gacha Orb Decor");
            #endregion

            #region April 2023 ( + Scavenger Clues)        
            //Dark Scavenger Clue
            GetGift(AvailableUntil(3, 4), "fireplanewar", "ShadowClaw", "Trident of Destruction");

            //Shadowy Scavenger Clue
            GetGift(AvailableUntil(27, 3), "manacradle", "The Mainyu", "ShadowFlame Eviscerator Pistol", "ShadowFlame Eviscerator Pistols", "ShadowFlame Eviscerator Revolver", "ShadowFlame Eviscerator Revolvers", "ShadowFlame Annihilator Rifle", "ShadowFlame Devastator");

            //Undead Scavenger Clue
            GetGift(AvailableUntil(10, 4), "dragontown", "Chaos Fluffy", "Dracosaster", "Dracotastrophe");

            //O_o Scavenger Clue
            GetGift(AvailableUntil(17, 4), "andre", "Giant Fist", "Navel Top Hat");

            //GigaWUT Scavenger Clue
            GetGift(AvailableUntil(24, 4), "dvg", "Munthor", "Giga Twilly");

            //Golden Treasure Hunt clue
            GetGift(AvailableUntil(1, 5), "necrodungeon", 48, "Golden Spear of Light");

            //Message Capsule Clue
            TGM.BuyAllMerge("L'il Twiggu Guest");
            TGM.BuyAllMerge("Baby Twiggu's Pod Pet");

            //Throny Scavenger Clue
            RR.GetWeapons();

            // Fireworks twilly
            if (DateTime.Now.Month == 7)
                Core.BuyItem(Bot.Map.Name, 1348, 78735, shopItemID: 48402);

            GetGift(AvailableUntil(10, 6), "ashray", "Ashray Fisherman", "Twig's Totally FUN-ctional Ride");
            GetGift(AvailableUntil(16, 6), "garden", "Creature 35", "Dark Astravian General Lance");
            #endregion April 2023 ( + Scavenger Clues) 

            #region July 2023
            GetGift(Permanent, "ontherun", "lumberhorc", "Maple Party Twig");

            #endregion July 2023

            #region August 2023

            // Cursed Wakizashi Pet (Treasure Hunt?)
            // CursedWazikashi.CursedWakizashiPet();
            GetGift(AvailableUntil(15, 9), "superslayin", "Charidon", "Charidon Pet", "Charidon Battlepet");
            GetGift(AvailableUntil(15, 9), "garden", "Creature 343", "Debris .45 Revolver", "Debris .45 Revolvers");

            #endregion August 2023

            #region September 2023
            
            GetGift(AvailableUntil(30, 9), "septhub", "Cursed Cecaelia", "DeepWater Waves", "Master Gunner Gween");
            GetGift(AvailableUntil(30, 9), "twilightzone", "Whale Louse", "Compact Cyamidae");
            
            #endregion

            #region October 2023

            GetGift(AvailableUntil(30, 10), "eventhub", "Slayer Cake", "Gravelyn's TopHat + Locks of DOOM", "Gravelyn's TopHat of DOOM", "ShadowScythe Commander's TopHat", "ShadowScythe Commander's TopHat + Locks");
            GetGift(AvailableUntil(30, 10), "lair", "Red Dragon", "Blacksteel Dragon Bow", "Blacksteel Dragon Spear");

            #endregion

            //GetGift(AvailableUntil(1, 1), "map", "monster", "");
            //GetGift(Permanent, "map", "monster", "");

            secondHalf = true;
            if ((int)mode == 0 || (int)mode >= 3)
                break;
        }
    }

    #region GetGift Functions

    /// <summary>
    /// Checks if the items have expired yet, and if not it farms it
    /// </summary>
    private void GetGift(DateTime expiresAt, string map, string monster, params string[] items)
    {
        if (expiresAt == Permanent)
        {
            switch (mode)
            {
                case DailyGiftsMode.Permanent_first_then_Rare:
                    if (secondHalf)
                        return;
                    break;
                case DailyGiftsMode.Rare_first_then_Permanent:
                    if (!secondHalf)
                        return;
                    break;
                case DailyGiftsMode.Rare_only:
                    return; ;
            }
        }
        else
        {
            if (expiresAt.AddDays(1) < DateTime.Now)
                return;

            switch (mode)
            {
                case DailyGiftsMode.Permanent_first_then_Rare:
                    if (!secondHalf)
                        return;
                    break;
                case DailyGiftsMode.Rare_first_then_Permanent:
                    if (secondHalf)
                        return;
                    break;
                case DailyGiftsMode.Permanent_only:
                    return;
            }
        }

        if (!Core.isSeasonalMapActive(map))
            return;

        if (Core.CheckInventory(items, toInv: false))
            return;

        Bot.Drops.Add(items);
        Core.Logger($"Daily Gift from {monster} in /{map.ToLower()}, " +
            (expiresAt == Permanent ? "they're permanent. " :
            $"available untill {expiresAt.ToString(formatInfo)[..10]}. ") +
            $"This monster drops the following items:\n[{DateTime.Now:HH:mm:ss}] (GetGift) \"" + string.Join("\" | \"", items) + "\"");

        foreach (string item in items)
        {
            Core.HuntMonster(map, monster, item, 1, false, log: false);
            Core.ToBank(item);
        }
    }
    /// <summary>
    /// Checks if the items have expired yet, and if not it farms it
    /// </summary>
    private void GetGift(DateTime expiresAt, string map, int monsterMapID, params string[] items)
    {
        if (expiresAt == Permanent)
        {
            switch (mode)
            {
                case DailyGiftsMode.Permanent_first_then_Rare:
                    if (secondHalf)
                        return;
                    break;
                case DailyGiftsMode.Rare_first_then_Permanent:
                    if (!secondHalf)
                        return;
                    break;
                case DailyGiftsMode.Rare_only:
                    return; ;
            }
        }
        else
        {
            if (expiresAt.AddDays(1) < DateTime.Now)
                return;

            switch (mode)
            {
                case DailyGiftsMode.Permanent_first_then_Rare:
                    if (!secondHalf)
                        return;
                    break;
                case DailyGiftsMode.Rare_first_then_Permanent:
                    if (secondHalf)
                        return;
                    break;
                case DailyGiftsMode.Permanent_only:
                    return;
            }
        }

        if (!Core.isSeasonalMapActive(map))
            return;

        Monster? monster = Bot.Monsters.MapMonsters.Find(m => m.MapID == monsterMapID);
        if (monster == null)
        {
            Core.Logger($"Something went wrong, the bot could not find any monster by the MapID of {monsterMapID} in {map}. Please report.");
            return;
        }

        if (Core.CheckInventory(items, toInv: false))
            return;

        Bot.Drops.Add(items);
        Core.Logger($"Daily Gift from {monster.Name} in /{map.ToLower()}, " +
            (expiresAt == Permanent ? "they're permanent. " :
            $"available untill {expiresAt.ToString(formatInfo)[..10]}. ") +
            $"This monster drops the following items:\n[{DateTime.Now:HH:mm:ss}] (GetGift) \"" + string.Join("\" | \"", items) + "\"");

        foreach (string item in items)
        {
            Core.HuntMonsterMapID(map, monster.MapID, item, 1, false, log: false);
            Core.ToBank(item);
        }
    }

    private DateTime AvailableUntil(int Day, int Month, int Year = 2023) => new(Year, Month, Day, 07, 00, 00, DateTimeKind.Utc);
    private DateTime AvailableUntil(int Day, Month Month, int Year = 2023) => new(Year, (int)Month, Day, 07, 00, 00, DateTimeKind.Utc);
    private DateTime Permanent = DateTime.MaxValue;
    private DateTimeFormatInfo formatInfo = CultureInfo.CurrentUICulture.DateTimeFormat;
    private DailyGiftsMode mode = DailyGiftsMode.All_Chronological;
    private bool secondHalf = false;

    #endregion
}

public enum Month
{
    January = 1,
    February = 2,
    March = 3,
    April = 4,
    May = 5,
    June = 6,
    July = 7,
    August = 8,
    September = 9,
    October = 10,
    November = 11,
    December = 12
}

public enum DailyGiftsMode
{
    All_Chronological = 0,
    Rare_first_then_Permanent = 1,
    Permanent_first_then_Rare = 2,
    Permanent_only = 3,
    Rare_only = 4,
}
