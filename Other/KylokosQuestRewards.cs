/*
name: Kylokos Quest Rewards
description: farms quest rewards from Pisces Venaris Mater` in /natatorium
tags: natatorium, quest reward, Kyloko, 
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class KylokosQuest
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetRewards();

        Core.SetOptions(false);
    }

    public void GetRewards()
    {

        List<ItemBase> RewardOptions = Core.EnsureLoad(9145).Rewards;

        foreach (ItemBase item in RewardOptions)
            Core.AddDrop(item.Name);

        Core.EquipClass(ClassType.Farm);

        foreach (ItemBase Reward in RewardOptions)
        {
            if (Core.CheckInventory(Reward.Name, toInv: false))
                return;

            Core.FarmingLogger(Reward.Name, 1);

            Core.EnsureAccept(9145);
            Core.HuntMonster("natatorium", "Anglerfish", "Anglerfish Star Shard", 10, isTemp: false, log: false);
            Core.HuntMonster("natatorium", "Merdraconian", "Merdraconian Star Shard", 10, isTemp: false, log: false);
            Core.EnsureComplete(9145, Reward.ID);
            Core.JumpWait();
            Core.ToBank(Reward.Name);
        }
    }
}
