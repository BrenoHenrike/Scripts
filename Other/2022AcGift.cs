/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class FreeAcGift
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        FreeAcs();

        Core.SetOptions(false);
    }

    public void FreeAcs()
    {
        if (!Core.isCompletedBefore(9057))
        {
            Core.EnsureAccept(9057);
            Core.KillMonster("battleontown", "Enter", "Spawn", "Frogzard", "Free AC Giftbox");
            Core.EnsureComplete(9057);
        }
    }
}
