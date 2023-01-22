/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class DiabolicalWarden
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        GetDrops();

        Core.SetOptions(false);
    }

    public void GetDrops()
    {
        string[] rewards = {
            "Diabolical Warden",
            "Diabolical Warden's Hair",
            "Diabolical Warden's Twintails",
            "Diabolical Warden's Visage",
            "Diabolical Warden's Visage + Locks",
            "Diabolical Zealot's Locks",
            "Diabolical Zealot's Ponytail"
        };

        if (Core.CheckInventory(rewards))
        {
            Core.Logger("You already have all of the items.");
            return;
        }

        Core.AddDrop(rewards);

        Core.EquipClass(ClassType.Solo);

        Bot.Quests.UpdateQuest(9044);

        foreach (string Reward in rewards)
        {
            if (Core.CheckInventory(Reward))
            {
                Core.ToBank(Reward);
                continue;
            }
            Core.FarmingLogger(Reward, 1);
            Core.HuntMonster("brokenwoods", "Eldritch Amalgamation", Reward, isTemp: false, log: false);
            Core.ToBank(Reward);
        }
    }
}
