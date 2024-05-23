/*
name: Unlock HardCore Metals
description: This script will unlock HardCore Metals using Vayle's Quests.
tags: unlock, hardcore, metals, vayle, quest, sdka, sepulchure, doom that looms, toiling with terror, a penny for your foughts, dark spirit donation
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Evil/SDKA/CoreSDKA.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class UnlockHardCoreMetals_Vayle_Quests
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreSDKA SDKA = new();
    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        SDKA.UnlockHardCoreMetals();

        Core.SetOptions(false);
    }
}
