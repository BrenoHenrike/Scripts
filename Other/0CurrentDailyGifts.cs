//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;
using System.Globalization;

public class CurrentDailyGifts
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetAllGifts();

        Core.SetOptions(false);
    }

    public void GetAllGifts()
    {
        int Day = 23;
        int Month = 09;
        int Year = 2022;
        DateTime lastUpdate = new(Year, Month, Day);
        Core.Logger("Last update to this bot was on: " + lastUpdate.ToString(formatInfo)[..10]);

        #region Old Permanent Gifts
        //Any gifts before this are either permanent or are gone. You guys can add more permanent ones if you feel like doing grunt work.
        //GetGift(Permanent, "map", "monster", new[] { "" });
        GetGift(Permanent, "legionarena", "Blade Master", new[] { "Soulfire Scarf", "Soulfire Sheath", "Soulfire Sheath + Scarf" });
        GetGift(Permanent, "maul", "Vending Machine", new[] { "Carmel Sandwich Cookie", "Carmel Sandwich Cookies", "Chocolate Sandwich Cookie", "Dual Sandwich Cookies", "Grilled Provolone Sandwich", "Milanesa And Mashed Potatoes", "Sweet Tart", "Sweet Tart with Coconut" });
        GetGift(Permanent, "garden", "Fa", "Re and Fa House Guest Duo");

        #endregion

        #region Expires 30-09-2022
        GetGift(AvailableUntil(30, 09), "mythperception", "Boto", new[] { "Privateer's Cutlass", "Privateer's Dual Grenados", "Privateer's Grenado", "Privateer's Hair", "Privateer's Hand Cannon + Grenado", "Privateer's Locks" });
        //Pitiless Privateer Gear is in Trickster's Merge
        GetGift(AvailableUntil(30, 09), "mythperception", "Corpo-Seco", new[] { "Cangaceiro's Carabina", "Cangaceiro's Crossed Peixeira", "Cangaceiro's Dual Peixeira", "Cangaceiro's Peixeira" });
        GetGift(AvailableUntil(30, 09), "mythperception", "Saci", new[] { "Cangaceira's Chapéu", "Cangaceira's Locks", "Cangaceiro Vitorioso", "Cangaceiro's Chapéu", "Cangaceiro's Chapéu + Beard", "Cangaceiro's Chapéu + Glasses", });
        GetGift(AvailableUntil(30, 09), "hiddendepths", "Aquamancer", "Crystal Staff of the Deep");
        GetGift(AvailableUntil(30, 09), "blazebeard", "Pirate Captain", new[] { "Broken Split Blade of the Deep", "Split Blade of the Deep", "Split Blades of the Deep" });
        Bot.Quests.UpdateQuest(6033);
        GetGift(AvailableUntil(30, 09), "celestialarenad", "Queen of Hope", new[] { "Arch Lich", "Arch Lich's Blessed Scrolls", "Arch Lich's Hood", "Arch Lich's Orb", "Arch Lich's Runes", "Arch Lich's Scrolls", "Arch Lich's Spear", "Arch Lich's Spears" });
        GetGift(AvailableUntil(30, 09), "deadmoor", "Nightmare", new[] { "Huasa's Cabello Elegante", "Huasa's Cabello Festejo", "Huasa's Cabello Oscuro", "Huasa's Rostro Elegante", "Huaso's Atuendo Elegante", "Huaso's Atuendo Escabroso", "Huaso's Atuendo Festejo", "Huaso's Atuendo Oscuro", "Huaso's Cabello Elegante", "Huaso's Cabello Festejo", "Huaso's Cabello Oscuro", "Huaso's Rostro Elegante" });
        GetGift(AvailableUntil(30, 09), "blazebeard", "Pirate Captain", "Pirate Captain Twig");
        GetGift(AvailableUntil(30, 09), "hiddendepths", "Aqua Familiar", new[] { "Dagger of the Depths", "Daggers of the Depths", "Sword of the Depths", "Swords of the Depths" });

        #endregion

        #region Expires 14-10-2022
        GetGift(Permanent, "eden", "Klawaii Machine", new[] { "Apa's Tour Guide Morph", "Crystallis Megaphone", "Eden City Tour Flag", "Eden Tour Guide's Vest", "Eta's Tour Guide Morph", "Furled Eden City Umbrella", "Key to Eden City", "Keys to Eden City", "Rainy Day Tour Guide" });
        GetGift(AvailableUntil(14, 10), "battleontown", "Zard", new[] { "Chibi Plushie Teka", "Chibi Teka Pet", "GIANT Haunted Teka Plushie", "Haunted Teka Plushie", "Teka's Birthday Hat" });
        //GetGift(AvailableUntil(14, 10), "lowtide", "Spectral Jellyfish", new[] { "items are to be announced" });
        //GetGift(Permanent, "garden", "Creature 83", new[] { "item are to be announced" });
        //GetGift(Permanent, "garden", "Creature 72", new[] { "item are to be announced" });
        //GetGift(AvailableUntil(14, 10), "mystcroftforest", "Barghest", new[] { "item are to be announced" });

        #endregion

        //GetGift(AvailableUntil(1, 1), "map", "monster", "");
        //GetGift(AvailableUntil(1, 1), "map", "monster", new[] { "" });
    }

    #region GetGift Functions
    /// <summary>
    /// Checks if the item has expired yet, and if not it farms it
    /// </summary>
    private void GetGift(DateTime expiresAt, string map, string monster, string item)
    {
        if ((expiresAt == Permanent ? false : expiresAt.AddDays(1) < DateTime.Now) || Core.CheckInventory(item, toInv: false))
            return;

        Core.Logger($"Daily Gift from {monster} in /{map.ToLower()}, " +
            (expiresAt == Permanent ? "it's permanent. " :
            $"available untill {new DateTime(expiresAt.Year, expiresAt.Month, expiresAt.Day).ToString(formatInfo)[..10]}. ") +
            $"This monster drops the following item:\n[{DateTime.Now:HH:mm:ss}] (GetGift) \"{item}\"");
        Core.HuntMonster(map, monster, item, 1, false, log: false);
        Core.ToBank(item);
    }
    /// <summary>
    /// Checks if the items have expired yet, and if not it farms it
    /// </summary>
    private void GetGift(DateTime expiresAt, string map, string monster, string[] items)
    {
        if ((expiresAt == Permanent ? false : expiresAt.AddDays(1) < DateTime.Now) || Core.CheckInventory(items, toInv: false))
            return;

        Core.AddDrop(items);
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
