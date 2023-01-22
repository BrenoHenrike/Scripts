/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class XansLair
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DoAll();

        Core.SetOptions(false);
    }

    public void DoAll()
    {
        Volcano();
        AndesisQuests();
        ScoriasQuestsQuests();
        WarlicsQuests();
    }

    public void Volcano()
    {
        if (Core.isCompletedBefore(1261))
            return;

        Story.PreLoad(this);

        // Level 1 1242
        Story.KillQuest(1242, "volcano", "Lava Golem");

        // Level 2 1243
        Story.MapItemQuest(1243, "volcano", 526, 3);
        Story.KillQuest(1243, "volcano", "Fire Imp");

        // Level 3 1244
        Story.KillQuest(1244, "volcano", "Fire Imp");

        // Level 4 1245
        Story.MapItemQuest(1245, "volcano", 527, 8);
        Story.KillQuest(1245, "volcano", "Lava Golem");

        // Level 5 1246
        Story.KillQuest(1246, "volcano", "Fire Imp");

        // Level 6 1247
        Story.KillQuest(1247, "volcano", "Fire Imp");

        // Level 7 1248
        Story.MapItemQuest(1248, "volcano", 528, 12);

        // Level 8 1249
        Story.KillQuest(1249, "volcano", "Flamethrower Dwakel");

        // Level 9 1250
        Story.KillQuest(1250, "volcano", "Fire Imp");
        Story.MapItemQuest(1250, "volcano", 529, 8);

        // Level 10 1251
        Story.KillQuest(1251, "volcano", "Flamethrower Dwakel");

        // Level 11 1252
        Story.MapItemQuest(1252, "volcano", 530, 10);
        Story.KillQuest(1252, "volcano", "Fire Imp");

        // Level 12 1253
        Story.KillQuest(1253, "volcano", "Flame Elemental");

        // Level 13 1254
        Story.MapItemQuest(1254, "volcano", 531, 12);

        // Level 14 1255
        Story.KillQuest(1255, "volcano", "Fire Imp");

        // Level 15 1256
        Story.KillQuest(1256, "volcano", "Fire Imp");

        // Level 16 1257
        Story.MapItemQuest(1257, "volcano", 532, 10);

        // Level 17 1258
        Story.KillQuest(1258, "volcano", "Flamethrower Dwakel");

        // Level 18 1259
        Story.KillQuest(1259, "volcano", new[] { "Fire Imp", "Flame Elemental" });

        // Level 19 1260
        Story.MapItemQuest(1260, "volcano", 533, 6);
        Story.KillQuest(1260, "volcano", "Fire Imp");

        // Level 20 1261
        Story.KillQuest(1261, "volcano", "Magman");
    }

    public void AndesisQuests()
    {
        if (Core.isCompletedBefore(1739))
            return;

        Story.PreLoad(this);

        // A Town Divided 1733
        Story.MapItemQuest(1733, "xantown", 922);

        // The Miller's Key 1734
        Story.KillQuest(1734, "xantown", "Fire Imp");

        // Trailblazer 1735
        Story.KillQuest(1735, "xantown", "Fire Imp");

        // Fire Brigade Of One 1736
        Story.KillQuest(1736, "xantown", "Fire Imp");

        // Fire Control 1737
        Story.MapItemQuest(1737, "xantown", 923, 4);

        // Andesi's Family Pendant 1738
        Story.MapItemQuest(1738, "xantown", 924);

        // Signed, Seared, Delivered 1739
        Story.MapItemQuest(1739, "xantown", 925);
    }

    public void ScoriasQuestsQuests()
    {
        if (Core.isCompletedBefore(1740))
            return;

        Story.PreLoad(this);

        // The Xan With The Plan 1740
        Story.KillQuest(1740, "xantown", "Xan");
    }

    public void WarlicsQuests()
    {
        if (Core.isCompletedBefore(2157))
            return;

        Story.PreLoad(this);

        // Locate The Sealed Library 2151
        Story.MapItemQuest(2151, "xancave", 1220);

        // A Powered Library Lock 2152
        Story.KillQuest(2152, "xancave", "Lava Goblin");

        // Luminate the Library Lock 2153
        Story.MapItemQuest(2153, "xancave", 1221);

        // Defend the Library! 2154
        Story.KillQuest(2154, "xancave", "Lava Goblin");

        // Crossing Over 2155
        Story.MapItemQuest(2155, "xancave", 1223, 8);
        Story.KillQuest(2155, "xancave", new[] { "Lava Goblin", "Lava Goblin" });

        // Guardian Of Shurpu 2156
        Story.KillQuest(2156, "xancave", "Shurpu Ring Guardian");

        // Face Xan 2157
        Story.MapItemQuest(2157, "xancave", 1222);
    }
}
