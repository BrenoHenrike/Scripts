/*
name: Shadow Dragon Shinobi (Seasonal) Class
description: This script farms the Shadow Dragon Shinobi class.
tags: seasonal, class, shadow, dragon, black friday, shinobi
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Seasonal/BlackFriday/ShadowDragonShinobiMerge.cs
using Skua.Core.Interfaces;

public class ShadowDragonShinobi
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreAdvanced Adv = new();
    private ShadowDragonShinobiMerge SDSM = new();
    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        GetClass();
        Core.SetOptions(false);
    }

    public void GetClass(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Shadow Dragon Shinobi") || !Core.isSeasonalMapActive("blackfridaywar"))
        {
            Core.Logger(Core.CheckInventory("Shadow Dragon Shinobi") ? "You already own Shadow Dragon Shinobi class." : "This class is only available during the Black Friday event.");
            return;
        }

        SDSM.BuyAllMerge("Shadow Dragon Shinobi");
        if (rankUpClass)
            Adv.RankUpClass("Shadow Dragon Shinobi");

    }
}
