/*
name: Posty Mc Nobbins Quest Rewards
description: gest the quest rewards from all 4 quest from mc nobbins.
tags: mc nobbins, quest reward, farm, lucky day, luck, seasonal
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs

using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class PostyMcNobbinsQuestRewards
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv => new();

    string[] PotDrops =
    {
    "Leprechaun Ranger",
    "Leprechaun Ranger's TopHat",
    "Leprechaun Ranger's TopHat + Locks",
    "Lucky Day TopHat",
    "Lucky Day TopHat + Glasses",
    "Lucky Day TopHat + Locks",
    "Lucky Emerald TopHat",
    "Lucky Emerald TopHat + Glasses",
    "Lucky Emerald TopHat + Locks",
    "Platinum Coin",
    "Rainbow Coin",
    };


    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Platinum Coin", "Rainbow Coin" });
        Core.SetOptions();

        test1();

        Core.SetOptions(false);
    }

    public void AllRewards()
    {
        if (!Core.isSeasonalMapActive("luck"))
            return;

        string[] AllRewards = (Core.EnsureLoad(5758).Rewards.Select(i => i.Name)).Concat(Core.EnsureLoad(5759).Rewards.Select(i => i.Name)).Concat(Core.EnsureLoad(5760).Rewards.Select(i => i.Name)).Concat(Core.EnsureLoad(5761).Rewards.Select(i => i.Name)).Concat(PotDrops).ToArray();

        Core.EquipClass(ClassType.Solo);
        Adv.SmartEnhance(Core.SoloClass);
        //Adv.BestGear(GenericGearBoost.dmgAll);

        Bot.Drops.Add(AllRewards);

        Core.RegisterQuests(5758, 5759, 5760, 5761);
        while (!Bot.ShouldExit && !Core.CheckInventory(AllRewards, toInv: false))
            Core.HuntMonster("luck", "Pot O' Gold", log: false);
        Core.CancelRegisteredQuests();
        Core.ToBank(AllRewards);
    }


    void test1()
    {
        Core.OneTimeMessage("ReadMe", "plese ignore the \"item cannot be added inventory\" message ingame just means you already have the item in the bank, should only be doing this for the drops from the pot.", true, true);

        var rewards1 = Core.QuestRewards(5758);
        var rewards2 = Core.QuestRewards(5759);
        var rewards3 = Core.QuestRewards(5760);
        var rewards4 = Core.QuestRewards(5761);

        string[] AllDrops = rewards1.Concat(rewards2).Concat(rewards3).Concat(rewards4).Concat(PotDrops).ToArray();

        foreach (string item in AllDrops)
        {
            if (!Core.CheckInventory(item, toInv: false))
                Bot.Drops.Add(item);
        }
        Core.AddDrop("Platinum Coin", "Rainbow Coin");
        Core.RegisterQuests(5758, 5759, 5760, 5761);
        foreach (string item in AllDrops)
        {
            while (!Bot.ShouldExit && !Core.CheckInventory(AllDrops, toInv: false))
            {
                if (Core.CheckInventory(item, toInv: false))
                {
                    Core.Logger($"{item} already exists in Inventory/Bank");
                    Bot.Drops.Remove(item);
                    return;
                }

                Core.FarmingLogger(item, 1);
                Core.HuntMonster("luck", "Pot O' Gold", item, isTemp: false, log: false);
                Bot.Drops.Remove(item);
                Core.ToBank(item);
            }
        }
        Core.CancelRegisteredQuests();
    }
}
