/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/VasalkarLairWar.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class DragonLordGrandMasterSet
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public LairWar War = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetSet();

        Core.SetOptions(false);
    }

    public void GetSet()
    {
        string[] rewards = Core.EnsureLoad(6689).Rewards.Select(i => i.Name).ToArray();
        if (Core.CheckInventory(rewards))
            return;

        int count = 0;
        Core.CheckSpaces(ref count, rewards);
        Core.AddDrop(rewards);
        War.Defend();

        Core.RegisterQuests(6689);
        Bot.Events.ItemDropped += ItemDropped;
        Core.Logger($"Farm for the DragonLord GrandMaster set started. Farming to get {rewards.Count() - count} more item" + ((rewards.Count() - count) > 1 ? "s" : ""));

        while (!Core.CheckInventory(rewards))
        {
            Core.KillMonster("lairdefend", "Eggs", "Left", "Flame Dragon General", log: false);
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
