/*
name: Nation Loyalty Rewarded
description: Does the Nation Loyalty Rewarded Quest to max the quest rewards.
tags: Nation Loyalty Rewarded, Nulgath, Nation, dark crystal shard, diamond of nulgath, diamond badge of nulgath 
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

        FarmQuest();

        Core.SetOptions(false);
    }

    public void FarmQuest()
    {
        List<ItemBase> RewardOptions = Core.EnsureLoad(4749).Rewards;

        foreach (ItemBase item in RewardOptions)
            Core.AddDrop(item.Name);

        Nation.NationRound4Medal();
        Nation.FarmUni13(1);

        foreach (ItemBase item in RewardOptions)
        {
            if (Bot.Inventory.IsMaxStack(item.ID))
                Core.Logger($"{item.Name} is max stack Checking next item in the \"Time is Money\" Quest's Rewards");
            else
            {
                Core.FarmingLogger(item.Name, Bot.Inventory.GetItem(item.ID).MaxStack);
                Core.RegisterQuests(4749);
                while (!Bot.ShouldExit && !Bot.Inventory.IsMaxStack(item.ID))
                {
                    //Nation Loyalty Rewarded 4749
                    Core.EquipClass(ClassType.Solo);
                    Adv.BestGear(GearBoost.Chaos);
                    Core.HuntMonster("aqlesson", "Carnax", "Carnax Eye", publicRoom: true, log: false);
                    Core.HuntMonster("deepchaos", "Kathool", "Kathool Tentacle", publicRoom: true, log: false);
                    Core.HuntMonster("dflesson", "Fluffy the Dracolich", "Fluffy's Bones", publicRoom: true, log: false);
                    Adv.BestGear(GearBoost.Dragonkin);
                    Core.HuntMonster("lair", "Red Dragon", "Red Dragon's Fang", publicRoom: true, log: false);
                    Adv.BestGear(GearBoost.Human);
                    Core.HuntMonster("bloodtitan", "Blood Titan", "Blood Titan's Blade", publicRoom: true, log: false);
                    Core.EquipClass(ClassType.Farm);
                    Core.KillMonster("tercessuinotlim", "m2", "Bottom", "Dark Makai", "Defeated Makai", 25, false, false);
                }
            }
                Core.CancelRegisteredQuests();
        }
    }
}
