/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
using Skua.Core.Interfaces;

public class Dwarfhold
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
        GravelynandVictoria();
    }

    public void StoryLine()
    {

        if (Core.isCompletedBefore(1757))
            return;

        Story.PreLoad(this);

        GravelynandVictoria();

        if (Core.IsMember)
        {
            //The Tools and the Talent 1743
            Story.KillQuest(1743, "dwarfhold", "Chaotic Draconian");

            //The Shining 1744
            Story.KillQuest(1744, "dwarfhold", "Glow Worm");

            //Your Ad Here 1745
            Story.MapItemQuest(1745, "dwarfhold", 929, 12);

            //Coal Hard Facts 1746
            Story.MapItemQuest(1746, "dwarfhold", 930, 15);

            //Material World 1747
            Story.KillQuest(1747, "dwarfhold", "Chaotic Draconian");

            //You're A Gem 1748
            Story.KillQuest(1748, "dwarfhold", "Chaotic Draconian");

            //Ore Eaters 1749
            Story.KillQuest(1749, "dwarfhold", "Glow Worm");

            //More Tools? Really? 1750
            Story.KillQuest(1750, "dwarfhold", "Amadeus");

            //The Strapping Hero 1751
            Story.KillQuest(1751, "dwarfhold", "Albino Bat");

            //The UnderHammers 1752
            Story.MapItemQuest(1752, "dwarfhold", 931, 8);

            //Fighting Back 1753
            Story.KillQuest(1753, "dwarfhold", "Chaos Drow");

            //Product 1754
            Story.KillQuest(1754, "dwarfhold", "Glow Worm");

            //Earn Your Stripes 1755
            Story.KillQuest(1755, "dwarfhold", "Albino Bat");

            //Stay Sharp  1756
            Story.MapItemQuest(1756, "dwarfhold", 932, 10);

            //Get Us Some Business! 1757
            Story.KillQuest(1757, "dwarfhold", "Albino Bat");
        }

    }
    public void GravelynandVictoria()
    {
        if (Core.isCompletedBefore(3380))
            return;

        Story.PreLoad(this);

        LoC.Ledgermayne();
        LoC.Escherion();
        LoC.Vath();

        //Whats Worse Than Drow? 3376
        Story.KillQuest(3376, "dwarfhold", "Chaos Drow");

        //We Can Replenish Our Armory Too! 3377
        Story.KillQuest(3377, "dwarfhold", "Chaotic Draconian");

        //Begemralded 3378
        Story.KillQuest(3378, "dwarfhold", "Gemrald");

        //Natural Resources 3379
        Story.KillQuest(3379, "dwarfhold", "Glow Worm");

        //Rally the Troops 3380
        Story.MapItemQuest(3380, "dwarfhold", new[] { 2506, 2508, 2509, 2510, 2511 });
    }
}
