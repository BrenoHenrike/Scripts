//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class EtherStormWastes
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DoAll();

        Core.SetOptions(false);
    }

    public void DoAll()
    {
        DragonPlane();
        FireStorm();
        Water();
        Wind();
        Fire();
        Desoloth();
        FireStorm();
        AirStorm();
        WaterStorm();
        EarthStorm();
        Dragonhame();
        DragonHeart();
    }

    public void DragonPlane()
    {
        if (Core.isCompletedBefore(1395))
            return;

        Story.PreLoad(this);

        //Explore The DragonPlane 1384
        Story.MapItemQuest(1384, "dragonplane", new[] { 682, 683, 684, 685, 686 });

        //Lots Of Fiber 1385
        Story.KillQuest(1385, "dragonplane", new[] { "Earth Elemental", "Water Elemental", "Wind Elemental", "Fire Elemental" });

        //Carrying The Torches 1386
        Story.MapItemQuest(1386, "dragonplane", 687, 6);

        //Meet me in the Earth Realm 1387
        Story.MapItemQuest(1387, "dragonplane", 688);

        //Breaking Chains 1388
        Story.KillQuest(1388, "dragonplane", new[] { "Earth Elemental", "Living Earth" });

        //Soil Sprites 1389
        Story.MapItemQuest(1389, "dragonplane", 689, 5);

        //Cripple The Earth Prime 1390
        Story.KillQuest(1390, "dragonplane", "Living Earth");

        //Ward Of The Stone 1391
        Story.MapItemQuest(1391, "dragonplane", 696, 5);

        //Moganth's Dracoscintilla 1392
        Story.KillQuest(1392, "dragonplane", "Moganth");

        //Meet me in the DragonPlane 1393
        Story.MapItemQuest(1393, "dragonplane", 688);

        if (Core.IsMember)
        {
            //Getting Your Feet Wet 1394
            Story.MapItemQuest(1394, "dragonplane", 687, 6);

            //Meet me in the Water Realm 1395
            Story.MapItemQuest(1395, "water", 688);
        }
    }

    public void Water()
    {
        if (!Core.IsMember)
            return;

        DragonPlane();
        if (Core.isCompletedBefore(1403))
            return;

        Story.PreLoad(this);
        //Weaken The Bonds 1396
        Story.KillQuest(1396, "water", new[] { "Water Elemental", "Living Water" });

        //Udaroth Is Ready 1397
        Story.MapItemQuest(1397, "water", 690, 6);

        //A Loophole 1398
        Story.KillQuest(1398, "water", "Living Water");

        //Flipping The Script 1399
        Story.MapItemQuest(1399, "water", 691, 6);

        //Udaroth, The Water Prime 1400
        Story.KillQuest(1400, "water", "Udaroth");

        //Meet me in the DragonPlane 1401
        Story.MapItemQuest(1401, "dragonplane", 688);

        //Up In The Air 1402
        Story.MapItemQuest(1402, "dragonplane", 687, 6);

        //Meet me in the Air Realm 1403
        Story.MapItemQuest(1403, "wind", 688);

    }

    public void Wind()
    {
        if (!Core.IsMember)
            return;

        Water();
        if (Core.isCompletedBefore(1411))
            return;

        Story.PreLoad(this);

        //A Weak Wind 1404
        Story.KillQuest(1404, "wind", new[] { "Wind Elemental", "Living Air" });

        //An Ounce Of Prevention 1405
        Story.MapItemQuest(1405, "wind", 695, 5);

        //This Is A Breeze 1406
        Story.KillQuest(1406, "wind", "Living Air");

        //Chime Time 1407
        Story.MapItemQuest(1407, "wind", 692, 6);

        //Cellot, The Air Prime 1408
        Story.KillQuest(1408, "wind", "Cellot");

        //Meet me in the DragonPlane 1409
        Story.MapItemQuest(1409, "dragonplane", 688);

        //Hot Hot Heat 1410
        Story.MapItemQuest(1410, "dragonplane", 687, 6);

        //Meet me in the Fire Plane 1411
        Story.MapItemQuest(1411, "fire", 688);

    }

    public void Fire()
    {
        if (!Core.IsMember)
            return;

        Wind();
        if (Core.isCompletedBefore(1417))
            return;

        Story.PreLoad(this);

        //Putting Out Fires 1412
        Story.KillQuest(1412, "fire", new[] { "Fire Elemental", "Living Fire" });

        //At A Loss 1413
        Story.MapItemQuest(1413, "fire", 693, 6);

        //Backburner 1414
        Story.KillQuest(1414, "fire", "Living Fire");

        //Open Fire 1415
        Story.MapItemQuest(1415, "fire", 694, 8);

        //Zellare, The Fire Prime 1416
        Story.KillQuest(1416, "fire", "Zellare");

        //Meet me in the DragonPlane 1417
        Story.MapItemQuest(1417, "dragonplane", 688);

    }
    public void Desoloth()
    {
        Fire();
        if (Core.isCompletedBefore(1418))
            return;

        //    [[DesolothFreed Character Page Badge]]
        //Open the DragonGate 1418
        if (!Story.QuestProgression(1418))
        {
            Core.EnsureAccept(1418);
            Core.HuntMonster("desoloth", "Desoloth", "Desoloth Freed!");
            Core.EnsureComplete(1418);
        }
    }

    public void FireStorm()
    {
        if (Core.isCompletedBefore(1547))
            return;

        Story.PreLoad(this);

        //Something Smells Rotten in EtherStorm 1532
        Story.KillQuest(1532, "firestorm", new[] { "Sulfur Imp", "Sulfur Imp" });

        //Play With Fire 1533
        Story.KillQuest(1533, "firestorm", "Living Fire");

        //Set Me On Fire 1542
        Story.MapItemQuest(1542, "firestorm", 784, 13);

        //Walk Through Fire 1543
        Story.MapItemQuest(1543, "firestorm", 785);

        //Everything's On Fire 1545
        Story.MapItemQuest(1545, "firestorm", 786, 20);

        //Baby's On Fire 1546
        Story.KillQuest(1546, "firestorm", "FireStorm Hatchling");

        //Fire In My Heart 1547
        Story.KillQuest(1547, "firestorm", "Ssikari");
    }
    public void AirStorm()
    {
        FireStorm();
        if (Core.isCompletedBefore(1577))
            return;

        Story.PreLoad(this);

        //A Fresh Breath of Life 1571
        Story.KillQuest(1571, "airstorm", "Living Air");

        //Where Air You? 1572
        Story.MapItemQuest(1572, "airstorm", 827, 4);

        //Invading Your (Sacred) Space 1573
        Story.MapItemQuest(1573, "airstorm", 823, 10);

        //There's Magic Every-Air 1574
        Story.KillQuest(1574, "airstorm", new[] { "Air Crystal", "KingCrystal" });

        //Dragon's Fire Blossom to be Found 1575
        Story.MapItemQuest(1575, "firestorm", 824, 12);
        Story.KillQuest(1575, "firestorm", "Sulfur Imp");

        //The Space Between 1576
        Story.KillQuest(1576, "airstorm", "Energy Tornado");

        //The Burning Question 1577
        Story.MapItemQuest(1577, "firestorm", 825);
    }
    public void WaterStorm()
    {
        AirStorm();
        if (Core.isCompletedBefore(1622))
            return;

        Story.PreLoad(this);

        //So, something washed up on the beach... 1616
        Story.MapItemQuest(1616, "waterstorm", 840);
        Story.MapItemQuest(1616, "waterstorm", 841);

        //Dilution Solution 1617
        Story.KillQuest(1617, "waterstorm", "Marsh Lurker");

        //A Frogdrake's Love Story 1618
        Story.MapItemQuest(1618, "waterstorm", 842, 13);

        //A Little Bit of Bubbly Never Hurt 1619
        Story.MapItemQuest(1619, "waterstorm", 843, 10);
        Story.KillQuest(1619, "waterstorm", new[] { "Living Water", "Living Water" });

        //Flight of the Fishwings 1620
        Story.KillQuest(1620, "waterstorm", new[] { "Fishwing", "Fishwing" });

        //Hunt for the Greater Good 1621
        Story.MapItemQuest(1621, "waterstorm", 844, 3);
        Story.KillQuest(1621, "waterstorm", new[] { "Fishman Soldier", "Marsh Lurker", "Frogdrake" });

        //Kill the Deep Dweller 1622
        Story.KillQuest(1622, "waterstorm", "Deep Dweller");
    }
    public void EarthStorm()
    {
        WaterStorm();
        if (Core.isCompletedBefore(1639))
            return;

        Story.PreLoad(this);

        //Call from Deep Within 1633
        Story.KillQuest(1633, "earthstorm", new[] { "Fire Elemental", "wind Elemental", "Water Elemental", "Earth Elemental" });

        //Chrysalis of Flames 1634
        Story.MapItemQuest(1634, "earthstorm", 860, 3);
        Story.KillQuest(1634, "earthstorm", new[] { "Crystallized Living Fire", "Sapphire Golem" });

        //Glittering Secrets of Old 1635
        Story.MapItemQuest(1635, "earthstorm", 862, 8);
        Story.KillQuest(1635, "earthstorm", new[] { "Crystalized Jellyfish", "Diamond Golem" });

        //Aria of Great Significance 
        if (!Story.QuestProgression(1636))
        {
            Core.EnsureAccept(1636);
            Core.GetMapItem(861, 8, "earthstorm");
            Core.GetMapItem(863, 1, "earthstorm");
            Core.HuntMonster("earthstorm", "Shard Spinner", "G Tone", 2);
            Core.HuntMonster("earthstorm", "Shard Spinner", "B Tone", 2);
            Core.HuntMonster("earthstorm", "Shard Spinner", "D Tone", 2);
            Core.HuntMonster("earthstorm", "Shard Spinner", "E Tone", 2);
            Core.HuntMonster("earthstorm", "Emerald Golem", "Greenglass Bells", 8);
            Core.EnsureComplete(1636);
        }

        //Rekindling a Pomegranate 1637
        Story.KillQuest(1637, "earthstorm", "Ruby Golem");

        //Legendary Crystal Skulls 1638
        Story.KillQuest(1638, "earthstorm", "Amethite");

        //Return of the Crystal Beauty 1639
        Story.KillQuest(1639, "earthstorm", "Arradia");
    }

    public void Dragonhame()
    {
        if (Core.isCompletedBefore(3522))
            return;

        Story.PreLoad(this);

        //Talk to Hs'Sakar in Firestorm 3497
        Story.MapItemQuest(3497, "firestorm", 2646);

        //Bring Amulet to Drakor 3498
        Story.KillQuest(3498, "firestorm", "Sulfur Imp");

        //Kill Us Some Zombies 3499
        Story.KillQuest(3499, "shadowfall", "Skeletal Knight");

        //Put 'Em Through the Wringer 3501
        Story.KillQuest(3501, "fotia", "Femme Cult Worshiper");

        //The Core of the Problem 3502
        Story.KillQuest(3502, "dragonplane", new[] { "Earth Elemental", "Water Elemental", "Wind Elemental", "Fire Elemental" });

        //The Scroll of Salubris 3503
        Story.KillQuest(3503, "Natatorium", "Merdraconian");

        //Must Be All Those Limbs 3504
        Story.KillQuest(3504, "wanders", new[] { "Lotus Spider", "Kalestri Worshiper" });

        //What We Need Is More Mages 3505
        Story.MapItemQuest(3505, "dragonrune", new[] { 2649, 2650, 2651, 2652, 2653 });

        //Protect the Eggs 3506
        Story.MapItemQuest(3506, "dragonhame", 2647, 8);
        Story.MapItemQuest(3506, "dragonhame", 2648);

        //Very, Very Sad 3507
        Story.KillQuest(3507, "dragonhame", "Infected Dragon");

        //Ward the Hospital 3509
        Story.KillQuest(3509, "dragonhame", "Infected Dragon");

        //Recover samples in Lair 3511
        if (!Story.QuestProgression(3511))
        {
            Core.EnsureAccept(3511);
            Core.HuntMonster("lair", "Dark Draconian", "Superior Dragon Blood", 3);
            Core.HuntMonster("lair", "Venom Draconian", "Draconian Venom", 3);
            Core.HuntMonster("lair", "Water Draconian", "Draconian Tears", 3);
            Core.HuntMonster("lair", "Wyvern", "Wyvern Scales", 3);
            Core.EnsureAccept(3511);
        }

        //Darkness to Calm Dragons 3512
        Story.KillQuest(3512, "lair", "Dark Draconian");

        //Quest for the Red Dragon’s Scroll 3513
        Story.KillQuest(3513, "lair", "Red Dragon");

        //Help Artix in Swordhavenundead 3514
        if (!Story.QuestProgression(3514))
        {
            Core.EnsureAccept(3514);
            Core.KillMonster("swordhavenundead", "Enter", "Spawn", "*", "Undead Slain", 13);
            Core.EnsureComplete(3514);
        }

        //Kill Dracolich in Temple 3515
        Story.KillQuest(3515, "temple", "Dracolich");

        //Research in Gilead 3516
        Story.KillQuest(3516, "DragonPlane", new[] { "Earth Elemental", "Water Elemental", "Wind Elemental", "Fire Elemental" });

        //Fire and Ice in Battleundera 3517
        if (!Story.QuestProgression(3517))
        {
            Core.EnsureAccept(3517);
            Core.HuntMonster("battleundera", "Skeletal Fire Mage", "Undead Fire Marrow", 6);
            Core.HuntMonster("battleundera", "Skeletal Ice Mage", "Undead Ice Marrow", 6);
            Core.EnsureComplete(3517);
        }

        //Unlife's a Lich 3518
        Story.KillQuest(3518, "battleundera", "Lich");

        //Dragonhame Infirmary Invasion 3519
        Story.KillQuest(3519, "dragonheart", "Zombie Dragon");

        //Elemental Dracolich Destruction 3520
        Story.KillQuest(3520, "infirmary", "Proto-Earth Dracolich");

        //Dragonplane in Peril 3522
        Story.ChainQuest(3522);
    }
    public void DragonHeart()
    {
        if (Core.isCompletedBefore(3535))
            return;

        Story.PreLoad(this);

        //Ward the Heart 3523
        Story.MapItemQuest(3523, "dragonheart", 2660, 5);

        //Infection Rejection 3524
        Story.KillQuest(3524, "dragonheart", "Infected Dragonling");

        //Purify the Plane 3525
        Story.MapItemQuest(3525, "dragonheart", 2661);

        //Zombie Dragon Destruction 3526
        Story.KillQuest(3526, "dragonheart", "Zombie Dragon");

        //Water Realm Purification 3527
        Story.MapItemQuest(3527, "dragonheart", 2662);
        Story.KillQuest(3527, "dragonheart", "Proto-Water Dracolich");

        //Water Cleanses Fire 3528
        Story.MapItemQuest(3528, "dragonheart", 2663, 7);

        //Fire Realm Purification 3529
        Story.MapItemQuest(3529, "dragonheart", 2664);
        Story.KillQuest(3529, "dragonheart", "Proto-Fire Dracolich");

        //Fire Cleanses Earth 3530
        Story.MapItemQuest(3530, "dragonheart", 2665, 7);

        //Earth Realm Purification 3531
        Story.MapItemQuest(3531, "dragonheart", 2666);
        Story.KillQuest(3531, "dragonheart", "Proto-Earth Dracolich");

        //Earth Cleanses Air 3532
        Story.MapItemQuest(3532, "dragonheart", 2667, 7);

        //Air Realm Purification 3533
        Story.MapItemQuest(3533, "dragonheart", 2668);
        Story.KillQuest(3533, "dragonheart", "Proto-Air Dracolich");

        //Dracolich Disconnection 3534
        Story.KillQuest(3534, "dragonheart", new[] { "Tempest Dracolich", "Deluge Dracolich", "Granite Dracolich", "Inferno Dracolich" });

        //Heart-searing Pain 3535
        Story.KillQuest(3535, "dragonheart", "Avatar of Desolich");

    }

}