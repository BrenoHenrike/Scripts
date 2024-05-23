/*
name: SoulForge Hammer
description: This script will get SoulForge Hammer.
tags: soul, forge, soulforge, hammer, unlock soul forge, soul forge hammer, legion, dage, underworld
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Legion/CoreLegion.cs
using Skua.Core.Interfaces;

public class SoulForgeHammer
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreLegion Legion = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Legion.SoulForgeHammer();

        Core.SetOptions(false);
    }
}
