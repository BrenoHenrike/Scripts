//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class CoreQOM
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreFarms Farm = new CoreFarms();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.RunCore();
    }

    public void CompleteEverything()
    {
        aTheftofLight();
        DoomwoodPaladinsTrial();
        DarkoviaDarkDiaspora();
        ShadowfallDarknessRising();
        SwordhavenTheNewWorld();
        TheDestroyer();
        TheReshaper();
        TheBook();
        TheQueensSecrets();
    }

    public void aTheftofLight()
    {
        //Progress Check
        if (Core.isCompletedBefore(5387))
            return;

        //Preload Quests
        Story.PreLoad(this);

        //Summon Help
        Story.MapItemQuest(4495, "celestialrealm", 3698);
        Story.KillQuest(4495, "celestialrealm", new[] { "Fallen Knight", "Celestial Bird of Paradise" });

        //Power Up!
        if (!Story.QuestProgression(4496))
            Core.BuyItem("embersea", 1100, "Basic Guard Potion", 10);
        Story.KillQuest(4496, "celestialrealm", new[] { "Celestial Bird of Paradise", "Fallen Knight" });

        //The Final Spell Fragment
        Story.MapItemQuest(4497, "celestialrealm", 3696);

        //Find the Map
        if (!Story.QuestProgression(4498))
        {
            Core.AddDrop("Dwakel Decoder");
            Core.GetMapItem(106, 1, "crashsite");
            Story.KillQuest(4498, "celestialrealm", "Infernal Knight");
        }

        //Reveal the Portal!
        Story.MapItemQuest(4499, "celestialrealm", 3693);

        //Investigate the Ruins
        Story.MapItemQuest(4500, "lostruins", 3694, 3);
        Story.KillQuest(4500, "lostruins", "Underworld Hound");

        //Take out the Knights
        Story.KillQuest(4501, "lostruins", "Fallen Knight");

        //Find the Clues
        if (!Story.QuestProgression(4502))
        {
            Core.EnsureAccept(4502);
            Core.HuntMonster("lostruins", "Underworld Hound", "Clue 1");
            Core.HuntMonster("lostruins", "Infernal Imp", "Clue 2");
            Core.HuntMonster("lostruins", "Fallen Knight", "Clue 3");
            Core.KillMonster("lostruins", "r5", "Left", "Infernal Knight", "Clue 4");
            Core.EnsureComplete(4502);
        }

        //Recover the Cage Key!
        if (!Story.QuestProgression(4503))
        {
            Core.EnsureAccept(4503);
            Farm.FishingREP(2);
            if (!Core.CheckInventory("Holy Oil"))
                Core.BuyItem("fishing", 356, "Holy Oil");
            Core.HuntMonster("lostruins", "Fallen Knight", "Cage Key");
            Core.EnsureComplete(4503);
        }

        //Protect Them
        if (!Story.QuestProgression(4504))
        {
            Core.EnsureAccept(4504);
            if (!Core.CheckInventory("Potent Guard Potion", 10))
                Core.BuyItem("embersea", 1100, "Potent Guard Potion", 10);
            Core.HuntMonster("lostruins", "Fallen Knight", "Infernal Enemy Defeated", 15);
            Core.EnsureComplete(4504);
        }

        //Break the Spell
        if (!Story.QuestProgression(4505))
        {
            Core.EnsureAccept(4505);
            Core.HuntMonster("lostruins", "Fallen Knight", "Brimstone-Stained Gauntlet", 5);
            Core.HuntMonster("lostruins", "Underworld Hound", "Onyx Spike", 3);
            Core.GetMapItem(3697, 5, "lostruins");
            Core.EnsureComplete(4505);
        }

        //Open the Cage
        Story.MapItemQuest(4506, "lostruins", 3695);

        //Defeat the Infernal Warlord
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(4507, "lostruins", "Infernal Warlord");

        //Celestial Realm at War
        Story.KillQuest(4509, "lostruinswar", "Infernal Imp");

        //Defeat the Diabolical Warlord!
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(4508, "lostruinswar", "Diabolical Warlord");

        //Infernal Destruction
        Story.KillQuest(5374, "infernalspire", new[] { "Fallen Knight", "Underworld Hound" });

        //Gone Without A Trace
        Story.KillQuest(5375, "infernalspire", new[] { "Fallen Knight", "Underworld Hound" });
        Story.MapItemQuest(5375, "infernalspire", 4729);
        Story.MapItemQuest(5375, "infernalspire", 4730);

        //Helzekiel
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(5376, "infernalspire", "Helzekiel");

        //Get the Keys
        Story.KillQuest(5377, "infernalspire", new[] { "Infernal Hound", "Dungeon Fiend", "Dungeon Fiend" });

        //Free the Captives
        Story.MapItemQuest(5378, "infernalspire", 4731, 6);

        //Energy Needed
        Story.KillQuest(5379, "infernalspire", "Dungeon Fiend");
        Story.MapItemQuest(5379, "infernalspire", 4732, 6);

        //Interrogate the Jailer
        Story.KillQuest(5380, "infernalspire", "Garvodeus");

        //Get the Code
        if (!Story.QuestProgression(5381))
        {
            Core.EnsureAccept(5381);
            Core.KillMonster("infernalspire", "r13", "Left", "Fallen Knight", "Override Code");
            Core.KillMonster("infernalspire", "r13", "Left", "Fallen Knight", "Fallen Knight Slain", 6);
            Core.HuntMonster("infernalspire", "Infernal Imp", "Infernal Imp Slain", 6);
            Core.EnsureComplete(5381);

        }

        //Enter the Code
        Story.MapItemQuest(5382, "infernalspire", 4733);

        //Smash it Up
        Story.MapItemQuest(5383, "infernalspire", 4734, 4);

        //Take out the Overseer
        Story.KillQuest(5384, "infernalspire", "Azkorath");

        //Clear the Invaders
        Story.KillQuest(5385, "infernalspire", new[] { "Infernal Knight", "Grievous Fiend" });

        //Find the Weapon
        Story.MapItemQuest(5386, "infernalspire", 4735);

        //What is THAT?
        Story.KillQuest(5387, "infernalspire", "Malxas");
    }

    public void DoomwoodPaladinsTrial()
    {
        //Progress Check
        if (Core.isCompletedBefore(5416))
            return;

        //Preload Quests
        Story.PreLoad(this);

        //A New Recruit
        if (!Story.QuestProgression(5404))
        {
            Core.EnsureAccept(5404);
            if (!Core.CheckInventory("Blessed Coffee of the Lightguard"))
            {
                Core.AddDrop("Blessed Coffee of the Lightguard");
                Core.EnsureAccept(5405);
                Core.HuntMonster("sandsea", "Oasis Monkey", "Pally Luwak Beans");
                Core.EnsureComplete(5405);
            }
            Core.EnsureComplete(5404);
        }

        //SLAY some UNDEAD
        Story.KillQuest(5406, "doompally", "Doomwood Ectomancer");

        //SLAY some MORE UNDEAD!!
        Story.KillQuest(5407, "doompally", "Doomwood Soldier");

        //Slay BIGGER!!
        Story.KillQuest(5408, "doompally", "Doomwood Ectomancer");

        //Babe in the Woods
        Story.MapItemQuest(5409, "doompally", 4758);

        //The Dark Thicket
        Story.KillQuest(5410, "doompally", "Doomwood Bonemuncher");
        Story.MapItemQuest(5410, "doompally", 4759, 5);

        //Emily
        Story.MapItemQuest(5411, "doompally", 4761);
        Story.MapItemQuest(5411, "doompally", 4760, 6);

        //Sage Advice
        Story.KillQuest(5412, "doompally", "Doomwood Treeant");

        //Abra-Cadaver
        Story.KillQuest(5413, "doompally", "Doomwood Ectomancer");

        //A (Skele)TON of skulls
        Story.KillQuest(5414, "doompally", "Doomwood Bonemuncher");

        //Summoning the Subjugator
        Story.MapItemQuest(5415, "doompally", 4762);

        //Subjugator Wraithbone
        Story.KillQuest(5416, "doompally", "Skeletal Subjugator");
    }

    public void DarkoviaDarkDiaspora()
    {
        //Progress Check
        if (Core.isCompletedBefore(5503))
            return;

        //Preload Quests
        Story.PreLoad(this);

        //Hounds and Infernals and Imp, Oh my!
        Story.KillQuest(5487, "DarkoviaInvasion", new[] { "Underworld Hound", "Infernal Imp" });

        //Like Imps in A Pod
        Story.MapItemQuest(5488, "DarkoviaInvasion", 4905, 6);

        //A Grievous Threat
        Story.KillQuest(5489, "DarkoviaInvasion", "Grievous Fiend");

        //Undead Investigation
        Story.MapItemQuest(5490, "SafiriaInvasion", 4904);

        //What We Need Is A Big Can Of Raid
        Story.KillQuest(5491, "SafiriaInvasion", new[] { "Fallen Knight", "Infernal Knight" });

        //Trapped!
        Story.MapItemQuest(5492, "SafiriaInvasion", 4895, 6);
        Story.MapItemQuest(5492, "SafiriaInvasion", 4896);

        //Won't Someone Think Of The Minions?
        Story.MapItemQuest(5493, "SafiriaInvasion", 4897, 9);

        //Here, Doggy Doggy
        Story.MapItemQuest(5494, "SafiriaInvasion", new[] { 4898, 4899 });
        Story.KillQuest(5494, "SafiriaInvasion", "Blood Maggot");

        //Ma'alech
        Story.KillQuest(5495, "SafiriaInvasion", "Ma'alech");

        //I'm Not Lycan This Situation
        Story.MapItemQuest(5496, "LycanInvasion", 4900);

        //The Best Way To Slay An Infernal
        Story.KillQuest(5497, "LycanInvasion", new[] { "Fallen Knight", "Infernal Knight" });

        //A Dire Situation
        Story.KillQuest(5498, "LycanInvasion", "Dire Wolf");

        //I'd Lycan To Go Now
        Story.MapItemQuest(5499, "LycanInvasion", 4901);
        Story.MapItemQuest(5499, "LycanInvasion", 4903, 6);

        //Lord Balax'el
        Story.KillQuest(5500, "LycanInvasion", "Lord Balax'el");

        //Follow Lady Solani
        Story.MapItemQuest(5501, "SafiriaInvasion", 4902);
        Story.KillQuest(5501, "SafiriaInvasion", "Fallen Knight");

        //Revenant Slayer
        Story.KillQuest(5502, "SafiriaInvasion", new[] { "Revenant", "Shadow Imp" });

        //Noddharath
        Story.KillQuest(5503, "SafiriaInvasion", "Noddharath");
    }

    public void ShadowfallDarknessRising()
    {
        //Progress Check
        if (Core.isCompletedBefore(5557))
            return;

        //Preload Quests
        Story.PreLoad(this);

        //Commander Tibias
        Story.MapItemQuest(5543, "ShadowfallInvasion", 5024);

        //Clear the Walls
        Story.KillQuest(5544, "ShadowfallInvasion", new[] { "Infernal Imp", "Infernal Knight" });

        //Load the Ballistas
        Story.MapItemQuest(5545, "ShadowfallInvasion", 5025, 4);
        Story.MapItemQuest(5545, "ShadowfallInvasion", 5026, 4);

        //Arm the Archers
        Story.KillQuest(5546, "ShadowfallInvasion", "Infernal Imp");

        //Reinforce the Walls
        Story.MapItemQuest(5547, "ShadowfallInvasion", 5027, 5);

        //Work Done!
        Story.MapItemQuest(5548, "ShadowfallInvasion", 5028);

        //Clear the Tower
        Story.KillQuest(5549, "ShadowfallInvasion", "Bone Creeper");

        //Find the Passage
        Story.MapItemQuest(5550, "ShadowfallInvasion", 5029);

        //Go Through That Door
        Story.KillQuest(5551, "ShadowfallInvasion", new[] { "Bone Guardian", "Bone Guardian" });

        //Infernal Attack
        Story.KillQuest(5552, "ShadowfallInvasion", new[] { "Nethermage", "Diabolical Scryer", "Fallen Knight" });

        //Here's A Hammer, Get To Work
        Story.MapItemQuest(5553, "ShadowfallInvasion", 5030, 9);

        //The Next Step
        Story.MapItemQuest(5554, "ShadowfallInvasion", 5031);

        //Soul Fuel
        Story.MapItemQuest(5555, "ShadowfallInvasion", 5032);
        Story.KillQuest(5555, "ShadowfallInvasion", "Diabolical Scryer");

        //Time to Fly
        Story.MapItemQuest(5556, "ShadowfallInvasion", 5033);

        //YOU again!
        Story.KillQuest(5557, "ShadowfallInvasion", "Lord Balax'el");
    }

    public void SwordhavenTheNewWorld()
    {
        //Progress Check
        if (Core.isCompletedBefore(5586))
            return;

        //Preload Quests
        Story.PreLoad(this);

        //Stand for Swordhaven
        Story.KillQuest(5575, "LycanInvasion", new[] { "Fallen Knight", "Infernal Knight" });

        //Use Their Energy Against Them
        Story.KillQuest(5576, "ShadowfallInvasion", "Nethermage");

        //Scry this!
        Story.KillQuest(5577, "ShadowfallInvasion", "Diabolical Scryer");

        //Recalibration
        if (!Story.QuestProgression(5578))
        {
            Core.EnsureAccept(5578);
            Core.KillMonster("DoomPally", "r3", "Right", "*", "Doomwood Invaders Fought", 4);
            Core.KillMonster("Darkovianvasion", "Enter", "Spawn", "*", "Darkovia Invaders Fought", 4);
            Core.KillMonster("ShadowfallInvasion", "r4", "Left", "*", "Shadowfall Invaders Fought", 4);
            Core.EnsureComplete(5578);
        }

        //Arm the Armored!
        Story.MapItemQuest(5579, "CastleInvasion", 5055, 5);
        Story.MapItemQuest(5579, "CastleInvasion", 5056);

        //Save the Citizens
        Story.MapItemQuest(5580, "CastleInvasion", 5058, 5);

        //Destroy the Infernals
        Story.KillQuest(5581, "CastleInvasion", new[] { "Infernal Knight", "Fallen Knight", "Nethermage" });

        //What is THAT??
        Story.KillQuest(5582, "CastleInvasion", "Giant Worm of Teeth");

        //Arm the Civilians
        Story.MapItemQuest(5583, "CastleInvasion", 5057, 4);

        //Get Those Beasts
        Story.KillQuest(5584, "CastleInvasion", new[] { "Infernal Imp", "Underworld Hound" });

        //It's Baaaaack!
        Story.KillQuest(5585, "CastleInvasion", "Giant Worm of Teeth");

        //Him Again???
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(5586, "CastleInvasion", "Lord Balax'el");
    }

    public void TheDestroyer()
    {
        if (Core.isCompletedBefore(5847))
            return;

        //Preload Quests
        Story.PreLoad(this);

        //What Happened to Baldric?
        Story.MapItemQuest(5791, "therift", 5228);

        //Quests Here Bug Out So This is Needed.
        Core.AcceptandCompleteTries = 3;

        //Get a Monitor
        Story.MapItemQuest(5803, "charredpath", 5248);

        //Follow the Pattern.. He Ne Ar Kr
        Story.MapItemQuest(5804, "crashsite", 5249);
        Story.KillQuest(5804, "crashsite", "Dwakel Warrior");

        //The PTM is Ready!
        Story.MapItemQuest(5805, "charredpath", 5256);
        Story.KillQuest(5805, "charredpath", new[] { "Noxious Fumes", "Toxic Bile" });

        //Save the Creatures
        Story.MapItemQuest(5806, "charredpath", 5250, 6);
        Story.KillQuest(5806, "charredpath", "Ragewing");

        //Clear the Treeants
        Story.MapItemQuest(5807, "charredpath", 5251, 3);
        Story.KillQuest(5807, "charredpath", "Toxic Treeant");

        //Excise the Infection
        Story.KillQuest(5808, "charredpath", "Infected Hare");

        //Root out the Plague
        Story.MapItemQuest(5809, "charredpath", 5252, 6);
        Story.KillQuest(5809, "charredpath", "Plague Spreader");

        //I Said Yes, Yes, Yes
        Story.MapItemQuest(5810, "charredpath", 5255);

        //Rally the Mages
        Story.MapItemQuest(5811, "therift", 5253, 4);
        Story.KillQuest(5811, "therift", "Mana Chest");

        //Wisteria Hysteria
        Story.KillQuest(5812, "charredpath", "Toxic Wisteria");

        //Confront Extriki
        Story.MapItemQuest(5813, "charredpath", 5254);

        //Remove the Bile
        Story.KillQuest(5819, "charredpath", "Noxious Fumes");

        //Get the Plague (Crystal)
        Story.KillQuest(5820, "charredpath", "Plague Spreader");

        //Grab the Growth
        Story.KillQuest(5821, "charredpath", "Infected Hare");

        //Treeants for Wood
        Story.KillQuest(5822, "farm", "Treeant");

        //Sand sounds better than Litter...
        if (!Story.QuestProgression(5823))
        {
            Core.EnsureAccept(5823);
            Core.HuntMonster("baconcat", "Litter Elemental", "Absorbent \"Sand\"", 8);
            Core.EnsureComplete(5823);
        }

        //Destroy the Zognax
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(5824, "charredpath", "Zognax");

        //Bandages Needed
        Story.KillQuest(5830, "charredpath", "Ravenous Parasite");

        //Shinies!
        Story.KillQuest(5831, "skytower", new[] { "Sunstone", "Moonstone", "Star Sapphire" });

        //Plushies
        Story.KillQuest(5832, "sewerpink", "Cutie Grumbley");

        //Sleepies
        if (!Story.QuestProgression(5833))
        {
            Core.EnsureAccept(5833);

            if (!Core.CheckInventory("Black Metal Cold Brew"))
            {
                Core.AddDrop("Black Metal Cold Brew");
                Core.EnsureAccept(5834);
                Core.HuntMonster("therift", "Mana Chest", "Liquid Mana", 4);
                Core.HuntMonster("therift", "Ravenous Parasite", "Parasite \"Spice\"", 4);
                Core.EnsureComplete(5834);
            }

            Core.EnsureComplete(5833);
        }

        //What's Next?
        Story.MapItemQuest(5835, "charredpath", 5270);

        //Make a Bed
        Story.KillQuest(5836, "charredpath", "Pustulisk");

        //Talk to Ravinos
        Story.MapItemQuest(5837, "underglade", 5271);

        //Into the Underglade
        Story.KillQuest(5838, "underglade", new[] { "Forest Spirit", "Tree Nymph" });

        //Clear the Spores
        Story.KillQuest(5839, "underglade", "Slime Spore");

        //Cleanse the Walls
        Story.MapItemQuest(5840, "underglade", 5272, 8);
        Story.KillQuest(5840, "underglade", "Forest Spirit");

        //Expose the Entrance
        Story.KillQuest(5841, "underglade", "Blackened Earth");

        //Gather the Glows
        Story.KillQuest(5842, "underglade", "Luminous Fungus");

        //More Corruption Revealed
        Story.MapItemQuest(5843, "underglade", 5273, 6);
        Story.KillQuest(5843, "underglade", "Forest Spirit");

        //The Goblin Threat
        Story.KillQuest(5844, "underglade", "Twisted Goblin");

        //Get an Offering
        Story.KillQuest(5845, "underglade", "Gemstone Elemental");

        //The Heart of the Glade
        Story.KillQuest(5846, "underglade", "Lunamoss");

        //Defeat Extriki
        Story.KillQuest(5847, "extriki", "Extriki");
    }

    public void TheReshaper(bool TerraneMerge = false)
    {
        //Progress Check
        if (Core.isCompletedBefore(5877))
            return;

        //Preload Quests
        Story.PreLoad(this);

        //Capture the Misshapen
        Story.KillQuest(5849, "Pilgrimage", new[] { "SpiderWing", "Urstrix" });

        //Defeat the Parasites
        Story.KillQuest(5850, "Pilgrimage", "Ravenous Parasite");

        //The Source
        Story.KillQuest(5851, "Pilgrimage", "Extrikiti");

        //Find Lucky!
        Story.MapItemQuest(5852, "Pilgrimage", 5289);
        Story.MapItemQuest(5852, "Pilgrimage", 5288, 3);
        Story.KillQuest(5852, "Pilgrimage", new[] { "Extrikiti", "Extrikiti" });

        //Gather a Scouting Party
        Story.MapItemQuest(5853, "Pilgrimage", 5290, 6);
        Story.MapItemQuest(5853, "Pilgrimage", 5291);

        //Defeat the Parasites
        Story.MapItemQuest(5854, "Pilgrimage", 5292);
        Story.KillQuest(5854, "Pilgrimage", "Ravenous Parasite");

        //Bad Dog
        Story.KillQuest(5855, "Pilgrimage", "Lucky");

        //Connect to the Earth
        Story.MapItemQuest(6276, "guardiantree", 5769, 5);
        Story.KillQuest(6276, "guardiantree", "Blossoming Treeant");

        //Help the Hedgies
        Story.MapItemQuest(6277, "guardiantree", 5776, 5);
        Story.MapItemQuest(6277, "guardiantree", 5770);

        //Cleanse the Corrupted Zards
        Story.KillQuest(6278, "guardiantree", "Corrupted Zard");

        //Plant the Seed
        if (!Story.QuestProgression(6279))
        {
            Core.EnsureAccept(6279);
            Core.KillMonster("guardiantree", "r2a", "Bottom", "Seed Spitter", "Perfect Seed");
            Core.GetMapItem(5771, map: "guardiantree");
            Core.EnsureComplete(6279);
        }

        //Reach the Top
        Story.MapItemQuest(6280, "guardiantree", 5772);

        //Cointain the Pollen
        Story.KillQuest(6281, "guardiantree", "Blossoming Treeant");

        //Pass Through the Pollen
        Story.MapItemQuest(6282, "guardiantree", 5773);
        Story.KillQuest(6282, "guardiantree", "Pollen Cloud");

        //Reinvigorate the Sprout
        if (!Story.QuestProgression(6283))
        {
            Core.EnsureAccept(6283);
            Core.KillMonster("guardiantree", "r8", "Left", "*", "Life Energy", 8);
            Core.EnsureComplete(6283);
        }

        //Up We Go!
        Story.MapItemQuest(6284, "guardiantree", 5774);

        //Grow a Bridge
        Story.MapItemQuest(6285, "guardiantree", 5775, 2);
        Story.KillQuest(6285, "guardiantree", "Myconid");

        //Take Down Terrane
        Story.KillQuest(6286, "guardiantree", "Terrane");
        if (TerraneMerge)
            return;

        //Explore the Cavern
        Story.MapItemQuest(5856, "TwistedCavern", 5293);
        Story.KillQuest(5856, "TwistedCavern", "Extrikiti");

        //Capture the Insects
        Story.KillQuest(5857, "TwistedCavern", "Infesting Swarm");

        //Face the Consequences
        if (!Story.QuestProgression(5858))
        {
            Core.EnsureAccept(5858);
            Core.HuntMonster("TwistedCavern", "Fungal Lord", "Fungal Lord Slain", 6);
            Core.HuntMonster("TwistedCavern", "Seed Stalker", "Seed Stalker Slain", 6);
            Core.HuntMonster("TwistedCavern", "Seed Stalker", "Scrap of Brown Cloth");
            Core.EnsureComplete(5858);
        }

        //Stalk This
        Story.KillQuest(5859, "TwistedCavern", "Seed Stalker");

        //Free the Flies
        Story.MapItemQuest(5860, "TwistedCavern", 5294);
        Story.KillQuest(5860, "TwistedCavern", "SpiderWing");

        //Follow the Swarm
        Story.MapItemQuest(5861, "TwistedCavern", 5295, 6);

        //Leading to the Wall
        Story.KillQuest(5862, "TwistedCavern", new[] { "Urstrix", "Fungal Lord" });

        //Get through the Wall
        Story.KillQuest(5863, "TwistedCavern", "Wall of Vines");

        //Defeat the Golem
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(5864, "TwistedCavern", "Lore Golem");

        //Time for a Dose
        Story.KillQuest(5866, "BrokenWoods", new[] { "Urstrix", "SpiderWing" });

        //Comparing Samples
        if (!Story.QuestProgression(5867))
        {
            Core.EnsureAccept(5867);
            Core.HuntMonster("brightoak", "Hootbear", "Hootbear Feathers", 2);
            Core.HuntMonster("BrokenWoods", "Urstrix", "Urstrix Feathers", 2);
            Core.HuntMonster("pines", "Leatherwing", "Leatherwing Claws", 2);
            Core.HuntMonster("BrokenWoods", "SpiderWing", "SpiderWing Claws", 2);
            Core.EnsureComplete(5867);
        }

        //Examine the Foliage
        Story.MapItemQuest(5868, "BrokenWoods", 5296, 4);
        Story.MapItemQuest(5868, "BrokenWoods", 5297, 4);
        Story.KillQuest(5868, "BrokenWoods", "Fungal Lord");

        //Investigate the Goo
        Story.KillQuest(5869, "BrokenWoods", "Extrikiti");

        //Second Dose
        Story.KillQuest(5870, "BrokenWoods", new[] { "Urstrix", "SpiderWing" });

        //Still Not Working
        Story.KillQuest(5871, "charredpath", new[] { "Ravenous Parasite", "Plague Spreader" });

        //Get the Final Ingredients       
        if (!Story.QuestProgression(5872))
        {
            Core.EnsureAccept(5872);
            Core.BuyItem("arcangrove", 211, "Health Potion", 25);

            Core.HuntMonster("Arcangrove", "Seed Spitter", "Uncorrupted Spitter Seeds", 3);
            Core.HuntMonster("poisonforest", "Treeant", "Treeant Berries", 4);
            Core.EnsureComplete(5872);
        }

        //Smash the Hive
        Story.KillQuest(5873, "BrokenWoods", "Hive");
        Story.PreLoad(this);

        //Acolyte's Medallions
        if (!Story.QuestProgression(5874))
        {
            Core.EnsureAccept(5874);
            Core.HuntMonster("Kolyaban", "Poisonous Darkblood", "Acolyte's Medallion", 4);
            Core.EnsureComplete(5874);
        }

        //Let's Hope This Works
        Story.KillQuest(5876, "Kolyaban", "Twisted Aria");

        //Defeat Kolyaban
        Story.KillQuest(5877, "Kolyaban", "Kolyaban");
    }

    public void TheBook()
    {
        if (Core.isCompletedBefore(8080))
            return;

        Story.PreLoad(this);

        //8048 | Time to Study
        Story.KillQuest(8048, "forestreach", new[] { "Monstrous Imp", "Eldritch Parasite" });

        //8049 | Calm Them Down
        Story.KillQuest(8049, "forestreach", "Chaos Spitter");

        //8050 | Get the Reagents
        Story.KillQuest(8050, "forestreach", "Chaos Sp-eye");
        Story.MapItemQuest(8050, "forestreach", 8362, 5);

        //8051 | Samples for Comparison
        Story.KillQuest(8051, "forestreach", "Chaos Spitter");
        Story.MapItemQuest(8051, "forestreach", 8363, 5);

        //8052 | More Testing Required
        Story.KillQuest(8052, "forestreach", new[] { "EldritchWing", "Chaos Sp-eye" });

        //8053 | Tidy Up
        Story.KillQuest(8053, "forestreach", new[] { "Eldritch Parasite", "Monstrous Imp" });

        //8054 | We Need Neutralizer
        Story.KillQuest(8054, "forestreach", new[] { "Chaos Spitter", "Chaos Sp-eye" });

        //8055 | Neutralize 'Em
        Story.MapItemQuest(8055, "forestreach", 8364, 5);

        //8056 | Vermin Clearing
        Story.KillQuest(8056, "backroom", "Chaos Rat");

        //8057 | Find the Book
        Story.MapItemQuest(8057, "backroom", 8365);

        //8058 | Get the Key
        Story.KillQuest(8058, "backroom", "Chaos Rat");

        //8059 | Get the Book
        Story.MapItemQuest(8059, "backroom", 8366);

        //8060 | It's a Book Wyrm!
        Story.KillQuest(8060, "backroom", "Book Wyrm");

        //8067 | The Eye of the Beholder
        Story.KillQuest(8067, "deepforest", "Creeping Gaze");

        //8068 | The Double - Edged Sword
        Story.KillQuest(8068, "deepforest", "Eldritch Stalker");

        //8069 | So Ichor - y
        Story.KillQuest(8069, "deepforest", "Terrarsite");

        //8070 | Reveal the Words
        Story.MapItemQuest(8070, "deepforest", 8415);

        //8071 | Shining Time
        Story.KillQuest(8071, "deepforest", "Deep Truffle");

        //8072 | Light the Way
        Story.MapItemQuest(8072, "deepforest", 8416, 8);

        //8073 | Chaos Gathers
        Story.KillQuest(8073, "deepforest", "Creeping Gaze");

        //8074 | Hold your Breath
        Story.KillQuest(8074, "deepforest", "Terrarsite");

        //8075 | Disperse the Mist
        Story.MapItemQuest(8075, "deepforest", 8418, 8);

        //8076 | Monsters in the Mist
        Story.KillQuest(8076, "deepforest", "Deep Truffle");

        //8077 | Truffle Hunting
        Story.KillQuest(8077, "deepforest", "Deep Truffle");

        //8078 | Bribe 'Em All
        Story.KillQuest(8078, "deepforest", "Cthulhoid");

        //8079 | Heading Back Home
        Story.MapItemQuest(8079, "deepforest", 8419, 4);
        Story.MapItemQuest(8079, "deepforest", 8420, 1);

        //8080 | The Aberration
        Story.KillQuest(8080, "deepforest", "Aberrant Horror");
    }

    public void TheQueensSecrets()
    {
        if (Core.isCompletedBefore(8107))
            return;

        Story.PreLoad(this);

        //8083 | Crystal Tears
        Story.KillQuest(8083, "transformation", "Monstrite");

        //8084 | That Which Grows
        Story.KillQuest(8084, "transformation", "Chaos Spitter");

        //8085 | That Which Flies
        Story.KillQuest(8085, "transformation", "Tentastrike");

        //8086 | That Which Flames
        Story.KillQuest(8086, "transformation", "Tentaflame");

        //8087 | A Caustic Oil
        Story.KillQuest(8087, "transformation", "Chaos Spitter");

        //8088 | Tools of the Trade
        if (!Story.QuestProgression(8088))
        {
            Core.EnsureAccept(8088);
            Core.HuntMonster("transformation", "Monstrite", "Mortar Stone");
            Core.HuntMonster("transformation", "Monstrite", "Pestle Stone");
            Core.EnsureComplete(8088);
        }

        //8089 | Burn it!
        Story.MapItemQuest(8089, "transformation", 8435);
        Story.MapItemQuest(8089, "transformation", 8436);
        Story.MapItemQuest(8089, "transformation", 8437);

        //8090 | Find the Tributes
        Story.KillQuest(8090, "transformation", "Deep Tunneler");

        //8091 | Compare the Crystal
        Story.KillQuest(8091, "transformation", "Monstrite");

        //8092 | Acid and Scratch Test
        Story.KillQuest(8092, "transformation", new[] { "Tentastrike", "Deep Tunneler" });

        //8093 | The Largest Monstrite
        Story.KillQuest(8093, "transformation", "Eldritch Abomination");

        //8094 | Tame the Queen
        Story.KillQuest(8094, "transformation", "Queen of Monsters");

        //8096 | Worms of Earth
        Story.KillQuest(8096, "downbelow", "Earthwyrm");

        //8097 | Find the Trail
        Story.KillQuest(8097, "downbelow", "Rumbling Rubble");

        //8098 | Open the Way
        Story.KillQuest(8098, "downbelow", "Monstrous Flame");
        Story.MapItemQuest(8098, "downbelow", 8491);

        //8099 | Follow the Trail
        Story.MapItemQuest(8099, "downbelow", 8492);
        Story.KillQuest(8099, "downbelow", "Earthwyrm");

        //8100 | Tentarachnid Horde
        Story.KillQuest(8100, "downbelow", "Tentarachnid");

        //8101 | The Shadows
        Story.KillQuest(8101, "downbelow", "Creeping Shadow");

        //8102 | Keep up the Search
        Story.MapItemQuest(8102, "downbelow", 8493);
        Story.KillQuest(8102, "downbelow", "Creeping Shadow");

        //8103 | Defeat the Guardian
        Story.KillQuest(8103, "downbelow", "Guardian Golem");

        //8104 | Wall of Rage
        Story.MapItemQuest(8104, "downbelow", 8494);
        Story.KillQuest(8104, "downbelow", "Living Rage");

        //8105 | Through the Fire
        Story.KillQuest(8105, "downbelow", "Living Rage");
        Story.MapItemQuest(8105, "downbelow", 8495);

        //8106 | Heart of the Power
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(8106, "downbelow", "Anka");

        //8107 | Undying Rage
        if (!Story.QuestProgression(8107))
        {
            Core.EnsureAccept(8107);
            Core.EquipClass(ClassType.Farm);
            Core.KillMonster("downbelow", "Enter", "Spawn", "Earthwyrm", "Anka's Followers Slain", 1000, false);
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("downbelow", "Anka", "Soul of Vengeance", 25, false);
            Core.EnsureComplete(8107);
        }
    }
}
