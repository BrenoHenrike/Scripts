//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs

using RBot;

public class DoomVaultA 
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreDailys Dailys = new CoreDailys();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        StoryLine();

        Core.SetOptions(false);
    }
    
    public void StoryLine()
    {
        // the challenge begins
        Core.KillQuest(QuestID: 2952, MapName: "doomvault", MonsterName: "Grim Soldier");

        //fight to survive
        Core.KillQuest(QuestID: 2953, MapName: "doomvault", MonsterName: "Grim Fighter");

        // the battle's heating up
        Core.KillQuest(QuestID: 2954, MapName: "doomvault", MonsterName: "Grim Fire Mage");

        // a close shave
        Core.KillQuest(QuestID: 2955, MapName: "doomvault", MonsterName: "Grim Shelleton", FollowupIDOverwrite: 2965);

        // eye spy a victim
        Core.KillQuest(QuestID: 2965, MapName: "doomvault", MonsterName: "Flying Spyball");

        // help me!
        Core.KillQuest(QuestID: 2966, MapName: "doomvault", MonsterName: "Princess Angler");

        // get your hands dirty
        Core.KillQuest(QuestID: 2967, MapName: "doomvault", MonsterName: "Grim Ectomancer");

        // a rocky battle
        Core.KillQuest(QuestID: 2968, MapName: "doomvault", MonsterName: "Fallen Light Statue");

        //soul-d of defeat
        Core.KillQuest(QuestID: 2969, MapName: "doomvault", MonsterName: "Grim Soldier");

        //the key to help me
        Core.KillQuest(QuestID: 2970, MapName: "doomvault", MonsterName: "Grim Shelleton");

        //help me again!
        Core.KillQuest(QuestID: 2971, MapName: "doomvault", MonsterName: "Princess Angler", FollowupIDOverwrite: 2974);

        //overheated hero
        Core.KillQuest(QuestID: 2974, MapName: "doomvault", MonsterName: "Grim Fire Mage", FollowupIDOverwrite: 2981);

        //the blade-breaker
        Core.KillQuest(QuestID: 2981, MapName: "doomvault", MonsterName: "Grim Lich");

        //anti-magic warrior
        Core.KillQuest(QuestID: 2982, MapName: "doomvault", MonsterName: "Grim Fighter");

        //elemental destroyer
        Core.KillQuest(QuestID: 2983, MapName: "doomvault", MonsterName: "Grim Ectomancer", FollowupIDOverwrite: 3006);

        //the unkillable
        Core.KillQuest(QuestID: 3006, MapName: "doomvault", MonsterName: "Grim Shelleton");

        //key to victory
        Core.KillQuest(QuestID: 3007, MapName: "doomvault", MonsterName: "Fallen Light Statue");

        //i command you, help me!
        Core.KillQuest(QuestID: 3008, MapName: "doomvault", MonsterName: "Ghost King Angler", hasFollowup: false);

    }
}