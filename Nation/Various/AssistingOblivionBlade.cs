/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class AssistingOblivionBlade
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreNation Nation = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        doit();

        Core.SetOptions(false);
    }

    public void doit()
    {
        if (!Core.IsMember)
            return;

        if (!Core.CheckInventory("The Secret 2"))
            return;

        if (!Core.CheckInventory("Tendurrr The Assistant"))
            Core.HuntMonster("tercessuinotlim", "Dark Makai", "Tendurrr The Assistant");

        List<ItemBase> RewardOptions = Core.EnsureLoad(5818).Rewards;
        List<string> RewardsList = new List<string>();
        foreach (Skua.Core.Models.Items.ItemBase Item in RewardOptions)
            RewardsList.Add(Item.Name);
        string[] Rewards = RewardsList.ToArray();
        Core.AddDrop(Rewards);

        Core.RegisterQuests(5818);
        foreach (ItemBase item in RewardOptions)
        {
            while (!Bot.ShouldExit && !Core.CheckInventory(item.ID, item.MaxStack))
            {
                Farm.TheSecret4();
                Nation.EssenceofNulgath(20);
                Nation.ApprovalAndFavor(50, 50);
                Core.KillMonster("boxes", "Fort2", "Left", "*", "Cubes", 50, false);
                Core.KillMonster("shadowblast", "r13", "Left", "*", "Fiend Seal", 10, false);
                Farm.BattleUnderB(quant: 200);
                Bot.Sleep(Core.ActionDelay);
            }
        }
        Core.CancelRegisteredQuests();
    }
}
