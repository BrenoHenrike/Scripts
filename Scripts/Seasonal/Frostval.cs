//cs_include Scripts/CoreBots.cs
using RBot;
using RBot.Options;
using System.Collections.Generic;

public class Frostval
{
    public CoreBots Core => CoreBots.Instance;
    public ScriptInterface Bot => ScriptInterface.Instance;

    public void ScriptMain(ScriptInterface Bot)
    {
        Core.SetOptions();

        Part1();
        Part2();
        Part3();
        Part4();
        Part5();
        Part6();
        Part7();

        Core.SetOptions(false);
    }

    public void Part1()
    {
        if (Bot.Quests.IsUnlocked(906))
            return;

        // Rescue Blizzy
        Core.KillQuest(QuestID: 155, MapName: "icecave", MonsterName: "Frosty");

        // Scary Snow Men
        Core.KillQuest(QuestID: 156, MapName: "icecave", MonsterName: "Snow Golem");

        // Moglin Popsicles
        Core.KillQuest(QuestID: 157, MapName: "icecave", MonsterName: "Frozen Moglin");

        // Crystal Spider
        Core.KillQuest(QuestID: 158, MapName: "icecave", MonsterName: "Ice Spider");

        // Fluffy Bears
        Core.KillQuest(QuestID: 159, MapName: "icecave", MonsterName: "Polar Bear");

        // Blue Eyed Beast
        Core.KillQuest(QuestID: 160, MapName: "icecave", MonsterName: "Frost Dragon");

        // Trouble Makers
        Core.KillQuest(QuestID: 161, MapName: "factory", MonsterName: "Sneevil Toy Maker");

        // Bad Ice Cream
        Core.KillQuest(QuestID: 162, MapName: "factory", MonsterName: "Snow Golem");

        // Greedy Sneevil
        Core.KillQuest(QuestID: 163, MapName: "factory", MonsterName: "Ebilsneezer");

        // Shadow Figure
        Core.KillQuest(QuestID: 164, MapName: "frost", MonsterName: "FrostScythe", FollowupIDOverwrite: 456);

        // 'Twas the night before Frostval
        Core.KillQuest(QuestID: 456, MapName: "icecave", MonsterName: "Frosty");

        // Find Page 2
        Core.KillQuest(QuestID: 457, MapName: "icecave", MonsterName: "Frozen Moglin");
        Core.MapItemQuest(QuestID: 457, MapName: "yulgar", MapItemID: 85);

        // Find Page 3
        Core.KillQuest(QuestID: 458, MapName: "icecave", MonsterName: "Frozen Moglin");
        Core.MapItemQuest(QuestID: 458, MapName: "battleontown", MapItemID: 86);

        // Find Page 4
        Core.KillQuest(QuestID: 459, MapName: "factory", MonsterName: "Sneevil Toy Maker");

        // Find Page 5
        Core.KillQuest(QuestID: 460, MapName: "northlandlight", MonsterName: "Santy Claws");

        // Find Page 6
        Core.MapItemQuest(QuestID: 461, MapName: "battleontown", MapItemID: 87);
        Core.KillQuest(QuestID: 461, MapName: "icecave", MonsterName: "Frozen Moglin", hasFollowup: false);

        // Spirit Abducted 
        Core.Join("frostval");
        Core.ChainComplete(905);
        //Core.EnsureComplete(905);
    }

    public void Part2()
    {
        if (Bot.Quests.IsUnlocked(1508))
            return;

        // Shaking the Globes
        Core.MapItemQuest(QuestID: 906, MapName: "snowglobe", MapItemID: 243, Amount: 10);
        Core.KillQuest(QuestID: 906, MapName: "snowglobe", MonsterName: "Snow Golem");

        //A Demonstration
        Core.KillQuest(QuestID: 907, MapName: "snowglobe", MonsterName: "Snow Golem");

        // Hearts of Ice
        Core.KillQuest(QuestID: 908, MapName: "snowglobe", MonsterName: "snowman Soldier");

        // Defeat Garaja
        Core.KillQuest(QuestID: 909, MapName: "snowglobe", MonsterName: "Garaja");

        // Springing Traps
        Core.KillQuest(QuestID: 910, MapName: "goldenruins", MonsterName: "Golden Warrior");
        Core.MapItemQuest(QuestID: 910, MapName: "goldenruins", MapItemID: 244, Amount: 10);

        // Frost Lions
        Core.KillQuest(QuestID: 911, MapName: "goldenruins", MonsterName: "Frost Lion");

        // Onslaught Keyrings
        Core.KillQuest(QuestID: 912, MapName: "goldenruins", MonsterName: "Golden Warrior");

        // Defeat Lionfang
        Core.KillQuest(QuestID: 913, MapName: "goldenruins", MonsterName: "Maximillian Lionfang", hasFollowup: false);
    }

