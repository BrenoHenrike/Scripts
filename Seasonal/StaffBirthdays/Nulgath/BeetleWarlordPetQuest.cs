/*
name: Beetle Warlord Pet
description: This will complete the Beetle Warlord Pet quest.
tags: quest, beetle, warlord, pet, staff, birthday, nulgath
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
//cs_include Scripts/Seasonal/StaffBirthdays/Nulgath/BeetleGeneralPetQuest.cs

using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class BeetleWarlordPetQuest
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreNation Nation = new();
    private CoreAdvanced Adv = new();
    private BeetleGeneralPet BGP = new();
    private TempleSiegeMerge TSM = new();

    public string OptionsStorage = "BeetleGeneralPet";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        CoreBots.Instance.SkipOptions,
        new Option<bool>("Armoronly", "\"Void Beetle Warlord\" Only", "Only get \"Void Beetle Warlord\" for RGoW", false),
    };

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        AutoReward(9078, 1, Bot.Config!.Get<bool>("Armoronly") ? "" : "Void Beetle Warlord");

        Core.SetOptions(false);
    }

    public void AutoReward(int questID = 9078, int quant = 1, string rewardItemName = "")
    {
        Farm.Experience(80);
        GetBeetlWarlordPet();

        Core.AddDrop("Baby Chaos Dragon", "Reaper's Soul");
        List<ItemBase> RewardOptions = Core.EnsureLoad(questID).Rewards;

        if (!string.IsNullOrEmpty(rewardItemName))
        {
            ItemBase? rewardItem = RewardOptions.Find(item => item.Name.Equals(rewardItemName, StringComparison.OrdinalIgnoreCase));
            if (rewardItem == null)
            {
                Core.Logger("The specified reward item name is not available as a reward.");
                return;
            }

            Core.AddDrop(rewardItem.Name);
            int rewardItemQuantity = Bot.Inventory.GetItem(rewardItem.ID)?.Quantity ?? 0;

            if (rewardItemQuantity >= quant)
                return;

            while (rewardItemQuantity < quant)
            {
                PerformQuestActions(questID);
                Core.EnsureComplete(questID, rewardItem.ID);
                Core.ToBank(rewardItem.ID);

                rewardItemQuantity = Bot.Inventory.GetItem(rewardItem.ID)?.Quantity ?? 0;
            }
        }
        else
        {
            foreach (ItemBase item in RewardOptions)
            {
                int itemQuantity = Bot.Inventory.GetItem(item.ID)?.Quantity ?? 0;

                while (itemQuantity < item.MaxStack)
                {
                    PerformQuestActions(questID);
                    Core.EnsureComplete(questID, item.ID);
                    Core.ToBank(item.ID);

                    itemQuantity = Core.CheckInventory(item.ID, toInv: false) ? Bot.Inventory.GetItem(item.ID)?.Quantity ?? 0 : item.MaxStack;
                }
            }
        }
    }

    private void PerformQuestActions(int questID)
    {
        Core.EnsureAccept(questID);
        Nation.FarmTotemofNulgath(1);
        Core.EquipClass(ClassType.Farm);
        Core.HuntMonster("dragonchallenge", "Chaos Dragon", "Baby Chaos Dragon", isTemp: false);
        Core.EquipClass(ClassType.Solo);
        Core.HuntMonster("thevoid", "Reaper", "Reaper's Soul", isTemp: false);
    }

    private void GetBeetlWarlordPet(params string[] items)
    {
        if (Core.CheckInventory(75663))
            return;

        Core.EnsureAccept(9077);
        Adv.BuyItem("tercessuinotlim", 1951, "Unmoulded Fiend Essence");
        BGP.AutoReward();
        Core.EnsureComplete(9077);
    }
}
