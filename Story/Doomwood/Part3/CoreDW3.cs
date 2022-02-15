//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/CoreDailys.cs

using RBot;

public class CoreDW3
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreDailys Dailys = new CoreDailys();
    public CoreBLOD BLoD = new CoreBLOD();

    public string[] StoryLineDrops =
    {
    "Deadtech War Medal",
    "Zealous Badge",
    "Salvaged Deadtech Node",
    "Kyger"
    };

    public string[] PinkSwordItems =
    {
    "Obsidian Light of Destiny",
    "Purified Undead Dragon Essence",
    "Shard of An Orb",
    "Rainbow Paladin",
    "Rainbow Paladin Cape",
    "Rainbow Paladin Helm + Locks",
    "Rainbow Paladin Helm",
    "Pink Blade of Destruction",
    "Rainbow Cat Ears + Locks",
    "Rainbow Cat Ears + Hair",
    "Fuchsia Dye",
    "Spirit Orb",
    "Zealous Badge",
    "Unicorn Essence",
    "Gem Power"
    };

    public void ScriptMain(ScriptInterface bot) //DO NOT RENAME THIS
    {
        Core.SetOptions();

        StoryLine();
        QuestsForUnicornEssence();
        PinkBlade();

        Core.SetOptions(false);
    }

    public void PinkBlade()
    {
        Core.AddDrop(PinkSwordItems);
        // Forging a Friendship - 7650
        Core.EnsureAccept(7650);
        //reqs:
        // Fuchsia Dye x50
        // Spirit Orb (Misc) x500
        // Zealous Badge x5
        // Unicorn Essence x5
        // Gem Power x5

        while (!Core.CheckInventory("Fuchsia Dye", 50))
        {
            Core.EnsureAccept(1487);
            Core.HuntMonster("natatorium", "Anglerfish", "Pink Coral", 3);
            Core.HuntMonster("bloodtuskwar", "Chaotic Vulture", "Amaranth Flower", 5);
            Core.EnsureComplete(1487);
        }

        while (!Core.CheckInventory("Spirit Orb", 500))
        {
            BLoD.UnlockMineCrafting();
            Farm.BattleUnderB("Bone Dust", 40);
            Farm.BattleUnderB("Undead Essence", 25);
            Core.ChainComplete(2082);
            Core.ChainComplete(2083);
        }

        while (!Core.CheckInventory("Zealous Badge", 5))
        {
            Core.EnsureAccept(7616);
            Core.HuntMonster("techdungeon", "Kalron the Cryptborg", "Immutable Dedication", 7);
            Core.HuntMonster("techdungeon", "DoomBorg Guard", "Paladin Armor Scraps", 30);
            Core.EnsureComplete(7616);
        }


        while (!Core.CheckInventory("Unicorn Essence", 5))
        {
            Core.EnsureAccept(3157);
            Core.HuntMonster("undergroundlabb", "Ultra Brutalcorn", "Ultra BrutalCorn");
            Core.EnsureComplete(3157);
        }

        Core.HuntMonster("undergroundlabb", "Ultra Battle Gem", "Gem Power", 5);

        Core.EnsureComplete(7650, 55884);
    }


    public void StoryLine()
    {
        if (Core.isCompletedBefore(7653))
            return;
        ///thorngarde > /stonewood > /techdungeon > /stonewooddeep > /techfortress

        //Part1
        //Map: Thorngarde
        // Bullies
        Core.KillQuest(7589, "Thorngarde", new[] { "CryptHacker", "NecroMech", "NecroDrone" });
        // Free the People
        Core.KillQuest(7590, "Thorngarde", "NecroMech");
        Core.MapItemQuest(7590, "Thorngarde", 7485, 5);
        // Sample It
        Core.KillQuest(7591, "Thorngarde", "NecroMech");
        // Swat the Flies
        Core.KillQuest(7592, "Thorngarde", "CryptHacker");
        // Reclaim the Supplies
        Core.KillQuest(7593, "Thorngarde", new[] { "NecroDrone", "NecroMech", "CryptHacker" });

        //Part1.Kyger - Requirements: Must have completed the 'Reclaim the Supplies' quest.
        // http://aqwwiki.wikidot.com/kyger-s-quests#Thorn
        // Who's That?
        Core.MapItemQuest(7594, "Thorngarde", 7486);
        // Find my Stuff
        Core.KillQuest(7595, "Thorngarde", "NecroDrone");
        Core.MapItemQuest(7595, "Thorngarde", 7487);
        Core.MapItemQuest(7595, "Thorngarde", 7488);
        // Save my Friends
        Core.KillQuest(7596, "Thorngarde", "NecroMech");
        Core.MapItemQuest(7596, "Thorngarde", 7489);
        Core.MapItemQuest(7596, "Thorngarde", 7490);
        Core.MapItemQuest(7596, "Thorngarde", 7491);
        // Pull the Wires
        Core.KillQuest(7597, "Thorngarde", "CryptHacker");
        // Dress-up Time!
        Core.KillQuest(7598, "Thorngarde", new[] { "CryptHacker", "NecroDrone" });
        // I Need a Distraction
        Core.KillQuest(7599, "Thorngarde", new[] { "NecroDrone", "NecroMech", "CryptHacker" }, AutoCompleteQuest: false);


        //Part1.1 - Requirements: Must have completed the 'Zyrus is Lost' quest.
        // Zyrus is Lost
        Core.KillQuest(7600, "Thorngarde", "Zyrus the BioKnight");
        // Investigate the Tech
        Core.KillQuest(7601, "Thorngarde", new[] { "NecroDrone", "NecroMech", "CryptHacker", "Zyrus the BioKnight" });

        //Part1.2 - Requirements: Must have completed the 'Zyrus is Lost' quest.
        //Map: Stonewood
        // Defeat the Deadtech
        Core.KillQuest(7603, "Stonewood", "NecroDrone");
        // Gather Spell Components
        Core.KillQuest(7604, "Stonewood", "Doomwood Treeant");
        Core.MapItemQuest(7604, "Stonewood", 7510, 6);
        // A Few Things More
        Core.KillQuest(7605, "Stonewood", new[] { "NecroDrone", "NecroMech" });
        // Search for the Axe
        Core.MapItemQuest(7606, "Stonewood", 7511, 4);
        Core.MapItemQuest(7606, "Stonewood", 7512);

        //Part1.2.1 - http://aqwwiki.wikidot.com/stonewood-s-quests
        // Defeat the BioKnight
        Core.KillQuest(7607, "Stonewood", "BioKnight");
        // Take the Axe!
        Core.MapItemQuest(7608, "Stonewood", 7513);


        //Part1.3 - Requirements: Must have completed the 'Take the Axe!' quest.
        //Map: Techdungeon
        // Find the Keys
        Core.KillQuest(7609, "Techdungeon", "NecroMech");
        // Ew, Rats
        Core.KillQuest(7610, "Techdungeon", "Rotting Rat");
        Core.MapItemQuest(7610, "Techdungeon", 7515, 6);
        // Grab Their Gear
        if (!Core.QuestProgression(7611))
        {
            Core.EnsureAccept(7611);
            Core.KillMonster("Techdungeon", "r4", "left", "DoomBorg Guard", "Guard Armor Piece", 5);
            Core.KillMonster("Techdungeon", "r4", "left", "DoomBorg Guard", "Guard Helm");
            Core.KillMonster("Techdungeon", "r4", "left", "DoomBorg Guard", "Deadtech Polearm");
            Core.EnsureComplete(7611);
        }
        // Talk to the Guard
        Core.MapItemQuest(7612, "Techdungeon", 7514);
        // Let Them Out!
        Core.MapItemQuest(7613, "Techdungeon", 7516);
        // Head on Out
        Core.KillQuest(7614, "Techdungeon", "DoomBorg Guard");
        Core.MapItemQuest(7614, "Techdungeon", 7517);
        // Take Him Down
        Core.KillQuest(7615, "Techdungeon", "Kalron the Cryptborg");

        //Part1.4 - Requirements: Must have completed the 'Take Him Down' quest.
        //Map: Techdungeon
        // A Solemn Favor
        Core.KillQuest(7616, "Techdungeon", new[] { "Kalron the Cryptborg", "DoomBorg Guard" });
        //http://aqwwiki.wikidot.com/cat-ears-quest :
        // Cat Ears?
        //Map: stonewooddeep
        Core.MapItemQuest(7623, "stonewooddeep", 7528, 1, AutoCompleteQuest: false);
        //http://aqwwiki.wikidot.com/stonewood-forest-s-quests :
        // EN GARDE!
        //Map: stonewooddeep
        Core.KillQuest(7624, "stonewooddeep", "Asherion");

        //Part1.4.1 - Requirements: Must have completed the 'EN GARDE!' quest.
        // http://aqwwiki.wikidot.com/kyger-s-quests#Forest
        // The Light Of Destiny
        Core.MapItemQuest(7625, "stonewooddeep", 7529);

        //Part1.4.2 - http://aqwwiki.wikidot.com/blinding-light-of-destiny-s-quests
        //Requirements: Must have completed the 'The Light Of Destiny' quest.
        //Map: stonewooddeep
        // Stuff for Dummies
        Core.KillQuest(7626, "stonewooddeep", "Doomwood Treeant");
        Core.MapItemQuest(7626, "stonewooddeep", 7530, 8);
        // Build the Dummies
        Core.KillQuest(7627, "stonewooddeep", "Doomwood Slime");
        Core.MapItemQuest(7627, "stonewooddeep", 7531, 6);
        // Battle the Dummies
        Core.KillQuest(7628, "stonewooddeep", "Target Dummy");
        // Lesson 1: Bravery
        Core.KillQuest(7629, "stonewooddeep", "Doomwood Slime");
        // Lesson 2: Armor
        Core.KillQuest(7630, "stonewooddeep", "Doomwood Slime");
        // Lesson 3: Protection
        if (!Core.QuestProgression(7631))
        {
            Core.EnsureAccept(7631);
            Core.HuntMonster("stonewooddeep", "CryptHacker", "Crypthacker Slain", 10);
            Core.HuntMonster("stonewooddeep", "CryptHacker", "Unidentified Clue");
            Core.EnsureComplete(7631);
        }
        // Lesson 4: Gather Intelligence
        Core.KillQuest(7632, "stonewooddeep", "CryptHacker");
        // Lesson 5: Demolition
        Core.KillQuest(7633, "stonewooddeep", new[] { "Doomwood Slime", "NecroDrone" });
        // Lesson 6: Searching
        Core.MapItemQuest(7634, "stonewooddeep", 7532);
        // Get the Axe
        Core.MapItemQuest(7635, "stonewooddeep", 7533);
        // Never Give Up   
        Core.KillQuest(7636, "stonewooddeep", "Sir Kut");

        //Part1.5 - Requirements: Must have completed the 'Never Give Up' quest.
        //Map: techfortress
        // I CPU U!
        Core.KillQuest(7637, "techfortress", "CryptHacker");
        // Keep Droning On
        Core.KillQuest(7638, "techfortress", "NecroDrone");
        // Gathering Data
        Core.KillQuest(7639, "techfortress", "CryptHacker");
        // Signal Flare
        Core.KillQuest(7640, "techfortress", "NecroDrone");
        Core.MapItemQuest(7640, "techfortress", 7561);
        // Medal Time
        Core.KillQuest(7641, "techfortress", "NecroMech");
        // Hidden Data
        Core.KillQuest(7642, "techfortress", "NecroMech");
        // Pass the CPU
        Core.KillQuest(7643, "techfortress", "CPU");
        // The Guards
        Core.KillQuest(7644, "techfortress", "DoomBorg Guard");
        // Circui-try This
        Core.KillQuest(7645, "techfortress", "DoomBorg Guard");
        // The Thing
        Core.KillQuest(7646, "techfortress", "Vortrix");
        // The Final Form
        Core.KillQuest(7653, "techfortress", "MechaVortrix");
    }

    public void QuestsForUnicornEssence()
    {
        if (Core.isCompletedBefore(3157))
            return;

        Core.AddDrop("Unicorn Essence");

        // Hunt for the Brutal Intruder
        Core.MapItemQuest(3148, "undergroundlab", 2144);
        Core.MapItemQuest(3148, "undergroundlab", 2145);
        Core.MapItemQuest(3148, "undergroundlab", 2146);
        Core.MapItemQuest(3148, "undergroundlab", 2147);
        Core.MapItemQuest(3148, "undergroundlab", 2148);
        // Brutal Detection Desired
        Core.MapItemQuest(3149, "undergroundlab", 2150);
        Core.MapItemQuest(3149, "undergroundlab", 2151);
        Core.MapItemQuest(3149, "undergroundlabb", 2152);
        Core.MapItemQuest(3149, "undergroundlabb", 2153);
        // Silence the Green Screamers
        Core.KillQuest(3150, "undergroundlabb", "Green Screamer");
        // Soundbooth of Horrors
        Core.KillQuest(3151, "undergroundlabb", "Soundbooth Horror");
        // Skeletons in the Server Closet
        Core.KillQuest(3152, "undergroundlabb", "Closet Skeleton");
        // Server Gremlags
        Core.KillQuest(3153, "undergroundlabb", "Server Gremlin");
        // Window Slayer
        Core.KillQuest(3154, "undergroundlabb", "Window");
        // Gamer Fuel
        if (!Core.QuestProgression(3155))
        {
            Core.EnsureAccept(3155);
            Core.HuntMonster("undergroundlabb", "Invisible Ninjas", "Sushi");
            Core.HuntMonster("undergroundlabb", "Invisible Ninjas", "Pocky");
            Core.HuntMonster("undergroundlabb", "Invisible Ninjas", "Coffee");
            Core.HuntMonster("undergroundlabb", "Invisible Ninjas", "TableGum");
            Core.EnsureComplete(3155);
        }
        // Key to Doom
        Core.KillQuest(3156, "undergroundlabb", "Rabid Server Hamster");
        // UltraBrutal Battle
        Core.KillQuest(3157, "undergroundlabb", "Ultra Brutalcorn");
    }
}