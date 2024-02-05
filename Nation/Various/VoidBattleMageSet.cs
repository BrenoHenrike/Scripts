/*
name: VoidBattleMageSet
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/VasalkarLairWar.cs

using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class VoidBattleMageSet
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
        War.Attack();

        List<ItemBase> RewardOptions = Core.EnsureLoad(6694).Rewards;

        foreach (ItemBase item in RewardOptions)
            Bot.Drops.Add(item.Name);

        List<ItemBase> PreInv = new();

        Core.RegisterQuests(6694);
        Core.EquipClass(ClassType.Solo);

        //the foreach can be simplified as such:
        foreach (ItemBase Reward in RewardOptions.Where(item => item.Name != null))
            Core.KillMonster("lairattack", "Eggs", "Left", "Flame Dragon General", Reward.Name);

        foreach (ItemBase item in RewardOptions)
            Core.ToBank(item.ID);
            
        Core.CancelRegisteredQuests();
    }

}
