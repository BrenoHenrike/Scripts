/*
name: Complete All Mogloween Story
description: This will complete all Mogloween story quests.
tags: story, quest, mogloween, seasonal, complete, all
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/Mogloween/CoreMogloween.cs
using Skua.Core.Interfaces;

public class DoAllMogloween
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreMogloween CoreMogloween = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        CoreMogloween.DoAll();

        Core.SetOptions(false);
    }

}
