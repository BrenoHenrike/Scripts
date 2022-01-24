//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs

//cs_include Scripts/CoreFile(Or folder)/Filename.cs

using RBot;

public class BotName
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreDailys Dailys = new CoreDailys();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        DoAll();

        Core.SetOptions(false);
    }

    public void DoAll()
    {
        Farm.Experience(20);
        Core.Logger("Volcano Quests");
        Volcano();
        Core.Logger("Andesis Quests");
        AndesisQuests();
        Core.Logger("Scorias Quests");
        ScoriasQuestsQuests();
        Core.Logger("Warlics Quests");
        WarlicsQuests();
    }

    public void Volcano()
    {
        if (Bot.Quests.IsUnlocked(1733))
        return;

        //Core.KillQuest(QuestID: , MapName: "volcano", MonsterName: "");

        Core.KillQuest(QuestID: 1242, MapName: "volcano", MonsterName: "Lava Golem");
        Core.KillQuest(QuestID: 1243, MapName: "volcano", MonsterName: "Fire Imp");   
        Core.MapItemQuest(QuestID: 1243, MapName: "volcano", MapItemID: 526, Amount: 3);  
        Core.KillQuest(QuestID: 1244, MapName: "volcano", MonsterName: "Fire Imp");
        Core.KillQuest(QuestID: 1245, MapName: "volcano", MonsterName: "Lava Golem");  
        Core.MapItemQuest(QuestID: 1245, MapName: "volcano", MapItemID: 527, Amount: 8);  
        Core.KillQuest(QuestID: 1246, MapName: "volcano", MonsterName: "Fire Imp");
        Core.KillQuest(QuestID: 1247, MapName: "volcano", MonsterName: "Fire Imp");
        Core.MapItemQuest(QuestID: 1248, MapName: "volcano", MapItemID: 528, Amount: 12);
        Core.KillQuest(QuestID: 1249, MapName: "volcano", MonsterName: "Flamethrower Dwakel|Lava Golem");
        Core.KillQuest(QuestID: 1250, MapName: "volcano", MonsterName: "Fire Imp");
        Core.MapItemQuest(QuestID: 1250, MapName: "volcano", MapItemID: 529, Amount: 8);
        Core.KillQuest(QuestID: 1251, MapName: "volcano", MonsterName: "Flamethrower Dwakel");
        Core.KillQuest(QuestID: 1252, MapName: "volcano", MonsterName: "Fire Imp");
        Core.MapItemQuest(QuestID: 1252, MapName: "volcano", MapItemID: 530, Amount: 10);
        Core.KillQuest(QuestID: 1253, MapName: "volcano", MonsterName: "Flame Elemental|Lava Golem");
        Core.MapItemQuest(QuestID: 1254, MapName: "volcano", MapItemID: 531, Amount: 12);
        Core.KillQuest(QuestID: 1255, MapName: "volcano", MonsterName: "Fire Imp");
        Core.KillQuest(QuestID: 1256, MapName: "volcano", MonsterName: "Fire Imp");
        Core.MapItemQuest(QuestID: 1257, MapName: "volcano", MapItemID: 532, Amount: 10);
        Core.KillQuest(QuestID: 1258, MapName: "volcano", MonsterName: "Flamethrower Dwakel"); 
        Core.KillQuest(QuestID: 1259, MapName: "volcano", MonsterNames: new[] {"Fire Imp", "Flame Elemental"});
        Core.KillQuest(QuestID: 1260, MapName: "volcano", MonsterName: "Fire Imp");
        Core.MapItemQuest(QuestID: 1260, MapName: "volcano", MapItemID: 533, Amount: 6);
        Core.KillQuest(QuestID: 1261, MapName: "volcano", MonsterName: "Magman", hasFollowup: false);
    }

    public void AndesisQuests()
    {
        if (Bot.Quests.IsUnlocked(1740))
        return;


        Core.MapItemQuest(QuestID: 1733, MapName: "xantown", MapItemID: 922);
        Core.KillQuest(QuestID: 1734, MapName: "xantown", MonsterName: "Fire Imp");
        Core.KillQuest(QuestID: 1735, MapName: "xantown", MonsterName: "Fire Imp");
        Core.KillQuest(QuestID: 1736, MapName: "xantown", MonsterName: "Fire Imp");
        Core.MapItemQuest(QuestID: 1737, MapName: "xantown", MapItemID: 923, Amount: 4);
        Core.MapItemQuest(QuestID: 1738, MapName: "xantown", MapItemID: 924);
        Core.MapItemQuest(QuestID: 1739, MapName: "xantown", MapItemID: 925, hasFollowup: false);
    }

    public void ScoriasQuestsQuests()
    {
        if (Bot.Quests.IsUnlocked(2151))
        return;


        Core.KillQuest(QuestID: 1740, MapName: "xantown", MonsterName: "Xan", hasFollowup: false);

    }

    public void WarlicsQuests()
    {
        if (Bot.Quests.IsUnlocked(2157))
        return;


        Core.MapItemQuest(QuestID: 2151, MapName: "xancave", MapItemID: 1220);
        Core.KillQuest(QuestID: 2152, MapName: "xancave", MonsterName: "Lava Goblin");
        Core.MapItemQuest(QuestID: 2153, MapName: "xancave", MapItemID: 1221);
        Core.KillQuest(QuestID: 2154, MapName: "xancave", MonsterName: "Lava Goblin");
        Core.KillQuest(QuestID: 2155, MapName: "xancave", MonsterName: "Lava Goblin");
        Core.KillQuest(QuestID: 2155, MapName: "xancave", MonsterName: "Lava Goblin");
        Core.MapItemQuest(QuestID: 2155, MapName: "xancave", MapItemID: 1223, Amount: 8);
        Core.KillQuest(QuestID: 2156, MapName: "xancave", MonsterName: "Shurpu Ring Guardian");
        Core.MapItemQuest(QuestID: 2157, MapName: "xancave", MapItemID: 1222, hasFollowup: false);
    }
}