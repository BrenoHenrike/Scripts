/*
name: Leonine Regalia Rewards
description: farms quest rewards from Leonine Regalia in /arcangrove
tags: quest reward, kylokos, leonineregalia, leonine, regalia, arcangrove, zodiac
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class LeonineRegalia
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
        if (Core.CheckInventory(Core.QuestRewards(9196)))
            return;

        List<ItemBase> RewardOptions = Core.EnsureLoad(9196).Rewards;

        foreach (ItemBase item in RewardOptions)
            Core.AddDrop(item.ID);

        Core.EquipClass(ClassType.Farm);
        foreach (ItemBase Reward in RewardOptions)
        {
            if (Core.CheckInventory(Reward.Name, toInv: false))
                Core.Logger($"{Reward.Name} obtained.");
            else Core.FarmingLogger(Reward.Name, 1);

            while (!Bot.ShouldExit && !Core.CheckInventory(Reward.Name))
            {
                Core.EnsureAccept(9196);
                Core.HuntMonster("onslaughttower", "Golden Caster", "Burning Star Shard", 25, false, false);
                Core.HuntMonster("onslaughttower", "Maximillian Lionfang", "Regal Star Shard", 1, false, false);
                Core.EnsureComplete(9196, Reward.ID);
                Core.ToBank(Reward.ID);
            }
        }
        Core.Logger("All rewards gathered.");
    }
}
