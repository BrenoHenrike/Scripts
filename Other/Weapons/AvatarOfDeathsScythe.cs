/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;
public class AvatarOfDeathsScythe
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetAoDS();

        Core.SetOptions(false);
    }

    public void GetAoDS()
    {
        if (Core.CheckInventory("Avatar Of Death's Scythe"))
            return;

        Core.AddDrop("Avatar Of Death's Scythe");
        Core.EnsureAccept(4511);
        Core.EquipClass(ClassType.Farm);
        Core.HuntMonster("lostruins", "Underworld Hound", "Energy of Death", 500, false);
        Core.EnsureComplete(4511);
        Bot.Wait.ForPickup("Avatar Of Death's Scythe");
    }
}
