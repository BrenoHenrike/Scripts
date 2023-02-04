/*
name: (ToD) Complete Throne of Darkness Story
description: This will finish the Throne of Darkness story.
tags: throne, darkness, farm, story, complete, throne, darkness, all
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs
using Skua.Core.Interfaces;

public class ThroneofDarkness
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreToD TOD = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        TOD.CompleteToD();

        Core.SetOptions(false);
    }
}
