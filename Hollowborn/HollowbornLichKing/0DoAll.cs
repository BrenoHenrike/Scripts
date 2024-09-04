/*
name: Hollowborn Lich King - Do all
description: does the *All* part of hollowborn Lich King
tags: hollowborn lich king, hollowborn, doall, do all
*/
//cs_include Scripts/Hollowborn/HollowbornLichKing/CoreHollowbornLichKing.cs
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
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;
using System.Linq;

public class HBLK0
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private static CoreHollowbornLichKing sHBLK = new();

    public string OptionsStorage = sHBLK.OptionsStorage;
    public bool DontPreconfigure = true;
    public List<IOption> Options = sHBLK.Options;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        sHBLK.GetAll();

        Core.SetOptions(false);
    }
}
