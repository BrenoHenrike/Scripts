/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Seasonal/StaffBirthdays/Nulgath/TempleSiege.cs
//cs_include Scripts/Nation/Various/DragonBlade[mem}.cs
//cs_include Scripts/Seasonal/StaffBirthdays/Nulgath/TempleSiegeMerge.cs
//cs_include Scripts/Hollowborn/CoreHollowborn.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
// using Skua.Core.Options;

public class BeetleGeneralPet
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    private TempleSiegeMerge TSM = new();
    private CoreNation Nation = new();
    private CoreHollowborn HB = new();
    int questID = 9076;
    int quant = 1;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        RequiredItemsandQuests("Beetle General Pet");
        AutoReward(questID, quant);

        Core.SetOptions(false);

    }

    public void AutoReward(int questID, int quant)
    {
        List<ItemBase> RewardOptions = Core.EnsureLoad(questID).Rewards;

        foreach (ItemBase item in RewardOptions)
            Core.AddDrop(item.Name);

        Core.AddDrop("Red Ant Pet", "Beetle EXP");
        foreach (ItemBase item in RewardOptions)
        {
            while (!Bot.ShouldExit && !Core.CheckInventory(item.ID, toInv: false))
            {
                Core.EnsureAccept(questID);
                Core.HuntMonster("giant", "Red Ant", "Red Ant Pet", isTemp: false);
                Nation.EssenceofNulgath(10);
                HB.FreshSouls(1, 10);
                Core.EnsureComplete(questID, item.ID);
                Core.JumpWait();
                Core.ToBank(item.ID);
            }
        }
    }

    void RequiredItemsandQuests(params string[] items)
    {
        if (!Core.CheckInventory("Beetle General Pet"))
            TSM.BuyAllMerge("Beetle General Pet");
    }
}
