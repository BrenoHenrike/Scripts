//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs


using RBot;

public class NecroDungeon
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreDailys Dailys = new CoreDailys();


    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        NecrodungeonStoryLine();

        Core.SetOptions(false);
    }

    public void NecrodungeonStoryLine()
    {
        if (Core.isCompletedBefore(2061))
            return;

        //descent into darkness - 2044
        Core.KillQuest(QuestID: 2044, MapName: "necrodungeon", MonsterName: "Bellhop");
        //retrieve the past, room 1 - 2045
        Core.KillQuest(QuestID: 2045, MapName: "necrodungeon", MonsterName: "Necropolis Soldier");
        Core.MapItemQuest(QuestID: 2045, MapName: "necrodungeon", MapItemID: 1001);
        //retrieve the past, room 2 - 2046
        Core.MapItemQuest(QuestID: 2046, MapName: "necrodungeon", MapItemID: 1015, Amount: 5);
        Core.MapItemQuest(QuestID: 2046, MapName: "necrodungeon", MapItemID: 1002);
        //retrieve the past, room 3 - 2047
        Core.KillQuest(QuestID: 2047, MapName: "necrodungeon", MonsterName: "SlimeSkull");
        Core.MapItemQuest(QuestID: 2047, MapName: "necrodungeon", MapItemID: 1003);
        //retrieve the past, room 4 - 2048
        Core.KillQuest(QuestID: 2048, MapName: "necrodungeon", MonsterNames: new[] { "Necropolis Soldier", "Ghoul" });
        Core.MapItemQuest(QuestID: 2048, MapName: "necrodungeon", MapItemID: 1004);
        //deeper into darkness - 2049
        Core.KillQuest(QuestID: 2049, MapName: "necrodungeon", MonsterName: "Bellhop");
        //retrieve the past, room 5 - 2050
        Core.KillQuest(QuestID: 2050, MapName: "necrodungeon", MonsterNames: new[] { "SlimeSkull", "Necropolis Soldier" });
        Core.MapItemQuest(QuestID: 2050, MapName: "necrodungeon", MapItemID: 1016, Amount: 3);
        Core.MapItemQuest(QuestID: 2050, MapName: "necrodungeon", MapItemID: 1005);
        //retrieve the past, room 6 - 2051
        Core.MapItemQuest(QuestID: 2051, MapName: "necrodungeon", MapItemID: 1017, Amount: 5);
        Core.MapItemQuest(QuestID: 2051, MapName: "necrodungeon", MapItemID: 1006);
        //retrieve the past, room 7 - 2052
        Core.KillQuest(QuestID: 2052, MapName: "necrodungeon", MonsterName: "SlimeSkull");
        Core.MapItemQuest(QuestID: 2052, MapName: "necrodungeon", MapItemID: 1007);
        //monter subway ahead - 2053
        Core.KillQuest(QuestID: 2053, MapName: "necrodungeon", MonsterName: "Doom Crawler");
        Core.MapItemQuest(QuestID: 2053, MapName: "necrodungeon", MapItemID: 1008);
        //underground railroad to... DOOM! - 2054
        Core.KillQuest(QuestID: 2054, MapName: "necrodungeon", MonsterName: "Ghoul");
        Core.MapItemQuest(QuestID: 2054, MapName: "necrodungeon", MapItemID: 1018, Amount: 8);
        Core.MapItemQuest(QuestID: 2054, MapName: "necrodungeon", MapItemID: 1009);
        //retrieve the past, room 10 - 2055
        Core.KillQuest(QuestID: 2055, MapName: "necrodungeon", MonsterName: "Necropolis Soldier");
        Core.MapItemQuest(QuestID: 2055, MapName: "necrodungeon", MapItemID: 1010);
        //the deepest descent - 2056
        Core.KillQuest(QuestID: 2056, MapName: "necrodungeon", MonsterName: "Bellhop");
        //retrieve the past, room 11 - 2057
        Core.MapItemQuest(QuestID: 2057, MapName: "necrodungeon", MapItemID: 1016, Amount: 5);
        Core.MapItemQuest(QuestID: 2057, MapName: "necrodungeon", MapItemID: 1011);
        //retrieve the past, room 12 - 2058
        Core.KillQuest(QuestID: 2058, MapName: "necrodungeon", MonsterName: "Necropolis Soldier");
        Core.MapItemQuest(QuestID: 2058, MapName: "necrodungeon", MapItemID: 1012);
        //retrieve the past, room 13 - 2059
        Core.MapItemQuest(QuestID: 2059, MapName: "necrodungeon", MapItemID: 1019, Amount: 5);
        Core.MapItemQuest(QuestID: 2059, MapName: "necrodungeon", MapItemID: 1013);
        //five times the fury - 2060
        if (!Bot.Quests.IsUnlocked(2061))
        {
            Core.EnsureAccept(2060);
            Core.KillMonster("necrodungeon", "r22", "Down", 889);
            Core.KillMonster("necrodungeon", "r22", "Down", 890);
            Core.KillMonster("necrodungeon", "r22", "Down", 891);
            Core.KillMonster("necrodungeon", "r22", "Down", 892);
            Core.KillMonster("necrodungeon", "r22", "Down", 893);
            Core.EnsureComplete(2060);
        }
        //the past will haunt you - 2061
        Core.MapItemQuest(QuestID: 2061, MapName: "necrodungeon", MapItemID: 1020);
    }
}