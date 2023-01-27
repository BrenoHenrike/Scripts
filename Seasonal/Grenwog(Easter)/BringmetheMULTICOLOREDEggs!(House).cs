/*
name: Easter Egg House
description: This will finish the Bring me the MULTICOLORED Egg quest to obtain the Easter Egg House.
tags: easter-egg-house, bring-me-the-multicoloredeggs, seasonal, easter
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class EasterEggHouse
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetMulticoloredEggs();

        Core.SetOptions(false);
    }

    public void GetMulticoloredEggs()
    {
        if (Bot.House.Contains("Easter Egg House"))
        {
            Core.Logger("You already own this House, Stopping Bot.");
            return;
        }

        Core.AddDrop("Multicolored Grenwog Egg");

        Core.EnsureAccept(5788);
        Core.HuntMonster("DarkoviaGrave", "Rainbow Chick", "Rainbow Chick");
        Core.GetMapItem(5226, 20, "DarkoviaGrave");
        Core.EnsureCompleteChoose(5788);
    }
}



