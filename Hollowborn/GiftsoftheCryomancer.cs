/*
name: Gifts of the Cryomancer
description: This script will farm all the rewards from the quest "Gifts of the Cryomancer"
tags: Gifts of the Cryomancer, staff, bithday
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Hollowborn/MergeShops/ShadowrealmMerge.cs
//cs_include Scripts/Story/AgeofRuin/CoreAOR.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
//cs_include Scripts/Other/MergeShops/YulgarsUndineMerge.cs
//cs_include Scripts/Hollowborn/MergeShops/DawnFortressMerge.cs
//cs_include Scripts/Story/Hollowborn/CoreHollowbornStory.cs
using Skua.Core.Interfaces;

public class GiftsoftheCryomancer
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public ShadowrealmMerge SRM = new();

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

    }
}
