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

        StoryLine();

        Core.SetOptions(false);
    }

    public void StoryLine()
    {
        if (Core.isCompletedBefore(1639))
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

        //So something washed up on the beach... 1616
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
}