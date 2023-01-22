/*
name: null
description: null
tags: null
*/
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

        Core.AcceptandCompleteTries = 1;

        // The Challenge Begins 2952
        Story.KillQuest(2952, "doomvault", "Grim Soldier");

        // Fight to Survive 2953
        Story.KillQuest(2953, "doomvault", "Grim Fighter");

        // The Battle's Heating Up 2954
        Story.KillQuest(2954, "doomvault", "Grim Fire Mage");

        // A Close Shave 2955
        Story.KillQuest(2955, "doomvault", "Grim Shelleton");

        // Eye Spy a Victim 2965
        Story.KillQuest(2965, "doomvault", "Flying Spyball");

        // Help Me! 2966
        if (!Story.QuestProgression(2966))
        {
            Core.EnsureAccept(2966);
            Core.HuntMonsterMapID("doomvault", 21, "Hand of the Princess");
            Core.EnsureComplete(2966);
        }

        // Get Your Hands Dirty 2967
        Story.KillQuest(2967, "doomvault", "Grim Ectomancer");

        // A Rocky Battle 2968
        Story.KillQuest(2968, "doomvault", "Fallen Light Statue");

        // Soul-d on Defeat 2969
        Story.KillQuest(2969, "doomvault", "Grim Souldier");

        // The Key to Help Me 2970
        Story.KillQuest(2970, "doomvault", "Grim Shelleton");

        // Help Me Again! 2971
        if (!Story.QuestProgression(2971))
        {
            Core.EnsureAccept(2971);
            Core.HuntMonsterMapID("doomvault", 37, "Angler Slain");
            Core.EnsureComplete(2971);
        }

        // Overheated Hero 2974
        Story.KillQuest(2974, "doomvault", "Grim Fire Mage");

        // The Blade-breaker 2981
        Story.KillQuest(2981, "doomvault", "Grim Lich");

        // Anti-Magic Warrior 2982
        Story.KillQuest(2982, "doomvault", "Grim Fighter");

        // Elemental Destroyer 2983
        Story.KillQuest(2983, "doomvault", "Grim Ectomancer");

        // The Unkillable 3006
        Story.KillQuest(3006, "doomvault", "Grim Shelleton");

        // Key to Victory 3007
        Story.KillQuest(3007, "doomvault", "Fallen Light Statue");

        // I Command You, Help Me! 3008
        Story.KillQuest(3008, "doomvault", "Ghost King Angler");
    }
}
