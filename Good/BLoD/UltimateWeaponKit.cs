/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
using Skua.Core.Interfaces;

public class UltimateWeaponKit
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreBLOD BLOD = new CoreBLOD();
    public CoreStory Story = new CoreStory();
    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        BLOD.UltimateWK("Bright Aura", 10000);

        Core.SetOptions(false);
    }
}
