/*
name: Second First Mate
description: Farms "All Drops" From Quest: "A Second First Mate".
tags: a second first mate, set, pirate, tlapd, seasonal, talk-like-a-pirate-day
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;


public class SecondFirstMate
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetDrops();

        Core.SetOptions(false);
    }


    private void GetDrops()
    {
        int i = 0;

        List<ItemBase> RewardOptions = Core.EnsureLoad(7714).Rewards;

        foreach (ItemBase item in RewardOptions)
            Bot.Drops.Add(item.Name);

        string[] QuestRewards = RewardOptions.Select(x => x.Name).ToArray();

        Core.EquipClass(ClassType.Farm);
        Core.RegisterQuests(7714);
        foreach (ItemBase Reward in RewardOptions)
        {
            if (Core.CheckInventory(Reward.Name, toInv: false))
                Core.Logger($"{Reward.Name} Found.");
            else
            {
                Core.FarmingLogger(Reward.Name, 1);
                while (!Bot.ShouldExit && !Core.CheckInventory(Reward.Name, toInv: false))
                {
                    Core.HuntMonster("pirates", "Undead Pirate", "Undead Pirates Cleared", 35);
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
