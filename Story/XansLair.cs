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

        Story.KillQuest(1242, "volcano", "Lava Golem");
        Story.MapItemQuest(1243, "volcano", 526, 3);
        Story.KillQuest(1243, "volcano", "Fire Imp");
        Story.KillQuest(1244, "volcano", "Fire Imp");
        Story.MapItemQuest(1245, "volcano", 527, 8);
        Story.KillQuest(1245, "volcano", "Lava Golem");
        Story.KillQuest(1246, "volcano", "Fire Imp");
        Story.KillQuest(1247, "volcano", "Fire Imp");
        Story.MapItemQuest(1248, "volcano", 528, 12);
        Story.KillQuest(1249, "volcano", "Flamethrower Dwakel");
        Story.KillQuest(1250, "volcano", "Fire Imp");
        Story.MapItemQuest(1250, "volcano", 529, 8);
        Story.KillQuest(1251, "volcano", "Flamethrower Dwakel");
        Story.MapItemQuest(1252, "volcano", 530, 10);
        Story.KillQuest(1252, "volcano", "Fire Imp");
        Story.KillQuest(1253, "volcano", "Flame Elemental");
        Story.MapItemQuest(1254, "volcano", 531, 12);
        Story.KillQuest(1255, "volcano", "Fire Imp");
        Story.KillQuest(1256, "volcano", "Fire Imp");
        Story.MapItemQuest(1257, "volcano", 532, 10);
        Story.KillQuest(1258, "volcano", "Flamethrower Dwakel");
        Story.KillQuest(1259, "volcano", new[] { "Fire Imp", "Flame Elemental" });
        Story.MapItemQuest(1260, "volcano", 533, 6);
        Story.KillQuest(1260, "volcano", "Fire Imp");
        Story.KillQuest(1261, "volcano", "Magman");
    }

    public void AndesisQuests()
    {
        if (Core.isCompletedBefore(1739))
            return;

        Story.PreLoad(this);

        Story.MapItemQuest(1733, "xantown", 922);
        Story.KillQuest(1734, "xantown", "Fire Imp");
        Story.KillQuest(1735, "xantown", "Fire Imp");
        Story.KillQuest(1736, "xantown", "Fire Imp");
        Story.MapItemQuest(1737, "xantown", 923, 4);
        Story.MapItemQuest(1738, "xantown", 924);
        Story.MapItemQuest(1739, "xantown", 925);
    }

    public void ScoriasQuestsQuests()
    {
        if (Core.isCompletedBefore(1740))
            return;

        Story.PreLoad(this);

        Story.KillQuest(1740, "xantown", "Xan");
    }

    public void WarlicsQuests()
    {
        if (Core.isCompletedBefore(2157))
            return;

        Story.PreLoad(this);

        Story.MapItemQuest(2151, "xancave", 1220);
        Story.KillQuest(2152, "xancave", "Lava Goblin");
        Story.MapItemQuest(2153, "xancave", 1221);
        Story.KillQuest(2154, "xancave", "Lava Goblin");
        Story.MapItemQuest(2155, "xancave", 1223, 8);
        Story.KillQuest(2155, "xancave", new[] { "Lava Goblin", "Lava Goblin" });
        Story.KillQuest(2156, "xancave", "Shurpu Ring Guardian");
        Story.MapItemQuest(2157, "xancave", 1222);
    }
}