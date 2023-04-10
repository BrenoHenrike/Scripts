/*
name: LootTheUndresser Quest Rewards
description: farms quest rewards from 'Loot the UnDresser` in /cursedshop
tags: loottheundresser, quest reward, cursed antique shop, cursed bust
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class LootTheUndresser
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreToD TOD = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetRewards();

        Core.SetOptions(false);
    }

    public void GetRewards()
    {
        TOD.AntiqueShop();

        List<ItemBase> RewardOptions = Core.EnsureLoad(5435).Rewards;

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
                Core.EnsureAccept(5435);
                Core.HuntMonster("cursedshop", "UnDresser", "Dressered Down", log: false);
                Core.EnsureComplete(5435, Reward.ID);
                Core.ToBank(Reward.ID);
            }
        }
        Core.Logger("all rewards gathered.");
    }
}
