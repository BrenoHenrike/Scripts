/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class TenacityChallenge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        QuestFarming();

        Core.SetOptions(false);
    }

    public void QuestFarming()
    {
        if (Core.CheckInventory("Nulgath Challenge Pet"))
        {
            Core.Logger("You Don't Have \"Nulgath Challenge Pet\". Pet is required for doing the quests.");
            return;
        }

        string[] Rewards = { "Tainted Gem", "Dark Crystal Shard", "Blood Gem of the Archfiend" };
        Core.AddDrop(Rewards);

        for (int i = 0; i < Rewards.Length; i++)
        {
            if (Bot.Inventory.IsMaxStack(Rewards[i]))
                Core.Logger($"\"{Rewards[i]}\" is max stack Checking next item in the \"Tenacity Challenge\" Quest's Rewards");
            else
            {
                while (!Bot.Inventory.IsMaxStack(Rewards[i]))
                {
                    //Tenacity Challenge 3319
                    Core.EnsureAccept(3319);

                    Core.HuntMonster("deathpits", "Ghastly Darkblood", "Dark Runes", 6);
                    Core.HuntMonster("evilwardage", "Bloodfiend", "Blood Runes", 7);

                    Core.EnsureCompleteChoose(3319, new[] { Rewards[i] });
                }
            }
        }
    }
}
