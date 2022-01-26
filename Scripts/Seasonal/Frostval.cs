//cs_include Scripts/CoreBots.cs
using RBot;

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
        Part8();
        Part9();
        Part10();
        Part11();

        Core.SetOptions(false);
    }

    public void Part1()
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

        // Find Page 6v
        Core.MapItemQuest(461, "battleontown", 87, hasFollowup: false);
        Core.KillQuest(461, "icecave", "Frozen Moglin", hasFollowup: false);

        // Spirit Abducted 
        Core.Join("frostval");
        Core.ChainComplete(905);
    }

    public void Part2()
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

    public void Part3()
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

    public void Part4()
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

    public void Part5()
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

    public void Part6()
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

    public void Part7()
    {
        if (Bot.Quests.IsUnlocked(3942))
            return;

        // Seek the Tower
        Core.MapItemQuest(3907, "frozentower", 3022);

        // A n-Ice Beginning
        Core.KillQuest(3908, "frozentower", "Polar Elemental");

        // Search for Syrrus
        Core.MapItemQuest(3909, "frozentower", 3019);

        // Building the Base
        Core.MapItemQuest(3910, "frozentower", 3004, 13);

        // Refugee Roundup
        Core.KillQuest(3911, "frozentower", MonsterNames: new[] { "Frostwyrm", "Frostwyrm" });

        // Retrieve the Water Starstone
        Core.KillQuest(3912, "frozentower", "FrostDeep Dweller");

        // Magical Attraction
        Core.MapItemQuest(3913, "frozentower", 3005, 13);

        // Frozen Blood
        Core.KillQuest(3914, "frozentower", "Twisted Ice");

        // Retrieve the Fire Starstone
        Core.MapItemQuest(3915, "frozentower", 3006);

        // Defend the Tower!
        Core.KillQuest(3916, "frozentower", "Polar Elemental");

        // Refugee Rescue Run
        Core.MapItemQuest(3917, "frozentower", 3007, 6);

        // Retrieve the Earth Starston
        Core.MapItemQuest(3918, "frozentower", 3013);

        // Polar Penetration and Progress
        Core.KillQuest(3919, "frozentower", "Polar Elemental");
        Core.MapItemQuest(3919, "frozentower", 3008, 6);

        // Save the Astronomer Apprentice
        Core.KillQuest(3920, "frozentower", new[] { "Polar Elemental", "Ice Wolf" } );
        Core.KillQuest(3920, "frozentower", "Ice Wolf");
        Core.MapItemQuest(3920, "frozentower", 3020); 
        
        // Glacial Elixir
        Core.KillQuest(3921, "frozentower", "FrostDeep Dweller");
        Core.MapItemQuest(3921, "frozentower", 3017, 6);

        // Retrieve the Energy Starstone
        Core.KillQuest(3922, "frozentower", "Polar Elemental");

        // Marking the Future
        Core.KillQuest(3923, "frozentower", "Frostwyrm");

        //Glacial Shift
        Core.MapItemQuest(3924, "frozentower", 3009, 6);

        // Divination Draft
        Core.KillQuest(3925, "frozentower", new[] { "Arctic Eel", "Frostwyrm" });
        Core.MapItemQuest(3925, "frozentower", 3012, 4);
        Core.MapItemQuest(3925, "frozentower", 3011, 4);

        // Retrieve the Light StarStone
        Core.MapItemQuest(3926, "frozentower", 3021, 4);

        // The Future is Bright
        Core.MapItemQuest(3927, "frozentower", 3014, 7);

        // Bled Bone Dry
        Core.KillQuest(3928, "frozentower", "Arctic Eel");

        // Chill of Fear
        Core.KillQuest(3929, "frozentower", "Polar Elemental");

        // Retrieve the Darkness Starstone
        Core.KillQuest(3930, "frozentower", "Twisted Ice");

        // Web of Fear
        Core.KillQuest(3931, "frozentower", "Frostwyrm");

        // Frozen in Time
        Core.MapItemQuest(3932, "frozentower", 3016, 13);

        // Heart of the Matter
        Core.KillQuest(3933, "frozentower", "Ice Wolf");

        // Retrieve the Wind Starstone
        Core.KillQuest(3934, "frozentower", "Rotten Ice");

        // Create the Gate
        Core.KillQuest(3935, "frozentower", "Ice Wolf");
        Core.MapItemQuest(3935, "frozentower", 3018, 13);

        // Drive Back the Invaders
        Core.KillQuest(3936, "frozentower", "Frost Invader");

        // Defeat the FrostSpawn Invaders
        Core.KillQuest(3937, "frozentower", "Frost Fangbeast", FollowupIDOverwrite: 3941);

        // FangBeast Bash-up
        Core.KillQuest(3941, "frozentower", "Frost Fangbeast", hasFollowup: false);
    }

    public void Part8()
    {
        if (Bot.Quests.IsUnlocked(3947))
            return;

        // FrozenRuins

        // Ravage the Reapers
        Core.KillQuest(3942, "frozenruins", "Frost Reaper");

        // Oh the Humanity
        Core.KillQuest(3943, "frozenruins", "Frost Reaper" ); 

        // Close the Gate
        Core.KillQuest(3944, "frozenruins", "Frost Reaper" );

        // Form the Lock
        Core.KillQuest(3945, "frozenruins", "Frozen Moglinster");

        Core.MapItemQuest(3945, "frozenruins", 3050, 10);
        // Glacera
        Core.KillQuest(3946, "frozenruins", "Frost Reaper", hasFollowup: false);
    }

    public void Part9()
    {
        if (Bot.Quests.IsUnlocked(3951))
            return;

        // Glaera     

        //A Frost Welcome
        Core.EnsureAccept(3947);        
        Core.MapItemQuest(3947, "glacera", 3048, 1);
        // Key to the Fortress
        Core.KillQuest(3948, "glacera", "Frost Invader");
        // Ravage the Reapers
        Core.MapItemQuest(3949, "glacera", 3049, 6);
        // Oh the Humanity
        Core.MapItemQuest(3950, "glacera", 3047, 1, hasFollowup: false);
    }

    public void Part10()
    {
        if (Bot.Quests.IsUnlocked(3958))
            return;

        // FrozenRuins encore

        // Rescue the Refugees
        Core.KillQuest(3951, "frozenruins", new[] { "Frost Invader", "Frozen Moglinster" } );
        // Defeat the Fangbeasts
        Core.KillQuest(3952, "frozenruins", "Frost Fangbeast");
        // Destroy the Frost Reapers
        Core.KillQuest(3953, "frozenruins", "Frost Reaper");
        // FrostSpawn General Takedown
        Core.KillQuest(3954, "frozenruins", "Frost General", hasFollowup: false);
    }

    public void Part11()
    {
        if (Bot.Quests.IsUnlocked(3971))
            return;

        // Northstar

        // From Refugee to Enemy
        Core.KillQuest(3958, "northstar", new[] { "Frost Invader", "Monstrous Refugee" });
        // Fangs and Blades
        Core.KillQuest(3959, "northstar", new[] { "Frost Fangbeast", "Frost Invader"} );
        // Reaping the Refugees
        Core.KillQuest(3960, "northstar", "Frost Reaper");
        // Saving Syrrus' Spirit
        Core.KillQuest(3961, "northstar", "Frost Reaper", FollowupIDOverwrite: 3972); //loadstone peice
        Core.MapItemQuest(3961, "northstar", 3060, 5, FollowupIDOverwrite: 3972); //plush bear
        Core.MapItemQuest(3961, "northstar", 3061, 7, FollowupIDOverwrite: 3972); //snowdrop blossom
        Core.MapItemQuest(3961, "northstar", 3073, 5, FollowupIDOverwrite: 3972); //journal page
        // It's a Trap!
        Core.MapItemQuest(3972, "northstar", 3063, 10);
        // Feast or Famine
        Core.KillQuest(3973, "northstar", new[] { "Frost Fangbeast", "Frost Invader", "Frost Reaper", "Frost Superreaper", "Monstrous Refugee"} );
        // Decipher the Freezing
        Core.KillQuest(3974, "northstar", new[] { "Frost Fangbeast", "Monstrous Refugee", "Frost Fangbeast", "Frost Invader", "Frost Reaper", "Monstrous Refugee" }, FollowupIDOverwrite: 3970);
        // A New Frost Monster
        Core.KillQuest(3970, "northstar", "The Queen's Gift");
        // Defeat Karok!
        Core.KillQuest(3971, "northstar", "Karok the Fallen", hasFollowup: false);



        // --------------------------------------------------------------------------------------------------------------------------

        // The rest of the Frostval quests are not necessary for Frostval Barbaria. Can skip and farm Frozen Orb directly using jump.

        // --------------------------------------------------------------------------------------------------------------------------
    }
}
