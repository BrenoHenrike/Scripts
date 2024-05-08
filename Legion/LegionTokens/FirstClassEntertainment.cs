/*
name: FirstClassEntertainment
description: Uses the appropriate pet to farm Legion Tokens
tags: legion, legion token, solo
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class FirstClassEntertainment
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreLegion Legion = new();
    public CoreAdvanced Adv = new();
    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Legion.LTFirstClassEntertainment();

        Core.SetOptions(false);
    }
}
