/*
name: Master of Your Craft
description: Farms "All Drops" From Quest: "Master of Your Craft".
tags: master of your craft, drops
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;

public class MasterofYourCraft
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();


    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Rewards);
        Core.SetOptions();

        Moyc();

        Core.SetOptions(false);
    }

    public readonly string[] Rewards =
{
        "Master Trainer",
        "GrandMaster Trainer",
        "Master Trainer's Helm + Locks",
        "Master Trainer's Helm",
        "Master Trainer's Minion",
        "Master Trainer's Sword",
    };

    public void Moyc()
    {
        var quest = Bot.Quests.EnsureLoad(3051);
        if (quest == null)
        {
            Core.Logger("Quest \"Master of Your Craft\" not found", stopBot: true);
            return;
        }
        var rewards = quest.Rewards.ToArray();

        if (!Core.CheckInventory("Dragon of Time"))
        {
            Core.Logger("Missing \"Dragon of Time\", to continue", stopBot: true);
        }

        Core.Unbank("Dragon of Time");
        Core.Equip("Dragon of Time");
        Adv.SmartEnhance("Dragon of Time");

        foreach (var reward in rewards)
        {
            if (Core.CheckInventory(reward.ID, toInv: false))
            {
                continue;
            }
            Core.AddDrop(reward.ID);
        }

        Core.RegisterQuests(quest.ID);
        while (!Bot.ShouldExit && rewards.Any(reward => !Core.CheckInventory(reward.ID)))
        {
            Core.HuntMonster("chchallenge", "Training Golem", "Rounds Won");
            foreach (var reward in rewards)
            {
                if (Bot.Inventory.Contains(reward.ID))
                {
                    Core.ToBank(reward.ID);
                }
            }
        }
    }

}

