/*
name: Evaluate Account
description: This script will give a popup telling you a bunch of information regarding your account.
tags: tool, evaluate, account, chrono, heromart, beta, founder, badges, enhancements, rare, seasonal
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using System.Globalization;
using Newtonsoft.Json;

public class EvalAcc
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface Bot)
    {
        Evaluate();
    }

    public void Evaluate()
    {
        #region Items

        Bot.Bank.Open();
        Bot.Quests.Load(forgeEnhIDs.Concat(new[] { 793, 2937, 8042 }).ToArray());
        Bot.Wait.ForTrue(() => Bot.Bank.Items.Any(), 20);

        var inv = Bot.Flash.GetGameObject<List<dynamic>>("world.myAvatar.items");
        var bank = Bot.Flash.GetGameObject<List<dynamic>>("world.bankinfo.items");
        var house = Bot.Flash.GetGameObject<List<dynamic>>("world.myAvatar.houseitems");

        int usedACs = 0;
        int equipment = 0;
        int classes = 0;
        int hmClasses = 0;
        int miscItems = 0;
        int houseItems = 0;

        int onePercentItems = 0;
        int rareItems = 0;
        int seasonalItems = 0;

        processItems("world.myAvatar.items");
        processItems("world.bankinfo.items");
        processItems("world.myAvatar.houseitems");

        #endregion
        #region Slots

        int extraSlots = 0;

        processSlot("world.myAvatar.objData.iBagSlots", 60);
        processSlot("world.myAvatar.objData.iBankSlots", 50);
        processSlot("world.myAvatar.objData.iHouseSlots", 30);

        #endregion
        #region Outputting

        // Some extra data for the output
        var accAge = accountAge(Bot.Flash.GetGameObject("world.myAvatar.objData.dCreated"));
        var gender = Bot.Flash.GetGameObject("world.myAvatar.objData.strGender");
        int ACs = Bot.Flash.GetGameObject<int>("world.myAvatar.objData.intCoins");
        var badges = JsonConvert.DeserializeObject<List<dynamic>>(Core.GetBadgeJSON().Result);

        // Joining Legion will use up at least 120 AC
        if (Core.isCompletedBefore(793))
            usedACs += 120;

        // The actual output
        Bot.ShowMessageBox(
            $"Level:\t\t\t\t{Bot.Player.Level}\n" +
            (accAge != null ? $"Account Age:\t\t\t{(int)(accAge.Value.TotalDays / 365.2425)} years, {(int)(accAge.Value.TotalDays - ((int)(accAge.Value.TotalDays / 365.2425) * 365.2425)) / 30} months\n" : String.Empty) +
            (badges != null ? $"Beta Tester:\t\t\t{checkbox(badges.Any(b => (int)b.badgeID == 1))}\n" : String.Empty) +
            (badges != null ? $"Founder:\t\t\t\t{checkbox(badges.Any(b => (int)b.badgeID == 2))}\n" : String.Empty) +
            (gender != null ? $"Gender:\t\t\t\t{(gender[1] == 'M' ? "Male" : "Female")}\n" : String.Empty) +

            $"\nMaxed Factions:\t\t\t{Bot.Reputation.FactionList.Count(f => f.Rank == 10)} out of 52\n" +
            $"Joined Legion:\t\t\t{checkbox(Core.isCompletedBefore(793))}\n" +
            $"Treasure Potion Count:\t\t{Bot.Inventory.GetItem(18927)?.Quantity}\n\n" +

            $"Current Gold:\t\t\t{ToKMB((int)Math.Round((double)Bot.Player.Gold / 100000d) * 100000)}\n" +
            $"Current ACs:\t\t\t{ToKMB((int)Math.Floor((double)ACs / 1000d) * 1000)}{(ACs > 0 ? "+" : String.Empty)}\n" +
            $"Spent ACs:\t\t\t{ToKMB((int)Math.Floor((double)usedACs / 1000d) * 1000)}{(usedACs > 0 ? "+" : String.Empty)}\n" +
            $"Extra Slots:\t\t\t{extraSlots} slots worth {extraSlots * 200} ACs\n\n" +

            $"Total Badge Count:\t\t{badges?.Count}\n" +
            $"Support Badge Count:\t\t{badges?.Count(b => (string)b.sCategory == "Support")}\n" +
            $"HeroMart Badge Count:\t\t{badges?.Count(b => (string)b.sCategory == "HeroMart")}\n" +
            $"Exclusive Badge Count:\t\t{badges?.Count(b => (string)b.sCategory == "Exclusive")}\n\n" +

            $"Equipment Count:\t\t\t{equipment}\n" +
            $"Class Count:\t\t\t{classes}\n" +
            $"HeroMart Class Count:\t\t{hmClasses}\n" +
            $"Misc Item Count:\t\t\t{miscItems}\n" +
            $"House Item Count:\t\t{houseItems}\n" +
            $"Rare Item Count:\t\t\t{rareItems}\n" +
            $"Seasonal Item Count:\t\t{seasonalItems}\n" +
            $"1% Drop Item Count:\t\t{onePercentItems}\n" +
            $"Collection Chest Count:\t\t{Bot.Inventory.Items.Count(x => isCollectionChest(x)) + Bot.Bank.Items.Count(x => isCollectionChest(x))}\n\n" +

            importantItemCheckbox(3, "Void Highlord", "Void Highlord (IoDA)") +
            importantItemCheckbox(3, "Legion Revenant") +
            importantItemCheckbox(3, "ArchMage") +
            importantItemCheckbox(3, "Dragon of Time") +
            importantItemCheckbox(3, "Lord Of Order") +
            importantItemCheckbox(2, "Necrotic Sword of Doom") +
            importantItemCheckbox(3, "Providence") +
            importantItemCheckbox(2, "Exalted Apotheosis") +
            importantItemCheckbox(2, "Radiant Goddess of War") +
            $"Awescended:\t\t\t{checkbox(Core.isCompletedBefore(8042))}\n\n" +

            $"Awe   \u200AEnhancements Unlocked:\t{checkbox(Core.isCompletedBefore(2937))}\n" +
            $"Forge Enhancements Unlocked:\t{forgeEnhIDs.Count(q => Core.isCompletedBefore(q))} out of {forgeEnhIDs.Count()}"

            , "Evaluation Complete");

        #endregion
        #region Methods

        void processItems(string prop)
        {
            var list = Bot.Flash.GetGameObject<List<dynamic>>(prop);
            if (list != null)
            {
                usedACs += list.Where(item => (int)item.bCoins == 1).Sum(x => (int)x.iCost);
                onePercentItems += list.Count(item => (int)item.iRty == 14);
                rareItems += list.Count(item => rareIDs.Contains((int)item.iRty));
                seasonalItems += list.Count(item => (int)item.iRty == 50);

                int _classes = 0;
                classes += _classes = list.Count(item => (string)item.sIcon == "iiclass");
                int _miscItems = 0;
                miscItems += _miscItems = list.Count(item => (string)item.sIcon == "iibag");
                int _houseItems = 0;
                houseItems += _houseItems = list.Count(item => houseCat.Contains((string)item.sType));
                equipment += list.Count() - _miscItems - _houseItems - _classes;
                hmClasses += list.Count(item => (string)item.sIcon == "iiclass" && this.hmClasses.Contains((string)item.sName));
            }
        }

        void processSlot(string prop, int baseCount)
        {
            int slots = Bot.Flash.GetGameObject<int>(prop);
            if (slots > baseCount)
                extraSlots += slots - baseCount;
        }

        TimeSpan? accountAge(string? input)
        {
            if (input == null)
                return null;

            Dictionary<string, int> Months = new()
            {
                { "Jan", 1 },
                { "Feb", 2 },
                { "Mar", 3 },
                { "May", 4 },
                { "Apr", 5 },
                { "Jun", 6 },
                { "Jul", 7 },
                { "Aug", 8 },
                { "Sep", 9 },
                { "Oct", 10},
                { "Nov", 11},
                { "Dec", 12}
            };

            string[] output = input[1..^1].Split(' ');
            string[] time = output[3].Split(':');
            return DateTime.Now.Subtract(
                new DateTime(
                    Int32.Parse(output[5]),
                    Months.First(x => x.Key == output[1]).Value,
                    Int32.Parse(output[2]),
                    Int32.Parse(time[0]),
                    Int32.Parse(time[1]),
                    Int32.Parse(time[2]),
                    DateTimeKind.Unspecified));
        }

        string ToKMB(int num)
        {
            if (num > 999999999 || num < -999999999)
                return num.ToString("0,,,.###B", CultureInfo.InvariantCulture);
            else if (num > 999999 || num < -999999)
                return num.ToString("0,,.##M", CultureInfo.InvariantCulture);
            else if (num > 999 || num < -999)
                return num.ToString("0,.#K", CultureInfo.InvariantCulture);
            else
                return num.ToString(CultureInfo.InvariantCulture);
        }

        string importantItemCheckbox(int tabs, params string[] items)
        {
            string _tabs = String.Empty;
            for (int t = 0; t < tabs; t++)
                _tabs += '\t';
            return $"{items[0]}:{_tabs}{checkbox(Core.CheckInventory(items, 1, true, false))}\n";
        }
        string checkbox(bool check)
            => $"[ {(check ? "🗸" : "\u200AX\u200A")} ]";

        bool isCollectionChest(InventoryItem item)
            => item.Category == ItemCategory.Pet && (item.Name.Contains("Chest") || item.Name.Contains("Collection"));
        #endregion
    }

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

    private string[] hmClasses =
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
}
