/*
name: Treasure Hunt Quest Rewards
description: This script will get the rewards from the Treasure Hunt Quest.
tags: treasure, hunt, quest, rewards, draco con, bob macguffin, spiked slayer mace, drakka defender
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/DracoCon.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class TreasureHuntQuest
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    private DracoCon DR = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        DoQuest();
        TreasureHuntRewards();

        Core.SetOptions(true);
    }

    public void DoQuest(bool CBoA = false)
    {
        DR.StoryLine();
        TreasureHuntRewards(CBoA: CBoA);
    }

    private void TreasureHuntRewards(int questID = 5373, bool CBoA = false)
    {
        if (CBoA && Core.CheckInventory("Spiked Slayer Mace"))
            return;

        List<ItemBase> RewardOptions = CBoA ? Core.EnsureLoad(questID).Rewards.Where(x => x.Name == "Spiked Slayer Mace").ToList() : Core.EnsureLoad(questID).Rewards;

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
                while (!Bot.ShouldExit && !Core.CheckInventory(Reward.Name, toInv: false))
                {
                    Core.HuntMonster("dracocon", "Treasure Pile", "Treasure Searched", log: false);
                }
            }
        }
    }
}
