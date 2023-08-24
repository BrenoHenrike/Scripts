/*
name: Beetle General Pet
description: This will complete the Beetle General Pet quest.
tags: quest, beetle, general, pet, staff, birthday, nulgath
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Seasonal/StaffBirthdays/Nulgath/TempleSiege.cs
//cs_include Scripts/Nation/Various/DragonBlade[mem].cs
//cs_include Scripts/Seasonal/StaffBirthdays/Nulgath/TempleSiegeMerge.cs
//cs_include Scripts/Hollowborn/CoreHollowborn.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class BeetleGeneralPet
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    private TempleSiegeMerge TSM = new();
    private CoreNation Nation = new();
    private CoreHollowborn HB = new();
    public CoreAdvanced Adv => new();


    public string OptionsStorage = "BeetleGeneralPet";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        CoreBots.Instance.SkipOptions,
        new Option<bool>("OnlyPet", "Only get the pet", " only get the warlord pet? or get the rest of the rewards(cosmetics)", false),
    };

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        RequiredItemsandQuests("Beetle General Pet");
        AutoReward();

        Core.SetOptions(false);

    }

    public void AutoReward(int questID = 9076)
    {
        Core.AddDrop("Red Ant Pet", "Beetle EXP", "Beetle Warlord Pet");

        while (!Bot.ShouldExit && !Core.CheckInventory("Beetle EXP", 8))
        {
            Core.EnsureAccept(questID);
            Core.HuntMonster("giant", "Red Ant", "Red Ant Pet", isTemp: false);
            Nation.EssenceofNulgath(10);
            HB.FreshSouls(1, 10);
            Core.EnsureComplete(questID);
            Core.JumpWait();
            Core.ToBank(Core.QuestRewards(questID));
        }
        Adv.BuyItem("tercessuinotlim", 1951, "Unmoulded Fiend Essence");
        Core.ChainComplete(9077);
        Bot.Wait.ForPickup("Beetle Warlord Pet");

        if (Bot.Config!.Get<bool>("OnlyPet"))
        {
            List<ItemBase> RewardOptions = Core.EnsureLoad(questID).Rewards;
            foreach (ItemBase item in RewardOptions)
                Core.AddDrop(item.Name);
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
    }

    void RequiredItemsandQuests(params string[] items)
    {
        if (!Core.CheckInventory("Beetle General Pet"))
            TSM.BuyAllMerge("Beetle General Pet");
    }
}