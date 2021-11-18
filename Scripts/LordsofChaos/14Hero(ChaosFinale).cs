//cs_include Scripts/CoreBots.cs
using RBot;
using RBot.Options;
using System.Collections.Generic;

public class SagaChaosFinale
{
    public CoreBots Core => CoreBots.Instance;

    public int questStart = 0;

    public string OptionsStorage = "SagaChaosFinale";
    public bool DontPreconfigure = true;

    public List<IOption> Options = new List<IOption>()
    {
        new Option<int>("startQuest", "Quest start index", "This will save the progress through the script.")
    };

    public static readonly int[] qIDs =
    {
        3578, /* 0  - */
        3579, /* 1  - */
        3580, /* 2  - */
        3581, /* 3  - */
        3582, /* 4  - */
        3583, /* 5  - */
        3584, /* 6  - */
        3585, /* 7  - */
        3586, /* 8  - */
        3587, /* 9  - */
        3588, /* 10 - */
        3589, /* 11 - */
        3590, /* 12 - */
        3591, /* 13 - */
        3764, /* 14 - */
        3765, /* 15 - */
        3766, /* 16 - */
        3779, /* 17 - */
        3781, /* 18 - */
        3788, /* 19 - */
        3783, /* 20 - */
        3789, /* 21 - */
        3785, /* 22 - */
        3790, /* 23 - */
        3787, /* 24 - */
        3608, /* 25 - */
        3618, /* 26 - */
        3609, /* 27 - */
        3610, /* 28 - */
        3611, /* 29 - */
        3612, /* 30 - */
        3613, /* 31 - */
        3614, /* 32 - */
        3615, /* 33 - */
        3616, /* 34 - */
        3617, /* 35 - */
        3619, /* 36 - */
        3792, /* 37 - */
        3794, /* 38 - */
        3795, /* 39 - */
        3620, /* 40 - */
        3796, /* 41 - */
        3797, /* 42 - */
        3798, /* 43 - */
        3799, /* 44 - */
        3875, /* 45 - */
        3876, /* 46 - */
        3877, /* 47 - */
        3878, /* 48 - */
        3879, /* 49 - */
        3880, /* 50 - */
        3881  /* 51 - */
	};

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        questStart = bot.Config.Get<int>("startQuest");

