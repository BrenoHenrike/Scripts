/*
name: Gifts of the Cryomancer
description: This script will farm all the rewards from the quest "Gifts of the Cryomancer"
tags: gifts of the cryomancer, staff, bithday
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class GiftsoftheCryomancer
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetRewards();

        Core.SetOptions(false);
    }

    public void GetRewards()
    {
        if (Core.CheckInventory(Core.QuestRewards(9589), toInv: false))
        {
            Core.Logger("All Rewards Obtained.");
            return;
        }

        Bot.Drops.Add(Core.QuestRewards(9589));
        Core.RegisterQuests(9589);
        while (!Bot.ShouldExit && !Core.CheckInventory(Core.QuestRewards(9589), toInv: false))
        {
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("shadowrealm", "Shadow Lord", "ShadowLord's Aura");
            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("icedungeon", "Ice Crystal", "Ice Crystal Shard", 10);
            Core.ToBank(Core.QuestRewards(9589));
        }
        Core.CancelRegisteredQuests();

    }
}
