//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class CoreVoltaire
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

    public void DoAll()
    {
        if (!Core.IsMember)
        {
            Core.Logger("Voltaire Saga Is Member Only. Skipping this Script");
            return;
        }

        FiveSaloon();
        Train();
        Blackstone();
        FireTown();
        FireRiver();
        FireTunnel();
        Sleezter();
        Neverlore();
        Skullpunch();
        Vampirates();
        TreasureIsland();
        GustavduGrog();
        Wormhole();
        Crownsreach();
    }

    public void FiveSaloon()
    {
        Story.Preload(This);
        
        if (Core.isCompletedBefore(1049))
            return;

        //Unknown name 1042
        Story.ChainQuest(1042);

        //Exit, Stage Right 1043
        Story.MapItemQuest(1043, "fivesaloon", 406);

        //The Wedge 1044
        Story.KillQuest(1044, "fivesaloon", "Bulletless Bandit");

        //Freedom? 1045
        Story.MapItemQuest(1045, "fivesaloon", 407);

        //Other Ideas?! 1046
        Story.KillQuest(1046, "fivesaloon", "Storagebox");

        //Got a Light 1047
        Story.KillQuest(1047, "fivesaloon", "Bulletless Bandit");

        //BADABOOM! 1048
        Story.MapItemQuest(1048, "fivesaloon", 408);

        //Lack of Subtlety 1049
        Story.KillQuest(1049, "fivesaloon", "One-Armed Bandit");
    }

     public void Train()
    {
        FiveSaloon();
        
        Story.Preload(This);
        
        if (Core.isCompletedBefore(1053))
            return;

        //Look Around 1050
        Story.MapItemQuest(1050, "train", 409);

        //Caboose Key 1051
        Story.KillQuest(1051, "train", "Ghostly Conductor");
        
        //Slow your Role 1052
        Story.MapItemQuest(1052, "train", 410);
        
        //Slow the Coal 1053
        Story.KillQuest(1053, "train", "Coal Ghoul");
    }

     public void Blackstone()
    {
        Train();
        
        Story.Preload(This);
        
        if (Core.isCompletedBefore(1057))
            return;
        
        //Laying Traps 1054
        Story.MapItemQuest(1054, "blackstone", 411, 12);
        
        //Gathering Mats 1055
        Story.MapItemQuest(1055, "blackstone", 412, 12);

        //Pirate Hats 1056
        Story.KillQuest(1056, "blackstone", "Necrosian Soldier");

        //That's a Wrap 1057
        Story.KillQuest(1057, "blackstone", "Yahorneth");
    }

    public void FireTown()
    {
        Story.Preload(This);
        
        if (Core.isCompletedBefore(1554))
            return;
        
        //Gaining Trust 1552
        Story.KillQuest(1552, "firetown", new[] { "Fire Elemental", "Fire Elemental", "Fire Elemental" });

        //If You Can't Stand the Heat... 1553
        Story.KillQuest(1553, "firetown", "Fire Elemental");

        //Retrieving Recollections 1554
        Story.MapItemQuest(1554, "firetown", 790, 10);
    }

    public void FireRiver()
    {
        FireTown();
        
        Story.Preload(This);
        
        if (Core.isCompletedBefore(1557))
            return;

        //Under Orders 1555
        Story.KillQuest(1555, "fireriver", "Swamp Thing");

        //Locket Holds the Key 1556
        Story.KillQuest(1556, "fireriver", "Lava Bat|Lava Garou");

        //Plans Fit for a King 1557
        Story.MapItemQuest(1557, "fireriver", 792, 10);

    }

    public void FireTunnel()
    {
        FireRiver();
        
        Story.Preload(This);
        
        if (Core.isCompletedBefore(1564))
            return;

        //Bound by Fire 1558
        Story.KillQuest(1558, "firetunnel", "Volcanic Ash Imp");

        //Heart of Fire 1559
        Story.MapItemQuest(1559, "firetunnel", 791, 10);

        //Spirit of a Dragon 1560
        Story.KillQuest(1560, "firetunnel", "Elder Magma Wyrm");

        //Initiate Shutdown Sequence 1561
        Story.ChainQuest(1561);

        //Aura of Dragon's Flame 1562
        Story.KillQuest(1562, "firetunnel", "Elder Magma Wyrm");

        //Spirit of the Black Unicorn 1563
        Story.KillQuest(1563, "firetown", new[] { "Obsidian Golem", "Fire Spirit", "Loup-Garou" });

        //Tie a Black Ribbon 'Round an Old Burnt Tree 1564
        Story.KillQuest(1564, "firetown", "Burnt Tree");
    }

    public void Sleezter()
    {
        Story.Preload(This);
        
        if (Core.isCompletedBefore(1972))
            return;

        //Make Way 1961
        Story.KillQuest(1961, "sleezter", "BunnyMinion");

        //Black Pelt Test 1962
        Story.KillQuest(1962, "sleezter", "BunnyMinion");

        //Network Fiber Spool 1963
        Story.MapItemQuest(1963, "sleezter", 973, 6);

        //Guess What in a Guess Where 1964
        Story.MapItemQuest(1964, "sleezter", 974);
        
        //Splitting Hares 1965
        Story.KillQuest(1965, "sleezter", "BunnyMinion");
        
        //Sleezter Bunny's Quarters 1966
        Story.MapItemQuest(1966, "sleezter", 975);
        
        //The Sleezter Bunivinci Code 1967
        Story.KillQuest(1967, "sleezter", "BunnyMinion");
        
        //Take Sleezter Bunny Out! 1968
        Story.KillQuest(1968, "sleezter", "Sleezter Bunny");
        
        //Mantis Ray Repair 1969
        Story.MapItemQuest(1969, "sleezter", 976, 6);
        
        //A Map Wouldn't Hurt 1970
        Story.KillQuest(1970, "sleezter", "BunnyMinion");
        
        //Endin' Minions 1971
        Story.MapItemQuest(1971, "sleezter", 977);
        
        //Breaking Bad Eggs 1972
        Story.MapItemQuest(1972, "sleezter", 978);
    }

    public void Neverlore()
    {
        Story.Preload(This);
        
        if (Core.isCompletedBefore(2234))
            return;

        //Find the Shadow's Door 2222
        Story.MapItemQuest(2222, "neverlore", 1315);

        //One-way Trip to Neverworld 2223
        Story.KillQuest(2223, "neverlore", "Whablobble");

        //Slash the Shadows 2224
        Story.KillQuest(2224, "neverworld", "Spid-Squider");

        //Deprogramming for Dummies 2225
        Story.MapItemQuest(2225, "neverworld", 1316, 10);
        Story.KillQuest(2225, "neverworld", "Snackistopheles");

        //Guardian of the Lab-rary 2226
        Story.MapItemQuest(2226, "neverworld", 1317);
        Story.KillQuest(2226, "neverworld", new[] { "Snackistopheles", "Spid-Squider", "Snackistopheles", "Spid-Squider", "Fishizzle" });

        //Quick switch! 2227
        Story.MapItemQuest(2227, "neverworld", 1318, 5);
        Story.MapItemQuest(2227, "neverworld", 1326);

        //We're BOOM-ed! 2228
        Story.MapItemQuest(2228, "neverworld", 1321);

        //De-Generation Situation 2229
        Story.KillQuest(2229, "neverworld", "Generator");

        //Shielded Against the Shadows 2230
        Story.MapItemQuest(2230, "neverworld", 1319);
        Story.KillQuest(2230, "neverworld", "Fishizzle");

        //Making a Many-armed Army 2231
        Story.KillQuest(2231, "neverworld", "Kennel Door");

        //Kreature Kibble  Khaos 2232
        Story.MapItemQuest(2232, "neverworld", 1320, 10);

        //The Gang's Not All Here 2233
        Story.KillQuest(2233, "neverworld", "Fishizzle");

        //Beast Maker Beat-down 2234
        Story.ChainQuest(2234);
    }

    public void Skullpunch()
    {
        Story.Preload(This);
        
        if (Core.isCompletedBefore(3099))
            return;

        //Fishbones, Fishbones 3097
        Story.KillQuest(3097, "skullpunch", "Fishbones");

        //Outlook Good 3098
        Story.MapItemQuest(3098, "skullpunch", 1990);

        //Roly Poly Fishbones 3099
        Story.KillQuest(3099, "skullpunch", "Fishbones");        
    }

    public void Vampirates()
    {
        Story.Preload(This);
        
        if (Core.isCompletedBefore(3108))
            return;
        
        //We Can't Stop Here! 3100
        Story.KillQuest(3100, "vampirates", "Creepy Bat");
        
        //It's Bat Country! 3101
        Story.KillQuest(3101, "vampirates", "Creepy Bat");
        
        //Vampirates! 3102
        Story.KillQuest(3102, "vampirates", "Vampirate");

        //Oh Right, They're Vampires 3103
        Story.KillQuest(3103, "vampirates", new[] { "Creepy Bat", "Vampirate" });
        
        //The Vampirate Captain 3104
        Story.MapItemQuest(3104, "vampirates", 1959);
        
        //How Are We Breathing Under Here!? 3105
        Story.KillQuest(3105, "vampirates", "Stranglerfish");
        
        //Unsink This Ship! 3106
        Story.KillQuest(3106, "vampirates", new[] { "Murderaconian", "Vampirate", "Vampirate" });
        
        //Release the Kraken! Wait, no - Don't. 3107
        Story.KillQuest(3107, "vampirates", "Bracken Kraken");
        
        //I Didn't Know They Could Swarm! 3108
        Story.KillQuest(3108, "vampirates", "Vampirate");
    }

    public void TreasureIsland()
    {
        Story.Preload(This);
        
        if (Core.isCompletedBefore(3111))
            return;

        //Now Where Are Those Landmarks? 3109
        Story.MapItemQuest(3109, "treasureisland", new[] { 1986, 1987, 1988, 1989 });
        
        //X Marks The Spot 3110
        Story.MapItemQuest(3110, "treasureisland", 1958);
        
        //Captain Von Poach 3111
        Story.KillQuest(3111, "treasureisland", "Captain Von Poach");
    }

    public void GustavduGrog()
    {
        Story.Preload(This);
        
        if (Core.isCompletedBefore(3119))
            return;

        //Me Knickers Got a Big Hole Across th' Bum 3114
        Story.KillQuest(3114, "skullpunch", new[] { "Vampirate", "Fishbones" });

        //Where'd Me Ship's Cargo? 3115
        Story.KillQuest(3115, "skullpunch", new[] { "Fishbones", "Fishbones", "Fishbones", "Fishbones" });

        //A Most Important Package 3116
        Story.KillQuest(3116, "skullpunch", "Fishbones");

        //Comfort Food 3117
        Story.KillQuest(3117, "skullpunch", new[] { "Shelleton", "Fishwing" });

        //Full o' Holes 3118
        Story.KillQuest(3118, "skullpunch", "Shelleton");

        //Keelhaulin' a Kraken! 3119
        Story.KillQuest(3119, "chaoskraken", "Chaos Kraken");
    }

    public void Wormhole()
    {
        Story.Preload(This);
        
        //Something In The Air 5051
        Story.KillQuest(5051, "wormhole", new[] { "Goth Girl", "Vamp Boy" });
        
        //They're Not Red Shirts - They're Deadshirts! 5052
        Story.MapItemQuest(5052, "wormhole", new[] { 4420, 4421, 4422 });
        Story.KillQuest(5052, "wormhole", "Deadshirt");

        //Curses! 5053
        Story.KillQuest(5053, "wormhole", "Cursed Alien");

        //Computers Don't Lie 5054
        Story.MapItemQuest(5054, "wormhole", new[] { 4423, 4424, 4425, 4426 });

        //Beam Me Up! 5055
        Story.KillQuest(5055, "wormhole", new[] { "Undead Astronaut", "Undead Astronaut" });

        //Exotic Pets 5056
        Story.KillQuest(5056, "wormhole", "Xenodog");

        //Not Trobbles 5057
        Story.KillQuest(5057, "wormhole", "Green Trobbolier|Purple Trobbolier"); // Be patient

        //System Failure! 5058
        Story.MapItemQuest(5058, "wormhole", 4428);

        //I Think I Saw This Movie Once 5059
        Story.KillQuest(5059, "wormhole", "Space Horror");

        //I Have A Short Fuse 5060
        Story.MapItemQuest(5060, "wormhole", 4427, 6);
        Story.KillQuest(5060, "wormhole", "Volatile Current");

        //More Trobboliers 5061
        Story.KillQuest(5061, "wormhole", "Green Trobbolier|Purple Trobbolier"); // Be patient

        //Maybe These Guys Know Something 5062
        Story.KillQuest(5062, "wormhole", new[] { "Stormslasher", "Undead Space Marine" });

        //Look At That Awesome Armor! 5063
        Story.KillQuest(5063, "wormhole", "Stormslasher");

        //Guardians of the Warehouse 5064
        Story.KillQuest(5064, "wormhole", "Guardian");

        //Undead Space 5065
        Story.KillQuest(5065, "wormhole", new[] { "Space Ghost", "Singularity" });

        //Trobbolegion! 5066
        Story.KillQuest(5066, "wormhole", "Trobbolegion");
    }

    public void Crownsreach()
    {
        Story.Preload(This);
        
        //Those Bloody Maggots 5637
        Story.MapItemQuest(5637, "crownsreachfxiii", 5115, 8);
        Story.KillQuest(5637, "crownsreachfxiii", "Blood Maggot");

        //Purple is the Best Color 5639
        Story.MapItemQuest(5639, "safiria", 5114);
        Story.KillQuest(5639, "safiria", "Albino Bat");

        //Get Some Candles 5640
        Story.KillQuest(5640, "battleundera", "Skeletal Fire Mage");

        //Time For a Break 5641
        Story.MapItemQuest(5641, "crownsreachfxiii", 5116);
        Story.KillQuest(5641, "crownsreachfxiii", "Vampire Bat");

        //Call the Exterminator 5642
        Story.KillQuest(5642, "crownsreachfxiii", "Eldritch Parasite");

        //Calm the Villagers 5643
        Story.KillQuest(5643, "crownsreachfxiii", "Panicking Villager");

        //Clean up the Ooze 5644
        Story.KillQuest(5644, "crownsreachfxiii", "Crawling Ooze");

        //They Stole My Stuff! 5645
        Story.KillQuest(5645, "crownsreachfxiii", "Crawling Ooze");

        //Ia! Ia! Shub'hathrys Fhtagn! 5646
        Story.KillQuest(5646, "crownsreachfxiii", "Shub-Hathrys");
    }
}
