/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/NewYear/CoreNewYear.cs
using Skua.Core.Interfaces;

public class NewYear
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreNewYear NY = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        NY.NewYear();

        Core.SetOptions(false);
    }

}