    public void Part3()
    {
        if (Bot.Quests.IsUnlocked(2522))
            return;


        // Snow Way to Know Where to Go
        Core.MapItemQuest(QuestID: 1508, MapName: "alpine", MapItemID: 758);

        // Arming the Undead Army
        Core.KillQuest(QuestID: 1509, MapName: "alpine", MonsterName: "Glacier Mole");

        // Cold As A Corpse
        Core.MapItemQuest(QuestID: 1510, MapName: "alpine", MapItemID: 759, Amount: 10);

        // Pretty Pretty Undead Princess Decor
        Core.MapItemQuest(QuestID: 1511, MapName: "alpine", MapItemID: 760, Amount: 13);

        // Deadfying Frost Lions
        Core.KillQuest(QuestID: 1512, MapName: "alpine", MonsterName: "Frost Lion", FollowupIDOverwrite: 1516);

        // Defiant Undead Deserters
        Core.KillQuest(QuestID: 1516, MapName: "alpine", MonsterName: "Frozen Deserter", FollowupIDOverwrite: 1513);

        // Forest Guadian Gauntlet
        Core.KillQuest(QuestID: 1513, MapName: "alpine", MonsterName: "Wendigo", FollowupIDOverwrite: 1519);

        // Snow Turning Back!
        Core.KillQuest(QuestID: 1519, MapName: "icevolcano", MonsterNames: new[] { "Snow Golem", "Dead-ly Ice Elemental" });
        Core.MapItemQuest(QuestID: 1519, MapName: "icevolcano", MapItemID: 761, Amount: 10);

        // Venom in Your Veins
        Core.KillQuest(QuestID: 1520, MapName: "icevolcano", MonsterName: "Ice Symbiote");

        // Song of the Frozen Heart
        Core.KillQuest(QuestID: 1521, MapName: "icevolcano", MonsterName: "Dead Morice", hasFollowup: false);
    }

