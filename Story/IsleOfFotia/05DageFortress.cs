/*
name: Dage Fortress
description: This will finish the DageFortress quest.
tags: story, quest, isle-of-fotia, dage-fortress
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/IsleOfFotia/CoreIsleOfFotia.cs

using Skua.Core.Interfaces;

public class DageFortress
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreIsleOfFotia CoreIsleOfFotia = new();


    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        CoreIsleOfFotia.DageFortress();

        Core.SetOptions(false);
    }

}
