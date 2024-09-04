/*
name: Star Festival Gift
description: This bot will get you all the rewards from the quest for the 'Star Festival Gift' quest in /starfestival
tags: star, festival, gift, yokai, river, fallen, moon, bow, ninja
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class StarFestivalGift
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        GetGifts();

        Core.SetOptions(false);
    }

    public void GetGifts()
    {
        string[] rewards = Core.QuestRewards(6449);
        if (Core.CheckInventory(rewards) || !Core.isSeasonalMapActive("starfest"))
            return;

        Core.AddDrop(rewards);
        Core.RegisterQuests(6449);
        while (!Bot.ShouldExit && !Core.CheckInventory(rewards))
            Core.HuntMonster("starfest", "Fallen Star", "Fallen Star", 7, log: false);
        Core.CancelRegisteredQuests();
    }
}
