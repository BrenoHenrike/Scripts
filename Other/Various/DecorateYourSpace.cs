/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs

using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class DecorateYourSpace
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Getthestuff();

        Core.SetOptions(false);
    }

    public void Getthestuff()
    {
        List<ItemBase> RewardOptions = Core.EnsureLoad(7782).Rewards;
        List<string> RewardsList = new List<string>();
        foreach (Skua.Core.Models.Items.ItemBase Item in RewardOptions)
            RewardsList.Add(Item.Name);

        string[] Rewards = RewardsList.ToArray();

        Core.AddDrop(Rewards);

        Core.EquipClass(ClassType.Farm);

        Core.RegisterQuests(7782);
        while (!Bot.ShouldExit && !Core.CheckInventory(Rewards))
            Core.KillMonster("yokaigrave", "r2", "Left", "*", "Graves Cleared", 10, log: false);
        Core.CancelRegisteredQuests();
    }
}