        for (int i = questStart; i < qIDs.Length; i++)
        {
            bot.Config.Set("startQuest", i);
            Core.Logger($"Starting {i}");
            Core.EnsureAccept(qIDs[i]);
            switch (i)
            {
                case 0: //
                    bot.Player.Join("mountdoomskull");
                    Core.Jump("Cut1", "Left");
                    // bot.SendPacket("%xt%zm%tryQuestComplete%1275%3578%-1%false%wvz%");
                    break;
                case 1: // 
                    bot.Player.Join("mountdoomskull");
                    Core.Jump("Cut2", "Left");
                    // bot.SendPacket("%xt%zm%tryQuestComplete%1275%3579%-1%false%wvz%");
                    break;
                case 2: // 
                    bot.Player.Join("mountdoomskull");
                    Core.Jump("Cut3", "Left");
                    // bot.SendPacket("%xt%zm%tryQuestComplete%1275%3580%-1%false%wvz%");
                    break;
                case 3: // 
                    bot.Player.Join("mountdoomskull");
                    Core.Jump("Cut4", "Left");
                    // bot.SendPacket("%xt%zm%tryQuestComplete%1275%3581%-1%false%wvz%");
                    break;
                case 4: // 
                    bot.Player.Join("mountdoomskull");
                    Core.Jump("Cut5", "Left");
                    // bot.SendPacket("%xt%zm%tryQuestComplete%1275%3582%-1%false%wvz%");
                    break;
                case 5: // 
                    bot.Player.Join("mountdoomskull");
                    Core.Jump("Cut6", "Left");
                    // bot.SendPacket("%xt%zm%tryQuestComplete%1275%3583%-1%false%wvz%");
                    break;
                case 6: // 
                    bot.Player.Join("mountdoomskull");
                    Core.Jump("Cut7", "Left");
                    // bot.SendPacket("%xt%zm%tryQuestComplete%1275%3584%-1%false%wvz%");
                    break;
                case 7: // 
                    bot.Player.Join("mountdoomskull");
                    Core.Jump("Cut8", "Left");
                    // bot.SendPacket("%xt%zm%tryQuestComplete%1275%3585%-1%false%wvz%");
                    break;
                case 8: // 
                    bot.Player.Join("mountdoomskull");
                    Core.Jump("Cut9", "Left");
                    // bot.SendPacket("%xt%zm%tryQuestComplete%1275%3586%-1%false%wvz%");
                    break;
                case 9: // 
                    bot.Player.Join("mountdoomskull");
                    Core.Jump("Cut10", "Left");
                    // bot.SendPacket("%xt%zm%tryQuestComplete%1275%3587%-1%false%wvz%");
                    break;
                case 10: // 
                    bot.Player.Join("mountdoomskull");
                    Core.Jump("Cut11", "Left");
                    // bot.SendPacket("%xt%zm%tryQuestComplete%1275%3588%-1%false%wvz%");
                    break;
                case 11: // 
                    bot.Player.Join("mountdoomskull");
                    Core.Jump("Cut12", "Left");
                    // bot.SendPacket("%xt%zm%tryQuestComplete%1275%3589%-1%false%wvz%");
                    break;
                case 12: // 
                    bot.Player.Join("mountdoomskull");
                    Core.Jump("Cut13", "Left");
                    // bot.SendPacket("%xt%zm%tryQuestComplete%1275%3590%-1%false%wvz%");
                    break;
                case 13: // 
                    bot.Player.Join("mountdoomskull");
                    Core.Jump("Cut13a", "Left");
                    // bot.SendPacket("%xt%zm%tryQuestComplete%1275%3591%-1%false%wvz%");
                    break;
                case 14: // 
                    bot.Player.Join("mountdoomskull");
                    Core.Jump("NPC2", "Left");
                    // bot.SendPacket("%xt%zm%tryQuestComplete%1275%3764%-1%false%wvz%");
                    break;
                case 15: // 
                    Core.GetMapItem(2726, map: "mountdoomskull");
                    break;
                case 16: // 
                    bot.Player.Join("mountdoomskull");
                    break;
                case 17: // 
                    bot.Player.Join("newfinale");
                    break;
                case 18: // 
                    Core.SmartKillMonster(qIDs[i], "newfinale", "Chaos Healer");
                    break;
                case 19: // 
                    Core.SmartKillMonster(qIDs[i], "newfinale", "Chaos Challenger");
                    break;
                case 20: // 
                    Core.SmartKillMonster(qIDs[i], "newfinale", "Chaos Virago");
                    break;
                case 21: // 
                    Core.SmartKillMonster(qIDs[i], "newfinale", "Chaorrupted Lycan Hunter");
                    break;
                case 22: // 
                    Core.SmartKillMonster(qIDs[i], "newfinale", "Shadow Slayer");
                    break;
                case 23: // 
                    Core.SmartKillMonster(qIDs[i], "newfinale", "Memory of Vampires");
                    break;
                case 24: // 
                    Core.SmartKillMonster(qIDs[i], "newfinale", "Chaotic Virago", completeQuest: true);
                    Core.EnsureComplete(qIDs[i]);
                    bot.SendPacket("%xt%zm%tryQuestComplete%2303%3791%-1%false%wvz%");
                    break;
                case 25: // 
                    bot.Player.Join("chaosbeast");
                    break;
                case 26: // 3618
                    bot.Player.Join("chaosbeast");
                    break;
                case 27: // 3609
                    bot.Player.Join("chaosbeast");
                    break;
                case 28: // 3610
                    bot.Player.Join("chaosbeast");
                    break;
                case 29: // 3611
                    bot.Player.Join("chaosbeast");
                    break;
                case 30: // 3612
                    bot.Player.Join("chaosbeast");
                    break;
                case 31: // 3613
                    bot.Player.Join("chaosbeast");
                    break;
                case 32: // 3614
                    bot.Player.Join("chaosbeast");
                    break;
                case 33: // 3615
                    bot.Player.Join("chaosbeast");
                    break;
                case 34: // 3616
                    bot.Player.Join("chaosbeast");
                    break;
                case 35: // 3617
                    bot.Player.Join("chaosbeast");
                    break;
                case 36: // 3619
                    bot.Player.Join("chaosbeast");
                    break;
                case 37: // 
                    bot.Player.Join("newfinale");
                    break;
                case 38: // 
                    Core.SmartKillMonster(qIDs[i], "newfinale", "Alliance Soldier");
                    break;
                case 39: // 
                    Core.GetMapItem(2894, map: "drakathfight");
                    break;
                case 40: // 
                    Core.SmartKillMonster(qIDs[i], "shadowrise", "Broken Bones|Darkness Elemental|Dry Ice Mage");
                    break;
                case 41: // 
                    Core.GetMapItem(2895, map: "shadowrise");
                    break;
                case 42: // 
                    bot.Player.Join("shadowattack");
                    break;
                case 43: // 
                    Core.GetMapItem(2896, map: "shadowattack");
                    break;
                case 44: // 
                    Core.SmartKillMonster(qIDs[i], "shadowattack", "Death");
                    break;
                case 45: // 
                    bot.Player.Join("confrontation");
                    break;
                case 46: // 
                    Core.KillMonster("finalbattle", "r1", "Right", "Drakath", "Drakath Defeated");
                    break;
                case 47: // 
                    Core.KillMonster("finalbattle", "r4", "Left", "Drakath", "Drakath Defeated");
                    break;
                case 48: // 
                    Core.KillMonster("finalbattle", "r9", "Left", "Drakath", "Drakath Defeated");
                    break;
                case 49: // 
                    Core.SmartKillMonster(qIDs[i], "chaosrealm", "Alteon");
                    break;
                case 50: // 
                    Core.SmartKillMonster(qIDs[i], "chaoslord", "*");
                    break;
                case 51: // 
                    Core.SmartKillMonster(qIDs[i], "finalshowdown", "Prince Drakath");
                    break;
            }
            Core.EnsureComplete(qIDs[i]);
            Core.Logger($"Finished {i}");
            Core.Rest();
            bot.Sleep(Core.ActionDelay);
        }

        Core.SetOptions(false);
    }
}