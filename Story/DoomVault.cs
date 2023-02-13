/*
name: Doom Vault (A) Story
description: This will finish the Doom Vault (A) Story.
tags: story, quest, doom-vault-a
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
        Core.EquipClass(ClassType.Farm);

        // The Challenge Begins 2952
        if (!Story.QuestProgression(2952))
        {
            Core.EnsureAccept(2952);
            Core.HuntMonsterMapID("doomvault", 1, "Soldier Slain", 5, log: false);
            Core.EnsureComplete(2952);
        }

        // Fight to Survive 2953
        if (!Story.QuestProgression(2953))
        {
            Core.EnsureAccept(2953);
            Core.HuntMonsterMapID("doomvault", 4, "Fighter Slain", 5, log: false);
            Core.EnsureComplete(2953);
        }

        // The Battle's Heating Up 2954
        if (!Story.QuestProgression(2954))
        {
            Core.EnsureAccept(2954);
            Core.HuntMonsterMapID("doomvault", 6, "Mage Slain", 10, log: false);
            Core.EnsureComplete(2954);
        }

        // A Close Shave 2955
        if (!Story.QuestProgression(2955))
        {
            InitFix("r8");
            Core.EnsureAccept(2955);
            Core.HuntMonsterMapID("doomvault", 13, "Shelleton Slain", 8, log: false);
            Core.EnsureComplete(2955);
        }

        // Eye Spy a Victim 2965
        if (!Story.QuestProgression(2965))
        {
            InitFix("r10");
            Core.EnsureAccept(2965);
            Core.HuntMonsterMapID("doomvault", 18, "Spyball Slain", 6, log: false);
            Core.EnsureComplete(2965);
        }

        // Help Me! 2966
        if (!Story.QuestProgression(2966))
        {
            Core.EquipClass(ClassType.Solo);
            Core.EnsureAccept(2966);
            InitFix("r14");
            Core.HuntMonsterMapID("doomvault", 21, "Hand of the Princess", log: false);
            Core.EnsureComplete(2966);
            Core.EquipClass(ClassType.Farm);
        }

        // Get Your Hands Dirty 2967
        if (!Story.QuestProgression(2967))
        {
            Core.EnsureAccept(2967);
            Core.HuntMonsterMapID("doomvault", 22, "Ectomancer Slain", 10, log: false);
            Core.EnsureComplete(2967);
        }

        // A Rocky Battle 2968
        if (!Story.QuestProgression(2968))
        {
            Core.EnsureAccept(2968);
            Core.HuntMonsterMapID("doomvault", 25, "Stone Key", log: false);
            Core.EnsureComplete(2968);
        }

        // Soul-d on Defeat 2969
        if (!Story.QuestProgression(2969))
        {
            Core.EnsureAccept(2969);
            Core.HuntMonsterMapID("doomvault", 30, "Grim Soul", 50, log: false);
            Core.EnsureComplete(2969);
        }

        // The Key to Help Me 2970
        if (!Story.QuestProgression(2970))
        {
            Core.EnsureAccept(2970);
            Core.HuntMonsterMapID("doomvault", 34, "Princess Key", log: false);
            Core.EnsureComplete(2970);
        }

        // Help Me Again! 2971
        if (!Story.QuestProgression(2971))
        {
            Core.EquipClass(ClassType.Solo);
            Core.EnsureAccept(2971);
            InitFix("r24");
            Core.HuntMonsterMapID("doomvault", 37, "Angler Slain", log: false);
            Core.EnsureComplete(2971);
            Core.EquipClass(ClassType.Farm);
        }

        // Overheated Hero 2974
        if (!Story.QuestProgression(2974))
        {
            Core.EnsureAccept(2974);
            Core.HuntMonsterMapID("doomvault", 38, "Mage Slain", 5, log: false);
            Core.EnsureComplete(2974);
        }

        // The Blade-breaker 2981
        if (!Story.QuestProgression(2981))
        {
            Core.EnsureAccept(2981);
            Core.HuntMonsterMapID("doomvault", 45, "Lich Slain", 3, log: false);
            Core.EnsureComplete(2981);
        }

        // Anti-Magic Warrior 2982
        if (!Story.QuestProgression(2982))
        {
            Core.EnsureAccept(2982);
            Core.HuntMonsterMapID("doomvault", 48, "Fighter Slain", 7, log: false);
            Core.EnsureComplete(2982);
        }

        // Elemental Destroyer 2983
        if (!Story.QuestProgression(2983))
        {
            Core.EnsureAccept(2983);
            Core.HuntMonsterMapID("doomvault", 51, "Ectomancer Slain", 3, log: false);
            Core.EnsureComplete(2983);
        }

        // The Unkillable 3006
        if (!Story.QuestProgression(3006))
        {
            Core.EnsureAccept(3006);
            Core.HuntMonsterMapID("doomvault", 54, "Shelleton Slain", 3, log: false);
            Core.EnsureComplete(3006);
        }

        // Key to Victory 3007
        if (!Story.QuestProgression(3007))
        {
            Core.EnsureAccept(3007);
            Core.HuntMonsterMapID("doomvault", 58, "Raxgore's Key", log: false);
            Core.EnsureComplete(3007);
        }

        // I Command You, Help Me! 3008
        if (!Story.QuestProgression(3008))
        {
            Core.EquipClass(ClassType.Solo);
            Core.EnsureAccept(3008);
            InitFix("r36");
            Core.HuntMonsterMapID("doomvault", 60, "King Slayer", log: false);
            Core.EnsureComplete(3008);
        }
    }

    void InitFix(string cell, string pad = "Left")
    {
        Bot.Sleep(2500);
        if (Bot.Player.Cell != "init")
            return;

        Core.Logger("looks like the cutscene broke again, initiating cell fix");
        if (Bot.Map.Name != "doomvault")
            Core.Join("doomvault", cell, pad);
        while (!Bot.ShouldExit && Bot.Player.Cell != cell)
        {
            Core.Jump(cell);
            Bot.Sleep(Core.ActionDelay);
        }
        Core.Logger($"{Bot.Player.Cell} Fixed.");
    }
}