/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Downward
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
        if (Core.isCompletedBefore(2884))
            return;

        Story.PreLoad(this);

        //Warlic's Letter To Rayst 2861
        Story.MapItemQuest(2861, "arcangrove", 1752);

        //Rayst's Response 2862
        Story.MapItemQuest(2862, "battleontown", 1753);

        //Chat With Cysero 2863
        Story.MapItemQuest(2863, "battleontown", 1754);

        //You'll Never Get Me, Copper! 2864
        Story.KillQuest(2864, "lair", "Bronze Draconian");

        //Lode Me Up 2865
        Story.KillQuest(2865, "cornelis", "Gargoyle");

        //The Spark 2866
        Story.KillQuest(2866, "Noobshire", "Kittarian Mouse Eater");

        //GI-ANTS 2867
        Story.KillQuest(2867, "giant", "Red Ant");

        //What Big Teeth You Have 2868
        Story.KillQuest(2868, "mudluk", "Swamp Lurker");

        //Speak To Alina 2869
        Story.MapItemQuest(2869, "battleontown", 1755);

        //Return The Potion To Cysero 2870
        Story.MapItemQuest(2870, "battleontown", 1756);

        //Enter the Entrance 2871
        Story.KillQuest(2871, "downward", "Unearthed Skeleton");

        //Can You Dig It? 2872
        Story.KillQuest(2872, "downward", "Rotfeeder Worm");

        //Snip Snip 2873
        Story.KillQuest(2873, "downward", "Rotfeeder Worm");

        //Resonations 2874
        Story.MapItemQuest(2874, "downward", 1757, 6);

        //Eureka! 2875
        Story.KillQuest(2875, "downward", "Rotfeeder Worm");

        //Push-Button Start 2876
        Story.MapItemQuest(2876, "downward", 1758);

        //Out Of Coal 2877
        Story.KillQuest(2877, "downward", "Rotfeeder Worm");

        //Start The Reaction 2878
        Story.MapItemQuest(2878, "downward", 1759, 8);

        //Start Her Up 2879
        Story.MapItemQuest(2879, "downward", 1760);

        //We Have Arrived! 2880
        Story.KillQuest(2880, "downward", "Mana Crystalized Undead");

        //Hardened Crystals 2881
        Story.KillQuest(2881, "downward", "Gemmed Burrower");

        //Quadtanium? 2882
        Story.MapItemQuest(2882, "downward", 1761, 6);

        //Capacitance 2883
        Story.KillQuest(2883, "downward", "Gemmed Burrower");

        //The Heart Of The World 2884
        Story.KillQuest(2884, "downward", "Crystal Mana Construct");

    }
}
