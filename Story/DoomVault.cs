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

        // the challenge begins
        Story.KillQuest(QuestID: 2952, MapName: "doomvault", MonsterName: "Grim Soldier");
        BypassPacket();

        //fight to survive
        Story.KillQuest(QuestID: 2953, MapName: "doomvault", MonsterName: "Grim Fighter");
        BypassPacket();

        // the battle's heating up
        Story.KillQuest(QuestID: 2954, MapName: "doomvault", MonsterName: "Grim Fire Mage");
        BypassPacket();

        // a close shave
        Story.KillQuest(QuestID: 2955, MapName: "doomvault", MonsterName: "Grim Shelleton");
        BypassPacket();

        // eye spy a victim
        Story.KillQuest(QuestID: 2965, MapName: "doomvault", MonsterName: "Flying Spyball");
        BypassPacket();

        // help me!
        Story.KillQuest(QuestID: 2966, MapName: "doomvault", MonsterName: "Princess Angler");
        BypassPacket();

        // get your hands dirty
        Story.KillQuest(QuestID: 2967, MapName: "doomvault", MonsterName: "Grim Ectomancer");
        BypassPacket();

        // a rocky battle
        Story.KillQuest(QuestID: 2968, MapName: "doomvault", MonsterName: "Fallen Light Statue");
        BypassPacket();

        //soul-d of defeat
        Story.KillQuest(QuestID: 2969, MapName: "doomvault", MonsterName: "Grim Soldier");
        BypassPacket();

        //the key to help me
        Story.KillQuest(QuestID: 2970, MapName: "doomvault", MonsterName: "Grim Shelleton");
        BypassPacket();

        //help me again!
        Story.KillQuest(QuestID: 2971, MapName: "doomvault", MonsterName: "Princess Angler");
        BypassPacket();

        //overheated hero
        Story.KillQuest(QuestID: 2974, MapName: "doomvault", MonsterName: "Grim Fire Mage");
        BypassPacket();

        //the blade-breaker
        Story.KillQuest(QuestID: 2981, MapName: "doomvault", MonsterName: "Grim Lich");
        BypassPacket();

        //anti-magic warrior
        Story.KillQuest(QuestID: 2982, MapName: "doomvault", MonsterName: "Grim Fighter");
        BypassPacket();

        //elemental destroyer
        Story.KillQuest(QuestID: 2983, MapName: "doomvault", MonsterName: "Grim Ectomancer");
        BypassPacket();

        //the unkillable
        Story.KillQuest(QuestID: 3006, MapName: "doomvault", MonsterName: "Grim Shelleton");
        BypassPacket();

        //key to victory
        Story.KillQuest(QuestID: 3007, MapName: "doomvault", MonsterName: "Fallen Light Statue");
        BypassPacket();

        //i command you, help me!
        Story.KillQuest(QuestID: 3008, MapName: "doomvault", MonsterName: "Ghost King Angler");
        BypassPacket();
    }

    private void BypassPacket()
        => Bot.SendClientPacket("{\"t\":\"xt\",\"b\":{\"r\":-1,\"o\":{\"cmd\":\"updateQuest\",\"iValue\":18,\"iIndex\":126}}}");
}
