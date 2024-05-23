/*
name: Astravia
description: This will finish the Astravia quest.
tags: story, quest, elegy-of-madness, darkon, astravia
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/ElegyofMadness(Darkon)/CoreAstravia.cs
using Skua.Core.Interfaces;

public class CompleteAstraviaLocation
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreAstravia Astravia => new();
    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Astravia.Astravia();

        Core.SetOptions(false);
    }
}
