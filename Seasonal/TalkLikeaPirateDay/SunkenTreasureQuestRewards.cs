/*
name: Sunken Treasure Quest Rewards
description: Farms "All Drops" From Quest: "Sunken Treasure?".
tags: sunken treasure, drops, set, pirate, tlapd, talk-like-a-pirate-day, seasonal
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;


public class SunkenTreasure
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetAll();

        Core.SetOptions(false);
    }

    public void GetAll()
    {
        int i = 0;

        List<ItemBase> RewardOptions = Core.EnsureLoad(7715).Rewards;

        foreach (ItemBase item in RewardOptions)
            Bot.Drops.Add(item.Name);

        string[] QuestRewards = RewardOptions.Select(x => x.Name).ToArray();

        Core.EquipClass(ClassType.Farm);
        Core.RegisterQuests(7715);
        foreach (ItemBase Reward in RewardOptions)
        {
            if (Core.CheckInventory(Reward.Name, toInv: false))
                Core.Logger($"{Reward.Name} Found.");
            else
            {
                Core.FarmingLogger(Reward.Name, 1);
                while (!Bot.ShouldExit && !Core.CheckInventory(Reward.Name, toInv: false))
                {
                    Core.HuntMonster("Pirates", "Shark Bait", "Waterlogged Chest", log: false);
                    Core.HuntMonster("Pirates", "Fishman Soldier", "Rusty Key", log: false);
                    Bot.Wait.ForPickup(Reward.Name);

                    i++;

                    if (i % 5 == 0)
                    {
                        Core.JumpWait();
                        Core.ToBank(QuestRewards);
                    }
                }
            }
        }
        Core.CancelRegisteredQuests();
        Core.ToBank(QuestRewards);
    }
}
