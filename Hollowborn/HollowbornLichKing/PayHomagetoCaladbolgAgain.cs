/*
name: Pay Homage to Caladbolg (Again)
description: farms the requirements, and does the quest "Pay Homage to Caladbolg (Again)"
tags: Pay Homage to Caladbolg (Again), Caladbolg, hollowborn, hollowborn, lich, king, hollowborn lich king, Hollowborn Caladbolg, Dual Hollowborn Caladbolgs, Hollowborn Caladbolg Companion, Hollowborn Caladbolg Battle Companion
*/

//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Hollowborn/CoreHollowborn.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Hollowborn/HollowbornLichKing/CoreHollowbornLichKing.cs
//cs_include Scripts/Legion/Revenant/CoreLR.cs
//cs_include Scripts/Story/QueenofMonsters/CoreQOM.cs


//cs_include Scripts/Legion/MergeShops/UndeadLegionMerge.cs
//cs_include Scripts/Hollowborn/Materials/HollowSoul.cs
//cs_include Scripts/Legion/Various/Caladbolg.cs
//cs_include Scripts/Legion/Various/SoulSand.cs
//cs_include Scripts/Legion/Various/LetitBurn(SoulEssence).cs

//cs_include Scripts/Legion/InfiniteLegionDarkCaster.cs
//cs_include Scripts/Story/Legion/SeraphicWar.cs
//cs_include Scripts/Legion/LegionExcercise/LegionExercise3.cs
//cs_include Scripts/Legion/LegionExcercise/LegionExercise4.cs
//cs_include Scripts/Nation/Various/DragonBlade[mem].cs
//cs_include Scripts/Legion/Various/LegionBonfire.cs

using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;

public class PayHomagetoCaladbolgAgain
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    public CoreLegion Legion = new();
    private CoreHollowbornLichKing CoreHollowbornLichKing = new();
    private HollowSoul HS = new();
    private Caladbolg Caladbolg = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetRewards();

        Core.SetOptions(false);
    }

    public void GetRewards(int QuestID = 9641)
    {
        if (Core.EnsureLoad(QuestID).Rewards.All(reward => Core.CheckInventory(reward.Name, toInv: false)))
            return;

        Caladbolg.GetCaladbolg(false);
        CoreHollowbornLichKing.Counterblow(CoreHollowbornLichKing.CounterblowRewards.Altar_Of_the_Hollowborn_Caladbolg);

        List<ItemBase> RewardOptions = Core.EnsureLoad(QuestID).Rewards;

        foreach (ItemBase item in RewardOptions)
            Core.AddDrop(item.Name);

        foreach (ItemBase Reward in RewardOptions)
        {
            if (Core.CheckInventory(Reward.Name, toInv: false))
                return;

            Core.Logger(Core.CheckInventory(Reward.ID, toInv: false) ? $"{Reward.Name}: ✅" : $"{Reward.Name} ❌");
            Core.RegisterQuests(QuestID);
            while (!Bot.ShouldExit && !Core.CheckInventory(Reward.ID))
            {
                Legion.FarmLegionToken(5);
                HS.GetYaSoulsHeeeere(1);
            }
            Core.CancelRegisteredQuests();

            Core.JumpWait();
            Core.ToBank(Reward.Name);
        }
    }
}
