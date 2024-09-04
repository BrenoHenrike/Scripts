/*
name: The Green WeaponSnack
description: farms quest rewards from `The Green WeaponSnack` in /gardenquest
tags: green, weaponsnack, cucumber, salt, sugar, spices, pickle, pickle pullover, pickle face
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class TheGreenWeaponSnack
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetRewards();

        Core.SetOptions(false);
    }

    int QuestID = 9692;

    public void GetRewards()
    {
        if (!Core.isSeasonalMapActive("gardenquest") || Core.CheckInventory(Core.QuestRewards(QuestID), toInv: false))
            return;

        List<ItemBase> RewardOptions = Core.EnsureLoad(QuestID).Rewards;

        foreach (ItemBase item in RewardOptions)
            Core.AddDrop(item.Name);

        Core.EquipClass(ClassType.Solo);

        foreach (ItemBase Reward in RewardOptions)
        {
            if (Core.CheckInventory(Reward.Name, toInv: false))
                continue;

            Core.Logger(Core.CheckInventory(Reward.ID, toInv: false) ? $"{Reward.Name}: ✅" : $"{Reward.Name} ❌");

            Core.FarmingLogger(Reward.Name, 1);
            Core.RegisterQuests(QuestID);
            while (!Bot.ShouldExit && !Core.CheckInventory(Reward.ID))
            {
                Core.KillMonster("gardenquest", "r2", "Right", "Overconfident Radish", "Spices", 10, false, false);
                Core.KillMonster("gardenquest", "r3", "Right", "Silly Karrot", "Salt + Sugar", 10, false, false);
                Core.KillMonster("gardenquest", "r7", "Right", "Vegetable Prince", "Cucumber", 10, false, false);
            }
            Core.CancelRegisteredQuests();
            Core.JumpWait();
            Core.ToBank(Reward.Name);
        }
        Bot.Lite.ReacceptQuest = false;
        Core.AbandonQuest(QuestID);
    }
}
