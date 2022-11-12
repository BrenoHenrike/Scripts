//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class CoreSoW
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.RunCore();
    }

    public void CompleteCoreSoW()
    {
        ShadowWar();
        ShadowlordKeep();
        Timestream();
        GraniteCove();
        BlackseaKeep();
        Junkhoard();
        Junkheap();
        Shadowgrove();
        AozoraHills();
        GhostNexus();
        Shadowsong();
        DarkAlly();
        DarkAlliance();
        InnerShadows();
        Tyndarius();
        RuinedCrown();
        Timekeep();
        TimestreamWar();
        DeadLines();
        ShadowFlame();
    }

    public void ShadowWar()
    {
        if (Core.isCompletedBefore(6852))
            return;

        Story.PreLoad(this);

        //Shadow Medals: Defend the Village! 6846
        Story.KillQuest(6846, "shadowwar", "Shadowflame Slasher");

        //Seed Spitter Oil 6847
        Story.KillQuest(6847, "shadowwar", "Seed Spitter");

        //Mega Shadow Medals 6848
        Story.KillQuest(6848, "shadowwar", "Shadowflame Slasher");

        //Shadow Samples 6849
        Story.KillQuest(6849, "shadowwar", "Umbral Goo");

        //Shadow Medals: Fight them Back! 6850
        Story.KillQuest(6850, "shadowwar", "Shadowflame Scout");

        //Interrogation 6851 //had multiple reports made it a questprog.
        if (!Story.QuestProgression(6851))
        {
            Core.EnsureAccept(6851);
            Core.HuntMonster("shadowwar", "Shadowflame Scout", "Useful Clue", 3);
            Core.EnsureComplete(6851);
            Bot.Wait.ForQuestComplete(6851); //insurance... because y[e]
        }

        //Defeat Malgor! 6852
        Story.KillQuest(6852, "malgor", "Malgor");
    }

    public void ShadowlordKeep()
    {
        if (Core.isCompletedBefore(6864))
            return;

        Story.PreLoad(this);

        //Defeat the Minions (6856)
        Story.MapItemQuest(6856, "shadowlordkeep", 6390);
        Story.KillQuest(6856, "shadowlordkeep", "Shadow Gunner|Shadow Mage");

        //Gather the Gunpowder (6857)
        Story.KillQuest(6857, "shadowlordkeep", "Shadow Gunner");

        //Build a Bomb (6858)
        Story.KillQuest(6858, "shadowlordkeep", new[] { "Shadow Mage", "Shadow Gunner|Shadow Mage" });

        //Light 'em Up! (6859)
        Story.MapItemQuest(6859, "shadowlordkeep", 6391, 12);

        //Fight the Shadows (6860)
        Story.KillQuest(6860, "shadowlordkeep", "Shadow Imp|Shadow Locust");

        //Magical Potion (6861)
        Story.KillQuest(6861, "shadowlordkeep", "Shadow Mage");

        //Overload the Energy Field (6862)
        Story.MapItemQuest(6862, "shadowlordkeep", 6392);
        Story.KillQuest(6862, "shadowlordkeep", "Stray Energy");

        //Defeat the Portal Guards (6863)
        Story.KillQuest(6863, "shadowlordkeep", "Portal Guard");

        //Go Through the Portal (6864)
        Story.MapItemQuest(6864, "timestream", 6393);
    }

    public void Timestream()
    {
        if (Core.isCompletedBefore(6867))
            return;

        ShadowlordKeep();

        Story.PreLoad(this);

        //Fuel for the Cell (6865)
        Story.KillQuest(6865, "timestream", "Spacetime Energy");

        //Open the Rift (6866)
        Story.MapItemQuest(6866, "timestream", 6394);

        //Defeat the ShadowKnight (6867)
        Story.KillQuest(6867, "timestream", "ShadowKnight Gar");
    }

    public void GraniteCove()
    {
        if (Core.isCompletedBefore(6883))
            return;

        Story.PreLoad(this);

        //Get A Disguise (6873)
        Story.KillQuest(6873, "granitecove", "Blacksea Pirate");

        //Gear Up! (6874)
        Story.MapItemQuest(6874, "granitecove", 6433);

        //Gather Information (6875)
        Story.MapItemQuest(6875, "granitecove", new[] { 6434, 6435, 6436 });

        //Find the Missing Pages (6876)
        Story.KillQuest(6876, "granitecove", "Blacksea Pirate|Jungle Treeant|Island Monkey");

        //Collect the Beans (6877)
        Story.KillQuest(6877, "granitecove", "Island Monkey");

        //Smooth the Way (6878)
        Story.KillQuest(6878, "granitecove", "Jungle Treeant");

        //Sweeten the Pot (6879)
        Story.KillQuest(6879, "granitecove", "Blacksea Pirate");

        //Wake the Statues (6880)
        Story.MapItemQuest(6880, "granitecove", 6438, 10);

        //Clear the Tattersail Pirates (6881)
        Story.KillQuest(6881, "granitecove", "Tattersail Pirate");

        //Kill Bill (6882)
        Story.KillQuest(6882, "granitecove", "Blacktooth Bill");

        //Defeat Teja! (6883)
        Story.KillQuest(6883, "granitecove", "Pirate Queen Teja");
    }

    public void BlackseaKeep()
    {
        if (Core.isCompletedBefore(6896))
            return;

        Story.PreLoad(this);

        //Get the Keys (6886)
        Story.KillQuest(6886, "blackseakeep", "Blacksea Scallywag");

        //Open the Chests (6887)
        Story.MapItemQuest(6887, "blackseakeep", 6446, 6);
        Story.KillQuest(6887, "blackseakeep", "Pure Darkness");

        //Interrogation Time (6888)
        Story.KillQuest(6888, "blackseakeep", "Blacksea Privateer");

        //Get the Picks (6889)
        Story.KillQuest(6889, "blackseakeep", "Blacksea Scallywag");

        //Break In! (6890)
        Story.MapItemQuest(6890, "blackseakeep", 6448);

        //Kick that Booty! (6891)
        Story.KillQuest(6891, "blackseakeep", "Blacksea Pirate Mage|Blacksea Privateer|Blacksea Scallywag");

        //We Need a Spell (6892)
        Story.KillQuest(6892, "blackseakeep", new[] { "Blacksea Pirate Mage", "Blacksea Pirate Mage" });

        //Light up the Shadows (6893)
        Story.MapItemQuest(6893, "blackseakeep", 6449, 7);

        //Get the Pitch (6894)
        Story.KillQuest(6894, "blackseakeep", "Blacksea Privateer");

        //Light It Up (6895)
        Story.MapItemQuest(6895, "blackseakeep", 6450, 8);

        //Get My Eye! (6896)
        Story.KillQuest(6896, "blackseakeep", "First Mate Bloodbone");
    }

    public void Junkhoard()
    {
        if (Core.isCompletedBefore(6960))
            return;

        Story.PreLoad(this);

        //"Salute" the Magpie (6949)
        Story.KillQuest(6949, "junkhoard", "Magpie");

        //Gather the Parts (6950)
        Story.KillQuest(6950, "junkhoard", "Junk Golem");

        //Engine Needs a Spark (6951)
        Story.KillQuest(6951, "junkhoard", "Portal Manifestation");

        //Metal for Welding (6952)
        Story.KillQuest(6952, "junkhoard", "Junk Golem");

        //Feel the Heat (6953)
        Story.KillQuest(6953, "junkhoard", "Portal Manifestation");

        //Find the Forge (6954)
        Story.MapItemQuest(6954, "junkhoard", 6497);

        //Get the Coal (6955)
        Story.KillQuest(6955, "junkhoard", "Magpie");

        //Defeat the Coal Elemental (6956)
        Story.KillQuest(6956, "junkhoard", "Coal Elemental");

        //Filthy Thieving Magpies (6957)
        Story.KillQuest(6957, "junkhoard", "Magpie");

        //Track the Tracker (6958)
        Story.KillQuest(6958, "junkhoard", "Junk Golem");

        //Follow the Chakram (6959)
        Story.MapItemQuest(6959, "junkhoard", 6498);

        //Get it BACK! (6960)
        Story.KillQuest(6960, "junkhoard", "Magpie Junk Heap");
    }

    public void Junkheap()
    {
        if (Core.isCompletedBefore(6974))
            return;

        Junkhoard();

        Story.PreLoad(this);

        //Whispering Walls (6961)
        Story.MapItemQuest(6961, "junkheap", 6500, 5);

        //So Many Magpies (6962)
        Story.KillQuest(6962, "junkheap", "Magpie");

        //Charge the Portals (6963)
        Story.MapItemQuest(6963, "junkheap", 6501);
        Story.KillQuest(6963, "junkheap", "Tiny Manifestation");

        //Through the Rift (6964)
        Story.MapItemQuest(6964, "junkheap", 6502);

        //Free Niknak (6965)
        Story.MapItemQuest(6965, "junkheap", 6503);
        Story.KillQuest(6965, "junkheap", "Shadowflame Scout");

        //Afraid of Shadows (6966)
        Story.KillQuest(6966, "junkheap", "Shadow Imp|Shadowflame Scout");

        //Wood for Splints (6967)
        Story.KillQuest(6967, "junkheap", "Dark Treeant");

        //First Aid (6968)
        Story.KillQuest(6968, "junkheap", new[] { "Shadow Imp", "Shadow Imp" });

        //Delivery Time (6969)
        Story.MapItemQuest(6969, "junkheap", 6504);

        //Need Food (6970)
        Story.KillQuest(6970, "junkheap", new[] { "Dark Treeant", "Shadowflame Scout" });

        //A New Coat (6971)
        Story.KillQuest(6971, "junkheap", "Shadow Imp");

        //Bruise the Bruiser (6972)
        Story.KillQuest(6972, "junkheap", "Shadowflame Bruiser");

        //Shadowy Yulgar (6973)
        Story.KillQuest(6973, "junkheap", "Shadowflame Yulgar");

        //Take the Portal (6974)
        Story.MapItemQuest(6974, "junkhoard", 6505);
    }

    public void Shadowgrove()
    {
        if (Core.isCompletedBefore(6999))
            return;

        Story.PreLoad(this);

        //Dragonlord Medals: Defend Arcangrove! (6993)
        Story.KillQuest(6993, "shadowgrove", "Shadow Dragonlord");

        //Dragonscale Powder (6994)
        Story.KillQuest(6994, "shadowgrove", "Shadow Wyvern|ShadowFlame Dragon");

        //Mega Dragonlord Medals (6995)
        Story.KillQuest(6995, "shadowgrove", "Shadow Dragonlord");

        //Dragon Oil (6996)
        Story.KillQuest(6996, "shadowgrove", "Shadow Wyvern|ShadowFlame Dragon");

        //Head of a Dragon (6997)
        Story.KillQuest(6997, "shadowgrove", "Mutant Shadow Dragon");

        //Mutant Dragon Oil (6998) - buggy mob
        if (!Story.QuestProgression(6998))
        {
            Core.EnsureAccept(6998);
            Core.KillMonster("shadowgrove", "r9", "Left", "Mutant Shadow Dragon", "Mutant Dragon Oil", 3);
            Core.EnsureComplete(6998);
        }

        //Titan Shadow Dragonlord (6999)
        Story.KillQuest(6999, "shadowgrove", "Titan Shadow Dragonlord");
    }

    public void AozoraHills()
    {
        if (Core.isCompletedBefore(7062))
            return;

        Story.PreLoad(this);

        //Gather Offerings (7050)
        Story.MapItemQuest(7050, "aozorahills", 6643, 5);
        Story.MapItemQuest(7050, "aozorahills", 6644, 5);
        Story.KillQuest(7050, "aozorahills", "Reishi");

        //Deliver the Gift (7051)
        Story.MapItemQuest(7051, "aozorahills", 6645);

        //Pest Control (7052)
        Story.KillQuest(7052, "aozorahills", "Aozora Kijimuna");

        //Prove Your Worth (7053)
        Story.KillQuest(7053, "aozorahills", "Osanshouo");

        //Light for the Lantern (7054)
        Story.KillQuest(7054, "aozorahills", "Kosenjobi");

        //Attune to Fuyurei (7055)
        Story.MapItemQuest(7055, "aozorahills", 6646);
        Story.KillQuest(7055, "aozorahills", "Fuyurei");

        //Follow the Path (7056)
        Story.MapItemQuest(7056, "aozorahills", 6647, 3);

        //Attune to Kijimuna (7057)
        Story.MapItemQuest(7057, "aozorahills", 6648);
        Story.KillQuest(7057, "aozorahills", "Aozora Kijimuna");

        //Path of the Kijimuna (7058)
        Story.MapItemQuest(7058, "aozorahills", 6649, 3);

        //Attune to Reishi (7059)
        Story.MapItemQuest(7059, "aozorahills", 6650);
        Story.KillQuest(7059, "aozorahills", "Reishi");

        //Path to Reishi (7060)
        Story.MapItemQuest(7060, "aozorahills", 6651, 3);

        //Black Flames (7061)
        Story.KillQuest(7061, "aozorahills", "Kosenjobi");

        //Defeat Grandmother Hasu (7062)
        Story.KillQuest(7062, "aozorahills", "Ghostly Hasu");

    }

    public void GhostNexus()
    {
        if (Core.isCompletedBefore(7118))
            return;

        AozoraHills();

        Story.PreLoad(this);

        //What's That I Hear? (7106)
        Story.MapItemQuest(7106, "ghostnexus", 6700);

        //Goat Gone Wild (7107)
        Story.KillQuest(7107, "ghostnexus", "Chaos Goat|Chaos Wolf");

        //Creepy Eyes (7108)
        Story.KillQuest(7108, "ghostnexus", "Chaos Sp-eye");

        //Find My Parents (7109)
        Story.MapItemQuest(7109, "ghostnexus", 6701);

        //More Food (7110)
        Story.KillQuest(7110, "ghostnexus", new[] { "Chaos Goat", "Chaos Wolf" });

        //Clear the Way (7111)
        Story.KillQuest(7111, "ghostnexus", "Gnarltooth");

        //Drive Them Back (7112)
        Story.KillQuest(7112, "ghostnexus", "Infernal Knight");

        //Calm the Yokai (7113)
        Story.KillQuest(7113, "ghostnexus", "Abumi Guchi|Tsukumogami");

        //Get the Supplies (7114)
        Story.KillQuest(7114, "ghostnexus", new[] { "Abumi Guchi", "Infernal Knight" });

        //Defeat the Nether Warlock (7115)
        Story.KillQuest(7115, "ghostnexus", "Nether Warlock");

        //So... Sad... (7116)
        Story.KillQuest(7116, "ghostnexus", "Manifestation of Grief");

        //Release the Rage (7117)
        Story.KillQuest(7117, "ghostnexus", "Manifestation of Rage");

        //Anguish is the Worst (7118)
        Story.KillQuest(7118, "ghostnexus", "Mahou's Anguish");
    }

    public void Shadowsong()
    {
        if (Core.isCompletedBefore(7348))
            return;

        Story.PreLoad(this);

        //Shadow Medal (7340)
        if (!Bot.Quests.IsUnlocked(7341))
        {
            Core.EnsureAccept(7340);
            Core.HuntMonster("shadowsong", "Shadowflame Troll", "Shadow Medal", 6);
            Core.EnsureComplete(7340);
        }

        //Song of Fate (7341)
        Story.KillQuest(7341, "shadowsong", "Tune-A-Fish");

        //Broken Strings (7342)
        Story.KillQuest(7342, "shadowsong", "Beatle");

        //Shattered Crystal (7343)
        Story.KillQuest(7343, "shadowsong", "Shattered Crystal");

        //Mega Shadow Medals (7345)
        if (!Bot.Quests.IsUnlocked(7345))
        {
            Core.EnsureAccept(7344);
            Core.HuntMonster("shadowsong", "Shadowflame Ur-Troll", "Mega Shadow Medal", 3);
            Core.EnsureComplete(7344);
        }

        //New Music (7345)
        Story.KillQuest(7345, "shadowsong", "Mozard");

        //Listen to the Drums (7346)
        Story.KillQuest(7346, "shadowsong", "Pachelbel's Cannon");

        //Strength in Music (7347)
        Story.KillQuest(7347, "shadowsong", "Mozard");

        //Stop the Music (7348)
        Story.KillQuest(7348, "shadowsong", "Oh'Garr");
    }

    public void DarkAlly()
    {
        if (Core.isCompletedBefore(7428))
            return;

        Story.PreLoad(this);

        if (!Story.QuestProgression(7419))
        {
            Story.MapItemQuest(7419, "darkally", 7179, 6);
            Core.KillMonster("darkally", "r2", "Left", "Shadow", "Shadow Destroyed", 10);
            Core.EnsureComplete(7419);
        }

        Story.KillQuest(7420, "darkally", new[] { "Underworld Golem", "Underworld Golem" });

        Story.MapItemQuest(7421, "darkally", 7180, 1);
        Story.MapItemQuest(7421, "darkally", 7181, 8);

        if (!Story.QuestProgression(7422))
        {
            Core.EnsureAccept(7422);
            Core.Join("Darkally", "r2", "Left");
            while (!Bot.ShouldExit && !Core.CheckInventory(53855, 10))
                Bot.Kill.Monster("Dark Makai");
            Core.EnsureComplete(7422);
        }

        if (!Story.QuestProgression(7423))
        {
            Core.EnsureAccept(7423);
            Core.KillMonster("Darkally", "r2", "Left", 4452, "Shredded Shadow", 9);
            Core.EnsureComplete(7423);
        }

        Story.KillQuest(7424, "darkally", new[] { "Legion Defector", "Legion Defector" });

        Story.KillQuest(7425, "darkally", "Frozen Pyromancer");

        Story.KillQuest(7426, "darkally", "Underworld Golem");

        Story.MapItemQuest(7427, "darkally", 7182, 1);

        Story.KillQuest(7428, "darkally", "Underfiend");
    }

    public void DarkAlliance()
    {
        if (Core.isCompletedBefore(7460))
            return;

        DarkAlly();
        Story.PreLoad(this);

        //Clear the Shadows --DAGE--
        Story.MapItemQuest(7446, "darkalliance", 7224, 8);
        Story.KillQuest(7446, "darkalliance", "Shadow");
        //Destroy the Swords
        Story.MapItemQuest(7447, "darkalliance", 7225, 1);
        Story.KillQuest(7447, "darkalliance", "Shadowblade");
        //Fuel for the Forge
        Story.KillQuest(7448, "darkalliance", "Shadow Void");
        //Sever the Connection
        Story.KillQuest(7449, "darkalliance", "Shadow Makai");
        //Armor Against the Shadow
        Story.KillQuest(7450, "darkalliance", "Shadow Void");
        //Aid from the Pyromancer
        Story.MapItemQuest(7451, "darkalliance", 7226, 8);
        //Find the Underflame --Frozen Pyromancer--
        Story.MapItemQuest(7452, "darkalliance", 7227, 1);
        //Gather some Underlava
        Story.KillQuest(7453, "darkalliance", "Underlava");
        //Gather Sulfur
        Story.KillQuest(7454, "darkalliance", "Underworld Imp");
        //Coal for Flame
        Story.MapItemQuest(7455, "darkalliance", 7228, 6);
        //Defeat the Underflame Guardian
        Story.KillQuest(7456, "darkalliance", "Underflame Guardian");
        //Take the Flame
        Story.MapItemQuest(7457, "darkalliance", 7229, 1);
        //Remove the Shadows --dage--
        Story.KillQuest(7458, "darkalliance", "Shadow");
        //Burn the Shadows
        Story.MapItemQuest(7459, "darkalliance", 7230, 1);
        //Defeat Shadow Nulgath
        Story.KillQuest(7460, "darkalliance", "ShadowFlame Nulgath");
    }

    public void InnerShadows()
    {
        if (Core.isCompletedBefore(7472))
            return;

        DarkAlliance();
        Story.PreLoad(this);

        //Gather the Shadows (7461)
        Story.KillQuest(7461, "innershadows", "Infected Minion|Shadowcrow");

        //Dusty Boost (7462)
        Story.KillQuest(7462, "innershadows", "Infected Minion");
        Story.MapItemQuest(7462, "innershadows", 7271);

        //Defeat my Minions (7463)
        Story.KillQuest(7463, "innershadows", "Infected Minion");

        //Pass the Portal (7464)
        Story.MapItemQuest(7464, "innershadows", 7272);

        //We Need to See (7465)
        Story.MapItemQuest(7465, "innershadows", 7273, 8);

        //Stop the Crows (7466)
        Story.KillQuest(7466, "innershadows", "Shadowcrow");

        //Convert the Shadowscythe (7467)
        Story.KillQuest(7467, "innershadows", "Shadow ShadowScythe");

        //Feed the Doomblade (7468)
        Story.KillQuest(7468, "innershadows", "Infected Minion");

        //Speak to the Ghosts (7469)
        Story.MapItemQuest(7469, "innershadows", 7274, 5);

        //Gather Spell Reagents (7470)
        Story.KillQuest(7470, "innershadows", new[] { "ShadowSpitter", "Shadowcrow" });

        //Follow the Trail (7471)
        Story.MapItemQuest(7471, "innershadows", 7275, 5);

        //Defeat Krahen (7472)
        Story.KillQuest(7472, "innershadows", "Krahen");
    }

    public void Tyndarius(bool WarTrainingMerge = false)
    {
        if (Core.isCompletedBefore(8243))
            return;

        InnerShadows();

        Adv.BestGear(GearBoost.Human);

        //War Medals
        Story.KillQuest(8125, "fireplanewar", "Shadowflame Soldier");

        // Mega War Medals
        Story.KillQuest(8126, "fireplanewar", "Shadowflame Soldier");

        // Shadowflame Un - slaught
        Story.KillQuest(8127, "fireplanewar", "Shadefire Onslaught");

        // Soldier On
        Story.KillQuest(8128, "fireplanewar", "Shadowflame Soldier");

        // Fanning the Flames
        Story.MapItemQuest(8129, "fireplanewar", 8523, 5);
        Story.KillQuest(8129, "fireplanewar", "Shadowflame Soldier");

        // Trailblazer
        Story.KillQuest(8130, "fireplanewar", "Shadefire Onslaught");

        // ShadowClaw
        Story.KillQuest(8131, "fireplanewar", "ShadowClaw");

        // In Their Element
        Story.KillQuest(8132, "fireplanewar", "Shadefire Elemental");

        // Cure the Fire
        Story.MapItemQuest(8133, "fireplanewar", 8524, 5);
        Story.KillQuest(8133, "fireplanewar", "Living Shadowflame");

        // Human Torch
        Story.KillQuest(8134, "fireplanewar", "Living Shadowflame");

        // ShadowFlame Phedra
        Story.KillQuest(8135, "fireplanewar", "ShadowFlame Phedra");

        // Gather Fuel
        Story.KillQuest(8136, "shadowfireplane", "Living Shadowflame");

        // Attune to Play
        Story.MapItemQuest(8137, "shadowfireplane", 8544, 5);

        // Sparks Will Fly
        Story.KillQuest(8138, "shadowfireplane", "Onslaught Knight");

        // Awaken Lady Fiamme
        Story.MapItemQuest(8139, "shadowfireplane", 8542);
        Bot.Sleep(5000);

        // Destroy the Barrier
        if (!Story.QuestProgression(8140))
        {
            Core.Join("shadowfireplane", "r6", "Left"); // for incase u start here
            Core.EnsureAccept(8140);
            Core.GetMapItem(8543);
            Core.KillMonster("shadowfireplane", "r6", "Left", "Shadow Wing", "Shadow Flamewing Defeated", 2);
            Core.KillMonster("shadowfireplane", "r6", "Left", "Shadowfire Summoner", "Shadowfire Summoner Defeated", 1);
            Core.EnsureComplete(8140);
        }

        // Blaze a Path
        Story.KillQuest(8141, "shadowfireplane", new[] { "Onslaught Knight", "Shadowfire Corporal" });

        // Into the Tiger's Den
        Story.KillQuest(8142, "shadowfireplane", "Shadowfire Tiger");

        // One Final Push
        Story.KillQuest(8143, "shadowfireplane", "Shadowfire Corporal");

        // Defeat Elius
        Story.KillQuest(8144, "shadowfireplane", "Elius");

        // Elementary Defense
        Story.KillQuest(8179, "fireinvasion", "Onslaught Knight");

        // Tiger Burning Bright
        Story.MapItemQuest(8180, "fireinvasion", 8728, 3);
        Story.KillQuest(8180, "fireinvasion", "Shadowfire Tiger");

        // Crush the Cavalry
        Story.KillQuest(8181, "fireinvasion", "Shadefire Cavalry");

        // Capture the Corporals
        Story.KillQuest(8182, "fireinvasion", "Shadowfire Corporal");

        // Light up the Night
        Story.MapItemQuest(8183, "fireinvasion", 8729, 6);
        Story.KillQuest(8183, "fireinvasion", "Shadefire Elemental");

        // Major Malfunction
        Story.KillQuest(8184, "fireinvasion", "Shadefire Major");

        // Darkness in Swordhaven
        Story.KillQuest(8185, "fireinvasion", new[] { "Shadefire Elemental", "Shadowfire Tiger" });

        // Extinguish the Flames
        Story.KillQuest(8186, "fireinvasion", "Living Shadowflame");

        // ShadeFire Colonel Clash
        Story.KillQuest(8187, "fireinvasion", "Shadefire Colonel");

        // Defense of Embersea
        Story.KillQuest(8188, "fireinvasion", "Living Shadowflame");

        // A Chilling Conflict
        Story.KillQuest(8189, "fireinvasion", "Onslaught Knight");

        // General Dismay
        Story.KillQuest(8190, "fireinvasion", "Shadefire General");

        //A Fallen Friend
        Story.KillQuest(8191, "fireinvasion", "Shadowflame Kyron");

        // Fire Fighting
        Story.KillQuest(8192, "fireinvasion", new[] { "Living Shadowflame", "Shadefire Cavalry" });

        // Shadefires of War
        Story.KillQuest(8193, "wartraining", "Simulated Shadefire");

        // Extinguish the Shadowflame
        Story.MapItemQuest(8194, "wartraining", 8746, 4);

        // A Major Pain in the Neck
        Story.KillQuest(8195, "wartraining", "Simulated Major");

        // The Envoy of Fire
        Story.KillQuest(8196, "wartraining", "Simulated Elius");

        // Putting Out Small Fires
        Story.KillQuest(8197, "wartraining", "Simulated Fire");

        // Element - ary
        Story.KillQuest(8198, "wartraining", "Simulated Elemental");

        // A Dragonslayer's Past
        Story.KillQuest(8199, "wartraining", "Simulated Fire");

        // The Champion of Fire
        Story.KillQuest(8200, "wartraining", "Fire Champion");

        // March of the Warfury
        Story.KillQuest(8201, "wartraining", "Warfury Soldier");

        // Elite It to Beat It
        Story.KillQuest(8202, "wartraining", "Warfury Elite");

        //The Goddess of War
        Story.KillQuest(8203, "wartraining", "Varga");
        if (WarTrainingMerge)
            return;

        // Warfury Training
        Story.KillQuest(8204, "wartraining", "Warfury Soldier");

        //Colonel Vanguard 8233
        Story.KillQuest(8233, "fireavatar", "Shadefire Colonel");

        //In a Major Way 8234
        Story.KillQuest(8234, "fireavatar", "Shadefire Major");

        //Elemental Backup 8235
        Story.MapItemQuest(8235, "fireavatar", 8859, 4);

        //The Cavalry's Here 8236
        Story.KillQuest(8236, "fireavatar", "Shadefire Cavalry");

        //Path of Flame 8237
        Story.KillQuest(8237, "fireavatar", "Shadefire Colonel");

        //The Envoy of Fire 8238
        Story.KillQuest(8238, "fireavatar", "Elius");

        //Living in Shadowflame 8239
        Story.KillQuest(8239, "fireavatar", "Living Shadowflame");

        //Elemental of Surprise 8240
        Story.KillQuest(8240, "fireavatar", "Shadefire Elemental");

        //Well That’s New 8241
        Story.KillQuest(8241, "fireavatar", "Shadow Lava");

        //Thermal Energy 8242
        Story.KillQuest(8242, "fireavatar", "Shadow Lava");

        //Avatar of Fire 8243
        Story.KillQuest(8243, "fireavatar", new[] { "Avatar Tyndarius", "Fire Orb" });
    }

    public void RuinedCrown()
    {
        if (Core.isCompletedBefore(8787))
            return;

        // 8778 Mental Damage Sponge
        Story.MapItemQuest(8778, "ruinedcrown", new[] { 10380, 10382, 10383 });

        // 8779 Scraping the Barrel
        Story.KillQuest(8779, "ruinedcrown", new[] { "Mana-Burdened Minion", "Mana-Burdened Knight" });

        // 8780 Fractals
        Story.MapItemQuest(8780, "ruinedcrown", 10384, 6);

        // 8781 Blind Retaliation
        Story.KillQuest(8781, "ruinedcrown", "Mana-Burdened Mage");

        // 8782 Deafening Silence
        Story.KillQuest(8782, "ruinedcrown", new[] { "Mana-Burdened Minion", "Mana-Burdened Knight" });

        // 8784 Stilled Mind (Yes 8784 before 8783)
        Story.MapItemQuest(8784, "ruinedcrown", 10385, 6);

        // 8783 Volatile Nature
        Story.KillQuest(8783, "ruinedcrown", "Frenzied Mana");

        // 8785 Heartache
        Story.KillQuest(8785, "ruinedcrown", "Mana-Burdened Mage");

        // 8786 Clouded Vision
        Story.MapItemQuest(8786, "ruinedcrown", 10386);
        Story.KillQuest(8786, "ruinedcrown", "Frenzied Mana");

        // 8787 Guilt Complex
        Story.KillQuest(8787, "ruinedcrown", "Calamitous Warlic");

        // 8788 Em-pathetic Connection (Merge Shop Quest)
        Core.EnsureAccept(8788);
        Core.HuntMonster("ruinedcrown", "Frenzied Mana", "Mana Residue", 8);
        Core.HuntMonster($"ruinedcrown", "Mana-Burdened Mage", "Mage’s Blood Sample", 8);
        Core.HuntMonster($"ruinedcrown", "Calamitous Warlic", "Warlic’s Favor");
        Core.EnsureComplete(8788);
    }

    public void Timekeep()
    {
        if (Core.isCompletedBefore(8813))
            return;

        RuinedCrown();

        // 8803|Mood Pendulum
        Story.MapItemQuest(8803, "Timekeep", new[] { 10455, 10456, 10457 });

        // 8804|Bug Issue
        Story.KillQuest(8804, "Timekeep", "Decaying Locust");

        // 8805|Advanced Age
        Story.KillQuest(8805, "Timekeep", "Distorted Imp");

        // 8806|One Singularity
        Story.MapItemQuest(8806, "Timekeep", 10458, 6);

        // 8807|Canaries on Edge
        Story.KillQuest(8807, "Timekeep", "Mana-Burdened Mage");

        // 8808|Caution! Wet Floor
        Story.MapItemQuest(8808, "Timekeep", new[] { 10459, 10460 });

        // 8809|Reflection in the Puddle
        Story.KillQuest(8809, "Timekeep", "Mumbler");

        // 8810|Meniscus Point
        Story.KillQuest(8810, "Timekeep", new[] { "Distorted Imp", "Mana-Burdened Mage" });

        // 8811|Distractions
        Story.KillQuest(8811, "Timekeep", new[] { "Mumbler", "Decaying Locust" });

        // 8812|Dog Bites Back
        Story.KillQuest(8812, "Timekeep", "Mal-formed Gar");

        // 8813|Janitorial Duties
        Story.KillQuest(8813, "Timekeep", new[] { "Mal-formed Gar", "Mumbler", "Decaying Locust" });
    }

    public void TimestreamWar()
    {
        if (Core.isCompletedBefore(8819))
            return;

        Timekeep();

        // 8814|Timestream Medals (dont need to do the mega metals)
        if (!Bot.Quests.IsUnlocked(8816))
        {
            Core.EnsureAccept(8814);
            Core.HuntMonster("Streamwar", "Decaying Locust", "Timestream Medal", 5);
            Core.EnsureComplete(8814);
        }

        // 8816|Teary Components
        Story.KillQuest(8816, "Streamwar", "Mumbler");

        // 8817|Proportional Retaliation         
        Story.KillQuest(8817, "Streamwar", "Decaying Locust");

        //Growing Pains (8818)
        Story.KillQuest(8818, "streamwar", "False Wyvern");

        //Middle Child (8819)
        Story.KillQuest(8819, "streamwar", "Second Speaker");
    }

    public void DeadLines()
    {
        if (Core.isCompletedBefore(8868))
            return;

        TimestreamWar();

        Story.PreLoad(this);

        //Falling Apart 8859
        Story.MapItemQuest(8859, "DeadLines", 10601, 6);

        //Baby Steps 8860
        Story.MapItemQuest(8860, "DeadLines", 10602);
        Story.KillQuest(8860, "DeadLines", "Frenzied Mana");

        //Eternal Flame 8861
        Story.MapItemQuest(8861, "DeadLines", new[] { 10603, 10604 });
        Story.KillQuest(8861, "DeadLines", "Shadowfall Warrior");

        //The Wolf Cries 8862
        Story.MapItemQuest(8862, "DeadLines", new[] { 10605, 10606 });
        Story.KillQuest(8862, "DeadLines", "Swordhaven Knight");

        //Breaking Down 8863
        Story.MapItemQuest(8863, "DeadLines", new[] { 10607, 10608 });
        Story.KillQuest(8863, "DeadLines", "Shadowfall Warrior");

        //Growth Through Hardship 8864
        Story.MapItemQuest(8864, "DeadLines", new[] { 10609, 10610 });
        Story.KillQuest(8864, "DeadLines", "Swordhaven Knight");

        //Arteries 8865
        Story.MapItemQuest(8865, "DeadLines", new[] { 10611, 10612 });
        Story.KillQuest(8865, "DeadLines", "Frenzied Mana");

        //Paranoiac 8866
        Story.MapItemQuest(8866, "DeadLines", 10613);
        Story.KillQuest(8866, "DeadLines", "Chaos Mage");

        //A Hard Swerve 8867
        Story.MapItemQuest(8867, "DeadLines", 10614);
        Story.KillQuest(8867, "DeadLines", "Frenzied Mana");

        //Nigh Invincible 8868
        Story.KillQuest(8868, "DeadLines", "Eternal Dragon");
    }

    public void ShadowFlame()
    {
        if (Core.isCompletedBefore(8965))
            return;

        Story.PreLoad(this);

        // Lies in Peace 8956
        Core.EquipClass(ClassType.Solo);
        Story.MapItemQuest(8956, "worldscore", 10877);
        Story.KillQuest(8956, "worldscore", "False Wyvern");

        // There Was an Attempt 8957
        Story.MapItemQuest(8957, "worldscore", 10878, 5);
        Story.KillQuest(8957, "worldscore", "Elemental Attempt");

        // Scared Straight 8958
        Story.MapItemQuest(8958, "worldscore", 10879);
        Story.KillQuest(8958, "worldscore", "Infernal Mockery");

        // Making Waves 8959
        Story.MapItemQuest(8959, "worldscore", 10880, 4);
        Story.KillQuest(8959, "worldscore", "Infernal Mockery");

        // In Circles 8960
        Story.MapItemQuest(8960, "worldscore", 10881);
        Story.KillQuest(8960, "worldscore", "Elemental Attempt");

        // Nameless Remnants 8961
        Story.MapItemQuest(8961, "worldscore", 10882, 4);

        // Dreams Unborn 8962
        Story.KillQuest(8962, "worldscore", "Crystalized Mana");

        // Innocent Crafts 8963
        Story.MapItemQuest(8963, "worldscore", 10883);
        Story.KillQuest(8963, "worldscore", "Infernal Mockery");

        // Cold Bedrock 8964
        Story.MapItemQuest(8964, "worldscore", 10884);
        Story.KillQuest(8964, "worldscore", "Elemental Attempt");

        // Rippling Heartbeat 8965
        Story.KillQuest(8965, "worldscore", "Mask of Tranquility");
    }
}