    public void Part4()
    {
        if (Bot.Quests.IsUnlocked(2576))
            return;

        // Locate Kezeroth
        Core.MapItemQuest(QuestID: 2522, MapName: "snowyvale", MapItemID: 1584);

        // Chronoton Detection
        Core.KillQuest(QuestID: 2523, MapName: "snowyvale", MonsterName: "Polar Golem");

        // Core Knowledge
        Core.MapItemQuest(QuestID: 2524, MapName: "snowyvale", MapItemID: 1585, Amount: 6);

        // Temporal Revelation
        Core.MapItemQuest(QuestID: 2525, MapName: "snowyvale", MapItemID: 1586);

        // Before the Darkest Hour
        Core.MapItemQuest(QuestID: 2526, MapName: "frostdeep", MapItemID: 1587);

        // Heart of Ice
        Core.KillQuest(QuestID: 2527, MapName: "frostdeep", MonsterNames: new[] { "Polar Golem", "Polar Elemental" });

        // Absolute Zero Success
        Core.KillQuest(QuestID: 2528, MapName: "frostdeep", MonsterNames: new[] { "Temple Prowler", "Polar Elemental", "Polar Golem" });

        // Dirty Secret
        Core.KillQuest(QuestID: 2529, MapName: "frostdeep", MonsterNames: new[] { "Temple Prowler", "Polar Mole" });

        // Frozen Venom
        Core.KillQuest(QuestID: 2530, MapName: "frostdeep", MonsterNames: new[] { "Polarwyrm Rider", "Polar Spider" });

        // Rune-ing His Plan
        Core.KillQuest(QuestID: 2531, MapName: "frostdeep", MonsterName: "Ancient Golem");

        // Deadly Beauty
        Core.KillQuest(QuestID: 2532, MapName: "frostdeep", MonsterNames: new[] { "Polar Elemental", "Polar Golem", "Polar Golem" });

        // Cold-Hearted Trophies
        Core.KillQuest(QuestID: 2533, MapName: "frostdeep", MonsterNames: new[] { "Polar Mole", "Temple Prowler", "Temple Prowler" });

        // Warmth in the Cold
        Core.KillQuest(QuestID: 2534, MapName: "frostdeep", MonsterNames: new[] { "Temple Spider", "Temple Maggot" });

        // Icy Prizes
        Core.KillQuest(QuestID: 2535, MapName: "frostdeep", MonsterNames: new[] { "Temple Prowler", "Temple Maggot" });

        // Fading Magic
        Core.KillQuest(QuestID: 2536, MapName: "frostdeep", MonsterNames: new[] { "Ancient Golem", "Ancient Golem" });

        // FrostDeep Dwellers
        Core.KillQuest(QuestID: 2537, MapName: "frostdeep", MonsterNames: new[] { "Polarwyrm Rider", "Polar Mole", "Polar Mole" });

        // A Breather
        Core.KillQuest(QuestID: 2538, MapName: "frostdeep", MonsterNames: new[] { "Polar Mole", "Temple Spider", "Polar Spider" });

        // Raiders From FrostDeep
        Core.KillQuest(QuestID: 2539, MapName: "frostdeep", MonsterNames: new[] { "Polar Draconian", "Temple Maggot" });

        // 8 Legged Frost Freaks
        Core.KillQuest(QuestID: 2540, MapName: "frostdeep", MonsterNames: new[] { "Temple Spider", "Polar Spider" });

        // Freezing the Stone
        Core.KillQuest(QuestID: 2541, MapName: "frostdeep", MonsterNames: new[] { "Ancient Golem", "Ancient Golem" });

        // Can You Feel the Chill Tonight?
        Core.KillQuest(QuestID: 2542, MapName: "frostdeep", MonsterNames: new[] { "Temple Prowler", "Polar Elemental", "Polar Elemental" });

        // Shrouded in Ice
        Core.KillQuest(QuestID: 2543, MapName: "frostdeep", MonsterNames: new[] { "Ancient Maggot", "Ancient Maggot" });

        // Hard Fight for a Cold Truth
        Core.KillQuest(QuestID: 2544, MapName: "frostdeep", MonsterNames: new[] { "Ancient Prowler", "Ancient Prowler" });

        // Sand and Shardin' Bones
        Core.KillQuest(QuestID: 2545, MapName: "frostdeep", MonsterNames: new[] { "Ancient Mole", "Ancient Mole" });

        // Older and Colder
        Core.KillQuest(QuestID: 2546, MapName: "frostdeep", MonsterNames: new[] { "Ancient Mole", "Ancient Prowler", "Ancient Maggot" });

        // The Sword Of Hope
        Core.KillQuest(QuestID: 2547, MapName: "frostdeep", MonsterNames: new[] { "Ancient Terror", "Ancient Terror" }, hasFollowup: false);
    }

    public void Part5()
    {
        if (Bot.Quests.IsUnlocked(6122))
            return;

        // A Little Warmth and Light
        Core.MapItemQuest(QuestID: 2576, MapName: "icerise", MapItemID: 1592, Amount: 5);

        // Behind Locked Doors
        Core.MapItemQuest(QuestID: 2577, MapName: "icerise", MapItemID: 1593);

        // The Lost Key
        Core.KillQuest(QuestID: 2578, MapName: "icerise", MonsterName: "Polar Golem");

        // Uncovering Pages Of The Past
        Core.KillQuest(QuestID: 2579, MapName: "icerise", MonsterNames: new[] { "Polar Golem", "Polar Elemental", "Arctic Direwolf" });

        // We Know Where To Look
        Core.KillQuest(QuestID: 2580, MapName: "icerise", MonsterNames: new[] { "Polar Golem", "Polar Elemental", "Arctic Direwolf" });

        // A Terrible Hiding Place
        Core.KillQuest(QuestID: 2581, MapName: "icerise", MonsterName: "Arctic Direwolf");

        // Face Kezeroth!
        Core.KillQuest(QuestID: 2582, MapName: "icerise", MonsterName: "Kezeroth", hasFollowup: false);
    }

