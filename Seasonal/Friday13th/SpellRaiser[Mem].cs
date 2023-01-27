/*
name: SpellRaiser Set (Member Only)
description: This will do the required quest for the SpellRaiser Set.
tags: spellraiser, member, friday-the-13th, seasonal
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/Friday13th/Story/CoreFriday13th.cs
using Skua.Core.Interfaces;

public class SpellRaiser
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFriday13th F13 = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetAll();

        Core.SetOptions(false);
    }

    public void GetAll()
    {
        string[] AllRewards = (Core.EnsureLoad(7400).Rewards.Select(i => i.Name)).Concat(Core.EnsureLoad(7402).Rewards.Select(i => i.Name)).Concat(Core.EnsureLoad(7404).Rewards.Select(i => i.Name)).ToArray();

        if (Core.CheckInventory(AllRewards, toInv: false))
            return;

        F13.Splatterwar();

        Bot.Drops.Add(AllRewards);

        Core.EquipClass(ClassType.Farm);

        Core.RegisterQuests(7400, 7402, 7404);
        while (!Bot.ShouldExit && !Core.CheckInventory(AllRewards, toInv: false))
            // Slasher Medals 7400   //Mega Slasher Medals 7402 //Bladehands 7404
            Core.KillMonster("splatterwardage", "r4", "Left", "*", log: false);
        Core.JumpWait();
        Core.ToBank(AllRewards);
        Core.CancelRegisteredQuests();


    }
}
