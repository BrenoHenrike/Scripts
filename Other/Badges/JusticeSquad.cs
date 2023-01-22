/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class JusticeSquadBadge
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
        if (Core.HasWebBadge(badge))
        {
            Core.Logger($"Already have the {badge} badge");
            return;
        }

        Core.Logger($"Doing a quest for the {badge}");
        Core.AddDrop("Enchanted Justice Blade");
        Core.EnsureAccept(5722);
        Core.HuntMonster("battleontown", "Zard", "Quill Pen", isTemp: true, log: false);
        Core.EnsureComplete(5722);


    }

    private string badge = "Justice Squad";
}
