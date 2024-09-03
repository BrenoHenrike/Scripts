/*
name: Harmonic Savant Rewards
description: farms quest rewards from Harmonic Savant` in /arcangrove
tags: quest reward, kylokos, harmonic, savant, harmonic libra, libra, arcangrove, zodiac
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class HarmonicSavant
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetRewards();

        Core.SetOptions(false);
    }

    public void GetRewards()
    {
        if (Core.CheckInventory(Core.QuestRewards(9198)))
            return;

        List<ItemBase> RewardOptions = Core.EnsureLoad(9198).Rewards;

        foreach (ItemBase item in RewardOptions)
            Core.AddDrop(item.ID);
        foreach (ItemBase Reward in RewardOptions)
        {
            if (Core.CheckInventory(Reward.Name, toInv: false))
                Core.Logger($"{Reward.Name} obtained.");
            else Core.FarmingLogger(Reward.Name, 1);

            while (!Bot.ShouldExit && !Core.CheckInventory(Reward.Name))
            {
                Core.EnsureAccept(9198);
                Core.EquipClass(ClassType.Solo);
                Core.HuntMonster("skytower", "Aspect of Good", "Good Star Shard", 1, false, false);
                Core.HuntMonster("skytower", "Aspect of Evil", "Evil Star Shard", 1, false, false);
                Core.EquipClass(ClassType.Farm);
                Core.HuntMonster("astraviajudge", "Juror", "Judgement Star Shard", 25, false, false);
                Core.EnsureComplete(9198, Reward.ID);
                Core.ToBank(Reward.ID);
            }
        }
        Core.Logger("all rewards gathered.");
    }
}
