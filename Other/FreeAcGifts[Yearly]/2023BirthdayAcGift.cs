/*
name: Free Birthday AC Gift 2023
description: This script will kill Agitated Orb for free 500 ACs.
tags: ac, free,500,2023,birthday
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class BirthdayAC2023
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        GetFreeAcs();
        Core.SetOptions(false);
    }

    public void GetFreeAcs()
    {
        Core.Logger("Quest has been Removed, blame AE");
        // Core.OneTimeMessage("WARNING", "This Quest is a ONE-TIME quest (per account).", true, true);

        // if (!Bot.Flash.CallGameFunction<bool>("world.myAvatar.isEmailVerified") || Bot.Player.Level < 20)
        // {
        //     Core.Logger("You need to be level 20 and have a verified email!");
        //     return;
        // }

        // if (!Bot.Quests.IsAvailable(9444))
        // {
        //     Core.Logger("Quest not avaible / is already completed.");
        //     return;
        // }

        // Core.EnsureAccept(9444);
        // Core.HuntMonster("yulgar", "Agitated Orb", "Free ACs... and Yogurt");
        // Core.EnsureComplete(9444);

    }
}
