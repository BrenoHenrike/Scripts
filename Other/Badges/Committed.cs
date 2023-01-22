/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Asylum.cs
using Skua.Core.Interfaces;

public class Committed
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public Asylum Asylum = new();

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

        Core.Logger($"Doing Asylum story for {badge} badge");
        Asylum.StoryLine();
    }

    private string badge = "Committed";
}
