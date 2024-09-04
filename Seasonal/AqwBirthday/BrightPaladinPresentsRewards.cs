/*
name: Bright Paladin Presents Quest Rewards
description: This script will farm all the quest rewards from "Bright Paladin Presents" quest in /eventhub
tags: seasonal, birthday, ascended, paladin, axe, hammer, mace, blade, light, rewards
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
// using Skua.Core.Options;

public class BrightPaladinPresents
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
        AutoReward(6553);  //-----|
    }



    public void AutoReward(int questID = 0000)
    {
        List<ItemBase> RewardOptions = Core.EnsureLoad(questID).Rewards;

        Bot.Drops.Add(Core.QuestRewards(questID));

        Core.EquipClass(ClassType.Solo);
        foreach (ItemBase item in RewardOptions)
        {
            while (!Bot.ShouldExit && !Core.CheckInventory(item.ID))
            {
                Core.EnsureAccept(questID);
                Core.HuntMonster("birthday", "Birthday Cake", "Slice of Cake", log: false);
                Core.HuntMonster("birthday", "Birthday Cake", "Fork", log: false);
                Core.HuntMonster("birthday", "Birthday Cake", "Holy Wasabi", log: false);
                Core.EnsureComplete(questID, item.ID);
            }
        }
    }
}
