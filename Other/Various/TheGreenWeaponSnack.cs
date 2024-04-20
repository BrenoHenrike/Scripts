/*
name: The Green WeaponSnack
description: farms quest rewards from `quest name here` in /mapname
tags: Green, WeaponSnack, 
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
            while (!Bot.ShouldExit && !Core.CheckInventory(Reward.ID))
            {
                Core.EnsureAccept(QuestID, true);
                Core.KillMonster("gardenquest", "r2", "Right", "Overconfident Radish", "Spices", 10, false, false);
                Core.KillMonster("gardenquest", "r3", "Right", "Silly Karrot", "Salt + Sugar", 10, false, false);
                Core.KillMonster("gardenquest", "r7", "Right", "Vegetable Prince", "Cucumber", 10, false, false);
                Core.EnsureCompleteMulti(QuestID);
            }

            Core.JumpWait();
            Core.ToBank(Reward.Name);
        }
        Bot.Lite.ReacceptQuest = false;
        Core.AbandonQuest(QuestID);
    }
}
