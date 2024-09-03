/*
name: Karkinos Mnimi Quest Rewards
description: This script will get the "Karkinos Mnimi" [9195] quest rewards in /arcangrove.
tags: karkinos, mnimi, quest rewards, arcangrove, zodiac, kylokos
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class KarkinosMnimi
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        DoQuest();

        Core.SetOptions(true);
    }

    public void DoQuest()
    {
        AutoReward();
    }

    public void AutoReward(int questID = 9195)
    {
        if (Core.CheckInventory(Core.QuestRewards(questID)))
            return;


        List<ItemBase> RewardOptions = Core.EnsureLoad(questID).Rewards;

        Bot.Drops.Add(Core.QuestRewards(questID));
        Core.EquipClass(ClassType.Farm);

        foreach (ItemBase item in RewardOptions)
        {
            while (!Bot.ShouldExit && !Core.CheckInventory(item.ID))
            {
                Core.EnsureAccept(questID);
                Core.HuntMonster("palooza", "Rock Lobster", "Lobster Shard", 20, false, false);
                Core.EnsureComplete(questID, item.ID);
            }
        }
    }
}
