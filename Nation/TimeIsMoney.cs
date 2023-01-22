/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class TimeIsMoney
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        FarmingQuest();

        Core.SetOptions(false);
    }

    public void FarmingQuest()
    {
        string[] Rewards = (Core.EnsureLoad(6185).Rewards.Select(i => i.Name).ToArray());
        Core.AddDrop(Rewards);

        if (!Core.CheckInventory(42581))
        {
            Core.Logger("You Don't Have \"Bounty Hunter Blade Pet\". Pet is required for doing the quests.");
            return;
        }
        for (int i = 0; i < Rewards.Length; i++)
        {
            if (Bot.Inventory.IsMaxStack(Rewards[i]))
                Core.Logger($"{Rewards[i]} is max stack Checking next item in the \"Time is Money\" Quest's Rewards");
            else
                while (!Bot.Inventory.IsMaxStack(Rewards[i]))
                {
                    //Time is Money 6185
                    Core.EnsureAccept(6185);

                    Core.HuntMonster("Mobius", "Slugfit", "Slugfit Horn", 5);
                    Core.HuntMonster("Mobius", "Fire Imp", "Imp Flame", 3);
                    Core.HuntMonster("bamboo", "Tanuki", "Tanuki Ears", 3);
                    Core.HuntMonster("greenguardwest", "Big Bad Boar", "Wereboar Tusk", 2);
                    Core.HuntMonster("junkyard", "Onibaba", "Onibaba Nails", 5);

                    Core.EnsureComplete(6185);
                }
        }
    }
}
