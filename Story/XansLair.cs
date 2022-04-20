//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class XansLair
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
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

        Story.PreLoad();

        Story.KillQuest(1242, MapName: "volcano", MonsterName: "Lava Golem");
        Story.KillQuest(1243, MapName: "volcano", MonsterName: "Fire Imp");
        Story.MapItemQuest(1243, MapName: "volcano", MapItemID: 526, Amount: 3);
        Story.KillQuest(1244, MapName: "volcano", MonsterName: "Fire Imp");
        Story.KillQuest(1245, MapName: "volcano", MonsterName: "Lava Golem");
        Story.MapItemQuest(1245, MapName: "volcano", MapItemID: 527, Amount: 8);
        Story.KillQuest(1246, MapName: "volcano", MonsterName: "Fire Imp");
        Story.KillQuest(1247, MapName: "volcano", MonsterName: "Fire Imp");
        Story.MapItemQuest(1248, MapName: "volcano", MapItemID: 528, Amount: 12);
        Story.KillQuest(1249, MapName: "volcano", MonsterName: "Flamethrower Dwakel");
        Story.KillQuest(1250, MapName: "volcano", MonsterName: "Fire Imp");
        Story.MapItemQuest(1250, MapName: "volcano", MapItemID: 529, Amount: 8);
        Story.KillQuest(1251, MapName: "volcano", MonsterName: "Flamethrower Dwakel");
        Story.KillQuest(1252, MapName: "volcano", MonsterName: "Fire Imp");
        Story.MapItemQuest(1252, MapName: "volcano", MapItemID: 530, Amount: 10);
        Story.KillQuest(1253, MapName: "volcano", MonsterName: "Flame Elemental");
        Story.MapItemQuest(1254, MapName: "volcano", MapItemID: 531, Amount: 12);
        Story.KillQuest(1255, MapName: "volcano", MonsterName: "Fire Imp");
        Story.KillQuest(1256, MapName: "volcano", MonsterName: "Fire Imp");
        Story.MapItemQuest(1257, MapName: "volcano", MapItemID: 532, Amount: 10);
        Story.KillQuest(1258, MapName: "volcano", MonsterName: "Flamethrower Dwakel");
        Story.KillQuest(1259, MapName: "volcano", MonsterNames: new[] { "Fire Imp", "Flame Elemental" });
        Story.KillQuest(1260, MapName: "volcano", MonsterName: "Fire Imp");
        Story.MapItemQuest(1260, MapName: "volcano", MapItemID: 533, Amount: 6);
        Story.KillQuest(1261, MapName: "volcano", MonsterName: "Magman");
    }

    public void AndesisQuests()
    {
        if (Core.isCompletedBefore(1739))
            return;

        Story.PreLoad();

        Story.MapItemQuest(1733, MapName: "xantown", MapItemID: 922);
        Story.KillQuest(1734, MapName: "xantown", MonsterName: "Fire Imp");
        Story.KillQuest(1735, MapName: "xantown", MonsterName: "Fire Imp");
        Story.KillQuest(1736, MapName: "xantown", MonsterName: "Fire Imp");
        Story.MapItemQuest(1737, MapName: "xantown", MapItemID: 923, Amount: 4);
        Story.MapItemQuest(1738, MapName: "xantown", MapItemID: 924);
        Story.MapItemQuest(1739, MapName: "xantown", MapItemID: 925);
    }

    public void ScoriasQuestsQuests()
    {
        if (Core.isCompletedBefore(1740))
            return;

        Story.PreLoad();

        Story.KillQuest(1740, MapName: "xantown", MonsterName: "Xan");
    }

    public void WarlicsQuests()
    {
        if (Core.isCompletedBefore(2157))
            return;

        Story.PreLoad();

        Story.MapItemQuest(2151, MapName: "xancave", MapItemID: 1220);
        Story.KillQuest(2152, MapName: "xancave", MonsterName: "Lava Goblin");
        Story.MapItemQuest(2153, MapName: "xancave", MapItemID: 1221);
        Story.KillQuest(2154, MapName: "xancave", MonsterName: "Lava Goblin");
        Story.KillQuest(2155, MapName: "xancave", MonsterName: "Lava Goblin");
        Story.KillQuest(2155, MapName: "xancave", MonsterName: "Lava Goblin");
        Story.MapItemQuest(2155, MapName: "xancave", MapItemID: 1223, Amount: 8);
        Story.KillQuest(2156, MapName: "xancave", MonsterName: "Shurpu Ring Guardian");
        Story.MapItemQuest(2157, MapName: "xancave", MapItemID: 1222);
    }
}