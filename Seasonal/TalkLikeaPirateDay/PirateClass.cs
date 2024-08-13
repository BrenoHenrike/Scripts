/*
name: Pirate Class
description: This script will get the Pirate Class.
tags: merge-shop, pirate, class, tlapd, talk-like-a-pirate-day, seasonal
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

public class PirateClass
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new();
    private BlazeBeardMerge BBM = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetPirate();

        Core.SetOptions(false);
    }

    public void GetPirate(bool rankUpClass = true)
    {
        if (Core.CheckInventory(new[] { "Classic Pirate", "Pirate" }, any: true))
        {
            if (rankUpClass)
                Adv.RankUpClass(Core.CheckInventory("Classic Pirate") ? "Classic Pirate" : "Pirate");
            return;
        }

        if (!Core.isSeasonalMapActive("blazebeard"))
            return;

        BBM.BuyAllMerge("Pirate");

        if (rankUpClass)
            Adv.RankUpClass("Pirate");
    }
}
