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
// using Skua.Core.Options;

public class BeetleGeneralPetQuest
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private TempleSiegeMerge TSM = new();
    private CoreNation Nation = new();
    private CoreHollowborn HB = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        AutoReward();

        Core.SetOptions(false);

    }

    public void AutoReward(int questID = 9076, int quant = 1, string rewardItemName = "")
    {
        // Add drops and experience at the beginning
        Core.AddDrop("Baby Chaos Dragon", "Reaper's Soul"); // Using names for these two
        Farm.Experience(80);

        // Beetle General Pet
        TSM.BuyAllMerge("Beetle General Pet");

        List<ItemBase> RewardOptions = Core.EnsureLoad(questID).Rewards;

        // Find the ItemBase object that matches the rewardItemName using case-insensitive comparison
        if (!string.IsNullOrEmpty(rewardItemName))
        {
            ItemBase? rewardItem = RewardOptions.Find(item => item.Name.Equals(rewardItemName, StringComparison.OrdinalIgnoreCase));
            if (rewardItem == null)
            {
                Core.Logger("The specified reward item name is not available as a reward.");
                return;
            }

            int rewardItemID = rewardItem.ID;
            Core.AddDrop(rewardItemID);
            int rewardItemQuantity = Bot.Inventory.GetItem(rewardItemID)?.Quantity ?? 0;

            if (rewardItemQuantity >= quant)
            {
                Core.Logger($"You already have the desired quantity ({quant}) of {rewardItemName}. Skipping banking for this item.");
                return;
            }

            while (rewardItemQuantity < quant)
            {
                PerformQuestActions(questID);
                Core.EnsureComplete(questID, rewardItemID);
                rewardItemQuantity = Bot.Inventory.GetItem(rewardItemID)?.Quantity ?? 0;
            }
        }
        else
        {
            foreach (ItemBase item in RewardOptions)
            {
                int itemQuantity = Bot.Inventory.GetItem(item.ID)?.Quantity ?? 0;

                // For non-specified part, check each item's MaxStack
                while (itemQuantity < (item.MaxStack > 1 ? item.MaxStack : 1))
                {
                    PerformQuestActions(questID);
                    Core.EnsureComplete(questID, item.ID);
                    // Check inventory without adding to inventory
                    itemQuantity = Core.CheckInventory(item.ID, toInv: false) ? Bot.Inventory.GetItem(item.ID)?.Quantity ?? 0 : item.MaxStack;
                }

                // After completing each quest, bank the rewards for non-specified items
                if (itemQuantity > 0)
                    Core.ToBank(item.ID);
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
}
