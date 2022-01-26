//cs_include Scripts/CoreBots.cs
using RBot;

public class Frostval
{
    public CoreBots Core => CoreBots.Instance;
    public ScriptInterface Bot => ScriptInterface.Instance;

    public void ScriptMain(ScriptInterface Bot)
    {
        Core.SetOptions();

        DoAll();

        Core.SetOptions(false);
    }

    public void DoAll()
    {
        IceCave();
        SnowGlobe();
        Alpine();
        SnowyVale();
        IceRise();
        ColdWindValley();
    }

    public void IceCave()
    {
        if (Bot.Quests.IsUnlocked(906))
            return;

        // Rescue Blizzy
        Core.KillQuest(155, "icecave", "Frosty");

        // Scary Snow Men
        Core.KillQuest(156, "icecave", "Snow Golem");

        // Moglin Popsicles
        Core.KillQuest(157, "icecave", "Frozen Moglin");

        // Crystal Spider
        Core.KillQuest(158, "icecave", "Ice Spider");

        // Fluffy Bears
        Core.KillQuest(159, "icecave", "Polar Bear");

        // Blue Eyed Beast
        Core.KillQuest(160, "icecave", "Frost Dragon");

        // Trouble Makers
        Core.KillQuest(161, "factory", "Sneevil Toy Maker");

        // Bad Ice Cream
        Core.KillQuest(162, "factory", "Snow Golem");

        // Greedy Sneevil
        Core.KillQuest(163, "factory", "Ebilsneezer");

        // Shadow Figure
        Core.KillQuest(164, "frost", "FrostScythe", FollowupIDOverwrite: 456);

        // 'Twas the night before Frostval
        Core.KillQuest(456, "icecave", "Frosty");

        // Find Page 2
        Core.KillQuest(457, "icecave", "Frozen Moglin");
        Core.MapItemQuest(457, "yulgar", 85);

        // Find Page 3
        Core.KillQuest(458, "icecave", "Frozen Moglin");
        Core.MapItemQuest(458, "battleontown", 86);

        // Find Page 4
        Core.KillQuest(459, "factory", "Sneevil Toy Maker");

        // Find Page 5
        Core.KillQuest(460, "northlandlight", "Santy Claws");

        // Find Page 6
        Core.MapItemQuest(461, "battleontown", 87, hasFollowup: false);
        Core.KillQuest(461, "icecave", "Frozen Moglin", hasFollowup: false);

        // Spirit Abducted 
        Core.Join("frostval");
        Core.ChainComplete(905);
    }

    public void SnowGlobe()
    {
        if (Bot.Quests.IsUnlocked(1508))
            return;

        // Shaking the Globes
        Core.MapItemQuest(906, "snowglobe", 243, 10);
        Core.KillQuest(906, "snowglobe", "Snow Golem");

        //A Demonstration
        Core.KillQuest(907, "snowglobe", "Snow Golem");

        // Hearts of Ice
        Core.KillQuest(908, "snowglobe", "snowman Soldier");

        // Defeat Garaja
        Core.KillQuest(909, "snowglobe", "Garaja");

        // Springing Traps
        Core.KillQuest(910, "goldenruins", "Golden Warrior");
        Core.MapItemQuest(910, "goldenruins", 244, 10);

        // Frost Lions
        Core.KillQuest(911, "goldenruins", "Frost Lion");

        // Onslaught Keyrings
        Core.KillQuest(912, "goldenruins", "Golden Warrior");

        // Defeat Lionfang
        Core.KillQuest(913, "goldenruins", "Maximillian Lionfang", hasFollowup: false);
    }

