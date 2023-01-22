/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class HordeZombieSLAYER

{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

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

        Core.Logger($"Doing quest for {badge} badge");

        Core.EquipClass(ClassType.Farm);

        Core.EnsureAccept(8670);
        Core.KillMonster("darkoviahorde", "r8", "Right", "*", "Zombie Defeated", 100, log: false);
        Core.EnsureComplete(8670);

    }

    private string badge = "Zombie Slayer";
}
