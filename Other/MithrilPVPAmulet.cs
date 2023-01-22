/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class MitrilPvPAmulet
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetAmulet();

        Core.SetOptions(false);
    }

    public void GetAmulet()
    {
        if (Core.CheckInventory(59667))
            return;

        Core.HuntMonster("thevoid", "Reaper", "Diamond PvP Amulet +5500", isTemp: false);
        Core.HuntMonster("thevoid", "Reaper", "Platinum PvP Amulet +5000", isTemp: false);

        Core.BuyItem(Bot.Map.Name, 222, "Mithril PvP Amulet +15000");
    }
}
