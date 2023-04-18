/*
name: Tenacity Challenge
description: Farms the quest Tenacity Challenge to get Tainte Gems, Dark Crystal Shards and Blood Gem of the Archfiend (requires Nulgath Challenge Pet)
tags: nation, tainted gem, dark crystal shard, blood gem of the archfiend, nulgath challenge pet
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class TenacityChallenge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        QuestFarming();
    }

    public void QuestFarming()
    {
        if (!Core.CheckInventory("Nulgath Challenge Pet"))
        {
            Core.Logger("You Don't Have \"Nulgath Challenge Pet\". Pet is required for doing the quests.");
            return;
        }

        string[] rewards = { "Tainted Gem", "Dark Crystal Shard", "Blood Gem of the Archfiend" };
        Core.AddDrop(rewards);

        Core.RegisterQuests(3319);
        foreach (string reward in rewards)
        {
            Core.Logger($"Checking for item: \"reward\"");
            while (!Bot.ShouldExit && !Bot.Inventory.IsMaxStack(reward))
            {
                Core.HuntMonster("deathpits", "Ghastly Darkblood", "Dark Runes", 6);
                Core.HuntMonster("evilwardage", "Bloodfiend", "Blood Runes", 7);

                Bot.Wait.ForPickup(reward);
            }
            Core.Logger($"\"{reward}\" is max stack.");
        }
        Core.CancelRegisteredQuests();
    }
}
