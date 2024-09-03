/*
name: Free Holiday AC Gift 2023
description: This script will kill Burlingster in /borgars to get free 500 AC.
tags: free, ac, burlingster, borgars, 500, 2023, holiday
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class HolidayAC2023
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
        Core.Logger("Quest has been Removed, blame AE");

        // Core.OneTimeMessage("WARNING", "This Quest is a ONE-TIME quest (per account).", true, true);

        // if (!Bot.Flash.CallGameFunction<bool>("world.myAvatar.isEmailVerified") || Bot.Player.Level < 20)
        // {
        //     Core.Logger("You need to be level 20 and have a verified email!");
        //     return;
        // }

        // if (!Core.isCompletedBefore(9578))
        // {
        //     Core.EnsureAccept(9578);
        //     Bot.Quests.UpdateQuest(7522);
        //     Core.EquipClass(ClassType.Solo);
        //     Core.HuntMonster("borgars", "Burglinster", "Cookie Dough");
        //     Core.EnsureComplete(9578);
        // }
    }
}
