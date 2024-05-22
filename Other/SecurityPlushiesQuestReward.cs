/*
name: Security Plushies Quest Rewards
description: farms quest rewards from `Security Plushies` [9731] in /balemorale
tags: balemorale, lady laidronette cavendish, security plushies, quest, rewards
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class SecurityPlushiesQuestRewards
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetRewards();

        Core.SetOptions(false);
    }

    public void GetRewards(int QuestID = 9731)
    {
        List<ItemBase> RewardOptions = Core.EnsureLoad(QuestID).Rewards;

        foreach (ItemBase item in RewardOptions)
            Core.AddDrop(item.Name);

        Core.EquipClass(ClassType.Farm);

        foreach (ItemBase Reward in RewardOptions)
        {
            if (Core.CheckInventory(Reward.Name, toInv: false))
                return;

            Core.Logger(Core.CheckInventory(Reward.ID, toInv: false) ? $"{Reward.Name}: ✅" : $"{Reward.Name} ❌");

            Core.RegisterQuests(QuestID);
            Core.FarmingLogger(Reward.Name, 1);
            while (!Bot.ShouldExit && !Core.CheckInventory(Reward.ID))
                Core.HuntMonster("midnightzone", "Vowed ShadowSlayer", "Nightmare-Resistant Thread", 10, log: false);
            Core.CancelRegisteredQuests();

            Core.JumpWait();
            Core.ToBank(Reward.Name);
        }
    }
}
