/*
name: Rose Rapiers
description: does the quests in /summerhub (part of the thorny scavenger clue) for both rose rapiers
tags: rose, rapier, summer, seasonal, summerhub
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
// using Skua.Core.Options;

public class RoseRapiers
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        GetWeapons();

        Core.SetOptions(true);
    }

    public void GetWeapons()
    {
        if (!Core.isSeasonalMapActive("Summerhub"))
            return;

        List<ItemBase> RewardOptions = Core.EnsureLoad(9278).Rewards;

        Bot.Drops.Add(Core.QuestRewards(9278));
        Bot.Drops.Add(new[] { "Rose Clue", "Not the Bush You Were Looking For", "Compass Rose" });

        foreach (ItemBase item in RewardOptions)
        {
            Bot.Drops.Add(item.ID);
            while (!Bot.ShouldExit && !Core.CheckInventory(item.ID))
            {
                Core.EnsureAccept(9275, 9276, 9277, 9278);

                //Edit the Hunt Below\add more if needed
                Core.HuntMonster("underglade", "Lunamoss", "Found the Rose Clue");
                Core.EnsureComplete(9275);
                Core.HuntMonster("terrarium", "Shambush", "Found the Not the Bush Clue");
                Core.EnsureComplete(9276);
                Core.HuntMonster("elemental", "Tree of Destiny", "Found the Compass Rose Clue");
                Core.EnsureComplete(9277);
                Core.HuntMonster("runedwoods", "Jir'abin", "Found the Rose Blades!");

                Core.EnsureComplete(9278, item.ID);
            }
        }
    }
}