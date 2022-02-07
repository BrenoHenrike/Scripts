//cs_include Scripts/CoreBots.cs

using Rbot;

public class SagaChaosFinale
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Core.AcceptandCompleteTries = 5;
        StoryLine();

        Core.SetOptions(false);
    }
    public void StoryLine()
    {
        if (Core.QuestProgression(3881))
            return;

        //12 Lords of Chaos
        Core.Join("mountdoomskull");
        Core.ChainQuest(3578);

        // Prologue: Good vs Evil
        Core.Join("mountdoomskull");
        Core.ChainQuest(3579);

        // 1st Lord of Chaos
        Core.Join("mountdoomskull");
        Core.ChainQuest(3580);

        // // 2nd Lord of Chaos
        // Core.Join("mountdoomskull");
        // Core.ChainQuest(3590);

        // 3rd Lord of Chaos
        Core.Join("mountdoomskull");
        Core.ChainQuest(3581);

        // 4th Lord of Chaos
        Core.Join("mountdoomskull");
        Core.ChainQuest(3582);

        // 5th Lord of Chaos
        Core.Join("mountdoomskull");
        Core.ChainQuest(3583);

        // 6th Lord of Chaos
        Core.Join("mountdoomskull");
        Core.ChainQuest(3584);

        // 7th Lord of Chaos
        Core.Join("mountdoomskull");
        Core.ChainQuest(3585);

        // 8th Lord of Chaos
        Core.Join("mountdoomskull");
        Core.ChainQuest(3586);

        // 9th Lord of Chaos
        Core.Join("mountdoomskull");
        Core.ChainQuest(3587);

        // 10th Lord of Chaos
        Core.Join("mountdoomskull");
        Core.ChainQuest(3588);

        // 11th Lord of Chaos
        Core.Join("mountdoomskull");
        Core.ChainQuest(3589);

        // 2nd Lord of Chaos
        Core.Join("mountdoomskull");
        Core.ChainQuest(3590);

        // 12th Lord of Chaos
        Core.Join("mountdoomskull");
        Core.ChainQuest(3591, FollowupIDOverwrite: 3764);

        // Mountain Top Reached
        Core.Join("mountdoomskull");
        Core.ChainQuest(3764);

        // Drakath Faced
        Core.GetMapItem(QuestID: 3765, MapItemID: 2726, MapName: "mountdoomskull");

        // Who is the 13th Lord of Chaos?
        Core.Join("mountdoomskull");
        Core.ChainQuest(3766, FollowupIDOverwrite: 3779);

        // World War Lore!
        Core.Join("newfinale");
        Core.ChainQuest(3779, FollowupIDOverwrite: 3781);

        // Battle for Chaos in Willowcreek!
        Core.KillQuest(3781, "newfinale", "Chaos Healer", FollowupIDOverwrite: 3788);

        // Defeat the Chaos Challenger
        Core.KillQuest(3788, "newfinale", "Chaos Challenger", FollowupIDOverwrite: 3783);

        // Battle for Chaos in Doomwood!
        Core.KillQuest(3783, "newfinale", "Chaos Virago", FollowupIDOverwrite: 3789);

        // Beat Chaorrupted Lycan Hunter
        Core.KillQuest(3789, "newfinale", "Chaorrupted Lycan Hunter", FollowupIDOverwrite: 3785);

        // Battle for Chaos in Darkovia!
        Core.KillQuest(3785, "newfinale", "Shadow Slayer", FollowupIDOverwrite: 3790);

        // Defeat the Memory of Vampires
        Core.KillQuest(3790, "newfinale", "Memory of Vampires", FollowupIDOverwrite: 3787);

        // Battle for Chaos in the Lair!
        Core.KillQuest(3787, "newfinale", "Chaotic Virago", FollowupIDOverwrite: 3608);

        // 1st Chaos Beast
        Core.Join("chaosbeast");
        Core.ChainQuest(3608, FollowupIDOverwrite: 3618);

        // 2nd Chaos Beast
        Core.Join("chaosbeast");
        Core.ChainQuest(3618, FollowupIDOverwrite: 3609);

        // 3rd Chaos Beast
        Core.Join("chaosbeast");
        Core.ChainQuest(3609);

        // 4th Chaos Beast
        Core.Join("chaosbeast");
        Core.ChainQuest(3610);

        // 5th Chaos Beast
        Core.Join("chaosbeast");
        Core.ChainQuest(3611);

        // 6th Chaos Beast
        Core.Join("chaosbeast");
        Core.ChainQuest(3612);

        // 7th Chaos Beast
        Core.Join("chaosbeast");
        Core.ChainQuest(3613);

        // 8th Chaos Beast
        Core.join(chaosbeast);
        Core.ChainQuest(3614);

        // 9th Chaos Beast
        Core.Join("chaosbeast");
        Core.ChainQuest(3615);

        // 10th Chaos Beast
        Core.Join("chaosbeast");
        Core.ChainQuest(3616);

        // 11th Chaos Beast
        Core.Join("chaosbeast");
        Core.ChainQuest(3617, FollowupIDOverwrite: 3619);

        // 12th Chaos Beast
        Core.Join("chaosbeast");
        Core.ChainQuest(3619, FollowupIDOverwrite: 3792);

        // Time to save Battleon!
        Core.Join("newfinale");
        Core.ChainQuest(3792, FollowupIDOverwrite: 3794);

        // Battle for Chaos in Battleon!
        Core.KillQuest(3794, "newfinale", "Alliance Soldier");

        // Battle the Champion of Chaos!
        Core.GetMapItem(QuestID: 3795, MapItemID: 2894, MapName: "drakathfight", FollowupIDOverwrite: 3620);

        // REUSE
        Core.KillQuest(QuestID: 3620, "shadowrise", "Broken Bones|Darkness Elemental|Dry Ice Mage", FollowupIDOverwrite: 3796);

        // Search for Death's Lair
        Core.GetMapItem(QuestID: 3796, MapItemID: 2895, MapName: "shadowrise");

        // Arrive in Shadowattack
        Core.Join("shadowattack");
        Core.ChainQuest(3797);

        // Find your way to Death's lair
        Core.GetMapItem(QuestID: 3798, MapItemID: 2896, MapName: "shadowattack");

        // Beat Death!
        Core.KillQuest(QuestID: 3799, "shadowattack", "Death", FollowupIDOverwrite: 3875);

        // Enter Confrontation
        Core.Join("confrontation");
        Core.ChainQuest(3875);

        // Defeat Drakath!
        Core.KillQuest(3876, "finalbattle", "Drakath");

        // Defeat Drakath... again!
        Core.KillQuest(3877, "finalbattle", "Drakath");

        // Defeat Drakath!
        Core.KillQuest(3878, "finalbattle", "Drakath");

        // Defeat the 12 Lords of Chaos!
        Core.KillQuest(QuestID: 3879, "chaosrealm", "Alteon");

        // Defeat the 13th Lord of Chaos
        Core.KillQuest(QuestID: 3880, "chaoslord", "*");

        // The Final Showdown!
        Core.KillQuest(QuestID: 3881, "finalshowdown", "Prince Drakath", hasFollowup: false);
    }
}