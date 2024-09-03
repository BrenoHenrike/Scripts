/*
name: Verdant Virgo Rewards
description: farms quest rewards from Verdant Virgo` in /arcangrove
tags: quest reward, kylokos, verdant, virgo, arcangrove, zodiac
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class VerdantVirgo
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
        if (Core.CheckInventory(Core.QuestRewards(9197)))
            return;

        List<ItemBase> RewardOptions = Core.EnsureLoad(9197).Rewards;

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
                Core.EnsureAccept(9197);
                Core.HuntMonster("underglade", "Forest Spirit", "Forest Star Shard", 50, false, false);
                Core.HuntMonster("underglade", "Tree Nymph", "Maiden's Star Shard", 50, false, false);
                Core.EnsureComplete(9197, Reward.ID);
                Core.ToBank(Reward.ID);
            }
        }
        Core.Logger("all rewards gathered.");
    }
}
