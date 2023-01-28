/*
name: Alpha Hunter And Rogue Set
description: This will do the quest to obtain the Alpha Hunter and Rogue set.
tags: alpha-hunter, alpha-rogue, black-friday, seasonal
*/
//cs_include Scripts/CoreBots.cs
using System.Linq;
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class BlackFridayAlphaHunterRogue
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public List<IOption> Options = new()
    {
        new Option<bool>("toBank", "Bank Items", "Bank Items after you're done?", true)
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetSets(Bot.Config!.Get<bool>("toBank"));

        Core.SetOptions(false);
    }

    public void GetSets(bool toBank = true)
    {
        if (!Core.isSeasonalMapActive("blackfridaywar"))
        {
            Core.Logger("This bot is seasonal only.");
            return;
        }

        var AllRewards1 = Core.EnsureLoad(6104).Rewards;
        var AllRewards2 = Core.EnsureLoad(6105).Rewards;
        var AllRewards3 = Core.EnsureLoad(6106).Rewards;
        var AllRewards4 = Core.EnsureLoad(6107).Rewards;
        var AllRewardsArray = AllRewards1.Concat(AllRewards2).Concat(AllRewards3).Concat(AllRewards4).Select(x => x.ID).ToArray();
        if (Core.CheckInventory(AllRewardsArray, toInv: false))
            return;

        Core.EquipClass(ClassType.Solo);
        Core.AddDrop(AllRewardsArray);
        Core.RegisterQuests(Core.FromTo(6104, 6107));
        while (!Bot.ShouldExit && !Core.CheckInventory(AllRewardsArray, toInv: false))
        {
            Core.KillMonster("blackfridaywar", "r4", "Left", "*", log: false);
            if (toBank && Bot.Inventory.FreeSlots == 0)
                Core.ToBank(AllRewardsArray);
        }
        Core.ToBank(AllRewardsArray);
    }
}
