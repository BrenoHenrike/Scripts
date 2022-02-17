using RBot;

public class Core13LoC
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void Complete13LOC(bool withExtras = false)
    {
        if (Core.IsMember)
        {
            Prologue();
            Escherion();
            Vath();
            Kitsune();
            Wolfwing();
            Kimberly();
            Ledgermayne();
            Tibicenas();
            KhasaandaHorc();
            Iadoa();
            Lionfang();
            Xiang();
            Alteon();
        }
        Hero();
        if (withExtras)
        {
            KhasaandaTroll(true);
            Extra();
        }
    }

    public void Prologue()
    {

        if (Core.isCompletedBefore(6219))
            return;

        //Map: PortalUndead
        if (!Core.QuestProgression(183))                                                  // Enter the gates
        {
            Core.EnsureAccept(183);            // Enter the gates
            Core.KillMonster("portalundead", "Enter", "Spawn", "Skeletal Fire Mage", "Defeated Fire Mage", 4);
            Core.EnsureComplete(183);
        }
        //Map: SwordhavenUndead
        Core.KillQuest(176, "swordhavenundead", "Skeletal Soldier");                                                // Undead Assault
        Core.KillQuest(177, "swordhavenundead", "Skeletal Ice Mage");                                               // Skull Crusher Mountain
        Core.KillQuest(178, "swordhavenundead", "Undead Giant");                                                    // The Undead Giant
        //Map: CastleUndead
        Core.MapItemQuest(179, "castleundead", 38, 5);                                                              // Talk to the Knights
        Core.KillQuest(180, "castleundead", "*");                                         // Defend the Throne
        if (!Core.QuestProgression(1))                                                    // The Arrival of Drakath cutscene
        {
            Core.Join("castleundead", "King2", "Center");
            Bot.SendPacket($"%xt%zm%updateQuest%188220%41%{(Core.HeroAlignment > 1 ? 1 : Core.HeroAlignment)}%");
            Bot.Sleep(2000);
            Core.Join("shadowfall");
            Bot.Sleep(2000);
        }
        //Map: ChaosCrypt
        Core.KillQuest(196, "chaoscrypt", "Chaorrupted Armor");                          // Recover Sepulchure's Cursed Armor!
        //Map: Prison
        Core.MapItemQuest(6216, "prison", 39, 5);                                        // Unlife Insurance
        Core.BuyQuest(6216, "prison", 1559, "Unlife Insurance Bond");
        //Map: Various
        Core.MapItemQuest(6217, "chaoscrypt", 5662);                                                                // Enter the Crypt
        Core.KillQuest(6218, "chaoscrypt", "Chaorrupted Knight");                                                   // Rescue the Knights
        Core.KillQuest(6219, "forestchaos", new[] { "Chaorrupted Wolf", "Chaorrupted Bear" });  // Forest of Chaos
    }

    public void Escherion()
    {

        if (Core.isCompletedBefore(272))
            return;

        //Map: Mobius
        Core.KillQuest(245, "mobius", "Chaos Sp-Eye");                                  // Winged Spies
        Core.MapItemQuest(246, "mobius", 42, 5);                                        // Chaos Prisoners
        Core.KillQuest(247, "mobius", "Fire Imp");            // IMP-possible Task
        Core.MapItemQuest(260, "mobius", 44);                 // You Can't Miss It
        Core.KillQuest(248, "mobius", "Cyclops Raider");                                // Far Sighted
        Core.KillQuest(249, "mobius", "Slugfit");                                       // Slugfest
        //Map: Faerie
        Core.KillQuest(250, "faerie", "Chainsaw Sneevil");                              // Chain Reaction
        Core.MapItemQuest(251, "faerie", 43, 7);                                        // Epic Drops
        Core.KillQuest(252, "faerie", "Chainsaw Sneevil");    // Jarring Theft
        Core.KillQuest(255, "faerie", "Cyclops Warlord");                               // Tree Hugger
        Core.KillQuest(256, "faerie", "Aracara");                                       // The Second Piece
        //Map: Cornelis
        Core.KillQuest(257, "cornelis", "Gargoyle");          // Ruined Ruins
        Core.MapItemQuest(261, "cornelis", 45);               // Energize!
        Core.KillQuest(258, "cornelis", "Gargoyle");          // Blueish Glow
        Core.MapItemQuest(262, "cornelis", 46);               // Quickdraw
        Core.KillQuest(259, "cornelis", "Stone Golem");       // Arm Yourself
        Core.MapItemQuest(263, "cornelis", 47);               // You've Been Framed
        //Map: Mobius
        Core.MapItemQuest(266, "mobius", 48);                                           // Some Assembly Required
        //Map: Cornelis
        Core.MapItemQuest(267, "mobius", 49);                 // Teleporter Report
        Core.KillQuest(264, "mobius", "Cyclops Raider");                                // Disguise!
        Core.KillQuest(265, "faerie", "Chainsaw Sneevil");    // To-go box
        //Map: Relativity
        Core.KillQuest(268, "relativity", "Cyclops Raider");                            // Find the Key! (Part One)
        Core.KillQuest(269, "relativity", "Fire Imp");                                  // Find the Key! (Part Two)
        Core.KillQuest(270, "relativity", "Head Gargoyle");                             // Find the Key! (Part Three)
        //Map: Hydra
        Core.MapItemQuest(271, "hydra", 50);                                            // The Lake Hydra
        Core.MapItemQuest(271, "hydra", 51);
        Core.MapItemQuest(271, "hydra", 52);
        //Map: Escherion
        if (!Core.QuestProgression(272))  // Escherion
        {
            Core.EnsureAccept(272);
            Core.KillEscherion();
            Core.EnsureComplete(272);
        }
    }

    public void Vath()
    {

        if (Core.isCompletedBefore(363))
            return;

        //Map: Pines
        Core.MapItemQuest(319, "tavern", 56, 7);                                                                            // Adorable Sisters
        Core.KillQuest(320, "pines", "Pine Grizzly");                                                                       // Warm and Furry
        Core.KillQuest(321, "pines", "Red Shell Turtle");                                                                   // Shell Rock
        Core.KillQuest(322, "pines", "Twistedtooth");                                             // Bear Facts
        Core.KillQuest(324, "pines", "Red Shell Turtle");                                                                   // The Spittoon Saloon
        Core.KillQuest(325, "pines", "Pine Grizzly");                                                                       // Bear it all!
        Core.KillQuest(326, "pines", "Leatherwing");                                                                        // Leather Feathers
        if (!Core.CheckInventory("Snowbeard's Gold"))                                                                       // Follow your Nose!
            Core.KillQuest(327, "pines", "Pine Troll");
        if (!Core.QuestProgression(323))                                                          // Give Snowbeard His Gold
            Core.EnsureComplete(323);
        //Map: Dwarfhold
        Core.MapItemQuest(344, "dwarfhold", 60);                                                  // Bad Memory
        Core.KillQuest(331, "mountainpath", "Ore Balboa");                                                                  // Squeeze Water from Stone
        Core.KillQuest(332, "mountainpath", "Vultragon");                                                                   // Carrion Carrying On
        Core.KillQuest(333, "dwarfhold", "Chaos Drow");                                                                     // Bagged Lunch
        Core.KillQuest(334, "dwarfhold", "Glow Worm");                                                                      // Radiant Lamps
        Core.KillQuest(335, "dwarfhold", "Albino Bat");                                                                     // Having a Blast
        Core.KillQuest(336, "dwarfhold", "Chaotic Draconian");                                                              // Secret Weapons
        Core.MapItemQuest(337, "dwarfhold", 59, 7);                                                                         // Rock Star
        Core.KillQuest(338, "dwarfhold", "Chaos Drow");                                                                     // All that Glitters
        Core.KillQuest(339, "dwarfhold", "Chaotic Draconian");                                                              // Gemeralds
        Core.KillQuest(340, "dwarfhold", "Albino Bat");                                                                     // Talc to Me
        if (!Core.QuestProgression(343))                                                          // Upper City Gates
        {
            Core.Join("dwarfhold", "rdoor", "Right");
            Core.EnsureComplete(343);
            Bot.Sleep(2500);
        }
        Core.KillQuest(341, "dwarfhold", "Amadeus");                                              // Rock me Amadeus
        //Map: UpperCity
        Core.MapItemQuest(346, "uppercity", 61);                                                                            // Disapoofed
        Core.KillQuest(347, "uppercity", "Drow Assassin");                                                                  // Hoodwinked
        Core.KillQuest(348, "uppercity", "Chaotic Draconian");                                                              // Claws for the Cause
        Core.KillQuest(349, "uppercity", "Chaos Egg");                                                                      // Scrambled Eggs
        Core.KillQuest(350, "uppercity", "Terradactyl");                                                                    // The King's Wings
        Core.KillQuest(351, "uppercity", "Rhino Beetle");                                                                   // Bugging Out
        Core.KillQuest(352, "uppercity", "Cave Lizard");                                                                    // Lizard Gizzard
        if (!Core.QuestProgression(1))                                                            // Confront Vath
        {
            Core.Join("vath");
            Bot.Player.Jump("CutCap", "Left");
            Bot.Sleep(2500);
        }
        Core.KillQuest(353, "dwarfprison", new[] { "Balboa", "Albino Bat", "Chaos Drow" });                                 // Mock the Lock
        if (!Core.QuestProgression(354))                                                          // Like Butter
        {
            Core.Join("dwarfprison", "Enter", "Right");
            Core.EnsureComplete(354);
        }


        Core.KillQuest(355, "dwarfprison", "Warden Elfis");                                       // Jailhouse Rock
        Core.KillQuest(356, "dwarfprison", new[] { "Albino Bat", "Balboa", "Chaos Drow" });       // Explosives 101

        if (!Core.QuestProgression(357))                                                          // Big Bada-Boom
        {
            Core.EnsureAccept(356);
            if (!Core.CheckInventory("Tee-En-Tee"))
            {
                Core.HuntMonster("dwarfprison", "Albino Bat", "Nitrate Elements", 3);
                Core.HuntMonster("dwarfprison", "Chaos Drow", "Drow Shoelaces", 3);
                Core.HuntMonster("dwarfprison", "Balboa", "Flint Stone", 2);
                Core.EnsureComplete(356);
            }
            Core.Join("dwarfprison");
            Core.ChainQuest(357);
        }
        //Map: Roc
        Core.MapItemQuest(362, "roc", 62);                                                              // Defeat Rock Roc
        //Map: Stalagbite
        Core.MapItemQuest(363, "stalagbite", 63);                                                       // Facing Vath
    }

    public void Kitsune()
    {

        if (Core.isCompletedBefore(488))
            return;

        //Turtle Power
        Core.KillQuest(380, "yokaiboat", "Kappa Ninja");
        Core.MapItemQuest(380, "yokaiboat", 64);

        //Setting Sail to Yokai
        if (!Core.QuestProgression(381))
        {
            Core.EnsureAccept(381);
            Core.KillMonster("yokaiboat", "r4", "Spawn", "3844", "Sail Permit");
            Core.EnsureComplete(381);
        }

        //Dragon Koi Tournament
        Core.KillQuest(382, "dragonkoi", "Ryoku");

        //Dog Days
        // Core.KillQuest("hachiko", "Samurai Nopperabo");
        // Core.KillQuest("hachiko", "Ninja Nopperabo");
        if (!Core.QuestProgression(402))
        {
            Core.EnsureAccept(402);
            Core.HuntMonster("hachiko", "Samurai Nopperabo", "Samurai Questioned", 5);
            Core.HuntMonster("hachiko", "Ninja Nopperabo", "Ninja Questioned", 5);
            Core.EnsureComplete(402);
        }

        //Faceless Threat - i think it has to be this cell? it wasnt getting the item from the rest
        if (!Core.QuestProgression(403))
        {
            Core.EnsureAccept(403);
            Core.KillMonster("hachiko", "Ox", "Center", "Samurai Nopperabo", "Note from DT");
            Core.EnsureComplete(403);
        }

        //Zodiac Puzzle Key
        if (!Core.QuestProgression(405))
        {
            Core.EnsureAccept(405);
            Core.KillMonster("hachiko", "Tiger", "Center", "Samurai Nopperabo", "Rat-Ox-Tiger Piece");
            Core.KillMonster("hachiko", "Snake", "Center", "Ninja Nopperabo", "Rabbit-Dragon-Snake piece");
            Core.KillMonster("hachiko", "Horse", "Center", "Samurai Nopperabo", "Horse-Sheep-Monkey piece");
            Core.KillMonster("hachiko", "Pig", "Center", "Ninja Nopperabo", "Rooster-Dog-Pig Piece");
            Core.EnsureComplete(405);
        }

        //Rescue!
        Core.KillQuest(406, "hachiko", "Dai Tengu");

        //Jinmenju Tree
        Core.MapItemQuest(466, "bamboo", 90);

        //Yokai Bandits
        Core.KillQuest(467, "bamboo", new[] { "Tanuki", "Tanuki" });

        //The Fiery Fiend
        Core.KillQuest(468, "bamboo", "SoulTaker");

        //Dumpster Diving
        Core.MapItemQuest(469, "junkyard", 91);

        //Reduce, Respawn, Recycle
        if (!Core.QuestProgression(470))
        {
            Core.EnsureAccept(470);
            Core.KillMonster("Junkyard", "Enter", "Spawn", 3847, "Wild Kara-Kasa", 5);
            Core.KillMonster("Junkyard", "Enter", "Spawn", 3848, "Wild Bakezouri", 1);
            Core.KillMonster("Junkyard", "r2", "Right", 3845, "Wild Bura-Bura", 4);
            Core.KillMonster("Junkyard", "r2", "Right", 3849, "Wild Biwa-Bokuboku", 3);
            Core.KillMonster("Junkyard", "r3", "Down", 3850, "Wild Koto-Furunushi", 2);
            Core.EnsureComplete(470);
        }
        // Core.KillQuest(470, "junkyard", new[] { "Tsukumo-Gami", "Tsukumo-Gami", "Tsukumo-Gami", "Tsukumo-Gami" });

        //The Hunt for the Hag
        Core.KillQuest(471, "junkyard", "Onibaba");

        //Su-she
        Core.KillQuest(473, "yokairiver", new[] { "Funa-Yurei", "Funa-Yurei", "Funa-Yurei" });

        //Kappa Cuisine
        if (!Core.QuestProgression(474))
        {
            Core.EnsureAccept(474);
            Core.KillMonster("yokairiver", "r4", "Left", "Kappa Ninja", "Dried Nori Leaf", 6);
            Core.KillMonster("yokairiver", "r4", "Left", "Kappa Ninja", "Sumeshi Bundle", 3);
            Core.KillMonster("yokairiver", "r4", "Left", "Kappa Ninja", "Fresh Cucumber", 1);
            Core.GetMapItem(92, 1, "yokairiver");
            Core.EnsureComplete(474);
        }

        //Hisssssy fit
        Core.KillQuest(476, "yokairiver", "Nure Onna");

        //The Purrrfect Crime
        Core.KillQuest(477, "yokaigrave", "Skello Kitty");

        //The Face Off
        Core.KillQuest(478, "yokaigrave", new[] { "Ninja Nopperabo", "Samurai Nopperabo" });

        //Confront Neko Mata
        Core.KillQuest(479, "yokaigrave", "Neko Mata");

        //Defeat O-dokuro
        Core.KillQuest(481, "odokuro", "O-dokuro");

        //Defeat O-Dokuro's Head
        Core.KillQuest(484, "yokaiwar", "O-Dokuro's Head");

        //Defeat Kitsune
        Core.KillQuest(488, "kitsune", "kitsune");
    }

    public void Wolfwing()
    {

        if (Core.isCompletedBefore(598))
            return;

        //Map: DarkoviaGrave
        Core.MapItemQuest(494, "darkoviagrave", 97);                                                    // Grave Mission
        Core.KillQuest(495, "darkoviagrave", "Skeletal Fire Mage");                                     // Lending a Helping Hand
        Core.KillQuest(496, "darkoviagrave", "Rattlebones");                                            // Bone Appetit
        Core.KillQuest(497, "darkoviagrave", "Albino Bat");                                             // Batting Cage
        Core.KillQuest(498, "darkoviagrave", "Blightfang");                   // His Bark is worse than his Blight
        //Map: GreenguardEast/West
        if (!Core.CheckInventory("Red's Big Wolf Slaying Axe") && !Bot.Quests.IsUnlocked(516))          // Can I axe you something?
        {
            Core.AddDrop("Red's Big Wolf Slaying Axe");
            Core.EnsureAccept(515);
            Core.HuntMonster("greenguardeast", "Spider", "Spider Documentation");
            Core.HuntMonster("greenguardeast", "Wolf", "Wolf Documentation");
            Core.HuntMonster("greenguardwest", "Slime", "Slime Documentation");
            Core.HuntMonster("greenguardwest", "Frogzard", "Frogzard Documentation");
            Core.HuntMonster("greenguardwest", "Big Bad Boar", "Wereboar Documentation");
            Core.EnsureComplete(515);
            Bot.Wait.ForPickup("Red's Big Wolf Slaying Axe");
        }
        //Map: DarkoviaForest
        if (!Core.QuestProgression(514, GetReward: false))                    // Lil' Red
            Core.EnsureComplete(514);
        Core.KillQuest(516, "darkoviaforest", "Dire Wolf");                                             // A Dire Situation
        Core.KillQuest(517, "darkoviaforest", new[] { "Blood Maggot", "Blood Maggot", "Blood Maggot" });  // Blood, Sweat, and Tears
        Core.KillQuest(518, "darkoviaforest", "Lich of the Stone");                                     // What a Lich!
        //Map: Safiria
        Core.KillQuest(519, "safiria", "Blood Maggot");                                                 // Feeding Grounds
        Core.KillQuest(520, "safiria", "Albino Bat");                                                   //Going Batty
        Core.KillQuest(521, "safiria", "Chaos Lycan");                                                  //Lycan Knights
        Core.KillQuest(522, "safiria", "Twisted Paw");                        //Twisted Paw
        //Map: Lycan
        Core.KillQuest(534, "lycan", "Dire Wolf");                                                      // A Gift Of Meat
        Core.KillQuest(535, "lycan", new[] { "Lycan", "Lycan Knight" });                                  // No Respect
        Core.KillQuest(536, "lycan", "Chaos Vampire Knight");                                           // Vampire Knights
        Core.KillQuest(537, "lycan", "Sanguine");                             // Sanguine
        //Map: LycanWar
        if (!Core.QuestProgression(564))
        {
            Core.KillMonster("lycanwar", "Boss", "Left", "Edvard");
            Bot.Sleep(5000);
            Core.UpdateQuest(564);
            Core.MapItemQuest(564, "chaoscave", 107);
        }
        //Map: ChaosCave                                                     // Search and Report
        Core.KillQuest(565, "chaoscave", "Werepyre");                                                   // The Key is the Key
        Core.KillQuest(566, "chaoscave", "Werepyre");                                                   // Secret Words
        Core.KillQuest(567, "chaoscave", "Dracowerepyre");                          // Dracowerepyre
        //Map: Wolfwing
        Core.HuntMonster("wolfwing", "Wolfwing");                                                       // Wolfwing
        Bot.Sleep(3000);
        if (Core.HeroAlignment == 0)
            Core.EnsureComplete(597);
        else
            Core.EnsureComplete(598);
    }

    public void Kimberly()
    {
        if (Core.isCompletedBefore(710))
            return;

        //Stairway to Heaven
        Core.KillQuest(648, "stairway", new[] { "Rock Lobster", "Grateful Undead" });

        //Rolling Stones
        Core.KillQuest(649, "stairway", "Rock Lobster");

        //Light My Fire
        Core.KillQuest(650, "stairway", "Grateful Undead");

        //Knockin' on Haven's Door
        Core.KillQuest(651, "stairway", new[] { "Elwood Bruise", "Jake Bruise" });

        //Staying Alive
        Core.KillQuest(658, "beehive", "Stinger");

        //Killer Queen
        Core.KillQuest(659, "beehive", "Killer Queen Bee");

        //Satisfaction
        Core.KillQuest(660, "beehive", "Lord Ovthedance");

        //Dance with Great Godfather of Souls
        if (!Core.QuestProgression(661))
        {
            Core.EnsureAccept(661);
            Core.Join("beehive");
            Core.SendPackets("%xt%zm%tryQuestComplete%30004%661%-1%false%wvz%");
        }

        //Bad Moon Rising
        Core.KillQuest(675, "orchestra", "Mozard");

        //Burning Down The House
        if (!Core.QuestProgression(676))
        {
            Core.EnsureAccept(676);
            Core.KillMonster("orchestra", "R4", "Down", "*", "Cannon Powder");
            Core.EnsureComplete(676);
        }

        //Superstition
        Core.KillQuest(677, "orchestra", "Mozard");

        //Soul Man
        Core.KillQuest(678, "orchestra", "Faust");

        //Great gig in the Sky
        Core.KillQuest(4827, "stairway", new[] { "Rock Lobster", "Grateful Undead" });


        //Mythsong War Cutscene
        Core.ChainQuest(707);

        //Pony Gary Yellow
        Core.KillQuest(709, "palooza", "Pony Gary Yellow");

        //Kimberly
        Core.KillQuest(710, "palooza", "Kimberly");
    }

    public void Ledgermayne()
    {

        if (Core.isCompletedBefore(847))
            return;

        //Observing the Observatory
        Core.MapItemQuest(805, "arcangrove", 139);
        //Ewa the Treekeeper
        Core.MapItemQuest(806, "cloister", 142);

        //Bear Necessities of LifeRoot
        Core.MapItemQuest(807, "cloister", 140);

        //Acorny Quest
        Core.KillQuest(808, "cloister", "Acornent");

        //Ravenloss
        Core.KillQuest(809, "cloister", "Karasu");

        //It's A Bough-t Time
        Core.BuyQuest(810, "arcangrove", 211, "Mana Potion");
        Core.MapItemQuest(810, "cloister", 141, 3);

        //Wendigo Whereabouts
        Core.KillQuest(811, "cloister", "Wendigo");

        //Find Paddy Lump
        Core.MapItemQuest(813, "mudluk", 143);

        //Toothy Smiles
        Core.KillQuest(814, "mudluk", "Swamp Lurker");

        //Slimy Cyrus
        Core.KillQuest(815, "mudluk", "Swamp Lurker");

        //Lord Of The Fleas
        Core.KillQuest(816, "arcangrove", "Gorillaphant");

        //Not The Best Idea
        Core.KillQuest(817, "mudluk", "Swamp Frogdrake");

        //Gates and Guardians
        Core.KillQuest(818, "mudluk", "Tiger Leech");

        //Water You Waiting For--Find Nisse
        Core.MapItemQuest(825, "natatorium", 144);

        //Dive Right In
        Core.MapItemQuest(826, "natatorium", 145, 12);

        //Seafood Diet
        Core.KillQuest(827, "natatorium", "Anglerfish");

        //Mercenaries
        Core.KillQuest(828, "natatorium", "Merdraconian");

        //Synchronized Slaying
        if (!Core.QuestProgression(829))
        {
            Core.EnsureAccept(829);
            Core.HuntMonster("arcangrove", "Seed Spitter", "Brain Coral", 5);
            Core.HuntMonster("cloister", "Acornent", "Staghorns", 4);
            Core.HuntMonster("cloister", "Karasu", "Sea Fan", 3);
            Core.HuntMonster("cloister", "Wendigo", "Sea Whip");
            Core.HuntMonster("mudluk", "Swamp Frogdrake|Swamp Lurker", "Anemones", 6);
            Core.EnsureComplete(829);
        }

        //The Deep End
        Core.KillQuest(830, "natatorium", "Nessie");

        //Find Umbra, the Master Shaman
        Core.MapItemQuest(831, "gilead", 146);

        //The Root of Elementals
        if (!Core.QuestProgression(832))
        {
            Core.EnsureAccept(832);
            Core.HuntMonster("gilead", "Earth Elemental", "Dregs Essence", 5);
            Core.HuntMonster("arcangrove", "Seed Spitter", "Spitter Spirit", 4);
            Core.EnsureComplete(832);
        }

        //Eupotamic Elementals
        if (!Core.QuestProgression(833))
        {
            Core.EnsureAccept(833);
            Core.HuntMonster("gilead", "Water Elemental", "Aqueous Essence", 5);
            Core.HuntMonster("natatorium", "Merdraconian", "MerCore", 6);
            Core.EnsureComplete(833);
        }

        //Breaking Wind Elementals
        if (!Core.QuestProgression(834))
        {
            Core.EnsureAccept(834);
            Core.HuntMonster("gilead", "Wind Elemental", "Welkin Essence", 5);
            Core.HuntMonster("cloister", "Karasu", "Karasu Soul", 8);
            Core.EnsureComplete(834);
        }

        //Fight Fire With Fire Salamanders
        if (!Core.QuestProgression(835))
        {
            Core.EnsureAccept(835);
            Core.HuntMonster("gilead", "Fire Elemental", "Pyre Essence", 5);
            Core.HuntMonster("mudluk", "Swamp Frogdrake", "Fire Salamander", 5);
            Core.EnsureComplete(835);
        }

        //Guardian of the Gilead Wrap
        Core.KillQuest(836, "gilead", "Mana Elemental");

        //Find Felsic the Magma Golem
        Core.MapItemQuest(838, "mafic", 147);

        //Liquid Hot Magma Maggots
        Core.KillQuest(839, "mafic", "Volcanic Maggot");

        //Scorched Serpents
        Core.KillQuest(840, "mafic", "Scoria Serpent");

        //Playing With Living Fire
        Core.KillQuest(841, "mafic", "Living Fire");

        //Kindling Relationship
        Core.KillQuest(842, "mafic", "Mafic Dragon");

        //Obey Your Thirst for Adventure
        Core.KillQuest(843, "elemental", "Mana Imp");

        //Captain Falcons
        Core.KillQuest(844, "elemental", "Mana Falcon");

        //Big, bad, and Baddest Bosses
        if (!Core.QuestProgression(845))
        {
            Core.EnsureAccept(845);
            Core.HuntMonster("mafic", "Mafic Dragon", "Astral Orb of Mafic");
            Core.HuntMonster("cloister", "Wendigo", "Astral Orb of the Cloister");
            Core.HuntMonster("mudluk", "Tiger Leech", "Astral Orb of Mudluk");
            Core.HuntMonster("natatorium", "Nessie", "Astral Orb of Natatorium");
            Core.HuntMonster("gilead", "Mana Elemental", "Astral Orb of Gilead");
            Core.EnsureComplete(845);
        }
        //The Great Mana Golem
        Core.KillQuest(846, "elemental", "Mana Golem");
        //Chaos Lord Ledgermayne
        Core.KillQuest(847, "ledgermayne", "Ledgermayne");
    }


    public void Tibicenas()
    {

        if (Core.isCompletedBefore(1005))
            return;

        //Sandport and Starboard
        Core.MapItemQuest(930, "sandport", 251);

        //Shark Diving
        Core.KillQuest(931, "sandport", "Sandshark");

        //Thieving Cut Throats
        Core.KillQuest(932, "sandport", "Tomb Robber");

        //Lost and Found
        Core.KillQuest(933, "sandport", "Tomb Robber");

        //Sell-Sword Sell-Outs
        if (!Core.QuestProgression(934))
        {
            Core.EnsureAccept(934);
            Core.Join("sandport", "r6", "Right");
            Bot.Player.Kill("*");
            Core.Jump("r5", "Right");
            Bot.Player.Kill("*");
            Bot.Player.Kill("*");
            Bot.Player.Kill("*");
            if (Bot.Quest.CanComplete(934))
                Core.EnsureComplete(934);
        }

        //Sacred Scarabs
        Core.KillQuest(967, "pyramid", "Golden Scarab");

        //A Noob is Guard
        Core.KillQuest(968, "pyramid", "Anubis Deathguard");

        //Bandaged Aids
        Core.KillQuest(969, "pyramid", "Mummy");

        //Keys to the Royal Chamber
        Core.KillQuest(970, "pyramid", "Golden Scarab");

        //Confront Duat
        Core.MapItemQuest(971, "pyramid", 304);

        //They've Gone Dark
        Core.KillQuest(972, "wanders", "Kalestri Worshiper");

        //Bad Doggies
        Core.KillQuest(973, "wanders", "Kalestri Hound");

        //Essentially Evil
        Core.KillQuest(974, "wanders", "Kalestri Hound");

        //Loose Threads
        Core.KillQuest(975, "wanders", "Lotus Spider");

        //Seek The Treasure
        Core.MapItemQuest(976, "wanders", 306);

        //Dreamsand
        Core.KillQuest(977, "wanders", "Lotus Spider");

        //I Dream Of...
        if (!Core.QuestProgression(978))
        {
            Core.EnsureAccept(978);
            Core.Join("wanders", "Boss", "Left");
            Bot.Sleep(1500);
            Bot.Player.UseSkill(0);
            Core.KillMonster("wanders", "Boss", "Left", "Sek-Duat", "Sek-Duat Defeated");
            Core.EnsureComplete(978);
        }

        //Sandsational Castle
        Core.MapItemQuest(995, "sandcastle", 361);

        //Furry Fury
        Core.KillQuest(996, "sandcastle", "War Hyena");

        //Keeping Secrets Under Wraps
        Core.KillQuest(997, "sandcastle", "War Mummy");

        //Gem Jam
        Core.KillQuest(998, "sandcastle", "War Hyena");

        //Enter the Sphinx
        Core.KillQuest(999, "sandcastle", "Chaos Sphinx");

        //Unlamented Lamia
        Core.KillQuest(1000, "djinn", "Lamia");

        //E-vase-ive Measures
        Core.KillQuest(1001, "sandsea", "Desert Vase");

        //Tri-hump-hant Camels
        Core.KillQuest(1002, "sandsea", "Bupers Camel");

        //I Don't Mean to Harp On It...
        Core.KillQuest(1003, "djinn", "Harpy");

        //In-djinn-ious Solution
        Core.MapItemQuest(1004, "djinn", 370, 5);

        //Chaos Lord Tibicenas
        Core.KillQuest(1005, "djinn", "Tibicenas");

    }

    public void KhasaandaHorc(bool bypassCheck = false)
    {
        if (!bypassCheck)
        {
            if (Core.isCompletedBefore(1473))
                return;
        }



        //Troll Stink!
        if (!Core.QuestProgression(1232))
        {
            Core.EnsureAccept(1232);
            Core.HuntMonster("crossroads", "Chinchilizard", "Scaly Skin Scrub", 7);
            Core.HuntMonster("bloodtusk", "Trollola Plant", "Perfumed Trollola Flower", 10);
            Core.MapItemQuest(1232, "bloodtusk", 523);
        }

        //It Not Time Yet
        if (!Core.QuestProgression(1233))
        {
            Core.EnsureAccept(1233);
            Core.HuntMonster("crossroads", "Lemurphant", "Lemurphant Stones", 5);
            Core.HuntMonster("crossroads", "Koalion", "Golden Down-fur", 5);
            Core.EnsureComplete(1233);
        }

        //Mountain Protection
        if (!Core.QuestProgression(1234))
        {
            Core.EnsureAccept(1234);
            Core.HuntMonster("bloodtusk", "Crystal-Rock", "Polished Rocks", 3);
            Core.HuntMonster("crossroads", "Lemurphant", "Lemurphant Ivory", 5);
            Core.HuntMonster("crossroads", "Chinchilizard", "Liz-Leather Thongs", 5);
            Core.MapItemQuest(1234, "crossroads", 525);
        }

        //Clear Mind, Cleanse Spirit
        if (!Core.QuestProgression(1235))
        {
            Core.EnsureAccept(1235);
            Core.HuntMonster("bloodtusk", "Trollola Plant", "Trollola Plant Resin", 4);
            Core.HuntMonster("crossroads", "Lemurphant", "Lemurphant Musk", 5);
            Core.HuntMonster("crossroads", "Koalion", "Fur for Firestarting", 5);
            Core.MapItemQuest(1235, "crossroads", 521, 10);
        }

        //She Who Asks
        Core.ChainQuest(1236);

        //Be Horc Inside
        if (!Core.QuestProgression(1237))
        {
            Core.EnsureAccept(1237);
            Core.HuntMonster("crossroads", "Koalion", "Koalion Claw", 5);
            Core.HuntMonster("crossroads", "Koalion", "Skin of the Mountain", 10);
            Core.HuntMonster("crossroads", "Lemurphant", "Lemurphant Tusks", 5);
            Core.MapItemQuest(1237, "crossroads", 524, 5);
            Core.MapItemQuest(1237, "crossroads", 522, 10);
            Core.EnsureComplete(1237);
        }

        //She Who Answers 2 - cutscene
        //if(!Core.QuestProgression(1241))
        //{
        Core.ChainQuest(1241);
        //}

        //Chaos Enrages the Horcs
        // if(!Core.QuestProgression(1273))
        //{
        Core.ChainQuest(1273);
        //}

        //Into, Under the Mountain
        Core.MapItemQuest(1280, "ravinetemple", 553);

        //Has the Land Been Tainted?
        Core.MapItemQuest(1281, "ravinetemple", 554, 5);
        Core.MapItemQuest(1281, "ravinetemple", 555, 10);
        Core.MapItemQuest(1281, "ravinetemple", 556, 10);

        //Tears of the Mountain
        Core.KillQuest(1282, "ravinetemple", "*");

        //Defend the UnderMountain
        Core.KillQuest(1283, "ravinetemple", "*");
        Core.MapItemQuest(1283, "ravinetemple", 557, 10);

        //Alliance Defiance
        Core.KillQuest(1284, "ravinetemple", "*");

        //Scout and Return
        Core.MapItemQuest(1375, "alliance", 679);
        Core.MapItemQuest(1375, "alliance", 680);

        //Good and Evil Not Always Right
        if (!Core.QuestProgression(1376))
        {
            Core.EnsureAccept(1376);
            Core.HuntMonster("alliance", "Good Soldier", "Good Soldier Vanquished", 10);
            Core.HuntMonster("alliance", "Evil Soldier", "Evil Soldier Vanquished", 10);
            Core.EnsureComplete(1376);
        }

        //Trapping Savage Soldiers
        Core.MapItemQuest(1377, "alliance", 675, 10);

        //Find What is Hidden Inside
        Core.MapItemQuest(1378, "alliance", 676);

        //Chaorruption Rejection
        Core.KillQuest(1379, "alliance", "Chaorrupted Evil Soldier|Chaorrupted Good Soldier");

        //Alliance Subdued
        //Core.KillQuest(1380, "alliance", new[] { "General Cynari", "General Tibias" });
        if (!Core.QuestProgression(1380))
        {
            Core.EnsureAccept(1380);
            Core.HuntMonster("alliance", "General Cynari", "Cynari Defeated!");
            Core.HuntMonster("alliance", "General Tibias", "Tibias Defeated!");
            Core.EnsureComplete(1380);
        }

        //Cleanse the Chaorruption
        Core.KillQuest(1424, "ancienttemple", "Chaotic Vulture");

        //Chaorruption Cure?
        Core.KillQuest(1425, "ancienttemple", "Chaotic Vulture");
        Core.MapItemQuest(1425, "ancienttemple", 706, 7);

        //Guardian Salvation
        Core.KillQuest(1426, "ancienttemple", "Chaos Troll Spirit");

        //Poison for a Purpose
        Core.KillQuest(1427, "ancienttemple", "Serpentress");

        //The Heart of the Temple Awaits
        Core.MapItemQuest(1428, "ancienttemple", 707);

        //Wounds in Stones and Beasts
        Core.MapItemQuest(1456, "orecavern", 717);

        //Light in Underhome
        Core.KillQuest(1457, "orecavern", "Crashroom");
        Core.MapItemQuest(1457, "orecavern", 719, 5);

        //Truth is its Own Light
        Core.MapItemQuest(1458, "orecavern", 718, 5);

        //Horcs Know Mercy
        Core.KillQuest(1459, "orecavern", "Chaorrupted Evil Soldier");

        //Battle the Baas!
        Core.KillQuest(1460, "orecavern", "Naga Baas");

        //Know the Nexus
        Core.MapItemQuest(1469, "dreamnexus", 734);
        Core.MapItemQuest(1469, "dreamnexus", 735);
        Core.MapItemQuest(1469, "dreamnexus", 736);
        Core.MapItemQuest(1469, "dreamnexus", 737);

        //Secure a Route Home
        if (!Core.QuestProgression(1470))
        {
            Core.EnsureAccept(1470);
            Core.HuntMonster("dreamnexus", "Dark Wyvern", "Wyvern Scales", 5);
            Core.HuntMonster("dreamnexus", "Dark Wyvern", "Wyvern Claws", 5);
            Core.HuntMonster("dreamnexus", "Aether Serpent", "Serpent Fangs", 5);
            Core.HuntMonster("dreamnexus", "Aether Serpent", "Serpent Hair", 5);
            Core.EnsureComplete(1470);
        }

        //DreamDancers' Orbs
        Core.MapItemQuest(1471, "dreamnexus", 738, 10);
        Core.MapItemQuest(1471, "dreamnexus", 739, 11);

        //Master the Flames 
        Core.KillQuest(1472, "dreamnexus", new[] { "Solar Phoenix", "Solar Phoenix" });
        // if (!Core.QuestProgression(1472))
        // {
        //     Core.EnsureAccept(1472);
        // Core.HuntMonster("dreamnexus", "Solar Phoenix", "Phoenix Tear", 5);
        // Core.HuntMonster("dreamnexus", "Solar Phoenix", "Phoenix Blood", 5);
        //     Core.EnsureComplete(1472);
        // }

        //Choose: Khasaanda Confrontation?
        Core.KillQuest(1473, "dreamnexus", "Khasaanda");

    }

    public void KhasaandaTroll(bool bypassCheck = false)
    {
        if (Core.isCompletedBefore(1468))
            return;

        //Horc Stink! 
        if (!Core.QuestProgression(1226))
        {
            Core.EnsureAccept(1226);
            Core.HuntMonster("crossroads", "Chinchilizard", "Scaly Skin Scrub", 7);
            Core.HuntMonster("bloodtusk", "Trollola Plant", "Perfumed Trollola Flower", 10);
            Core.MapItemQuest(1226, "bloodtusk", 523);
        }

        //The Time Grows Closer
        Core.KillQuest(1227, "crossroads", new[] { "Koalion", "Lemurphant" });

        //Like Calls to Like
        if (!Core.QuestProgression(1228))
        {
            Core.EnsureAccept(1228);
            Core.HuntMonster("bloodtusk", "Crystal-Rock", "Mountain Crystal", 3);
            Core.HuntMonster("crossroads", "Chinchilizard", "Liz-Leather Thongs", 5);
            Core.HuntMonster("crossroads", "Lemurphant", "Lemurphant Ivory", 5);
            Core.MapItemQuest(1228, "crossroads", 525);
        }

        //Incense Makes Sense
        if (!Core.QuestProgression(1229))
        {
            Core.EnsureAccept(1229);
            Core.HuntMonster("crossroads", "Lemurphant", "Lemurphant Musk", 5);
            Core.HuntMonster("bloodtusk", "Trollola Plant", "Trollola Plant Resin", 4);
            Core.HuntMonster("crossroads", "Koalion", "Fur for Firestarting", 5);
            Core.MapItemQuest(1229, "crossroads", 521, 10);
        }

        //She Who asks 1
        if (!Core.QuestProgression(1230))
        {
            Core.ChainQuest(1230);
        }

        //The Troll Inside
        if (!Core.QuestProgression(1231))
        {
            Core.EnsureAccept(1231);
            Core.HuntMonster("crossroads", "Lemurphant", "Lemurphant Tusks", 5);
            Core.HuntMonster("crossroads", "Koalion", "Koalion Claw", 5);
            Core.HuntMonster("bloodtusk", "Crystal-Rock", "Singing Crystals", 10);
            Core.MapItemQuest(1231, "crossroads", 522, 5);
            Core.MapItemQuest(1231, "crossroads", 524, 10);
        }

        //Eclipse - cutscene
        if (!Core.QuestProgression(1240))
        {
            Core.EnsureAccept(1240);
            Core.Join("crossroads");
            Bot.Sleep(2000);
            Core.Join("crossroads");
            Core.Jump("CutE", "Left");
            Bot.Sleep(2000);
            Core.EnsureComplete(1240);
        }

        //Chaos scars the Trolls

        Core.ChainQuest(1272);

        //Guarded Secrets, Hidden Treasures
        Core.MapItemQuest(1274, "ravinetemple", 553);

        //Evidence of Chaos
        Core.MapItemQuest(1275, "ravinetemple", 554, 5);
        Core.MapItemQuest(1275, "ravinetemple", 555, 10);
        Core.MapItemQuest(1275, "ravinetemple", 556, 10);

        //Learn More of the Ore
        Core.KillQuest(1276, "ravinetemple", "*");

        //Too Little, Too Late. Still Needed
        Core.KillQuest(1277, "ravinetemple", "*");
        Core.MapItemQuest(1277, "ravinetemple", 557, 10);
        Core.MapItemQuest(1277, "ravinetemple", 557, 10);

        //Alliance Defiance
        Core.KillQuest(1278, "ravinetemple", "*");

        //The Headquartes of Good and Evil
        Core.MapItemQuest(1369, "alliance", 679);
        Core.MapItemQuest(1369, "alliance", 680);

        //Treat Nullification, Good and Bad
        Core.KillQuest(1370, "alliance", new[] { "Good Soldier", "Evil Soldier" });

        //Trap the Keepers
        Core.MapItemQuest(1371, "alliance", 675, 10);

        //Find What is Hidden Inside
        Core.MapItemQuest(1372, "alliance", 676);

        //Chaorruption Annihilation
        Core.KillQuest(1373, "alliance", "Chaorrupted Evil Soldier|Chaorrupted Good Soldier");

        //Alliance Demotion
        Core.KillQuest(1374, "alliance", new[] { "General Cynari", "General Tibias" });

        //Contain the Chaorruption
        Core.KillQuest(1419, "ancienttemple", "Chaotic Vulture|Chaotic Horcboar");

        //Ancient Ointment
        Core.KillQuest(1420, "ancienttemple", "Chaotic Vulture|Chaotic Horcboar");
        Core.MapItemQuest(1420, "ancienttemple", 706, 7);

        //Anoint the Ancients
        Core.KillQuest(1421, "ancienttemple", "Chaos Troll Spirit|Chaos Horc Spirit");

        //Serpents Do No Harm
        Core.KillQuest(1422, "ancienttemple", "Serpentress");

        //Though Nature Bars the Way
        Core.MapItemQuest(1423, "ancienttemple", 707);

        //Descent Into Darkness
        Core.MapItemQuest(1451, "orecavern", 717);

        //Out of the Darkness
        Core.KillQuest(1452, "orecavern", "Crashroom");
        Core.MapItemQuest(1452, "orecavern", 719, 5);

        //Shine a Light on Deception
        Core.MapItemQuest(1453, "orecaveern", 718, 5);

        //Save Yourself, Save the Soldiers
        Core.KillQuest(1454, "orecavern", "Chaorrupted Evil Soldier|Chaorrupted Good Soldier");

        //Battle the Baas!
        Core.KillQuest(1455, "orecavern", "Naga Baas");

        //Know the Nexus
        Core.MapItemQuest(1464, "dreamnexus", 734);
        Core.MapItemQuest(1464, "dreamnexus", 735);
        Core.MapItemQuest(1464, "dreamnexus", 736);
        Core.MapItemQuest(1464, "dreamnexus", 737);

        //Secure a Route Home
        if (!Core.QuestProgression(1465))
        {
            Core.EnsureAccept(1465);
            Core.HuntMonster("dreamnexus", "Dark Wyvern", "Wyvern Scales", 2);
            Core.HuntMonster("dreamnexus", "Dark Wyvern", "Wyvern Claws", 2);
            Core.HuntMonster("dreamnexus", "Aether Serpent", "Serpent Fangs", 2);
            Core.HuntMonster("dreamnexus", "Aether Serpent", "Serpent Hair", 2);
            Core.EnsureComplete(1465);
        }

        //DreamDancers' Orbs
        Core.MapItemQuest(1466, "dreamnexus", 738, 10);
        Core.MapItemQuest(1466, "dreamnexus", 739, 11);

        //Master the Flames
        Core.KillQuest(1467, "dreamnexus", new[] { "Solar Phoenix", "Solar Phoenix" });

        //Choose: Khasaanda Confrontation?
        Core.KillQuest(1468, "dreamnexus", "Khasaanda");
    }

    public void Iadoa()
    {

        if (Core.isCompletedBefore(2519))
            return;

        //Time to Learn the Truth
        Core.MapItemQuest(2239, "thespan", 1358);
        Core.MapItemQuest(2239, "thespan", 1359);
        Core.MapItemQuest(2239, "thespan", 1360);
        Core.MapItemQuest(2239, "thespan", 1361);
        Core.MapItemQuest(2239, "thespan", 1362);
        Core.MapItemQuest(2239, "thespan", 1363);

        //Gain Access to Doors
        Core.KillQuest(2240, "timelibrary", new[] { "Sneak", "Tog", "Shadowscythe" });

        //Adventures and Quests
        if (!Core.QuestProgression(2241))
        {
            Core.EnsureAccept(2241);
            Core.HuntMonster("timelibrary", "Moglin Ghost", "Necromancy in AQ Knowledge");
            Core.HuntMonster("timelibrary", "Undead Knight", "Tales of Planet-Peril Page", 6);
            Core.MapItemQuest(2241, "timelibrary", 1365, 3);
        }

        //A Fable of Dragons
        if (!Core.QuestProgression(2242))
        {
            Core.EnsureAccept(2242);
            Core.HuntMonster("timelibrary", "Tog", "Elemental Orb Knowledge", 8);
            Core.HuntMonster("timelibrary", "Tog", "\"Draconic Prophecy\" Page", 6);
            Core.MapItemQuest(2242, "timelibrary", 1366, 2);
        }

        //Mechas and Quests
        if (!Core.QuestProgression(2243))
        {
            Core.EnsureAccept(2243);
            Core.HuntMonster("timelibrary", "Shadowscythe", "Shadowscythe Combat Strategy", 7);
            Core.HuntMonster("timelibrary", "Training Globe", "Galactic Hypertron Engines: A Primer");
            Core.MapItemQuest(2243, "timelibrary", 1367);
        }

        //After the Chaos
        Core.KillQuest(2244, "timelibrary", new[] { "Queen's Lieutenant", "Queen's Recruit", "Queen's Knight" });
        if (!Core.QuestProgression(2244))
        {
            Core.EnsureAccept(2244);
            Core.HuntMonster("timelibrary", "Queen's Knight", "Princess Freed… but for what?", 2);
            Core.HuntMonster("timelibrary", "Queen's Knight", "The Future is Now", 3);
            Core.HuntMonster("timelibrary", "Queen's Knight", "Past Failures Brought Us Here", 2);
            Core.MapItemQuest(2244, "timelibrary", 1368);
        }

        //Trust is Not Ephemeral
        Core.KillQuest(2253, "timevoid", "Ephemerite");

        //In a Split Exasecond
        if (!Core.QuestProgression(2254))
        {
            Core.EnsureAccept(2254);
            Core.HuntMonster("timevoid", "Time-Travel Fairy", "Exaglass", 4);
            Core.HuntMonster("timevoid", "Time-Travel Fairy", "Fairy Plasma", 8);
            Core.MapItemQuest(2254, "timevoid", 1438, 16);
        }

        //Time to Prove Yourself
        Core.KillQuest(2255, "timevoid", new[] { "Time-Travel Fairy", "Ephemerite" });
        Core.MapItemQuest(2255, "timevoid", 1439, 15);

        //Fill the Empty Hours
        Core.KillQuest(2256, "timevoid", new[] { "Void Phoenix", "Time-Travel Fairy" });

        //Clock of the Long Now
        Core.MapItemQuest(2257, "timevoid", 1440);
        Core.MapItemQuest(2257, "timevoid", 1441);
        Core.MapItemQuest(2257, "timevoid", 1442);
        Core.MapItemQuest(2257, "timevoid", 1443);

        //Unending Avatar
        if (!Core.QuestProgression(2258))
        {
            Core.EnsureAccept(2258);
            Core.HuntMonster("timevoid", "Unending Avatar", "Avatar Slain");
            Core.EnsureComplete(2258);
        }

        //Construct Your Reality
        Core.MapItemQuest(2376, "aqlesson", 1467);

        //Reach the Temple
        Core.KillQuest(2377, "aqlesson", "Ninja ");

        //Not All Hope is Lost
        Core.MapItemQuest(2378, "aqlesson", 1468, 8);
        Core.MapItemQuest(2378, "aqlesson", 1469);

        //Bolster the Elements
        if (!Core.QuestProgression(2379))
        {
            Core.EnsureAccept(2379);
            Core.HuntMonster("aqlesson", "Eternite Ore", "TimeSpark Acquired", 3);
            Core.HuntMonster("aqlesson", "Water Elemental", "Tears of Joy Acquired", 3);
            Core.MapItemQuest(2379, "aqlesson", 1470, 3);
            Core.MapItemQuest(2379, "aqlesson", 1471, 3);
        }

        //Maintain Elemental Strength
        Core.KillQuest(2380, "aqlesson", new[] { "Ice Elemental", "Fire Elemental" });
        Core.MapItemQuest(2380, "aqlesson", 1473, 3);
        Core.MapItemQuest(2380, "aqlesson", 1472, 3);

        //Rescue the Innocent
        Core.KillQuest(2381, "aqlesson", "Void Dragon");

        //Get Fired Up... or Shatter!
        Core.KillQuest(2382, "aqlesson", "Firezard");

        //Enemies on Ice
        Core.KillQuest(2383, "aqlesson", "Ice Elemental");

        //Tek-nical Forging Skill
        Core.MapItemQuest(2384, "thespan", 1474);

        //Akriloth Assault
        Core.KillQuest(2385, "aqlesson", "Akriloth");

        //Proto-Chaos Beast Battle!
        Core.KillQuest(2386, "aqlesson", "Carnax");

        //Elemental Orb Awareness
        Core.MapItemQuest(2470, "dflesson", 1549, 8);

        //Fight Chaos with Fire!
        if (!Core.QuestProgression(2471))
        {
            Core.EnsureAccept(2471);
            Core.HuntMonster("dflesson", "Fire Elemental", "Slain Flame Elemental", 8);
            Core.HuntMonster("dflesson", "Fire Elemental", "Chaos Gemerald", 4);
            Core.EnsureComplete(2471);
        }

        //Save Aria
        if (!Core.QuestProgression(2472))
        {
            Core.EnsureAccept(2472);
            Core.HuntMonster("dflesson", "Lava Slime", "Slain Slime", 5);
            Core.HuntMonster("dflesson", "Fire Elemental", "Slain Elemental", 6);
            Core.HuntMonster("dflesson", "Fire Elemental", "Blue Clue");
            Core.EnsureComplete(2472);
        }

        //Find the Time to Travel
        if (!Core.QuestProgression(2473))
        {
            Core.EnsureAccept(2473);
            Core.HuntMonster("dflesson", "Tog", "Life AND the Universe", 4);
            Core.HuntMonster("dflesson", "Agitated Orb", "Everything", 5);
            Core.HuntMonster("dflesson", "Agitated Orb", "Hoopy Frood's Towel", 3);
            Core.EnsureComplete(2473);
        }

        //Dragon Egg... or Junk?
        Core.KillQuest(2474, "dflesson", "Vultragon");

        //Dracolich Fortress Detected
        Core.KillQuest(2475, "dflesson", "Chaos Sp-Eye");

        //Bone up on the Boss
        Core.KillQuest(2476, "dflesson", "Chaorrupted Evil Soldier");

        //Defend the Town!
        Core.KillQuest(2477, "dflesson", new[] { "Lava Golem", "Fire Elemental" });

        //ChickenCows, Bacon, and Battle!
        Core.KillQuest(2478, "dflesson", new[] { "Chaotic Chicken", "Chaotic Horcboar" });

        //The 2nd Proto-Chaos Beast
        Core.KillQuest(2479, "dflesson", "Fluffy the Dracolich");

        //Board the Ship to Your Future
        Core.MapItemQuest(2504, "mqlesson", 1580);

        //Heal the Chaos Lord
        Core.KillQuest(2505, "mqlesson", "Asteroid");

        //Shadowscythe Detection Beacons
        Core.MapItemQuest(2506, "mqlesson", 1581, 5);

        //Test Potential Traitors
        Core.KillQuest(2507, "mqlesson", "MystRaven Student");

        //Defeat Training Globes
        Core.KillQuest(2508, "mqlesson", "Training Globe");

        //Take Flight into the Future
        Core.KillQuest(2509, "mqlesson", "MystRaven Student");

        //Secrets of the Universe
        Core.KillQuest(2510, "mqlesson", "Chaos Shadowscythe");

        //Mysterious!
        Core.KillQuest(2511, "mqlesson", new[] { "Chaos Shadowscythe", "Chaos Shadowscythe" });
        if (!Core.QuestProgression(2511))
        {
            Core.EnsureAccept(2511);
            Core.HuntMonster("mqlesson", "Chaos Shadowscythe", "Truth Glasses");
            Core.HuntMonster("mqlesson", "Chaos Shadowscythe", "Darkness Destroyed", 13);
            Core.EnsureComplete(2511);
        }

        //The 3rd Proto-Chaos Beast
        Core.KillQuest(2512, "mqlesson", "Dragonoid");

        //Chaos Waits and Watches
        Core.KillQuest(2513, "deepchaos", "Chaotic Merdrac");

        //The Lure of Chaosanity
        Core.KillQuest(2515, "deepchaos", "Chaos Angler");

        //Music of Nightmares
        Core.MapItemQuest(2516, "deepchaos", 1582, 3);

        //Chaos Beast Kathool
        Core.KillQuest(2517, "deepchaos", "Kathool");

        //Starry, Starry Night
        Core.KillQuest(2518, "timespace", "Astral Ephemerite");

        //Chaos Lord Iadoa
        Core.KillQuest(2519, "timespace", "Chaos Lord Iadoa");
    }

    public void Lionfang()
    {


        if (Core.isCompletedBefore(2814))
            return;

        //Final Rest
        Core.KillQuest(2612, "blackhorn", "Restless Undead");

        //Disturbing The Peace
        Core.MapItemQuest(2613, "blackhorn", 1616, 10);

        //Sampling Silk
        Core.KillQuest(2614, "blackhorn", "Tomb Spider");

        //Fire Is The Thing
        Core.KillQuest(2615, "blackhorn", new[] { "Tomb Spider", "Restless Undead" });

        //The Wall Comes Down
        Core.MapItemQuest(2616, "blackhorn", 1617);

        //The Bonefeeder
        Core.KillQuest(2617, "blackhorn", "Bonefeeder Spider");

        //What Lies Beyond?
        Core.MapItemQuest(2618, "blackhorn", 1618);

        //Toxic
        Core.KillQuest(2619, "blackhorn", "Tomb Spider");

        //Very Toxic
        Core.KillQuest(2620, "blackhorn", "Restless Undead");

        //Really, VERY VERY TOXIC!
        Core.MapItemQuest(2621, "blackhorn", 1619);

        //Lion Hunting
        Core.MapItemQuest(2622, "onslaughttower", 1620);
        Core.MapItemQuest(2622, "onslaughttower", 1621);
        Core.MapItemQuest(2622, "onslaughttower", 1622);
        Core.MapItemQuest(2622, "onslaughttower", 1623);

        //Secret Of The Death Fog
        Core.KillQuest(2623, "onslaughttower", "Golden Caster|Golden Warrior|Golden Cavalry");

        //The Key To Survival
        Core.KillQuest(2624, "onslaughttower", "Golden Caster|Golden Warrior|Golden Cavalry");

        //The Tools
        Core.MapItemQuest(2625, "onslaughttower", 1624, 8);

        //The Talent
        Core.MapItemQuest(2626, "onslaughttower", 1625);

        //The Local Locale
        Core.MapItemQuest(2627, "onslaughttower", 1626, 4);

        //Who Holds The Key?
        Core.KillQuest(2628, "onslaughttower", "Golden Caster|Golden Warrior|Golden Cavalry");

        //Leave No Rug Unturned
        Core.MapItemQuest(2629, "onslaughttower", 1627);

        //Tame The Lion
        Core.KillQuest(2630, "onslaughttower", "Maximillian Lionfang");

        //Take Up The Cause
        Core.KillQuest(2666, "falguard", "Chaonslaught Caster|Chaonslaught Warrior|Chaonslaught Cavalry");

        //Well Kept Secrets
        Core.MapItemQuest(2667, "falguard", 1628, 6);

        //Feeding On The Fallen
        Core.KillQuest(2668, "falguard", new[] { "Chaonslaught Warrior", "Chaonslaught Cavalry" });

        //Special Delivery
        Core.MapItemQuest(2669, "falguard", 1629);

        //Precious Scraps
        Core.KillQuest(2670, "falguard", "Chaonslaught Warrior|Chaonslaught Cavalry");

        //Restocking
        Core.MapItemQuest(2671, "falguard", 1630);

        //An Innside Job
        Core.MapItemQuest(2672, "falguard", 1631);

        //Streets Run Red
        Core.KillQuest(2673, "falguard", "Chaonslaught Caster");

        //Open the Temple
        Core.MapItemQuest(2674, "falguard", 1632);

        //The Open Temple
        Core.KillQuest(2675, "falguard", "Primarch");

        //Remains
        Core.KillQuest(2720, "deathpits", "Rotting Darkblood");

        //Thriving In Rot
        Core.MapItemQuest(2721, "deathpits", 1691, 5);

        //Rotting Ribs
        Core.KillQuest(2722, "deathpits", "Rotting Darkblood");

        //A Perfect Skull
        Core.KillQuest(2723, "deathpits", "Rotting Darkblood");

        //Deeper Into Death
        Core.KillQuest(2724, "deathpits", "Ghastly Darkblood");

        //Precise Placement
        Core.KillQuest(2725, "deathpits", "Ghastly Darkblood");

        //Painted Protection
        Core.MapItemQuest(2726, "deathpits", 1692, 6);

        //They Sense You
        Core.KillQuest(2727, "deathpits", "Rotting Darkblood");

        //They Hate You
        Core.KillQuest(2728, "deathpits", "Ghastly Darkblood");

        //The Sundered
        Core.KillQuest(2729, "deathpits", "Sundered Darkblood");

        //Rotstone
        Core.MapItemQuest(2730, "deathpits", 1693, 9);

        //Honor The Dead
        Core.KillQuest(2731, "deathpits", new[] { "Sundered Darkblood", "Ghastly Darkblood", "Rotting Darkblood" });

        //Ties to Life
        Core.MapItemQuest(2732, "deathpits", 1694, 12);

        //Destroy Wrathful Vestis and Secure The Tears
        Core.KillQuest(2740, "deathpits", "Wrathful Vestis");
        Core.MapItemQuest(2740, "deathpits", 1695, 1);

        //Surveillance for Sir Valence
        Core.MapItemQuest(2792, "venomvaults", 1724);

        //Well Planned Getaway
        Core.KillQuest(2793, "venomvaults", "Chaonslaught Warrior");

        //Secrets Of The Mad Prince
        Core.MapItemQuest(2794, "venomvaults", 1725);

        //Potion of Cleansing
        Core.MapItemQuest(2796, "venomvaults", 1726);

        //You've Been Noticed
        Core.KillQuest(2797, "venomvaults", "Chaonslaught Caster");

        //Thorny Situations
        Core.MapItemQuest(2798, "venomvaults", 1727, 5);

        //Other Ingredients
        Core.KillQuest(2799, "venomvaults", "Chaonslaught Caster");

        //Time For Supplies
        Core.KillQuest(2800, "venomvaults", "Chaonslaught Warrior");

        //Cooking Without Fire
        Core.KillQuest(2801, "venomvaults", "Chaonslaught Caster|Chaonslaught Warrior");

        //Introduction
        Core.MapItemQuest(2802, "venomvaults", 1728, 3);

        //Courtyard Key
        Core.KillQuest(2803, "venomvaults", "Chaonslaught Caster|Chaonslaught Warrior");

        //Take Out The Chaos Manticore!
        Core.KillQuest(2804, "venomvaults", "Manticore");

        //Shocking Footwear
        Core.MapItemQuest(2805, "stormtemple", 1729, 4);

        //New Shoes
        Core.KillQuest(2806, "stormtemple", "Chaonslaught Warrior");

        //Mouth Of The Lion
        Core.KillQuest(2807, "stormtemple", "Chaonslaught Caster|Chaonslaught Warrior|Chaonslaught Cavalry");

        //Storm the Storm Temple
        Core.KillQuest(2808, "stormtemple", "Chaonslaught Caster|Chaonslaught Warrior|Chaonslaught Cavalry");

        //A High Minded Matter
        Core.MapItemQuest(2809, "stormtemple", 1730, 3);

        //Storm Bottles
        Core.KillQuest(2810, "stormtemple", "Chaonslaught Caster");

        //Breaching Defenses
        Core.MapItemQuest(2811, "stormtemple", 1731);

        //Chaos Lightning Rods
        Core.KillQuest(2812, "MastormtemplepName", "Chaonslaught Cavalry");

        //Barrier Buster
        Core.MapItemQuest(2813, "stormtemple", 1732);

        //Face Chaos Lord Lionfang!
        Core.KillQuest(2814, "stormtemple", "Chaos Lord Lionfang");
    }

    public void Xiang()
    {
        if (Core.isCompletedBefore(3189))
            return;

        Core.AddDrop("Perfect Prism", "Unchaorrupted Sample", "Harpy Feather");

        //Bright Idea
        Core.MapItemQuest(2909, "battleoff", 1779);

        //Spare Parts
        Core.MapItemQuest(2910, "battleoff", 1780, 8);

        //Power It Uo
        Core.KillQuest(2911, "battleoff", "Evil Moglin");

        //Filthy Creatures
        Core.KillQuest(2912, "battleoff", "Evil Moglin");

        //Wave After Wave
        Core.KillQuest(2913, "brightfall", "Undead Minion");

        //Take out Their Firepower
        Core.KillQuest(2914, "brightfall", "Undead Mage");

        //Help Where It is Needed
        Core.MapItemQuest(2915, "brightfall", 1781, 6);

        //Bring A Ward To A Swordfight
        Core.MapItemQuest(2916, "brightfall", 1782, 8);

        //Cut Off The Head
        Core.KillQuest(2917, "brightfall", "Painadin Overlord");

        //Rearm The Legion of Light
        Core.Join("overworld");
        Core.ChainQuest(2918);

        //Speak to Dage The Good
        Core.KillQuest(2919, "overworld", "Undead Minion");

        //Free Their Souls
        Core.KillQuest(2920, "overworld", "Undead Minion");

        //One Ring
        Core.KillQuest(2921, "overworld", "Undead Minion");

        //Severing Ties
        Core.KillQuest(2922, "overworld", "Undead Mage");

        //Legion's Lifesblood
        Core.MapItemQuest(2923, "overworld", 1800, 6);

        //Legion's Purpose
        Core.KillQuest(2924, "overworld", "Undead Bruiser");

        //What's His Endgame
        Core.KillQuest(2925, "overworld", "Undead Bruiser");

        //A Stopping Block
        Core.KillQuest(2926, "overworld", "Undead Minion|Undead Mage|Undead Bruiser");

        //Boost Morale
        Core.MapItemQuest(2927, "overworld", 1801, 8);

        //Alteon's Folly
        Core.KillQuest(2928, "overworld", "Undead Minion|Undead Mage|Undead Bruiser");

        //DoomFire
        Core.KillQuest(2929, "overworld", "Undead Minion|Undead Mage|Undead Bruiser");

        //Spoiled Souls
        Core.KillQuest(2930, "overworld", "Undead Minion|Undead Mage|Undead Bruiser");

        //Purity of Bone
        Core.MapItemQuest(2931, "overworld", 1802, 10);

        //Undead Artix Returns!
        Core.KillQuest(2932, "overworld", "Undead Artix");

        //I Can't Touch This
        Core.KillQuest(3166, "reddeath", "Fire Leech|Grim Widow|Reddeath Moglin|Swamp Wraith");

        //Nope, Still a Ghost
        Core.KillQuest(3167, "reddeath", "Reddeath Moglin");
        Core.MapItemQuest(3167, "reddeath", 2178);
        Core.MapItemQuest(3167, "reddeath", 2179);

        //First We Need a Beacon...
        Core.MapItemQuest(3168, "reddeath", 2180);

        //Light It Up
        Core.KillQuest(3169, "reddeath", "Fire Leech");

        //...Next We need a Trap
        Core.KillQuest(3170, "reddeath", "Grim Widow");

        //For Spirits, Not People
        Core.KillQuest(3171, "reddeath", "Swamp Wraith");

        //Still To Fragile
        Core.KillQuest(3172, "reddeath", "Swamp Wraith");

        //Craft a Better Defense
        Core.MapItemQuest(3183, "battleontown", 2203);

        //Reflect the Damage
        Core.KillQuest(3184, "earthstorm", "Shard Spinner");
        // while (!Core.CheckInventory("Perfect Prism") | !Core.QuestProgression(3184))
        // {
        //     Core.EnsureAccept(3184);
        //     Core.HuntMonster("earthstorm", "Shard Spinner", "Reflective Fragment", 5);
        //     Core.EnsureComplete(3184);
        //     Bot.Wait.ForDrop("Perfect Prism");
        // }

        //Pure Chaos
        Core.KillQuest(3185, "bloodtuskwar", "Chaotic Horcboar");
        // while (!Core.CheckInventory("Unchaorrupted Sample") | !Core.QuestProgression(3185))
        // {
        //     Core.EnsureAccept(3185);
        //     Core.HuntMonster("bloodtuskwar", "Chaotic Horcboar", "Vials of Blood", 5);
        //     Core.EnsureComplete(3185);
        //     Bot.Wait.ForDrop("Unchaorrupted Sample");
        // }

        //Enemies of a Feather Flock Together
        Core.KillQuest(3186, "bloodtuskwar", "Chaos Tigriff");
        // while (!Core.CheckInventory("Harpy Feather") | !Core.QuestProgression(3186))
        // {
        //     Core.EnsureAccept(3186);
        //     Core.HuntMonster("bloodtuskwar", "Chaos Tigriff", "Feathers", 5);
        //     Core.EnsureComplete(3186);
        //     Bot.Wait.ForDrop("Harpy Feather");
        // }

        //Ward Off the Beast
        Core.Join("mirrorportal");
        Core.ChainQuest(3187);

        //Horror Takes Flight
        Core.KillQuest(3188, "mirrorportal", "Chaos Harpy");

        //Good, Evil and Chaos Battle!]
        if (!Core.QuestProgression(3189))
        {
            if (Core.CheckInventory("Void Highlord"))
            {
                Bot.Skills.StartAdvanced("Void Highlord", true, RBot.Skills.ClassUseMode.Def);
                Core.KillQuest(3189, "mirrorportal", "Chaos Lord Xiang");
            }
            else
            if (Core.CheckInventory("Healer"))
            {
                Bot.Skills.StartAdvanced("Healer", true, RBot.Skills.ClassUseMode.Base);
                Core.KillQuest(3189, "mirrorportal", "Chaos Lord Xiang");
            }
            else
                Core.KillQuest(3189, "mirrorportal", "Chaos Lord Xiang");
        }
    }

    public void Alteon()
    {
        if (Core.isCompletedBefore(3160))
            return;

        //Bandit Bounty
        Core.KillQuest(3077, "archives", "Chaos Bandit");

        //Thwarting the Spies
        Core.KillQuest(3078, "archives", "Camouflaged Sp-Eye");

        //Fight Chaos With Clerics
        Core.KillQuest(3079, "archives", new[] { "Chaos Bandit", "Camouflaged Sp-Eye" });

        //Locate the Source
        Core.KillQuest(3080, "archives", "Chaos Bandit|Camouflaged Sp-Eye");
        Core.MapItemQuest(3080, "archives", 1937);

        //Plagued Rats
        Core.KillQuest(3081, "archives", "Chaos Rat");

        //Nope, Nope, Nope!
        Core.KillQuest(3082, "archives", "Chaos Spider");

        //Still More Research To Be Done!
        Core.KillQuest(3083, "archives", new[] { "Chaos Spider", "Chaos Rat" });

        //That's One Big Sludgebeast.
        Core.KillQuest(3084, "archives", "Sludgelord");

        //Back to Jail With You!
        Core.KillQuest(3094, "armory", "Chaorrupted Prisoner");

        //We May Need A Militia
        Core.KillQuest(3095, "armory", "Chaorrupted Prisoner");
        Core.MapItemQuest(3095, "armory", 1956, 4);

        //An Ounce Of Prevention
        Core.KillQuest(3096, "armory", "Chaos Drifter");

        //Axe Them To Leave! / Freeze 'Em Out! / Burn 'Em Up!
        Core.KillQuest(3096, "armory", "Chaorrupted Prisoner");

        //Freeze 'Em Out!
        Core.KillQuest(3090, "armory", "Chaos Mage");

        //Burn 'Em Up!
        Core.KillQuest(3091, "armory", "Chaos Mage");

        //Under Siege
        Core.MapItemQuest(3092, "armory", 1957);

        //No, NOW We're Under Siege
        Core.KillQuest(3093, "armory", "Chaos General");

        //Chaos Not Invited
        Core.KillQuest(3120, "ceremony", "Chaos Invader");

        //Better Letter Go!
        Core.MapItemQuest(3121, "yulgar", 2108);
        Core.MapItemQuest(3121, "yulgar", 2109);
        Core.MapItemQuest(3121, "yulgar", 2110);
        Core.MapItemQuest(3121, "archives", 2111);
        Core.MapItemQuest(3121, "swordhaven", 2112);
        Core.MapItemQuest(3121, "swordhaven", 2113);
        Core.MapItemQuest(3121, "swordhaven", 2114);
        Core.MapItemQuest(3121, "swordhaven", 2115);

        //Decor Rater
        Core.MapItemQuest(3122, "swordhaven", 2116, 8);

        //Cold Feet, Warm Heart
        Core.KillQuest(3123, "mafic", "Living Fire");

        //Chaos STILL Not Invited
        Core.KillQuest(3124, "ceremony", "Chaos Invader");

        //Protect the Princesses
        Core.MapItemQuest(3125, "ceremony", 2118, 6);

        //Seal the Chapel
        Core.MapItemQuest(3126, "ceremony", 2119);
        Core.KillQuest(3126, "ceremony", "Chaos Invader");

        //Chaos Kills!
        Core.KillQuest(3127, "ceremony", "Chaos Justicar");

        //Endless Aisle of Chaos
        Core.MapItemQuest(3133, "chaosaltar", 2127, 12);

        //Save the Princess... Again!
        Core.KillQuest(3134, "chaosaltar", "Princess Thrall");

        //Chaos Dragon Confrontation
        Core.KillQuest(3158, "castleroof", "Chaos Dragon");

        //To Catch a King
        Core.MapItemQuest(3159, "swordhavenfalls", 2158);

        //Chaos Lord Alteon
        Core.KillQuest(3160, "swordhavenfalls", "Chaos Lord Alteon");
    }

    public void Hero()
    {
        Core.BuyItem(Bot.Map.Name, 993, "Lore's Champion Daggers");
        if (Core.CheckInventory("Lore's Champion Daggers", toInv: false))
        {
            Bot.Sleep(Core.ActionDelay);
            Core.ToBank("Lore's Champion Daggers");
            Core.Logger($"Chapter: \"Chaos Lord {Bot.Player.Username}\" already complete. Skipping");
            return;
        }

        if (!Core.IsMember)
        {
            Prologue();
            Escherion();
            Vath();
            Kitsune();
            Wolfwing();
            Kimberly();
            Ledgermayne();
            Tibicenas();
            KhasaandaHorc();
            Iadoa();
            Lionfang();
            Xiang();
            Alteon();
        }

        //12 Lords of Chaos
        Core.ChainQuest(3578);

        // Prologue: Good vs Evil
        Core.ChainQuest(3579);

        // 1st Lord of Chaos
        Core.ChainQuest(3580);

        // 3rd Lord of Chaos
        Core.ChainQuest(3581);

        // 4th Lord of Chaos
        Core.ChainQuest(3582);

        // 5th Lord of Chaos
        Core.ChainQuest(3583);

        // 6th Lord of Chaos
        Core.ChainQuest(3584);

        // 7th Lord of Chaos
        Core.ChainQuest(3585);

        // 8th Lord of Chaos
        Core.ChainQuest(3586);

        // 9th Lord of Chaos
        Core.ChainQuest(3587);

        // 10th Lord of Chaos
        Core.ChainQuest(3588);

        // 11th Lord of Chaos
        Core.ChainQuest(3589);

        // 2nd Lord of Chaos
        Core.ChainQuest(3590);

        // 12th Lord of Chaos
        Core.ChainQuest(3591);

        // Mountain Top Reached
        Core.ChainQuest(3764);

        // Drakath Faced
        Core.MapItemQuest(3765, "mountdoomskull", 2726);

        // Who is the 13th Lord of Chaos?
        Core.ChainQuest(3766);

        // World War Lore!
        Core.ChainQuest(3779);

        // Battle for Chaos in Willowcreek!
        Core.KillQuest(3781, "newfinale", "Chaos Healer");

        // Defeat the Chaos Challenger
        Core.KillQuest(3788, "newfinale", "Chaos Challenger");

        // Battle for Chaos in Doomwood!
        Core.KillQuest(3783, "newfinale", "Chaotic Virago|Chaorrupted Healer|");

        // Beat Chaorrupted Lycan Hunter
        Core.KillQuest(3789, "newfinale", "Chaorrupted Lycan Hunter");

        // Battle for Chaos in Darkovia!
        Core.KillQuest(3785, "newfinale", "Shadow Slayer|Chaorrupted Healer|Mana Blaster");

        // Defeat the Memory of Vampires
        Core.KillQuest(3790, "newfinale", "Memory of Vampires");

        // Battle for Chaos in the Lair!
        Core.KillQuest(3787, "newfinale", "Chaotic Virago|Chaos Draconian|Chaos Crystal");

        // 1st Chaos Beast
        Core.ChainQuest(3608);

        // 2nd Chaos Beast
        Core.ChainQuest(3618);

        // 3rd Chaos Beast
        Core.ChainQuest(3609);

        // 4th Chaos Beast
        Core.ChainQuest(3610);

        // 5th Chaos Beast
        Core.ChainQuest(3611);

        // 6th Chaos Beast
        Core.ChainQuest(3612);

        // 7th Chaos Beast
        Core.ChainQuest(3613);

        // 8th Chaos Beast
        Core.ChainQuest(3614);

        // 9th Chaos Beast
        Core.ChainQuest(3615);

        // 10th Chaos Beast
        Core.ChainQuest(3616);

        // 11th Chaos Beast
        Core.ChainQuest(3617);

        // 12th Chaos Beast
        Core.ChainQuest(3619);

        //The Chaos Beast is called forth.
        Core.ChainQuest(3791);

        // Time to save Battleon!
        Core.ChainQuest(3792);

        // Battle for Chaos in Battleon!
        Core.KillQuest(3794, "newfinale", "Alliance Soldier");

        //Become a Chaos Lord
        Core.Join("newfinale", "NPC2", "Left");
        Core.SendPackets("%xt%zm%setAchievement%15075%ia1%20%1%");

        // Battle the Champion of Chaos!
        Core.MapItemQuest(3795, "drakathfight", 2894);

        // REUSE
        Core.KillQuest(3620, "shadowrise", "Broken Bones|Darkness Elemental|Dry Ice Mage");

        // Search for Death's Lair
        Core.MapItemQuest(3796, "shadowrise", 2895);

        // Arrive in Shadowattack
        Core.ChainQuest(3797);

        // Find your way to Death's lair
        Core.MapItemQuest(3798, "shadowattack", 2896);

        // Beat Death!
        Core.KillQuest(3799, "shadowattack", "Death");

        // Enter Confrontation
        Core.Join("confrontation");
        Core.ChainQuest(3875);

        // Defeat Drakath!
        Core.KillQuest(3876, "finalbattle", "Drakath");

        // Defeat Drakath... again!
        Core.KillQuest(3877, "finalbattle", "Drakath");

        // Defeat Drakath!
        Core.KillQuest(3878, "finalbattle", "Drakath");

        // Defeat the 12 Lords of Chaos!
        Core.KillQuest(3879, "chaosrealm", "Alteon");

        // Defeat the 13th Lord of Chaos
        Core.KillQuest(3880, "chaoslord", "*");

        // The Final Showdown!
        Core.KillQuest(3881, "finalshowdown", "Prince Drakath");

        Core.Relogin();
        Core.BuyItem(Bot.Map.Name, 948, "Lore's Champion Daggers");
        Bot.Sleep(Core.ActionDelay);
        Core.ToBank("Lore's Champion Daggers");
    }

    public void Extra()
    {
        if (Core.QuestProgression(3824))
            return;

        //Arrive in DreadHaven
        Core.ChainQuest(3812);

        //Kill SlugWrath in Dreadhaven
        Core.ChainQuest(3813);

        //Kill Bandit Drakath in Dreadhaven
        Core.ChainQuest(3814);

        //Up the Mountain
        Core.KillQuest(3815, "falcontower", "Lady Knight|Sir Knight");

        //Higher Up
        Core.KillQuest(3816, "falcontower", "Lady Knight|Sir Knight");

        //Even Higher
        Core.KillQuest(3817, "falcontower", "Lady Knight|Sir Knight");

        //Falconreach Tower
        Core.KillQuest(3818, "falcontower", "Lady Knight|Sir Knight");

        //Climb the Tower
        Core.KillQuest(3819, "falcontower", "Lady Knight|Sir Knight");

        //To the Dragonlord
        Core.KillQuest(3820, "falcontower", "Lady Knight|Sir Knight");

        //Defeat the Dragonlord
        Core.KillQuest(3821, "falcontower", "DragonLord");

        //Defeat Dragon Drakath
        Core.KillQuest(3822, "falcontower", "Dragon Drakath");

        //Defeat Sepulchure
        Core.KillQuest(3823, "falcontower", "Sepulchure");

        //Defeat Alteon
        Core.KillQuest(3824, "falcontower", "Alteon");
    }

}