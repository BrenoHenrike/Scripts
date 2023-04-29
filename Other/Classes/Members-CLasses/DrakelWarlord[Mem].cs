/*
name: Drakel Warlord (Member) Class
description: This script farms the Drakel Warlord class.
tags: warlord,member,class,deathpitarena
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/Other\MergeShops\DeathPitArenaRepMerge.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class DrakelWarlord
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private DeathPitArenaRepMerge DPARM = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        GetClass();
        Core.SetOptions(false);
    }

    public void GetClass()
    {
        if (Core.CheckInventory("Drakel Warlord"))
            return;

        DPARM.BuyAllMerge("Drakel Warlord");
    }
}
