/*
name: Trick Town Story
description: This will complete the Trick Town story quest.
tags: story, quest, mogloween, seasonal, trick, town
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/Mogloween/CoreMogloween.cs
using Skua.Core.Interfaces;

public class TrickTown
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreMogloween CoreMogloween = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        CoreMogloween.TrickTown();

        Core.SetOptions(false);
    }

}
