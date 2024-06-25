/*
name: Exalted Soul Cleaver
description: This scripts farms the Exalted Soul Cleaver class.
tags: exalted, soul, cleaver, legion, class, esc, dage, undead legion merge
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Legion/Revenant/CoreLR.cs
//cs_include Scripts/Legion/InfiniteLegionDarkCaster.cs
//cs_include Scripts/Story/Legion/SeraphicWar.cs
//cs_include Scripts/Legion/LegionExcercise/LegionExercise3.cs
//cs_include Scripts/Legion/LegionExcercise/LegionExercise4.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Nation/Various/DragonBlade[mem].cs
//cs_include Scripts/Legion/Various/UndeadLegionMerge.cs
using Skua.Core.Interfaces;

public class ExaltedSoulCleaver
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private UndeadLegionMerge ULM = new();
    private CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        GetClass();
        Core.SetOptions(false);
    }

    public void GetClass(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Exalted Soul Cleaver"))
        {
            Core.Logger("You already own Exalted Soul Cleaver class.");
            return;
        }

        ULM.BuyAllMerge("Exalted Soul Cleaver");

        if (rankUpClass)
            Adv.RankUpClass("Exalted Soul Cleaver");

    }
}
