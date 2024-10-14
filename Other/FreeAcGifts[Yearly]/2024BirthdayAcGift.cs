/*
name: Free Birthday AC Gift 2023
description: This script will kill Agitated Orb for free 500 ACs.
tags: ac, free, 500, 2023, birthday
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class BirthdayAC2024
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
        if (Bot.Quests.IsAvailable(9937) && !Core.isCompletedBefore(9937))
        {
            Core.EnsureAccept(9937);
            Core.HuntMonster("yulgar", "Agitated Orb", "Free ACs...");
            Core.EnsureComplete(9937);
        }

    }
}
