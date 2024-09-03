/*
name: Sovereign Of Storms Class
description: This script will farm the Sovereign Of Storms class (without the ultras part).
tags: sos, sovereign, sovereignofstorms, lothian treasury, queen iona, iona, class
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
//cs_include Scripts/Story/AgeOfRuin/CoreAOR.cs
//cs_include Scripts/Other/MergeShops/FelixsGildedGearMerge.cs
//cs_include Scripts/Other/MergeShops/LoughshineLootMerge.cs
//cs_include Scripts/Other/MergeShops/LiaTaraHillLootMerge.cs
//cs_include Scripts/Other/MergeShops/ColdThunderMerge.cs
//cs_include Scripts/Other/MergeShops/LothianTreasuryMerge.cs
using Skua.Core.Interfaces;

public class SovereignOfStorms
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreAdvanced Adv = new();
    private LothianTreasuryMerge LTM = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        GetSOS();

        Core.SetOptions(false);
    }

    public void GetSOS(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Sovereign of Storms"))
        {
            if (rankUpClass)
                Adv.RankUpClass("Sovereign of Storms");
            return;
        }

        LTM.BuyAllMerge("Sovereign of Storms");

        if (rankUpClass)
            Adv.RankUpClass("Sovereign of Storms");
    }
}