    public void Part6()
    {
        if (Bot.Quests.IsUnlocked(3907))
            return;

        // Help Blizzy
        Core.MapItemQuest(QuestID: 6122, MapName: "coldwindvalley", MapItemID: 5547);
        Core.MapItemQuest(QuestID: 6122, MapName: "coldwindvalley", MapItemID: 5548);
        Core.MapItemQuest(QuestID: 6122, MapName: "coldwindvalley", MapItemID: 5549);
        Core.MapItemQuest(QuestID: 6122, MapName: "coldwindvalley", MapItemID: 5550);

        // Gather Ammunition
        Core.KillQuest(QuestID: 6123, MapName: "coldwindvalley", MonsterName: "Hail Elemental");

        // Arm the Mob
        Core.KillQuest(QuestID: 6124, MapName: "coldwindvalley", MonsterName: "Scarecrow");
        Core.MapItemQuest(QuestID: 6124, MapName: "coldwindvalley", MapItemID: 5551, Amount: 5);

        // Gather Bait
        Core.KillQuest(QuestID: 6125, MapName: "coldwindvalley", MonsterName: "Arctic Wolf");

        // Bait the Trap
        Core.KillQuest(QuestID: 6126, MapName: "coldwindvalley", MonsterName: "Ice Master Yeti");
        Core.MapItemQuest(QuestID: 6126, MapName: "coldwindvalley", MapItemID: 5552);

        // Gather Snowman Pieces
        Core.KillQuest(QuestID: 6127, MapName: "coldwindvalley", MonsterName: "Snow Golem");
        Core.MapItemQuest(QuestID: 6127, MapName: "coldwindvalley", MapItemID: 5553, Amount: 2);

        // Gather Snowman Decorations
        Core.KillQuest(QuestID: 6128, MapName: "coldwindvalley", MonsterName: "Coal Imp");
        Core.MapItemQuest(QuestID: 6128, MapName: "coldwindvalley", MapItemID: 5554);

        // Grab some Garb
        Core.KillQuest(QuestID: 6129, MapName: "coldwindvalley", MonsterName: "Frost Goblin");

        // Bait and Gifts
        Core.MapItemQuest(QuestID: 6130, MapName: "coldwindvalley", MapItemID: 5555);

        // Check out the Cave
        Core.KillQuest(QuestID: 6131, MapName: "coldwindvalley", MonsterName: "Arctusk");

        // Holly and Ice
        Core.KillQuest(QuestID: 6132, MapName: "coldwindvalley", MonsterName: "Snow Golem");
        Core.MapItemQuest(QuestID: 6132, MapName: "coldwindvalley", MapItemID: 5557, Amount: 8, hasFollowup: false);
    }

    public void Part7()
    {
        if (Bot.Quests.IsUnlocked(3920))
            return;

        // Seek the Tower
        Core.MapItemQuest(QuestID: 3907, MapName: "frozentower", MapItemID: 3022);

        // A n-Ice Beginning
        Core.KillQuest(QuestID: 3908, MapName: "frozentower", MonsterName: "Polar Elemental");

        // Search for Syrrus
        Core.MapItemQuest(QuestID: 3909, MapName: "frozentower", MapItemID: 3019);

        // Building the Base
        Core.MapItemQuest(QuestID: 3910, MapName: "frozentower", MapItemID: 3004, Amount: 13);

        // Refugee Roundup
        Core.KillQuest(QuestID: 3911, MapName: "frozentower", MonsterNames: new[] { "Frostwyrm", "Frostwyrm" });

        // Retrieve the Water Starstone
        Core.KillQuest(QuestID: 3912, MapName: "frozentower", MonsterName: "FrostDeep Dweller");

        // Magical Attraction
        Core.MapItemQuest(QuestID: 3913, MapName: "frozentower", MapItemID: 3005, Amount: 13);

        // Frozen Blood
        Core.KillQuest(QuestID: 3914, MapName: "frozentower", MonsterName: "Twisted Ice");

        // Retrieve the Fire Starstone
        Core.MapItemQuest(QuestID: 3915, MapName: "frozentower", MapItemID: 3006);

        // Defend the Tower!
        Core.KillQuest(QuestID: 3916, MapName: "frozentower", MonsterName: "Polar Elemental");

        // Refugee Rescue Run
        Core.MapItemQuest(QuestID: 3917, MapName: "frozentower", MapItemID: 3007, Amount: 6);

        // Retrieve the Earth Starston
        Core.MapItemQuest(QuestID: 3918, MapName: "frozentower", MapItemID: 3013);

        // Polar Penetration and Progress
        Core.KillQuest(QuestID: 3919, MapName: "frozentower", MonsterName: "Polar Elemental");
        Core.MapItemQuest(QuestID: 3919, MapName: "frozentower", MapItemID: 3008, Amount: 6);

        // Save the Astronomer Apprentice
        Core.KillQuest(QuestID: 3920, MapName: "frozentower", MonsterName: "Polar Elemental");
        Core.MapItemQuest(QuestID: 3920, MapName: "frozentower", MapItemID: 3020, hasFollowup: false);
    }
}
