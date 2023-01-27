/*
name:  Darkons Second Errand
description:  Darkons Second Errand
tags: darkon, errand, second
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Darkon/CoreDarkon.cs
//cs_include Scripts/Story/ElegyofMadness(Darkon)/CoreAstravia.cs
using Skua.Core.Interfaces;

public class SecondErrand
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDarkon Darkon = new CoreDarkon();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Darkon.SecondErrand();

        Core.SetOptions(false);
    }
}
