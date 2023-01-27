/*
name: The Lost Knight Set and Backup Blade (Member Only)
description: This will do the required quest for the The Lost Knight Set and Backup Blade Cape.
tags: the-lost-knight, backup-blade, member, friday-the-13th, seasonal
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/Friday13th/Story/CoreFriday13th.cs
using Skua.Core.Interfaces;

public class TheLostKnightAndBackupBlade
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreFriday13th F13 = new();


    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetAll();

        Core.SetOptions(false);
    }

    public void GetAll()
    {
        string[] AllRewards = (Core.EnsureLoad(7401).Rewards.Select(i => i.Name)).Concat(Core.EnsureLoad(7403).Rewards.Select(i => i.Name)).Concat(Core.EnsureLoad(7405).Rewards.Select(i => i.Name)).ToArray();

        if (Core.CheckInventory(AllRewards, toInv: false))
            return;

        F13.Splatterwar();

        Bot.Drops.Add(AllRewards);

        Core.EquipClass(ClassType.Farm);

        Core.RegisterQuests(7401, 7403, 7405);
        while (!Bot.ShouldExit && !Core.CheckInventory(AllRewards, toInv: false))
            //Legion Medals 7401   //Mega Legion Medals 7403 //Jagged Canines 7405
            Core.KillMonster("splatterwarshrade", "r3", "Right", "*", log: false);
        Core.JumpWait();
        Core.ToBank(AllRewards);
        Core.CancelRegisteredQuests();

    }
}
