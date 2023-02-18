/*
name: Gravelyn's Warrior Badge
description: Kills 100 skeletons to get you the Gravelyn's Warrior Badge
tags: gravelyn, warrior, badge, skeletons, shadowfall
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class GravelynsWarrior
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Badge();

        Core.SetOptions(false);
    }

    public void Badge()
    {
        if (Core.HasWebBadge(badge) || Core.isCompletedBefore(8671))
        {
            Core.Logger($"Already have the {badge} badge");
            return;
        }

        Core.EnsureAccept(8671);
        Core.KillMonster("shadowfall", "New6", "Left", "*", "Skeleton Defeated", 100);
        Core.EnsureComplete(8671);
    }

    private string badge = "Gravelyn's Warrior";
}
