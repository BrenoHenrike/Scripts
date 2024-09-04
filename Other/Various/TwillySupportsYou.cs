/*
name: Twilly Supports You!
description: This script does the quest [9779] "Twilly Supports You!" and gets the rewards.
tags: twilly, supports, you, quest, 9779, rewards, chickencow, take twilly's money, extremely enthusiastic hero
*/
//cs_include Scripts/CoreBots.cs

using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Utils;

public class TwillySupportsYou
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetDrops();

        Core.SetOptions(false);
    }

    public void GetDrops()
    {
        if (!Core.CheckInventory("Twilly Supports You!") || Core.CheckInventory(Core.EnsureLoad(9779).Rewards.Select(x => x.Name).ToArray(), toInv: false))
        {
            Core.Logger("You either don't have the \"Twilly Supports You!\" pet or already have the rewards.");
            return;
        }

        List<ItemBase> RewardOptions = Core.EnsureLoad(9779).Rewards;

        foreach (ItemBase item in RewardOptions.Where(x => x != null && !Core.CheckInventory(x.ID, toInv: false)))
            Bot.Drops.Add(item.ID);

        Core.EquipClass(ClassType.Farm);

        foreach (ItemBase Reward in RewardOptions)
        {
            if (Core.CheckInventory(Reward.ID, toInv: false))
                continue;

            Core.FarmingLogger(Reward.Name, 1);

            Core.EnsureAccept(9779);

            Core.HuntMonster("battlefowl", "Chickencow", "Sammich", log: false);

            Core.EnsureComplete(9779, Reward.ID);
            Core.JumpWait();
            Core.ToBank(Reward.ID);
        }

    }
}

