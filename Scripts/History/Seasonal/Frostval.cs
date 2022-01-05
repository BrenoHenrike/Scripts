//cs_include Scripts/CoreBots.cs
using RBot;
using RBot.Options;
using System.Collections.Generic;

public class Frostval
{
    public CoreBots Core => CoreBots.Instance;

    public int questStart = 0;

    public string OptionsStorage = "Frostvale";
    public bool DontPreconfigure = true;

    public List<IOption> Options = new List<IOption>()
    {
        new Option<int>("startQuest", "Quest start index", "This will save the progress through the script.")
    };

    public static readonly int[] qIDs =
    {
        //* /Join IceCave
            155, // Rescue Blizzy
            156, // Scary Snow Men
            157, // Moglin Popsicles
            158, // Crystal Spider
            159, // Fluffy Bears
            160, // Blue Eyed Beast
        //* /Join Factory
            161, // Trouble Makers
            162, // Bad Ice Cream
            163, // Greedy Sneevil
            164, // Shadow Figure
        //* /Join Frostvale
            456, // 'Twas the night before Frostval
            457, // Find Page 2
            458, // Find Page 3
            459, // Find Page 4
            460, // Find Page 5
            461, // Find Page 6
            905, // Spirit Abducted
        //* /Join SnowGlobe
            906, // Shaking the Globes
            907, // A Demonstration
            908, // Hearts of Ice
            909, // Defeat Garaja
        //* /Join GoldenRuins
            910, // Springing Traps
            911, // Frost Lions
            912, // Onslaught Keyrings
            913, // Defeat Lionfang
        //* /Join Alpine
            1508, // Snow Way to Know Where to Go
            1509, // Arming the Undead Army
            1510, // Cold As A Corpse
            1511, // Pretty Pretty Undead Princess Decor
            1512, // Deadifying Frost Lions
            1516, // Defiant Undead Deserters
            1513, // Forest Guardian Gauntlet
        //* /Join IceVolcano
            1519, // Snow Turning Back!
            1520, // Venom in Your Veins
            1521, // Song of the Frozen Heart
        //* /Join SnowyVale
            2522, // Locate Kezeroth
            2523, // Chronoton Detection
            2524, // Core Knowledge
            2525, // Temporal Revelation
            2526, // Before the Darkest Hour
        //* /Join FrostDeep
            2527, // Heart of Ice
            2528, // Absolute Zero Success
            2529, // Dirty Secret
            2530, // Frozen Venom
            2531, // Rune-ing His Plan
            2532, // Deadly Beauty
            2533, // Cold-Hearted Trophies
            2534, // Warmth in the Cold
            2535, // Icy Prizes
            2536, // Fading Magic
            2537, // FrostDeep Dwellers
            2538, // A Breather
            2539, // Raiders From ForstDeep
            2540, // 8 Legged Frost Freaks
            2541, // Freezing the Stone
            2542, // Can You Feel the Chill Tonight?
            2543, // Shrouded on Ice
            2544, // Hard Fight for a Cold Truth
            2545, // Sand and Shardin' Bones
            2546, // Older and Colder
            2547, // The Sword Of Hope
            2576, // A Little Warmth and Light
            2577, // Behind Locked Doors
            2578, // The Lost Key
            2579, // Uncovering Pages of The Past
            2580, // We Know Where To Look
            2581, // A Terrible Hiding Place
            2582, // Face Kezeroth
    };

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Core.AcceptandCompleteTries = 5;

        questStart = bot.Config.Get<int>("startQuest");

