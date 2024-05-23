/*
name: Dark Spirit Orbs
description: This script will farm 10500 Dark Spirit Orbs.
tags: dso, a penny for your foughts, sdka, evil, quest, farm
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Evil/SDKA/CoreSDKA.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class DarkSpiritOrbs
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreSDKA SDKA = new();
    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        SDKA.DSO(10500);

        Core.SetOptions(false);
    }
}
