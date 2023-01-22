/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Story/SkyGuardSaga.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class SkyPirateBadge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public SkyGuardSaga SkyGuardSaga = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Badge();

        Core.SetOptions(false);
    }

    public void Badge()
    {
        if (!Core.IsMember)
            return;

        if (Core.HasWebBadge("SkyPirate Slayer"))
        {
            Core.Logger("Already have the SkyPirate Slayer badge");
            return;
        }

        SkyGuardSaga.DoAll();

        Core.AddDrop("SkyPirate Annhilator Recognition");
        Core.EquipClass(ClassType.Farm);

        Core.EnsureAccept(1291);
        Core.KillMonster("strategy", "r22", "Left", "*", "SkyPirate Annihilator Token", 100, log: false);
        Core.EnsureComplete(1291);

        Core.JumpWait();
    }
}
