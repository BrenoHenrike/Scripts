/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/BloodMoon.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class LycanShamanSet
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
            "Lycan Shaman",
            "Lycan Shaman Helm",
            "Lycan Shaman's Familiar",
            "Lycan Shaman Staff"
        };
        if (Core.CheckInventory(rewards))
            return;

        int count = 0;

        Core.CheckSpaces(ref count, rewards);
        Core.AddDrop(rewards);

        BloodMoon.BloodMoonSaga();

        Core.RegisterQuests(6073);
        Bot.Events.ItemDropped += ItemDropped;
        Core.Logger($"Farm for the Lycan Shaman set started. Farming to get {rewards.Count() - count} more item" + ((rewards.Count() - count) > 1 ? "s" : ""));

        while (!Bot.ShouldExit && !Core.CheckInventory(rewards))
        {
            Core.HuntMonster("bloodwarlycan", "Blood Guardian", "Ruby Crest", 5);
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
