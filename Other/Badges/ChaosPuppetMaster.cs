/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Story/QueenofMonsters/CoreQOM.cs
using Skua.Core.Interfaces;

public class ChaosPuppetMaster
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreQOM QOM = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Badge();

        Core.SetOptions(false);
    }

    public void Badge()
    {
        if (Core.HasWebBadge(badge))
        {
            Core.Logger($"Already have the {badge} badge");
            return;
        }

        Core.Logger($"Doing QOM story for {badge} badge");
        QOM.CompleteEverything();

    }

    private string badge = "Chaos PuppetMaster";
}
