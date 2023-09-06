/*
name: Search For Gifts Quest Rewards
description: This script will get all the rewards of search for gifts quest.
tags: search, for, gifts, rewards, seasonal, akibalight, ai no miko
*/
//cs_include Scripts/Seasonal/FrostvalInJuly/frostblademaster.cs
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class SearchForGiftsRewards
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private FrostBladeMaster FBM = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        SearchForGifts();
        Core.SetOptions(false);
    }

    public void SearchForGifts()
    {
        FBM.SagaName();
        
        if (!Core.isSeasonalMapActive("akibalight"))
            return;

        string[] rewards = Core.QuestRewards(6992);
        if (Core.CheckInventory(rewards, toInv: false))
            return;

        Core.AddDrop(rewards);

        Core.RegisterQuests(6992);
        while (!Bot.ShouldExit && !Core.CheckInventory(rewards))
            Core.HuntMonster("bamboo", "Bamboo Wisp", "Red-Wrapped Gift", 5);
        Core.CancelRegisteredQuests();
        Core.ToBank(rewards);
    }
}