    public void Alpine()
    {
        if (Bot.Quests.IsUnlocked(2522))
            return;


        // Snow Way to Know Where to Go
        Core.MapItemQuest(1508, "alpine", 758);

        // Arming the Undead Army
        Core.KillQuest(1509, "alpine", "Glacier Mole");

        // Cold As A Corpse
        Core.MapItemQuest(1510, "alpine", 759, 10);

        // Pretty Pretty Undead Princess Decor
        Core.MapItemQuest(1511, "alpine", 760, 13);

        // Deadfying Frost Lions
        Core.KillQuest(1512, "alpine", "Frost Lion", FollowupIDOverwrite: 1516);

        // Defiant Undead Deserters
        Core.KillQuest(1516, "alpine", "Frozen Deserter", FollowupIDOverwrite: 1513);

        // Forest Guadian Gauntlet
        Core.KillQuest(1513, "alpine", "Wendigo", FollowupIDOverwrite: 1519);

        // Snow Turning Back!
        Core.KillQuest(1519, "icevolcano", MonsterNames: new[] { "Snow Golem", "Dead-ly Ice Elemental" });
        Core.MapItemQuest(1519, "icevolcano", 761, 10);

        // Venom in Your Veins
        Core.KillQuest(1520, "icevolcano", "Ice Symbiote");

        // Song of the Frozen Heart
        Core.KillQuest(1521, "icevolcano", "Dead Morice", hasFollowup: false);
    }

    public void SnowyVale()
    {
        if (Bot.Quests.IsUnlocked(2576))
            return;

        // Locate Kezeroth
        Core.MapItemQuest(2522, "snowyvale", 1584);

        // Chronoton Detection
        Core.KillQuest(2523, "snowyvale", "Polar Golem");

        // Core Knowledge
        Core.MapItemQuest(2524, "snowyvale", 1585, 6);

        // Temporal Revelation
        Core.MapItemQuest(2525, "snowyvale", 1586);

        // Before the Darkest Hour
        Core.MapItemQuest(2526, "frostdeep", 1587);

        // Heart of Ice
        Core.KillQuest(2527, "frostdeep", MonsterNames: new[] { "Polar Golem", "Polar Elemental" });

        // Absolute Zero Success
        Core.KillQuest(2528, "frostdeep", MonsterNames: new[] { "Temple Prowler", "Polar Elemental", "Polar Golem" });

        // Dirty Secret
        Core.KillQuest(2529, "frostdeep", MonsterNames: new[] { "Temple Prowler", "Polar Mole" });

        // Frozen Venom
        Core.KillQuest(2530, "frostdeep", MonsterNames: new[] { "Polarwyrm Rider", "Polar Spider" });

        // Rune-ing His Plan
        Core.KillQuest(2531, "frostdeep", "Ancient Golem");

        // Deadly Beauty
        Core.KillQuest(2532, "frostdeep", MonsterNames: new[] { "Polar Elemental", "Polar Golem", "Polar Golem" });

        // Cold-Hearted Trophies
        Core.KillQuest(2533, "frostdeep", MonsterNames: new[] { "Polar Mole", "Temple Prowler", "Temple Prowler" });

        // Warmth in the Cold
        Core.KillQuest(2534, "frostdeep", MonsterNames: new[] { "Temple Spider", "Temple Maggot" });

        // Icy Prizes
        Core.KillQuest(2535, "frostdeep", MonsterNames: new[] { "Temple Prowler", "Temple Maggot" });

        // Fading Magic
        Core.KillQuest(2536, "frostdeep", MonsterNames: new[] { "Ancient Golem", "Ancient Golem" });

        // FrostDeep Dwellers
        Core.KillQuest(2537, "frostdeep", MonsterNames: new[] { "Polarwyrm Rider", "Polar Mole", "Polar Mole" });

        // A Breather
        Core.KillQuest(2538, "frostdeep", MonsterNames: new[] { "Polar Mole", "Temple Spider", "Polar Spider" });

        // Raiders From FrostDeep
        Core.KillQuest(2539, "frostdeep", MonsterNames: new[] { "Polar Draconian", "Temple Maggot" });

        // 8 Legged Frost Freaks
        Core.KillQuest(2540, "frostdeep", MonsterNames: new[] { "Temple Spider", "Polar Spider" });

        // Freezing the Stone
        Core.KillQuest(2541, "frostdeep", MonsterNames: new[] { "Ancient Golem", "Ancient Golem" });

        // Can You Feel the Chill Tonight?
        Core.KillQuest(2542, "frostdeep", MonsterNames: new[] { "Temple Prowler", "Polar Elemental", "Polar Elemental" });

        // Shrouded in Ice
        Core.KillQuest(2543, "frostdeep", MonsterNames: new[] { "Ancient Maggot", "Ancient Maggot" });

        // Hard Fight for a Cold Truth
        Core.KillQuest(2544, "frostdeep", MonsterNames: new[] { "Ancient Prowler", "Ancient Prowler" });

        // Sand and Shardin' Bones
        Core.KillQuest(2545, "frostdeep", MonsterNames: new[] { "Ancient Mole", "Ancient Mole" });

        // Older and Colder
        Core.KillQuest(2546, "frostdeep", MonsterNames: new[] { "Ancient Mole", "Ancient Prowler", "Ancient Maggot" });

        // The Sword Of Hope
        Core.KillQuest(2547, "frostdeep", MonsterNames: new[] { "Ancient Terror", "Ancient Terror" }, hasFollowup: false);
    }

