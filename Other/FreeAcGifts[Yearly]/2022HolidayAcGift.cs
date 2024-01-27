/*
name: Free Holiday AC Gift 2022
description: This script will kill the Frogzard in Battleon Town to get a free AC Giftbox.
tags: free, ac, gift, box, frogzard, battleon, town,500,2022,holiday
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class HolidayAC2022
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
        Core.OneTimeMessage("WARNING", "This Quest is a ONE-TIME quest (per account).", true, true);
        Core.Logger("You need to be level 20 and have a verified email!");

        if (!Core.isCompletedBefore(9057))
        {
            Core.EnsureAccept(9057);
            Core.KillMonster("battleontown", "Enter", "Spawn", "Frogzard", "Free AC Giftbox");
            Core.EnsureComplete(9057);
        }
    }
}
