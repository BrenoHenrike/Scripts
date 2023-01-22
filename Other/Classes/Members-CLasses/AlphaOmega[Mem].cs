/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;
using Skua.Core.Models.Shops;

public class AlphaOmega
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreStory Story = new CoreStory();
    public CoreDailies Daily = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetAlphaOmega();

        Core.SetOptions(false);
    }

    public void GetAlphaOmega(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Alpha Omega") || !Core.IsMember)
            return;

        Farm.BludrutBrawlBoss("Combat Trophy", 300); 
        Core.BuyItem("digitalmain", 561, "Alpha Omega");

        if (rankUpClass)
            Adv.rankUpClass("Alpha Omega");
    }
}