    public void IceRise()
    {
        if (Bot.Quests.IsUnlocked(6122))
            return;

        // A Little Warmth and Light
        Core.MapItemQuest(2576, "icerise", 1592, 5);

        // Behind Locked Doors
        Core.MapItemQuest(2577, "icerise", 1593);

        // The Lost Key
        Core.KillQuest(2578, "icerise", "Polar Golem");

        // Uncovering Pages Of The Past
        Core.KillQuest(2579, "icerise", MonsterNames: new[] { "Polar Golem", "Polar Elemental", "Arctic Direwolf" });

        // We Know Where To Look
        Core.KillQuest(2580, "icerise", MonsterNames: new[] { "Polar Golem", "Polar Elemental", "Arctic Direwolf" });

        // A Terrible Hiding Place
        Core.KillQuest(2581, "icerise", "Arctic Direwolf");

        // Face Kezeroth!
        Core.KillQuest(2582, "icerise", "Kezeroth", hasFollowup: false);
    }

    public void ColdWindValley()
    {
        if (Bot.Quests.IsUnlocked(3907))
            return;

        // Help Blizzy
        Core.MapItemQuest(6122, "coldwindvalley", 5547);
        Core.MapItemQuest(6122, "coldwindvalley", 5548);
        Core.MapItemQuest(6122, "coldwindvalley", 5549);
        Core.MapItemQuest(6122, "coldwindvalley", 5550);

        // Gather Ammunition
        Core.KillQuest(6123, "coldwindvalley", "Hail Elemental");

        // Arm the Mob
        Core.KillQuest(6124, "coldwindvalley", "Scarecrow");
        Core.MapItemQuest(6124, "coldwindvalley", 5551, 5);

        // Gather Bait
        Core.KillQuest(6125, "coldwindvalley", "Arctic Wolf");

        // Bait the Trap
        Core.KillQuest(6126, "coldwindvalley", "Ice Master Yeti");
        Core.MapItemQuest(6126, "coldwindvalley", 5552);

        // Gather Snowman Pieces
        Core.KillQuest(6127, "coldwindvalley", "Snow Golem");
        Core.MapItemQuest(6127, "coldwindvalley", 5553, 2);

        // Gather Snowman Decorations
        Core.KillQuest(6128, "coldwindvalley", "Coal Imp");
        Core.MapItemQuest(6128, "coldwindvalley", 5554);

        // Grab some Garb
        Core.KillQuest(6129, "coldwindvalley", "Frost Goblin");

        // Bait and Gifts
        Core.MapItemQuest(6130, "coldwindvalley", 5555);

        // Check out the Cave
        Core.KillQuest(6131, "coldwindvalley", "Arctusk");

        // Holly and Ice
        Core.KillQuest(6132, "coldwindvalley", "Snow Golem", hasFollowup: false);
        Core.MapItemQuest(6132, "coldwindvalley", 5557, 8, hasFollowup: false);
    }

        // --------------------------------------------------------------------------------------------------------------------------

        // The rest of the Frostval quests are not necessary for Frostval Barbaria. Can skip and farm Frozen Orb directly using jump.

        // --------------------------------------------------------------------------------------------------------------------------
}
