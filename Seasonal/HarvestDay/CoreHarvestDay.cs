using System.Reflection.PortableExecutable;
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class CoreHarvestDay
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DoAll();

        Core.SetOptions(false);
    }

    public void DoAll()
    {
        Harvest();
        Turdraken();
        Float();
        Banquet();
        Grams();
        ArtixHome();
        FoulFarm();
        KillerKitchen();
        FurbleFeast();
        FurborgShip();
        MeatLab();
        GothicDream();
        MemetNightmare();
        NightmareWar();
        EpilTakeOver();
    }

    public void Harvest()
    {
        if (!Core.isSeasonalMapActive("harvest"))
            return;
        if (Core.isCompletedBefore(136))
            return;

        Story.PreLoad(this);

        //Scout the Cornycopia (130)
        Story.MapItemQuest(130, "harvest", 36);

        //Unboiling Water (131)
        if (!Story.QuestProgression(131))
        {
            Core.EnsureAccept(131, 420);
            Core.GetMapItem(31, 1, "harvest");
            Core.EnsureComplete(420);
            Core.EnsureComplete(131);
        }

        //The Corn has Ears (132)
        if (!Story.QuestProgression(132))
        {
            Core.EnsureAccept(132, 421);
            Core.HuntMonster("harvest", "Corn Stalker", "Corn Stalker Ears", 8);
            Core.EnsureComplete(421);
            Core.EnsureComplete(132);
        }

        //An Apple a Day (133)
        if (!Story.QuestProgression(133))
        {
            Core.EnsureAccept(133, 422);
            Core.HuntMonster("harvest", "Bad Apple", "Worm", 5);
            Core.EnsureComplete(422);
            Core.EnsureComplete(133);
        }

        //Whine n' Cheese (134)
        if (!Story.QuestProgression(134))
        {
            Core.EnsureAccept(134, 423);
            Core.HuntMonster("harvest", "Grapes of Wrath", "Whine", 8);
            Core.EnsureComplete(423);
            Core.EnsureComplete(134);
        }

        //Fruit of the Loot (135)
        Story.KillQuest(135, "harvest", "*");
        Story.MapItemQuest(135, "harvest", 37);

        //The Turdraken (136)
        Story.KillQuest(136, "harvest", "Turdraken");
    }

    public void Turdraken()
    {
        Harvest();
        if (!Core.isSeasonalMapActive("turdraken"))
            return;
        if (Core.isCompletedBefore(430) && Core.IsMember)
            return;

        Story.PreLoad(this);

        //Pain-Apple! 139
        Story.KillQuest(139, "turdraken", "Bad Apple");

        //Squishy Squash! 140
        Story.KillQuest(140, "turdraken", "Gourd Monster");

        //Ice Cream! 141
        Story.KillQuest(141, "turdraken", "Mad Strawberry");

        //Ring of Pain! 142
        Story.KillQuest(142, "turdraken", "Pineapple Beast");

        //Cholesterious 143
        Story.KillQuest(143, "turdraken", "Cholesterious");

        //Turkey go Boom! 429
        Story.ChainQuest(429);

        //Cater To His Every Dish 430
        if (!Story.QuestProgression(430))
        {
            Core.EnsureAccept(430);
            Core.HuntMonster("orctown", "Horc Warrior", "Pulled Horc Sandwich");
            Core.HuntMonster("farm", "Mosquito", "French Flies");
            Core.HuntMonster("GreenguardEast", "Spider", "Corn Cob Web");
            Core.HuntMonster("sewer", "GreenRat", "Ratatouille");
            Core.HuntMonster("uppercity", "Rhino Beetle", "Chocolate Covered Beetle");
            Core.EnsureComplete(430);
        }

    }

    public void Float()
    {
        Turdraken();
        if (!Core.isSeasonalMapActive("float"))
            return;
        if (Core.isCompletedBefore(897) && Core.IsMember)
            return;

        Story.PreLoad(this);

        //Turtle Shells Pop Balloons 893
        Story.KillQuest(893, "pines", "Red Shell Turtle");

        //Make things Pop with a Sentry Bot 894
        Story.KillQuest(894, "crashsite", "Sentry Bot");

        //Web Browsing for Balloons 895
        Story.KillQuest(895, "twilight", "Gressil");

        //Complete Airheads 896
        Story.KillQuest(896, "float", new[] { "Cysero Balloon", "Beleen Balloon" });

        //The Final Pilgrimage 897
        Story.KillQuest(897, "float", "Twilly Balloon");

    }

    public void Banquet()
    {
        Turdraken();
        if (!Core.isSeasonalMapActive("banquet"))
            return;
        if (Core.isCompletedBefore(1436))
            return;

        Story.PreLoad(this);

        //Subdue the Squadrons 1433
        Story.KillQuest(1433, "banquet", "Hungry Knight");

        //Pactagonal Aid Required 1434
        Story.MapItemQuest(1434, "banquet", 712);

        //Contain the Chaos 1435
        Story.MapItemQuest(1435, "banquet", 713, 7);

        //Chaorrupted Captain Encounter 1436
        Story.KillQuest(1436, "banquet", "Hungry Knight Captain");

    }

    public void Grams()
    {
        Banquet();
        if (!Core.isSeasonalMapActive("grams"))
            return;
        if (Core.isCompletedBefore(1444))
            return;

        Story.PreLoad(this);
        //Bandits are Bad News 1441
        Story.MapItemQuest(1441, "grams", 715);

        //Creatures Serving Chaos 1442
        Story.KillQuest(1442, "grams", new[] { "Wolf", "Wereboar", "Spider" });

        //Bandit Battles 1443
        Story.KillQuest(1443, "grams", "Bandit");

        //Can You Regain Grams? 1444
        Story.KillQuest(1444, "grams", "Grumpy Granny");
    }

    public void ArtixHome()
    {
        Grams();
        if (!Core.isSeasonalMapActive("artixhome"))
            return;
        if (Core.isCompletedBefore(1440) && Core.IsMember)
            return;

        Story.PreLoad(this);

        //Chaos Critters in the Crops 1437
        Story.KillQuest(1437, "artixhome", new[] { "Mosquito", "Chaorrupted Poultrygeist" });

        //Zombehs Aren't So Smart 1438
        Story.MapItemQuest(1438, "artixhome", 714, 10);
        Story.KillQuest(1438, "artixhome", "Zombeh");

        //It's the Anti-Mall 1439
        Story.MapItemQuest(1439, "artixhome", 716, 5);
        Story.KillQuest(1439, "artixhome", "Treeant");

        //28 Battles Later... 1440
        Story.KillQuest(1440, "artixhome", "Zombeh");

    }

    public void FoulFarm()
    {
        if (!Core.isSeasonalMapActive("foulfarm"))
            return;
        if (Core.isCompletedBefore(6090))
            return;

        Story.PreLoad(this);

        //Give Me The Gold! 6086
        Story.KillQuest(6086, "harvestzombie", "Golden Warrior");

        //Cyser-Os! 6088
        while (!Bot.ShouldExit && !Core.CheckInventory("Soulflare"))
        {
            //Cyser-Os! 6088
            Core.AddDrop("Soulflare");
            Core.EnsureAccept(6088);
            Core.HuntMonster("battlefowl", "ChickenCow", "Box of Cyser-Os", 3);
            Bot.Wait.ForDrop("Soulflare");
            Core.EnsureComplete(6088);
        }

        //A Golden Blade 6087
        if (!Story.QuestProgression(6087))
            Core.ChainComplete(6087);

        // Cover the Shine 6089
        if (!Story.QuestProgression(6089) || !Core.CheckInventory("Muddy Soulflare"))
        {
            Core.EnsureAccept(6089);
            Core.HuntMonster("brightoak", "Tainted Earth", "Sticky Mud", 8);
            Bot.Wait.ForDrop("Muddy Soulflare");
            Core.EnsureComplete(6089);
        }

        //Face the Rider 6090
        Story.KillQuest(6090, "Dullahan", "Wretched Rider");

    }

    public void KillerKitchen()
    {
        if (!Core.isSeasonalMapActive("killerkitchen"))
            return;
        if (Core.isCompletedBefore(3214))
            return;

        Story.PreLoad(this);

        //Oishii's Special Blend 3209
        Story.MapItemQuest(3209, "killerkitchen", 2231);

        //Cornstalkerbread Stuffing 3210
        Story.KillQuest(3210, "harvest", new[] { "Corn Stalker", "Corn Stalker" });

        //Super Special Chutney 3211 
        Story.KillQuest(3211, "harvest", new[] { "Bad Apple", "Grapes of Wrath" });

        //Oishii's Secret Ingredient 3212
        Story.KillQuest(3212, "killerkitchen", "Harvest Sneevil");

        //Taking Out the Turdrakolich 3213
        Story.KillQuest(3213, "killerkitchen", "Turdrakolich");

        //Defeat Ultra Turdrakolich 3214
        Story.KillQuest(3214, "killerkitchen", "Ultra Turdrakolich");

    }

    public void FurbleFeast()
    {
        if (!Core.isSeasonalMapActive("furblefeast"))
            return;
        if (Core.isCompletedBefore(7224))
            return;

        Story.PreLoad(this);

        // Scavengers 7214
        Story.KillQuest(7214, "furblefeast", "Harvest Sneevil");

        // Wired 7215
        Story.KillQuest(7215, "furblefeast", "Turkey");

        // Turdraken Tremor 7216
        Story.KillQuest(7216, "furblefeast", "Mini Turdraken");

        // Meat and Eggs 7217
        Story.KillQuest(7217, "furblefeast", new[] { "Harvest Sneevil", "Turkey" });

        // Zombies! 7218
        Story.KillQuest(7218, "furblefeast", "Meat Zombie");

        // Fight the Furbles 7219
        Story.KillQuest(7219, "furblefeast", "Angry Furble");

        // Pollinate 7220
        Story.MapItemQuest(7220, "furblefeast", 6843, 10);
        Story.KillQuest(7220, "poisonforest", "Treeant");

        // Mecha-Furbles 7221
        Story.KillQuest(7221, "furblefeast", "Mecha Furble");

        // Sabotage 7222
        Story.KillQuest(7222, "crashsite", new[] { "Dwakel Blaster", "Dwakel Warrior" });

        // SMASH 7223
        Story.MapItemQuest(7223, "furblefeast", 6844, 4);

        // Take Him Out 7224
        Story.KillQuest(7224, "furblefeast", "Furborg");

    }

    public void FurborgShip()
    {
        FurbleFeast();
        if (!Core.isSeasonalMapActive("furborgship"))
            return;
        if (Core.isCompletedBefore(7231))
            return;

        Story.PreLoad(this);

        // Get Outta Here 7225
        Story.MapItemQuest(7225, "furborgship", 6855);

        // Level Up! 7226
        Story.MapItemQuest(7226, "furborgship", 6856);

        // Keep Climbing 7227
        Story.MapItemQuest(7227, "furborgship", 6857);

        // Up, Up, Up! 7228
        Story.MapItemQuest(7228, "furborgship", 6858);

        // Get to the Queen 7229
        Story.MapItemQuest(7229, "furborgship", 6859);

        // Kill the Queen 7230
        Story.KillQuest(7230, "furborgship", "Feegrix Queen");

        // Furbapon 7231
        Story.KillQuest(7231, "furborgship", "Furborg Guard");

    }

    public void MeatLab()
    {
        if (!Core.isSeasonalMapActive("meatlab"))
            return;
        if (Core.isCompletedBefore(7213))
            return;

        Story.PreLoad(this);

        // Rhison Samples 7200
        Story.KillQuest(7200, "bloodtusk", "Rhison");

        // Test Sample #1 7201
        Story.MapItemQuest(7201, "meatlab", new[] { 6834, 6835 });

        // Spice it Up 7202
        Story.KillQuest(7202, "firestorm", "Firestorm Hatchling");

        // Test Sample #2 7203
        Story.MapItemQuest(7203, "meatlab", new[] { 6836, 6837 });

        // Tenderize 7204
        Story.KillQuest(7204, "pirates", "Fishwing");

        // Test Sample #3 7205
        Story.MapItemQuest(7205, "meatlab", new[] { 6838, 6839 });

        // Meat Multiplication 7206
        Story.KillQuest(7206, "guru", "Trobble");

        // Test Sample #4 7207
        Story.MapItemQuest(7207, "meatlab", 6840);

        // Time to Clean 7208
        Story.KillQuest(7208, "meatlab", "Meat Nubbin");

        // They're Eating the Wires 7209
        Story.KillQuest(7209, "meatlab", "Electrical Fire");

        // Turn 'em off and on Again 7210
        Story.KillQuest(7210, "meatlab", "Repair Drone");

        // So... Greasy 7211
        Story.MapItemQuest(7211, "meatlab", 6841);
        Story.KillQuest(7211, "meatlab", "Meat Nubbin");

        // Shut it Down  7212
        Story.MapItemQuest(7211, "meatlab", 6842);

        // Kill it with Fire 7213
        Story.KillQuest(7213, "meatlab", "Meatmongous");

    }

    public void GothicDream()
    {
        if (!Core.isSeasonalMapActive("gothicdream"))
            return;
        if (Core.isCompletedBefore(7795))
            return;

        Story.PreLoad(this);

        // That Cake is a Lie 7784
        Story.KillQuest(7784, "gothicdream", "Monster Cake");

        // Get the Cupcakes 7785
        Story.KillQuest(7785, "gothicdream", "Killer Cupcake");

        // Wash it Down 7786
        Story.KillQuest(7786, "gothicdream", "Angry Punch");

        // Freezer Key 7787
        Story.KillQuest(7787, "gothicdream", "Angry Punch");

        // Ice it Up 7788
        Story.KillQuest(7788, "gothicdream", "Ice Elemental");

        // I Scream, You Scream! 7789
        Story.KillQuest(7789, "gothicdream", "Ice Scream");

        // Clear the Hallway 7790
        Story.KillQuest(7790, "gothicdream", new[] { "Spooky Lantern", "Bat Garland" });

        // Sleepytime Gear 7791
        Story.MapItemQuest(7791, "gothicdream", new[] { 7820, 7821 });
        Story.KillQuest(7791, "gothicdream", new[] { "Spooky Lantern", "Bat Garland" });

        // Too Bright 7792
        Story.KillQuest(7792, "gothicdream", "Spooky Lantern");

        // What's That Under my Bed? 7793
        Story.KillQuest(7793, "gothicdream", "Bed Monster");

        // Why Can't I Sleep? 7794
        Story.KillQuest(7794, "gothicdream", "Sleep Paralysis");

        // Clean Up Clean Up Everybody Do Your Scare 7795
        Story.KillQuest(7795, "gothicdream", new[] { "Spooky Lantern", "Bat Garland", "Ice Scream" });

    }

    public void MemetNightmare()
    {
        GothicDream();
        if (!Core.isSeasonalMapActive("memetnightmare"))
            return;
        if (Core.isCompletedBefore(7808))
            return;

        Story.PreLoad(this);

        // Talk to Memet 7796
        Story.MapItemQuest(7796, "memetnightmare", 7836);

        // Try Coffee 7797
        Story.KillQuest(7797, "memetnightmare", "Eye Wyrm");

        // Put Out the Embers 7798
        Story.KillQuest(7798, "memetnightmare", "Burning Ember");

        // Kibble for Nibbles 7799
        Story.KillQuest(7799, "memetnightmare", "Nightmare Maw");

        // Board up the Windows 7800
        Story.KillQuest(7800, "memetnightmare", new[] { "Fire Cyclone", "Fire Cyclone" });

        // Energy Needed 7801
        Story.KillQuest(7801, "memetnightmare", "Eye Wyrm");

        // Audition the Wyrms 7802
        Story.KillQuest(7802, "memetnightmare", "Eye Wyrm");

        // Killer Beats 7803
        Story.KillQuest(7803, "memetnightmare", "Nightmare Maw");

        // Stop the Storm 7804
        Story.KillQuest(7804, "memetnightmare", new[] { "Fire Cyclone", "Burning Ember" });

        // Cannibal Mermaids? 7805
        Story.KillQuest(7805, "memetnightmare", "Scuttlefish");

        // Infiltrate the Mermaids 7806
        Story.KillQuest(7806, "memetnightmare", "Cannibal Mermaid");

        // Lighten the Despair 7807
        Story.KillQuest(7807, "memetnightmare", "Dark Pit of Despair");

        // Quiet Down, Will Ya? 7808
        Story.KillQuest(7808, "memetnightmare", new[] { "Fire Cyclone", "Burning Ember", "Cannibal Mermaid" });


    }

    public void NightmareWar()
    {
        MemetNightmare();
        if (!Core.isSeasonalMapActive("nightmarewar"))
            return;
        if (Core.isCompletedBefore(7813))
            return;
        Story.PreLoad(this);

        // Murderbug Medal 7809
        Story.KillQuest(7809, "nightmarewar", "Zombie Cicada");

        // Mega Murderbug Medal 7810
        Story.KillQuest(7810, "nightmarewar", "Zombie Cicada");

        // Elemental Medal 7811
        Story.KillQuest(7811, "nightmarewar", "Storm Core");

        // Mega Elemental Medal 7812
        Story.KillQuest(7812, "nightmarewar", "Storm Core");

        // Fire Tornado 7813
        Story.KillQuest(7813, "nightmarewar", "THIS YEAR");

    }

    public void EpilTakeOver()
    {
        if (Core.isCompletedBefore(8953) && !Core.isSeasonalMapActive("EbilTakeOver"))
            return;

        Story.PreLoad(this);

        // Turning Traitor 8937
        Story.KillQuest(8937, "ebiltakeover", "Traitor Goon");

        // Bait and Switch 8938
        Story.MapItemQuest(8938, "ebiltakeover", 10859, 4);

        // Fishman Flop 8939
        Story.KillQuest(8939, "ebiltakeover", "Ebil Fishman");

        // Chief Fish Marketing Officer 8940
        Story.KillQuest(8940, "ebiltakeover", "Ebil Kuro");

        //Slime Time 8941
        Story.KillQuest(8941, "ebiltakeover", "EbilCorp Slime");

        //Keycard Access 8942
        Story.KillQuest(8942, "ebiltakeover", "Traitor Goon");

        //Lay Offs 8943
        Story.KillQuest(8943, "ebiltakeover", "EbilCorp Slime");

        // Human Remains Officer 8945
        Story.KillQuest(8945, "ebiltakeover", "Ebil Jack Sprat");

        //Traitor Traps 8946
        Story.MapItemQuest(8946, "ebiltakeover", 10860, 5);

        //Drones and Drones 8947
        Story.KillQuest(8947, "ebiltakeover", new[] { "Traitor Goon", "Ebil Battle Drone" });

        //Tech Collection 8948
        Story.KillQuest(8948, "ebiltakeover", "Ebil Battle Drone");

        // General General 8949
        Story.KillQuest(8949, "ebiltakeover", "Ebil General Porkon");

        //Variety Is The Spice Of Life 8950
        Story.KillQuest(8950, "ebiltakeover", new[] { "Ebil Fishman", "EbilCorp Slime", "Ebil Battle Drone" });

        //Draconian Destruction 8951
        Story.KillQuest(8951, "ebiltakeover", "Ebil Draconian");

        //Final Notice 8952
        Story.KillQuest(8952, "ebiltakeover", "Traitor Goon");

        // Chief Immolation Officer 8953
        Story.KillQuest(8953, "ebiltakeover", "Ebil Red Dragon");
    }
}