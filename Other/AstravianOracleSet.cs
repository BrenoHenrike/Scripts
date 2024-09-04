/*
name: Astravian Oracle Set
description: does the follow quests to get the astravian oracle set: 'mourning cygnus' & 'extinguished eridanus'
tags: mourning cygnus, extinguished eridanus, quest, reward, astravian oracle set
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
// using Skua.Core.Options;

public class AstravianOracleSet
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    private CoreAdvanced Adv => new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        DoQuests();

        Core.SetOptions(true);
    }

    public void DoQuests()
    {
        ExtinguishedEridanus(9208);
        MourningCygnus(9209);
    }

    public void ExtinguishedEridanus(int questID = 0000, int quant = 1)
    {
        List<ItemBase> RewardOptions = Core.EnsureLoad(questID).Rewards;

        foreach (ItemBase item in RewardOptions)
            Core.AddDrop(item.Name);

        Core.EquipClass(ClassType.Farm);
        //Adv.BestGear(RacialGearBoost.Elemental);
        foreach (ItemBase item in RewardOptions)
        {
            while (!Bot.ShouldExit && !Core.CheckInventory(item.ID, quant))
            {
                Core.EnsureAccept(questID);
                Core.KillMonster("eridani", "r6", "Left", "*", "Extinguished Shard", 15, log: false);
                Core.EnsureComplete(questID, item.ID);
            }
            Core.ToBank(item.ID);
        }
    }

    public void MourningCygnus(int questID = 0000, int quant = 1)
    {
        List<ItemBase> RewardOptions = Core.EnsureLoad(questID).Rewards;

        foreach (ItemBase item in RewardOptions)
            Core.AddDrop(item.Name);

        Core.EquipClass(ClassType.Farm);
        //Adv.BestGear(RacialGearBoost.Human);

        foreach (ItemBase item in RewardOptions)
        {
            while (!Bot.ShouldExit && !Core.CheckInventory(item.ID, quant))
            {
                Core.EnsureAccept(questID);
                Core.KillMonster("astravia", "r6", "Top", "*", "Broken Astravia Shards", 15, log: false);
                Core.EnsureComplete(questID, item.ID);
            }
            Core.ToBank(item.ID);
        }
    }
}
