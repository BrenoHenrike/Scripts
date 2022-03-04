//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class DoomwoodPart3
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        StoryLine();

        Core.SetOptions(false);
    }

    public string[] StoryLineDrops =
    {
        "Deadtech War Medal",
        "Zealous Badge",
        "Salvaged Deadtech Node",
        "Kyger"
    };

    public void StoryLine()
    {
        if (Core.isCompletedBefore(7653))
            return;
        ///thorngarde > /stonewood > /techdungeon > /stonewooddeep > /techfortress

        //Part1
        //Map: Thorngarde
        // Bullies
        Story.KillQuest(7589, "Thorngarde", new[] { "CryptHacker", "NecroMech", "NecroDrone" });
        // Free the People
        Story.KillQuest(7590, "Thorngarde", "NecroMech");
        Story.MapItemQuest(7590, "Thorngarde", 7485, 5);
        // Sample It
        Story.KillQuest(7591, "Thorngarde", "NecroMech");
        // Swat the Flies
        Story.KillQuest(7592, "Thorngarde", "CryptHacker");
        // Reclaim the Supplies
        Story.KillQuest(7593, "Thorngarde", new[] { "NecroDrone", "NecroMech", "CryptHacker" });

        //Part1.Kyger - Requirements: Must have completed the 'Reclaim the Supplies' quest.
        // http://aqwwiki.wikidot.com/kyger-s-quests#Thorn
        // Who's That?
        Story.MapItemQuest(7594, "Thorngarde", 7486);
        // Find my Stuff
        Story.KillQuest(7595, "Thorngarde", "NecroDrone");
        Story.MapItemQuest(7595, "Thorngarde", 7487);
        Story.MapItemQuest(7595, "Thorngarde", 7488);
        // Save my Friends
        Story.KillQuest(7596, "Thorngarde", "NecroMech");
        Story.MapItemQuest(7596, "Thorngarde", 7489);
        Story.MapItemQuest(7596, "Thorngarde", 7490);
        Story.MapItemQuest(7596, "Thorngarde", 7491);
        // Pull the Wires
        Story.KillQuest(7597, "Thorngarde", "CryptHacker");
        // Dress-up Time!
        Story.KillQuest(7598, "Thorngarde", new[] { "CryptHacker", "NecroDrone" });
        // I Need a Distraction
        Story.KillQuest(7599, "Thorngarde", new[] { "NecroDrone", "NecroMech", "CryptHacker" });


        //Part1.1 - Requirements: Must have completed the 'Zyrus is Lost' quest.
        // Zyrus is Lost
        Story.KillQuest(7600, "Thorngarde", "Zyrus the BioKnight");
        // Investigate the Tech
        Story.KillQuest(7601, "Thorngarde", new[] { "NecroDrone", "NecroMech", "CryptHacker", "Zyrus the BioKnight" });

        //Part1.2 - Requirements: Must have completed the 'Zyrus is Lost' quest.
        //Map: Stonewood
        // Defeat the Deadtech
        Story.KillQuest(7603, "Stonewood", "NecroDrone");
        // Gather Spell Components
        Story.KillQuest(7604, "Stonewood", "Doomwood Treeant");
        Story.MapItemQuest(7604, "Stonewood", 7510, 6);
        // A Few Things More
        Story.KillQuest(7605, "Stonewood", new[] { "NecroDrone", "NecroMech" });
        // Search for the Axe
        Story.MapItemQuest(7606, "Stonewood", 7511, 4);
        Story.MapItemQuest(7606, "Stonewood", 7512);

        //Part1.2.1 - http://aqwwiki.wikidot.com/stonewood-s-quests
        // Defeat the BioKnight
        Story.KillQuest(7607, "Stonewood", "BioKnight");
        // Take the Axe!
        Story.MapItemQuest(7608, "Stonewood", 7513);


        //Part1.3 - Requirements: Must have completed the 'Take the Axe!' quest.
        //Map: Techdungeon
        // Find the Keys
        Story.KillQuest(7609, "Techdungeon", "NecroMech");
        // Ew, Rats
        Story.KillQuest(7610, "Techdungeon", "Rotting Rat");
        Story.MapItemQuest(7610, "Techdungeon", 7515, 6);
        // Grab Their Gear
        if (!Story.QuestProgression(7611))
        {
            Core.EnsureAccept(7611);
            Core.KillMonster("Techdungeon", "r4", "left", "DoomBorg Guard", "Guard Armor Piece", 5);
            Core.KillMonster("Techdungeon", "r4", "left", "DoomBorg Guard", "Guard Helm");
            Core.KillMonster("Techdungeon", "r4", "left", "DoomBorg Guard", "Deadtech Polearm");
            Core.EnsureComplete(7611);
        }
        // Talk to the Guard
        Story.MapItemQuest(7612, "Techdungeon", 7514);
        // Let Them Out!
        Story.MapItemQuest(7613, "Techdungeon", 7516);
        // Head on Out
        Story.KillQuest(7614, "Techdungeon", "DoomBorg Guard");
        Story.MapItemQuest(7614, "Techdungeon", 7517);
        // Take Him Down
        Story.KillQuest(7615, "Techdungeon", "Kalron the Cryptborg");

        //Part1.4 - Requirements: Must have completed the 'Take Him Down' quest.
        //Map: Techdungeon
        // A Solemn Favor
        Story.KillQuest(7616, "Techdungeon", new[] { "Kalron the Cryptborg", "DoomBorg Guard" });
        //http://aqwwiki.wikidot.com/cat-ears-quest :
        // Cat Ears?
        //Map: stonewooddeep
        Story.MapItemQuest(7623, "stonewooddeep", 7528, 1);
        //http://aqwwiki.wikidot.com/stonewood-forest-s-quests :
        // EN GARDE!
        //Map: stonewooddeep
        Story.KillQuest(7624, "stonewooddeep", "Asherion");

        //Part1.4.1 - Requirements: Must have completed the 'EN GARDE!' quest.
        // http://aqwwiki.wikidot.com/kyger-s-quests#Forest
        // The Light Of Destiny
        Story.MapItemQuest(7625, "stonewooddeep", 7529);

        //Part1.4.2 - http://aqwwiki.wikidot.com/blinding-light-of-destiny-s-quests
        //Requirements: Must have completed the 'The Light Of Destiny' quest.
        //Map: stonewooddeep
        // Stuff for Dummies
        Story.KillQuest(7626, "stonewooddeep", "Doomwood Treeant");
        Story.MapItemQuest(7626, "stonewooddeep", 7530, 8);
        // Build the Dummies
        Story.KillQuest(7627, "stonewooddeep", "Doomwood Slime");
        Story.MapItemQuest(7627, "stonewooddeep", 7531, 6);
        // Battle the Dummies
        Story.KillQuest(7628, "stonewooddeep", "Target Dummy");
        // Lesson 1: Bravery
        Story.KillQuest(7629, "stonewooddeep", "Doomwood Slime");
        // Lesson 2: Armor
        Story.KillQuest(7630, "stonewooddeep", "Doomwood Slime");
        // Lesson 3: Protection
        if (!Story.QuestProgression(7631))
        {
            Core.EnsureAccept(7631);
            Core.HuntMonster("stonewooddeep", "CryptHacker", "Crypthacker Slain", 10);
            Core.HuntMonster("stonewooddeep", "CryptHacker", "Unidentified Clue");
            Core.EnsureComplete(7631);
        }
        // Lesson 4: Gather Intelligence
        Story.KillQuest(7632, "stonewooddeep", "CryptHacker");
        // Lesson 5: Demolition
        Story.KillQuest(7633, "stonewooddeep", new[] { "Doomwood Slime", "NecroDrone" });
        // Lesson 6: Searching
        Story.MapItemQuest(7634, "stonewooddeep", 7532);
        // Get the Axe
        Story.MapItemQuest(7635, "stonewooddeep", 7533);
        // Never Give Up   
        Story.KillQuest(7636, "stonewooddeep", "Sir Kut");

        //Part1.5 - Requirements: Must have completed the 'Never Give Up' quest.
        //Map: techfortress
        // I CPU U!
        Story.KillQuest(7637, "techfortress", "CryptHacker");
        // Keep Droning On
        Story.KillQuest(7638, "techfortress", "NecroDrone");
        // Gathering Data
        Story.KillQuest(7639, "techfortress", "CryptHacker");
        // Signal Flare
        Story.KillQuest(7640, "techfortress", "NecroDrone");
        Story.MapItemQuest(7640, "techfortress", 7561);
        // Medal Time
        Story.KillQuest(7641, "techfortress", "NecroMech");
        // Hidden Data
        Story.KillQuest(7642, "techfortress", "NecroMech");
        // Pass the CPU
        Story.KillQuest(7643, "techfortress", "CPU");
        // The Guards
        Story.KillQuest(7644, "techfortress", "DoomBorg Guard");
        // Circui-try This
        Story.KillQuest(7645, "techfortress", "DoomBorg Guard");
        // The Thing
        Story.KillQuest(7646, "techfortress", "Vortrix");
        // The Final Form
        Story.KillQuest(7653, "techfortress", "MechaVortrix");
    }
}