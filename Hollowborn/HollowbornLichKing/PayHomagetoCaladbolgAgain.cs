/*
name: Pay Homage to Caladbolg (Again)
description: farms the requirements, and does the quest "Pay Homage to Caladbolg (Again)"
tags: pay homage to caladbolg (again), caladbolg, hollowborn, hollowborn, lich, king, hollowborn lich king, hollowborn caladbolg, dual hollowborn caladbolgs, hollowborn caladbolg companion, hollowborn caladbolg battle companion
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

    public void GetRewards()
    {
        if (Core.EnsureLoad(9641).Rewards.All(reward => Core.CheckInventory(reward.Name, toInv: false))
        || !Core.CheckInventory("Altar Of Caladbolg"))
        {
            Core.Logger(!Core.CheckInventory("Altar Of Caladbolg") ? "This bot requires you to have a \"Altar Of Caladbolg\". Stopping the bot." : "All rewards owned, stopping the bot");
            return;
        }

        List<ItemBase> RewardOptions = Core.EnsureLoad(9641).Rewards;

        foreach (ItemBase item in RewardOptions)
            Core.AddDrop(item.ID);

        Caladbolg.GetCaladbolg(false);
        CoreHollowbornLichKing.Counterblow(CoreHollowbornLichKing.CounterblowRewards.Altar_Of_the_Hollowborn_Caladbolg);

        foreach (ItemBase Reward in RewardOptions)
        {
            if (Core.CheckInventory(Reward.Name, toInv: false))
                return;

            Core.Logger(Core.CheckInventory(Reward.ID, toInv: false) ? $"{Reward.Name}: ✅" : $"{Reward.Name} ❌");
            // Core.ResetQuest(9641);
            while (!Bot.ShouldExit && !Core.CheckInventory(Reward.ID))
            {
                Core.EnsureAccept(9641);
                Legion.FarmLegionToken(5);
                HS.GetYaSoulsHeeeere(1);
                Core.EnsureCompleteMulti(9641);
            }

            Core.JumpWait();
            Core.ToBank(Reward.Name);
        }
        Core.CancelRegisteredQuests();
    }
}
