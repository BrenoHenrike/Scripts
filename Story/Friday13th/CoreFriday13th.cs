//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class CoreFriday13th
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Core.RunCore();

        Core.SetOptions(false);
    }

    public void CompleteFriday13th()
    {
        Skullpunch();
        Lowe();
        Saloonfront();
        Firehouse();
        Sleezter();
        Neverhub();
        Battledoom();
        Wormhole();
        Crownsreachfxiii();
        Gonnagetcha();
        Greymoor();
        Puzzlebox();
        Splatterwar();
        Deadfly();
        Oddities();
    }

    public void Skullpunch()
    {
        if (Core.isCompletedBefore(3119))
            return;

        if (!Core.IsMember && !CalculateFriday13())
        {
            Core.Logger("You must be Member or wait until Friday13th to complete Skullpunch.");
            return;
        }

        Story.PreLoad(this);

        // Fishbones, Fishbones 3097
        Story.KillQuest(3097, "Skullpunch", "Fishbones");

        // Outlook Good 3098
        Story.MapItemQuest(3098, "Skullpunch", 1990);

        // Roly Poly Fishbones 3099
        Story.KillQuest(3099, "Skullpunch", "Fishbones");

        // We Can't Stop Here! 3100
        Story.KillQuest(3100, "Vampirates", "Creepy Bat");

        // It's Bat Country! 3101
        Story.KillQuest(3101, "Vampirates", "Creepy Bat");

        // Vampirates! 3102
        Story.KillQuest(3102, "Vampirates", "Vampirate");

        // Oh Right, They're Vampires 3103
        Story.KillQuest(3103, "Vampirates", new[] { "Creepy Bat", "Vampirate" });

        // The Vampirate Captain 3104
        Story.MapItemQuest(3104, "Vampirates", 1959);

        // How Are We Breathing Under Here!? 3105
        Story.KillQuest(3105, "Vampirates", "Stranglerfish");

        // Unsink This Ship! 3106
        Story.KillQuest(3106, "Vampirates", new[] { "Murderaconian", "Vampirate", "Vampirate" });

        // Release the Kraken! Wait, no - Don't. 3107
        Story.KillQuest(3107, "Vampirates", "Bracken Kraken");

        // I Didn't Know They Could Swarm! 3108
        Story.KillQuest(3108, "Vampirates", "Vampirate");

        // Now Where Are Those Landmarks? 3109
        Story.MapItemQuest(3109, "TreasureIsland", new[] { 1986, 1987, 1988, 1989 });

        // X Marks The Spot 3110
        Story.MapItemQuest(3110, "TreasureIsland", 1958);

        // Captain Von Poach 3111
        Story.KillQuest(3111, "TreasureIsland", "Captain Von Poach");

        //Me Knickers Got a Big Hole Across th' Bum 3114
        Story.KillQuest(3114, "Skullpunch", new[] { "Vampirate", "Fishbones" });

        // Where'd Me Ship's Cargo? 3115
        Story.KillQuest(3115, "Skullpunch", new[] { "Fishbones", "Fishbones", "Fishbones", "Fishbones" });

        // A Most Important Package 3116
        Story.KillQuest(3116, "Skullpunch", "Fishbones");

        // Comfort Food 3117
        Story.KillQuest(3117, "Skullpunch", new[] { "Shelleton", "Fishwing" });

        // Full o' Holes 3118
        Story.KillQuest(3118, "Skullpunch", "Shelleton");

        // Keelhaulin' a Kraken! 3119
        Story.KillQuest(3119, "ChaosKraken", "Chaos Kraken");
    }

    public void Lowe()
    {
        if (Core.isCompletedBefore(764))
            return;

        if (!Core.IsMember && !CalculateFriday13())
        {
            Core.Logger("You must be Member or wait until Friday13th to complete Lowe.");
            return;
        }

        Story.PreLoad(this);

        // Listen to George Lowe's story 750
        Story.ChainQuest(750);

        // Ladies First 751
        Story.KillQuest(751, "Brain", "Slimed Girl");

        // Slimy Boys 752
        Story.KillQuest(752, "Brain", "Slimed Boy");

        // The Slime Masquerade 753
        Story.KillQuest(753, "Brain", "Brain Slurper");

        // ONE OF THEM 754
        Story.MapItemQuest(754, "Brain", 132);

        // Spilt Salt 755
        Story.KillQuest(755, "Brain", "Slimed Girl|Slimed Boy|Brain Slurper");

        // Defeat the Giant Brain Slime Prime 756
        Story.ChainQuest(756);

        // The First 6 Chapters 757
        Story.KillQuest(757, "Ebildread", new[] { "Pink Ghostly Sheet", "Pink Ghostly Sheet", "Pink Ghostly Sheet", "Pink Ghostly Sheet", "Pink Ghostly Sheet", "Pink Ghostly Sheet" });

        // The Last 6 Chapters 758
        Story.KillQuest(758, "Ebildread", new[] { "Pink Ghostly Soldier", "Pink Ghostly Soldier", "Pink Ghostly Soldier", "Pink Ghostly Soldier", "Pink Ghostly Soldier", "Pink Ghostly Soldier" });

        // The Glossary 759
        Story.KillQuest(759, "Ebildread", "Jay Sun");

        // Cover Me 760
        Story.KillQuest(760, "Ebildread", "Pink Hand");

        // Dweam-Maze 761
        Story.ChainQuest(761);

        // Big Bad Kwueger Man 762
        Story.ChainQuest(762);

        // George Lowe-viathan 763
        Story.ChainQuest(763);

        // Super George Lowe-viathan 764
        Story.KillQuest(764, "SuperLowe", "Super Lowe-Viathan");
    }

    public void Saloonfront()
    {
        if (Core.isCompletedBefore(1057))
            return;

        if (!Core.IsMember && !CalculateFriday13())
        {
            Core.Logger("You must be Member or wait until Friday13th to complete Saloon Front.");
            return;
        }

        Story.PreLoad(this);

        // Deady's Return 1042
        Story.ChainQuest(1042);

        // Exit, Stage Right 1043
        Story.MapItemQuest(1043, "Fivesaloon", 406);

        // The Wedge 1044
        Story.KillQuest(1044, "Fivesaloon", "Bulletless Bandit");

        // Freedom? 1045
        Story.MapItemQuest(1045, "Fivesaloon", 407);

        // Other Ideas?! 1046
        Story.KillQuest(1046, "Fivesaloon", "Storagebox");

        // Got a Light 1047
        Story.KillQuest(1047, "Fivesaloon", "Bulletless Bandit");

        // BADABOOM! 1048
        Story.MapItemQuest(1048, "Fivesaloon", 408);

        // Lack of Subtlety 1049
        Story.KillQuest(1049, "Fivesaloon", "One-Armed Bandit");

        // Look Around 1050
        Story.MapItemQuest(1050, "Train", 409);

        // Caboose Key 1051
        Story.KillQuest(1051, "Train", "Ghostly Conductor");

        // Slow your Role 1052
        Story.MapItemQuest(1052, "Train", 410);

        // Slow the Coal 1053
        Story.KillQuest(1053, "Train", "Coal Ghoul");

        // Laying Traps 1054
        Story.MapItemQuest(1054, "Blackstone", 411, 12);

        // Gathering Mats 1055
        Story.MapItemQuest(1055, "Blackstone", 412, 12);

        // Pirate Hats 1056
        Story.KillQuest(1056, "Blackstone", "Necrosian Soldier");

        // That's a Wrap 1057
        Story.KillQuest(1057, "Blackstone", "Yahorneth");
    }

    public void Firehouse()
    {
        if (Core.isCompletedBefore(1564))
            return;

        if (!Core.IsMember && !CalculateFriday13())
        {
            Core.Logger("You must be Member or wait until Friday13th to complete Firehouse.");
            return;
        }

        Story.PreLoad(this);

        // Gaining Trust 1552
        Story.KillQuest(1552, "FireTown", new[] { "Fire Elemental", "Fire Elemental", "Fire Elemental" });

        // If You Can't Stand the Heat... 1553
        Story.KillQuest(1553, "FireTown", "Fire Elemental");

        // Retrieving Recollections 1554
        Story.MapItemQuest(1554, "FireTown", 790, 10);

        // Under Orders 1555
        Story.KillQuest(1555, "FireRiver", "Swamp Thing");

        // Locket Holds the Key 1556
        Story.KillQuest(1556, "FireRiver", "Lava Bat|Lava Garou");

        // Plans Fit for a King 1557
        Story.MapItemQuest(1557, "FireRiver", 792, 10);

        // Bound by Fire 1558
        Story.KillQuest(1558, "FireTunnel", "Volcanic Ash Imp");

        // Heart of Fire 1559
        Story.MapItemQuest(1559, "FireTunnel", 791, 10);

        // Spirit of a Dragon 1560
        Story.KillQuest(1560, "FireTunnel", "Elder Magma Wyrm");

        // Initiate Shutdown Sequence 1561
        Story.ChainQuest(1561);

        // Aura of Dragon's Flame 1562
        Story.KillQuest(1562, "FireTunnel", "Elder Magma Wyrm");

        // Spirit of the Black Unicorn 1563
        if (!Story.QuestProgression(1563))
        {
            Core.EnsureAccept(1563);
            Core.HuntMonster("firetown", "Obsidian Golem", "Heart of Stone", 5);
            Core.HuntMonster("firetown", "Fire Spirit", "Fiery Will", 5);
            Core.HuntMonster("fireriver", "Loup-Garou", "Primal Spirit", 5);
            Core.EnsureComplete(1563);
        }

        // Tie a Black Ribbon 'Round an Old Burnt Tree 1564
        Story.KillQuest(1564, "FireTown", "Burnt Tree");
    }

    public void Sleezter()
    {
        if (Core.isCompletedBefore(1972))
            return;

        if (!Core.IsMember && !CalculateFriday13())
        {
            Core.Logger("You must be Member or wait until Friday13th to complete Sleezter.");
            return;
        }

        Story.PreLoad(this);

        // Make Way 1961
        Story.KillQuest(1961, "Sleezter", "BunnyMinion");

        // Black Pelt Test 1962
        Story.KillQuest(1962, "Sleezter", "BunnyMinion");

        // Network Fiber Spool 1963
        Story.MapItemQuest(1963, "Sleezter", 973, 6);

        // Guess What in a Guess Where 1964
        Story.MapItemQuest(1964, "Sleezter", 974);

        // Splitting Hares 1965
        Story.KillQuest(1965, "Sleezter", "BunnyMinion");

        // Sleezter Bunny's Quarters 1966
        Story.MapItemQuest(1966, "Sleezter", 975);

        // The Sleezter Bunivinci Code 1967
        Story.KillQuest(1967, "Sleezter", "BunnyMinion");

        // Take Sleezter Bunny Out! 1968
        Story.KillQuest(1968, "Sleezter", "Sleezter Bunny");

        // Mantis Ray Repair 1969
        Story.MapItemQuest(1969, "Sleezter", 976, 6);

        // A Map Wouldn't Hurt 1970
        Story.KillQuest(1970, "Sleezter", "BunnyMinion");

        // Endin' Minions 1971
        Story.MapItemQuest(1971, "Sleezter", 977);

        // Breaking Bad Eggs 1972
        Story.MapItemQuest(1972, "Sleezter", 978);

        // Mantis Ray
        Story.ChainQuest(2021);
    }

    public void Neverhub()
    {
        if (Core.isCompletedBefore(2234))
            return;

        if (!Core.IsMember && !CalculateFriday13())
        {
            Core.Logger("You must be Member or wait until Friday13th to complete Neverlore.");
            return;
        }

        Story.PreLoad(this);

        // Find the Shadow's Door 2222
        Story.MapItemQuest(2222, "neverlore", 1315);

        // One-way Trip to Neverworld 2223
        Story.KillQuest(2223, "neverlore", "Whablobble");

        // Slash the Shadows 2224
        Story.KillQuest(2224, "Neverworld", "Spid-Squider");

        // Deprogramming for Dummies 2225
        Story.MapItemQuest(2225, "Neverworld", 1316, 10);
        Story.KillQuest(2225, "Neverworld", "Snackistopheles");

        // Guardian of the Lab-rary 2226
        Story.MapItemQuest(2226, "Neverworld", 1317);
        Story.KillQuest(2226, "Neverworld", new[] { "Snackistopheles", "Spid-Squider", "Snackistopheles", "Spid-Squider", "Fishizzle" });

        // Quick switch! 2227
        Story.MapItemQuest(2227, "Neverworld", 1318, 5);
        Story.MapItemQuest(2227, "Neverworld", 1326);

        // We're BOOM-ed! 2228
        Story.MapItemQuest(2228, "Neverworld", 1321);

        // De-Generation Situation 2229
        Story.KillQuest(2229, "Neverworld", "Generator");

        // Shielded Against the Shadows 2230
        Story.MapItemQuest(2230, "Neverworld", 1319);
        Story.KillQuest(2230, "Neverworld", "Fishizzle");

        // Making a Many-armed Army 2231
        Story.KillQuest(2231, "Neverworld", "Kennel Door");

        // Kreature Kibble  Khaos 2232
        Story.MapItemQuest(2232, "Neverworld", 1320, 10);

        // The Gang's Not All Here 2233
        Story.KillQuest(2233, "Neverworld", "Fishizzle");

        // Beast Maker Beat-down 2234
        Story.ChainQuest(2234);
    }

    public void Battledoom()
    {
        if (Core.isCompletedBefore(4656))
            return;

        if (!Core.IsMember && !CalculateFriday13())
        {
            Core.Logger("You must be Member or wait until Friday13th to complete Battledoom.");
            return;
        }

        Core.BankingBlackList.AddRange(new[] { "Unlucky Gem I", "Unlucky Gem II", "Unlucky Gem III",
                                               "Unlucky Gem IV", "Unlucky Gem V", "Unlucky Gem VI",
                                               "Unlucky Gem VII", "Cursed Mirror of Enutrof", "Shadowglass Shard" });

        if (Core.CheckInventory("Cursed Mirror of Enutrof"))
            return;

        Story.LegacyQuestManager(QuestLogic, 4648, 4649, 4650, 4651, 4652, 4653, 4654, 4655, 4656);

        void QuestLogic()
        {
            switch (Story.LegacyQuestID)
            {
                case 4648: // Shadowy Reconnaissance 4648
                    Core.HuntMonster("Battledoom", "Shadow Slime", "Shadow Slime Defeated", 5);
                    Core.HuntMonster("Battledoom", "Shadow Flying Eye", "Shadow Eyeball Defeated", 3);
                    break;

                case 4649: // Slippery Shadows 4649
                    Core.HuntMonster("Battledoom", "Shadow Skelly", "Necronomicon Page", 6);
                    break;

                case 4650: // Through the Looking-Glass 4650
                    Core.HuntMonster("Battledoom", "Shadow Skelly", "Mirror Fragment Retrieved");
                    break;

                case 4651: // Necro-Polished 4651
                    Core.GetMapItem(3976, 1, "necropolis");
                    Core.HuntMonster("Battledoom", "Shadow Skelly", "Shadow Skeletons Defeated", 13);
                    break;

                case 4652: // Cavernous Chaos 4652
                    Core.HuntMonster("NecroCavern", "Shadow Imp", "Mirror Fragment Found");
                    Core.HuntMonster("NecroCavern", "ShadowStone Elemental", "Mirror Fragment Located");
                    break;

                case 4653: // Mirror, Mirror, Off the Wall 4653
                    Core.GetMapItem(3975, 1, "battleoff");
                    Core.HuntMonster("Battleoff", "Evil Moglin", "Evil Moglin Defeated", 3);
                    break;

                case 4654: // To the Underworld! 4654
                    Core.HuntMonster("Underworld", "Undead Legend", "Mirror Fragment Obtained");
                    Core.HuntMonster("Underworld", "Klunk", "Mirror Fragment Acquired");
                    break;

                case 4655: // Shadow of Corruption 4655
                    Core.HuntMonster("CelestialRealm", "Shadow Beast", "Final Mirror Fragment Found");
                    break;

                case 4656: // Hunt for Shadowglass Shards 4656
                    Core.HuntMonster("Battledoom", "Shadow Skelly", "Shadow Skeleton Defeated", 5);
                    Core.HuntMonster("Battledoom", "Shadow Slime", "Shadow Slime Defeated", 5);
                    Core.HuntMonster("Battledoom", "Shadow Flying Eye", "Shadow Eyeball Defeated", 5);
                    Core.HuntMonster("Battledoom", "Shadow Beast", "Shadow Beast Defeated", 5);
                    break;
            }
        }
    }

    public void Wormhole()
    {
        if (Core.isCompletedBefore(5066))
            return;

        if (!Core.IsMember && !CalculateFriday13())
        {
            Core.Logger("You must be Member or wait until Friday13th to complete Wormhole.");
            return;
        }

        Story.PreLoad(this);

        // Something In The Air 5051
        Story.KillQuest(5051, "Wormhole", new[] { "Goth Girl", "Vamp Boy" });

        // They're Not Red Shirts - They're Deadshirts! 5052
        Story.MapItemQuest(5052, "Wormhole", new[] { 4420, 4421, 4422 });
        Story.KillQuest(5052, "Wormhole", "Deadshirt");

        // Curses! 5053
        Story.KillQuest(5053, "Wormhole", "Cursed Alien");

        // Computers Don't Lie 5054
        Story.MapItemQuest(5054, "Wormhole", new[] { 4423, 4424, 4425, 4426 });

        // Beam Me Up! 5055
        Story.KillQuest(5055, "Wormhole", new[] { "Undead Astronaut", "Undead Astronaut" });

        // Exotic Pets 5056
        Story.KillQuest(5056, "Wormhole", "Xenodog");

        // Not Trobbles 5057
        Story.KillQuest(5057, "Wormhole", "Green Trobbolier");

        // System Failure! 5058
        Story.MapItemQuest(5058, "Wormhole", 4428);

        // I Think I Saw This Movie Once 5059
        Story.KillQuest(5059, "Wormhole", "Space Horror");

        // I Have A Short Fuse 5060
        Story.MapItemQuest(5060, "Wormhole", 4427, 6);
        Story.KillQuest(5060, "Wormhole", "Volatile Current");

        // More Trobboliers 5061
        Story.KillQuest(5061, "Wormhole", "Green Trobbolier");

        // Maybe These Guys Know Something 5062
        Story.KillQuest(5062, "Wormhole", new[] { "Stormslasher", "Undead Space Marine" });

        // Look At That Awesome Armor! 5063
        Story.KillQuest(5063, "Wormhole", "Stormslasher");

        // Guardians of the Warehouse 5064
        Story.KillQuest(5064, "Wormhole", "Guardian");

        // Undead Space 5065
        Story.KillQuest(5065, "Wormhole", new[] { "Space Ghost", "Singularity" });

        // Trobbolegion! 5066
        Story.KillQuest(5066, "Wormhole", "Trobbolegion");
    }

    public void Crownsreachfxiii()
    {
        if (Core.isCompletedBefore(5646))
            return;

        if (!Core.IsMember && !CalculateFriday13())
        {
            Core.Logger("You must be Member or wait until Friday13th to complete Crownsreach FXIII.");
            return;
        }

        Story.PreLoad(this);

        // Those Bloody Maggots 5637
        Story.MapItemQuest(5637, "CrownsReachFXIII", 5115, 8);
        Story.KillQuest(5637, "CrownsReachFXIII", "Blood Maggot");

        // Purple is the Best Color 5639
        Story.MapItemQuest(5639, "Safiria", 5114);
        Story.KillQuest(5639, "Safiria", "Albino Bat");

        // Get Some Candles 5640
        Story.KillQuest(5640, "BattleUnderA", "Skeletal Fire Mage");

        // Time For a Break 5641
        Story.MapItemQuest(5641, "CrownsReachFXIII", 5116);
        Story.KillQuest(5641, "CrownsReachFXIII", "Vampire Bat");

        // Call the Exterminator 5642
        Story.KillQuest(5642, "CrownsReachFXIII", "Eldritch Parasite");

        // Calm the Villagers 5643
        Story.KillQuest(5643, "CrownsReachFXIII", "Panicking Villager");

        // Clean up the Ooze 5644
        Story.KillQuest(5644, "CrownsReachFXIII", "Crawling Ooze");

        // They Stole My Stuff! 5645
        Story.KillQuest(5645, "CrownsReachFXIII", "Crawling Ooze");

        // Ia! Ia! Shub'hathrys Fhtagn! 5646
        Story.KillQuest(5646, "CrownsReachFXIII", "Shub-Hathrys");
    }

    public void Gonnagetcha()
    {
        if (Core.isCompletedBefore(6269))
            return;

        if (!Core.IsMember && !CalculateFriday13())
        {
            Core.Logger("You must be Member or wait until Friday13th to complete Gonnagetcha.");
            return;
        }

        Story.PreLoad(this);

        // Who Screamed? 6259
        Story.MapItemQuest(6259, "Gonnagetcha", 5735);
        Story.KillQuest(6259, "Gonnagetcha", "Vengeful Ghost");

        // Check the Other Cabin 6260
        Story.MapItemQuest(6260, "Gonnagetcha", 5736, 3);
        Story.KillQuest(6260, "Gonnagetcha", "Restless Spirit");

        // Check the Staff Building 6261
        Story.MapItemQuest(6261, "Gonnagetcha", 5737);
        Story.KillQuest(6261, "Gonnagetcha", "Restless Spirit");

        // What are They Looking For? 6262
        Story.MapItemQuest(6262, "Gonnagetcha", new[] { 5738, 5739 });

        // Cysero was Right! 6263
        Story.KillQuest(6263, "Gonnagetcha", new[] { "Murkonian", "Murkonian" });

        // Explore the Black Knight's Cabin 6264
        Story.MapItemQuest(6264, "Gonnagetcha", new[] { 5740, 5741 });
        Story.KillQuest(6264, "Gonnagetcha", "Black Knight Spirit");

        // Get Through the Mirror 6265
        Story.MapItemQuest(6265, "Gonnagetcha", new[] { 5742, 5743 });

        // Rescue the Campers 6266
        Story.MapItemQuest(6266, "Gonnagetcha", 5744, 6);
        Story.KillQuest(6266, "Gonnagetcha", "Shrade Cultist");

        // Stop the Ringleader 6267
        Story.KillQuest(6267, "Gonnagetcha", "Bride of Shrade");

        // Defeat Shrade! 6268
        Story.KillQuest(6268, "Gonnagetcha", "Shrade");

        // Revitalize the Camp 6269
        Story.KillQuest(6269, "Gonnagetcha", new[] { "Vengeful Ghost", "Shrade Cultist" });
    }

    public void Greymoor()
    {
        if (Core.isCompletedBefore(6420))
            return;

        if (!Core.IsMember && !CalculateFriday13())
        {
            Core.Logger("You must be Member or wait until Friday13th to complete Greymoor.");
            return;
        }

        Story.PreLoad(this);

        // Tired of Gremlins 6409
        Story.KillQuest(6409, "Greymoor", new[] { "Auto Gremlin", "Auto Gremlin", "Auto Gremlin" });

        // I Don't Mean To Be CRUDE 6410
        Story.KillQuest(6410, "Greymoor", "Oil Elemental");

        // When I Say Jump... 6411
        Story.KillQuest(6411, "Greymoor", "Huge Auto Gremlin");

        // Energizing! 6412
        Story.KillQuest(6412, "Greymoor", "Energy Elemental");

        // Cabin in the Woods 6413
        Story.MapItemQuest(6413, "Greymoor", 5912, 5);

        // A Base-ic Solution 6414
        Story.KillQuest(6414, "Greymoor", "Spooky Treeant");

        // Surprise! 6415
        Story.MapItemQuest(6415, "Greymoor", new[] { 5913, 5914 });

        // First Aid Supplies 6416
        Story.KillQuest(6416, "Greymoor", "Spooky Treeant");

        // Wood for Trap? 6421
        Story.KillQuest(6421, "Greymoor", "Spooky Treeant");

        // Tool Time 6417
        Story.MapItemQuest(6417, "Greymoor", 5915);

        // Obstacle Course 6418
        Story.MapItemQuest(6418, "Greymoor", 5916, 5);

        // Barricades! 6419
        Story.MapItemQuest(6419, "Greymoor", 5917, 4);

        // Take Him Out! 6420
        Story.KillQuest(6420, "Greymoor", "Shrade");
    }

    public void Puzzlebox()
    {
        if (Core.isCompletedBefore(7399))
            return;

        if (!Core.IsMember && !CalculateFriday13())
        {
            Core.Logger("You must be Member or wait until Friday13th to complete Puzzlebox.");
            return;
        }

        Story.PreLoad(this);

        // Find the Souls 7394
        Story.KillQuest(7394, "PuzzleBox", "Bones of the Doomed");

        // The Blackened Heart 7395
        Core.Join("PuzzleBox", "r5", "right", true);
        Story.KillQuest(7395, "PuzzleBox", "Cursed Guardian");

        // The Ancient Athame 7396
        Core.Join("PuzzleBox", "r6", "right", true);
        Story.KillQuest(7396, "PuzzleBox", "Cursed Guardian");

        // The Withered Hand 7397
        Core.Join("PuzzleBox", "r7", "right", true);
        Story.KillQuest(7397, "PuzzleBox", "Cursed Guardian");

        // The Bones 7398
        Story.KillQuest(7398, "PuzzleBox", "Bones of the Doomed");

        // The Puzzle 7399
        Story.MapItemQuest(7399, "PuzzleBox", 7165);
    }

    public void Splatterwar()
    {
        Puzzlebox();

        if (Core.isCompletedBefore(7407))
            return;

        if (!Core.IsMember && !CalculateFriday13())
        {
            Core.Logger("You must be Member or wait until Friday13th to complete Splatter War.");
            return;
        }

        Story.PreLoad(this);

        // Slasher Medals 7400
        Story.KillQuest(7400, "SplatterWarDage", "Bladehands");

        // Mega Slasher Medals 7402
        Story.KillQuest(7402, "SplatterWarDage", "Bladehands");

        // Bladehands 7404
        Story.KillQuest(7404, "SplatterWarDage", "Bladehands");

        // Defeat Shrade 7406
        Story.KillQuest(7406, "SplatterWarDage", "Shrade");

        // Legion Medals 7401
        Story.KillQuest(7401, "SplatterWarShrade", "Legion Maw");

        // Mega Legion Medals 7403
        Story.KillQuest(7403, "SplatterWarShrade", "Legion Maw");

        // Jagged Canines 7405
        Story.KillQuest(7405, "SplatterWarShrade", "Legion Maw");

        // Defeat Dage 7407
        Story.KillQuest(7407, "SplatterWarShrade", "Dage The Evil");
    }

    public void Deadfly()
    {
        if (Core.isCompletedBefore(8232))
            return;

        if (!Core.IsMember && !CalculateFriday13())
        {
            Core.Logger("You must be Member or wait until Friday13th to complete Deadfly.");
            return;
        }

        Story.PreLoad(this);

        // The Flies 8217
        Story.KillQuest(8217, "Deadfly", "Grave Flies");

        // Dem Bones 8218
        Story.MapItemQuest(8218, "Deadfly", 8766, 8);

        // Gather the Reagents 8219
        Story.MapItemQuest(8219, "Deadfly", 8767, 7);
        Story.KillQuest(8219, "Deadfly", "Skeletal Minion");

        // Rip it Out 8220
        Story.KillQuest(8220, "Deadfly", "Gutripper");

        // Steal the Wands 8221
        Story.KillQuest(8221, "Deadfly", "Skeletal Mage");

        //Get to Emily 8222
        Story.MapItemQuest(8222, "Deadfly", 8768);

        // More of the Flies 8223
        Story.KillQuest(8223, "Deadfly", "Grave Flies");

        // Follow the Swarm 8224
        Story.MapItemQuest(8224, "Deadfly", new[] { 8769, 8770 });

        // Find Emily 8225
        Story.MapItemQuest(8225, "Deadfly", 8771);
        Story.KillQuest(8225, "Deadfly", "Skeletal Mage");

        // The Deadfly 8226
        Story.KillQuest(8226, "Deadfly", "Deadfly");

        // Gossip Time 8227
        Story.KillQuest(8227, "Deadfly", "Skeletal Minion");

        // Souls for Offering 8228
        if (!Story.QuestProgression(8228))
        {
            Core.EnsureAccept(8228);
            Core.HuntMonster("deadfly", "Skeletal Mage", "Fresh Soul", 8, log: false);
            Core.GetMapItem(8772, 1, "deadfly");
            Core.EnsureComplete(8228);
        }

        // Clue by 8 8229
        Story.KillQuest(8229, "RotFinger", "Lost Soul");

        // Cloth n' Coal 8230
        Story.MapItemQuest(8230, "RotFinger", 8773, 5);
        Story.KillQuest(8230, "RotFinger", "Blade Master");

        // Bait the Hook 8231
        Story.MapItemQuest(8231, "RotFinger", 8774, 10);

        // Rotfinger's Reckoning 8232
        Story.KillQuest(8232, "RotFinger", "Rotfinger");
    }

    public void Oddities()
    {
        if (Core.isCompletedBefore(8667))
            return;

        if (!Core.IsMember && !CalculateFriday13())
        {
            Core.Logger("You must be Member or wait until Friday13th to complete Oddities.");
            return;
        }

        Story.PreLoad(this);

        // Shop of Vandalized Horrors 8654
        if (!Story.QuestProgression(8654))
        {
            Core.EnsureAccept(8654);
            Core.KillMonster("Oddities", "Enter", "Spawn", "*", "Cursed Artifact Slain", 13);
            Core.EnsureComplete(8654);
        }

        // Browsing for a Way Out 8655
        Story.MapItemQuest(8655, "Oddities", 10149);

        // Key for Free 8656
        Story.MapItemQuest(8656, "Oddities", 10150, 4);
        Story.MapItemQuest(8656, "Oddities", 10151);
        Story.KillQuest(8656, "Oddities", "Cursed Doll-Head");

        // Rampaging Desk Jockey 8657
        Story.KillQuest(8657, "Oddities", "Writing Desk");

        // Steeped in Porcelain 8658
        Story.MapItemQuest(8658, "Oddities", 10152);
        Story.MapItemQuest(8658, "Oddities", 10153, 2);
        Story.MapItemQuest(8658, "Oddities", 10154);
        Story.KillQuest(8658, "Oddities", new[] { "Cursed Curio", "Gothic Chest", "Oddity Swarm" });

        // Stuffy Guests  8659
        Story.KillQuest(8659, "Oddities", "Creepy Baby|Dready Bear");

        // Snotty Crumbs 8660
        Story.KillQuest(8660, "Oddities", new[] { "Oddity Swarm", "Cursed Doll-Head" });

        // Wobbly Rune Writing 8661
        Story.MapItemQuest(8661, "Oddities", 10155, 6);

        // Broken Sights 8662
        Story.KillQuest(8662, "Oddities", "Opera Glasses");

        // Teddy Tailor 8663
        if (!Story.QuestProgression(8663))
        {
            Core.EnsureAccept(8663);
            Core.HuntMonster("Oddities", "Cursed Curio", "Bow Tie");
            Core.HuntMonster("Oddities", "Creepy Baby", "Button Eye", 2);
            Core.HuntMonster("Oddities", "Dready Bear", "Party Hat");
            Core.HuntMonster("Oddities", "Dready Bear", "Fur Scrap", 6);
            Core.EnsureComplete(8663);
        }
        // Immaterial Dowsing 8664
        Story.KillQuest(8664, "Oddities", "Dready Bear");

        // Be Our Bait Guest 8665
        Story.MapItemQuest(8665, "Oddities", 10156);

        // Pint-Sized Poltergeist 8666
        Story.KillQuest(8666, "Oddities", "Cursed Spirit");

        // Deep Cleanse 8667
        Story.KillQuest(8667, "Oddities", new[] { "Cursed Curio", "Creepy Baby", "Cursed Spirit" }, GetReward: false);
    }

    bool CalculateFriday13()
            => new DateTime(DateTime.Now.Year, DateTime.Now.Month, 13).DayOfWeek == DayOfWeek.Friday && DateTime.Now.Day >= 5;
}
