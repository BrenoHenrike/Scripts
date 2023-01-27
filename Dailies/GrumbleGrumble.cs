/*
name:  Grumble Grumble Daily
description:  Grumble Grumble
tags: daily, grumble grumble
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
using Skua.Core.Interfaces;

public class GrumbleGrumble
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDailies Daily = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Daily.GrumbleGrumble();

        Core.SetOptions(false);
    }
}
