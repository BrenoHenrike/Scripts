/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/Mogloween/CoreMogloween.cs
using Skua.Core.Interfaces;

public class AsylumMogloween
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreMogloween CoreMogloween = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        CoreMogloween.Asylum();

        Core.SetOptions(false);
    }

}
