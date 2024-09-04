/*
name: Yokai Ascension Badge
description: This will get the Yokai Ascension badge.
tags: badge, yokai, ascension, dragons, dragons of yokai, yokai realm
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/DragonsOfYokai/CoreDOY.cs
using Skua.Core.Interfaces;

public class YokaiAscension
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    private CoreDOY DOY = new();
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

        Core.Logger($"Doing Yokai Realm story for {badge} badge");

        DOY.YokaiRealm();
    }

    private string badge = "Yokai Ascension";
}
