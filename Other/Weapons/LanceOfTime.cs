/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;
public class LanceOfTime
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetLoT();

        Core.SetOptions(false);
    }

    public void GetLoT()
    {
        if (Core.CheckInventory("Lance of Time"))
            return;

        Core.AddDrop("Lance of Time");
        Core.EnsureAccept(4512);
        Core.EquipClass(ClassType.Farm);
        Core.HuntMonster("celestialrealm", "Infernal Imp", "Captured Time", 500, false);
        Core.EnsureComplete(4512);
        Bot.Wait.ForPickup("Lance of Time");
    }
}
