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
        Volcano();
        AndesisQuests();
        ScoriasQuestsQuests();
        WarlicsQuests();
        Dailys.Pyromancer();
    }

    public void Volcano()
    {
        if (Bot.Quests.IsUnlocked(1733))
        return;

        //Core.KillQuest(QuestID: , MapName: "volcano", MonsterName: "");

        //Level 1
        Core.KillQuest(QuestID: 1242, MapName: "volcano", MonsterName: "Lava Golem");
        //Level 2
            //Core.HuntMonster("volcano", "Fire Imp", "Broiled Clue", 7, true);  
        Core.MapItemQuest(QuestID: 1243, MapName: "volcano", MapItemID: 526, Amount: 3, hasFollowup: false);   
        Core.KillQuest(QuestID: 1243, MapName: "volcano", MonsterName: "Fire Imp");   
        //Level 3
        Core.KillQuest(QuestID: 1244, MapName: "volcano", MonsterName: "Fire Imp");
        //Level 4
            //Core.HuntMonster("volcano", "Lava Golem", "Blazing Clue", 10, true);
        Core.MapItemQuest(QuestID: 1245, MapName: "volcano", MapItemID: 527, Amount: 8, hasFollowup: false);  
        Core.KillQuest(QuestID: 1245, MapName: "volcano", MonsterName: "Lava Golem");  
        //Level 5
        Core.KillQuest(QuestID: 1246, MapName: "volcano", MonsterName: "Fire Imp");
        //Level 6
        Core.KillQuest(QuestID: 1247, MapName: "volcano", MonsterName: "Fire Imp");
        //Level 7
        Core.MapItemQuest(QuestID: 1248, MapName: "volcano", MapItemID: 528, Amount: 12);
        //Level 8
        Core.KillQuest(QuestID: 1249, MapName: "volcano", MonsterName: "Flamethrower Dwakel|Lava Golem");
        //Level 9
            //Core.HuntMonster("volcano", "Fire Imp", "Torched Clue", 10, true);
        Core.MapItemQuest(QuestID: 1250, MapName: "volcano", MapItemID: 529, Amount: 8, hasFollowup: false);
        Core.KillQuest(QuestID: 1250, MapName: "volcano", MonsterName: "Fire Imp");
        //Level 10
        Core.KillQuest(QuestID: 1251, MapName: "volcano", MonsterName: "Flamethrower Dwakel");
        //Level 11
        Core.MapItemQuest(QuestID: 1252, MapName: "volcano", MapItemID: 530, Amount: 10, hasFollowup: false);
        Core.KillQuest(QuestID: 1252, MapName: "volcano", MonsterName: "Fire Imp");
        //Level 12
        Core.KillQuest(QuestID: 1253, MapName: "volcano", MonsterName: "Flame Elemental|Lava Golem");
        //Level 13
        Core.MapItemQuest(QuestID: 1254, MapName: "volcano", MapItemID: 531, Amount: 12);
        //Level 14
        Core.KillQuest(QuestID: 1255, MapName: "volcano", MonsterName: "Fire Imp");
        //Level 15
        Core.KillQuest(QuestID: 1256, MapName: "volcano", MonsterName: "Fire Imp");
        //Level 16
        Core.MapItemQuest(QuestID: 1257, MapName: "volcano", MapItemID: 532, Amount: 10);
        //Level 17
        Core.KillQuest(QuestID: 1258, MapName: "volcano", MonsterName: "Flamethrower Dwakel");
        //Level 18 
        Core.KillQuest(QuestID: 1259, MapName: "volcano", MonsterName: "Fire Imp|Flame Elemental");
            // Core.HuntMonster("volcano", "Fire Imp", "Singed Clue", 7);
            // Core.HuntMonster("volcano", "Fire Imp", "Flame Elementa", 15, true);
        Core.EnsureComplete(1141);
        //Level 19
            //Core.HuntMonster("volcano", "Fire Imp", "Smoking Clue", 13);
        Core.MapItemQuest(QuestID: 1260, MapName: "volcano", MapItemID: 533, Amount: 6, hasFollowup: false);
        Core.KillQuest(QuestID: 1260, MapName: "volcano", MonsterName: "Fire Imp");
        //Level 20
        Core.KillQuest(QuestID: 1261, MapName: "volcano", MonsterName: "Magman", hasFollowup: false);
    }

    public void AndesisQuests()
    {
        if (Bot.Quests.IsUnlocked(1740))
        return;

        // /join xantown

        //A town devided
        // The Mill is Sealed x1
        // Go to the Mill at Screen 10 of Basani.
        Core.MapItemQuest(QuestID: 1733, MapName: "xantown", MapItemID: 922, Amount: 1, hasFollowup: true);
        //The Milelr's Key
        // The Mill Key x1
        // Dropped by Lava Golem
        // Dropped by Fire Imp (2)
        Core.KillQuest(QuestID: 1734, MapName: "xantown", MonsterName: "Fire Imp");

        //trailblazer
        // Fire Monster Cleared x12
        // Dropped by Fire Imp(2)
        // Dropped by Lava Golem
        Core.KillQuest(QuestID: 1735, MapName: "xantown", MonsterName: "Fire Imp");

        //fire brigade of one
        //The Only Bucket In Town x1
        // Dropped by Fire Imp(2)
        // Dropped by Lava Golem
        Core.KillQuest(QuestID: 1736, MapName: "xantown", MonsterName: "Fire Imp");

        //fire control
        // Blazing Inferno Doused x4
        // Click on the arrows around the map
        Core.MapItemQuest(QuestID: 1737, MapName: "xantown", MapItemID: 923, Amount: 4);

        //andesis's famly pendant
        //Andesi's Pendant x1
        // Go in House 26 on Screen 7 and click on the floor boards on Screen 9 of Basani.
        Core.MapItemQuest(QuestID: 1738, MapName: "xantown", MapItemID: 924);

        //signed, seared, delivered
        // Deliver Pendant to Scoria x1
        // Go to Screen 10 of Basani
        Core.MapItemQuest(QuestID: 1739, MapName: "xantown", MapItemID: 925, hasFollowup: false);
    }

    public void ScoriasQuestsQuests()
    {
        if (Bot.Quests.IsUnlocked(2151))
        return;

        // /join xantown

        //The Xan With The Plan - 1740
        // Get Some Xanswers x1
        // Dropped by Xan
        Core.KillQuest(QuestID: 1740, MapName: "xantown", MonsterName: "Xan", hasFollowup: false);

    }

    public void WarlicsQuests()
    {
        if (Bot.Quests.IsUnlocked(2209))
        return;

        // /Join xancave

        //locate the sealed library - QuestID
        // Library Door Located! x1
        // Go to the massive metal door on Screen 4.
        Core.MapItemQuest(QuestID: 2151, MapName: "xancave", MapItemID: 1220);

        //a powered library lockv
        // Shurpu Fire Gem x10
        // Dropped by Lava Goblin
        // Dropped by Lava Mage (Version 1)
        Core.KillQuest(QuestID: 2152, MapName: "xancave", MonsterName: "Lava Goblin");

        //lumiante the library lock - QuestID
        // Library Door Unlocked x1
        // Go to the massive metal door and click on gold arrows untill all three orange lights on the doors are turned on.
        Core.MapItemQuest(QuestID: 2153, MapName: "xancave", MapItemID: 1221);

        //defend the lobrary! - QuestID
        // Monster Cleared x15
        // Dropped by Lava Goblin
        // Dropped by Lava Mage (Version 1)
        Core.KillQuest(QuestID: 2154, MapName: "xancave", MonsterName: "Lava Goblin");

        //crossing over - QuestID
        // Fire Salts x10
        // Dropped by Lava Goblin
        // Dropped by Lava Mage (Version 1)
        // Fire Wire x10
        // Dropped by Lava Goblin
        // Dropped by Lava Mage (Version 1)
        // Board x8
        // Click on blue arrows around the map.
        Core.MapItemQuest(QuestID: 2155, MapName: "xancave", MapItemID: 1223, Amount: 8, hasFollowup: false);
        Core.KillQuest(QuestID: 2155, MapName: "xancave", MonsterName: "Lava Goblin");


        //guardian of shurpu - QuestID
        // Ring of Shurpu x1
        // Dropped by Shurpu Ring Guardian
        Core.KillQuest(QuestID: 2156, MapName: "xancave", MonsterName: "Shurpu Ring Guardian");

        //face xan - QuestID
        // Here's Xan x1
        // Go to Last screen
        Core.MapItemQuest(QuestID: 2157, MapName: "xancave", MapItemID: 1222, hasFollowup: false);
    }
}