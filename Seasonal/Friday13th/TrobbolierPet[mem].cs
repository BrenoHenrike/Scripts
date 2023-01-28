/*
name: Trobbolier Pet (Member Only)
description: This will finish the required quest to obtain all of the Trobbolier Pets.
tags: trobbolier-pet, member, friday-the-13th, seasonal
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Seasonal/Friday13th/Story/CoreFriday13th.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class TrobbolierPet
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFriday13th F13 = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetAll();

        Core.SetOptions(false);
    }

    public void GetAll()
    {
        F13.Wormhole();

        List<ItemBase> RewardOptions = Core.EnsureLoad(5067).Rewards;

        foreach (ItemBase item in RewardOptions)
            Core.AddDrop(item.Name);

        Core.EquipClass(ClassType.Farm);

        foreach (ItemBase Reward in RewardOptions)
        {
            if (Core.CheckInventory(Reward.Name, toInv: false))
                continue;
            Core.FarmingLogger(Reward.Name, 1);

            while (!Bot.ShouldExit && !Core.CheckInventory(Reward.Name))
            {
                Core.EnsureAccept(5067);
                Core.HuntMonster("wormhole", "Blue Trobbolier", "Blue Trobbolier Fluff", 4, false);
                Core.HuntMonster("wormhole", "Purple Trobbolier", "Purple Trobbolier Fluff", 4, false);
                Core.HuntMonster("wormhole", "Green Trobbolier", "Green Trobbolier Fluff", 4, false);
                Core.HuntMonster("wormhole", "Red Trobbolier", "Red Trobbolier Fluff", 4, false);
                Core.EnsureComplete(5067, Reward.ID);
                Core.JumpWait();
                Core.ToBank(Reward.Name);
            }
        }
    }
}
