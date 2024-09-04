/*
name: Tinsel Quest Rewards
description: This script will farm "Gift Theft?! NOT COOL." [8177] quest rewards.
tags: tinsel, quest, rewards, bright lights, festive, ushanka, party, seasonal, frostval, july
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class TinselQuestRewards
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        //Gift Theft?! NOT COOL.
        AutoReward(8177);

        Core.SetOptions(true);
    }

    public void AutoReward(int questID = 0000)
    {
        List<ItemBase> RewardOptions = Core.EnsureLoad(questID).Rewards;

        Bot.Drops.Add(Core.QuestRewards(questID));

        foreach (ItemBase item in RewardOptions)
        {
            while (!Bot.ShouldExit && !Core.CheckInventory(item.ID))
            {
                Core.EnsureAccept(questID);
                Core.HuntMonster("brightlights", "Snow Golem", "Cool Gift", log: false);
                Core.EnsureComplete(questID, item.ID);
            }
        }
    }
}

