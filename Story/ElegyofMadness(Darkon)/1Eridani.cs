/*
name: Erindani
description: This will finish the Eridani quest.
tags: story, quest, elegy-of-madness, darkon, eridani
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/ElegyofMadness(Darkon)/CoreAstravia.cs
using Skua.Core.Interfaces;

public class CompleteEridani
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreAstravia Astravia => new();
    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Astravia.Eridani();

        Core.SetOptions(false);
    }
}
