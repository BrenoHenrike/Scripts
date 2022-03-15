//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class Frostvale
{
    public CoreBots Core => CoreBots.Instance;
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface Bot)
    {
        Core.AcceptandCompleteTries = 5;
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
        if (Core.isCompletedBefore(906))
            return;

        Story.PreLoad();

        // Rescue Blizzy
        Story.KillQuest(155, "icecave", "Frosty");

        // Scary Snow Men
        Story.KillQuest(156, "icecave", "Snow Golem");

        // Moglin Popsicles
        Story.KillQuest(157, "icecave", "Frozen Moglin");

        // Crystal Spider
        Story.KillQuest(158, "icecave", "Ice Spider");

        // Fluffy Bears
        Story.KillQuest(159, "icecave", "Polar Bear");

        // Blue Eyed Beast
        Story.KillQuest(160, "icecave", "Frost Dragon");

        // Trouble Makers
        Story.KillQuest(161, "factory", "Sneevil Toy Maker");

        // Bad Ice Cream
        Story.KillQuest(162, "factory", "Snow Golem");

        // Greedy Sneevil
        Story.KillQuest(163, "factory", "Ebilsneezer");

        // Shadow Figure
        Story.KillQuest(164, "frost", "FrostScythe");

        // 'Twas the night before Frostval
        Story.KillQuest(456, "icecave", "Frosty");

        // Find Page 2
        Story.KillQuest(457, "icecave", "Frozen Moglin");
        Story.MapItemQuest(457, "yulgar", 85);

        // Find Page 3
        Story.KillQuest(458, "icecave", "Frozen Moglin");
        Story.MapItemQuest(458, "battleontown", 86);

        // Find Page 4
        Story.KillQuest(459, "factory", "Sneevil Toy Maker");

        // Find Page 5
        Story.KillQuest(460, "northlandlight", "Santy Claws");

        // Find Page 6
        Story.MapItemQuest(461, "battleontown", 87);
        Story.KillQuest(461, "icecave", "Frozen Moglin");

        // Spirit Abducted 
        Core.Join("frostvale");
        Core.ChainComplete(905);
    }

    public void SnowGlobe()
    {
        if (Core.isCompletedBefore(1508))
            return;

        Story.PreLoad();

        // Shaking the Globes
        Story.MapItemQuest(906, "snowglobe", 243, 10);
        Story.KillQuest(906, "snowglobe", "Snow Golem");

        //A Demonstration
        Story.KillQuest(907, "snowglobe", "Snow Golem");

        // Hearts of Ice
        Story.KillQuest(908, "snowglobe", "snowman Soldier");

        // Defeat Garaja
        Story.KillQuest(909, "snowglobe", "Garaja");

        // Springing Traps
        Story.KillQuest(910, "goldenruins", "Golden Warrior");
        Story.MapItemQuest(910, "goldenruins", 244, 10);

        // Frost Lions
        Story.KillQuest(911, "goldenruins", "Frost Lion");

        // Onslaught Keyrings
        Story.KillQuest(912, "goldenruins", "Golden Warrior");

        // Defeat Lionfang
        Story.KillQuest(913, "goldenruins", "Maximillian Lionfang");
    }

    public void Alpine()
    {
        if (Core.isCompletedBefore(1521))
            return;

        Story.PreLoad();

        // Snow Way to Know Where to Go
        Story.MapItemQuest(1508, "alpine", 758);

        // Arming the Undead Army
        Story.KillQuest(1509, "alpine", "Glacier Mole");

        // Cold As A Corpse
        Story.MapItemQuest(1510, "alpine", 759, 10);

        // Pretty Pretty Undead Princess Decor
        Story.MapItemQuest(1511, "alpine", 760, 13);

        // Deadfying Frost Lions
        Story.KillQuest(1512, "alpine", "Frost Lion");

        // Defiant Undead Deserters
        Story.KillQuest(1516, "alpine", "Frozen Deserter");

        // Forest Guadian Gauntlet
        Story.KillQuest(1513, "alpine", "Wendigo");

        // Snow Turning Back!
        Story.KillQuest(1519, "icevolcano", MonsterNames: new[] { "Snow Golem", "Dead-ly Ice Elemental" });
        Story.MapItemQuest(1519, "icevolcano", 761, 10);

        // Venom in Your Veins
        Story.KillQuest(1520, "icevolcano", "Ice Symbiote");

        // Song of the Frozen Heart
        Story.KillQuest(1521, "icevolcano", "Dead Morice");
    }

    public void SnowyVale()
    {
        if (Core.isCompletedBefore(2576))
            return;

        Story.PreLoad();

        Core.AddDrop("Ray of Hope", "Sands of Time");

        // Locate Kezeroth
        Story.MapItemQuest(2522, "snowyvale", 1584);

        // Chronoton Detection
        Story.KillQuest(2523, "snowyvale", "Polar Golem");

        // Core Knowledge
        Story.MapItemQuest(2524, "snowyvale", 1585, 6);

        // Temporal Revelation
        Story.MapItemQuest(2525, "snowyvale", 1586);

        // Before the Darkest Hour - Will continue after the QuestComplete tries end (idk how many it is but y[e])
        if (!Story.QuestProgression(2527))
        {
            Core.EnsureAccept(2526);
            Core.Join("frostdeep", "Enter", "Spawn");
            Bot.Sleep(2500);
            Core.EnsureComplete(2526);
        }

        // Heart of Ice
        Story.KillQuest(2527, "frostdeep", MonsterNames: new[] { "Polar Golem", "Polar Elemental" });

        // Absolute Zero Success
        Story.KillQuest(2528, "frostdeep", MonsterNames: new[] { "Temple Prowler", "Polar Elemental", "Polar Golem" });

        // Dirty Secret
        Story.KillQuest(2529, "frostdeep", MonsterNames: new[] { "Temple Prowler", "Polar Mole" });

        // Frozen Venom
        Story.KillQuest(2530, "frostdeep", MonsterNames: new[] { "Polarwyrm Rider", "Polar Spider" });

        // Rune-ing His Plan
        Story.KillQuest(2531, "frostdeep", "Ancient Golem");

        // Deadly Beauty
        Story.KillQuest(2532, "frostdeep", MonsterNames: new[] { "Polar Elemental", "Polar Golem", "Polar Golem" });

        // Cold-Hearted Trophies
        Story.KillQuest(2533, "frostdeep", MonsterNames: new[] { "Polar Mole", "Temple Prowler", "Temple Prowler" });

        // Warmth in the Cold
        Story.KillQuest(2534, "frostdeep", MonsterNames: new[] { "Temple Spider", "Temple Maggot" });

        // Icy Prizes
        Story.KillQuest(2535, "frostdeep", MonsterNames: new[] { "Temple Prowler", "Temple Maggot" });

        // Fading Magic - may bug out as its 2 items from 1 mob if the delay doesnt work idfk, doesnt work as a string[] as it gets the sand drop 
        Story.KillQuest(2536, "frostdeep", "Ancient Golem");
        Bot.Sleep(2500);
        Story.KillQuest(2536, "frostdeep", "Ancient Golem");
        //Story.KillQuest(2536, "frostdeep", MonsterNames: new[] { "Ancient Golem", "Ancient Golem" });

        // FrostDeep Dwellers
        Story.KillQuest(2537, "frostdeep", MonsterNames: new[] { "Polarwyrm Rider", "Polar Mole", "Polar Mole" });

        // A Breather
        Story.KillQuest(2538, "frostdeep", MonsterNames: new[] { "Polar Mole", "Temple Spider", "Polar Spider" });

        // Raiders From FrostDeep
        Story.KillQuest(2539, "frostdeep", MonsterNames: new[] { "Polar Draconian", "Temple Maggot" });

        // 8 Legged Frost Freaks
        Story.KillQuest(2540, "frostdeep", MonsterNames: new[] { "Temple Spider", "Polar Spider" });

        // Freezing the Stone
        Story.KillQuest(2541, "frostdeep", MonsterNames: new[] { "Ancient Golem", "Ancient Golem" });

        // Can You Feel the Chill Tonight?
        Story.KillQuest(2542, "frostdeep", MonsterNames: new[] { "Temple Prowler", "Polar Elemental", "Polar Elemental" });

        // Shrouded in Ice
        Story.KillQuest(2543, "frostdeep", MonsterNames: new[] { "Ancient Maggot", "Ancient Maggot" });

        // Hard Fight for a Cold Truth
        Story.KillQuest(2544, "frostdeep", MonsterNames: new[] { "Ancient Prowler", "Ancient Prowler" });

        // Sand and Shardin' Bones
        Story.KillQuest(2545, "frostdeep", MonsterNames: new[] { "Ancient Mole", "Ancient Mole" });

        // Older and Colder
        Story.KillQuest(2546, "frostdeep", MonsterNames: new[] { "Ancient Mole", "Ancient Prowler", "Ancient Maggot" });

        // The Sword Of Hope
        Story.KillQuest(2547, "frostdeep", MonsterNames: new[] { "Ancient Terror", "Ancient Terror" });
    }

    public void IceRise()
    {
        if (Core.isCompletedBefore(2582))
            return;

        Story.PreLoad();

        // A Little Warmth and Light
        Story.MapItemQuest(2576, "icerise", 1592, 5);

        // Behind Locked Doors
        Story.MapItemQuest(2577, "icerise", 1593);

        // The Lost Key
        Story.KillQuest(2578, "icerise", "Polar Golem");

        // Uncovering Pages Of The Past
        Story.KillQuest(2579, "icerise", MonsterNames: new[] { "Polar Golem", "Polar Elemental", "Arctic Direwolf" });

        // We Know Where To Look
        Story.KillQuest(2580, "icerise", MonsterNames: new[] { "Polar Golem", "Polar Elemental", "Arctic Direwolf" });

        // A Terrible Hiding Place
        Story.KillQuest(2581, "icerise", "Arctic Direwolf");

        // Face Kezeroth!
        Story.KillQuest(2582, "icerise", "Kezeroth");
    }

    public void ColdWindValley()
    {
        if (Core.isCompletedBefore(6132))
            return;

        Story.PreLoad();

        // Help Blizzy
        Story.MapItemQuest(6122, "coldwindvalley", 5547);
        Story.MapItemQuest(6122, "coldwindvalley", 5548);
        Story.MapItemQuest(6122, "coldwindvalley", 5549);
        Story.MapItemQuest(6122, "coldwindvalley", 5550);

        // Gather Ammunition
        Story.KillQuest(6123, "coldwindvalley", "Hail Elemental");

        // Arm the Mob
        Story.KillQuest(6124, "farm", "Scarecrow");
        Story.MapItemQuest(6124, "coldwindvalley", 5551, 5);

        // Gather Bait
        Story.KillQuest(6125, "coldwindvalley", "Arctic Wolf");

        // Bait the Trap
        Story.KillQuest(6126, "coldwindvalley", "Ice Master Yeti");
        Story.MapItemQuest(6126, "coldwindvalley", 5552);

        // Gather Snowman Pieces
        Story.KillQuest(6127, "coldwindvalley", "Snow Golem");
        Story.MapItemQuest(6127, "coldwindvalley", 5553, 2);

        // Gather Snowman Decorations
        Story.KillQuest(6128, "coldwindvalley", "Coal Imp");
        Story.MapItemQuest(6128, "coldwindvalley", 5554);

        // Grab some Garb
        Story.KillQuest(6129, "coldwindvalley", "Frost Goblin");

        // Bait and Gifts
        Story.MapItemQuest(6130, "coldwindvalley", 5555);

        // Check out the Cave
        Story.MapItemQuest(6131, "coldwindvalley", 5556);
        Story.KillQuest(6131, "coldwindvalley", "Arctusk");

        // Holly and Ice
        Story.KillQuest(6132, "coldwindvalley", "Snow Golem");
        Story.MapItemQuest(6132, "coldwindvalley", 5557, 8);
    }

    // --------------------------------------------------------------------------------------------------------------------------

    // The rest of the Frostval quests are not necessary for Frostval Barbaria. Can skip and farm Frozen Orb directly using jump.

    // --------------------------------------------------------------------------------------------------------------------------
}
