/*
name: Alpha Pirate Class
description: This will merge the Alpha Pirate Class Token.
tags: merge-shop, alpha, pirate, class
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Seasonal/TalkLikeaPirateDay/BlazeBeardStory.cs
//cs_include Scripts/Seasonal/TalkLikeaPirateDay/MergeShops/BlazeBeardMerge.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;
using Skua.Core.Models.Shops;

public class AlphaPirate
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new();
    private BlazeBeardMerge BBM = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetAlphaPirate();

        Core.SetOptions(false);
    }

    public void GetAlphaPirate(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Alpha Pirate") || !Core.isSeasonalMapActive("blazebeard"))
            return;

        BBM.BuyAllMerge("Alpha Pirate");

        if (rankUpClass)
            Adv.RankUpClass("Alpha Pirate");
    }
}
