/*
name: Dage Birthday (All)
description: This script will complete all the seasonal Dage Birthday storylines.
tags: darkpath, dark path, futurelegion, future legion, undervoid, under void, legionbarracks, legion barracks, cocytus, cocytus barracks, cocytusbarracks, dage, seasonal, birthday, march, story, all
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/StaffBirthdays/DageTheEvil/CoreDageBirthday.cs
using Skua.Core.Interfaces;

public class DageBirthdayAll
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreDageBirthday Dage = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Dage.DoAll();
        Core.SetOptions(false);
    }
}
