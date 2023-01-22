/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/RavenlossSaga.cs
using Skua.Core.Interfaces;

public class RavenlossWarAndChampion
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public RavenlossSaga Ravenloss = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Badge();

        Core.SetOptions(false);
    }

    public void Badge()
    {
        if (Core.HasWebBadge("Ravenloss War") || Core.HasWebBadge("Ravenloss Champion"))
        {
            Core.Logger("Already have the Ravenloss War and Champion badge");
            return;
        }

        Core.Logger("Doing Ravenloss story for Ravenloss War and Champion badge");
        Ravenloss.DoAll();

        Core.EquipClass(ClassType.Farm);
        Core.EnsureAccept(8668);
        Core.HuntMonster("ravenloss", "ChaosWeaver Magi", "ChaosWeaver Defeated", 100, log: false);
        Core.EnsureComplete(8668);
    }

}
