using System.Runtime.CompilerServices;
using RBot;

public class CoreQOM
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();
    public CoreFarms Farm = new CoreFarms();

    public void CompleteEverything()
    {

        //Preload Quests
        Story.PreLoad();

        CompleteCelestialRealmATheftofLight();
        CompleteDoomwoodPaladinsTrial();
        CompleteDarkoviaDarkDiaspora();
        CompleteShadowfallDarknessRising();
        CompleteSwordhavenTheNewWorld();
        CompleteTheDestroyer();
        CompleteTheReshaper();

    }

    public void CompleteCelestialRealmATheftofLight()
    {

        CelestialRealm();
        LostRuins();
        LostRuinsWar();
        InfernalSpire();
    }

    public void CompleteDoomwoodPaladinsTrial()
    {

        DoomPally();

    }

    public void CompleteDarkoviaDarkDiaspora()
    {

        DarkoviaInvasion();
        SafiriaInvasion();
        LycanInvasion();
        SafiriaInvasion2();

    }

    public void CompleteShadowfallDarknessRising()
    {

        ShadowfallInvasion();

    }

    public void CompleteSwordhavenTheNewWorld()
    {

        CastleInvasion();

    }

    public void CompleteTheDestroyer()
    {

        TheRift();
        CharredPath();
        Underglade();
        Extriki();

    }

    public void CompleteTheReshaper()
    {

        Pilgrimage();
        GuardianTree();
        TwistedCaverns();
        BrokenWoods();
        Kolyaban();

    }

    // ------------------------------------------------------------------------------------------------------------------------------ //
    // 1 Celestial Realm: A Theft of Light
    // ------------------------------------------------------------------------------------------------------------------------------ //

    public void CelestialRealm()
    {
        //Progress Check
        if (Core.isCompletedBefore(4499))
            return;

        //Preload Quests
        Story.PreLoad();

        //Summon Help
        Story.MapItemQuest(4495, "celestialrealm", 3698);
        Story.KillQuest(4495, "celestialrealm", new[] { "Fallen Knight", "Celestial Bird of Paradise" });

        //Power Up!
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
    }

    public void LostRuins()
    {
        //Progress Check
        if (Core.isCompletedBefore(4507))
            return;

        //Preload Quests
        Story.PreLoad();

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
    }

    public void LostRuinsWar()
    {
        //Progress Check
        if (Core.isCompletedBefore(4508))
            return;

        //Preload Quests
        Story.PreLoad();

        //Celestial Realm at War
        Story.KillQuest(4509, "lostruinswar", "Fallen Knight");

        //Defeat the Diabolical Warlord!
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(4508, "lostruinswar", "Diabolical Warlord");
    }

    public void InfernalSpire()
    {
        //Progress Check
        if (Core.isCompletedBefore(5387))
            return;

        //Preload Quests
        Story.PreLoad();

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
        Story.KillQuest(5377, "infernalspire", new[] { "Infernal Hound", "Dungeon Fiend", "Infernal Hound" });

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

    // ------------------------------------------------------------------------------------------------------------------------------ //
    // 2 Doomwood: Paladin's Trial
    // ------------------------------------------------------------------------------------------------------------------------------ //

    public void DoomPally()
    {

        //A New Recruit
        if (!Story.QuestProgression(5404))
        {
            Core.EnsureAccept(5404);
            if (!Core.CheckInventory("Blessed Coffee of the Lightguard"))
            {
                Core.AddDrop("Blessed Coffee of the Lightguard");
                Core.EnsureAccept(5405);
                Core.HuntMonster("sandsea", "Sand Monkey", "Pally Luwak Beans");
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

    // ------------------------------------------------------------------------------------------------------------------------------ //
    // 3 Darkovia: Dark Diaspora
    // ------------------------------------------------------------------------------------------------------------------------------ //

    public void DarkoviaInvasion()
    {

        //Progress Check
        if (Core.isCompletedBefore(5503))
            return;

        //Preload Quests
        Story.PreLoad();

        //Hounds and Infernals and Imp, Oh my!
        Story.KillQuest(5487, "DarkoviaInvasion", new[] { "Underworld Hound", "Infernal Imp" });

        //Like Imps in A Pod
        Story.MapItemQuest(5488, "DarkoviaInvasion", 4905, 6);

        //A Grievous Threat
        Story.KillQuest(5489, "DarkoviaInvasion", "Grievous Fiend");

        //Undead Investigation
        Story.MapItemQuest(5490, "SafiriaInvasion", 4904);
    }

    public void SafiriaInvasion()
    {

        //Progress Check
        if (Core.isCompletedBefore(5496))
            return;

        //Preload Quests
        Story.PreLoad();

        //What We Need Is A Big Can Of Raid
        Story.KillQuest(5491, "SafiriaInvasion", new[] { "Fallen Knight", "Infernal Knight" });

        //Trapped!
        Story.MapItemQuest(5492, "SafiriaInvasion", 4895, 6);
        Story.MapItemQuest(5492, "SafiriaInvasion", 4896);

        //Won't Someone Think Of The Minions?
        Story.MapItemQuest(5493, "SafiriaInvasion", 4897, 9);

        //Here, Doggy Doggy
        Story.KillQuest(5494, "SafiriaInvasion", "Blood Maggot");
        Story.MapItemQuest(5494, "SafiriaInvasion", 4898);
        Story.MapItemQuest(5494, "SafiriaInvasion", 4899);

        //Ma'alech
        Story.KillQuest(5495, "SafiriaInvasion", "Ma'alech");

        //I'm Not Lycan This Situation
        Story.MapItemQuest(5496, "LycanInvasion", 4900);
    }

    public void LycanInvasion()
    {

        //Progress Check
        if (Core.isCompletedBefore(5500))
            return;

        //Preload Quests
        Story.PreLoad();

        //The Best Way To Slay An Infernal
        Story.KillQuest(5497, "LycanInvasion", new[] { "Fallen Knight", "Infernal Knight" });

        //A Dire Situation
        Story.KillQuest(5498, "LycanInvasion", "Dire Wolf|Hulking Dire Wolf");

        //I'd Lycan To Go Now
        Story.MapItemQuest(5499, "LycanInvasion", 4901);
        Story.MapItemQuest(5499, "LycanInvasion", 4903, 6);

        //Lord Balax'el
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(5500, "LycanInvasion", "Lord Balax'el");
    }

    public void SafiriaInvasion2()
    {

        //Progress Check
        if (Core.isCompletedBefore(5503))
            return;

        //Preload Quests
        Story.PreLoad();

        //Follow Lady Solani
        Story.KillQuest(5501, "SafiriaInvasion", "Fallen Knight");
        Story.MapItemQuest(5501, "SafiriaInvasion", 4902);

        //Revenant Slayer
        Story.KillQuest(5502, "SafiriaInvasion", new[] { "Revenant", "Shadow Imp" });

        //Noddharath
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(5503, "SafiriaInvasion", "Noddharath");
    }

    // ------------------------------------------------------------------------------------------------------------------------------ //
    // 4 Shadowfall: Darkness Rising
    // ------------------------------------------------------------------------------------------------------------------------------ //

    public void ShadowfallInvasion()
    {

        //Progress Check
        if (Core.isCompletedBefore(5557))
            return;

        //Preload Quests
        Story.PreLoad();

        //Commander Tibias
        Story.MapItemQuest(5543, "ShadowfallInvasion", 5024);

        //Clear the Walls
        Story.KillQuest(5544, "ShadowfallInvasion", new[] { "Infernal Imp", "Infernal Knight" });

        //Load the Ballistas
        Story.MapItemQuest(5545, "ShadowfallInvasion", 5025, 4);
        Story.MapItemQuest(5545, "ShadowfallInvasion", 5026, 4);

        //Arm the Archers
        Story.KillQuest(5546, "ShadowfallInvasion", "Infernal Imp|Infernal Knight");

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
        Story.KillQuest(5555, "ShadowfallInvasion", "Diabolical Scryer|Nethermage");
        Story.MapItemQuest(5555, "ShadowfallInvasion", 5032);

        //Time to Fly
        Story.MapItemQuest(5556, "ShadowfallInvasion", 5033);

        //YOU again!
        Story.KillQuest(5557, "ShadowfallInvasion", "Lord Balax'el");
    }

    // ------------------------------------------------------------------------------------------------------------------------------ //
    // 5 Swordhaven: The New World
    // ------------------------------------------------------------------------------------------------------------------------------ //

    public void CastleInvasion()
    {
        //Progress Check
        if (Core.isCompletedBefore(5586))
            return;

        //Preload Quests
        Story.PreLoad();

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
            Core.KillMonster("DarkoviaInvasion", "Enter", "Spawn", "*", "Darkovia Invaders Fought", 4);
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

    // ------------------------------------------------------------------------------------------------------------------------------ //
    // 6 The Destroyer
    // ------------------------------------------------------------------------------------------------------------------------------ //

    public void TheRift()
    {
        if (Core.isCompletedBefore(5791))
            return;
        //What Happened to Baldric?
        Story.MapItemQuest(5791, "therift", 5228);
    }

    public void CharredPath()
    {
        //Progress Check
        if (Core.isCompletedBefore(5836))
            return;

        //Quests Here Bug Out So This is Needed.
        Core.AcceptandCompleteTries = 3;

        //Preload Quests
        Story.PreLoad();

        //Get a Monitor
        Story.MapItemQuest(5803, "charredpath", 5248);

        //Follow the Pattern.. He Ne Ar Kr
        Story.KillQuest(5804, "crashsite", "Dwakel Warrior");
        Story.MapItemQuest(5804, "crashsite", 5249);

        //The PTM is Ready!
        Story.KillQuest(5805, "charredpath", new[] { "Noxious Fumes", "Toxic Bile" });
        Story.MapItemQuest(5805, "charredpath", 5256);

        //Save the Creatures
        Story.KillQuest(5806, "charredpath", "Ragewing");
        Story.MapItemQuest(5806, "charredpath", 5250, 6);

        //Clear the Treeants
        Story.KillQuest(5807, "charredpath", "Toxic Treeant");
        Story.MapItemQuest(5807, "charredpath", 5251, 3);

        //Excise the Infection
        Story.KillQuest(5808, "charredpath", "Infected Hare");

        //Root out the Plague
        Story.KillQuest(5809, "charredpath", "Plague Spreader");
        Story.MapItemQuest(5809, "charredpath", 5252, 6);

        //I Said Yes, Yes, Yes
        Story.MapItemQuest(5810, "charredpath", 5255);

        //Rally the Mages
        Story.KillQuest(5811, "therift", "Mana Chest");
        Story.MapItemQuest(5811, "therift", 5253, 4);

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
    }

    public void Underglade()
    {
        //Progress Check
        if (Core.isCompletedBefore(5846))
            return;

        //Quests Here Bug Out So This is Needed.
        Core.AcceptandCompleteTries = 3;

        //Preload Quests
        Story.PreLoad();

        //Talk to Ravinos
        Story.MapItemQuest(5837, "underglade", 5271);

        //Into the Underglade
        Story.KillQuest(5838, "underglade", new[] { "Forest Spirit", "Tree Nymph" });

        //Clear the Spores
        Story.KillQuest(5839, "underglade", "Slime Spore");

        //Cleanse the Walls
        Story.KillQuest(5840, "underglade", "Forest Spirit|Tree Nymph");
        Story.MapItemQuest(5840, "underglade", 5272, 8);

        //Expose the Entrance
        Story.KillQuest(5841, "underglade", "Blackened Earth");

        //Gather the Glows
        Story.KillQuest(5842, "underglade", "Luminous Fungus");

        //More Corruption Revealed
        Story.KillQuest(5843, "underglade", "Forest Spirit|Tree Nymph");
        Story.MapItemQuest(5843, "underglade", 5273, 6);

        //The Goblin Threat
        Story.KillQuest(5844, "underglade", "Twisted Goblin");

        //Get an Offering
        Story.KillQuest(5845, "underglade", "Gemstone Elemental");

        //The Heart of the Glade
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(5846, "underglade", "Lunamoss");
    }

    public void Extriki()
    {
        //Defeat Extriki
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(5847, "extriki", "Extriki");
    }

    // ------------------------------------------------------------------------------------------------------------------------------ //
    // 7 The Reshaper
    // ------------------------------------------------------------------------------------------------------------------------------ //

    public void Pilgrimage()
    {
        //Progress Check
        if (Core.isCompletedBefore(5855))
            return;

        //Preload Quests
        Story.PreLoad();

        //Capture the Misshapen
        Story.KillQuest(5849, "Pilgrimage", new[] { "SpiderWing", "Urstrix" });

        //Defeat the Parasites
        Story.KillQuest(5850, "Pilgrimage", "Ravenous Parasite");

        //The Source
        Story.KillQuest(5851, "Pilgrimage", "Extrikiti");

        //Find Lucky!
        Story.KillQuest(5852, "Pilgrimage", new[] { "Extrikiti", "Extrikiti" });
        Story.MapItemQuest(5852, "Pilgrimage", 5288, 3);
        Story.MapItemQuest(5852, "Pilgrimage", 5289);

        //Gather a Scouting Party
        Story.MapItemQuest(5853, "Pilgrimage", 5290, 6);
        Story.MapItemQuest(5853, "Pilgrimage", 5291);

        //Defeat the Parasites
        Story.KillQuest(5854, "Pilgrimage", "Ravenous Parasite");
        Story.MapItemQuest(5854, "Pilgrimage", 5292);

        //Bad Dog
        Story.KillQuest(5855, "Pilgrimage", "Lucky");
    }

    public void GuardianTree()
    {
        //Progress Check
        if (Core.isCompletedBefore(6286))
            return;

        //Preload Quests
        Story.PreLoad();

        //Connect to the Earth
        Story.KillQuest(6276, "guardiantree", "Blossoming Treeant");
        Story.MapItemQuest(6276, "guardiantree", 5769, 5);

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
        Story.KillQuest(6282, "guardiantree", "Pollen Cloud");
        Story.MapItemQuest(6282, "guardiantree", 5773);

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
        Story.KillQuest(6285, "guardiantree", "Myconid");
        Story.MapItemQuest(6285, "guardiantree", 5775, 2);

        //Take Down Terrane
        Story.KillQuest(6286, "guardiantree", "Terrane");
    }

    public void TwistedCaverns()
    {
        //Progress Check
        if (Core.isCompletedBefore(5864))
            return;

        //Preload Quests
        Story.PreLoad();

        //Explore the Cavern
        Story.KillQuest(5856, "TwistedCavern", "Extrikiti");
        Story.MapItemQuest(5856, "TwistedCavern", 5293);

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
    }

    public void BrokenWoods()
    {
        //Progress Check
        if (Core.isCompletedBefore(5873))
            return;

        //Preload Quests
        Story.PreLoad();

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
        Story.KillQuest(5868, "BrokenWoods", "Fungal Lord");
        Story.MapItemQuest(5868, "BrokenWoods", 5296, 4);
        Story.MapItemQuest(5868, "BrokenWoods", 5297, 4);

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
            if (!Core.CheckInventory("Health Potion"))
                Core.BuyItem("Embersea", 1100, "Health Potion");

            Core.HuntMonster("Arcangrove", "Seed Spitter", "Uncorrupted Spitter Seeds", 3);
            Core.HuntMonster("poisonforest", "Treeant", "Treeant Berries", 4);
            Core.EnsureComplete(5872);
        }

        //Smash the Hive
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(5873, "BrokenWoods", "Hive");
    }

    public void Kolyaban()
    {
        //Progress Check
        if (Core.isCompletedBefore(5877))
            return;

        //Preload Quests
        Story.PreLoad();

        //Acolyte's Medallions
        if (!Story.QuestProgression(5874))
        {
            Core.EnsureAccept(5874);
            Core.HuntMonster("Kolyaban", "Poisonous Darkblood", "Acolyte's Medallion", 4);
            Core.EnsureComplete(5874);
        }

        //Let's Hope This Works
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(5876, "Kolyaban", "Twisted Aria");

        //Defeat Kolyaban
        Story.KillQuest(5877, "Kolyaban", "Kolyaban");
    }
}