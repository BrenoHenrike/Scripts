//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
using Skua.Core.Interfaces;

public class Mobius
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public Core13LoC LoC => new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DoAll();

        Core.SetOptions(false);
    }

    public void DoAll()
    {
        StoryLine();
        Saathel();
        GravelynandVictoria();
    }
    public void StoryLine()
    {
        if (Core.isCompletedBefore(2375))
            return;

        Story.PreLoad(this);

        //Waking History 2365
        Story.MapItemQuest(2365, "mobius", 1465);

        //Gathering Shadows 2366
        Story.MapItemQuest(2366, "mobius", 1463, 10);

        //A Less Direct Path 2367
        Story.KillQuest(2367, "mobius", "Fire Imp");

        //Interference 2368
        Story.KillQuest(2368, "mobius", "Fire Imp");

        //The Race Is On 2369
        Story.MapItemQuest(2369, "mobius", 1464, 10);

        //The Darkness Inside 2370
        Story.KillQuest(2370, "mobius", "Slugfit");

        //The Luna 2371
        Story.MapItemQuest(2371, "mobius", 1466);

        //Star of the Wild 2372
        Story.KillQuest(2372, "Guru", "Wisteria");

        //Star of the Seas 2373
        Story.KillQuest(2373, "natatorium", "Marianus");

        //The Star of Earth 2374
        Story.KillQuest(2374, "pines", "Pine Troll");

        //Star of the Flames 2375
        Story.KillQuest(2375, "greendragon", "Greenguard Dragon");

    }

    public void Saathel()
    {
        if (Core.isCompletedBefore(2364))
            return;

        Story.PreLoad(this);

        // The Dead City 2354
        Story.MapItemQuest(2354, "mobius", 1465);

        //Grains of Chaos 2355
        Story.KillQuest(2355, "mobius", "Chaos Sp-Eye");

        //Call of the Sun 2356
        Story.MapItemQuest(2356, "mobius", 1461, 10);

        //Shattered Past 2357
        Story.KillQuest(2357, "mobius", "Chaos Sp-Eye");

        //The Spoiler 2358
        Story.MapItemQuest(2358, "mobius", 1462, 10);

        //Heart Of The Sun 2359
        Story.KillQuest(2359, "mobius", "Slugfit");

        //The Sol 2360
        Story.MapItemQuest(2360, "mobius", 1466);

        //Star of the Wild 2361
        Story.KillQuest(2361, "Guru", "Wisteria");

        //Star of the Seas 2362
        Story.KillQuest(2362, "natatorium", "Marianus");

        //Star of the Earth 2363
        Story.KillQuest(2363, "pines", "Pine Troll");

        //The Star of Flames 2364
        Story.KillQuest(2364, "greendragon", "Greenguard Dragon");

    }

    public void GravelynandVictoria()
    {
        if (Core.isCompletedBefore(3370))
            return;

        Story.PreLoad(this);

        LoC.Ledgermayne();
        LoC.Escherion();
        LoC.Vath();

        //You'll Poke Your Eye Out 3366
        Story.KillQuest(3366, "mobius", "Chaos Sp-Eye");

        //Imp-roved Arrows 3367
        Story.KillQuest(3367, "mobius", "Fire Imp");

        //Raiders of the Lost Armory 3368
        Story.KillQuest(3368, "mobius", "Cyclops Raider");

        //Subtle Persuasion 3369
        Story.KillQuest(3369, "faerie", "Cyclops Warlord");

        //Mobilize Mobius 3370
        Story.MapItemQuest(3370, "mobius", 2504, 6);

    }
}
