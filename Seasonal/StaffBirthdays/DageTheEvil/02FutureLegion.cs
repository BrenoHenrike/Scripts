/*
name: Future Legion Story
description: This will complete the Future Legion story.
tags: story, quest, future, legion, legion, staff, birthday
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/StaffBirthdays/DageTheEvil/CoreDageBirthday.cs
using Skua.Core.Interfaces;

public class FutureLegion
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreDageBirthday Dage = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Dage.FutureLegion();
        Core.SetOptions(false);
    }


}
