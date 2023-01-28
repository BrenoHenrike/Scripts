/*
name:  Necrotic Sword of Doom Daily
description:  
tags: daily, nsod, necrotic sword of doom
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
using Skua.Core.Interfaces;

public class NSODDaily
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreDailies Daily = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Daily.NSoDDaily();

        Core.SetOptions(false);
    }
}


