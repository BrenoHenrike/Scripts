//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
public class DoomVaultA
{
    public IScriptInterface Bot => IScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();
    public CoreAdvanced Adv = new();
    public CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        StoryLine();

        Core.SetOptions(false);
    }

    public void StoryLine()
    {
        if (Core.isCompletedBefore(3008))
            return;

        Story.PreLoad(this);

        Bot.Quests.UpdateQuest(3008);

        // the challenge begins
        Story.KillQuest(2952, "doomvault", "Grim Soldier");

        //fight to survive
        Story.KillQuest(2953, "doomvault", "Grim Fighter");


        // the battle's heating up
        Story.KillQuest(2954, "doomvault", "Grim Fire Mage");


        // a close shave
        Story.KillQuest(2955, "doomvault", "Grim Shelleton");


        // eye spy a victim
        Story.KillQuest(2965, "doomvault", "Flying Spyball");


        // help me!
        if (!Story.QuestProgression(2966))
        {
            Core.EnsureAccept(2966);
            Core.HuntMonsterMapID("doomvault", 21, "Hand of the Princess");
            Core.EnsureComplete(2966);
        }

        // get your hands dirty
        Story.KillQuest(2967, "doomvault", "Grim Ectomancer");


        // a rocky battle
        Story.KillQuest(2968, "doomvault", "Fallen Light Statue");

        //soul-d of defeat
        Story.KillQuest(2969, "doomvault", "Grim Soldier");

        //the key to help me
        Story.KillQuest(2970, "doomvault", "Grim Shelleton");

        //help me again!
        if (!Story.QuestProgression(2971))
        {
            Core.EnsureAccept(2971);
            Core.HuntMonsterMapID("doomvault", 37, "Angler Slain");
            Core.EnsureComplete(2971);
        }

        //overheated hero
        Story.KillQuest(2974, "doomvault", "Grim Fire Mage");

        //the blade-breaker
        Story.KillQuest(2981, "doomvault", "Grim Lich");

        //anti-magic warrior
        Story.KillQuest(2982, "doomvault", "Grim Fighter");

        //elemental destroyer
        Story.KillQuest(2983, "doomvault", "Grim Ectomancer");

        //the unkillable
        Story.KillQuest(3006, "doomvault", "Grim Shelleton");

        //key to victory
        Story.KillQuest(3007, "doomvault", "Fallen Light Statue");

        //i command you, help me!
        Story.KillQuest(3008, "doomvault", "Ghost King Angler");
    }
}