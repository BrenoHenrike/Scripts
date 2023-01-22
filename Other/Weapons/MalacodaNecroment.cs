/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class MalacodaNecroment
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        FreeLegendary();

        Core.SetOptions(false);
    }

    public void FreeLegendary()
    {
        if (Core.CheckInventory("Malacoda Necroment"))
            return;

        Core.AddDrop("Malacoda Necroment");
        Core.EnsureAccept(3577);
        Core.HuntMonster("battleundera", "Lich", "Crystal of Reanimation");
        Core.EnsureComplete(3577, 24217);
    }
}
