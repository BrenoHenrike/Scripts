//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class CoreDoomwood
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.RunCore();
    }

    public void CompleteDoomwood()
    {
        DoomwoodPart1();
        DoomwoodPart2();
        DoomwoodPart3();
        AQWZombies();
    }

    public void DoomwoodPart1()
    {
        DoomwoodForest();
        ChoppingMaul();
        NecroTower();
        NecroU();
        TempleOfTheLight();
        MadScientistsLab();
    }

    public void DoomwoodPart2()
    {
        NecropolisDungeon();
        NecropolisCavern();
    }

    public void DoomwoodPart3()
    {
        Thornsgarde();
        Stonewood();
        TechDungeon();
        StonewoodForest();
        TechFortress();
    }


    #region Part 1

    public void DoomwoodForest()
    {
        if (Core.isCompletedBefore(1089))
            return;

        Story.PreLoad(this);

        //1080    Lightguard Keep Found
        Story.ChainQuest(1080);

        //1064    Bony Battalion
        Story.KillQuest(1064, "doomwood", new[] { "Doomwood Ectomancer", "Doomwood Bonemuncher", "Doomwood Soldier" });

        //1065    Warrior Rez-queue
        Story.MapItemQuest(1065, "doomwood", 423, 5);

        //1066    Bone-tired Backup
        if (!Story.QuestProgression(1066))
        {
            Core.EnsureAccept(1066);
            Core.KillMonster("doomwood", "r6", "Right", "*", "Warrior Reinforced");
            Core.EnsureComplete(1066);
        }

        //1067    Reconnaissance Route
        Story.MapItemQuest(1067, "doomwood", 422);

        //1068    Fight Against Shadowed Light
        Story.KillQuest(1068, "doomwood", "Undead Paladin");

        //1069    Camouflage: Skelly-Style
        Story.KillQuest(1069, "doomwood", new[] { "Doomwood Bonemuncher", "Doomwood Ectomancer", "Doomwood Soldier" });

        //1070    De(ad)ception
        if (!Story.QuestProgression(1070))
        {
            Core.EnsureAccept(1070);
            Core.GetMapItem(427, map: "doomundead");
            Core.KillMonster("doomundead", "r3", "Right", "*", "Light Knight Lifeforce", 5);
            Core.EnsureComplete(1070);
        }

        //1089    Artix Stopped!
        Story.ChainQuest(1089);
    }

    public void ChoppingMaul()
    {
        if (Core.isCompletedBefore(1085))
            return;

        DoomwoodForest();

        Story.PreLoad(this);

        //1081    Zorbak's Hideout
        Story.MapItemQuest(1081, "maul", 435);

        //1082    Stink-tuary
        Story.MapItemQuest(1082, "maul", 434, 13);

        //1083    The Infected
        Story.KillQuest(1083, "maul", new[] { "Slimeskull", "Personal Chopper" });

        //1084    Chopping Spree
        if (!Story.QuestProgression(1084))
        {
            Core.EnsureAccept(1084);
            Core.KillMonster("maul", "r2", "Left", "*", "Body Part Donation", 10);
            Core.GetMapItem(436, 2, "maul");
            Core.EnsureComplete(1084);
        }

        //1085    GraveStop the Creature
        Story.KillQuest(1085, "maul", "Creature Creation");
    }

    public void NecroTower()
    {
        if (Core.isCompletedBefore(1101))
            return;

        ChoppingMaul();

        Story.PreLoad(this);

        //1087    ID What You Did There
        if (!Story.QuestProgression(1087))
        {
            Core.EnsureAccept(1087);
            Core.KillMonster("necrotower", "r2", "Left", "Doomwood Treeant", "Pain-per", 5);
            Core.KillMonster("necrotower", "r2", "Left", "Slimeskull", "Toxic Goo", 5);
            Core.EnsureComplete(1087);
        }

        //1088    The Ego and the ID
        Story.KillQuest(1088, "necrotower", new[] { "DoomWood Soldier", "DoomWood Soldier" });

        //1090    An IDeal Seal
        Story.KillQuest(1090, "necrotower", "DoomWood Bonemuncher");

        //1091    Need for Speed (Reading)!
        Story.MapItemQuest(1091, "necrotower", 438);

        //Necro Tower Elevator Minigame
        for (int i = 1092; i <= 1101; i++)
            Story.ChainQuest(i);
    }

    public void NecroU()
    {
        if (Core.isCompletedBefore(1154))
            return;

        NecroTower();

        Story.PreLoad(this);

        //1112    Silence is Ghoulden
        Story.KillQuest(1112, "necroU", "Ghoul");

        //1113    Ghouls with Gall
        Story.MapItemQuest(1113, "necroU", 450, 5);

        //1114    Goals for Ghouls and Other Undead
        Story.KillQuest(1114, "necroU", new[] { "Ghoul", "Doomwood Soldier", "Doomwood Soldier" });

        //1115    Knee Bone's Connected to the Thigh Bone
        Story.MapItemQuest(1115, "necroU", 449, 6);

        //1116    Hip Bone's Connected to the Back Bone
        Story.KillQuest(1116, "necroU", new[] { "Doomwood Soldier", "Doomwood Soldier" });

        //1117    Back Bone's Connected to the Neck Bone
        Story.MapItemQuest(1117, "necroU", 451, 3);
        Story.KillQuest(1117, "necroU", "Doomwood Treeant");

        //1118   Slip 'n Slimes
        Story.MapItemQuest(1118, "necroU", 452, 5);
        Story.KillQuest(1118, "necroU", "Slimeskull");

        //1119   Sl-eye-me
        Story.MapItemQuest(1119, "necroU", 453, 5);

        //1120   Bones Over Brawn
        Story.KillQuest(1120, "necroU", "Doomwood Soldier");

        //1121   Skullaton Shells
        Story.MapItemQuest(1121, "necroU", 454, 3);
        Story.MapItemQuest(1121, "necroU", 455, 3);
        Story.KillQuest(1121, "necroU", new[] { "Shelleton", "Necro U" });

        //1154   Noxious Noxus
        Story.ChainQuest(1154);

        //1170   Vordred Boss!
        Story.KillQuest(1170, "vordredboss", "Ultra Vordred");
    }

    public void TempleOfTheLight()
    {
        if (Core.isCompletedBefore(1148))
            return;

        Story.PreLoad(this);

        //1123    Level 1
        if (!Story.QuestProgression(1123))
        {
            Core.EnsureAccept(1123);
            Core.KillMonster("temple", "r2", "up", "SlimeSkull", "Slimeskull Trophy", 5);
            Core.KillMonster("temple", "r2", "up", "Doomwood Bonemuncher", "Munched Boneshard", 5);
            Core.KillMonster("temple", "r2", "up", "Shelleton", "Shelleton Shrapnel", 5);
            Core.EnsureComplete(1123);
        }

        //1124    Level 2
        if (!Story.QuestProgression(1124))
        {
            Core.EnsureAccept(1124);
            Core.GetMapItem(456, 6, "temple");
            Core.KillMonster("temple", "r3", "up", "Undead Mage", "Necrotic Rune", 10);
            Core.KillMonster("temple", "r3", "up", "Doomwood Ectomancer", "Ecto-Covered Rune", 3);
            Core.EnsureComplete(1124);
        }

        //1125    Level 3
        if (!Story.QuestProgression(1125))
        {
            Core.EnsureAccept(1125);
            Core.KillMonster("temple", "r4", "up", "Ghoul", "Ghoulish Gear", 1);
            Core.KillMonster("temple", "r4", "up", "Lich", "Haunted Habiliment", 1);
            Core.EnsureComplete(1125);
        }

        //1126    Level 4
        if (!Story.QuestProgression(1126))
        {
            Core.EnsureAccept(1126);
            Core.GetMapItem(457, 8, "temple");
            Core.KillMonster("temple", "r5", "up", "Skeletal Fire Mage", "Flame Extinguished", 10);
            Core.EnsureComplete(1126);
        }

        //1127    Level 5
        Story.KillQuest(1127, "temple", "Doomwood Ectomancer");

        //1128    Level 6
        Story.KillQuest(1128, "temple", new[] { "Doomwood Bonemuncher", "Sanguine Souleater" });

        //1129    Level 7
        Story.MapItemQuest(1129, "temple", 458, 12);

        //1130    Level 8
        Story.KillQuest(1130, "temple", new[] { "Skeletal Fire Mage", "Doomwood Ectomancer" });

        //1131    Level 9
        Story.MapItemQuest(1131, "temple", 459, 8);
        Story.KillQuest(1131, "temple", "Ghoul");

        //1132    Level 10
        Story.KillQuest(1132, "temple", "Doomwood Ectomancer");

        //1133    Level 11
        Story.MapItemQuest(1133, "temple", 460, 10);
        Story.KillQuest(1133, "temple", "Sanguine Souleater");

        //1134    Level 12
        Story.KillQuest(1134, "temple", new[] { "SlimeSkull", "Doomwood Ectomancer" });

        //1135    Level 13
        Story.MapItemQuest(1135, "temple", 461, 12);

        //1137    Level 14
        Story.KillQuest(1137, "temple", "Doomwood Soldier");

        //1138    Level 15
        Story.KillQuest(1138, "temple", "Ghoul");

        //1139    Level 16
        Story.MapItemQuest(1139, "temple", 462, 10);

        //1140    Level 17
        Story.KillQuest(1140, "temple", new[] { "Doomwood Bonemuncher", "Skeleton" });

        //1141    Level 18
        Story.KillQuest(1141, "temple", new[] { "Undead Mage", "Skeletal Fire Mage" });

        //1142    Level 19
        Story.MapItemQuest(1142, "temple", 463, 6);
        Story.KillQuest(1142, "temple", new[] { "Shelleton", "Skeleton" });

        //1143    Level 20
        Story.KillQuest(1143, "temple", "Lich");

        //1144    Defy the Dracolich
        Story.KillQuest(1144, "temple", "Dracolich");

        //1145    Restore the Tome
        Story.MapItemQuest(1145, "temple", 464, 10);

        //1146    Recover the Pages
        Story.KillQuest(1146, "temple", "Doomwood Bonemuncher");

        //1147    Reconstruct the Codex
        Story.KillQuest(1147, "temple", "Cryptkeeper Lich");

        //1148    Galvanize the Guardian
        if (!Story.QuestProgression(1148))
        {
            Core.EnsureAccept(1148);
            Core.KillMonster("temple", "r2", "up", "Shelleton", "Basilisk's Scale", isTemp: false);
            Core.KillMonster("temple", "r2", "up", "Shelleton", "Scroll of Magic Inversion", isTemp: false);
            Core.JumpWait();
            Core.BuyItem("temple", 287, "Scroll of Cure Petrification");
            Core.EnsureComplete(1148);
        }
    }

    public void MadScientistsLab()
    {
        if (Core.isCompletedBefore(1169))
            return;

        Story.PreLoad(this);

        //1155    CastleMania: Disharmony and Despair
        Story.MapItemQuest(1155, "lab", 488);

        //1156    Stringing Your Enemies Along
        Story.KillQuest(1156, "lab", "Ant Giant|Giant Scorpion");

        //1157    Out of Tune(ing Knobs)
        Story.MapItemQuest(1157, "lab", 489, 3);
        Story.KillQuest(1157, "lab", "Ant Giant");

        //1158    Code of Conduct-or
        Story.KillQuest(1158, "lab", "Ant Giant|Giant Scorpion");

        //1159    Sending Out an SOS to the World
        Story.KillQuest(1159, "lab", "Ant Giant|Giant Scorpion");

        //1160    This is FINAL ZAP!
        Story.MapItemQuest(1160, "lab", 490, 6);

        //1161   You Doing This Again?
        Story.MapItemQuest(1161, "lab", 491, 7);
        Story.KillQuest(1161, "lab", "Giant Scorpion");

        //1162    Die, All of You!
        Story.KillQuest(1162, "lab", "Ant Giant|Giant Scorpion");

        //1163    Ample Amps Required
        Story.MapItemQuest(1163, "lab", 492, 10);

        //1164    Upward Over the Mountain
        Story.MapItemQuest(1164, "mountain", 493);

        //1169    Charging Up!
        Story.MapItemQuest(1169, "mountain", 494);
    }

    #endregion

    #region Part 2

    public void NecropolisDungeon()
    {
        if (Core.isCompletedBefore(2061))
            return;

        Story.PreLoad(this);

        //2044    Descent into Darkness
        if (!Story.QuestProgression(2044))
        {
            Core.EnsureAccept(2044);
            Core.JumpWait();
            Core.KillMonster("necrodungeon", "r6", "Down", "*", "1 Floor Descended", 10);
            Core.EnsureComplete(2044);
        }

        //2045    Retrieve the Past, Room 1
        Story.MapItemQuest(2045, "necrodungeon", 1001);
        Story.KillQuest(2045, "necrodungeon", "Necropolis Soldier");

        //2046    Retrieve the Past, Room 2
        Story.MapItemQuest(2046, "necrodungeon", 1015, 5);
        Story.MapItemQuest(2046, "necrodungeon", 1002);

        //2047    Retrieve the Past, Room 3
        Story.MapItemQuest(2047, "necrodungeon", 1003);
        Story.KillQuest(2047, "necrodungeon", "SlimeSkull");

        //2048    Retrieve the Past, Room 4
        Story.MapItemQuest(2048, "necrodungeon", 1004);
        Story.KillQuest(2048, "necrodungeon", new[] { "Necropolis Soldier", "Ghoul" });

        //2049    Deeper into Darkness
        if (!Story.QuestProgression(2049))
        {
            Core.EnsureAccept(2049);
            Core.KillMonster("necrodungeon", "r11", "Down", "*", "1 Floor Descended", 10);
            Core.JumpWait();
            Core.EnsureComplete(2049);
        }

        //2050    Retrieve the Past, Room 5
        Story.MapItemQuest(2050, "necrodungeon", 1005);
        Story.MapItemQuest(2050, "necrodungeon", 1016, 3);
        Story.KillQuest(2050, "necrodungeon", new[] { "SlimeSkull", "Necropolis Soldier" });

        //2051    Retrieve the Past, Room 6
        Story.MapItemQuest(2051, "necrodungeon", 1017, 5);
        Story.MapItemQuest(2051, "necrodungeon", 1006);

        //2052    Retrieve the Past, Room 7
        Story.MapItemQuest(2052, "necrodungeon", 1007);
        Story.KillQuest(2052, "necrodungeon", "SlimeSkull");

        //2053    Monster Subway Ahead!
        Story.MapItemQuest(2053, "necrodungeon", 1008);
        Story.KillQuest(2053, "necrodungeon", "Doom Crawler");

        //2054    Underground RailRoad... to DOOM!
        Story.KillQuest(2054, "necrodungeon", "Ghoul");
        Story.MapItemQuest(2054, "necrodungeon", 1009);
        Story.MapItemQuest(2054, "necrodungeon", 1018, 8);

        //2055    Retrieve the Past, Room 10
        Story.MapItemQuest(2055, "necrodungeon", 1010);
        Story.KillQuest(2055, "necrodungeon", "Necropolis Soldier");

        //2056    The Deepest Descent
        if (!Story.QuestProgression(2056))
        {
            Core.EnsureAccept(2056);
            Core.JumpWait();
            Core.KillMonster("necrodungeon", "r18", "Down", "*", "1 Floor Descended", 10);
            Core.EnsureComplete(2056);
        }

        //2057    Retrieve the Past, Room 11
        Story.MapItemQuest(2057, "necrodungeon", 1016, 5);
        Story.MapItemQuest(2057, "necrodungeon", 1011);

        //2058    Retrieve the Past, Room 12
        Story.MapItemQuest(2058, "necrodungeon", 1012);
        Story.KillQuest(2058, "necrodungeon", "Necropolis Soldier");

        //2059    Retrieve the Past, Room 13
        Story.MapItemQuest(2059, "necrodungeon", 1019, 5);
        Story.MapItemQuest(2059, "necrodungeon", 1013);

        //2060    Fives Times the Fury
        if (!Bot.Quests.IsUnlocked(2061))
        {
            Core.EnsureAccept(2060);
            Core.KillMonster("necrodungeon", "r22", "Down", 889);
            Core.KillMonster("necrodungeon", "r22", "Down", 890);
            Core.KillMonster("necrodungeon", "r22", "Down", 891);
            Core.KillMonster("necrodungeon", "r22", "Down", 892);
            Core.KillMonster("necrodungeon", "r22", "Down", 893);
            Core.EnsureComplete(2060);
        }

        //2061    The Past Will Haunt You
        Story.MapItemQuest(2061, "necrodungeon", 1020);
    }

    public void NecropolisCavern()
    {
        if (Core.isCompletedBefore(2077))
            return;

        Story.PreLoad(this);

        //2070    Thou Shalt Not Pass
        Story.KillQuest(2070, "necrocavern", "Shadowstone Elemental");

        //2071    Blinded by the Darkness
        Story.MapItemQuest(2071, "necrocavern", 1042, 6);
        Story.KillQuest(2071, "necrocavern", "Shadow Imp");

        //2072    The Tale Never Dies
        Story.MapItemQuest(2072, "necrocavern", 1044);
        Story.MapItemQuest(2072, "necrocavern", 1045, 3);
        Story.KillQuest(2072, "necrocavern", new[] { "Shadowstone Elemental", "Shadow Imp" });

        //2073    Doom Outside the Dome
        Story.KillQuest(2073, "necrocavern", "Shadowstone Elemental");

        //2074    Last Bastion of Light
        Story.KillQuest(2074, "necrocavern", new[] { "Shadowstone Elemental", "Shadow Imp" });

        //2075    Shadowy Corruption
        Story.MapItemQuest(2075, "necrocavern", 1043, 5);

        //2076    Strength of the Darkness
        Story.KillQuest(2076, "necrocavern", "Shadow Dragon");

        //2077    Bring Down the Necropolis
        Story.KillQuest(2077, "necrocavern", "Shadowstone Support");
    }

    #endregion

    #region Part 3

    public void Thornsgarde()
    {
        if (Core.isCompletedBefore(7653))
            return;

        Story.PreLoad(this);

        //7589    Bullies
        Story.KillQuest(7589, "thorngarde", new[] { "CryptHacker", "NecroMech", "NecroDrone" });

        //7590    Free the People
        Story.KillQuest(7590, "thorngarde", "NecroMech");
        Story.MapItemQuest(7590, "thorngarde", 7485, 5);

        //7591    Sample It
        Story.KillQuest(7591, "thorngarde", "NecroMech");

        //7592    Swat the Flies
        Story.KillQuest(7592, "thorngarde", "CryptHacker");

        //7593    Reclaim the Supplies
        if (!Story.QuestProgression(7593))
        {
            Core.EnsureAccept(7593);
            Core.HuntMonster("thorngarde", "CryptHacker", "Cured Meat", 3);
            Core.HuntMonster("thorngarde", "NecroMech", "Bag of Grain", 3);
            Core.HuntMonster("thorngarde", "NecroDrone", "Jug of Water", 5);
            Core.EnsureComplete(7593);
        }

        //7594    Who's That?
        Story.MapItemQuest(7594, "thorngarde", 7486);

        //7595    Find my Stuff
        Story.KillQuest(7595, "thorngarde", "NecroDrone");
        Story.MapItemQuest(7595, "thorngarde", new[] { 7487, 7488 });

        //7596    Save my Friends
        Story.KillQuest(7596, "thorngarde", "NecroMech");
        Story.MapItemQuest(7596, "thorngarde", new[] { 7489, 7490, 7491 });

        //7597    Pull the Wires
        Story.KillQuest(7597, "thorngarde", "CryptHacker");

        //7598    Dress-up Time!
        Story.KillQuest(7598, "thorngarde", new[] { "CryptHacker", "NecroDrone" });

        //7599    I Need a Distraction
        if (!Story.QuestProgression(7599))
        {
            Core.EnsureAccept(7599);
            Core.HuntMonster("thorngarde", "NecroMech", "NecroMech Slain", 5);
            Core.HuntMonster("thorngarde", "CryptHacker", "CryptHacker Slain", 5);
            Core.HuntMonster("thorngarde", "NecroDrone", "NecroDrone Slain", 5);
            Core.EnsureComplete(7599);
        }

        //7600    Zyrus is Lost
        Story.KillQuest(7600, "thorngarde", "Zyrus the BioKnight");

        //7601    Investigate the Tech
        if (!Story.QuestProgression(7601))
        {
            Core.EnsureAccept(7601);
            Core.HuntMonster("thorngarde", "NecroDrone", "Deadtech Power Core", 7);
            Core.HuntMonster("thorngarde", "NecroMech", "NecroMech Targeting Systems", 5);
            Core.HuntMonster("thorngarde", "CryptHacker", "CryptHacker Circuitry", 15);
            Core.HuntMonster("thorngarde", "Zyrus the BioKnight", "BioKnight Engine", 3);
            Core.EnsureComplete(7601);
        }
    }

    public void Stonewood()
    {
        if (Core.isCompletedBefore(7653))
            return;

        Story.PreLoad(this);

        //7603    Defeat the Deadtech
        Story.KillQuest(7603, "stonewood", "NecroDrone");

        //7604    Gather Spell Components
        Story.KillQuest(7604, "stonewood", "Doomwood Treeant");
        Story.MapItemQuest(7604, "stonewood", 7510, 6);

        //7605    A Few Things More
        Story.KillQuest(7605, "stonewood", new[] { "NecroDrone", "NecroMech" });

        //7606    Search for the Axe
        Story.MapItemQuest(7606, "stonewood", 7511, 4);
        Story.MapItemQuest(7606, "stonewood", 7512);

        //7607    Defeat the BioKnight
        Story.KillQuest(7607, "stonewood", "BioKnight");

        //7608    Take the Axe!
        Story.MapItemQuest(7608, "stonewood", 7513);
    }

    public void TechDungeon()
    {
        if (Core.isCompletedBefore(7636))
            return;

        Story.PreLoad(this);

        //7609    Find the Keys
        Story.KillQuest(7609, "techdungeon", "NecroMech");

        //7610    Ew, Rats
        Story.KillQuest(7610, "techdungeon", "Rotting Rat");
        Story.MapItemQuest(7610, "techdungeon", 7515, 6);

        //7611    Grab Their Gear
        if (!Story.QuestProgression(7611))
        {
            Core.EnsureAccept(7611);
            Core.KillMonster("techdungeon", "r4", "left", "DoomBorg Guard", "Guard Armor Piece", 5);
            Core.KillMonster("techdungeon", "r4", "left", "DoomBorg Guard", "Guard Helm");
            Core.KillMonster("techdungeon", "r4", "left", "DoomBorg Guard", "Deadtech Polearm");
            Core.EnsureComplete(7611);
        }

        //7612    Talk to the Guard
        Story.MapItemQuest(7612, "techdungeon", 7514);

        //7613    Let Them Out!
        Story.MapItemQuest(7613, "techdungeon", 7516);

        //7614    Head on Out
        Story.KillQuest(7614, "techdungeon", "DoomBorg Guard");
        Story.MapItemQuest(7614, "techdungeon", 7517);

        //7615    Take Him Down
        Story.KillQuest(7615, "techdungeon", "Kalron the Cryptborg");
    }

    public void StonewoodForest()
    {
        if (Core.isCompletedBefore(7636))
            return;

        Story.PreLoad(this);

        //7623    Cat Ears?
        Story.MapItemQuest(7623, "stonewooddeep", 7528, 1);

        //7624    EN GARDE!
        Story.KillQuest(7624, "stonewooddeep", "Asherion");

        //7625    The Light Of Destiny
        Story.MapItemQuest(7625, "stonewooddeep", 7529);

        //7626    Stuff for Dummies
        if (!Story.QuestProgression(7626))
        {
            Bot.Options.AttackWithoutTarget = true;
            Core.EnsureAccept(7626);
            Core.KillMonster("stonewooddeep", "r2", "Right", "Doomwood Treeant", "Sturdy Wood", 8);
            Story.MapItemQuest(7626, "stonewooddeep", 7530, 8);
            Bot.Options.AttackWithoutTarget = false;
        }

        //7627    Build the Dummies
        if (!Story.QuestProgression(7627))
        {
            Core.EnsureAccept(7627);
            Bot.Options.AttackWithoutTarget = true;
            Core.KillMonster("stonewooddeep", "r3", "Right", "Doomwood Treeant", "Area Cleared", 10);
            Bot.Options.AttackWithoutTarget = false;
            Core.GetMapItem(7531, 6, "stonewooddeep");
            Core.EnsureComplete(7627);
        }

        //7628    Battle the Dummies
        if (!Story.QuestProgression(7628))
        {
            Core.EnsureAccept(7628);
            Core.HuntMonsterMapID("stonewooddeep", 4 | 5, "Target Dummy Slain", 6);
            Core.EnsureComplete(7628);
        }

        //7629    Lesson 1: Bravery
        if (!Story.QuestProgression(7629))
        {
            Core.EnsureAccept(7629);
            Core.KillMonster("stonewooddeep", "r3", "Right", "Doomwood SLime", "Slime Slain", 10);
            Core.EnsureComplete(7629);
        }

        //7630    Lesson 2: Armor
        if (!Story.QuestProgression(7630))
        {
            Core.EnsureAccept(7630);
            Core.KillMonster("stonewooddeep", "r3", "Right", "Doomwood SLime", "Stolen Armor", 7);
            Core.EnsureComplete(7630);
        }

        //7631    Lesson 3: Protection
        if (!Story.QuestProgression(7631))
        {
            Core.EnsureAccept(7631);
            Core.HuntMonster("stonewooddeep", "CryptHacker", "Crypthacker Slain", 10);
            Core.HuntMonster("stonewooddeep", "CryptHacker", "Unidentified Clue");
            Core.EnsureComplete(7631);
        }

        //7632    Lesson 4: Gather Intelligence
        Story.KillQuest(7632, "stonewooddeep", "CryptHacker");

        //7633    Lesson 5: Demolition
        if (!Story.QuestProgression(7633))
        {
            Core.EnsureAccept(7633);
            Core.KillMonster("stonewooddeep", "r3", "Right", "Doomwood SLime", "Acid Ooze", 6);
            Core.HuntMonster("stonewooddeep", "NecroDrone", "Explosive Tech", 6);
            Core.EnsureComplete(7633);
        }

        //7634    Lesson 6: Searching
        Story.MapItemQuest(7634, "stonewooddeep", 7532);

        //7635    Get the Axe
        Story.MapItemQuest(7635, "stonewooddeep", 7533);

        //7636    Never Give Up
        Story.KillQuest(7636, "stonewooddeep", "Sir Kut");
    }

    public void TechFortress()
    {
        if (Core.isCompletedBefore(7653))
            return;

        Story.PreLoad(this);

        //7637    I CPU U!
        Story.KillQuest(7637, "techfortress", "CryptHacker");

        //7638    Keep Droning On
        Story.KillQuest(7638, "techfortress", "NecroDrone");

        //7639    Gathering Data
        Story.KillQuest(7639, "techfortress", "CryptHacker");

        //7640    Signal Flare
        Story.KillQuest(7640, "techfortress", "NecroDrone");
        Story.MapItemQuest(7640, "techfortress", 7561);

        //7641    Medal Time
        if (!Story.QuestProgression(7641))
        {
            Core.EnsureAccept(7641);
            Core.HuntMonster("techfortress", "NecroDrone", "NecroMedals", 5);
            Core.EnsureComplete(7641);
        }

        //7642    Hidden Data
        Story.KillQuest(7642, "techfortress", "NecroMech");

        //7643    Pass the CPU
        Story.KillQuest(7643, "techfortress", "CPU");

        //7644    The Guards
        Story.KillQuest(7644, "techfortress", "DoomBorg Guard");

        //7645    Circui-try This
        Story.KillQuest(7645, "techfortress", "DoomBorg Guard");

        //7646    The Thing
        Story.KillQuest(7646, "techfortress", "Vortrix");

        //7653    The Final Form
        Story.KillQuest(7653, "techfortress", "MechaVortrix");
    }

    #endregion

    public void AQWZombies()
    {
        if (Core.isCompletedBefore(2128))
            return;

        Story.PreLoad(this);

        //2093    Undead Assault
        Story.KillQuest(2093, "battleundera", "Skeletal Soldier");

        //2094    Skull Crusher Mountain
        Story.KillQuest(2094, "battleundera", "Skeletal Ice Mage");

        //2095    The Undead Giant
        Story.KillQuest(2095, "battleundera", "Angry Undead Giant");

        //2096    Talk to the Knights
        Story.MapItemQuest(2096, "doomhaven", 1174, 5);

        //2097    Defend the Throne Room
        Story.KillQuest(2097, "Doomhaven", "Skeletal Viking");

        //2117    Rolith Defeated
        Story.ChainQuest(2117);

        //2120    Lair
        Story.ChainQuest(2120);

        //2121    Mythsong
        Story.ChainQuest(2121);

        //2122    Arcangrove
        Story.ChainQuest(2122);

        //2123    Willowshire
        Story.ChainQuest(2123);

        //2119    Battleon
        Story.ChainQuest(2119);

        //2124    Keep the Area Clear
        Story.KillQuest(2124, "doomwar", "Angry Zombie");

        //2125    Defeat Zombie Dragons
        Story.KillQuest(2125, "doomwar", "Zombie Dragon");

        //2126    Defeat Your Fallen Friends
        if (!Story.QuestProgression(2126))
        {
            Core.EnsureAccept(2126);
            Core.KillMonster("doomwar", "r5", "left", "Cyzerombie");
            Core.KillMonster("doomwar", "r7", "left", "Zombie Warlic");
            Core.KillMonster("doomwar", "r9", "left", "Zombie Galanoth");
            Core.KillMonster("doomwar", "r3", "left", "Zhoombie");
            Core.EnsureComplete(2126);
        }

        //2127    Long Unlive the King!
        Story.KillQuest(2127, "doomwar", "Zombie King Alteon");

        //2128    Dark Sepulchure Must be Slain!
        Story.KillQuest(2128, "sepulchure", "Dark Sepulchure");
    }
}