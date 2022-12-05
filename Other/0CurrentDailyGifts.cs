//cs_include Scripts/CoreBots.cs
using System.Globalization;
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class CurrentDailyGifts
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private static CoreBots sCore = new();

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

        GetAllGifts(Bot.Config.Get<DailyGiftsMode>("mode"));

        Core.SetOptions(false);
    }

    public void GetAllGifts(DailyGiftsMode mode)
    {
        int Day = 4;
        int Month = 12;
        int Year = 2022;
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

            #endregion

            #region Expires 30-09-2022
            GetGift(AvailableUntil(30, 09), "mythperception", "Boto", "Privateer's Cutlass", "Privateer's Dual Grenados", "Privateer's Grenado", "Privateer's Hair", "Privateer's Hand Cannon + Grenado", "Privateer's Locks");
            //Pitiless Privateer Gear is in Trickster's Merge
            GetGift(AvailableUntil(30, 09), "mythperception", "Corpo-Seco", "Cangaceiro's Carabina", "Cangaceiro's Crossed Peixeira", "Cangaceiro's Dual Peixeira", "Cangaceiro's Peixeira");
            GetGift(AvailableUntil(30, 09), "mythperception", "Saci", "Cangaceira's Chapéu", "Cangaceira's Locks", "Cangaceiro Vitorioso", "Cangaceiro's Chapéu", "Cangaceiro's Chapéu + Beard", "Cangaceiro's Chapéu + Glasses");
            GetGift(AvailableUntil(30, 09), "hiddendepths", "Aquamancer", "Crystal Staff of the Deep");
            GetGift(AvailableUntil(30, 09), "blazebeard", "Pirate Captain", "Broken Split Blade of the Deep", "Split Blade of the Deep", "Split Blades of the Deep");
            Bot.Quests.UpdateQuest(6033);
            GetGift(AvailableUntil(30, 09), "celestialarenad", "Queen of Hope", "Arch Lich", "Arch Lich's Blessed Scrolls", "Arch Lich's Hood", "Arch Lich's Orb", "Arch Lich's Runes", "Arch Lich's Scrolls", "Arch Lich's Spear", "Arch Lich's Spears");
            GetGift(AvailableUntil(30, 09), "deadmoor", "Nightmare", "Huasa's Cabello Elegante", "Huasa's Cabello Festejo", "Huasa's Cabello Oscuro", "Huasa's Rostro Elegante", "Huaso's Atuendo Elegante", "Huaso's Atuendo Escabroso", "Huaso's Atuendo Festejo", "Huaso's Atuendo Oscuro", "Huaso's Cabello Elegante", "Huaso's Cabello Festejo", "Huaso's Cabello Oscuro", "Huaso's Rostro Elegante");
            GetGift(AvailableUntil(30, 09), "blazebeard", "Pirate Captain", "Pirate Captain Twig");
            GetGift(AvailableUntil(30, 09), "hiddendepths", "Aqua Familiar", "Dagger of the Depths", "Daggers of the Depths", "Sword of the Depths", "Swords of the Depths");

            #endregion

            #region September 22 - 30 leave October 14
            GetGift(Permanent, "eden", "Klawaii Machine", "Apa's Tour Guide Morph", "Crystallis Megaphone", "Eden City Tour Flag", "Eden Tour Guide's Vest", "Eta's Tour Guide Morph", "Furled Eden City Umbrella", "Key to Eden City", "Keys to Eden City", "Rainy Day Tour Guide");
            GetGift(AvailableUntil(14, 10), "battleontown", "Zard", "Chibi Plushie Teka", "Chibi Teka Pet", "GIANT Haunted Teka Plushie", "Haunted Teka Plushie", "Teka's Birthday Hat");
            GetGift(AvailableUntil(14, 10), "lowtide", "Spectral Jellyfish", "Lance of the Depths", "Spear of the Depths", "Staff of the Depths", "Trident of the Depths");
            GetGift(Permanent, "garden", "Creature 83", "Chibi Darkon's Shag", "Chibi Darkon's Twintails", "Chibi Drago's Shag", "Chibi Drago's Twintails", "Chibi Fa's Shag", "Chibi Fa's Twintails", "Chibi La's Shag", "Chibi La's Twintails", "Chibi Mi's Shag", "Chibi Mi's Twintails", "Chibi Re's Shag", "Chibi Re's Twintails", "Chibi So's Shag", "Chibi So's Twintails", "Chibi Suki's Shag", "Chibi Suki's Twintails", "Chibi Ti's Shag", "Chibi Ti's Twintails");
            GetGift(Permanent, "garden", "Creature 72", "Chibi Darkon's Pillow", "Chibi Drago's Pillow", "Chibi Fa's Pillow", "Chibi La's Pillow", "Chibi Mi's Pillow", "Chibi Re's Pillow", "Chibi So's Pillow", "Chibi Suki's Pillow", "Chibi Ti's Pillow");


            #endregion

            #region October 1 - 16 leave November 25

            GetGift(Permanent, "falcontower", "Alteon", "Alteon's Dragon Sword", "Alteon's Polished Dragon Sword");
            GetGift(Permanent, "aqlesson", "Carnax", "Guardian Blade Evolution", "Sanctified Guardian Blade");
            GetGift(Permanent, "superdeath", "Super Death", "HeroSmash Demolicious Guard", "HeroSmash Electina Guard", "HeroSmash General Smash Guard", "HeroSmash Hottica Guard", "HeroSmash Rider Guard", "HeroSmash Ultimate Good Guard");
            GetGift(AvailableUntil(25, 11), "mystcroftforest", "Barghest", "Skullbound Kamas", "Skullbound Kusarigama", "Skullbound Kama");
            GetGift(AvailableUntil(25, 11), "evilwardage", "Dilligas", "Oversoul Black Dragon Pet", "Oversoul Black Dragon Battlepet");

            #endregion

            #region October 17 - 31 leave November 25
            GetGift(Permanent, "sepulchure", "Dark Sepulchure", "Gravelyn's Champion Wings", "Gravelyn's Purified DoomBlade");
            GetGift(Permanent, "dflesson", "Fluffy the Dracolich", "Zurvana's Blessing", "Zurvana's Wrath", "Zurvana's Wrath", "Zurvana's Pity", "Zurvana's Pity");
            GetGift(Permanent, "mqlesson", "Dragonoid", "Dragonoid Edge", "Dragonoid Edges");

            #endregion

            #region October 24 - 30 leave November 25
            GetGift(Permanent, "aqw3d", "Trolluk", "Underworld Anguish Scythe");
            GetGift(AvailableUntil(25, 11), "crescentmoon", "Royce", "Headless Horseman", "Ghost Eta");

            #endregion

            #region November 11 - 18 leave November ??

            GetGift(AvailableUntil(30, 11), "moonlab", "Nightmare Zorbak", "Zorbak's VordredSlayer", "Zorbak's VordredSlayers");
            //GetGift(AvailableUntil(1, 1), "tricktown", "Madam Ester", "Twilleven's Power", "Zorbaknar");
            //GetGift(AvailableUntil(1, 1), "birthday", "Twilly Pinata", "Enchanted League Competitor's Hair", "Enchanted League Competitor's Locks", "Enchanted League Competitor's Locks and Shades", "Enchanted League Competitor's Shades", "League Competitor's Hair Morph", "League Competitor's Locks Morph", "League Competitor's Shaded Locks Morph", "League Competitor's Shades Morph", "Competitor Watch", "Custom Competitor's Watch", "Custom Competitor's Phone", "Competitor's Cane", "Custom Competitor's Cane");
            GetGift(AvailableUntil(30, 11), "deathofgames", "Titan Fluffy", "Enchanted Transcendence Blade", "Enchanted Transcendence Blades", "Transcendence Blade", "Transcendence Blades");
            GetGift(AvailableUntil(30, 11), "citadelruins", "Grand Inquisitor Murry", "Crimson Grand Inquisitor Armor", "Grand Inquisitor's Cape", "Crimson Grand Inquisitor's Bearded Headdress", "Crimson Grand Inquisitor's Collared Headdress", "Crimson Grand Inquisitor's Headdress", "Crimson Grand Inquisitor's Shaded Headdress");
            GetGift(AvailableUntil(30, 11), "harvestqueen", "Harvest Queen", "Cursed Pumpkin Pet", "Cute Pumpkin Pet");
            GetGift(AvailableUntil(30, 11), "birdswithharms", "Turking", "Harvest Assassin", "Harvest Assassin's Cape", "Harvest Assassin's Hood", "Harvest Assassin's Blades", "Harvest Assassin's Sword + Axe", "Harvest Assassin's Blade");

            #endregion

            #region November 25 leaves December 9

            GetGift(AvailableUntil(9, 12), "ebiltakeover", "Ebil Jack Sprat", "Pink POSE! Pitchfork", "Pitchfork of Shadows");
            GetGift(AvailableUntil(9, 12), "hbchallenge", "Module 005", "Module 005 Chibi Pet");
            GetGift(AvailableUntil(9, 12), "ebiltakeover", "Ebil Red Dragon", "Darkened Adherent Faceplate", "Darken", "Darken Helm", "Prisma Guard", "Prisma Cracked Orb", "Prisma Plunger" ,"Prisma Shoe-Chucks");
            GetGift(AvailableUntil(9, 12), "blackfridaywar", "Deal Bot 2.0", "Rose Phoenix Sword", "Obsidian Phoenix Sword");
            GetGift(AvailableUntil(9, 12), "dreammaster", "Calico Cobby", "Cute Calico Cobby Pet");

            #endregion

            #region December 4 leaves December 31

            GetGift(AvailableUntil(31, 12), "ebilcorphq", "Master Chairman", "Suave Suit of Ebil", "Chic Frostval Locks", "Chic Frostval Locks + Glasses", "Chic Locks", "Chic Locks + Glasses", "Suave Frostval Hair + Glasses", "Suave Frostval Hat", "Suave Hair", "Suave Hair + Glasses", "Classy Cane of Ebil", "Elegant Cane of Ebil");
            
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

        if (Core.CheckInventory(items, toInv: false))
            return;

        Bot.Drops.Add(items);
        Core.Logger($"Daily Gift from {monster} in /{map.ToLower()}, " +
            (expiresAt == Permanent ? "they're permanent. " :
            $"available untill {expiresAt.ToString(formatInfo)[..10]}. ") +
            $"This monster drops the following items:\n[{DateTime.Now:HH:mm:ss}] (GetGift) \"" + String.Join("\" | \"", items) + "\"");
        foreach (string item in items)
        {
            Core.HuntMonster(map, monster, item, 1, false, log: false);
            Core.ToBank(item);
        }
    }


    private DateTime AvailableUntil(int Day, int Month, int Year = 2022) => new(Year, Month, Day, 07, 00, 00, DateTimeKind.Utc);
    private DateTime AvailableUntil(int Day, Month Month, int Year = 2022) => new(Year, (int)Month, Day, 07, 00, 00, DateTimeKind.Utc);
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