//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class DoomwoodPart3
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(IScriptInterface bot)
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
        {
            Core.Logger($"Story: Doomwood Part 3 - Complete");
            return;
        }

        Story.PreLoad(this);

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
        if (!Story.QuestProgression(7593))
        {
            Core.EnsureAccept(7593);
            Core.HuntMonster("Thorngarde", "CryptHacker", "Cured Meat", 3);
            Core.HuntMonster("Thorngarde", "NecroMech", "Bag of Grain", 3);
            Core.HuntMonster("Thorngarde", "NecroDrone", "Jug of Water", 5);
            Core.EnsureComplete(7593);
        }

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
        if (!Story.QuestProgression(7599))
        {
            Core.EnsureAccept(7599);
            Core.HuntMonster("Thorngarde", "NecroMech", "NecroMech Slain", 5);
            Core.HuntMonster("Thorngarde", "CryptHacker", "CryptHacker Slain", 5);
            Core.HuntMonster("Thorngarde", "NecroDrone", "NecroDrone Slain", 5);
            Core.EnsureComplete(7599);
        }

        // Zyrus is Lost
        Story.KillQuest(7600, "Thorngarde", "Zyrus the BioKnight");

        // Investigate the Tech
        if (!Story.QuestProgression(7601))
        {
            Core.EnsureAccept(7601);
            Core.HuntMonster("Thorngarde", "NecroDrone", "Deadtech Power Core", 7);
            Core.HuntMonster("Thorngarde", "NecroMech", "NecroMech Targeting Systems", 5);
            Core.HuntMonster("Thorngarde", "CryptHacker", "CryptHacker Circuitry", 15);
            Core.HuntMonster("Thorngarde", "Zyrus the BioKnight", "BioKnight Engine", 3);
            Core.EnsureComplete(7601);
        }

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

        // Defeat the BioKnight
        Story.KillQuest(7607, "Stonewood", "BioKnight");

        // Take the Axe!
        Story.MapItemQuest(7608, "Stonewood", 7513);

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

        // A Solemn Favor
        Story.KillQuest(7616, "Techdungeon", new[] { "Kalron the Cryptborg", "DoomBorg Guard" });

        //Map: stonewooddeep
        Story.MapItemQuest(7623, "stonewooddeep", 7528, 1);

        // EN GARDE!
        //Map: stonewooddeep
        Story.KillQuest(7624, "stonewooddeep", "Asherion");

        // The Light Of Destiny
        Story.MapItemQuest(7625, "stonewooddeep", 7529);

        // Stuff for Dummies
        if (!Story.QuestProgression(7626))
        {
            Bot.Options.AttackWithoutTarget = true;
            Core.EnsureAccept(7626);
            Core.KillMonster("stonewooddeep", "r2", "Right", "Doomwood Treeant", "Sturdy Wood", 8);
            Story.MapItemQuest(7626, "stonewooddeep", 7530, 8);
            Bot.Options.AttackWithoutTarget = false;
        }

        // Build the Dummies
        if (!Story.QuestProgression(7627))
        {
            Core.EnsureAccept(7627);
            Bot.Options.AttackWithoutTarget = true;
            Core.KillMonster("stonewooddeep", "r3", "Right", "Doomwood Treeant", "Area Cleared", 10);
            Bot.Options.AttackWithoutTarget = false;
            Core.GetMapItem(7531, 6, "stonewooddeep");
            Core.EnsureComplete(7627);
        }

        // Battle the Dummies
        if (!Story.QuestProgression(7628))
        {
            Core.EnsureAccept(7628);
            Core.HuntMonsterMapID("stonewooddeep", 4 | 5, "Target Dummy Slain", 6);
            Core.EnsureComplete(7628);
        }

        // Lesson 1: Bravery
        if (!Story.QuestProgression(7629))
        {
            Core.EnsureAccept(7629);
            Core.KillMonster("stonewooddeep", "r3", "Right", "Doomwood SLime", "Slime Slain", 10);
            Core.EnsureComplete(7629);
        }

        // Lesson 2: Armor
        if (!Story.QuestProgression(7630))
        {
            Core.EnsureAccept(7630);
            Core.KillMonster("stonewooddeep", "r3", "Right", "Doomwood SLime", "Stolen Armor", 7);
            Core.EnsureComplete(7630);
        }

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
        if (!Story.QuestProgression(7633))
        {
            Core.EnsureAccept(7633);
            Core.KillMonster("stonewooddeep", "r3", "Right", "Doomwood SLime", "Acid Ooze", 6);
            Core.HuntMonster("stonewooddeep", "NecroDrone", "Explosive Tech", 6);
            Core.EnsureComplete(7633);
        }

        // Lesson 6: Searching
        Story.MapItemQuest(7634, "stonewooddeep", 7532);

        // Get the Axe
        Story.MapItemQuest(7635, "stonewooddeep", 7533);

        // Never Give Up   
        Story.KillQuest(7636, "stonewooddeep", "Sir Kut");

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
        if (!Story.QuestProgression(7641))
        {
            Core.EnsureAccept(7641);
            Core.HuntMonster("techfortress", "NecroDrone", "NecroMedals", 5);
            Core.EnsureComplete(7641);
        }

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