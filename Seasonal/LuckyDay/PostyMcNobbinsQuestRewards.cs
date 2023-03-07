/*
name: Posty Mc Nobbins Quest Rewards
description: gest the quest rewards from all 4 quest from mc nobbins.
tags: mc nobbins, quest reward, farm, lucky day, luck, seasonal
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs

using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class PostyMcNobbinsQuestRewards
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv => new();

    string[] PotDrops =
    {
    "Leprechaun Ranger",
    "Leprechaun Ranger's TopHat",
    "Leprechaun Ranger's TopHat + Locks",
    "Lucky Day TopHat",
    "Lucky Day TopHat + Glasses",
    "Lucky Day TopHat + Locks",
    "Lucky Emerald TopHat",
    "Lucky Emerald TopHat + Glasses",
    "Lucky Emerald TopHat + Locks",
    "Platinum Coin",
    "Rainbow Coin",
    };


    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        AllRewards();

        Core.SetOptions(false);
    }

    public void AllRewards()
    {
        if (!Core.isSeasonalMapActive("luck"))
            return;

        string[] AllRewards = (Core.EnsureLoad(5758).Rewards.Select(i => i.Name)).Concat(Core.EnsureLoad(5759).Rewards.Select(i => i.Name)).Concat(Core.EnsureLoad(5760).Rewards.Select(i => i.Name)).Concat(Core.EnsureLoad(5761).Rewards.Select(i => i.Name)).ToArray();

        Core.EquipClass(ClassType.Solo);
        Adv.SmartEnhance(Core.SoloClass);
        Adv.BestGear(GearBoost.dmgAll);

        Bot.Drops.Add(AllRewards.Concat(PotDrops).ToArray());

        Core.EquipClass(ClassType.Farm);

        Core.RegisterQuests(5758, 5759, 5760, 5761);
        while (!Bot.ShouldExit && !Core.CheckInventory(AllRewards, toInv: false))
            Core.HuntMonster("luck", "Pot O' Gold", log: false);
        Core.CancelRegisteredQuests();
        Core.ToBank(AllRewards);
    }
}