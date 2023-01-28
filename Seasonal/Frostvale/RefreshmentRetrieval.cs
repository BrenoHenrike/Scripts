/*
name: Refreshment Retrieval
description: This will obtain all of the reward items on Refreshment Retrieval quest.
tags: refreshment-retrieval, seasonal, frostvale
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class RefreshmentRetrieval
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    int questID = 9029;
    int quant = 1;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();


        RandomReward(questID, quant);

        Core.SetOptions(false);
    }


    private void RandomReward(int questID, int quant)
    {
        QuestPreReq();
        int i = 0;

        List<ItemBase> RewardOptions = Core.EnsureLoad(questID).Rewards;

        foreach (ItemBase item in RewardOptions)
            Bot.Drops.Add(item.Name);

        string[] QuestRewards = RewardOptions.Select(x => x.Name).ToArray();

        Core.EquipClass(ClassType.Solo);
        Core.RegisterQuests(questID);
        foreach (ItemBase Reward in RewardOptions)
        {
            if (Core.CheckInventory(Reward.Name, toInv: false))
                Core.Logger($"{Reward.Name} Found.");
            else
            {
                Core.FarmingLogger(Reward.Name, 1);
                while (!Bot.ShouldExit && !Core.CheckInventory(Reward.Name, toInv: false))
                {

                    Core.HuntMonster("carolinn", "Frostval Deer", "Frostval Refreshments", 10);

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

    public void QuestPreReq()
    {
        if (Core.isCompletedBefore(9028))
            return;

        Core.AddDrop("Red Ribbon");
        Story.KillQuest(9028, "caroling", "Frostval Tree");
    }
}
