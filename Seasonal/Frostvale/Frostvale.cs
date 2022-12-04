//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Frostvale
{
    public CoreBots Core => CoreBots.Instance;
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        DoAll();

        Core.SetOptions(false);
    }

    public void DoAll()
    {
        if (!Core.isSeasonalMapActive("frostvale"))
        {
            Core.Logger($"it is Currently {DateTime.Now.ToString("MMMM")}, The Maps Will Be out In December, as per the Design Notes.");
            return;
        }
        IceCave();
        SnowGlobe();
        Alpine();
        SnowyVale();
        IceRise();
        ColdWindValley();
        BrightLights();
        Battlefield();
        Darkwinter();
        Frozensoul();
        Howardshill();
        Icerisepast();
        Winterhorror();
        //Gifthulu();
        Cryostorm();
        Icewindpass();
        Icepike();
    }

    public void IceCave()
    {
        if (Core.isCompletedBefore(906) || !Core.isSeasonalMapActive("icecave"))
            return;

        Story.PreLoad(this);

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
        Story.ChainQuest(905);
    }

    public void SnowGlobe()
    {
        if (Core.isCompletedBefore(1508) || !Core.isSeasonalMapActive("snowglobe"))
            return;

        Story.PreLoad(this);

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
        if (Core.isCompletedBefore(1521) || !Core.isSeasonalMapActive("alpine"))
            return;

        Story.PreLoad(this);

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
        Story.KillQuest(1519, "icevolcano", new[] { "Snow Golem", "Dead-ly Ice Elemental" });
        Story.MapItemQuest(1519, "icevolcano", 761, 10);

        // Venom in Your Veins
        Story.KillQuest(1520, "icevolcano", "Ice Symbiote");

        // Song of the Frozen Heart
        Story.KillQuest(1521, "icevolcano", "Dead Morice");
    }

    public void SnowyVale()
    {
        if (Core.isCompletedBefore(2576) || !Core.isSeasonalMapActive("snowyvale"))
            return;

        Story.PreLoad(this);

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
        // if (!Story.QuestProgression(2526))
        // {
        //     Core.EnsureAccept(2526);
        //     Core.Join("frostdeep");
        //     Bot.Wait.ForMapLoad("frostdeep");
        //     Core.GetMapItem(1587, 1, "frostdeep");
        //     Core.EnsureComplete(2526);
        // }
        Story.MapItemQuest(2526, "frostdeep", 1587, AutoCompleteQuest: false);

        // Heart of Ice
        Story.KillQuest(2527, "frostdeep", new[] { "Polar Golem", "Polar Elemental" });

        // Absolute Zero Success
        Story.KillQuest(2528, "frostdeep", new[] { "Temple Prowler", "Polar Elemental", "Polar Golem" });

        // Dirty Secret
        Story.KillQuest(2529, "frostdeep", new[] { "Temple Prowler", "Polar Mole" });

        // Frozen Venom
        Story.KillQuest(2530, "frostdeep", new[] { "Polarwyrm Rider", "Polar Spider" });

        // Rune-ing His Plan
        Story.KillQuest(2531, "frostdeep", "Ancient Golem");

        // Deadly Beauty
        Story.KillQuest(2532, "frostdeep", new[] { "Polar Elemental", "Polar Golem", "Polar Golem" });

        // Cold-Hearted Trophies
        Story.KillQuest(2533, "frostdeep", new[] { "Polar Mole", "Temple Prowler", "Temple Prowler" });

        // Warmth in the Cold
        Story.KillQuest(2534, "frostdeep", new[] { "Temple Spider", "Temple Maggot" });

        // Icy Prizes
        Story.KillQuest(2535, "frostdeep", new[] { "Temple Prowler", "Temple Maggot" });

        // Fading Magic - may bug out as its 2 items from 1 mob if the delay doesnt work idfk, doesnt work as a string[] as it gets the sand drop 
        if (!Story.QuestProgression(2536))
        {
            Core.EnsureAccept(2536);
            Core.HuntMonster("frostdeep", "Ancient Golem", "Sands of Time", 6);
            Core.HuntMonster("frostdeep", "Ancient Golem", "Obsidian Key", 2);
            Core.EnsureComplete(2536);
        }

        // FrostDeep Dwellers
        Story.KillQuest(2537, "frostdeep", new[] { "Polarwyrm Rider", "Polar Mole", "Polar Mole" });

        // A Breather
        Story.KillQuest(2538, "frostdeep", new[] { "Polar Mole", "Temple Spider", "Polar Spider" });

        // Raiders From FrostDeep
        Story.KillQuest(2539, "frostdeep", new[] { "Polar Draconian", "Temple Maggot" });

        // 8 Legged Frost Freaks
        Story.KillQuest(2540, "frostdeep", new[] { "Temple Spider", "Polar Spider" });

        // Freezing the Stone
        Story.KillQuest(2541, "frostdeep", new[] { "Ancient Golem", "Ancient Golem" });

        // Can You Feel the Chill Tonight?
        Story.KillQuest(2542, "frostdeep", new[] { "Temple Prowler", "Polar Elemental", "Polar Elemental" });

        // Shrouded in Ice
        Story.KillQuest(2543, "frostdeep", new[] { "Ancient Maggot", "Ancient Maggot" });

        // Hard Fight for a Cold Truth
        Story.KillQuest(2544, "frostdeep", new[] { "Ancient Prowler", "Ancient Prowler" });

        // Sand and Shardin' Bones
        Story.KillQuest(2545, "frostdeep", new[] { "Ancient Mole", "Ancient Mole" });

        // Older and Colder
        Story.KillQuest(2546, "frostdeep", new[] { "Ancient Mole", "Ancient Prowler", "Ancient Maggot" });

        // The Sword Of Hope
        Story.KillQuest(2547, "frostdeep", new[] { "Ancient Terror", "Ancient Terror" });
    }

    public void IceRise()
    {
        if (Core.isCompletedBefore(2582) || !Core.isSeasonalMapActive("icerise"))
            return;

        Story.PreLoad(this);

        // A Little Warmth and Light
        Story.MapItemQuest(2576, "icerise", 1592, 5);

        // Behind Locked Doors
        Story.MapItemQuest(2577, "icerise", 1593);

        // The Lost Key
        Story.KillQuest(2578, "icerise", "Polar Golem");

        // Uncovering Pages Of The Past
        Story.KillQuest(2579, "icerise", new[] { "Polar Golem", "Polar Elemental", "Arctic Direwolf" });

        // We Know Where To Look
        Story.KillQuest(2580, "icerise", new[] { "Polar Golem", "Polar Elemental", "Arctic Direwolf" });

        // A Terrible Hiding Place
        Story.KillQuest(2581, "icerise", "Arctic Direwolf");

        // Face Kezeroth!
        Story.KillQuest(2582, "icerise", "Kezeroth");
    }

    public void ColdWindValley()
    {
        if (Core.isCompletedBefore(6132) || !Core.isSeasonalMapActive("coldwindvalley"))
            return;

        Story.PreLoad(this);

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


    public void BrightLights()
    {
        if (Core.isCompletedBefore(8176) || !Core.isSeasonalMapActive("icestorm"))
            return;

        Story.PreLoad(this);

        // 6423|Gathering Information
        Story.MapItemQuest(6423, "icestorm", new[] { 5925, 5926, 5927 });

        // 6424|A Test of Alliance
        Story.MapItemQuest(6424, "lair", 5928);
        Story.KillQuest(6424, "lair", "Wyvern");

        // 6425|Inspiring the Young
        Story.KillQuest(6425, "icestorm", new[] { "Earth Dragonling", "Earth Dragonling" });

        // 6426|Following the Path
        Story.MapItemQuest(6426, "icestorm", 5929);
        Story.MapItemQuest(6426, "icestorm", 5930, 6);

        // 6427|Party Crashers
        Story.KillQuest(6427, "icestorm", "Dragon Hunter");

        // 6428|Protect the Dragonlings
        if (!Story.QuestProgression(6428))
        {
            Core.EnsureAccept(6428);
            Core.HuntMonster("icestorm", "Fire Dragonling", "Fire Dragonling Evacuated", 4);
            Core.HuntMonster("icestorm", "Wind Dragonling", "Wind Dragonling Evacuated", 4);
            Core.HuntMonster("icestorm", "Water Dragonling", "Water Dragonling Evacuated", 4);
            Core.HuntMonster("icestorm", "Earth Dragonling", "Earth Dragonling Evacuated", 4);
            Core.EnsureComplete(6428);
        }

        // 6429|The Ultimate Party Pooper
        Story.KillQuest(6429, "icestorm", "Duncan");

        // 6430|Returning the Gifts
        Story.MapItemQuest(6430, "icestorm", 5931, 4);

        // 8173|Bang, Smash, BOOM
        Story.KillQuest(8173, "brightlights", "Chaos Gemrald");

        // 8174|Fire and Fuses
        Story.KillQuest(8174, "brightlights", new[] { "Fire Imp", "Water Goblin" });

        // 8175|Light up the Night
        if (!Story.QuestProgression(8175))
        {
            Core.EnsureAccept(8175);
            Core.HuntMonster("kingcoal", "Frost King", "Gold Powder", 2);
            Core.HuntMonster("blindingsnow", "Nythera", "Indigo Powder", 5);
            Core.HuntMonster("northlandlight", "Santy Claws", "Red Powder", 5);
            Core.HuntMonster("brightlights", "Water Draconian", "Blue Powder", 15);
            Core.HuntMonster("brightlights", "HoliDrake", "Silver Powder", 2);
            Core.EnsureComplete(8175);
        }

        // 8176|Bright Lights Festival Rewards
        Story.KillQuest(8176, "brightlights", "Chaos Gemrald", GetReward: false);

    }

    public void Battlefield()
    {
        if (Core.isCompletedBefore(2575) || !Core.isSeasonalMapActive("battlefield"))
            return;

        Story.PreLoad(this);

        // Mana for the Magi 2570
        Story.KillQuest(2570, "newbie", "Slime", GetReward: false);

        // Gathering Spell Components 2571
        Story.KillQuest(2571, "hydra", "Fire Imp", GetReward: false);

        // Looking for Loggers 2572
        Story.KillQuest(2572, "farm", "Treeant", GetReward: false);

        // Ballista Cables 2573
        Story.KillQuest(2573, "orctown", "Horc Warrior", GetReward: false);

        // Arrowheads for Archers 2574
        Story.KillQuest(2574, "yokairiver", "Kappa Ninja", GetReward: false);

        // Fetching Fletching Feathers 2575
        Story.KillQuest(2575, "creatures", "Red Bird", GetReward: false);
    }


    public void Darkwinter()
    {
        if (Core.isCompletedBefore(3260) || !Core.isSeasonalMapActive("darkwinter"))
            return;

        //Good way | Yorumi & Einyuki Questline
        Core.ChangeAlignment(Alignment.Good);

        Story.PreLoad(this);

        // Feed the Greed 3217
        Story.KillQuest(3217, "darkwinter", new[] { "Blighted Moglin", "White Stalker", "Blighted Moglin" });
        // if (!Story.QuestProgression(3217))
        // {
        //     Core.EnsureAccept(3217);
        //     Core.HuntMonster("darkwinter", "Blighted Moglin", "Frostval Gift", 5);
        //     Core.HuntMonster("darkwinter", "White Stalker", "Frostval Decoration", 5);
        //     Core.HuntMonster("darkwinter", "Blighted Moglin", "Frostval Dessert", 5);
        //     Core.EnsureComplete(3217);
        // }

        // Sleet Samples 3218
        Story.KillQuest(3218, "darkwinter", "White Stalker");

        // Blighted Deer 3219
        Story.KillQuest(3219, "darkwinter", "Blighted Deer");

        // Frosty Hearts 3220
        Story.KillQuest(3220, "darkwinter", "Ice Golem");

        // On the Offensive 3221
        Story.KillQuest(3221, "darkwinter", "Legion Minion");

        // Inoculation 3222
        Story.MapItemQuest(3222, "darkwinter", new[] { 2280, 2281 }, 6);

        // A Different Way 3223
        Story.KillQuest(3223, "darkwinter", "Blighted Deer");

        // Breaking In 3257
        Story.MapItemQuest(3257, "darkwinter", 2315);

        // Break the Barrier 3258
        Story.KillQuest(3258, "darkwinter", "Ice Golem");

        // The Final Ward 3259
        Story.KillQuest(3259, "darkwinter", "Frost Golem");

        // Defeat Frostfang (Good) 3260 /Evil is the same
        Story.KillQuest(3260, "darkwinter", "Frost Fang");
    }

    public void Frozensoul()
    {
        if (Core.isCompletedBefore(7264) || !Core.isSeasonalMapActive("frozensoul"))
            return;

        Story.PreLoad(this);

        // Looks like quest is not unlocked behind anything
        // Ice Cold Killer 7262
        Story.KillQuest(7262, "frozensoul", "Frozen Minion", GetReward: false);

        // Get Jacked 7263
        Story.KillQuest(7263, "frozensoul", "Jack Frost", GetReward: false);

        // Shatter the FrozenSoul Queen 7264
        Story.KillQuest(7264, "frozensoul", "FrozenSoul Queen", GetReward: false);
    }

    public void Howardshill()
    {
        if (Core.isCompletedBefore(7854) || !Core.isSeasonalMapActive("howardshill"))
            return;

        Story.PreLoad(this);

        // Blizzy's
        // Find the Source 7843
        Story.KillQuest(7843, "howardshill", "Frozen Wisp");
        Story.MapItemQuest(7843, "howardshill", 7921);

        // Try the Door 7844
        Story.MapItemQuest(7844, "howardshill", 7922);

        // Find the Key 7845
        Story.KillQuest(7845, "howardshill", "Frozen Treeant");

        //Howard's
        // Till the Ground 7846
        Story.KillQuest(7846, "howardshill", "FrostBite");

        // Beautiful Blossoms 7847
        Story.KillQuest(7847, "howardshill", "Chillybones");

        // Moldy Trees 7848
        Story.KillQuest(7848, "howardshill", "Frozen Treeant");

        // Ichor for Elixir 7849
        Story.KillQuest(7849, "howardshill", "Chillybones");

        // Frozen Tears 7850
        Story.KillQuest(7850, "howardshill", "Chillybones");

        // Keep them Away 7851
        Story.KillQuest(7851, "howardshill", "FrostBite");

        // Light up the Darkness 7852
        Story.KillQuest(7852, "howardshill", "Frozen Wisp");

        // Return to Blizzy 7853
        Story.KillQuest(7853, "howardshill", "Chillybones");
        Story.MapItemQuest(7853, "howardshill", 7924);

        // Howard's Grief 7854
        Story.KillQuest(7854, "howardshill", "Howard's Grief");
    }

    public void Icerisepast()
    {
        if (!Core.IsMember || Core.isCompletedBefore(3904) || !Core.isSeasonalMapActive("Icerisepast"))
            return;

        Story.PreLoad(this);

        // Through the pass 3899
        Story.KillQuest(3899, "icerisepast", "Ice Wolf");

        // Higher Passes 3900
        Story.KillQuest(3900, "icerisepast", new[] { "Ice Bear", "Ice Bear", "Ice Bear" });

        // Bears? 3901
        Story.MapItemQuest(3901, "icerisepast", 2987);

        // In the Den 3902
        Story.KillQuest(3902, "icerisepast", "Guard Drumlin");

        // The Camp 3903
        Story.KillQuest(3903, "icerisepast", "Drumlin");

        // Fire from the Hole 3904
        Story.KillQuest(3904, "icerisepast", "Ice Drumlinster");
    }

    public void Winterhorror()
    {
        if (Core.isCompletedBefore(7859) || !Core.isSeasonalMapActive("winterhorror"))
            return;

        Story.PreLoad(this);

        // Monster Gems 7856
        Story.KillQuest(7856, "winterhorror", "Chillybones");

        // Mega Monster Gems 7857
        Story.KillQuest(7857, "winterhorror", "FrostBite");

        // Oh Heck! 7858
        Story.KillQuest(7858, "winterhorror", "Arthur and Elise");

        // He's Ragin' 7859
        Story.KillQuest(7859, "winterhorror", $"Howard’s Rage");
    }

    public void Gifthulu()
    {
        if (!Core.isSeasonalMapActive("gifthulu"))
            return;
        //Not avaiable
        //There is no quests over here
    }


    public void Cryostorm()
    {
        if (Core.isCompletedBefore(4716) || !Core.isSeasonalMapActive("cryostorm"))
            return;

        Story.PreLoad(this);

        // Plans for Frostval
        Story.MapItemQuest(4705, "cryostorm", 4069);
        Story.MapItemQuest(4705, "cryostorm", 4070);
        Story.KillQuest(4705, "cryostorm", "Glacial Elemental");

        // Find the Missing Presents
        Story.MapItemQuest(4706, "cryostorm", 4067, 8);

        // More Gifts
        Story.KillQuest(4707, "cryostorm", new[] { "Glacial Wolf", "Cryo Mammoth", "Glacial Elemental" });

        // Warmth for the Small
        Story.KillQuest(4708, "cryostorm", "Glacial Wolf");

        // Cut Down the Tree
        Story.MapItemQuest(4709, "cryostorm", 4068);
        Story.KillQuest(4709, "cryostorm", "Glacial Wolf");

        // Decorate the Tree
        if (!Story.QuestProgression(4710))
        {
            Core.EnsureAccept(4710);
            Core.HuntMonster("cryostorm", "Cryo Mammoth", "Gilded Moglin Ornament", 2);
            Core.HuntMonster("cryostorm", "Glacial Elemental", "Frosty Wreath", 3);
            Core.HuntMonster("cryostorm", "Glacial Wolf", "Frostval Cane", 5);
            Core.HuntMonster("cryostorm", "Cryo Mammoth", "Frostval Bells", 7);
            Core.HuntMonster("cryostorm", "Glacial Elemental", "Frostval Lights", 10);
            Core.EnsureComplete(4710);
        }

        // Find the Ice StarStone
        Story.KillQuest(4711, "cryostorm", "Behemoth");

        // War Medal Quest
        if (!Core.isCompletedBefore(4716))
        {
            Core.EnsureAccept(4712);
            Core.HuntMonster("cryowar", "Frost Reaper", "Cryo War Medal", 10);
            Core.EnsureComplete(4712);
        }

        // Defeat Ultra Karok
        Story.KillQuest(4716, "cryowar", "Super-Charged Karok");
    }

    public void Icewindpass()
    {
        if (Core.isCompletedBefore(5596) || !Core.isSeasonalMapActive("icewindpass"))
            return;

        Story.PreLoad(this);

        // Where is Karok?
        Story.MapItemQuest(5587, "icewindpass", 5074, 5);

        // Cloaking Spell
        Story.KillQuest(5588, "icewindpass", "Glacial Elemental");

        // Splattered Mana
        Story.MapItemQuest(5589, "icewindpass", 5075, 5);
        Story.KillQuest(5589, "icewindpass", "Glacial Elemental");

        // Dispell the Spell
        Story.KillQuest(5590, "icewindpass", "Living Snow");

        // Catch Up to Karok
        Story.KillQuest(5591, "icewindpass", "Frost Invader");

        // Blast the Frostspawn Symbiote
        Story.KillQuest(5592, "icewindpass", "Frostspawn Symbiote");

        // Keep Going!
        Story.KillQuest(5593, "icewindpass", "Frost Invader");

        // Take it Down!
        Story.KillQuest(5594, "icewindpass", "Frostspawn Horror");

        // Keep the Frostspawn Away!
        Story.KillQuest(5595, "icewindpass", new[] { "Frostspawn Troll", "Frost Invader" });

        // Take a Break from Fighting 
        Story.KillQuest(5596, "icewindpass", new[] { "Polar Golem", "Glacial Elemental" });
    }

    public void Icepike()
    {
        if (Core.isCompletedBefore(5617) || !Core.isSeasonalMapActive("icepike"))
            return;

        Story.PreLoad(this);

        // Fight For Kezeroth!
        if (!Bot.Quests.IsUnlocked(5606))
        {
            Core.EnsureAccept(5597);
            Core.HuntMonster("icewindwar", "Kezeroth's Blade", "Frostspawn Medal", 10);
            Core.EnsureComplete(5597);
        }

        // WHAT is THAT?
        Story.KillQuest(5601, "icewindwar", "Soricomorpha");

        // Take a Look Around
        Story.MapItemQuest(5606, "icepike", 5085, 2);
        Story.KillQuest(5606, "icepike", "Living Ice");

        // The Stars Have Foretold
        Story.MapItemQuest(5607, "icepike", new[] { 5086, 5087 });

        // Continue this Path
        Story.MapItemQuest(5608, "icepike", 5088, 5);
        Story.KillQuest(5608, "icepike", "Ice Lord");

        // Cross the Ice Bridge
        Story.MapItemQuest(5609, "icepike", 5089);

        // Free The Moglins
        Story.KillQuest(5610, "icepike", "Frozen Moglin");

        // Get the Moglinsters
        Story.KillQuest(5612, "icepike", "Frozen Moglinster");

        // Take the Crystal
        Story.MapItemQuest(5613, "icepike", 5090);

        // You have to Fight
        Story.KillQuest(5614, "icepike", "Crystal of Glacera");

        // Fight your way Clear
        Story.MapItemQuest(5615, "icepike", 5091);

        // Take down Kezeroth!
        Story.KillQuest(5616, "icepike", "Chained Kezeroth");

        // Karok still Stands
        Story.KillQuest(5617, "icepike", "Karok the Fallen");
    }



    // --------------------------------------------------------------------------------------------------------------------------

    // The rest of the Frostval quests are not necessary for Frostval Barbarian. Can skip and farm Frozen Orb directly using jump.

    // --------------------------------------------------------------------------------------------------------------------------
}
