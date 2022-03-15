//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;
public class DoomVaultA
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        StoryLine();

        Core.SetOptions(false);
    }

    public void StoryLine()
    {
        if (Core.isCompletedBefore(3008))
            return;
        Core.AcceptandCompleteTries = 1;

        Story.PreLoad();

        // the challenge begins
        Story.KillQuest(QuestID: 2952, MapName: "doomvault", MonsterName: "Grim Soldier");
        Bot.Quests.UpdateQuest(3008);

        //fight to survive
        Story.KillQuest(QuestID: 2953, MapName: "doomvault", MonsterName: "Grim Fighter");
        Bot.Quests.UpdateQuest(3008);

        // the battle's heating up
        Story.KillQuest(QuestID: 2954, MapName: "doomvault", MonsterName: "Grim Fire Mage");
        Bot.Quests.UpdateQuest(3008);

        // a close shave
        Story.KillQuest(QuestID: 2955, MapName: "doomvault", MonsterName: "Grim Shelleton");
        Bot.Quests.UpdateQuest(3008);

        // eye spy a victim
        Story.KillQuest(QuestID: 2965, MapName: "doomvault", MonsterName: "Flying Spyball");
        Bot.Quests.UpdateQuest(3008);

        // help me!
        Story.KillQuest(QuestID: 2966, MapName: "doomvault", MonsterName: "Princess Angler");
        Bot.Quests.UpdateQuest(3008);

        // get your hands dirty
        Story.KillQuest(QuestID: 2967, MapName: "doomvault", MonsterName: "Grim Ectomancer");
        Bot.Quests.UpdateQuest(3008);

        // a rocky battle
        Story.KillQuest(QuestID: 2968, MapName: "doomvault", MonsterName: "Fallen Light Statue");
        Bot.Quests.UpdateQuest(3008);

        //soul-d of defeat
        Story.KillQuest(QuestID: 2969, MapName: "doomvault", MonsterName: "Grim Soldier");
        Bot.Quests.UpdateQuest(3008);

        //the key to help me
        Story.KillQuest(QuestID: 2970, MapName: "doomvault", MonsterName: "Grim Shelleton");
        Bot.Quests.UpdateQuest(3008);

        //help me again!
        Story.KillQuest(QuestID: 2971, MapName: "doomvault", MonsterName: "Princess Angler");
        Bot.Quests.UpdateQuest(3008);

        //overheated hero
        Story.KillQuest(QuestID: 2974, MapName: "doomvault", MonsterName: "Grim Fire Mage");
        Bot.Quests.UpdateQuest(3008);

        //the blade-breaker
        Story.KillQuest(QuestID: 2981, MapName: "doomvault", MonsterName: "Grim Lich");
        Bot.Quests.UpdateQuest(3008);

        //anti-magic warrior
        Story.KillQuest(QuestID: 2982, MapName: "doomvault", MonsterName: "Grim Fighter");
        Bot.Quests.UpdateQuest(3008);

        //elemental destroyer
        Story.KillQuest(QuestID: 2983, MapName: "doomvault", MonsterName: "Grim Ectomancer");
        Bot.Quests.UpdateQuest(3008);

        //the unkillable
        Story.KillQuest(QuestID: 3006, MapName: "doomvault", MonsterName: "Grim Shelleton");
        Bot.Quests.UpdateQuest(3008);

        //key to victory
        Story.KillQuest(QuestID: 3007, MapName: "doomvault", MonsterName: "Fallen Light Statue");
        Bot.Quests.UpdateQuest(3008);

        //i command you, help me!
        Story.KillQuest(QuestID: 3008, MapName: "doomvault", MonsterName: "Ghost King Angler");
    }
}
