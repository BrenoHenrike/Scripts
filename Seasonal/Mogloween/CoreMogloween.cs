//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs

using Skua.Core.Interfaces;

public class CoreMogloween
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Core.RunCore();

        Core.SetOptions(false);
    }

    public void DoAll()
    {
        if (!Core.isSeasonalMapActive("mogloween"))
            return;
        Mogloween();
        CandyShop();
        CandyCorn();
        Pie();
        Asylum();
        PoeHub();
        MystcroftForest();
        Chromafection();
        TwigsArcade();
        Masquerade();
        That();
        NecroCarnival();
        TrickTown();
    }

    public void Mogloween()
    {
        if (!Core.isSeasonalMapActive("mogloween"))
            return;
        if (Core.isCompletedBefore(395))
            return;

        Story.PreLoad(this);
        //Candy Craze 95
        Story.KillQuest(95, "mogloween", "Pumpkinhead Fred");

        //Chocolate Goodness 96
        Story.KillQuest(96, "mogloween", "Pumpkinhead Fred");

        //Chilling Costume 93
        Story.KillQuest(93, "mogloween", "Ghostly Sheet");

        //Candy Basket 94
        Story.KillQuest(94, "mogloween", "Pumpkinhead Fred");

        //Lollipop Potion 97
        Story.KillQuest(97, "mogloween", "Jack-O-Doom");

        //Mystery Candy 98
        if (!Story.QuestProgression(98))
        {
            Core.EnsureAccept(98);
            Core.KillMonster("mogloween", "Pit1", "Right", "Blister", "Mystery Candy", 3);
            Core.EnsureComplete(98);
        }

        //Can't have enough 99
        if (!Story.QuestProgression(99))
        {
            Core.EnsureAccept(99);
            Core.KillMonster("mogloween", "Pit1", "Right", "Blister", "Mystery Candy", 5);
            Core.EnsureComplete(99);
        }

        //Squash the King 391
        Story.KillQuest(391, "mogloween", "Great Pumpkin King");

        //A Bone To Pick 392
        Story.KillQuest(392, "mogloweengrave", "Graveyard Soldier");

        //Were are the Wolves 393
        Story.KillQuest(393, "mogloweengrave", "Graveyard Werewolf");

        //Killer, Chiller, Thriller Here Tonight 394
        Story.KillQuest(394, "mogloweengrave", "Thriller");

        //Candy Shop Cutscene 395
        Story.MapItemQuest(395, "candyshop", 69);

    }

    public void CandyShop()
    {
        if (!Core.isSeasonalMapActive("candyshop"))
            return;
        Mogloween();
        if (Core.isCompletedBefore(401))
            return;

        Story.PreLoad(this);

        //Clear A Path 396
        Story.KillQuest(396, "candyshop", "Dark Moglinster");

        //Kanthalite-D 397
        Story.KillQuest(397, "candyshop", "Sugarrush Ghoul");

        //Myx It Up 398
        Story.KillQuest(398, "candyshop", "Super Moglinster");

        //Candy Conundrum 399
        Story.KillQuest(399, "candyshop", "Dark Moglinster");

        //Boos Clues 400
        if (!Story.QuestProgression(400))
        {
            Core.EnsureAccept(400);
            Core.HuntMonster("voltabolt", "Moglinsterbot", "Fluoride Element");
            Core.HuntMonster("voltabolt", "Moglinsterbot", "Stainless Steel Veneers");
            Core.HuntMonster("voltabolt", "Nightmare Dentist Chair", "Robotic Drill Bit");
            Core.HuntMonster("voltabolt", "Nightmare Dentist Chair", "Fill-up's Screwdriver");
            Core.EnsureComplete(400);
        }

        //What’s Up, Doc? 401
        Story.KillQuest(401, "voltabolt", "Dental Driller");
    }
    public void CandyCorn()
    {
        if (!Core.isSeasonalMapActive("candycorn"))
            return;
        CandyShop();
        if (Core.isCompletedBefore(878))
            return;

        Story.PreLoad(this);

        //Investigate the Farmhouse 871
        Story.MapItemQuest(871, "candycorn", 193);

        //Children of Chaos 872
        Story.KillQuest(872, "candycorn", "Candy Kid");

        //Where is that Barn Key? 873
        Story.KillQuest(873, "candycorn", "Candy Kid");

        //Investigate the Barn 874
        Story.MapItemQuest(874, "candycorn", 194);

        //Malik-EYE is so Grounded 875
        Story.KillQuest(875, "candycorn", "Malik-EYE");

        //Clearing the Candy Corn Field  876
        Story.KillQuest(876, "candycorn", "Field Guardian");

        //So Sick of EYE-Sac 877
        Story.KillQuest(877, "candycorn", "EYE-Sac");

        //She Who Walks Behind The Stalks 878
        Story.KillQuest(878, "candycorn", "Stalkwalker");

    }
    public void Pie()
    {
        if (!Core.isSeasonalMapActive("pie"))
            return;
        CandyShop();
        if (Core.isCompletedBefore(1363))
            return;

        Story.PreLoad(this);

        //Take a Bite Out of Crime 1355
        Story.KillQuest(1355, "pie", "Gourdo");

        //Skullberry Picking 1356
        Story.MapItemQuest(1356, "pie", 654, 10);

        //Can I Axe You a Question 1357
        Story.KillQuest(1357, "pie", "Gourdo");

        //Don't Forge-t 1358
        Story.MapItemQuest(1358, "pie", 655);

        //A Monstrous Appetite 1359
        if (!Story.QuestProgression(1359))
        {
            Core.EnsureAccept(1359);
            Core.HuntMonster("pie", "Gourdo", "Pie Defended", 13);
            Core.HuntMonster("pie", "Gourdo", "Friend Avenged", 2);
            Core.EnsureComplete(1359);
        }

        //Undead End in Sight 1360
        Story.MapItemQuest(1360, "pie", 656);

        //Missing Servant 1361
        Story.MapItemQuest(1361, "pie", 658);

        //Remember Your Quest 1362
        Story.KillQuest(1362, "pie", "Myst Imp");

        //Head to WillowCreek! 1363
        Story.MapItemQuest(1363, "willowcreek", 657);

    }

    public void Asylum()
    {
        if (Core.isCompletedBefore(2466))
            return;

        Story.PreLoad(this);

        //Reflect Upon the Situation 2454
        Story.KillQuest(2454, "mirrormaze", "Insane Ghoul");

        //Escape the Insanity! 2455
        Story.MapItemQuest(2455, "mirrormaze", 1522);

        //Smoke and Mirrors 2456
        Story.MapItemQuest(2456, "mirrormaze", 1523, 6);

        //Shot in the Dark 2457
        Story.KillQuest(2457, "mirrormaze", "Insane Ghoul");

        //Cyser - Oh Where Art Thou? 2458
        Story.MapItemQuest(2458, "mirrormaze", 1524);

        // [[Catacombs]]

        //Tricks Trail Trackin 2459
        Story.MapItemQuest(2459, "catacombs", new[] { 1525, 1526, 1527, 1528, 1529, 1530 });

        //Cultivate Answers 2460
        Story.KillQuest(2460, "catacombs", "Scorpion Cultist");

        //One Found Doll 2461
        Story.MapItemQuest(2461, "catacombs", 1531);

        //The Key to Saving Kimberly 2462
        Story.KillQuest(2462, "catacombs", "Scorpion Cultist");

        //Right Tool for the Job 2463
        Story.MapItemQuest(2463, "catacombs", 1532, 10);

        //Pick the Lock 2464
        Story.MapItemQuest(2464, "catacombs", 1533);

        //Destroy DeSawed 2465
        if (!Story.QuestProgression(2465))
        {
            Core.EnsureAccept(2465);
            Core.KillMonster("catacombs", "Frame6", "Left", "Dr. De'Sawed", "Disarmed De'Sawed");
            Core.EnsureComplete(2465);
        }

        //Demolish DeSawed FOR REAL 2466
        if (!Story.QuestProgression(2466))
        {
            Core.EnsureAccept(2466);
            Core.KillMonster("catacombs", "Boss2", "Left", "Dr. De'Sawed", "De'Sawed Defeated");
            Core.EnsureComplete(2466);
        }
    }

    public void PoeHub()
    {
        if (!Core.isSeasonalMapActive("poehub"))
            return;
        if (Core.isCompletedBefore(4571))
            return;

        Story.PreLoad(this);

        //Story Time! 3833
        Story.KillQuest(3833, "poehub", "Bookcase");

        //Medicine Man 3834
        if (!Story.QuestProgression(3834))
        {
            Core.EnsureAccept(3834);
            Core.HuntMonster("crusher", "Gothic Chest", "Mortar");
            Core.HuntMonster("crusher", "Gothic Chest", "Pestle");
            Core.HuntMonster("crusher", "Gothic Chest", "Syringe");
            Core.HuntMonster("crusher", "Gothic Chest", "Gourd Medicine Vial");
            Core.EnsureComplete(3834);
        }
        //Her Herbalism 3835
        Story.MapItemQuest(3835, "crusher", 2941, 2);
        Story.MapItemQuest(3835, "crusher", 2942, 3);
        Story.MapItemQuest(3835, "crusher", 2943, 2);
        Story.KillQuest(3835, "crusher", "Bookcase");

        //Extra-Sensory Siblings 3836
        Story.KillQuest(3836, "crusher", "Apparition");

        //Dr's House(of Crusher)Call 3837
        Story.MapItemQuest(3837, "crusher", 2944);

        //Spook and the Book 3838
        Story.KillQuest(3838, "crusher", "Bookcase");

        //Sampling Sips 3839
        Story.KillQuest(3839, "cask", "Moglinberry Vines");

        //Glow with the Flambeaux 3840
        Story.KillQuest(3840, "cask", "Fire Performer");

        //Nitre Fighter 3841
        Story.MapItemQuest(3841, "cask", 2945);
        Story.KillQuest(3841, "cask", "Nitre Golem");

        //Spider Trouble 3842
        Story.KillQuest(3842, "cask", "Cellar Spider");

        //Coat of Harms 3843
        Story.MapItemQuest(3843, "cask", 2952);

        //Break through the Wall 3844
        Story.MapItemQuest(3844, "cask", 2946);

        //Search the Bones 3845
        Story.MapItemQuest(3845, "cask", 2953);

        //Defeat the Bone Terror 4570
        Story.KillQuest(4570, "cask", "Bone Terror");

        //Defeat Macabre Sepulchure 4571
        Story.KillQuest(4571, "wyvern", "Macabre Sepulchure");
    }

    public void MystcroftForest()
    {
        if (!Core.isSeasonalMapActive("mystcroftforest"))
            return;
        if (Core.isCompletedBefore(5427))
            return;

        Story.PreLoad(this);

        //Tome of The Otherworld 5418
        Story.KillQuest(5418, "mystcroftforest", "Eerie Apparition");

        //Monster Data 5419
        Story.KillQuest(5419, "mystcroftforest", "Eerie Apparition");

        //The Best Costumes Need the Best Fabric 5420
        Story.KillQuest(5420, "mystcroftforest", "Eerie Apparition");

        //Accessorize! 5421
        Story.MapItemQuest(5421, "mystcroftforest", 4801, 8);
        Story.KillQuest(5421, "mystcroftforest", "Grim Goblin");

        //Make those Munchies 5422
        Story.MapItemQuest(5422, "mystcroftforest", 4802, 8);
        Story.KillQuest(5422, "mystcroftforest", "Eerie Apparition");

        //Every Party Needs Drinks 5423
        Story.KillQuest(5423, "mystcroftforest", "Grim Goblin");

        //Beautiful Creatures and their Uses 5424
        Story.KillQuest(5424, "timevoid", "Void Phoenix");

        //Ooh, Shiny! 5425
        if (!Story.QuestProgression(5425))
        {
            Core.EnsureAccept(5425);
            Core.HuntMonster("skytower", "Moonstone", "Moonstone Crystal", 10);
            Core.HuntMonster("skytower", "Sunstone", "Sunstone Crystal", 10);
            Core.HuntMonster("skytower", "Star Sapphire", "Sapphire Crystal", 10);
            Core.EnsureComplete(5425);
        }

        //Time to get Dressed Up! 5426
        Story.MapItemQuest(5426, "mystcroftforest", 4800);
        Story.KillQuest(5426, "mystcroftforest", "Grim Goblin");

        //EEEEK 5427
        Story.KillQuest(5427, "mystcroftforest", "Barghest");
    }

    public void Chromafection()
    {
        if (!Core.isSeasonalMapActive("chromafection"))
            return;
        if (Core.isCompletedBefore(6537))
            return;

        Story.PreLoad(this);

        //Wacky Winka’s Candy Shop 6531
        Story.MapItemQuest(6531, "chromafection", 6004);

        //Nougat-ta Save the Parents 6532
        Story.KillQuest(6532, "chromafection", "Colorless Drone");

        //Chroma Kids 6533
        Story.MapItemQuest(6533, "chromafection", 6006, 10);

        //Caramel Clues 6534
        Story.KillQuest(6534, "chromafection", "Free Samples");

        //Sweet, Sweet Proof 6535
        Story.MapItemQuest(6535, "chromafection", 6007, 15);

        //Whacky Snacky 6536
        Story.MapItemQuest(6536, "chromafection", 6005);

        //Chroma CRASH 6537
        Story.KillQuest(6537, "chromafection", "Chromafection");

        //Color Farming 6538
        Story.KillQuest(6538, "chromafection", "Chromafection");
    }

    public void TwigsArcade()
    {
        if (!Core.isSeasonalMapActive("twigsarcade"))
            return;
        if (Core.isCompletedBefore(6579))
            return;

        Story.PreLoad(this);

        //Stop the Hurting 6568
        Story.KillQuest(6568, "twigsarcade", new[] { "Scotty Sneevil", "Clucky Moo" });

        //Gather the Goods 6569
        Story.MapItemQuest(6569, "twigsarcade", 6069, 3);
        Story.MapItemQuest(6569, "twigsarcade", 6070, 3);
        Story.KillQuest(6569, "twigsarcade", "Scotty Sneevil");

        //Spooky Casings 6570
        Story.KillQuest(6570, "twigsarcade", "Scotty Sneevil");

        //Do a Goo-d Job 6571
        Story.KillQuest(6571, "twigsarcade", "Ectoplasm");

        //Plop Plop Fizz Fizz 6572
        Story.MapItemQuest(6572, "twigsarcade", 6071);

        //Test the Ghost box 6573
        Story.KillQuest(6573, "twigsarcade", new[] { "Scotty Sneevil", "Clucky Moo" });

        //Get Wid of CWEEPY TWIG 6574
        Story.KillQuest(6574, "twigsarcade", "Cweepy Twig");

        //Cranky Spirits 6575
        Story.KillQuest(6575, "twigsarcade", "Spirit Residue");

        //It's Too S-warm 6576
        Story.KillQuest(6576, "twigsarcade", "Swarmer");

        //Get into the Panic Room 6577
        Story.KillQuest(6577, "twigsarcade", "Cweepy Twilly");

        //Smash the Wall 6578
        Story.MapItemQuest(6578, "twigsarcade", 6072);

        //Scary Baby 6579
        Story.KillQuest(6579, "twigsarcade", "Baby");
    }

    public void Masquerade()
    {
        if (!Core.isSeasonalMapActive("masquerade"))
            return;
        if (Core.isCompletedBefore(7154))
            return;

        Story.PreLoad(this);

        //Mmm... Cake 7139
        Story.KillQuest(7139, "masquerade", "Harried Waiter");

        //Catch the Sprites 7140
        Story.KillQuest(7140, "masquerade", "Garden Sprite");

        //Spicy 7141
        Story.KillQuest(7141, "masquerade", "Kitchen Brownie");

        //Things are Getting Hairy 7142
        Story.MapItemQuest(7142, "masquerade", new[] { 6779, 6780, 6781, 6782, 6783 });

        //Let 'em Loose 7143
        Story.MapItemQuest(7143, "masquerade", 6784);

        //Gather Spores 7144
        Story.KillQuest(7144, "masquerade", "Glowshroom");

        //Getting Dusty 7145
        Story.KillQuest(7145, "masquerade", "Garden Sprite");

        //Gather Moss 7146
        Story.KillQuest(7146, "masquerade", "Fae Treeant");

        //Feed Bramblebite 7147
        Story.MapItemQuest(7147, "masquerade", 6785);

        //Feed Bramblebite 7148
        Story.MapItemQuest(7148, "masquerade", 6786, 6);
        Story.KillQuest(7148, "masquerade", "Glass Spitter");

        //So Glamourous 7149
        Story.KillQuest(7149, "masquerade", "Garden Sprite");

        //Likin' the Lichen 7150
        Story.KillQuest(7150, "masquerade", "Fae Treeant");

        //It's Wine Time 7151
        Story.KillQuest(7151, "masquerade", "Wandering Guest");

        //Nap Time for Barb 7152
        Story.MapItemQuest(7152, "masquerade", 6789);
        Story.KillQuest(7152, "masquerade", "Scriptkeeper");

        //Sneak Out 7153
        Story.MapItemQuest(7153, "masquerade", 6787);

        //Break Out 7154
        Story.KillQuest(7154, "masquerade", "Aermhar");
    }

    public void That()
    {
        if (!Core.isSeasonalMapActive("that"))
            return;
        if (Core.isCompletedBefore(7179))
            return;

        Story.PreLoad(this);

        //Yeti Witual 7167
        Story.MapItemQuest(7167, "that", 6790, 8);
        Story.KillQuest(7167, "that", "Candy Goblin");

        //Get the Fur 7168
        Story.KillQuest(7168, "that", "Moglinster");

        //Vapor Needed 7169
        Story.KillQuest(7169, "that", "Mystcroft Ghost");

        //Trap the Flame 7170
        Story.KillQuest(7170, "that", "Will o' The Wisp");

        //Down the Well 7171
        Story.MapItemQuest(7171, "that", 6791);

        //Cweepie-cwawlie Time 7172
        Story.KillQuest(7172, "that", new[] { "Well Spider", "Congealed Fear" });

        //Gwoop Destruction 7173
        Story.KillQuest(7173, "that", "Congealed Fear");

        //Seawch for Bubble 7174
        Story.MapItemQuest(7174, "that", 6792, 6);
        Story.MapItemQuest(7174, "that", 6793);

        //Free Bubble! 7175
        Story.MapItemQuest(7175, "that", 6794);
        Story.KillQuest(7175, "that", "Congealed Fear");

        //Echo... echo... echo 7176
        Story.KillQuest(7176, "that", "Echo of That");

        //Gather Hope 7177
        Story.KillQuest(7177, "that", "Shattered Hope");

        //Bweak the Blacklights 7178
        Story.KillQuest(7178, "that", "Blacklights");

        //Get Wid of THAT 7179
        Story.KillQuest(7179, "that", "That");
    }

    public void NecroCarnival()
    {
        if (!Core.isSeasonalMapActive("necrocarnival"))
            return;
        if (Core.isCompletedBefore(8374))
            return;

        Story.PreLoad(this);

        //Dolls' Hide and Seek 8363
        Story.MapItemQuest(8363, "necrocarnival", 9249, 8);
        Story.KillQuest(8363, "necrocarnival", "Gummy Tapeworm");

        //Arts and Crafts 8364
        Story.MapItemQuest(8364, "necrocarnival", 9250);
        Story.KillQuest(8364, "necrocarnival", "Skeleclown");

        //Lemonade, Chewy Ice 8365
        Story.MapItemQuest(8365, "necrocarnival", 9251, 2);
        Story.KillQuest(8365, "necrocarnival", new[] { "Mooch Treeant", "Gummy Tapeworm" });

        //Screams and Tag 8366
        Story.MapItemQuest(8366, "necrocarnival", 9252);
        Story.KillQuest(8366, "necrocarnival", "Skeleclown");

        //Playful Teething 8367
        Story.MapItemQuest(8367, "necrocarnival", 9253, 4);
        Story.KillQuest(8367, "necrocarnival", "Mooch Treeant");

        //Days Pass 8368
        Story.MapItemQuest(8368, "necrocarnival", 9254);
        Story.KillQuest(8368, "necrocarnival", "Cotton Tick");

        //Pinned Bugs 8369
        Story.MapItemQuest(8369, "necrocarnival", 9255);
        Story.KillQuest(8369, "necrocarnival", "Gummy Tapeworm");

        //Sweet Glue 8370
        Story.KillQuest(8370, "necrocarnival", "Mooch Treeant");

        //Witch Soup 8371
        Story.MapItemQuest(8371, "necrocarnival", 9256);
        Story.KillQuest(8371, "necrocarnival", new[] { "Mooch Treeant", "Gummy Tapeworm", "Skeleclown" });

        //Written Promise 8372
        Story.MapItemQuest(8372, "necrocarnival", 9257, 7);
        Story.KillQuest(8372, "necrocarnival", "Cotton Tick");

        //All Fall Down 8373
        Story.MapItemQuest(8373, "necrocarnival", 9258);
        Story.KillQuest(8373, "necrocarnival", "Skeleclown");

        //Lullaby 8374
        Story.KillQuest(8374, "necrocarnival", "Deva");
    }

    public void TrickTown()
    {
        if (!Core.isSeasonalMapActive("tricktown"))
            return;
        if (Core.isCompletedBefore(8935))
            return;

        Story.PreLoad(this);

        // Punchergeist 8926
        Story.MapItemQuest(8926, "tricktown", 10813);
        Story.KillQuest(8926, "tricktown", "Playful Ghost");

        // Party Town 8927
        Story.MapItemQuest(8927, "tricktown", 10804, 3);
        Story.KillQuest(8927, "tricktown", "Decay Spirit");

        // Sweet Mucus 8928
        Story.MapItemQuest(8928, "tricktown", 10805, 5);
        Story.KillQuest(8928, "tricktown", "Playful Ghost");

        // Door to Door 8929
        if (!Story.QuestProgression(8929))
        {
            Core.EnsureAccept(8929);
            while (!Bot.ShouldExit && !Core.CheckInventory(73465, 300))
            {
                Core.AddDrop("Treats");
                Core.Join("tricktown");
                Core.KillMonster("trickortreat", "Enter", "Spawn", "Trick or Treater");
                Bot.Wait.ForPickup("Treats");
            }
            Core.EnsureComplete(8929);
        }

        // Crusted Compost 8930
        Story.MapItemQuest(8930, "tricktown", 10806, 3);
        Story.KillQuest(8930, "tricktown", "Rotting Pumpkin");

        // A Spot of Sludge 8931
        Story.MapItemQuest(8931, "tricktown", 10807, 4);
        Story.KillQuest(8931, "tricktown", "Decay Spirit");

        // Messy Unrest 8932
        Story.KillQuest(8932, "tricktown", new[] { "Decay Spirit", "Playful Ghost" });

        // Plain Monster 8933
        Story.MapItemQuest(8933, "tricktown", 10808);
        Story.KillQuest(8933, "tricktown", "Rotting Mound");

        // Study Group 8934
        Story.MapItemQuest(8934, "tricktown", 10809, 5);
        Story.KillQuest(8934, "tricktown", "Rotting Mound");

        // The Fruits of Labor 8935
        Story.KillQuest(8935, "tricktown", "Madam Ester");
    }
}
