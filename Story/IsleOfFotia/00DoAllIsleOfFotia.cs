/*
name: Complete Isle Of Fotia Story
description: This will complete the Isle Of Fotia story.
tags: story, quest, isle-of-fotia, complete, all
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/IsleOfFotia/CoreIsleOfFotia.cs
using Skua.Core.Interfaces;

public class DoAllIsleOfFotia
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreIsleOfFotia CoreIsleOfFotia = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        CoreIsleOfFotia.CompleteALL();

        Core.SetOptions(false);
    }
}
