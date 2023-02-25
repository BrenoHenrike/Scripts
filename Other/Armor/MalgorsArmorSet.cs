/*
name: Malgors Armor Set
description: completes the quest "build malgor's armor Set" for the set.
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs

//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
//cs_include Scripts/Legion/YamiNoRonin/CoreYnR.cs
//cs_include Scripts/Other/MergeShops/DeadLinesMerge.cs
//cs_include Scripts/Other/MergeShops/RuinedCrownMerge.cs
//cs_include Scripts/Other/MergeShops/TimekeepMerge.cs
//cs_include Scripts/Other/MergeShops/StreamwarMerge.cs
//cs_include Scripts/Other/MergeShops/WorldsCoreMerge.cs
//cs_include Scripts/Other/MergeShops/ManaCradleMerge.cs

//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Legion/SwordMaster.cs

using Skua.Core.Interfaces;


public class MalgorsArmorSet
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new();
    public CoreSoW SoW = new();
    public CoreYnR YNR = new();
    private DeadLinesMerge DeadLinesMerge = new();
    private RuinedCrownMerge RuinedCrownMerge = new();
    private TimekeepMerge TimekeepMerge = new();
    private StreamwarMerge StreamwarMerge = new();
    private WorldsCoreMerge WorldsCoreMerge = new();
    private ManaCradleMerge ManaCradleMerge = new();

    string[] Set =
    {
    "Malgor the ShadowLord",
    "ShadowLord's Helm"
    };

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        GetSet();

        Core.SetOptions(false);
    }

    public void GetSet()
    {
        if (Core.CheckInventory(Set))
            return;

        YNR.GetYnR();

        while (!Bot.ShouldExit && !Core.CheckInventory(Set))
        {
            Adv.BuyItem("alchemyacademy", 395, "Gold Voucher 500k", 30);
            DeadLinesMerge.BuyAllMerge(buyOnlyThis: "Timestream Ravager");
            RuinedCrownMerge.BuyAllMerge(buyOnlyThis: "ShadowFlame Defender");
            TimekeepMerge.BuyAllMerge(buyOnlyThis: "Mana Guardian");
            StreamwarMerge.BuyAllMerge(buyOnlyThis: "Dark Dragon Slayer");
            WorldsCoreMerge.BuyAllMerge(buyOnlyThis: "Mystical Devotee of Mana");
            ManaCradleMerge.BuyAllMerge(buyOnlyThis: "Dragon's Tear");
            Core.ChainComplete(9127);
        }
    }
}
