/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class MasterofYourCraft
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();


    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Rewards);
        Core.SetOptions();

        MOYC();

        Core.SetOptions(false);
    }

    public readonly string[] Rewards =
{
        "Master Trainer",
        "GrandMaster Trainer",
        "Master Trainer's Helm + Locks",
        "Master Trainer's Helm",
        "Master Trainer's Minion",
        "Master Trainer's Sword",
    };

    public void MOYC()
    {
        Core.AddDrop(Rewards);

        if (Core.CheckInventory("Dragon of Time"))
        {
            Bot.Inventory.EquipItem("Dragon of Time");
            Adv.SmartEnhance("Dragon of Time");
        }

        int i = 1;
        while (!Bot.ShouldExit && !Core.CheckInventory(Rewards, toInv: false))
        {
            Core.EnsureAccept(3051);
            Core.HuntMonster("chchallenge", "Training Golem", "Rounds Won");
            Core.EnsureComplete(3051);
            Core.ToBank(Rewards);
            Core.Logger($"Completed x{i++}");
        }
    }

}

