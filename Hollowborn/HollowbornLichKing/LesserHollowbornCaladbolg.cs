/*
name: Lesser Hollowborn Caladbolg
description: This script will get Lesser Hollowborn Caladbolg by completing "How Much For One Rib?" quest.
tags: lesser caladbolg, caladbolg, hollowborn, dark unicorn rib, how much for one rib, quest
*/
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Hollowborn/CoreHollowborn.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Legion/Revenant/CoreLR.cs
//cs_include Scripts/Story/QueenofMonsters/CoreQOM.cs
//cs_include Scripts/Hollowborn/HollowbornLichKing/CoreHollowbornLichKing.cs
//cs_include Scripts/Legion/MergeShops/UndeadLegionMerge.cs
//cs_include Scripts/Legion/Various/SoulSand.cs
//cs_include Scripts/Hollowborn/Materials/HollowSoul.cs
//cs_include Scripts/Legion/Various/LetitBurn(SoulEssence).cs
//cs_include Scripts/Legion/InfiniteLegionDarkCaster.cs
//cs_include Scripts/Story/Legion/SeraphicWar.cs
//cs_include Scripts/Legion/LegionExcercise/LegionExercise3.cs
//cs_include Scripts/Legion/LegionExcercise/LegionExercise4.cs
//cs_include Scripts/Nation/Various/DragonBlade[mem].cs
//cs_include Scripts/Legion/Various/LegionBonfire.cs
using Skua.Core.Interfaces;

public class LesserHollowbornCaladbolg
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreHollowbornLichKing CoreHollowbornLichKing = new();
    private CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetLHC();

        Core.SetOptions(false);
    }

    public void GetLHC()
    {
        // Lesser Hollowborn Caladbolg
        if (Core.CheckInventory(85029, toInv: false))
            return;


        Core.AddDrop("Lesser Hollowborn Caladbolg", "Darkon's Receipt", "Ingredients?", "Hollow Soul");

        CoreHollowbornLichKing.Counterblow(CoreHollowbornLichKing.CounterblowRewards.Altar_Of_the_Hollowborn_Caladbolg);

        Core.EquipClass(ClassType.Solo);
        // for darkon receipts v
        Core.RegisterQuests(7325, 9665);
        while (!Bot.ShouldExit && !Core.CheckInventory(85029))
            Core.HuntMonster("doomvault", "Binky", "Dark Unicorn Rib", isTemp: false, log: false);
        Core.CancelRegisteredQuests();
        Core.Logger("RNG gods have smiled upon you! Banking the weapon.");
        Core.ToBank("Lesser Hollowborn Caladbolg");

    }
}