        for (int i = questStart; i < qIDs.Length; i++)
        {
            if (i != qIDs.Length - 1 && i != 34 && bot.Quests.IsUnlocked(qIDs[i+1]))
                continue;
            bot.Config.Set("startQuest", i);
            Core.Logger($"Starting {i}");
            Core.EnsureAccept(qIDs[i]);
            switch (i)
            {
                case 0: // Rescue Blizzy
                    Core.SmartKillMonster(qIDs[i], "icecave", "Frosty");
                    break;
                case 1: // Scary Snow Men
                    Core.SmartKillMonster(qIDs[i], "icecave", "Snow Golem");
                    break;
                case 2: // Moglin Popsicles
                    Core.SmartKillMonster(qIDs[i], "icecave", "Frozen Moglin");
                    break;
                case 3: // Crystal Spider
                    Core.SmartKillMonster(qIDs[i], "icecave", "Ice Spider");
                    break;
                case 4: // Fluffy Bears
                    Core.SmartKillMonster(qIDs[i], "icecave", "Polar Bear");
                    break;
                case 5: // Blue Eyed Beast
                    Core.SmartKillMonster(qIDs[i], "icecave", "Frost Dragon");
                    break;
                case 6: // Trouble Makers
                    Core.SmartKillMonster(qIDs[i], "factory", "Sneevil Toy Maker");
                    break;
                case 7: // Bad Ice Cream
                    Core.SmartKillMonster(qIDs[i], "factory", "Snow Golem");
                    break;
                case 8: // Greedy Sneevil
                    Core.SmartKillMonster(qIDs[i], "factory", "Ebilsneezer");
                    break;
                case 9: // Shadow Figure
                    Core.SmartKillMonster(qIDs[i], "frost", "FrostScythe");
                    break;
                case 10: // 'Twas the night before Frostval
                    Core.SmartKillMonster(qIDs[i], "icecave", "Frosty");
                    break;
                case 11: // Find Page 2
                    Core.GetMapItem(85, map: "yulgar");
                    Core.SmartKillMonster(qIDs[i], "icecave", "Frozen Moglin");
                    break;
                case 12: // Find Page 3
                    Core.GetMapItem(86, map: "battleontown");
                    Core.SmartKillMonster(qIDs[i], "icecave", "Frozen Moglin");
                    break;
                case 13: // Find Page 4
                    Core.SmartKillMonster(qIDs[i], "factory", "Sneevil Toy Maker");
                    break;
                case 14: // Find Page 5
                    Core.SmartKillMonster(qIDs[i], "northlandlight", "Santy Claws");
                    break;
                case 15: // Find Page 6
                    Core.GetMapItem(87, map: "battleontown");
                    Core.SmartKillMonster(qIDs[i], "icecave", "Frozen Moglin");
                    break;
                case 16: // Spirit Abducted
                    break;
                case 17: // Shaking the Globes
                    Core.GetMapItem(243, 10, "snowglobe");
                    Core.SmartKillMonster(qIDs[i], "snowglobe", "Snow Golem");
                    break;
                case 18: // A Demonstration
                    Core.SmartKillMonster(qIDs[i], "snowglobe", "Snow Golem");
                    break;
                case 19: // Hearts of Ice
                    Core.SmartKillMonster(qIDs[i], "snowglobe", "Snowman Soldier");
                    break;
                case 20: // Defeat Garaja
                    Core.SmartKillMonster(qIDs[i], "snowglobe", "Garaja");
                    break;
                case 21: // Springing Traps
                    Core.GetMapItem(244, 10, "goldenruins");
                    Core.SmartKillMonster(qIDs[i], "goldenruins", "Golden Warrior");
                    break;
                case 22: // Frost Lions
                    Core.SmartKillMonster(qIDs[i], "goldenruins", "Frost Lion");
                    break;
                case 23: // Onslaught Keyrings
                    Core.SmartKillMonster(qIDs[i], "goldenruins", "Golden Warrior");
                    break;
                case 24: // Defeat Lionfang
                    Core.SmartKillMonster(qIDs[i], "goldenruins", "Maximillian Lionfang");
                    break;
                case 25: // Snow Way to Know Where to Go
                    Core.GetMapItem(758, map: "alpine");
                    break;
                case 26: // Arming the Undead Army
                    Core.SmartKillMonster(qIDs[i], "alpine", "Glacier Mole");
                    break;
                case 27: // Cold As A Corpse
                    Core.GetMapItem(759, 10, "alpine");
                    break;
                case 28: // Pretty Pretty Undead Princess Decor
                    Core.GetMapItem(760, 13, "alpine");
                    break;
                case 29: // Deadfying Frost Lions
                    Core.SmartKillMonster(qIDs[i], "alpine", "Frost Lion");
                    break;
                case 30: // Defiant Undead Deserters
                    Core.SmartKillMonster(qIDs[i], "alpine", "Frozen Deserter");
                    break;
                case 31: // Forest Guadian Gauntlet
                    Core.SmartKillMonster(qIDs[i], "alpine", "Wendigo");
                    break;
                case 32: // Snow Turning Back!
                    Core.GetMapItem(761, 10, "icevolcano");
                    Core.SmartKillMonster(qIDs[i], "icevolcano", new[] { "Snow Golem", "Dead-ly Ice Elemental" });
                    break;
                case 33: // Venom in Your Veins
                    Core.SmartKillMonster(qIDs[i], "icevolcano", "Ice Symbiote");
                    break;
                case 34: // Song of the Frozen Heart
                    Core.SmartKillMonster(qIDs[i], "icevolcano", "Dead Morice");
                    break;
                case 35: // Locate Kezeroth
                    Core.GetMapItem(1584, map: "snowyvale");
                    break;
                case 36: // Chronoton Detection
                    Core.SmartKillMonster(qIDs[i], "snowyvale", "Polar Golem");
                    break;
                case 37: // Core Knowledge
                    Core.GetMapItem(1585, 6, "snowyvale");
                    break;
                case 38: // Temporal Revelation
                    Core.GetMapItem(1586, map: "snowyvale");
                    break;
                case 39: // Before the Darkest Hour
                    Core.GetMapItem(1587, map: "frostdeep");
                    break;
                case 40: // Heart of Ice
                    Core.SmartKillMonster(qIDs[i], "frostdeep", new[] { "Polar Golem", "Polar Elemental" });
                    break;
                case 41: // Absolute Zero Success
                    Core.SmartKillMonster(qIDs[i], "frostdeep", new[] { "Temple Prowler", "Polar Elemental", "Polar Golem" });
                    break;
                case 42: // Dirty Secret
                    Core.SmartKillMonster(qIDs[i], "frostdeep", new[] { "Temple Prowler", "Polar Mole" });
                    break;
                case 43: // Frozen Venom
                    Core.SmartKillMonster(qIDs[i], "frostdeep", new[] { "Polarwyrm Rider", "Polar Spider" });
                    break;
                case 44: // Rune-ing His Plan
                    Core.SmartKillMonster(qIDs[i], "frostdeep", "Ancient Golem");
                    break;
                case 45: // Deadly Beauty
                    Core.SmartKillMonster(qIDs[i], "frostdeep", new[] { "Polar Elemental", "Polar Golem", "Polar Golem" });
                    break;
                case 46: // Cold-Hearted Trophies
                    Core.SmartKillMonster(qIDs[i], "frostdeep", new[] { "Polar Mole", "Temple Prowler", "Temple Prowler" });
                    break;
                case 47: // Warmth in the Cold
                    Core.SmartKillMonster(qIDs[i], "frostdeep", new[] { "Temple Spider", "Temple Maggot" });
                    break;
                case 48: // Icy Prizes
                    Core.SmartKillMonster(qIDs[i], "frostdeep", new[] { "Temple Prowler", "Temple Maggot" });
                    break;
                case 49: // Fading Magic
                    Core.SmartKillMonster(qIDs[i], "frostdeep", new[] { "Ancient Golem", "Ancient Golem" });
                    break;
                case 50: // FrostDeep Dwellers
                    Core.SmartKillMonster(qIDs[i], "frostdeep", new[] { "Polarwyrm Rider", "Polar Mole", "Polar Mole" });
                    break;
                case 51: // A Breather
                    Core.SmartKillMonster(qIDs[i], "frostdeep", new[] { "Polar Mole", "Temple Spider", "Polar Spider" });
                    break;
                case 52: // Raiders From FrostDeep
                    Core.SmartKillMonster(qIDs[i], "frostdeep", new[] { "Polar Draconian", "Temple Maggot" });
                    break;
                case 53: // 8 Legged Frost Freaks
                    Core.SmartKillMonster(qIDs[i], "frostdeep", new[] { "Temple Spider", "Polar Spider" });
                    break;
                case 54: // Freezing the Stone
                    Core.SmartKillMonster(qIDs[i], "frostdeep", new[] { "Ancient Golem", "Ancient Golem" });
                    break;
                case 55: // Can You Feel the Chill Tonight?
                    Core.SmartKillMonster(qIDs[i], "frostdeep", new[] { "Temple Prowler", "Polar Elemental", "Polar Elemental" });
                    break;
                case 56: // Shrouded in Ice
                    Core.SmartKillMonster(qIDs[i], "frostdeep", new[] { "Ancient Maggot", "Ancient Maggot" });
                    break;
                case 57: // Hard Fight for a Cold Truth
                    Core.SmartKillMonster(qIDs[i], "frostdeep", new[] { "Ancient Prowler", "Ancient Prowler" });
                    break;
                case 58: // Sand and Shardin' Bones
                    Core.SmartKillMonster(qIDs[i], "frostdeep", new[] { "Ancient Mole", "Ancient Mole" });
                    break;
                case 59: // Older and Colder
                    Core.SmartKillMonster(qIDs[i], "frostdeep", new[] { "Ancient Mole", "Ancient Prowler", "Ancient Maggot" });
                    break;
                case 60: // The Sword Of Hope
                    Core.SmartKillMonster(qIDs[i], "frostdeep", new[] { "Ancient Terror", "Ancient Terror" });
                    break;
                case 61: // A Little Warmth and Light
                    Core.GetMapItem(1592, 5, "icerise");
                    break;
                case 62: // Behind Locked Doors
                    Core.GetMapItem(1593, map: "icerise");
                    break;
                case 63: // The Lost Key
                    Core.SmartKillMonster(qIDs[i], "icerise", "Polar Golem");
                    break;
                case 64: // Uncovering Pages Of The Past
                    Core.SmartKillMonster(qIDs[i], "icerise", new[] { "Polar Golem", "Polar Elemental", "Arctic Direwolf" });
                    break;
                case 65: // We Know Where To Look
                    Core.SmartKillMonster(qIDs[i], "icerise", new[] { "Polar Golem", "Polar Elemental", "Arctic Direwolf" });
                    break;
                case 66: // A Terrible Hiding Place
                    Core.SmartKillMonster(qIDs[i], "icerise", "Arctic Direwolf");
                    break;
                case 67: // Face Kezeroth!
                    Core.SmartKillMonster(qIDs[i], "icerise", "Kezeroth");
                    break;
            }
            Core.EnsureComplete(qIDs[i]);
            Core.Logger($"Finished {i}");
            Core.Rest();
        }

        Core.SetOptions(false);
    }
}