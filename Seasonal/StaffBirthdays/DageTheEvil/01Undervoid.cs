/*
name: Undervoid Story
description: This will complete the Undervoid story.
tags: story, quest, void, nation, legion, underworld, staff, birthday
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/StaffBirthdays/DageTheEvil/CoreDageBirthday.cs
using Skua.Core.Interfaces;

public class UndervoidStory
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    private CoreDageBirthday Dage = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Dage.Undervoid();

        Core.SetOptions(false);
    }

}
