/*
name: Astravia Judgement
description: This will finish the Astravia Judgement quest.
tags: story, quest, elegy-of-madness, darkon, astravia, judgement
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/ElegyofMadness(Darkon)/CoreAstravia.cs
using Skua.Core.Interfaces;

public class CompleteAstraviaJudgement
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreAstravia Astravia => new();
    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Astravia.AstraviaJudgement();

        Core.SetOptions(false);
    }
}
