/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;
public class GuardianOfSpiritsBlade
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetGoSB();

        Core.SetOptions(false);
    }

    public void GetGoSB()
    {
        if (Core.CheckInventory("Guardian of Spirits' Blade"))
            return;

        Core.AddDrop("Guardian of Spirits' Blade");
        Core.EnsureAccept(4510);
        Core.EquipClass(ClassType.Farm);
        Core.HuntMonster("celestialrealm", "Fallen Knight", "Trapped Spirits", 500, false);
        Core.EnsureComplete(4510);
        Bot.Wait.ForPickup("Guardian of Spirits' Blade");
    }
}
