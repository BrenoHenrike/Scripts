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
//cs_include Scripts/Legion/CoreLegion.cs

using Skua.Core.Interfaces;

public class GetLegionCastle
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDailies Daily = new();
    public CoreLegion Legion = new CoreLegion();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        AllYourBaseAreBelongToUs();

        Core.SetOptions(false);
    }
    public void AllYourBaseAreBelongToUs()
    {
        if (Core.CheckInventory("Legion Castle"))
            return;

        Legion.FarmLegionToken(25000);
        if (Core.CheckInventory("Shadow Shround", 15))
            Daily.ShadowShroud();

        Core.BuyItem("Underworld", 238, "Legion Castle");
    }
}
