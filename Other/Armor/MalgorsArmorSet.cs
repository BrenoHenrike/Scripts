/*
name: Malgors Armor Set
description: completes the quest "build malgor's armor Set" for the set.
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/ShadowsOfWar/CoreSoWMats.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
//cs_include Scripts/Legion/CoreLegion.cs

//cs_include Scripts/Legion/YamiNoRonin/CoreYnR.cs
//cs_include Scripts/ShadowsOfWar/MergeShops/DeadLinesMerge.cs
//cs_include Scripts/ShadowsOfWar/MergeShops/ShadowflameFinaleMerge.cs
//cs_include Scripts/ShadowsOfWar/MergeShops/TimekeepMerge.cs
//cs_include Scripts/ShadowsOfWar/MergeShops/StreamwarMerge.cs
//cs_include Scripts/ShadowsOfWar/MergeShops/WorldsCoreMerge.cs
//cs_include Scripts/ShadowsOfWar/MergeShops/ManaCradleMerge.cs

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
    private ShadowflameFinaleMerge ShadowflameFinaleMerge = new();
    private TimekeepMerge TimekeepMerge = new();
    private StreamwarMerge StreamwarMerge = new();
    private WorldsCoreMerge WorldsCoreMerge = new();
    private ManaCradleMerge ManaCradleMerge = new();

    string[] ArmorSet =
    {
    "Malgor the ShadowLord",
    "ShadowLord's Helm"
    };

    string[] QuestItems =
    {
    "Timestream Ravager",
    "ShadowFlame Defender",
    "Mana Guardian",
    "Dark Dragon Slayer",
    "Mystical Devotee of Mana",
    "Dragon's Tear"
    };

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(ArmorSet.Concat(QuestItems));
        Core.SetOptions();

        GetSet(true, ArmorSet);

        Core.SetOptions(false);
    }

    public void GetSet(bool useSet = true, string[]? item = null)
    {
        string[]? items = useSet ? ArmorSet : item;

        if (Core.CheckInventory(items))
        {
            Core.ToBank(SoW.MalgorDrops.Concat(SoW.MainyuDrops).ToArray());
            return;
        }

        Adv.GearStore();
        Core.BossClass();

        while (!Bot.ShouldExit && !Core.CheckInventory(items))
        {
            DeadLinesMerge.BuyAllMerge(buyOnlyThis: "Timestream Ravager");
            ShadowflameFinaleMerge.BuyAllMerge(buyOnlyThis: "ShadowFlame Defender");
            TimekeepMerge.BuyAllMerge(buyOnlyThis: "Mana Guardian");
            StreamwarMerge.BuyAllMerge(buyOnlyThis: "Dark Dragon Slayer");
            WorldsCoreMerge.BuyAllMerge(buyOnlyThis: "Mystical Devotee of Mana");
            ManaCradleMerge.BuyAllMerge(buyOnlyThis: "Dragon's Tear");
            Adv.BuyItem("alchemyacademy", 395, "Gold Voucher 500k", 30);
            Core.Unbank(QuestItems);
            Core.ChainComplete(9127);
        }
        Adv.GearStore(true);
        Core.ToBank(SoW.MalgorDrops.Concat(SoW.MainyuDrops).ToArray());
    }
}
