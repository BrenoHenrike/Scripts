/*
name: Treasures of Aotearoa
description: This script will farm all the rewards from Treasures of Aotearoa quest.
tags: treasures,of,aotearoa,seasonal,summer,break,valencia,dandelion,kiwi,cookie
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
// using Skua.Core.Options;

public class TreasuresOfAotearoa
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    int questID = 9279;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();
        DoQuest();
        Core.SetOptions(true);
    }

    public void DoQuest()
    {
        AutoReward(9279);
        RandomReward(9279);
    }

    public void AutoReward(int questID = 0000)
    {
        int[] RewardOptions = new[] { 77816, 77817, 77818, 77819, 77823, 78618 };

        Bot.Drops.Add(Core.QuestRewards(questID));

        foreach (int ID in RewardOptions)
        {
            while (!Bot.ShouldExit && !Core.CheckInventory(ID))
            {
                Core.EnsureAccept(questID);

                Core.HuntMonster("burningbeach", "Water Goblin", "Stolen Egg", 5, log: false);

                Core.EnsureComplete(questID, ID);
            }
        }
    }

    private void RandomReward(int questID = 0000, int quant = 1)
    {
        int i = 0;

        List<ItemBase> RewardOptions = Core.EnsureLoad(questID).Rewards;

        foreach (ItemBase item in RewardOptions)
            Bot.Drops.Add(item.Name);

        string[] QuestRewards = RewardOptions.Select(x => x.Name).ToArray();

        Core.EquipClass(ClassType.Farm);
        Core.RegisterQuests(questID);
        foreach (ItemBase Reward in RewardOptions)
        {
            if (Core.CheckInventory(Reward.Name, toInv: false))
                Core.Logger($"{Reward.Name} Found.");
            else
            {
                Core.FarmingLogger(Reward.Name, 1);
                while (!Bot.ShouldExit && !Core.CheckInventory(Reward.Name, quant, toInv: false))
                {

                    Core.HuntMonster("burningbeach", "Water Goblin", "Stolen Egg", 5, log: false);


                    i++;

                    if (i % 5 == 0)
                    {
                        Core.JumpWait();
                        Core.ToBank(QuestRewards);
                    }
                }
            }
        }
    }
}