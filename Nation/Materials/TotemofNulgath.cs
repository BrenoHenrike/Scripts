/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;

public class TotemofNulgath
{
    public CoreBots Core => CoreBots.Instance;
    public CoreNation Nation = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Nation.FarmTotemofNulgath();

        Core.SetOptions(false);
    }
}
