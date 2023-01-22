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

public class AlphaPirate
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

        GetAlphaPirate();

        Core.SetOptions(false);
    }

    public void GetAlphaPirate(bool rankUpClass = true)
    {
        if (!Core.isSeasonalMapActive("blazebeard"))
            return;

        if (!Core.CheckInventory("Alpha Pirate Class Token"))
            return;

        Core.BuyItem("blazebeard", 108, "Alpha Pirate");

        if (rankUpClass)
            Adv.rankUpClass("Alpha Pirate");
    }
}
