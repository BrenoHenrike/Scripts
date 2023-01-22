/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Concert
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
        if (!Core.IsMember)
        {
            Core.Logger("Concert Event Is Member Only. Skipping this Script");
            return;
        }

        Story.PreLoad(this);

        Vertigo();
        Darkness();
        Feardeath();
    }

    public void Vertigo()
    {
        if (Core.isCompletedBefore(1024))
            return;

        //Up to the Highest Heights 1020
        Story.MapItemQuest(1020, "vertigo", 390);

        //High-sterical 1021
        Story.KillQuest(1021, "vertigo", new[] { "Fear Muncher", "Banished Banshee" });

        //Elevation Sensation 1022
        Story.KillQuest(1022, "vertigo", new[] { "Banished Banshee", "Cloaked Fiend" });

        //Altitude Adjustment 1023
        Story.KillQuest(1023, "vertigo", new[] { "Cloaked Fiend", "Abandoned Dolly" });

        //Conquer Vertigo 1024
        Story.KillQuest(1024, "vertigo", "Vertigo");
    }

    public void Darkness()
    {
        if (Core.isCompletedBefore(1029))
            return;

        //No Matter How Dark the Night 1025
        Story.KillQuest(1025, "darkness", new[] { "Banished Banshee", "Fear Muncher" });

        //The Dark (K)Nights 1026
        Story.KillQuest(1026, "darkness", "Cloaked Fiend");

        //Something, Something Dark Side 1027
        Story.KillQuest(1027, "darkness", new[] { "Banished Banshee", "Abandoned Dolly" });

        //One Little Candle 1028
        Story.MapItemQuest(1028, "darkness", 392, 10);

        //Conquer Nyctox 1029
        Story.KillQuest(1029, "darkness", "Nyctox");
    }

    public void Feardeath()
    {
        if (Core.isCompletedBefore(1035))
            return;

        //Death Be Not Pround 1030
        Story.MapItemQuest(1030, "feardeath", 391);

        //What Fear Makes 1031
        Story.KillQuest(1031, "feardeath", "Abandoned Dolly");

        //The Little Death 1032
        Story.KillQuest(1032, "feardeath", "Fear Muncher");

        //Terrify the Terrorkind 1033
        Story.KillQuest(1033, "feardeath", new[] { "Fear Muncher", "Abandoned Dolly", "Banished Banshee", "Cloaked Fiend" });

        //Conquer Thanatops 1034
        Story.KillQuest(1034, "feardeath", "Thanotops");

        //Face Fear 1034
        Story.KillQuest(1035, "fear", "Fear");
    }
}
