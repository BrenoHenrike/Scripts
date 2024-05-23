/*
name: Midnight Assassin Set
description: farms the "Midnight Assassin" set from Quest: "Vampire Medals, Mega Vampire Medals".
tags: vampire medals, mega vampire medals, set
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/BloodMoon.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class MidnightAssassinSet
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public BloodMoon BloodMoon = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetAll();

        Core.SetOptions(false);
    }

    public void GetAll()
    {
        string[] rewards = {
            "Midnight Assassin Daggers",
            "Midnight Assassin Dirk",
            "Midnight Assassin",
            "Midnight Assassin Helm"
        };
        if (Core.CheckInventory(rewards))
            return;

        int count = 0;

        Core.CheckSpaces(ref count, rewards);
        Core.AddDrop(rewards);

        BloodMoon.BloodMoonSaga();

        Core.RegisterQuests(6070, 6071);
        Bot.Events.ItemDropped += ItemDropped;
        Core.Logger($"Farm for the Midnight Assassin set started. Farming to get {rewards.Length - count} more item" + ((rewards.Length - count) > 1 ? "s" : ""));

        while (!Bot.ShouldExit && !Core.CheckInventory(rewards))
        {
            Core.HuntMonster("bloodwarlycan", "Vampiric Knight", "Vampire Medal", 5);
            Core.HuntMonster("bloodwarlycan", "Vampiric Knight", "Mega Vampire Medal", 3);
            Bot.Wait.ForPickup("*");
        }

        Bot.Events.ItemDropped -= ItemDropped;
        Core.CancelRegisteredQuests();

        void ItemDropped(ItemBase item, bool addedToInv, int quantityNow)
        {
            if (rewards.Contains(item.Name))
            {
                count++;
                Core.Logger($"Got {item.Name}, {rewards.Length - count} items to go");
            }
        }
    }
}
