/*
name: Free 500 accs
description: the 500 free acs quest
tags: acs, free
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class AcGift2023
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
        Core.OneTimeMessage("WARNING", "This Quest is a ONE-TIME quest (per account).", true, true);

        if (!Bot.Quests.IsAvailable(9444))
        {
            Core.Logger("Quest not avaible / is already completed.");
            return;
        }

        Core.EnsureAccept(9444);
        Core.HuntMonster("yulgar", "Agitated Orb", "Free ACs... and Yogurt");
        Core.EnsureComplete(9444);


    }
}
