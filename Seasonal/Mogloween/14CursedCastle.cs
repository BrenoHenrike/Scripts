/*
name: Cursed Castle Story
description: This will complete the cursed castle Story quest.
tags: story, quest, mogloween, seasonal, cursed, castle
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/Mogloween/CoreMogloween.cs
using Skua.Core.Interfaces;

public class CursedCastle
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreMogloween CoreMogloween = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        CoreMogloween.CursedCastle();

        Core.SetOptions(false);
    }

}
