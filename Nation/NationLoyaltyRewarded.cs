/*
name: Nation Loyalty Rewarded
description: Does the Nation Loyalty Rewarded Quest to max the quest rewards.
tags: nation loyalty rewarded, nulgath, nation, dark crystal shard, diamond of nulgath, diamond badge of nulgath
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/CoreAdvanced.cs

using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class NationLoyaltyRewarded
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreNation Nation = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        FarmQuest("Diamond Badge of Nulgath", 1);

        Core.SetOptions(false);
    }

    public void FarmQuest(String? farmItem = null, int quant = 0)
    {
        List<ItemBase>? RewardOptions = null;
        if (farmItem == null)
        {
            RewardOptions = Core.EnsureLoad(4749).Rewards;
            foreach (ItemBase item in RewardOptions)
                Core.AddDrop(item.Name);
        }
        else Core.AddDrop(farmItem);

        Nation.NationRound4Medal();
        Nation.FarmUni13(1);
        if (RewardOptions != null)
            foreach (ItemBase item in RewardOptions)
            {
                if (Bot.Inventory.IsMaxStack(item.ID))
                    Core.Logger($"{item.Name} is max stack Checking next item in the \"Time is Money\" Quest's Rewards");
                else
                {
                    Core.FarmingLogger(item.Name, item.MaxStack);
                    //Nation Loyalty Rewarded 4749
                    Core.RegisterQuests(4749);
                    while (!Bot.ShouldExit && !Bot.Inventory.IsMaxStack(item.ID))
                        NLR();
                    Core.CancelRegisteredQuests();

                }
            }
        else
        {
            Core.FarmingLogger(farmItem, quant);
            Core.RegisterQuests(4749);
            while (!Bot.ShouldExit && !Core.CheckInventory(farmItem, quant))
                NLR();
            Core.CancelRegisteredQuests();
        }


    }
    public void NLR()
    {
        Core.EquipClass(ClassType.Solo);
        Core.HuntMonster("aqlesson", "Carnax", "Carnax Eye", publicRoom: true, log: false);
        Core.HuntMonster("deepchaos", "Kathool", "Kathool Tentacle", publicRoom: true, log: false);
        Core.HuntMonster("dflesson", "Fluffy the Dracolich", "Fluffy's Bones", publicRoom: true, log: false);
        Core.HuntMonster("lair", "Red Dragon", "Red Dragon's Fang", publicRoom: true, log: false);
        Core.HuntMonster("bloodtitan", "Blood Titan", "Blood Titan's Blade", publicRoom: true, log: false);
        Core.EquipClass(ClassType.Farm);
        Core.KillMonster("tercessuinotlim", "m2", "Left", "*", "Defeated Makai", 25, false, false);
    }
}
