//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/ShadowWar.cs
//cs_include Scripts/Story/Legion/DarkAlly.cs
//cs_include Scripts/Story/Legion/DarkAlliance.cs
using RBot;

public class SOW
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public ShadowWar SW = new();
    public DarkAlly_Story DAlly = new();
    public DarkAlliance_Story DAlliance = new();


    public void ScriptMain(ScriptInterface bot)
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
    }

    public void ShadowWar()
    {
        SW.DoAll();
    }

    public void ShadowlordKeep()
    {
        if (Core.isCompletedBefore(6864))
            return;

        Story.PreLoad();

        //Defeat the Minions (6856)
        Story.KillQuest(6856, "shadowlordkeep", "Shadow Gunner|Shadow Mage");
        Story.MapItemQuest(6856, "shadowlordkeep", 6390);

        //Gather the Gunpowder (6857)
        Story.KillQuest(6857, "shadowlordkeep", "Shadow Gunner");

        //Build a Bomb (6858)
        Story.KillQuest(6858, "shadowlordkeep", new[] { "Shadow Mage", "Shadow Gunner|Shadow Mage" });

        //Light 'em Up! (6859)
        Story.MapItemQuest(6859, "shadowlordkeep", 6391);

        //Fight the Shadows (6860)
        Story.KillQuest(6860, "shadowlordkeep", "Shadow Imp|Shadow Locust");

        //Magical Potion (6861)
        Story.KillQuest(6861, "shadowlordkeep", "Shadow Mage");

        //Overload the Energy Field (6862)
        Story.KillQuest(6862, "shadowlordkeep", "Stray Energy");
        Story.MapItemQuest(6862, "shadowlordkeep", 6392);

        //Defeat the Portal Guards (6863)
        Story.KillQuest(6863, "shadowlordkeep", "Portal Guard");

        //Go Through the Portal (6864)
        Story.MapItemQuest(6864, "timestream", 6393);
    }

    public void Timestream()
    {
        if (Core.isCompletedBefore(6867))
            return;

        Story.PreLoad();

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

        Story.PreLoad();

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

        Story.PreLoad();

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

        Story.PreLoad();

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

        Story.PreLoad();

        //Whispering Walls (6961)
        Story.MapItemQuest(6961, "junkheap", 6500, 5);

        //So Many Magpies (6962)
        Story.KillQuest(6962, "junkheap", "Magpie");

        //Charge the Portals (6963)
        Story.KillQuest(6963, "junkheap", "Tiny Manifestation");
        Story.MapItemQuest(6963, "junkheap", 6501);

        //Through the Rift (6964)
        Story.MapItemQuest(6964, "junkheap", 6502);

        //Free Niknak (6965)
        Story.KillQuest(6965, "junkheap", "Shadowflame Scout");
        Story.MapItemQuest(6965, "junkheap", 6503);

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

        Story.PreLoad();

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

        //Mutant Dragon Oil (6998)
        Story.KillQuest(6998, "shadowgrove", "Mutant Shadow Dragon");

        //Titan Shadow Dragonlord (6999)
        Story.KillQuest(6999, "shadowgrove", "Titan Shadow Dragonlord");
    }

    public void AozoraHills()
    {
        if (Core.isCompletedBefore(7062))
            return;

        Story.PreLoad();

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
        Story.KillQuest(7055, "aozorahills", "Fuyurei");
        Story.MapItemQuest(7055, "aozorahills", 6646);

        //Follow the Path (7056)
        Story.MapItemQuest(7056, "aozorahills", 6647, 3);

        //Attune to Kijimuna (7057)
        Story.KillQuest(7057, "aozorahills", "Aozora Kijimuna");
        Story.MapItemQuest(7057, "aozorahills", 6648);

        //Path of the Kijimuna (7058)
        Story.MapItemQuest(7058, "aozorahills", 6649, 3);

        //Attune to Reishi (7059)
        Story.KillQuest(7059, "aozorahills", "Reishi");
        Story.MapItemQuest(7059, "aozorahills", 6650);

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

        Story.PreLoad();

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

        Story.PreLoad();

        //Shadow Medal (7340)
        Story.KillQuest(7340, "shadowsong", "Shadowflame Troll");

        //Song of Fate (7341)
        Story.KillQuest(7341, "shadowsong", "Beatle|Tune-A-Fish");

        //Broken Strings (7342)
        Story.KillQuest(7342, "shadowsong", "Beatle");

        //Shattered Crystal (7343)
        Story.KillQuest(7343, "shadowsong", "Shattered Crystal");

        //Mega Shadow Medals (7344)
        Story.KillQuest(7344, "shadowsong", "Shadowflame Troll|Shadowflame Ur-Troll");

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
        DAlly.DarkAlly_Questline();
    }

    public void DarkAlliance()
    {
        DAlliance.DarkAlliance_Questline();
    }

    public void InnerShadows()
    {
        DAlliance.DarkAlliance_Questline();

        if (Core.isCompletedBefore(7472))
            return;

        Story.PreLoad();

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

}
